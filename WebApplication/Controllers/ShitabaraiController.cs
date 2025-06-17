using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Repositories;
using WebApplication.Services;
using Moq;
using Microsoft.AspNetCore.Http;
using WebApplication.Common;


namespace WebApplication.Controllers
{
    /// <summary>
    /// 下払いコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShitabaraiController : ControllerBase
    {
        private readonly ILogger<ShitabaraiController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly MapApiSettings _mapApiSettings;

        private readonly IShitabaraiService _shitabaraiService;

        public ShitabaraiController(ILogger<ShitabaraiController> logger, ApplicationDbContext context, IOptions<MapApiSettings> mapApiSettings, IShitabaraiService shitabaraiService)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
            _shitabaraiService = shitabaraiService;

        }

        /// <summary>
        /// 下払いチェックデータリストの取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="printDate">印刷日</param>
        /// <param name="yosyasakiFrom">傭車先開始</param>
        /// <param name="yosyasakiTo">傭車先終了</param>
        /// <param name="shiharaiNengetu">支払年月</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shiharaiDateTo">支払日終了</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="TakeNum">取得件数</param>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>データリスト</returns>
        [HttpGet("GetShitabaraiCheckDataListEx")]
        public async Task<IActionResult> GetShitabaraiCheckDataList(int CompanyID, DateTime? printDate, string yosyasakiFrom, string yosyasakiTo, DateTime? shiharaiNengetu, int? shimeDay, int? zeiKubun, DateTime? shiharaiDateTo, string shiharaiTantou, int TakeNum = 100, int checkShitabaraiId = 0)
        {
            try
            {
                List<V_ShitabaraiCheckDataList> shitabaraiCheckDataList = await _shitabaraiService.GetShitabaraiCheckDataList(CompanyID, printDate, yosyasakiFrom, yosyasakiTo, shiharaiNengetu, shimeDay, zeiKubun, shiharaiDateTo, shiharaiTantou, checkShitabaraiId);
                return new OkObjectResult(shitabaraiCheckDataList);
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
        /// 下払い問い合わせ発行処理
        /// </summary>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns>結果</returns>
        [HttpPost("PublishShitabaraiCheckDataList")]
        public async Task<Dto.MsterDataCommonResultValDto> PublishShitabaraiCheckDataList(string jsonString)
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
                Dto.ShitabaraiInquiryPublishDto dataDto = System.Text.Json.JsonSerializer.Deserialize<Dto.ShitabaraiInquiryPublishDto>(abc);

                await _shitabaraiService.PublishShitabaraiCheckDataList(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// T_Check_Shitabaraiの取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTCheckShitabaraiById")]
        public async Task<IActionResult> GetTCheckShitabaraiById(int checkShitabaraiId)
        {
            try
            {
                T_Check_Shitabarai entity = await _shitabaraiService.GetTCheckShitabaraiById(checkShitabaraiId);
                return new OkObjectResult(entity);

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
        /// T_Check_Shitabarai_Detailの取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTCheckShitabaraiDetailById")]
        public async Task<IActionResult> GetTCheckShitabaraiDetailById(int checkShitabaraiId)
        {
            try
            {
                T_Check_Shitabarai_Detail entity = await _shitabaraiService.GetTCheckShitabaraiDetailById(checkShitabaraiId);
                return new OkObjectResult(entity);

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
        /// T_Check_Shitabarai_Detailを複数取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>エンティティリスト</returns>
        [HttpGet("GetTCheckShitabaraiDetailListById")]
        public async Task<IActionResult> GetTCheckShitabaraiDetailListById(int checkShitabaraiId)
        {
            try
            {
                List<T_Check_Shitabarai_Detail> entity = await _shitabaraiService.GetTCheckShitabaraiDetailListById(checkShitabaraiId);
                return new OkObjectResult(entity);

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
        /// T_Uriage_Shitabaraiの取得
        /// </summary>
        /// <param name="uriageShiharaiID">売上支払ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTUriageShitabaraiById")]
        public async Task<IActionResult> GetTUriageShitabaraiById(int uriageShiharaiID)
        {
            try
            {
                T_Uriage_Shitabarai entity = await _shitabaraiService.GetTUriageShitabaraiById(uriageShiharaiID);
                return new OkObjectResult(entity);

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
        /// T_Anken_Detailの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTAnkenDetailByUriageID")]
        public async Task<IActionResult> GetTAnkenDetailByUriageID(int uriageID)
        {
            try
            {
                T_Anken_Detail entity = await _shitabaraiService.GetTAnkenDetailByUriageID(uriageID);
                return new OkObjectResult(entity);

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
        /// M_Syaryoの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetMSyaryoByUriageID")]
        public async Task<IActionResult> GetMSyaryoByUriageID(int uriageID)
        {
            try
            {
                M_Syaryo entity = await _shitabaraiService.GetMSyaryoByUriageID(uriageID);
                return new OkObjectResult(entity);

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
        /// T_Uriageの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTUriageById")]
        public async Task<IActionResult> GetTUriageById(int uriageID)
        {
            try
            {
                T_Uriage entity = await _shitabaraiService.GetTUriageById(uriageID);
                return new OkObjectResult(entity);

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
        /// 指定された売上IDに基づいて、売上支払詳細情報を取得します。
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>売上支払詳細情報のエンティティ</returns>
        [HttpGet("GetTShitabaraiDetail")]
        public async Task<IActionResult> GetTShitabaraiDetail(int uriageID)
        {
            try
            {
                T_Shitabarai_Detail entity = await _shitabaraiService.GetTShitabaraiDetail(uriageID);
                return new OkObjectResult(entity);

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
        /// 指定された支払IDに基づいて、支払情報を取得します。
        /// </summary>
        /// <param name="shitabaraiId">支払ID</param>
        /// <returns>支払情報のエンティティ</returns>
        [HttpGet("GetTShitabarai")]
        public async Task<IActionResult> GetTShitabarai(int shitabaraiId)
        {
            try
            {
                T_Shitabarai entity = await _shitabaraiService.GetTShitabarai(shitabaraiId);
                return new OkObjectResult(entity);

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
        /// 指定された支払IDに基づいて、情報を取得します。
        /// </summary>
        /// <param name="shitabaraiID">支払ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetTYosyaShiharai")]
        public async Task<IActionResult> GetTYosyaShiharai(int shitabaraiID)
        {
            try
            {
                T_YosyaShiharai entity = await _shitabaraiService.GetTYosyaShiharai(shitabaraiID);
                return new OkObjectResult(entity);

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
        /// 指定されたチェック支払IDに基づいて、印刷支払情報を取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>印刷支払情報のエンティティ</returns>
        [HttpGet("GetTPrintShiharai")]
        public async Task<IActionResult> GetTPrintShiharai(int checkShitabaraiId)
        {
            try
            {
                T_Print_Shitabarai entity = await _shitabaraiService.GetTPrintShiharai(checkShitabaraiId);
                return new OkObjectResult(entity);

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
        /// 指定された印刷支払IDに基づいて、印刷支払詳細情報のリストを取得します。
        /// </summary>
        /// <param name="printShitabaraiId">印刷支払ID</param>
        /// <returns>印刷支払詳細情報のリスト</returns>
        [HttpGet("GetTPrintShiharaiDetail")]
        public async Task<IActionResult> GetTPrintShiharaiDetail(int printShitabaraiId)
        {
            try
            {
                List<T_Print_Shitabarai_Detail> entity = await _shitabaraiService.GetTPrintShiharaiDetail(printShitabaraiId);
                return new OkObjectResult(entity);

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
        /// 指定されたチェック支払IDおよびオプションの売上支払IDに基づいて、支払変更情報のリストを取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <param name="uriageShitabaraiId">オプションの売上支払ID</param>
        /// <returns>支払変更情報のリスト</returns>
        [HttpGet("GetTCheckShitabaraiChange")]
        public async Task<IActionResult> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null)
        {
            try
            {
                List<T_Check_Shitabarai_Change> entity = await _shitabaraiService.GetTCheckShitabaraiChange(checkShitabaraiId, uriageShitabaraiId);
                return new OkObjectResult(entity);

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
        /// 指定されたIDに基づいて、情報を取得します。
        /// </summary>
        /// <param name="yosyaBranchId">ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetMYosyaBranch")]
        public async Task<IActionResult> GetMYosyaBranch(int yosyaBranchId)
        {
            try
            {
                M_Yosya_Branch entity = await _shitabaraiService.GetMYosyaBranch(yosyaBranchId);
                return new OkObjectResult(entity);

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
        /// 下洗情報の登録
        /// 下払問合せ変更承認：一括確定処理
        /// 下払問合せ変更承認：暫定→確定切り替え
        /// </summary>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns>結果</returns>
        [HttpPost("ShitabaraiPostBatchRegistration")]
        public async Task<Dto.MsterDataCommonResultValDto> ShitabaraiPostBatchRegistration(string jsonString)
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
                Model.ShitabaraiBatchRegistrationModel dataDto = System.Text.Json.JsonSerializer.Deserialize<Model.ShitabaraiBatchRegistrationModel>(abc);
                resultVal.RetrunFlg = await _shitabaraiService.ShitabaraiPostBatchRegistration(dataDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// 下払問合せ変更承認：暫定←確定切り替え
        /// 下払情報のキャンセル
        /// </summary>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns>結果</returns>
        [HttpPost("ShitabaraiPostBatchCancel")]
        public async Task<Dto.MsterDataCommonResultValDto> ShitabaraiPostBatchCancel(string jsonString)
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
                Model.ShitabaraiBatchRegistrationModel dataDto = System.Text.Json.JsonSerializer.Deserialize<Model.ShitabaraiBatchRegistrationModel>(abc);

                // WEB時の処理
                if (dataDto.Check_Kubun == 1)
                {
                    await _shitabaraiService.ShitabaraiPostBatchCancelWeb(dataDto);
                }
                else if (dataDto.Check_Kubun == 2) // 帳票時の処理
                {
                    await _shitabaraiService.ShitabaraiPostBatchCancelNote(dataDto);
                }
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// 下払問合せ変更承認：下払明細　承認（保存）
        /// </summary>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns>結果</returns>
        [HttpPost("PostShitabaraiModalApproval")]
        public async Task<Dto.MsterDataCommonResultValDto> PostShitabaraiModalApproval(string jsonString)
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
                ShitabaraiModalApprovalModel dataDto = System.Text.Json.JsonSerializer.Deserialize<ShitabaraiModalApprovalModel>(abc);

                resultVal.RetrunFlg = await _shitabaraiService.PostShitabaraiModalApproval(dataDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }


        /// <summary>
        /// T_Check_Shitabarai_Doneを返却する
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック支払ID</param>
        /// <returns>エンティティ</returns>
        [HttpGet("GetT_Check_Shitabarai_Done")]
        public async Task<IActionResult> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID)
        {

            T_Check_Shitabarai_Done resultVal = null;

            try
            {
                IShitabaraiService shitabaraiService = _shitabaraiService;

                resultVal = await shitabaraiService.GetT_Check_Shitabarai_Done(Check_Shitabarai_ID);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
            finally
            {

            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Uriage・T_Check_Shitabarai_Detail・T_Check_Shitabaraiの更新
        /// </summary>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns>結果</returns>
        [HttpPost("UpdateApprovalStatusShitabarai")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateApprovalStatus(string jsonString)
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
                ShitabaraiApprovalStatusModel dataDto = System.Text.Json.JsonSerializer.Deserialize<ShitabaraiApprovalStatusModel>(abc);

                IShitabaraiService shitabaraiService = _shitabaraiService;

                // WEB時の処理
                if (dataDto.Check_Kubun == 1)
                {
                    await shitabaraiService.UpdateApprovalStatusForWeb(dataDto.Check_Shitabarai_ID, dataDto.Uriage_Shiharai_ID);
                }
                else if (dataDto.Check_Kubun == 2) // 帳票時の処理
                {
                    await shitabaraiService.UpdateApprovalStatusForChohyo(dataDto.Check_Shitabarai_ID, dataDto.Uriage_Shiharai_ID);
                }
                resultVal.RetrunFlg = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }
    }
}
