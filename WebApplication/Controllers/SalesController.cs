using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 売上に関する操作を提供するコントローラークラス。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly MapApiSettings _mapApiSettings;
        private readonly ISalesService _salesService;

        /// <summary>
        /// SalesControllerのコンストラクタ。
        /// </summary>
        /// <param name="logger">ロガーインスタンス。</param>
        /// <param name="context">データベースコンテキスト。</param>
        /// <param name="mapApiSettings">マップAPI設定。</param>
        /// <param name="salesService">売上サービス。</param>
        public SalesController(ILogger<SalesController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings, ISalesService salesService)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
            _salesService = salesService;
        }

        /// <summary>
        /// 日報登録の情報の取得
        /// </summary>
        /// <param name="Anken_ID">案件ID。</param>
        /// <param name="KokyakuId">顧客ID。</param>
        /// <param name="Driver_ID">ドライバーID。</param>
        /// <param name="Haisya_ID">配車ID。</param>
        /// <returns>SalesModel</returns>
        [HttpGet("GetSale")]
        public async Task<IActionResult> GetSale(int Anken_ID, int KokyakuId, int Driver_ID, int Haisya_ID)
        {
            try
            {
                SalesModel Sales = await _salesService.GetSales(Anken_ID, KokyakuId, Driver_ID, Haisya_ID);
                return new OkObjectResult(Sales);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 売上の情報の取得
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>SalesModel</returns>
        [HttpGet("GetUriage")]
        public async Task<IActionResult> GetUriage(int Uriage_ID)
        {
            try
            {
                SalesModel Sales = await _salesService.GetUriages(Uriage_ID);
                return new OkObjectResult(Sales);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Customer_ID">顧客ID。</param>
        /// <returns>Customer</returns>
        [HttpGet("GetCustome")]
        public async Task<IActionResult> GetCustome(int Customer_ID)
        {
            try
            {
                M_Customer_Branch Customer = await _salesService.GetCustomer(Customer_ID);
                return new OkObjectResult(Customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 専属情報の取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID。</param>
        /// <param name="date">日付。</param>
        /// <returns>SenzokuData</returns>
        [HttpGet("GetSenzokuDat")]
        public async Task<IActionResult> GetSenzokuDat(int Senzoku_ID, DateTime? date)
        {
            try
            {
                SenzokuModel SenzokuData = await _salesService.GetSenzokuData(Senzoku_ID, date);
                return new OkObjectResult(SenzokuData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="jsonString">JSON文字列。</param>
        /// <returns>Dto.MsterDataCommonResultValDto</returns>
        [HttpPost("PostSale")]
        public async Task<Dto.MsterDataCommonResultValDto> PostSale(string jsonString)
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
                SalesModel dataDto = System.Text.Json.JsonSerializer.Deserialize<SalesModel>(abc);

                await _salesService.PostSales(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 売上情報の削除
        /// </summary>
        /// <param name="jsonString">JSON文字列。</param>
        /// <returns>Dto.MsterDataCommonResultValDto</returns>
        [HttpPost("DeleteSale")]
        public async Task<Dto.MsterDataCommonResultValDto> DeleteSale(string jsonString)
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
                SalesModel dataDto = System.Text.Json.JsonSerializer.Deserialize<SalesModel>(abc);

                await _salesService.DeleteSales(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 専属と顧客情報のみの取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID。</param>
        /// <returns>SenzokuData</returns>
        [HttpGet("GetSenzok")]
        public async Task<IActionResult> GetSenzoku(int Senzoku_ID)
        {
            try
            {
                SenzokuModel Senzoku = await _salesService.GetSenzoku(Senzoku_ID);
                return new OkObjectResult(Senzoku);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 売上情報の登録（月額専属）
        /// </summary>
        /// <param name="jsonString">JSON文字列。</param>
        /// <returns>Dto.MsterDataCommonResultValDto</returns>
        [HttpPost("PostUriageDat")]
        public async Task<Dto.MsterDataCommonResultValDto> PostUriageDat(string jsonString)
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
                PostUriageDataModel dataDto = System.Text.Json.JsonSerializer.Deserialize<PostUriageDataModel>(abc);

                await _salesService.PostUriageData(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// T_Uriageの確定登録
        /// </summary>
        /// <param name="jsonString">JSON文字列。</param>
        /// <returns>Dto.MsterDataCommonResultValDto</returns>
        [HttpPost("PostKakuteiUriageDat")]
        public async Task<Dto.MsterDataCommonResultValDto> PostKakuteiUriageDat(string jsonString)
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
                List<int> dataDto = System.Text.Json.JsonSerializer.Deserialize<List<int>>(abc);

                await _salesService.PostKakuteiUriageData(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 請求先の締日を取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>SenzokuData</returns>
        [HttpGet("GetCommitSeikyuShimeTime")]
        public async Task<IActionResult> GetCommitSeikyuShimeTime(int Customer_ID)
        {
            try
            {
                DateTime? Senzoku = await _salesService.GetCommitSeikyuShimeTime(Customer_ID);
                return new OkObjectResult(Senzoku);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 下払先の締日を取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>SenzokuData</returns>
        [HttpGet("GetCommitShitabaraiShimeTime")]
        public async Task<IActionResult> GetCommitShitabaraiShimeTime(int Customer_ID)
        {
            try
            {
                DateTime? Senzoku = await _salesService.GetCommitShitabaraiShimeTime(Customer_ID);
                return new OkObjectResult(Senzoku);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }
    }
}
