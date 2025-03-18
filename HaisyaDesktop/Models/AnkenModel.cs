using HaisyaDesktop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.Models
{
    public class AnkenModel : BaseModel
    {

        public AnkenModel()
        {

        }

        public class AnkenInfo
        {

            public int AnkenID { get; set; }

            public int KokyakuId { get; set; }

            public string KokyakuName { get; set; }

            public string SyasyuDisplay { get; set; }

            public string TsumiFacilityName { get; set; }

            public string TsumiAddress { get; set; }

            public Latlon TsumiPoint { get; set; }

            public int Daisuu { get; set; }

            public DateTime TsumiDate { get; set; }

            public DateTime OroshiDate { get; set; }

            public string Products { get; set; }
        }

        public class AnkenDataModelDto
        {

            public Dto.T_Anken_Local T_Anken { set; get; }

            public Dto.T_Anken_Detail_Local T_Anken_Detail { set; get; }

            public Dto.T_Anken_Publish_Local T_Anken_Publish { set; get; }

            public Dto.T_Anken_Remark_Local T_Anken_Remarks { set; get; }

            public Dto.T_Anken_Luggage_Local T_Anken_Luggage { set; get; }

            public List<Dto.T_Anken_OyaKokyaku_Local> T_Anken_OyaKokyakuList { set; get; }

            public List<Dto.T_Anken_Point_Local> T_Anken_PointList { set; get; }

            public List<Dto.T_Anken_Excharge_Local> T_Anken_ExchargeList { set; get; }

        }


        public class PointDto_Local : WebApplication.Model.PointDto
        {
            public Map_Building_NameItem_Local BuildingNameItem { get; set; }

            /// <summary>ポイント日時</summary>
            public DateTime? PointDateTime { get; set; }

            public bool btnPointReg { get; set; }

            //public bool btnRoadType { get; set; }


            /// <summary>ポイントステータス選択</summary>
            public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsStatusKubun { get; set; }
            /// <summary>ポイント暫定確定選択</summary>
            public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsTimeKubun { get; set; }
            /// <summary>ポイント道路選択</summary>
            public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsRoadType { get; set; }
            /// <summary>ポイント選択</summary>
            public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsPointKubun { get; set; }




        }

        public class UpdateAnkenDataDto
        {
            public Dto.T_Anken_Local Anken { get; set; }

            public string ErrrMessage { get; set; }
        }

    }


    //public class AnkenRegisterModel : SearchModelForAnkenList
    //{
    //    //public MapApiSettings MapApiSettings { set; get; }

    //    public int WebViewFlg { set; get; } = 0;

    //    //public string MapUrl { set; get; }



    //    /// <summary>案件ステータス</summary>
    //    public int SelectPointTab { get; set; }

    //    /// <summary>積みポイント</summary>
    //    public List<AnkenModel.PointDto_Local> TsumiPointList { get; set; }
    //    /// <summary>卸しポイント</summary>
    //    public List<AnkenModel.PointDto_Local> OroshiPointList { get; set; }
    //    /// <summary>経由ポイント</summary>
    //    public List<AnkenModel.PointDto_Local> ThroughPointList { get; set; }

    //    /// <summary>ポイント暫定確定選択</summary>
    //    public IEnumerable<SelectListItem> PointComboBoxItemsStatusKubun { get; set; }
    //    /// <summary>ポイント暫定確定選択</summary>
    //    public IEnumerable<SelectListItem> PointComboBoxItemsTimeKubun { get; set; }

    //    /////////////////////////T_Anken//////////////////////////////
    //    public int Anken_ID { get; set; }
    //    public string Anken_No { get; set; }
    //    public int Anken_Status { get; set; }
    //    public int Anken_Latest_Order { get; set; }
    //    public int Company_ID { get; set; }
    //    public int Branch_ID { get; set; }
    //    public int Anken_Kubun { get; set; }

    //    /// <summary>マップAPIのURL（JS用）</summary>
    //    public string MapsApiForJSUrl { get; set; }
    //    /// <summary>案件ステータス</summary>
    //    public int AnkenStatus { get; set; }

    //    public int HaisyaPlanKubun { get; set; }

    //    /// <summary>顧客情報</summary>
    //    [Display(Name = "顧客")]
    //    public int Customer_Branch_ID { get; set; }
    //    public string KokyakuName { get; set; }
    //    // <summary>顧客担当者ID</summary>
    //    public int KokyakuTantouId { get; set; }
    //    // <summary>顧客担当名称</summary>
    //    public string KokyakuTantouName { get; set; }
    //    // <summary>顧客担当名称</summary>
    //    public string KokyakuTantouPhone { get; set; }

    //    /// <summary>親顧客リスト</summary>
    //    public List<Dto.T_Anken_OyaKokyaku_Local> OyaKokyakuListData { get; set; }
    //    /// <summary>親顧客リスト</summary>
    //    public string OyaKokyakuLabel { get; set; }

    //    [Display(Name = "標準運賃\r\nエリア")]
    //    public string Eria { set; get; }

    //    /// <summary>車種タイプ情報</summary>
    //    [Display(Name = "車種")]
    //    public string SyasyuDisplay { get; set; }
    //    public string Syasyu { get; set; }
    //    public string SyasyuSize { get; set; }
    //    public string Kata { get; set; }
    //    //public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

    //    /// <summary>台数</summary>
    //    [Display(Name = "台数")]
    //    public int Daisuu { get; set; }
    //    public IEnumerable<SelectListItem> DaisuuSelectList { set; get; }

    //    /// <summary>案件担当</summary>
    //    [Display(Name = "担当\r\n配車")]
    //    public int TantouID { get; set; }
    //    public string TantouName { get; set; }
    //    //public IEnumerable<SelectListItem> TantouSelectList { set; get; }

    //    /// <summary>案件営業担当</summary>
    //    [Display(Name = "営業\r\n担当")]
    //    public int EigyoID { get; set; }
    //    public string EigyoName { get; set; }
    //    public IEnumerable<SelectListItem> EigyoSelectList { set; get; }

    //    //public string Products { get; set; }

    //    /// <summary>案件公開グループ</summary>
    //    [DisplayName("案件公開\r\nグループ")]
    //    public int PublishGroup { get; set; }
    //    public IEnumerable<SelectListItem> PublishGroupSelectList { set; get; }
    //    public bool PublishFlg { get; set; }
    //    public DateTime? PublishFromDatetime { get; set; }
    //    public DateTime? PublishToDatetime { get; set; }

    //    /// <summary>ルート検索オプション情報</summary>
    //    [Display(Name = "フェリー")]
    //    public string Ferry { get; set; }
    //    public IEnumerable<SelectListItem> FerrySelectList { set; get; }
    //    [Display(Name = "規制考慮")]
    //    public string Regulation { get; set; }
    //    public IEnumerable<SelectListItem> RegulationSelectList { set; get; }
    //    [Display(Name = "2段階Uﾀｰﾝ\r\n回避指定")]
    //    public string Twouturn { get; set; }
    //    public IEnumerable<SelectListItem> TwouturnSelectList { set; get; }

    //    public bool EigyoshoModori { get; set; }

    //    /// <summary>請求金額区分（暫定/確定）</summary>
    //    public int SeikyuKubun { get; set; }

    //    // 積卸し時間
    //    public string TsumiTaskTime { get; set; }
    //    public string OroshiTaskTime { get; set; }

    //    public bool CheckOroshiSpace { get; set; }
    //    public bool EdnGoBackEigyosyo { get; set; }

    //    /// <summary>車番連絡</summary>
    //    public int NumberCommLimitKubun { get; set; }
    //    //public DateTime? NumberCommLimitDate { get; set; }
    //    //public string NumberCommLimitTime { get; set; }
    //    public DateTime? NumberCommLimitDateTime { get; set; }

    //    public string Address { get; set; }

    //    public string SelectArea { get; set; }
    //    public string SelectKen { get; set; }
    //    public string SelectShiku { get; set; }
    //    public string SelectChyo { get; set; }

    //    public double Height { get; set; }
    //    public double Width { get; set; }
    //    public double Weight { get; set; }
    //    public double Nenpi { get; set; }

    //    public double BaseFee { get; set; }　　//基本運賃
    //    public double ExtraCharge { get; set; }     //追加費用
    //    public double Toll { get; set; }    //有料道路
    //    public double Discount { get; set; }    //割引額
    //    public double GrossAmount { get; set; }     //請求運賃


    //    // 選択されたドライブルートリスト
    //    public DriveRouteListDisplay_Local SelectedDriveRouteDisplay { set; get; }

    //    /// <summary></summary>
    //    public string ExecType { get; set; }

    //}


    //public class MapddressListModel
    //{

    //    public List<Map_Building_NameItem_Local> BuildingList { set; get; }

    //    public List<MapAddressItem_Local> Addresslist { set; get; }

    //}



    /// <summary>
    ///　案件画面　自動車ルート一覧
    /// </summary>
    public class DriveRouteListDto_Local
    {
        // ルートの追加料金
        public List<Dto.ExtraChargeDto_Local> ExchargeDataList { get; set; }

        public List<Dto.DriveRouteListDisplay_Local> DriveRouteListDisplayList { get; set; }

        public string ErrrMessage { get; set; }
    }



    public class DriveRouteListModel
    {
        public List<Dto.DriveRouteListDisplay_Local> DriveRouteListData { set; get; }

        public List<Dto.ExtraChargeDto_Local> ExtraChargeList { set; get; }
    }


    public class SelectListItemEx : SelectListItem
    {
        public string Category { get; set; }
    }


    public class AnkenListModel
    {
        /// <summary>検索項目</summary>
        public SearchModelForAnkenList Search { get; set; }

        public List<Dto.V_AnkenDataList_Local> AnkenDataLists { get; set; }

    }

    

    public class SearchModelForAnkenList : BaseModel
    {

        /// <summary>
        /// 
        /// </summary>
        public string SelectTab { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SelectGroup { get; set; }

    }

    /// <summary>
    /// 案件画面　自動車ルート詳細画面
    /// </summary>
    public class RouteDetailModel : DriveRouteListDto_Local
    {
        public int WebViewFlg { set; get; } = 0;

        //public AnkenModel RouteDetai { get; set; }

        //public MapApiSettings MapApiSettings { set; get; }

        /// <summary>マップAPIのURL（JS用）</summary>
        public string MapsApiForJSUrl { get; set; }

        public Dto.DriveRouteListDisplay_Local driveList { get; set; }

        public string routeType { get; set; }

        //public string routeTypeDisplay { get; set; }

        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public int EndCount { get; set; }

        //public int WebViewFlg { set; get; }

        public Latlon CenterLatlon { set; get; }


    }




}
