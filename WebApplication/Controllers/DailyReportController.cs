using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Services;
using Microsoft.AspNetCore.Http;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 日報に関する操作を提供するコントローラークラス。
    /// </summary>
    [ApiController]
    [Route("DailyReport")]
    public class DailyReportController : MyBaseController
    {
        private readonly ILogger<DailyReportController> _logger;
        private readonly IDailyReportRegistrationDetailService _dailyReportRegistrationDetailService;
        private readonly IDailyReportService _dailyReportService;

        /// <summary>
        /// DailyReportControllerのコンストラクタ。
        /// </summary>
        /// <param name="logger">ロガーインスタンス。</param>
        /// <param name="context">データベースコンテキスト。</param>
        /// <param name="mapApiSettings">マップAPI設定。</param>
        /// <param name="dailyReportRegistrationDetailService">日報登録詳細サービス。</param>
        /// <param name="dailyReportService">日報サービス。</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト。</param>
        public DailyReportController(ILogger<DailyReportController> logger, ApplicationDbContext context,
            IOptions<MapApiSettings> mapApiSettings, IDailyReportRegistrationDetailService dailyReportRegistrationDetailService,
            IDailyReportService dailyReportService, ApplicationDbContextKintai contextKintai)
        {
            _logger = logger;
            _context = context;
            _mapApiSettings = mapApiSettings.Value;
            _contextKintai = contextKintai;
            _dailyReportRegistrationDetailService = dailyReportRegistrationDetailService;
            _dailyReportService = dailyReportService;
        }

        /// <summary>
        /// 日報登録（案件）の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件ID。</param>
        /// <param name="AnkenDisplay_ID">案件配車用ID。</param>
        /// <param name="DriverCd">乗務員CD（デジタコデータ取得で使用）。</param>
        /// <param name="Syaban">車番CD（デジタコデータ取得で使用）。</param>
        /// <param name="KokyakuId">顧客ID（任意）。</param>
        /// <param name="Driver_ID">ドライバーID（任意）。</param>
        /// <param name="Haisya_Kubun">配車区分（任意）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（任意）。</param>
        /// <param name="Haisya_ID">配車ID（任意）。</param>
        /// <param name="StartDatetime">開始日時。</param>
        /// <param name="EndDatetime">終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportRegistrationDetailModel"/>を返します。</returns>
        [HttpGet("GetDailyReportAnken")]
        public async Task<IActionResult> GetDailyReportAnken(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban,
                                        int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID,
                                        DateTime StartDatetime, DateTime EndDatetime)
        {
            try
            {
                var DailyReportRegistrationDetail = await _dailyReportRegistrationDetailService.GetDailyReportRegistrationAnken(Anken_ID, AnkenDisplay_ID,
                    DriverCd, Syaban, KokyakuId, Driver_ID, Haisya_Kubun, SyaryoManagement_ID, Haisya_ID,
                    StartDatetime, EndDatetime);
                return new OkObjectResult(DailyReportRegistrationDetail);
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
        /// 日報登録の詳細情報を取得します。
        /// </summary>
        /// <param name="Anken_ID">案件ID。</param>
        /// <param name="AnkenDisplay_ID">案件配車用ID。</param>
        /// <param name="DriverCd">乗務員CD（デジタコデータ取得で使用）。</param>
        /// <param name="Syaban">車番CD（デジタコデータ取得で使用）。</param>
        /// <param name="KokyakuId">顧客ID（任意）。</param>
        /// <param name="Driver_ID">ドライバーID（任意）。</param>
        /// <param name="Haisya_Kubun">配車区分（任意）。</param>
        /// <param name="SyaryoManagement_ID">車両管理ID（任意）。</param>
        /// <param name="Haisya_ID">配車ID（任意）。</param>
        /// <param name="StartDatetime">開始日時。</param>
        /// <param name="EndDatetime">終了日時。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="DailyReportRegistrationDetailModel"/>を返します。</returns>
        [HttpGet("GetDailyReportDetail")]
        public async Task<IActionResult> GetDailyReportDetail(int Anken_ID, int AnkenDisplay_ID, int DriverCd, int Syaban,
                                        int? KokyakuId, int? Driver_ID, int? Haisya_Kubun, int? SyaryoManagement_ID, int? Haisya_ID,
                                        DateTime StartDatetime, DateTime EndDatetime)
        {
            try
            {
                var DailyReportRegistrationDetail = await _dailyReportRegistrationDetailService.GetDailyReportRegistrationDetail(Anken_ID, AnkenDisplay_ID,
                    DriverCd, Syaban, KokyakuId, Driver_ID, Haisya_Kubun, SyaryoManagement_ID, Haisya_ID,
                    StartDatetime, EndDatetime);
                return new OkObjectResult(DailyReportRegistrationDetail);
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
        /// CertificationRequestModelを取得します。
        /// </summary>
        /// <param name="Nippou_Approval_ID">日報承認ID。</param>
        /// <returns>非同期操作を表すタスク。結果として<see cref="CertificationRequestModel"/>を返します。</returns>
        [HttpGet("GetCertificationRequestModel")]
        public async Task<IActionResult> GetCertificationRequestModel(int Nippou_Approval_ID)
        {
            try
            {
                var CertificationRequestModel = await _dailyReportRegistrationDetailService.GetCertificationRequestModel(Nippou_Approval_ID);
                return new OkObjectResult(CertificationRequestModel);
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
        /// CertificationRequestModelデータを投稿します。
        /// </summary>
        /// <returns>非同期操作を表すタスク。成功時に`true`を返します。</returns>
        [HttpPost("PostCertificationReques")]
        public async Task<Dto.MsterDataCommonResultValDto> PostCertificationReques()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                CertificationRequestModel dataDto = GetMultipartFormDataContentData<CertificationRequestModel>("name");

                var Jiko_ID = await _dailyReportRegistrationDetailService.PostCertificationRequest(dataDto);
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
        /// DailyReportRegistrationDetailModelデータを投稿します。
        /// </summary>
        /// <returns>非同期操作を表すタスク。成功時に`true`を返します。</returns>
        [HttpPost("PostProvisionalRegistration")]
        public async Task<Dto.MsterDataCommonResultValDto> PostProvisionalRegistration()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                DailyReportRegistrationDetailModel dataDto = GetMultipartFormDataContentData<DailyReportRegistrationDetailModel>("name");

                var Jiko_ID = await _dailyReportRegistrationDetailService.ProvisionalRegistration(dataDto);
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
        /// 日報データを取得します。
        /// </summary>
        /// <returns>取得した日報データのリスト (List&lt;T_Nippou&gt;) を表すTask。</returns>
        /// <remarks>
        /// データベースから全ての日報データを取得し、クライアントに返します。
        /// 特定の条件に基づいたフィルタリングは、このメソッドには含まれていません。
        /// </remarks>
        [HttpGet("GetTNippous")]
        public async Task<IActionResult> GetTNippous()
        {
            try
            {
                var nippous = await _dailyReportService.GetTNippous();
                return new OkObjectResult(nippous);
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
        /// 指定AnkenDisplay_IDの日報データを取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件表示車用ID。</param>
        /// <returns>取得した日報データを表すTask。</returns>
        /// <remarks>
        /// データベースから指定AnkenDisplay_IDの日報データを取得し、クライアントに返します。
        /// 特定の条件に基づいたフィルタリングは、このメソッドには含まれていません。
        /// </remarks>
        [HttpGet("GetNippou")]
        public async Task<IActionResult> GetNippou(int AnkenDisplay_ID)
        {
            try
            {
                var nippous = await _dailyReportService.GetNippou(AnkenDisplay_ID);
                return new OkObjectResult(nippous);
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
