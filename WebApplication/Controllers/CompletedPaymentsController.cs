using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Services;
using Microsoft.AspNetCore.Http;
using System;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 完了した支払いを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompletedPaymentsController : ControllerBase
    {
        private readonly ICompletedPaymentsService _completedPaymentsService;

        public CompletedPaymentsController(ICompletedPaymentsService completedPaymentsService)
        {
            _completedPaymentsService = completedPaymentsService;
        }

        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("InsertTNyukin")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertTNyukin(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<T_Nyukin> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<T_Nyukin>>(abc);

                await _completedPaymentsService.InsertTNyukin(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("InsertOrUpdateTNyukin")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertOrUpdateTNyukin(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                List<T_Nyukin> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<T_Nyukin>>(abc);

                await _completedPaymentsService.InsertOrUpdateTNyukin(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// PaymentInputDataListデータの返却
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>PaymentInputDataList</returns>
        [HttpGet("GetPaymentInputDataLis")]
        public async Task<IActionResult> GetPaymentInputDataLis(int Seikyu_ID)
        {
            try
            {
                PaymentInputDataListModel PaymentInputDataList = await _completedPaymentsService.GetPaymentInputDataList(Seikyu_ID);
                return new OkObjectResult(PaymentInputDataList);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("PostPaymentInputDat")]
        public async Task<Dto.MsterDataCommonResultValDto> PostPaymentInputDat(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                PostPaymentInputDataModel dataDto = System.Text.Json.JsonSerializer.Deserialize<PostPaymentInputDataModel>(abc);

                await _completedPaymentsService.PostPaymentInputData(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }
    }
}
