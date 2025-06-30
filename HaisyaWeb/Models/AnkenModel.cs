using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HaisyaWeb.Models.MapApiModel;

namespace HaisyaWeb.Models
{
    public class AnkenModel : CommonModel
    {
        /// <summary>
        /// 案件画面修正用DTO
        /// データベースからデータ取得用
        /// </summary>
        public class AnkenDataModelDto_Local
        {
            public T_Anken_Local T_Anken { set; get; }

            public T_Anken_Detail_Local T_Anken_Detail { set; get; }

            public T_Anken_Publish_Local T_Anken_Publish { set; get; }

            public List<T_Anken_OyaKokyaku_Local> T_Anken_OyaKokyakuList { set; get; }

            public List<T_Anken_Point_Local> T_Anken_PointList { set; get; }

            public List<T_Anken_Excharge_Local> T_Anken_ExchargeList { set; get; }

            public T_Anken_Riyounso_Local T_Anken_Riyounso { set; get; }

            public List<T_Anken_Luggage_Local> T_Anken_LuggageList { set; get; }

            public List<T_Anken_Equipment_Local> T_Anken_EquipmentList { set; get; }

            public List<T_Anken_Riyounso_Point_Local> T_Anken_Riyounso_PointList { set; get; }

            public List<T_Anken_Display_Local> T_Anken_DisplayList { set; get; }

            public V_LoginUser_Local V_LoginUser { set; get; }

            public M_Customer_Tantou_Local M_Customer_Tantou { set; get; }

            [StringLength(255)]
            public string AnkenRemarks { get; set; }
        }


        /// <summary>
        /// 案件データ登録時の返却用DTO
        /// </summary>
        public class UpdateAnkenDataDto
        {
            public T_Anken_Local Anken { get; set; }

            public string ErrrMessage { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class AnkenCopyDataDto_Local : WebApplication.Model.AnkenCopyDataDto;



        /// <summary>
        /// 住所検索画面DTO（モーダル）
        /// </summary>
        public class SelectAddressModalDto
        {
            public int CompanyID { set; get; }

            public int UserID { set; get; }

            public List<Dto.M_Area_Local> AreaList { set; get; }

            /// <summary>
            /// 登録ポイントの表示区分
            /// </summary>
            public int SelectPointKubun { set; get; }

            public IEnumerable<SelectListItem> GroupSelectList { get; set; }
        }

        /// <summary>
        /// 住所検索画面の検索タブ用DTO（モーダル）
        /// </summary>
        public class SearchAddressListDto
        {
            public List<Dto.M_PostCode_Local> SearchPostCodeList { set; get; }

            public List<Map_Building_NameItem_Local> SearchTatemonoList { set; get; }
        }

        /// <summary>
        /// ポイント登録画面用DTO（モーダル）
        /// </summary>
        public class PointRegisterDto
        {
            public Dto.T_Point_Local PointData { get; set; }

            public int SelectPointKubun { get; set; }

            public int SelectGroupID { get; set; }

            public int CompanyID { set; get; }

            public int UserID { set; get; }

            public IEnumerable<SelectListItem> GroupSelectList { get; set; }
        }

        /// <summary>
        /// 荷物情報選択画面用DTO（モーダル）
        /// </summary>
        public class SelectLuggageDto
        {
            public List<Dto.T_Anken_Luggage_Local> AnkenLuggageList { get; set; }

            public List<Dto.M_Luggage_Local> LuggageList { get; set; }

            public List<Dto.M_Luggage_Group_Local> LuggageGroupList { get; set; }

            public int CompanyID { set; get; }

            public int UserID { set; get; }

            public int AnkenID { set; get; }
        }

        /// <summary>
        /// 装備品情報選択画面用DTO（モーダル）
        /// </summary>
        public class SelectEquipmentDto
        {
            public List<Dto.T_Anken_Equipment_Local> AnkenEquipmentList { get; set; }

            public List<Dto.M_Equipment_Local> EquipmentList { get; set; }

            public List<Dto.M_Equipment_Group_Local> EquipmentGroupList { get; set; }

            public int CompanyID { set; get; }

            public int UserID { set; get; }

            public int AnkenID { set; get; }
        }

        /// <summary>
        /// 案件画面登録用DTO
        /// </summary>
        public class AnkenRegisterModel : SearchModelForAnkenList
        {
            /// <summary>マップAPIの認証等設定用（JS用）</summary>
            public MapApiSettings MapApiSettings { set; get; }

            /// <summary>マップAPIの認証環境　0：Local,１：Server　</summary>
            public int WebViewFlg { set; get; } = 0;

            //public string MapUrl { set; get; }

            public int UserID { set; get; }

            public string Title { set; get; }

            /// <summary>
            /// 登録画面
            /// "Anken"：案件画面、"DilyReport"：日報画面
            /// </summary>
            public string PageType { set; get; } = "Anken";

            /// <summary>選択過去案件ID</summary>
            public int Anken_ID_KakoSelect { set; get; }

            /// <summary>案件ステータス</summary>
            public int SelectPointTab { get; set; }

            /// <summary>経由ポイント</summary>
            public List<PointDto_Local> PointList { get; set; }
            /// <summary>経由ポイント選択リスト</summary>
            public IEnumerable<SelectListItem> PointSelectList { set; get; }
            /// <summary>経由ポイント引き込み選択リスト</summary>
            public IEnumerable<SelectListItem> PointSelectRoadTypeList { set; get; }

            /// <summary>ポイント暫定確定選択</summary>
            public IEnumerable<SelectListItem> PointComboBoxItemsStatusKubun { get; set; }
            /// <summary>ポイント暫定確定選択</summary>
            public IEnumerable<SelectListItem> PointComboBoxItemsTimeKubun { get; set; }

            /// <summary>ポイント暫定確定選択</summary>
            public IEnumerable<SelectListItem> HighwayKubunList { set; get; }

            #region T_Anken
            /////////////////////////T_Anken//////////////////////////////
            public int Anken_ID { get; set; }
            [Display(Name = "案件№")]
            public string Anken_No { get; set; }
            public int Anken_Status { get; set; }
            public int Anken_Latest_Order { get; set; }
            public int Company_ID { get; set; }
            public int Branch_ID { get; set; }
            public int Anken_Kubun { get; set; }
            public int SenzokuID { get; set; } = 0;
            public int SenzokuDriverID { get; set; } = 0;
            #endregion T_Anken

            #region T_Anken_Detail
            /////////////////////////T_Anken_Detail//////////////////////////////
            /// <summary>
            /// 登録区分　：　利用運送の場合、0：都度運行、1：固定運行
            /// </summary>
            public int RegKubun { get; set; }

            /// <summary>配車日</summary>
            [Column(TypeName = "date")]
            public DateTime TargetDate { get; set; }

            /// <summary>マップAPIのURL（JS用）</summary>
            public string MapsApiForJSUrl { get; set; }
            /// <summary>案件ステータス</summary>
            public int AnkenStatus { get; set; }

            [Display(Name = "配車予定")]
            public int HaisyaPlanKubun { get; set; }
            [Display(Name = "配車乗務員ID")]
            public int HaisyaDriverId { get; set; }
            [Display(Name = "配車乗務員車輌ID")]
            public int HaisyaDriverSyaryoId { get; set; }
            [Display(Name = "配車乗務員ID")]
            public string HaisyaDriverDisplay { get; set; }

            [Display(Name = "案件名")]
            public string WorkName { get; set; }
            /// <summary>顧客情報</summary>
            [Display(Name = "顧客")]
            public new int KokyakuId { get; set; }
            [Display(Name = "顧客CD")]
            public string KokyakuCode { get; set; }
            [Display(Name = "顧客名")]
            public new string KokyakuName { get; set; }
            [Display(Name = "顧客担当")]
            public int KokyakuTantouId { get; set; }
            [Display(Name = "顧客担当")]
            public string KokyakuTantouName { get; set; }
            [Display(Name = "顧客担当電話")]
            public string KokyakuTantouPhone { get; set; }
            [DisplayName("顧客情報")]
            [StringLength(255)]
            [DataType(DataType.MultilineText)]
            public string KokyakuInfoDisplay { get; set; }

            [Display(Name = "固定電話")]
            public string KokyakuTantouPhone1 { get; set; }
            [Display(Name = "Fax電話")]
            public string KokyakuTantouFax1 { get; set; }
            [Display(Name = "携帯電話")]
            public string KokyakuTantouCellPhone { get; set; }

            [DisplayName("親顧客リスト")]
            public List<T_Anken_OyaKokyaku_Local> OyaKokyakuListData { get; set; }
            [DisplayName("親顧客")]
            public string OyaKokyakuLabel { get; set; }

            [DisplayName("その他追加費用")]
            public List<ExtraChargeDto_Local> ExtraChargeList { get; set; }
            [DisplayName("その他追加費用")]
            public List<AnkenExchargeDto_Local> ExtraChargeListEx { get; set; }

            [Display(Name = "運賃\r\nエリア")]
            public int Area { set; get; }

            /// <summary>車種タイプ情報</summary>
            [Display(Name = "車種")]
            public int SyasyuID { get; set; }
            public string SyasyuDisplay { get; set; }
            public string Syasyu { get; set; }
            public string SyasyuSize { get; set; }
            public string Kata { get; set; }

            [Display(Name = "荷物重量")]
            public double LuggageWeight { get; set; }

            /// <summary>乗務員原価計算区分</summary>
            [Display(Name = "乗務員原価計算区分")]
            public int DriverGrossCalc { get; set; }
            public IEnumerable<SelectListItem> DriverGrossCalcSelectList { set; get; }

            /// <summary>台数</summary>
            [Display(Name = "台数")]
            public int Daisuu { get; set; }
            public IEnumerable<SelectListItem> DaisuuSelectList { set; get; }

            /// <summary>案件担当</summary>
            [Display(Name = "配車")]
            public string HaisyaTantouID { get; set; }
            //public string HaisyaTantouName { get; set; }

            /// <summary>案件営業担当</summary>
            [Display(Name = "営業")]
            public int EigyoID { get; set; }
            //public string EigyoName { get; set; }
            public IEnumerable<SelectListItem> EigyoSelectList { set; get; }

            /// <summary>案件公開グループ</summary>
            [DisplayName("案件公開設定")]
            public int PublishGroup { get; set; }
            public IEnumerable<SelectListItem> PublishGroupSelectList { set; get; }
            public bool PublishFlg { get; set; }
            public DateTime? PublishFromDatetime { get; set; }
            public DateTime? PublishToDatetime { get; set; }

            /// <summary>ルート検索オプション情報</summary>
            [Display(Name = "フェリー")]
            public string Ferry { get; set; }
            public IEnumerable<SelectListItem> FerrySelectList { set; get; }
            [Display(Name = "規制考慮")]
            public string TimeRestriction { get; set; }
            public IEnumerable<SelectListItem> TimeRestrictionSelectList { set; get; }
            [Display(Name = "2段階Uﾀｰﾝ\r\n回避指定")]
            public string Twouturn { get; set; }
            public IEnumerable<SelectListItem> TwouturnSelectList { set; get; }

            public bool EigyoshoModori { get; set; }

            /// <summary>請求金額区分（暫定/確定）</summary>
            [Display(Name = "運賃区分")]
            public int SeikyuKubun { get; set; }

            /// <summary>高速区分</summary>
            [Display(Name = "高速代")]
            public int TollKubun { get; set; }
            [Display(Name = "高速代指定")]
            [Column(TypeName = "money")]
            public double TollMoney { get; set; }
            [Display(Name = "高速代備考")]
            [StringLength(255)]
            [DataType(DataType.MultilineText)]
            public string TollRemarks { get; set; }

            // 積卸し時間
            public string TsumiTaskTime { get; set; }
            public string OroshiTaskTime { get; set; }

            public bool CheckOroshiSpace { get; set; }
            public bool EdnGoBackEigyosyo { get; set; }

            /// <summary>車番連絡</summary>
            [Display(Name = "車番連絡")]
            public int NumberCommLimitKubun { get; set; }

            [Display(Name = "希望期日")]
            public DateTime? NumberCommLimitDateTime { get; set; }

            public string Address { get; set; }

            public string SelectArea { get; set; }
            public string SelectKen { get; set; }
            public string SelectShiku { get; set; }
            public string SelectChyo { get; set; }

            public double Height { get; set; }
            public double Width { get; set; }
            public double Weight { get; set; }
            public double Nenpi { get; set; }

            [Display(Name = "基本運賃")]
            public double BaseFee { get; set; }  //基本運賃
            [Display(Name = "追加費用")]
            public double ExtraCharge { get; set; }     //追加費用
            [Display(Name = "有料道路")]
            public double Toll { get; set; }    //有料道路
            [Display(Name = "値引額")]
            public double Discount { get; set; }    //割引額
            [Display(Name = "請求運賃")]
            public double GrossAmount { get; set; }     //請求運賃

            // 選択されたドライブルートリスト
            public DriveRouteListDisplay_Local SelectedDriveRouteDisplay { set; get; }

            public string ExecType { get; set; }

            [DisplayName("特記事項")]
            [StringLength(255)]
            [DataType(DataType.MultilineText)]
            public string SyabanRenrakuRemarks { get; set; }

            public List<Dto.T_Anken_Luggage_Local> AnkenLuggageList { get; set; }

            public List<Dto.T_Anken_Equipment_Local> AnkenEquipmentList { get; set; }

            [DisplayName("荷物情報")]
            [DataType(DataType.MultilineText)]
            public string LuggageDisplay { get; set; }

            [DisplayName("装備品情報")]
            [DataType(DataType.MultilineText)]
            public string EquipmentDisplay { get; set; }

            [DisplayName("内部連絡")]
            [StringLength(100)]
            public string Notice { get; set; }
            #endregion T_Anken_Detail
        }

        /// <summary>
        /// 利用運送登録用Dto
        /// </summary>
        public class RiyoUnsoAnkenRegisterModel : SearchModelForAnkenList
        {
            public MapApiSettings MapApiSettings { set; get; }

            public int WebViewFlg { set; get; } = 0;

            /// <summary>
            /// 登録区分、０：新規、１：修正
            /// </summary>
            public int RegKubun { set; get; } = 0;

            public int UserID { set; get; }

            public T_Anken_Riyounso_Local AnkenRiyounsoData { set; get; }

            public List<T_Anken_Riyounso_Point_Local> AnkenRiyounsoPointList { set; get; }

            /////////////////////////T_Anken//////////////////////////////
            public int Anken_ID { get; set; }
            public string Anken_No { get; set; }
            public int Anken_Status { get; set; }
            public int Anken_Latest_Order { get; set; }
            public int Company_ID { get; set; }
            public int Branch_ID { get; set; }
            public int Anken_Kubun { get; set; }

            /// <summary>マップAPIのURL（JS用）</summary>
            public string MapsApiForJSUrl { get; set; }

            /// <summary>
            /// 登録区分　：　利用運送の場合、0：都度運行、1：固定運行
            /// </summary>
            public int Reg_Kubun { get; set; }
        }

        /// <summary>
        /// ドライブルート検索結果表示用DTO
        /// </summary>
        public class DriveRouteListModel
        {
            public List<DriveRouteListDisplay_Local> DriveRouteListData { set; get; }

            public List<ExtraChargeDto_Local> ExtraChargeList { set; get; }
        }

        /// <summary>
        /// 案件変更履歴画面
        /// </summary>        
        public class AnkenRirekiModel
        {
            public List<Dto.V_AnkenDataList_Local> AnkenList { set; get; }

            public List<Dto.M_Code_Data_Local> CodeList { set; get; }

            public List<Dto.M_CompanyUser_Local> CompanyUserList { set; get; }

            public List<Dto.M_CompanyUser_Group_Local> CompanyUserGroupList { set; get; }

            public List<Dto.T_Anken_Point_Local> AnkenPointList { set; get; }
        }

        /// <summary>
        /// 案件画面　自動車ルート詳細画面
        /// </summary>
        public class RouteDetailModel : DriveRouteListDto_Local
        {
            public int WebViewFlg { set; get; }

            public MapApiSettings MapApiSettings { set; get; }

            /// <summary>マップAPIのURL（JS用）</summary>
            public string MapsApiForJSUrl { get; set; }

            public DriveRouteListDisplay_Local driveList { get; set; }

            public string routeType { get; set; }

            public string StartAddress { get; set; }

            public string EndAddress { get; set; }

            public int EndCount { get; set; }

            public Latlon CenterLatlon { set; get; }
        }

        /// <summary>
        /// 案件一覧用DTO
        /// </summary>
        public class AnkenListModel
        {
            /// <summary> 検索項目 </summary>
            public SearchModelForAnkenList Search { get; set; }

            public IEnumerable<V_AnkenDataList_Local> AnkenDataLists { get; set; }

            public List<V_HaisyaDataList_Local> HaisyaDataLists { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 案件一覧用検索用DTO
        /// </summary>
        public class SearchModelForAnkenList : CommonModel
        {
            /// <summary>
            /// Select Tab
            /// </summary>
            public string SelectTab { get; set; }

            /// <summary>
            /// Select Group
            /// </summary>
            public int SelectGroup { get; set; }

            /// <summary>
            /// Select Filter
            /// </summary>
            public int SelectFilter { get; set; }

            /// <summary>
            /// Check single switch
            /// </summary>
            public bool SingleSwitch { get; set; }

            /// <summary>
            /// Kokyaku Id
            /// </summary>
            public string KokyakuId { get; set; }

            /// <summary>
            /// Kokyaku Name
            /// </summary>
            public string KokyakuName { get; set; }

            /// <summary>
            /// Select Seikyu Tantou
            /// </summary>
            public string SelectSeikyuTantou { get; set; }

            /// <summary>
            /// Check collapse
            /// </summary>
            public bool IsCollapse { get; set; }
        }

        public class AnkenBaseModel : SenzokuBaseModel
        {

        }

        public class SenzokuBaseModel : SearchModelForAnkenList
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int Company_ID { get; set; }

            /// <summary>
            /// ログインユーザーID
            /// </summary>
            public int User_ID { set; get; }

            // <summary>
            /// 選択された日付
            /// </summary>
            [Column(TypeName = "date")]
            public DateTime TargetDate { set; get; }

            /// <summary>
            /// SenzokuID
            /// </summary>
            public int SenzokuID { set; get; }

            /// <summary>
            /// SenzokuDriverID
            /// </summary>
            public int SenzokuDriverID { set; get; }

            /// <summary>
            /// AnkenId
            /// </summary>
            public int AnkenId { set; get; }

            /// <summary>
            /// 専属担当グループID
            /// </summary>
            public int TantouID { set; get; }
        }

        /// <summary>
        /// 専属乗務員一覧用DTO
        /// </summary>
        public class SenzokuSelectDriverModel : SenzokuBaseModel
        {
            public List<V_Senzoku_Local> SenzokuList { get; set; }

            public List<V_Senzoku_Driver_Local> SenzokuDriverList { get; set; }

            public List<M_CompanyDriver_Local> CompanyDriverList { get; set; }

            public List<Dto.M_CompanyUser_Group_Local> CompanyUserGroupList { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class SenzokuRegsterListModel : SenzokuBaseModel
        {
            public V_Senzoku_Local SenzokuData { get; set; }

            public V_Senzoku_Driver_Local SenzokuDriverData { get; set; }

            public M_CompanyDriver_Local CompanyDriverData { get; set; }

            public IEnumerable<Dto.V_AnkenDataList_Local> AnkenDataList { get; set; }

            public List<Dto.V_HaisyaDataList_Local> haisyaList { get; set; }
        }

        /// <summary>
        /// 過去案件検索一覧画面
        /// </summary>
        public class SelectKakoAnkenListDto
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int Company_ID { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForAnkenList Search { get; set; }

            [Display(Name = "年月")]
            [Column(TypeName = "date")]
            public DateTime SelectMonth_From { get; set; }

            [Display(Name = "年月")]
            [Column(TypeName = "date")]
            public DateTime SelectMonth_To { get; set; }

            /// <summary>
            /// V_AnkenDataListリスト
            /// </summary>
            public IEnumerable<V_AnkenDataList_Local> AnkenDataLists { get; set; }

            /// <summary>
            /// 案件登録用モデル
            /// </summary>
            public AnkenRegisterModel AnkenRegister { get; set; }
        }

        /// <summary>
        /// 案件照会画面
        /// </summary>
        public class AnkenReferenceModel
        {
            /// <summary> 会社ID </summary>
            public int Company_ID { get; set; }
            /// <summary> 権限 </summary>
            public bool EditEnabled { set; get; }

            public Dto.V_AnkenDataList_Local AnkenData { set; get; }

            public List<Dto.M_Code_Data_Local> CodeList { set; get; }

            public List<Dto.M_CompanyUser_Local> CompanyUserList { set; get; }

            public List<Dto.M_CompanyUser_Group_Local> CompanyUserGroupList { set; get; }

            public List<Dto.T_Anken_Point_Local> AnkenPointList { set; get; }

            public List<Dto.V_HaisyaDataList_Local> HaisyaDataList { set; get; }

            public IEnumerable<SelectListItem> HaisyaKubunSelectList { set; get; }

            public int AnkenDisplayID { get; set; }

            public M_Report_Serch_Kubun_Local Report_Serch_Kubun { get; internal set; }
        }

        /// <summary>
        /// 案件照会傭車登録画面
        /// </summary>
        public class AnkenReferenceYosyaRegModel
        {

            /// <summary> 企業ID </summary>
            public int CompanyID { set; get; }

            /// <summary> ログインユーザID </summary>
            public int UserID { set; get; }

            /// <summary> 権限 </summary>
            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            /// <summary> 選択されたAnkenDisplayID </summary>
            public int AnkenDisplayID { set; get; }

            public Dto.V_AnkenDataList_Local AnkenData { set; get; }

            /// <summary> 選択担当者 </summary>
            public IEnumerable<SelectListItem> TantouSelectList { set; get; }

            /// <summary> 暫定・確定選択リスト </summary>
            public IEnumerable<SelectListItem> MoneyKubunSelectList { set; get; }

            public Dto.T_Haisya_Yosya_Local Yosya { set; get; }

            public bool UnsoFlg { set; get; }

            public string YosyaBranchName { set; get; }
            public string YosyaTantouName { set; get; }
            public string YosyaDriverName { set; get; }
            public string YosyaDriverSyaryoName { set; get; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SelectListItemEx : SelectListItem
    {
        public string Category { get; set; }
    }
}
