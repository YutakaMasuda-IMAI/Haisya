using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 請求データを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceDataController : ControllerBase
    {
        private readonly ILogger<MasterDataController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceService _service;

        /// <summary>
        /// InvoiceDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="service">請求サービス</param>
        public InvoiceDataController(ILogger<MasterDataController> logger, ApplicationDbContext context, IInvoiceService service)
            => (_logger, _context, _service) = (logger, context, service);

        /// <summary>
        /// 請求書の発行業務プロセス
        /// </summary>
        /// <returns>結果</returns>
        [HttpPost("Publish")]
        public async Task<MsterDataCommonResultValDto> Publish()
        {
            using InvoiceDataModel m = new(_context, _service);
            return await Publish<InvoicePublish>(d => m.Publish(d));
        }

        /// <summary>
        /// 請求書チェックの発行業務プロセス
        /// </summary>
        [HttpPost("PublishCheck")]
        public async Task<MsterDataCommonResultValDto> PublishCheck()
        {
            using InvoiceDataModel m = new(_context, _service);
            return await Publish<InvoiceCheckPublish>(d => m.PublishCheck(d));
        }

        /// <summary>
        /// 発行業務プロセスを実行する
        /// </summary>
        /// <typeparam name="T">発行データの型</typeparam>
        /// <param name="publishing_process">発行プロセス</param>
        /// <returns>結果</returns>
        private async Task<MsterDataCommonResultValDto> Publish<T>(Func<T, Task> publishing_process)
        {
            MsterDataCommonResultValDto r = new();
            try
            {
                IFormCollection form = await Request.ReadFormAsync();
                if (!form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues json))
                {
                    throw new Exception("name無し");
                }

                InvoiceDataModel m = new(_context, _service);
                T data = JsonSerializer.Deserialize<T>(json);
                await publishing_process(data);
                r.RetrunFlg = true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                Response.StatusCode = x is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                r.ErrrMessage = x.Message;
            }
            finally
            {
            }
            return r;
        }

        /// <summary>
        /// 請求データリストを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月 (yyyy-MM-dd)</param>
        /// <param name="closing_days">締日</param>
        /// <param name="seikyu_tantou">請求担当</param>
        /// <param name="tax_category">税区分</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日 (yyyy-MM-dd)</param>
        /// <param name="sale_date">売上年月日（まで）(yyyy-MM-dd)</param>
        /// <returns>請求データリスト</returns>
        [HttpGet("GetInvoiceDataList")]
        public async Task<IActionResult> GetInvoiceDataList(
            int company_id,
            string year_month,
            int closing_days,
            int seikyu_tantou,
            int customer_id1,
            int customer_id2,
            string publish_date,
            string sale_date)
        {
            IEnumerable<V_InvoiceDataList> r;
            InvoiceDataModel m = new(_context, _service);
            try
            {
                // パラメータを元に請求データリストを取得
                r = await m.GetInvoiceDataList(
                    company_id,
                    year_month,
                    closing_days,
                    seikyu_tantou,
                    customer_id1,
                    customer_id2,
                    publish_date,
                    sale_date);
                return new OkObjectResult(r);
            }
            // エラーが発生した場合
            catch (Exception x)
            {
                return CommonHelper.HandleError(x);
            }
        }

        /// <summary>
        /// 請求書チェックデータリストを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月 (yyyy-MM-dd)</param>
        /// <param name="closing_days">締日</param>
        /// <param name="seikyu_tantou">請求担当</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日 (yyyy-MM-dd)</param>
        /// <param name="sale_date">売上年月日（まで）(yyyy-MM-dd)</param>
        /// <returns>請求チェックデータリスト</returns>
        [HttpGet("GetInvoiceCheckDataList")]
        public async Task<IActionResult> GetInvoiceCheckDataList(
            int company_id,
            string year_month,
            int closing_days,
            int seikyu_tantou,
            int customer_id1,
            int customer_id2,
            string publish_date,
            string sale_date)
        {
            IEnumerable<V_InvoiceCheckDataList> r;
            InvoiceDataModel m = new(_context, _service);
            // 請求チェックデータリストを取得する
            try
            {
                r = await m.GetInvoiceCheckDataList(
                    company_id,
                    year_month,
                    closing_days,
                    seikyu_tantou,
                    customer_id1,
                    customer_id2,
                    publish_date,
                    sale_date);
                return new OkObjectResult(r);
            }
            // エラーが発生した場合
            catch (Exception x)
            {
                return CommonHelper.HandleError(x);
            }
        }

        /// <summary>
        /// エラーを返す
        /// </summary>
        /// <param name="x">例外</param>
        protected IActionResult Error(Exception x) =>
            new ObjectResult(x.Message)
            {
                StatusCode = x is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError
            };
    }
}
