using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 請求データのコントローラークラス
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SeikyuDataController : ControllerBase
    {
        private readonly ILogger<MasterDataController> _logger;
        private readonly IReceiptService _receiptService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="receiptService">レシートサービス</param>
        public SeikyuDataController(ILogger<MasterDataController> logger, ApplicationDbContext context, IReceiptService receiptService)
        {
            _logger = logger;
            _context = context;
            _receiptService = receiptService;
        }

        /// <summary>
        /// 案件データの一覧を返却する
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="seikyuNengetsu">請求年月</param>
        /// <param name="tokuisakiID">得意先ID</param>
        /// <param name="tokuisakiIDTo">得意先ID（終了）</param>
        /// <returns>案件データの一覧</returns>
        [HttpGet("GetSeikyuCheckDataList")]
        public async Task<IActionResult> GetSeikyuCheckDataList(int iCompanyID, string seikyuNengetsu, string tokuisakiID, string tokuisakiIDTo)
        {
            IEnumerable<V_SeikyuCheckDataList> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetSeikyuCheckDataList(iCompanyID, seikyuNengetsu, tokuisakiID, tokuisakiIDTo);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 案件データの一覧を返却する
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="seikyuNengetsu">請求年月</param>
        /// <param name="targetTokuisaki">対象得意先</param>
        /// <param name="toTokuisaki">得意先（終了）</param>
        /// <param name="selectSeikyuTantou">請求担当者</param>
        /// <param name="shimeDay">締日</param>
        /// <returns>案件データの一覧</returns>
        [HttpGet("GetSeikyuDataList")]
        public async Task<IActionResult> GetSeikyuDataList(int iCompanyID, string seikyuNengetsu, string targetTokuisaki, string toTokuisaki, string selectSeikyuTantou, string shimeDay)
        {
            IEnumerable<V_SeikyuDataList> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetSeikyuDataList(iCompanyID, seikyuNengetsu, targetTokuisaki, toTokuisaki, selectSeikyuTantou, shimeDay);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 指定条件の請求発行データリストを返却する
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="seikyuNengetsu">請求年月</param>
        /// <param name="targetTokuisaki">対象得意先</param>
        /// <param name="toTokuisaki">得意先（終了）</param>
        /// <param name="tokuisakiName">曖昧検索</param>
        /// <param name="shimeDay">締日</param>
        /// <returns>請求発行データリスト</returns>
        [HttpGet("GetSeikyuZumiDataList")]
        public async Task<IActionResult> GetSeikyuZumiDataList(int iCompanyID, string seikyuNengetsu, string targetTokuisaki, string toTokuisaki, string shimeDay, string tokuisakiName = null)
        {
            IEnumerable<V_SeikyuZumiDataList> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetSeikyuZumiDataList(iCompanyID, seikyuNengetsu, targetTokuisaki, toTokuisaki, tokuisakiName, shimeDay);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 集計値の一覧を返却する（月指定）
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>集計値の一覧</returns>
        [HttpGet("GetT_Print_Seikyu")]
        public async Task<IActionResult> GetT_Print_Seikyu(int Check_Seikyu_ID)
        {
            T_Print_Seikyu resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Print_Seikyu(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 集計値の一覧を返却する（月指定）
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>集計値の一覧</returns>
        [HttpGet("GetT_Print_SeikyuBySeikyuId")]
        public async Task<IActionResult> GetT_Print_SeikyuBySeikyuId(int Seikyu_ID = 0)
        {
            T_Print_Seikyu resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Print_SeikyuBySeikyuId(Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Print_Seikyu_Detailを返却する
        /// </summary>
        /// <param name="Print_Seikyu_ID">印刷請求ID</param>
        /// <returns>印刷請求詳細</returns>
        [HttpGet("GetT_Print_Seikyu_Detail")]
        public async Task<IActionResult> GetT_Print_Seikyu_Detail(int Print_Seikyu_ID)
        {
            IEnumerable<T_Print_Seikyu_Detail> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Print_Seikyu_Detail(Print_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 入金情報の一覧を返却する（月指定）
        /// </summary>
        /// <param name="Customer_Branch_ID">顧客支店ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締日</param>
        /// <returns>入金情報の一覧</returns>
        [HttpGet("GetT_Nyukin")]
        public async Task<IActionResult> GetT_Nyukin(int Customer_Branch_ID, DateTime Seikyu_Month, int Shime_Day)
        {
            IEnumerable<T_Nyukin> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Nyukin(Customer_Branch_ID, Seikyu_Month, Shime_Day);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// 入金情報の一覧を返却する（月指定）
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締日</param>
        /// <returns>入金情報の一覧</returns>
        [HttpGet("GetT_NyukinBySeikyuId")]
        public async Task<IActionResult> GetT_NyukinBySeikyuId(int Seikyu_ID, DateTime Seikyu_Month, int Shime_Day)
        {
            IEnumerable<T_Nyukin> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_NyukinBySeikyuId(Seikyu_ID, Seikyu_Month, Shime_Day);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Uriage_Unchinを返却する
        /// </summary>
        /// <param name="Uriage_Unchin_Id">売上運賃ID</param>
        /// <returns>売上運賃</returns>
        [HttpGet("GetT_Uriage_Unchin")]
        public async Task<IActionResult> GetT_Uriage_Unchin(int Uriage_Unchin_Id)
        {
            T_Uriage_Unchin resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Uriage_Unchin(Uriage_Unchin_Id);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Anken_Detailの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>案件詳細</returns>
        [HttpGet("GetTAnkenDetailByUriageID")]
        public async Task<IActionResult> GetTAnkenDetailByUriageID(int uriageID)
        {
            try
            {
                SeikyuDataModel model = new(_context);
                T_Anken_Detail entity = await model.GetTAnkenDetailByUriageID(uriageID);
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// M_SyaryoManagementの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>車両管理情報</returns>
        [HttpGet("GetMSyaryoManagementByUriageID")]
        public async Task<IActionResult> GetMSyaryoManagementByUriageID(int uriageID)
        {
            try
            {
                SeikyuDataModel model = new(_context);
                M_SyaryoManagement entity = await model.GetMSyaryoManagementByUriageID(uriageID);
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// M_Syaryoの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>車両情報</returns>
        [HttpGet("GetMSyaryoByUriageID")]
        public async Task<IActionResult> GetMSyaryoByUriageID(int uriageID)
        {
            try
            {
                SeikyuDataModel model = new(_context);
                M_Syaryo entity = await model.GetMSyaryoByUriageID(uriageID);
                return new OkObjectResult(entity);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// T_Uriage_Unchinを返却する
        /// </summary>
        /// <param name="Customer_Branch_ID">顧客支店ID</param>
        /// <returns>顧客支店情報</returns>
        [HttpGet("GetM_Customer_Branch")]
        public async Task<IActionResult> GetM_Customer_Branch(int Customer_Branch_ID)
        {
            M_Customer_Branch resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetM_Customer_Branch(Customer_Branch_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// List<T_Check_Seikyu_Change>を返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>請求変更リスト</returns>
        [HttpGet("GetT_Check_Seikyu_Change")]
        public async Task<IActionResult> GetT_Check_Seikyu_Change(int Check_Seikyu_ID)
        {
            IEnumerable<T_Check_Seikyu_Change> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu_Change(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyu_Changeを返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>請求変更情報</returns>
        [HttpGet("GetT_Check_Seikyu_ChangeData")]
        public async Task<IActionResult> GetT_Check_Seikyu_ChangeData(int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            T_Check_Seikyu_Change resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu_ChangeData(Check_Seikyu_ID, Uriage_Unchin_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyu_Detailを返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>請求チェック詳細</returns>
        [HttpGet("GetT_Check_Seikyu_Detail")]
        public async Task<IActionResult> GetT_Check_Seikyu_Detail(int Check_Seikyu_ID)
        {
            T_Check_Seikyu_Detail resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu_Detail(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyu_Detailを返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>請求チェック詳細リスト</returns>
        [HttpGet("GetT_Check_Seikyu_Detail_All")]
        public async Task<IActionResult> GetT_Check_Seikyu_Detail_All(int Check_Seikyu_ID)
        {
            List<T_Check_Seikyu_Detail> resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu_Detail_All(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyuを返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>請求チェック情報</returns>
        [HttpGet("GetT_Check_Seikyu")]
        public async Task<IActionResult> GetT_Check_Seikyu(int Check_Seikyu_ID)
        {
            T_Check_Seikyu resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyu_Doneを返却する
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <returns>請求チェック完了情報</returns>
        [HttpGet("GetT_Check_Seikyu_Done")]
        public async Task<IActionResult> GetT_Check_Seikyu_Done(int Check_Seikyu_ID)
        {
            T_Check_Seikyu_Done resultVal = null;

            try
            {
                SeikyuDataModel model = new(_context);
                resultVal = await model.GetT_Check_Seikyu_Done(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
            finally
            {
            }
            return new OkObjectResult(resultVal);
        }

        /// <summary>
        /// T_Check_Seikyu_Detailの更新
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="Qty">数量</param>
        /// <param name="Unit">単位</param>
        /// <param name="UnitPrice">単価</param>
        /// <param name="CalcPrice">計算価格</param>
        /// <param name="SeikyuUnchin">請求運賃</param>
        /// <param name="Tatekaekin">立替金</param>
        /// <param name="Warimashi1">割増1</param>
        /// <param name="Warimashi2">割増2</param>
        /// <param name="Warimashi3">割増3</param>
        /// <param name="Warimashi4">割増4</param>
        /// <param name="Warimashi5">割増5</param>
        /// <param name="SeikyuTotal">請求合計</param>
        /// <param name="loginUser">ログインユーザー</param>
        /// <returns>更新結果</returns>
        [HttpGet("UpdateT_Check_Seikyu_Detail")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateT_Check_Seikyu_Detail(int Uriage_Unchin_ID, int Qty,
                                            int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5, int SeikyuTotal, int loginUser)
        {
            {
                Dto.MsterDataCommonResultValDto resultVal = new();
                try
                {
                    SeikyuDataModel model = new(_context);
                    await model.UpdateCheckSeikyuDetail(Uriage_Unchin_ID, Qty, Unit, UnitPrice, CalcPrice, SeikyuUnchin, Tatekaekin, Warimashi1, Warimashi2, Warimashi3, Warimashi4, Warimashi5, SeikyuTotal, loginUser);
                    resultVal.RetrunFlg = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    resultVal.ErrrMessage = ex.Message;
                }
                finally
                {

                }
                return resultVal;
            }
        }

        /// <summary>
        /// 請求問合せ変更承認：一括確定処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("PostBatchRegistration")]
        public async Task<Dto.MsterDataCommonResultValDto> PostBatchRegistration(string jsonString)
        {
            SeikyuDataModel model = new(_context);

            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {

                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                BatchRegistrationModel dataDto = System.Text.Json.JsonSerializer.Deserialize<BatchRegistrationModel>(abc);

                resultVal.RetrunFlg = await model.PostBatchRegistration(dataDto);
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
        /// 売上情報の登録
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("PostModalApproval")]
        public async Task<Dto.MsterDataCommonResultValDto> PostModalApproval(string jsonString)
        {
            SeikyuDataModel model = new(_context);

            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {

                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                ModalApprovalModel dataDto = System.Text.Json.JsonSerializer.Deserialize<ModalApprovalModel>(abc);

                // WEB時の処理
                if (dataDto.Check_Kubun == 1)
                {
                    // 金額変更ありの場合
                    if (dataDto.Change_Flg == 1)
                    {
                        T_Check_Seikyu_Change CheckSeikyuChangeData = await model.GetT_Check_Seikyu_ChangeData(dataDto.Check_Seikyu_ID, dataDto.Uriage_Unchin_ID);
                        await model.UpdateCheckSeikyuDetail(CheckSeikyuChangeData, dataDto.User_ID);
                        await model.UpdateUriageUnchin(CheckSeikyuChangeData, dataDto.User_ID);
                    }
                }
                else if (dataDto.Check_Kubun == 2) // 帳票時の処理
                {
                    await model.UpdateCheckSeikyuDetail2(dataDto.UpdateCheckSeikyuDetail);
                    await model.UpdateUriageUnchin2(dataDto.UpdateUriageUnchin, dataDto.User_ID, dataDto.Uriage_Unchin_ID);
                    await model.RegistrationCheckSeikyuChange(dataDto.UpdateCheckSeikyuChange, dataDto.User_ID, dataDto.Check_Seikyu_ID, dataDto.Uriage_Unchin_ID);
                }
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
        /// 請求問合せ変更承認：暫定←→確定切り替え
        /// T_Uriage・T_Check_Seikyu_Detail・T_Check_Seikyuの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("UpdateApprovalStatus")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateApprovalStatus(string jsonString)
        {
            SeikyuDataModel model = new(_context);

            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {

                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                ApprovalStatusModel dataDto = System.Text.Json.JsonSerializer.Deserialize<ApprovalStatusModel>(abc);

                resultVal.RetrunFlg = await model.UpdateApprovalStatus(dataDto);
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
        /// T_Uriage_Unchinの更新
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="Tsumi">積み</param>
        /// <param name="Oroshi">卸し</param>
        /// <param name="Luggage">荷物</param>
        /// <param name="Zei_Kubun">税区分</param>
        /// <param name="Qty">数量</param>
        /// <param name="Unit">単位</param>
        /// <param name="UnitPrice">単価</param>
        /// <param name="CalcPrice">計算価格</param>
        /// <param name="SeikyuUnchin">請求運賃</param>
        /// <param name="Tatekaekin">立替金</param>
        /// <param name="Warimashi1">割増1</param>
        /// <param name="Warimashi2">割増2</param>
        /// <param name="Warimashi3">割増3</param>
        /// <param name="Warimashi4">割増4</param>
        /// <param name="Warimashi5">割増5</param>
        /// <param name="SeikyuTotal">請求合計</param>
        /// <param name="loginUser">ログインユーザー</param>
        /// <returns>更新結果</returns>
        [HttpGet("UpdateT_Uriage_Unchin")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateT_Uriage_Unchin(int Uriage_Unchin_ID,
                                            string Tsumi, string Oroshi, string Luggage, int Zei_Kubun, int Qty,
                                            int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5, int SeikyuTotal, int loginUser)
        {
            {
                Dto.MsterDataCommonResultValDto resultVal = new();
                try
                {
                    SeikyuDataModel model = new(_context);
                    await model.UpdateUriageUnchin(Uriage_Unchin_ID, Tsumi, Oroshi, Luggage, Zei_Kubun,
                    Qty, Unit, UnitPrice, CalcPrice, SeikyuUnchin, Tatekaekin, Warimashi1, Warimashi2, Warimashi3, Warimashi4, Warimashi5, SeikyuTotal, loginUser);
                    resultVal.RetrunFlg = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    resultVal.ErrrMessage = ex.Message;
                }
                finally
                {

                }
                return resultVal;
            }
        }

        /// <summary>
        /// T_Uriageの更新
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <param name="reg_Status">登録ステータス</param>
        /// <returns>更新結果</returns>
        [HttpGet("UpdateT_Uriage")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateT_Uriage(int Uriage_ID, int reg_Status)
        {
            {
                Dto.MsterDataCommonResultValDto resultVal = new();
                try
                {
                    SeikyuDataModel model = new(_context);
                    await model.UpdateUriage(Uriage_ID, reg_Status);
                    resultVal.RetrunFlg = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    resultVal.ErrrMessage = ex.Message;
                }
                finally
                {

                }
                return resultVal;
            }
        }

        /// <summary>
        /// T_Check_Seikyuの更新
        /// </summary>
        /// <param name="Check_Seikyu_ID">請求チェックID</param>
        /// <param name="Check_Kubun">チェック区分</param>
        /// <returns>更新結果</returns>
        [HttpGet("UpdateT_Check_Seikyu")]
        public async Task<Dto.MsterDataCommonResultValDto> UpdateT_Check_Seikyu(int Check_Seikyu_ID, int Check_Kubun)
        {
            {
                Dto.MsterDataCommonResultValDto resultVal = new();
                try
                {
                    SeikyuDataModel model = new(_context);
                    await model.UpdateCheckSeikyu(Check_Seikyu_ID, Check_Kubun);
                    resultVal.RetrunFlg = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    resultVal.ErrrMessage = ex.Message;
                }
                finally
                {

                }
                return resultVal;
            }
        }

        /// <summary>
        /// T_Check_Seikyu_Changeの新規登録、更新
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [HttpGet("InsertUpdateT_Check_Seikyu_Change")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdateT_Check_Seikyu_Change(int Uriage_Unchin_ID, int Qty,
                                            int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5, int SeikyuTotal, int insertUser)
        {
            {
                Dto.MsterDataCommonResultValDto resultVal = new();
                try
                {
                    SeikyuDataModel model = new(_context);
                    await model.InsertUpdateCheckSeikyuChange(Uriage_Unchin_ID, Qty, Unit, UnitPrice, CalcPrice, SeikyuUnchin, Tatekaekin, Warimashi1, Warimashi2, Warimashi3, Warimashi4, Warimashi5, SeikyuTotal, insertUser);
                    resultVal.RetrunFlg = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    resultVal.ErrrMessage = ex.Message;
                }
                finally
                {

                }
                return resultVal;
            }
        }


    }
}
