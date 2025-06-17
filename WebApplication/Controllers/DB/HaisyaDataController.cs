using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;
using WebApplication.Services;
using static WebApplication.Model.HaisyaDataModel;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 配車データを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HaisyaDataController : MyBaseController
    {
        private readonly ILogger<HaisyaDataController> _logger;
        private readonly IOperationInstructionsService _operationInstructionsService;
        private readonly ISyabanRenrakuService _syabanRenrakuService;

        /// <summary>
        /// HaisyaDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト</param>
        /// <param name="syabanRenrakuService">車番連絡サービス</param>
        /// <param name="operationInstructionsService">運行指示サービス</param>
        public HaisyaDataController(ILogger<HaisyaDataController> logger, ApplicationDbContext context,
            ApplicationDbContextKintai contextKintai, ISyabanRenrakuService syabanRenrakuService,
            IOperationInstructionsService operationInstructionsService)
        {
            _logger = logger;
            _context = context;
            _contextKintai = contextKintai;
            _operationInstructionsService = operationInstructionsService;
            _syabanRenrakuService = syabanRenrakuService;
        }

        #region HaisyaData

        /// <summary>
        /// 指定された条件で配車表示リストを取得します。
        /// </summary>
        /// <param name="targetDateFrom">開始日</param>
        /// <param name="targetDateTo">終了日</param>
        /// <param name="companyId">会社ID</param>
        /// <returns>配車表示リスト</returns>
        [HttpGet("GetHaisyaDisplayList")]
        public async Task<IActionResult> GetHaisyaDisplayList(string targetDateFrom, string targetDateTo, int companyId = 0)
        {
            IEnumerable<T_Anken_Display> resultVal = null;

            try
            {
                HaisyaDataModel model = new(_context);
                resultVal = model.GetAnkenDisplayList(targetDateFrom, targetDateTo, companyId);
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
        /// 配車データを取得する
        /// </summary>
        /// <param name="haisyaId">配車のId</param>
        /// <returns>配車データ</returns>
        [HttpGet("GetHaisyaData")]
        public async Task<IActionResult> GetHaisyaData(int haisyaId)
        {
            T_Haisya resultVal = null;

            try
            {
                resultVal = await _context.T_Haisyas.Where(m => m.Haisya_ID == haisyaId).FirstOrDefaultAsync();

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
        /// V_HaisyaDataListを返却する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="targetDate">対象日</param>
        /// <param name="targetDateFrom">開始日</param>
        /// <param name="targetDateTo">終了日</param>
        /// <param name="customerId">顧客ID</param>
        /// <param name="ankenId">案件ID</param>
        /// <param name="senzokuId">専属ID</param>
        /// <param name="driverId">ドライバーID</param>
        /// <param name="ankenDisplayId">案件表示ID</param>
        /// <param name="targetDateUnderLastest">指定日以下で指定日以降終了の最新</param>
        /// <returns>配車データリスト</returns>
        [HttpGet("GetHaisyaDataList")]
        public async Task<IActionResult> GetHaisyaDataList(int companyId, string targetDate,
                                            string targetDateFrom, string targetDateTo, int customerId,
                                            int ankenId = 0, int senzokuId = 0, int driverId = 0, int ankenDisplayId = 0,
                                            string targetDateUnderLastest = null)
        {
            IEnumerable<V_HaisyaDataList> resultVal = null;

            try
            {
                HaisyaDataModel model = new(_context);
                resultVal = await model.GetHaisyaDataList(companyId, customerId, 0, 0,
                                        targetDate, targetDateFrom, targetDateTo, senzokuId, ankenId, 0, 0, driverId, ankenDisplayId, null, targetDateUnderLastest);
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
        /// 指定条件で車番連絡リストを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="customerId">顧客ID</param>
        /// <param name="customerTantouId">顧客担当ID</param>
        /// <param name="targetDate">対象日</param>
        /// <param name="groupID">グループID</param>
        /// <param name="Ids">IDリスト</param>
        /// <returns>車番連絡リスト</returns>
        [HttpGet("GetSyabanRenrakuList")]
        public async Task<IActionResult> GetSyabanRenrakuList(int companyId, int customerId,
                                                                        int customerTantouId, String targetDate, int groupID, int Ids)
        {
            IEnumerable<V_HaisyaDataList> resultVal = null;

            try
            {
                HaisyaDataModel model = new(_context);
                resultVal = await model.GetHaisyaDataList(companyId, customerId, customerTantouId, 0, targetDate, null, null, 0, 0, 0, groupID, 0, 0, Ids.ToString());
                foreach (var item in resultVal)
                {
                    string remarks = await _syabanRenrakuService.GetSyabanRenrakuRemarks(item.AnkenDisplay_ID);
                    item.Remarks = remarks;
                }

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
        /// 指定条件でT_Haisya_Driver_Day_Remarkを返却する
        /// </summary>
        /// <param name="targetDay"></param>
        /// <param name="DriverId"></param>
        /// <returns></returns>
        [HttpGet("GetHaisyaDriverDayRemarks")]
        public async Task<IActionResult> GetHaisyaDriverDayRemarks(string targetDay, int DriverId = 0)
        {
            try
            {
                IQueryable<Data.T_Haisya_Driver_Day_Remark> query = _context.T_Haisya_Driver_Day_Remarks.Where(m => m.Date == DateTime.Parse(targetDay));
                if (DriverId > 0) query.Where(m => m.Driver_ID == DriverId);
                List<Data.T_Haisya_Driver_Day_Remark> resultVal = await query.ToListAsync();
                return new OkObjectResult(resultVal);
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
        /// 新規配車割当て処理
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddNewHaisyaData")]
        public async Task<Dto.MsterDataCommonResultValDto> AddNewHaisyaData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                HaisyaDataModelDto dataDto = GetMultipartFormDataContentData<HaisyaDataModelDto>("name");

                HaisyaDataModel model = new(_context);
                T_Haisya t_Haisya = await model.AddNewHaisyaData(dataDto);
                resultVal.RetrunFlg = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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
        /// 配車割当て解除処理
        /// </summary>
        /// <returns></returns>
        [HttpPost("UnassignHaisyaData")]
        public async Task<Dto.MsterDataCommonResultValDto> UnassignHaisyaData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                T_Haisya dataDto = GetMultipartFormDataContentData<T_Haisya>("name");

                HaisyaDataModel model = new(_context);
                await model.UnassignHaisyaData(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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
        /// 配車割当ての移動処理
        /// </summary>
        /// <returns></returns>
        [HttpPost("MoveHaisyaData")]
        public async Task<Dto.MsterDataCommonResultValDto> MoveHaisyaData()
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                HaisyaDataModelDto dataDto = GetMultipartFormDataContentData<HaisyaDataModelDto>("name");

                HaisyaDataModel model = new(_context);
                T_Haisya t_Haisya = await model.MoveHaisyaData(dataDto);
                resultVal.RetrunFlg = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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
        /// 配車ステータスの更新
        /// </summary>
        /// <param name="HaisyaID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpGet("UpdateStatusHaisya")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateStatusHaisya(int HaisyaID, int Status)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                HaisyaDataModel model = new(_context);
                await model.UpdateStatusHaisya(HaisyaID, Status);
                resultVal.RetrunFlg = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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
        /// 指定AnkenIDの配車済み判定を返却
        /// </summary>
        /// <param name="AnkenID"></param>
        /// <returns></returns>
        [HttpGet("CheckEnableContact")]
        public async Task<IActionResult> CheckEnableContact(int AnkenID)
        {
            ContactAbility resultVal;

            try
            {
                HaisyaDataModel model = new(_context);

                resultVal = await model.CheckEnableContact(AnkenID);

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
        /// 案件に対する出発地点、戻り地点の配車データの取得処理
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("GetHaisyaAroundList")]
        public async Task<IActionResult> GetHaisyaAroundList(String targetDate, int companyId)
        {
            List<T_Haisya_Around> resultVal = null;

            try
            {
                resultVal = await _context.T_Haisya_Arounds.Where(m => m.Day == DateTime.Parse(targetDate) && m.Company_ID == companyId).ToListAsync();

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
        /// 運行指示書を取得
        /// </summary>
        /// <param name="ankenId"></param>
        /// <returns></returns>
        [HttpGet("GetOperationInstructions")]
        public async Task<IActionResult> GetOperationInstructions(int ankenId)
        {
            OperationInstructionsModel resultVal;
            try
            {
                resultVal = await _operationInstructionsService.GetOperationInstructions(ankenId);

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
        /// 車番連絡を取得
        /// </summary>
        /// <param name="selectTantou"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetSyabanRenraku")]
        public async Task<IActionResult> GetSyabanRenraku(int companyID, int selectTantou, DateTime selectedDate, DateTime selectedEndDate, int filter)
        {
            List<SyabanRenrakuModel> resultVal;

            try
            {
                resultVal = await _syabanRenrakuService.GetSyabanRenrakuAsync(companyID, selectTantou, selectedDate, selectedEndDate, filter);

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
        /// 車番連絡情報を登録します。
        /// </summary>
        /// <param name="syabanRenrakuPostModel">登録する車番連絡情報のリスト。</param>
        /// <returns>
        /// 登録が成功した場合、ステータスコード200 (OK) とメッセージ "success" を返します。
        /// </returns>
        /// <remarks>
        /// このメソッドは、複数の車番連絡情報を一度に登録するために使用されます。  
        /// 登録に失敗した場合、適切なエラーメッセージを返すように拡張することが可能です。
        /// </remarks>
        [HttpPost("PostSyabanRenraku")]
        public async Task<object> PostSyabanRenraku([FromBody] List<SyabanRenrakuPostModel> syabanRenrakuPostModel)
        {
            try
            {
                await _syabanRenrakuService.PostSyabanRenrakuAsync(syabanRenrakuPostModel);
                return Ok($"success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 傭車ドライバーの配車データを登録する
        /// </summary>
        /// <returns></returns>
        [HttpPost("RegisterYosyaDriver")]
        public async Task<Dto.MsterDataCommonResultValDto> RegisterYosyaDriver()
        {

            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                // パラメーターの取得
                HaisyaYosyaDriverRegisterDto dataDto = GetMultipartFormDataContentData<HaisyaYosyaDriverRegisterDto>("name");

                using HaisyaDataModel model = new(_context);
                resultVal = await model.ExecRegisterYosyaDriver(dataDto.loginUser, dataDto.AnkenDisplayID, dataDto.HaisyaYosya);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                resultVal.ErrrMessage = ex.Message;
            }
            return resultVal;
        }


        #endregion

        #region T_Haisya_Yosya
        [HttpGet("GetHaisyaYosyaData")]
        public async Task<IActionResult> GetHaisyaYosyaData(int HaisyaId)
        {
            try
            {
                IQueryable<Data.T_Haisya_Yosya> query = _context.T_Haisya_Yosyas.Where(m => m.Haisya_ID == HaisyaId);
                Data.T_Haisya_Yosya resultVal = await query.FirstOrDefaultAsync();
                return new OkObjectResult(resultVal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                return CommonHelper.HandleError(ex);
            }
        }
        #endregion T_Haisya_Yosya

    }
}
