using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HaisyaWeb.Models.AnkenModel;

namespace HaisyaWeb.API.WebApp
{
    class AnkenDataApi : BaseHttpClient
    {
        public AnkenDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 案件データの新規登録
        /// </summary>
        /// <param name="ankenDataModelDto">AnkenDataModelDto</param>
        /// <returns></returns>
        public async Task<T_Anken_Local> AddNewAnkenData(AnkenDataModelDto_Local ankenDataModelDto)
        {
            string url = "AnkenData/AddNewAnkenData";
            return await ExecHttpDataToResponse<AnkenDataModelDto_Local, T_Anken_Local>(ankenDataModelDto, url);
        }

        /// <summary>
        /// 案件データの更新
        /// </summary>
        /// <param name="ankenDataModelDto">AnkenDataModelDto</param>
        /// <returns></returns>
        public async Task<UpdateAnkenDataDto> UpdateAnkenData(AnkenDataModelDto_Local ankenDataModelDto)
        {
            string url = "AnkenData/UpdateAnkenData";
            return await ExecHttpDataToResponse<AnkenDataModelDto_Local, UpdateAnkenDataDto>(ankenDataModelDto, url);
        }

        /// <summary>
        /// 案件データの更新
        /// </summary>
        /// <param name="ankenDataModelDto">AnkenDataModelDto</param>
        /// <returns></returns>
        public async Task<UpdateAnkenDataDto> UpdateAnkenDataForPoint(AnkenDataModelDto_Local ankenDataModelDto)
        {
            string url = "AnkenData/UpdateAnkenDataForPoint";
            return await ExecHttpDataToResponse<AnkenDataModelDto_Local, UpdateAnkenDataDto>(ankenDataModelDto, url);
        }

        /// <summary>
        /// 案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="iCompanyID"></param>
        /// <param name="branchID"></param>
        /// <param name="RirekiKubun">案件履歴も全て取得するかどうか。0：最新のみ、1：履歴も含める</param>
        /// <param name="AnkenID"></param>
        /// <returns></returns>
        public async Task<List<V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom, string targetDateTo,
                                                                        int iCompanyID, int customerID = 0, int branchID = 0,
                                                                        int SenzokuID = 0, int TakeNum = 0, int RirekiKubun = 0,
                                                                        int AnkenID = 0)
        {

            string url = string.Format("AnkenData/GetAnkenDataList?CcompanyID={0}", iCompanyID);
            if (targetDate != null) { url += string.Format("&targetDate={0}", targetDate.Replace("/", "-")); }
            if (targetDateFrom != null) { url += string.Format("&targetDateFrom={0}", targetDateFrom.Replace("/", "-")); }
            if (targetDateTo != null) { url += string.Format("&targetDateTo={0}", targetDateTo.Replace("/", "-")); }
            if (SenzokuID > 0) { url += string.Format("&SenzokuID={0}", SenzokuID.ToString()); }
            if (TakeNum > 0) { url += string.Format("&TakeNum={0}", TakeNum.ToString()); }
            if (customerID > 0) { url += string.Format("&customerID={0}", customerID.ToString()); }
            if (branchID > 0) { url += string.Format("&branchID={0}", branchID.ToString()); }
            url += string.Format("&RirekiKubun={0}", RirekiKubun.ToString());
            url += string.Format("&AnkenID={0}", AnkenID.ToString());

            return await GetHttpData<List<V_AnkenDataList_Local>>(url);
        }

        /// <summary>
        /// 案件データの返却
        /// </summary>
        /// <param name="AnkenId"></param>
        /// <returns></returns>
        public async Task<AnkenDataModelDto_Local> GetAnkenData(int AnkenId)
        {
            string url = string.Format("AnkenData/GetAnkenData?AnkenId={0}", AnkenId);
            return await GetHttpData<AnkenDataModelDto_Local>(url);
        }

        /// <summary>
        /// 自動車ルート候補一覧検索
        /// </summary>
        /// <param name="area">標準運賃計算エリア</param>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="from">出発地点</param>
        /// <param name="to">到着地点</param>
        /// <param name="waypoint">経由地点</param>
        /// <param name="searchparam">検索挙動変更</param>
        /// <param name="height">車種</param>
        /// <param name="width">型</param>
        /// <param name="departuretime">出発時刻指定</param>
        /// <param name="regulationtype">詳細車種</param>
        /// <param name="tolltype">料金車種</param>
        /// <param name="smartic">スマートIC利用指定</param>
        /// <param name="timerestriction">規制考慮</param>
        /// <param name="twouturn">2段階Uターン回避指定</param>
        /// <param name="ferry">フェリー考慮指定</param>
        /// <param name="t_Anken_Excharge_s">追加料金</param>
        /// <returns>正常：レスポンスのボディ / 異常：null</returns>
        public async Task<DriveRouteListDto_Local> GetDriveRouteListExAsync(
                                                        int area,
                                                        int iCompanyID,
                                                        string from, string to,
                                                        string waypoint = "",
                                                        string syasyu = null,
                                                        string kata = null,
                                                        double height = 0,
                                                        double width = 0,
                                                        double weight = 0,
                                                        double nenpi = 0,
                                                        string fromstype = "",
                                                        string totype = "",
                                                        string mpointstype = "",
                                                        string syasyuSize = null,
                                                        int searchparam = 1,
                                                        string departuretime = null,
                                                        string regulationtype = "121100",
                                                        string tolltype = "large",
                                                        string smartic = "true",
                                                        string timerestriction = "true",
                                                        string twouturn = "false",
                                                        string ferry = "false",
                                                        string TsumiTime = "00:00",
                                                        string OroshiTime = "00:00",
                                                        int DriverGrossCalcKubun = 0,
                                                        List<AnkenExchargeDto_Local> t_Anken_Excharge_s = null)
        {
            string url = string.Format("Anken/GetDriveRouteListEx?CompanyID={0}", iCompanyID);
            url += string.Format("&area={0}", area);
            url += string.Format("&from={0}", from);
            url += string.Format("&to={0}", to);
            if (waypoint.Length > 0) { url += string.Format("&waypoint={0}", waypoint); }
            if (syasyu != null) { url += string.Format("&syasyu={0}", syasyu); }
            if (kata != null) { url += string.Format("&kata={0}", kata); }
            url += string.Format("&height={0}", height);
            url += string.Format("&width={0}", width);
            url += string.Format("&weight={0}", weight);
            url += string.Format("&nenpi={0}", nenpi);
            if (!string.IsNullOrEmpty(fromstype))
            {
                url += string.Format("&fromstype={0}", fromstype);
            }
            if (!string.IsNullOrEmpty(totype))
            {
                url += string.Format("&totype={0}", totype);
            }
            if (mpointstype.Length > 0) { url += string.Format("&waypointtype={0}", mpointstype); }
            if (syasyuSize != null) { url += string.Format("&syasyuSize={0}", syasyuSize); }
            url += string.Format("&searchparam={0}", searchparam);
            if (departuretime != null) { url += string.Format("&departuretime={0}", departuretime); }
            url += string.Format("&regulationtype={0}", regulationtype);
            url += string.Format("&tolltype={0}", tolltype);
            url += string.Format("&smartic={0}", smartic);
            url += string.Format("&timerestriction={0}", timerestriction);
            url += string.Format("&twouturn={0}", twouturn);
            url += string.Format("&ferry={0}", ferry);
            url += string.Format("&TsumiTime={0}", TsumiTime);
            url += string.Format("&OroshiTime={0}", OroshiTime);
            url += string.Format("&DriverGrossCalc={0}", DriverGrossCalcKubun);

            return await ExecHttpDataToResponse<List<AnkenExchargeDto_Local>, DriveRouteListDto_Local>(t_Anken_Excharge_s, url);
        }

        /// <summary>
        /// 自動車ルート検索
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="iCompanyID"></param>
        /// <param name="syasyu"></param>
        /// <param name="kata"></param>
        /// <param name="syasyuSize"></param>
        /// <param name="TsumiTime"></param>
        /// <param name="OroshiTime"></param>
        /// <param name="searchparam"></param>
        /// <param name="datum"></param>
        /// <param name="llunit"></param>
        /// <returns></returns>
        public async Task<DriveRouteListDto_Local> GetDriveDetailAsync(
                                                                        int iCompanyID,
                                                                        string routeID,
                                                                        string routeType,
                                                                        int area,
                                                                        string syasyu,
                                                                        string kata,
                                                                        string syasyuSize,
                                                                        string TsumiTime = "00:00",
                                                                        string OroshiTime = "00:00",
                                                                        string datum = "JGD",
                                                                        string llunit = "dec"
                                                                        )
        {
            string url = _baseUrl + string.Format("Anken/GetDriveDetail?CompanyID={0}", iCompanyID);
            url += string.Format("&routeID={0}", routeID);
            url += string.Format("&routeType={0}", routeType);
            url += string.Format("&area={0}", area);
            url += string.Format("&syasyu={0}", syasyu);
            url += string.Format("&kata={0}", kata);
            url += string.Format("&syasyuSize={0}", syasyuSize);
            if (datum != null) { url += string.Format("&datum={0}", datum); }
            if (llunit != null) { url += string.Format("&llunit={0}", llunit); }
            url += string.Format("&TsumiTime={0}", TsumiTime);
            url += string.Format("&OroshiTime={0}", OroshiTime);

            return await GetHttpData<DriveRouteListDto_Local>(url);
        }

        #region M_Customer
        /// <summary>
        /// 指定ユーザーIDのM_Customerの登録件数の降順の指定件数を返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Local>> GetCustomerListForUser(int CompanyID, int UserID, int Top)
        {
            string url = string.Format("Anken/GetCustomerListForUser?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<M_Customer_Local>>(url);
        }

        /// <summary>
        /// 指定ユーザーIDのM_Customerの直近の登録データの指定件数を返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Local>> GetCustomerListToMostRecentForUser(int CompanyID, int UserID, int Top)
        {
            string url = string.Format("Anken/GetCustomerListToMostRecentForUser?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<M_Customer_Local>>(url);
        }

        #endregion M_Customer

        #region T_Point
        /// <summary>
        /// T_Pointの保存データをBuildingZidをキーに検索して返却
        /// 重複データチェック用に使用
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<T_Point_Local> GetPointDataToZip(string addressCode, int userId, int groupId)
        {
            string url = string.Format("AnkenData/T_PointData?addressCode={0}", addressCode);
            url += string.Format("&userId={0}", userId);
            url += string.Format("&groupId={0}", groupId);

            //データ取得
            return await GetHttpData<T_Point_Local>(url);
        }

        /// <summary>
        /// UserIdを元にT_Pointのリストを抽出いて返却
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<T_Point_Local>> GetPointListFromUserId(int userId, int groupId)
        {
            string url = string.Format("AnkenData/T_PointListFromUserId?dummy=1");
            url += string.Format("&userId={0}", userId);
            url += string.Format("&groupId={0}", groupId);

            //データ取得
            return await GetHttpData<List<T_Point_Local>>(url);
        }

        /// <summary>
        /// T_Pointへのデータ更新
        /// </summary>
        /// <param name="t_Point">T_Point</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdatePointData(T_Point_Local t_Point)
        {
            string url = string.Format("AnkenData/InsertUpdatePointData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData(t_Point, url);
        }
        #endregion T_Point

        #region T_Anken_Point
        public async Task<List<T_Anken_Point_Local>> GetAnkenPointList(int AnkenID, int AnkenOrder = 0)
        {
            string url = string.Format("AnkenData/GetAnkenPointList?AnkenID={0}", AnkenID.ToString());
            url += string.Format("&AnkenOrder={0}", AnkenOrder);
            return await GetHttpData<List<T_Anken_Point_Local>>(url);
        }
        #endregion T_Anken_Point

        #region T_Anken_Display
        /// <summary>
        /// 指定AnkenDisplay_IDからT_Anken_Displayを返却
        /// </summary>
        /// <param name="ankenDisplayId"></param>
        /// <returns></returns>
        public async Task<T_Anken_Display_Local> GetAnkenDisplayData(int ankenDisplayId)
        {
            string url = string.Format("AnkenData/GetAnkenPointList?ankenDisplayId={0}", ankenDisplayId.ToString());
            return await GetHttpData<T_Anken_Display_Local>(url);
        }
        #endregion T_Anken_Display

        /// <summary>
        /// 住所検索使用率TOP30
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<T_Anken_Point_Local>> GetAnkenPointForUtilizationRate(int CompanyID, int UserID, int Top)
        {
            string url = string.Format("AnkenData/GetAnkenPointForUtilizationRate?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<T_Anken_Point_Local>>(url);
        }

        /// <summary>
        /// 住所検索使用履歴
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public async Task<List<T_Anken_Point_Local>> GetAnkenPointForRireki(int CompanyID, int UserID, int Top)
        {
            string url = string.Format("AnkenData/GetAnkenPointForRireki?CompanyID={0}", CompanyID.ToString());
            url += string.Format("&UserID={0}", UserID);
            url += string.Format("&Top={0}", Top);
            return await GetHttpData<List<T_Anken_Point_Local>>(url);
        }

        /// <summary>
        /// T_Anken_Displayを返却
        /// </summary>
        /// <returns></returns>
        public async Task<List<T_Anken_Display_Local>> GetAnkenDisplayList(String targetDateFrom, String targetDateTo, int companyId = 0)
        {
            string url = string.Format("HaisyaData/GetHaisyaDisplayList?targetDateFrom={0}&targetDateTo={1}&companyId={2}", targetDateFrom, targetDateTo, companyId);
            //データ取得
            return await GetHttpData<List<T_Anken_Display_Local>>(url);
        }

        /// <summary>
        /// 指定された案件IDリストに基づいてT_Anken_Displayを非同期に取得
        /// </summary>
        /// <param name="Anken_IDs"></param>
        /// <returns>T_Anken_Displayオブジェクトのリスト</returns>
        public async Task<List<T_Anken_Display_Local>> GetAnkenDisplayAsync(List<int> Anken_IDs)
        {
            var response = await MakePostGetRequestAsync<List<int>, List<T_Anken_Display_Local>>(Anken_IDs, _baseUrl, "AnkenData/GetAnkenDisplayListAsync");
            return response.Data;
        }
    }
}
