using HaisyaWeb.API.WebApp;
using HaisyaWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaisyaWeb.Service
{
    /// <summary>
    /// 担当区分
    /// </summary>
    public enum TantouLists
    {
        ALL = 0,
        Tantou = 1,
        Eigyo = 2,
        Seikyu = 3,
    }

    /// <summary>
    /// ユーザーグループ区分
    /// </summary>
    public enum UserGroupLists
    {
        ALL = 0,
        Haisya = 1,
        Seikyu = 2,
        Eigyo = 3,
        Shiharai = 4,

    }

    /// <summary>
    /// 住所エリア
    /// </summary>
    public enum AddressArea
    {
        Hokkaido = 1,
        Tohoku = 2,
        Hokuriku = 3,
        Kanto = 4,
        Chubu = 5,
        Kinki = 6,
        Chugoku = 7,
        Shikoku = 8,
        Kyusyu = 9,
        Okinawa = 10,
    }

    /// <summary>
    /// 検索共通サービスクラス
    /// </summary>
    public class SearchCommonService
    {

        /// <summary>
        ///  過去15ヶ月分のリストを返却
        /// </summary>
        /// <returns>年月の選択リスト</returns>
        public static IEnumerable<SelectListItem> GetYearMonthSelect()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 0; i < 15; i++)
            {
                DateTime bb = DateTime.Now.AddMonths(-i);

                list.Add(new SelectListItem() { Value = bb.ToString("yyyyMM"), Text = bb.ToString("yyyy年MM月"), Selected = (0 == i) });
            }

            return list;

        }

        /// <summary>
        /// 配車担当選択リストを返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="listKubun">担当区分</param>
        /// <param name="notAllFlg">全員フラグ</param>
        /// <param name="AllDisplay">全員表示テキスト</param>
        /// <param name="user_group">ユーザーグループ区分</param>
        /// <returns>担当選択リスト</returns>
        public static async Task<List<SelectListItem>> GetTantouSelect(MapApiSettings _mapApiSettiong, int CompanyId, 
                                            TantouLists listKubun = TantouLists.ALL,
                                            bool notAllFlg = false, string AllDisplay = "全員", UserGroupLists user_group = UserGroupLists.ALL)
        {
            List<SelectListItem> list = new();

            MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_CompanyUser_Local> companyUser = await api.GetCompanyUserList(CompanyId);

            if (!notAllFlg)
            {
                list.Add(new SelectListItem() { Value = "ALL", Text = AllDisplay, Selected = true });
            }

            switch (listKubun)
            {
                case TantouLists.Tantou:
                    companyUser = companyUser.Where(m => m.Tantou_Flg).ToList();
                    break;
                case TantouLists.Eigyo:
                    companyUser = companyUser.Where(m => m.Eigyo_Flg).ToList();
                    break;
                case TantouLists.Seikyu:
                    companyUser = companyUser.Where(m => m.Seikyu_Flg).ToList();
                    break;
                case TantouLists.ALL:
                default:
                    break;
            }

            if (user_group != UserGroupLists.ALL)
            {
                HashSet<int> h = (await api.GetCompanyUserGroupUserList((int)user_group)).Select(ug => ug.User_ID).ToHashSet();
                companyUser = companyUser.Where(m => h.Contains(m.User_ID)).ToList();
            }

            foreach (var item in companyUser)
            {
                list.Add(new SelectListItem() { Value = item.User_ID.ToString(), Text = item.Display_Name, Selected = false });
            }
            
            return list;
        }

        /// <summary>
        /// 車種選択リストの返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <returns>車種選択リスト</returns>
        public static async Task<List<SelectListItem>> GetSyasyuSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(CompanyId);
            foreach (var item in dataList)
            {
                list.Add(new SelectListItem() { Value = item.Syaryo_ID.ToString(), Text = item.SyasyuDisplay, Selected = false });
            }
            return list;
        }

        /// <summary>
        /// 型選択リストの返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="Size">サイズ</param>
        /// <returns>型選択リスト</returns>
        public static async Task<List<SelectListItem>> GetKataSelect(MapApiSettings _mapApiSettiong, int CompanyId, string Size = null)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(CompanyId, null, null, Size);
            var queryKata = dataList.OrderBy(x => x.KataDisplay).GroupBy(x => new { KataDisplay = x.KataDisplay, kata = x.KATA });;
            foreach (var item in queryKata)
            {
                list.Add(new SelectListItem() { Value = item.Key.kata, Text = item.Key.KataDisplay, Selected = false });
            }
            return list;
        }

        /// <summary>
        /// 型マスタ選択リストの返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <returns>型マスタ選択リスト</returns>
        public static async Task<List<SelectListItem>> GetKataMasterSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Kata_Local> dataList = await api.GetKataList(CompanyId);
            List<Dto.M_Kata_Local> queryKata = dataList.OrderBy(x => x.SortOrder).ToList();
            foreach (var item in queryKata)
            {
                list.Add(new SelectListItem() { Value = item.Kata_ID, Text = item.Kata_Display + " (" + item.Kata_ID + ")", Selected = false });
            }
            return list;
        }

        /// <summary>
        /// 車種サイズ選択リストの返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <returns>車種サイズ選択リスト</returns>
        public static async Task<List<SelectListItem>> GetSyasyuSizeSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_SyaryoSize_Local> dataList = await api.GetSyaryoSizeList(CompanyId);
            IOrderedEnumerable<Dto.M_SyaryoSize_Local> querySyasyu = dataList.OrderBy(x => x.SortOrder);
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.SIZE, Text = item.SIZE, Selected = false });
            }
            return list;
        }

        /// <summary>
        /// エリア選択リストの選択
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <returns>エリア選択リスト</returns>
        public static async Task<List<SelectListItem>> GetAreaSelectListItem(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_Area_Local> dataList = await api.GetAreaList(CompanyId);
            IOrderedEnumerable<Dto.M_Area_Local> querySyasyu = dataList.OrderBy(x => x.Sort_Order);
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.Area_ID.ToString(), Text = item.Area, Selected = false });
            }
            return list;
        }

        /// <summary>
        /// 公開グループ選択リストの選択
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="BranchID">支店ID</param>
        /// <param name="UserID">ユーザーID</param>
        /// <returns>公開グループ選択リスト</returns>
        public static async Task<List<SelectListItem>> GetPublishGroupSelectListItem(MapApiSettings _mapApiSettiong,
                                                                                int CompanyID, int BranchID, int UserID)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_PublishGroup_Local> dataList = await api.M_PublishGroupList(CompanyID, BranchID, UserID);
            IOrderedEnumerable<Dto.M_PublishGroup_Local> querySyasyu = dataList.OrderBy(x => x.Publish_Group_Name);
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.PublishGroup_ID.ToString(), Text = item.Publish_Group_Name, Selected = false });
            }
            return list;
        }

        /// <summary>
        /// ユーザーグループ選択リストを返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="listKubun">ユーザーグループ区分</param>
        /// <param name="notAllFlg">全てフラグ</param>
        /// <param name="AllDisplay">全て表示テキスト</param>
        /// <param name="AllValue">全ての値</param>
        /// <returns>ユーザーグループ選択リスト</returns>
        public static async Task<List<SelectListItem>> GetUserGroupSelect(MapApiSettings _mapApiSettiong, int CompanyId, UserGroupLists listKubun,
                                            bool notAllFlg = false, string AllDisplay = "全て", string AllValue = "ALL")
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_CompanyUser_Group_Local> companyUser = await api.GetCompanyUserGroupList(CompanyId, listKubun);

            if (!notAllFlg)
            {
                list.Add(new SelectListItem() { Value = AllValue, Text = AllDisplay, Selected = true });
            }

            switch (listKubun)
            {
                case UserGroupLists.ALL:
                    companyUser = companyUser.Where(m => m.Company_ID == CompanyId).ToList();
                    break;
                default:
                    break;
            }

            foreach (var item in companyUser)
            {
                list.Add(new SelectListItem() { Value = item.Group_ID.ToString(), Text = item.Display_Name, Selected = false });
            }

            return list;
        }


        /// <summary>
        /// ユーザーグループ選択初期値を返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="listKubun">ユーザーグループ区分</param>
        /// <param name="loginUserId">ログインユーザーID</param>
        /// <param name="notAllFlg">全てフラグ</param>
        /// <param name="AllValue">全ての値</param>
        /// <returns>ユーザーグループ選択初期値</returns>
        public static async Task<string> GetUserGroupDefaultVal(MapApiSettings _mapApiSettiong, int CompanyId, UserGroupLists listKubun,
                                           int loginUserId, bool notAllFlg = false, string AllValue = "ALL")
        {
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_CompanyUser_GroupUser_Local> groupUser = await api.GetCompanyUserGroupUserList((int)listKubun);
            groupUser = groupUser.Where(m => m.Default_Flg == true && m.User_ID == loginUserId).ToList();

            if (groupUser != null && groupUser.Count > 0) return groupUser.FirstOrDefault().Group_ID.ToString();

            if (notAllFlg == false) return AllValue;

            return 0.ToString();
        }


        /// <summary>
        /// 車輌選択リストを返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <param name="Syasyu">車種</param>
        /// <param name="Spec">仕様</param>
        /// <param name="notAllFlg">全てフラグ</param>
        /// <param name="AllDisplay">全て表示テキスト</param>
        /// <returns>車輌選択リスト</returns>
        public static async Task<List<SelectListItem>> GetUSyaryoSelect(MapApiSettings _mapApiSettiong, int CompanyId, string Syasyu = null, string Spec = null,
                                            bool notAllFlg = false, string AllDisplay = "全て")
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(CompanyId, Syasyu, Spec);
            dataList = dataList.OrderBy(m => m.SYASYU).ThenBy(m => m.KATA).ToList();

            if (!notAllFlg)
            {
                list.Add(new SelectListItem() { Value = "ALL", Text = AllDisplay, Selected = true });
            }

            foreach (var item in dataList)
            {
                list.Add(new SelectListItem() { Value = item.Syaryo_ID.ToString(), Text = item.SyasyuDisplay, Selected = false });
            }

            return list;
        }

        /// <summary>
        /// 車輌台帳選択リストを返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CompanyId">会社ID</param>
        /// <returns>車輌台帳選択リスト</returns>
        public static async Task<List<SelectListItem>> GetSyaryoManagementSelect(MapApiSettings _mapApiSettiong, int CompanyId)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_SyaryoManagement_Local> listData = await api.GetSyaryoManagementList(CompanyId);
            listData = listData.OrderBy(m => m.Syasyu).ThenBy(m => m.Kata).ThenBy(m => int.Parse(m.Syaban_Number)).ToList();

            foreach (var item in listData)
            {
                string s = "【" + item.Syasyu + item.Kata + "】" + item.Syaban_Chiiki + item.Syaban_Bunrui + item.Syaban_Kana + item.Syaban_Number;
                list.Add(new SelectListItem() { Value = item.SyaryoManagement_ID.ToString(), Text = s, Selected = false });
            }

            return list;
        }


        /// <summary>
        ///  過去５年分のリストを返却
        /// </summary>
        /// <returns>年の選択リスト</returns>
        public static IEnumerable<SelectListItem> GetYearSelectList()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 0; i < 5; i++)
            {
                DateTime bb = DateTime.Now.AddYears(-i);

                list.Add(new SelectListItem() { Value = bb.ToString("yyyy"), Text = bb.ToString("yyyy年"), Selected = (0 == i) });
            }

            return list;

        }

        public const string ALL_YEAR_TEXT = "全年度";
        /// <summary>
        /// 過去のリストを返却
        /// </summary>
        /// <param name="num_years">年数</param>
        /// <param name="exclude_all_year">全年度を除外するかどうか</param>
        /// <returns>年の選択リスト</returns>
        public static IEnumerable<SelectListItem> GetYearSelectListV2([Range(5, 20)] int num_years = 5, bool exclude_all_year = false)
        {
            List<SelectListItem> l = exclude_all_year ? new() : new()
            {
                new SelectListItem() { Value = "0", Text = ALL_YEAR_TEXT, Selected = true }
            };
            DateTime d = DateTime.Now;
            for (int i = 0; i < num_years; i++)
            {
                l.Add(new SelectListItem() { Value = $"{d.Year}", Text = $"{d.Year}年", Selected = exclude_all_year && i == 0 });
                d = d.AddYears(-1);
            }
            return l;
        }

        /// <summary>
        ///  １２ヶ月のリストを返却
        /// </summary>
        /// <returns>月の選択リスト</returns>
        public static IEnumerable<SelectListItem> GetMonthSelectList()
        {
            List<SelectListItem> list = new();

            //return Enumerable.Range(DateTime.Now.Month - 9, 10).Select(t => new DropDownListModel() { Value = t.ToString(), DisplayText = t.ToString() });

            for (int i = 1; i <= 12; i++)
            {
                   list.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString("#月"), Selected = (0 == i) });
            }

            return list;

        }

        /// <summary>
        /// M_Code_Dataのリストの返却
        /// </summary>
        /// <param name="_mapApiSettiong">API設定</param>
        /// <param name="CodeID">コードID</param>
        /// <param name="flgNoDelFlg">削除フラグデータも取得するかどうか</param>
        /// <returns>コードデータの選択リスト</returns>
        public static async Task<List<SelectListItem>> GetCodeDataSelectList(MapApiSettings _mapApiSettiong, int CodeID, bool flgNoDelFlg = false)
        {
            List<SelectListItem> list = new();

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            List<Dto.M_Code_Data_Local> dataList = await api.M_Code_DataList(CodeID);
            if (flgNoDelFlg) dataList = dataList.Where(m => m.Del_Flg == false).ToList();
            IOrderedEnumerable<Dto.M_Code_Data_Local> querySyasyu = dataList.OrderBy(x => x.SortOrder);
            foreach (var item in querySyasyu)
            {
                list.Add(new SelectListItem() { Value = item.Code_Data.ToString(), Text = item.Code_Name, Selected = false });
            }
            return list;

        }

        /// <summary>
        /// M_Report_Serch_Kubunのリストを取得
        /// </summary>
        /// <param name="api_setting">API設定</param>
        /// <param name="reportSearchId">レポート検索ID</param>
        /// <param name="selectedId">選択されたID</param>
        /// <returns>レポート検索区分の選択リスト</returns>
        public static async Task<List<SelectListItem>> GetReportSearchKubunList(MapApiSettings api_setting, int reportSearchId, int? selectedId)
            => (await (new MasterDataApi(api_setting)).GetReportSearchKubunList(reportSearchId))
            .OrderBy(x => x.Sort_Order)
            .Select(x => new SelectListItem() { Value = x.Report_Serch_Kubun_ID.ToString(), Text = x.Display_Title, Selected = selectedId == x.Report_Serch_Kubun_ID })
            .ToList();

    }
}
