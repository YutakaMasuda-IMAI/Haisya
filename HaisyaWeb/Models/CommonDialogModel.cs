using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 共通ダイアログモデルクラス
    /// </summary>
    public class CommonDialogModel
    {
        /// <summary>
        /// 共通顧客選択画面用モデルクラス
        /// </summary>
        public class SelectAddressModel : CommonDialogBase
        {
            /// <summary>マップAPIのURL（JS用）</summary>
            public string MapsApiForJSUrl { get; set; }

            /// <summary>マップAPIの認証環境　0：Local,１：Server　</summary>
            public int WebViewFlg { set; get; } = 0;

            /// <summary>マップAPIの認証等設定用（JS用）</summary>
            public MapApiSettings MapApiSettings { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            /// <summary>地図表示区分 </summary>
            public int MapDisplayKubun { set; get; }

            /// <summary>
            /// 所検索使用率TOP30
            /// 住所検索使用履歴TOP30 
            /// </summary>
            public List<Dto.T_Anken_Point_Local> AnkenPointList { set; get; }

            public List<Dto.M_Area_Local> AreaList { set; get; }

            public List<Dto.M_Area_Ken_Local> AreaKenList { set; get; }
        }

        /// <summary>
        /// 共通親顧客選択画面用model
        /// </summary>
        public class SelectCustomerOyaModel : CommonDialogBase
        {

            public List<Dto.M_Customer_Local> CustomerList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            /// <summary>
            /// 検索用顧客コード
            /// </summary>
            public string SearchCustomerCd {  set; get; }

        }

        /// <summary>
        /// 共通顧客選択画面用モデルクラス
        /// </summary>
        public class SelectCustomerModel : CommonDialogBase
        {

            public List<Dto.M_Customer_Branch_Local> CustomerBranchList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

        }

        /// <summary>
        /// 共通顧客担当選択画面用model
        /// </summary>
        public class SelectCustomerTantouModel : CommonDialogBase
        {

            public List<Dto.M_Customer_Tantou_Local> CustomerTantouList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            public int CustomerID { set; get; }

        }

        /// <summary>
        /// 共通仕入れ業者選択画面用モデルクラス
        /// </summary>
        public class SelectVenderModel : CommonDialogBase
        {

            public List<Dto.M_Vender_Local> VenderList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

        }
        
        /// <summary>
        /// 共通車輌選択画面用モデルクラス
        /// </summary>
        public class SelectSyaryoManagementModel : CommonDialogBase
        {
            /// <summary>M_SyaryoManagementリスト </summary>
            public List<Dto.M_SyaryoManagement_Local> SyaryoManagementList { set; get; }

            /// <summary>M_SyaryoManagementリスト </summary>
            public List<Dto.M_CompanyDriver_Syaryo_Local> CompanyDriverSyaryoList { set; get; }

            /// <summary>M_CompanyDriverリスト </summary>
            public List<Dto.M_CompanyDriver_Local> CompanyDriverList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            [Display(Name = "担当車輌")]
            public int MyTantouOnlyFlg { set; get; } = 0;

            //選択担当者
            public IEnumerable<SelectListItem> TantouSelectList { set; get; }

            //選択車種
            public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

            ////選択型
            //public IEnumerable<SelectListItem> KataSelectList { set; get; }

            [Display(Name = "配車担当")]
            public string SelectTantou { get; set; }

            [Display(Name = "車種")]
            public int SelectSyasyuID { get; set; }

            //[Display(Name = "型")]
            //public string SelectKata { get; set; }

            [StringLength(4)]
            [Display(Name = "車番")]
            public string Syaban { get; set; }

        }

        /// <summary>
        /// 共通乗務員選択画面用モデルクラス
        /// </summary>
        public class SelectCompanyDriverModel : CommonDialogBase
        {
            /// <summary>V_Customer_Driverリスト </summary>
            public List<Dto.V_CompanyDriver_Local> CompanyDriverList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            [Display(Name = "担当車輌のみ")]
            public bool MyTantouOnlyFlg { set; get; } = true;

            //選択担当者
            public IEnumerable<SelectListItem> TantouSelectList { set; get; }

            //選択車種
            public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

            [Display(Name = "配車担当")]
            public string SelectTantou { get; set; }

            [Display(Name = "車種")]
            public int SelectSyasyuID { get; set; }

            [StringLength(4)]
            [Display(Name = "車番")]
            public string Syaban { get; set; }

            /// <summary> 現在使用中のみを表示するかどうかのフラグ 0:全て、1:使用中 </summary>
            [Display(Name = "使用中のみ")]
            public bool NowUsingSyaryoFlg { get; set; }
        }

        /// <summary>
        /// 共通傭車選択画面用モデルクラス
        /// </summary>
        public class SelectCustomerDriverModel : CommonDialogBase
        {
            /// <summary>V_Customer_Syaryoリスト </summary>
            public List<Dto.V_Customer_Syaryo_Local> CustomerSyaryoList { set; get; }

            /// <summary>M_CompanyBranchリスト </summary>
            public List<Dto.M_Customer_Branch_Local> CustomerBranchList { set; get; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public string ListIdName { set; get; }

            //選択担当者
            public IEnumerable<SelectListItem> TantouSelectList { set; get; }

            //選択車種
            public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

            [Display(Name = "配車担当")]
            public string SelectTantou { get; set; }

            [Display(Name = "車種")]
            public int SelectSyasyuID { get; set; }

            [StringLength(4)]
            [Display(Name = "車番")]
            public string Syaban { get; set; }

            /// <summary>傭車コード検索 </summary>
            public string code { get; set; }

            /// <summary>傭車名検索 </summary>
            public string key { get; set; }

            /// <summary>電話(Fax)番号検索 </summary>
            public string phone { get; set; }

            /// <summary>HTMLのList表示用DivのID名 </summary>
            public int CustomerBranchID { get; set; }

            /// <summary> 現在使用中のみを表示するかどうかのフラグ </summary>
            public int NowUsingSyaryoFlg { get; set; }

        }

        /// <summary>
        /// 共通選択画面ベースクラス
        /// </summary>
        public class CommonDialogBase
        {
            /// <summary>会社ID </summary>
            public int CompanyID { set; get; }

            public int SelectedID { set; get; }

            public int SelectedCD { set; get; }

            public int UserID { set; get; }

            /// <summary> 表示数 </summary>
            public int SelectTopCount { set; get; }

            /// <summary> 表示用タイトル </summary>
            public string TheadTitle { set; get; }
        }
    }
}
