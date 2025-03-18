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
using WebApplication.Model;
using WebApplication.Services;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// 案件データを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AnkenDataController : MyBaseController
    {
        private readonly ILogger<AnkenDataController> _logger;
        private readonly IReceiptService _receiptService;

        /// <summary>
        /// AnkenDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="receiptService">レシートサービス</param>
        public AnkenDataController(ILogger<AnkenDataController> logger, ApplicationDbContext context, IReceiptService receiptService)
        {
            _logger = logger;
            _context = context;
            _receiptService = receiptService;
        }

        #region AnkenData
        /// <summary>
        /// 案件データの新規登録
        /// </summary>
        /// <returns>新規登録された案件データ</returns>
        [HttpPost("AddNewAnkenData")]
        public async Task<T_Anken> AddNewAnkenData()
        {
            T_Anken resultVal;
            try
            {
                // パラメーターの取得
                AnkenDataModelDto dataDto = GetMultipartFormDataContentData<AnkenDataModelDto>("name");

                AnkenDataModel model = new(_context);
                T_Anken t_Anken = await model.AddNewAnkenData(dataDto);

                resultVal = t_Anken;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                throw;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 案件データを更新
        /// </summary>
        /// <returns>更新された案件データ</returns>
        [HttpPost("UpdateAnkenData")]
        public async Task<AnkenModel.UpdateAnkenDataDto> UpdateAnkenData()
        {
            AnkenModel.UpdateAnkenDataDto resultVal = new() { Anken = new(), };
            try
            {
                // パラメーターの取得
                AnkenDataModelDto dataDto = GetMultipartFormDataContentData<AnkenDataModelDto>("name");

                AnkenDataModel model = new(_context);
                T_Anken t_Anken = await model.UpdateAnkenData(dataDto);
                resultVal.Anken = t_Anken;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                resultVal.ErrrMessage = ex.Message;
                throw;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 案件データの一覧を返却する
        /// </summary>
        /// <param name="targetDate">対象日</param>
        /// <param name="targetDateFrom">対象開始日</param>
        /// <param name="targetDateTo">対象終了日</param>
        /// <param name="CcompanyID">会社ID</param>
        /// <param name="branchID">支店ID</param>
        /// <param name="SenzokuID">専属ID</param>
        /// <param name="RirekiKubun">履歴区分</param>
        /// <param name="AnkenID">案件ID</param>
        /// <returns>案件データの一覧</returns>
        [HttpGet("GetAnkenDataList")]
        public async Task<IEnumerable<V_AnkenDataList>> GetAnkenDataList(int CcompanyID, string targetDate,
                                    string targetDateFrom, string targetDateTo, int CustomerID = 0, int branchID = 0, int SenzokuID = 0, int TakeNum = 0,
                                    int RirekiKubun = 0, int AnkenID = 0)
        {
            IEnumerable<V_AnkenDataList> resultVal = null;
            try
            {
                AnkenDataModel model = new(_context);
                resultVal = await model.GetAnkenDataList(CcompanyID, CustomerID, branchID, targetDate, targetDateFrom, targetDateTo, SenzokuID, TakeNum, RirekiKubun, AnkenID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                throw;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// 案件IDを元にAnkenDataを返却する
        /// </summary>
        /// <param name="AnkenId">案件ID</param>
        /// <returns>案件データ</returns>
        [HttpGet("GetAnkenData")]
        public async Task<AnkenDataModelDto> GetAnkenData(int AnkenId)
        {
            AnkenDataModelDto dto = new()
            {
                T_Anken = new(),
                T_Anken_Publish = new(),
                T_Anken_Detail = new(),
                T_Anken_PointList = new(),
                T_Anken_Remarks = new(),
                T_Anken_ExchargeList = new(),
                T_Anken_LuggageList = new(),
                T_Anken_Riyounso = new(),
                T_Anken_Riyounso_PointList = new(),
                M_Customer_Tantou = new(),
            };

            try
            {
                dto.T_Anken = await _context.T_Ankens.FirstOrDefaultAsync(m => m.Anken_ID == AnkenId);
                dto.T_Anken_Publish = await _context.T_Anken_Publishes.FirstOrDefaultAsync(m => m.Anken_ID == AnkenId);
                dto.T_Anken_Detail = await _context.T_Anken_Details.FirstOrDefaultAsync(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order);
                dto.T_Anken_Remarks = await _context.T_Anken_Remarks.FirstOrDefaultAsync(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order);
                dto.T_Anken_LuggageList = await _context.T_Anken_Luggages.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).ToListAsync();
                dto.T_Anken_EquipmentList = await _context.T_Anken_Equipments.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).ToListAsync();
                dto.T_Anken_PointList = await _context.T_Anken_Points.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).
                                                                OrderBy(m => m.Kubun).ThenBy(m => m.Point_Order).ToListAsync();
                dto.T_Anken_ExchargeList = await _context.T_Anken_Excharges.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).
                                                                OrderBy(m => m.Komoku_ID).ToListAsync();
                dto.T_Anken_Riyounso = await _context.T_Anken_Riyounsos.FirstOrDefaultAsync(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order);
                dto.T_Anken_Riyounso_PointList = await _context.T_Anken_Riyounso_Points.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).
                                                                OrderBy(m => m.Point_Order).ToListAsync();

                List<T_Anken_OyaKokyaku> t_Anken_OyaKokyakus = new();
                t_Anken_OyaKokyakus = await _context.T_Anken_OyaKokyakus.Where(m => m.Anken_ID == AnkenId && m.Anken_Order == dto.T_Anken.Anken_Latest_Order).OrderBy(m => m.Kokyaku_Order).ToListAsync();
                if (t_Anken_OyaKokyakus != null && t_Anken_OyaKokyakus.Count > 0)
                {
                    dto.T_Anken_OyaKokyakuList = new();
                    dto.T_Anken_OyaKokyakuList = t_Anken_OyaKokyakus;
                }

                dto.M_Customer_Tantou = await _context.M_Customer_Tantous.FirstOrDefaultAsync(m => m.Tantou_ID == dto.T_Anken_Detail.KokyakuTantouId);
                Data.M_Customer_Branch branch = await _context.M_Customer_Branches.FirstOrDefaultAsync(m => m.Customer_Branch_ID == dto.T_Anken_Detail.KokyakuId);
                if (branch != null) { dto.AnkenRemarks = branch.AnkenRemarks; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }
            finally { }
            return dto;
        }
        #endregion AnkenData

        #region T_Point
        /// <summary>
        /// T_Pointデータを返却する
        /// </summary>
        /// <param name="addressCode">住所コード</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <returns>T_Pointデータ</returns>
        [HttpGet("T_PointData")]
        public async Task<Data.T_Point> T_PointData(string addressCode, int userId, int groupId)
        {
            try
            {
                IQueryable<Data.T_Point> data = null;

                if (addressCode == null) { return null; }

                data = _context.T_Points.Where(m => m.Address_Code == addressCode);
                if (userId > 0) { data = data.Where(m => m.User_ID == userId); }
                if (groupId > 0) { data = data.Where(m => m.Group_ID == groupId); }

                return await data.FirstOrDefaultAsync();
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
        /// T_Pointデータの新規・更新を行う
        /// </summary>
        /// <returns>新規・更新されたT_Pointデータ</returns>
        [HttpPost("InsertUpdatePointData")]
        public async Task<Dto.MsterDataCommonResultValDto> InsertUpdatePointData()
        {
            Dto.MsterDataCommonResultValDto resultVal;
            try
            {
                // パラメーターの取得
                T_Point dataDto = GetMultipartFormDataContentData<T_Point>("name");

                AnkenDataModel model = new(_context);
                Dto.MsterDataCommonResultValDto result = await model.InsertUpdatePointData(dataDto);
                resultVal = result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                throw;
            }
            finally
            {
            }
            return resultVal;
        }

        /// <summary>
        /// ユーザーIDに基づいてT_Pointデータを返却する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <returns>T_Pointデータのリスト</returns>
        [HttpGet("T_PointListFromUserId")]
        public async Task<List<Data.T_Point>> T_PointListFromUserId(int userId, int groupId)
        {
            List<Data.T_Point> data = null;

            if (userId == 0 && groupId == 0) { return null; }

            try
            {
                data = await _context.T_Points.Where(m => m.User_ID == userId || _context.M_CompanyUser_GroupUsers.Where(m => m.User_ID == userId).Select(m => m.Group_ID).Contains(m.Group_ID)).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }
            finally { }
            return data;
        }
        #endregion T_Point

        #region T_Anken_Point
        /// <summary>
        /// 案件ポイントリストを取得する
        /// </summary>
        /// <param name="AnkenID">案件ID</param>
        /// <param name="AnkenOrder">案件オーダー</param>
        /// <returns>案件ポイントリスト</returns>
        [HttpGet("GetAnkenPointList")]
        public async Task<List<Data.T_Anken_Point>> GetAnkenPointList(int AnkenID, int AnkenOrder = 0)
        {
            try
            {
                IQueryable<Data.T_Anken_Point> data = _context.T_Anken_Points.Where(m => m.Anken_ID == AnkenID);

                if (AnkenOrder > 0)
                {
                    data = data.Where(m => m.Anken_Order == AnkenOrder);
                }

                return await data.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }
        }
        #endregion T_Anken_Point

        #region T_Anken_Display
        /// <summary>
        /// 指定AnkenDisplay_IDからT_Anken_Displayを返却
        /// </summary>
        /// <param name="ankenDisplayId">AnkenDisplay_ID</param>
        /// <returns>T_Anken_Displayデータ</returns>
        [HttpGet("GetAnkenDisplayData")]
        public async Task<Data.T_Anken_Display> GetAnkenDisplayData(int ankenDisplayId)
        {
            try
            {
                IQueryable<Data.T_Anken_Display> data = null;

                if (ankenDisplayId == 0) { return null; }

                data = _context.T_Anken_Displays.Where(m => m.AnkenDisplay_ID == ankenDisplayId);
                return await data.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }
        #endregion T_Anken_Display

        /// <summary>
        /// 住所検索使用率TOP30
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="Top">トップ数</param>
        /// <returns>住所検索使用率TOP30</returns>
        [HttpGet("GetAnkenPointForUtilizationRate")]
        public async Task<List<T_Anken_Point>> GetAnkenPointForUtilizationRate(int userId, int Top = 0)
        {
            if (userId == 0) { throw new Exception("パラメーターエラー不正：userId"); }

            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " MIN([Anken_ID]) AS [Anken_ID]";
            sql += " ,MIN([Anken_Order]) AS [Anken_Order]";
            sql += " ,MIN([Kubun]) AS [Kubun]";
            sql += " ,COUNT(*) AS [Point_Order]";
            sql += " ,Null AS [SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,Null AS [Address_Level]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,Null AS [BuildingNameRead]";
            sql += " ,Null AS [Point_KoumokuTitle]";
            sql += " ,Null AS [Point_Type]";
            sql += " ,Null AS [PointName]";
            sql += " ,Null AS [PointDate]";
            sql += " ,Null AS [PointTime]";
            sql += " ,Null AS [PointTimeKubun]";
            sql += " ,Null AS [PointStatusKubun]";
            sql += " ,Null AS [FlgGenchiKakunin]";
            sql += " ,Null AS [TollDisplay]";
            sql += " ,Null AS [TollDisplayHeight]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ,Null AS [RoadType]";
            sql += " ,Null AS [Insert_Datetime]";
            sql += " ,Null AS [Insert_User]";
            sql += " ,Null AS [Update_Datetime]";
            sql += " ,Null AS [Update_User]";

            sql += " FROM [T_Anken_Point] P";
            sql += " WHERE";
            sql += " ([Insert_User] = {0}";
            sql += " OR";
            sql += " [Update_User] = {0})";
            sql += " AND [Post_code] IS NOT NULL";

            sql += " GROUP BY";
            sql += " [Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";
            sql += " ORDER BY";
            sql += " COUNT(*) DESC";

            try
            {
                List<T_Anken_Point> result = await _context.T_Anken_Points.FromSqlRaw(sql, userId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        /// <summary>
        /// 住所検索使用履歴TOP30
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="Top">トップ数</param>
        /// <returns>住所検索使用履歴TOP30</returns>
        [HttpGet("GetAnkenPointForRireki")]
        public async Task<List<T_Anken_Point>> GetAnkenPointForRireki(int userId, int Top)
        {
            if (userId == 0) { throw new Exception("パラメーターエラー不正：userId"); }

            string sql = "";
            sql += " SELECT";
            if (Top > 0) { sql += " TOP " + Top.ToString(); }
            sql += " MIN([Anken_ID]) AS [Anken_ID]";
            sql += " ,MIN([Anken_Order]) AS [Anken_Order]";
            sql += " ,MIN([Kubun]) AS [Kubun]";
            sql += " ,COUNT(*) AS [Point_Order]";
            sql += " ,Null AS [SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,Null AS [Address_Level]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,Null AS [BuildingNameRead]";
            sql += " ,Null AS [Point_KoumokuTitle]";
            sql += " ,Null AS [Point_Type]";
            sql += " ,Null AS [PointName]";
            sql += " ,Null AS [PointDate]";
            sql += " ,Null AS [PointTime]";
            sql += " ,Null AS [PointTimeKubun]";
            sql += " ,Null AS [PointStatusKubun]";
            sql += " ,Null AS [FlgGenchiKakunin]";
            sql += " ,Null AS [TollDisplay]";
            sql += " ,Null AS [TollDisplayHeight]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ,Null AS [RoadType]";
            sql += " ,MAX([Insert_Datetime]) AS [Insert_Datetime]";
            sql += " ,Null AS [Insert_User]";
            sql += " ,Null AS [Update_Datetime]";
            sql += " ,Null AS [Update_User]";

            sql += " FROM";
            sql += " (";
            sql += " SELECT ";
            sql += " [Anken_ID]";
            sql += " ,[Anken_Order]";
            sql += " ,[Kubun]";
            sql += " ,[Point_Order]";
            sql += " ,[SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += ",[Address4]";
            sql += ",[Insert_Datetime]";
            sql += " FROM [T_Anken_Point] P";
            sql += " WHERE";
            sql += " [Insert_User] = {0}";
            sql += " AND [Post_code] IS NOT NULL";

            sql += " UNION ALL";

            sql += " SELECT ";
            sql += " [Anken_ID]";
            sql += " ,[Anken_Order]";
            sql += " ,[Kubun]";
            sql += " ,[Point_Order]";
            sql += " ,[SEKubun]";
            sql += " ,[Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";
            sql += " ,[Update_Datetime] AS [Insert_Datetime]";
            sql += " FROM [T_Anken_Point] P";
            sql += " WHERE";
            sql += " [Update_User] = {0}";
            sql += " AND [Post_code] IS NOT NULL";

            sql += " )X";
            sql += " GROUP BY";
            sql += " [Address]";
            sql += " ,[Address_Code]";
            sql += " ,[Lng]";
            sql += " ,[Lat]";
            sql += " ,[BuildingName]";
            sql += " ,[BuildingZid]";
            sql += " ,[BuildingZid_Attr]";
            sql += " ,[Post_code]";
            sql += " ,[Address2]";
            sql += " ,[Address3]";
            sql += " ,[Address4]";

            sql += " ORDER BY";
            sql += " [Insert_Datetime] DESC";

            try
            {
                List<T_Anken_Point> result = await _context.T_Anken_Points.FromSqlRaw(sql, userId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        /// <summary>
        /// 指定された案件IDリストに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="Anken_IDs">Anken_IDのリスト</param>
        /// <returns>T_Anken_Displayのオブジェクトのリスト</returns>
        [HttpPost("GetAnkenDisplayListAsync")]
        public async Task<IActionResult> GetAnkenDisplayListAsync([FromBody] List<int> Anken_IDs)
        {
            try
            {
                // 案件IDリストに基づいてT_Anken_Displayを取得
                List<T_Anken_Display> AnkenDisplay = await _receiptService.GetAnkenDisplayAsync(Anken_IDs);
                return new OkObjectResult(AnkenDisplay);

            }
            // 例外処理
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }
    }
}
