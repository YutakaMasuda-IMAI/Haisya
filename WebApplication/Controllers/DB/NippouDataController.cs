using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    [ApiController]
    [Route("NippouData")]
    public class NippouDataController : MyBaseController
    {
        private readonly ILogger<NippouDataController> _logger;
        private readonly IReceiptService _receiptService;

        public NippouDataController(ILogger<NippouDataController> logger, ApplicationDbContext context, IReceiptService receiptService)
        {
            _logger = logger;
            _context = context;
            _receiptService = receiptService;
        }

        /// <summary>
        /// コメントの登録
        /// </summary>
        /// <param name="jsonString">JSON形式のコメントデータ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertCommentAsync")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertCommentAsync(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                T_Nippou dataDto = GetMultipartFormDataContentData<T_Nippou>("name");

                await _receiptService.InsertCommentAsync(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// 受領の登録
        /// </summary>
        /// <param name="jsonString">JSON形式の受領データ</param>
        /// <returns>登録結果</returns>
        [HttpPost("InsertReceiptAsync")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertReceiptAsync(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out var abc);
                T_Nippou dataDto = System.Text.Json.JsonSerializer.Deserialize<T_Nippou>(abc);

                await _receiptService.InsertReceiptAsync(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// T_Nippouのリストを検索して返却
        /// </summary>
        /// <param name="t_Nippous">T_Nippouのリスト</param>
        /// <returns>検索結果</returns>
        [HttpPost("SearchTNippouAsync")]
        public async Task<IActionResult> SearchTNippouAsync([FromBody] List<T_Nippou> t_Nippous)
        {
            try
            {
                List<T_Nippou> Nippou = await _receiptService.GetTNippouAsync(t_Nippous);
                return new OkObjectResult(Nippou);

            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }
    }
}
