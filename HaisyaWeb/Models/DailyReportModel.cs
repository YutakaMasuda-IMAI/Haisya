using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 日報モデル
    /// </summary>
    public class DailyReportModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForDailyReportList Search { get; set; }

            /// <summary>
            /// データリスト
            /// </summary>
            public List<HaisyaDataList> DataDataLists { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }

            /// <summary>
            /// 配車データリスト
            /// </summary>
            public List<Dto.V_HaisyaDataList_Local> HaisyaDataList { set; get; }

            /// <summary>
            /// 選択された配車データ
            /// </summary>
            public Dto.V_HaisyaDataList_Local HaisyaDataData { set; get; }

            /// <summary>
            /// 案件表示
            /// </summary>
            public T_Anken_Display_Local AnkenDisplay { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 日報リスト検索モデル
        /// </summary>
        public class SearchModelForDailyReportList : CommonModel
        {
            /// <summary> </summary>
            public string SelectTab { get; set; }

            /// <summary> </summary>
            public int SelectGroup { get; set; }

            /// <summary> </summary>
            public int SelectFilter { get; set; }

            /// <summary>案件担当</summary>
            [Display(Name = "請求担当")]
            public int TantouID { get; set; }
            public string TantouName { get; set; }

            /// <summary> </summary>
            public IEnumerable<SelectListItem> SelectSeikyuTantouList { set; get; }

            /// <summary> </summary>
            public bool SingleSwitch { get; set; }

            /// <summary> 選択キー  </summary>
            public int Anken_ID { get; set; }

            /// <summary> 選択キー  </summary>
            public int AnkenDisplay_ID { get; set; }

            /// <summary> 乗務員CD  </summary>
            public int DriverCd { get; set; }

            /// <summary> 車番  </summary>
            public int Syaban { get; set; }

            [Required]
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }

            /// <summary>
            /// 顧客ID
            /// </summary>
            public string KokyakuId { get; set; }

            /// <summary>
            /// 顧客名
            /// </summary>
            public string KokyakuName { get; set; }

            /// <summary>
            /// 折りたたみ状態
            /// </summary>
            public bool IsCollapse { get; set; }
        }

        /// <summary>
        /// 配車データリスト
        /// </summary>
        public class HaisyaDataList : V_HaisyaDataList_Local
        {
            /// <summary>
            /// 領収書フラグ
            /// </summary>
            public bool SetReciptFlg { set; get; }

            /// <summary>
            /// 日報
            /// </summary>
            public T_Nippou_Local TNippou { set; get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public HaisyaDataList() { }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list">配車データリスト</param>
            public HaisyaDataList(V_HaisyaDataList_Local list)
            {
                // 親クラスのプロパティ情報を一気に取得して使用する。
                List<PropertyInfo> props = list
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                    .ToList();

                props.ForEach(prop =>
                {
                    object propValue = prop.GetValue(list);
                    typeof(V_HaisyaDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
                });
            }
        }

        /// <summary>
        /// 日報登録モデル
        /// </summary>
        public partial class DailyReportRegistrationModel : DataListModel
        {
            public List<PointDto_Local> PointList { get; set; }

            public string ExecType { get; set; }

            /// <summary>  </summary>
            public int NumOfFilter { get; set; }

            public string Eria { set; get; }
            public string Ferry { get; set; }
            public string Regulation { get; set; }
            public string Twouturn { get; set; }
            public string TsumiTaskTime { get; set; }
            public string OroshiTaskTime { get; set; }
            public double Height { get; set; }
            public double Width { get; set; }
            public double Weight { get; set; }
            public double Nenpi { get; set; }
            public int SyasyuID { get; set; }
            public string SyasyuSize { get; set; }
            public string Syasyu { get; set; }
            public string Kata { get; set; }

            /// <summary> 選択キー  </summary>
            public int Anken_ID { get; set; }
            public int AnkenDisplay_ID { get; set; }
        }

        /// <summary>
        /// 日報登録詳細モデル
        /// </summary>
        public partial class DailyReportRegistrationDetailModel : DailyReportRegistrationDetailModel_Base
        {
            public V_HaisyaDataList_Local HaisyaDataList { get; set; }

            public List<DriveRouteListDisplay_Local> DriveRouteListData { set; get; }

            public List<PointDto_Local> PointList { get; set; }

            /// <summary>経由ポイント選択リスト</summary>
            public IEnumerable<SelectListItem> PointSelectList { set; get; }

            // 選択されたドライブルートリスト
            public DriveRouteListDisplay_Local SelectedDriveRouteDisplay { set; get; }

            /// <summary> M_Code_DataのID=3　ポイント区分名 </summary>
            public List<Dto.M_Code_Data_Local> PointKubunCode { set; get; }

            public string Area { set; get; }
            public string Ferry { get; set; }
            public string Regulation { get; set; }
            public string Twouturn { get; set; }
            public string TsumiTaskTime { get; set; }
            public string OroshiTaskTime { get; set; }
            public double? Height { get; set; }
            public double? Width { get; set; }
            public double? Weight { get; set; }
            public double? Nenpi { get; set; }
            public int SyasyuID { get; set; }
            public string SyasyuSize { get; set; }
            public string Syasyu { get; set; }
            public string Kata { get; set; }
            public int Company_ID { get; set; }
            public string PageType { set; get; } = "Anken";
            public int Anken_ID { get; set; }
            public int AnkenDisplay_ID { get; set; }

            public IEnumerable<SelectListItem> GroupUserSelectList { set; get; }
            public IEnumerable<SelectListItem> FutanKubunList { set; get; }

            public int Index { get; set; }
            public string FormattedIntervalTime { get; set; }
            public int SelectedFutanKubun { get; set; }

            /// <summary>
            /// 検索
            /// </summary>
            public SearchModelForDailyReportList Search { get; set; }
        }

        /// <summary>
        /// WebApplicationから返却されるDto
        /// </summary>
        public class DailyReportRegistrationDetailModel_Base
        {
            public T_Anken_Detail_Local AnkenDetail { get; set; }
            public T_Anken_Display_Local AnkenDisplay { get; set; }
            public List<T_Anken_Point_Local> PointLists { get; set; }
            public T_Uriage_Local Uriage { get; set; }
            public List<M_Customer_TollSeikyuKubun_Local> TollSeikyuKubun { get; set; }
            public string SeikyuRemarks { get; set; }
            public string DisplayName { get; set; }
            public string SyabanNumber { get; set; }
            public List<T_KUDGIVT_Local> DegitakoData { get; set; }
            public List<T_KUDGSIR_Local> HighwayData { get; set; }
            public List<int> KUDGSIRIdList { get; set; }
            public T_Nippou_Stay_Local Nippou_Stay { get; set; }
            public T_Nippou_Kaiso_Local Nippou_Kaiso { get; set; }
            public List<T_Nippou_Stay_Degitako_Local> Nippou_Stay_Degitako { get; set; }
            public T_Nippou_Local Nippou { get; set; }
            public List<T_Nippou_Kaiso_Degitako_Local> Nippou_Kaiso_Degitako { get; set; }
            public List<OtherPaidData_Local> OtherPaidDataList { get; set; }
            public int NumOfFilter { get; set; }
            public int Nippou_ID { get; set; }

            public List<int> NippouStayDegitakoIdList { get; set; }

            public List<int> NippouKaisoDegitakoIdList { get; set; }

            public T_Nippou_Approval_Local NippouApproval { set; get; }

            public string ExecType { get; set; }

            public List<CodeDataDto> CodeDataDto { get; set; }
            public List<T_Nippou_Toll_Other> NippouTollOther { get; set; }
            public List<T_Nippou_Toll_Other> InitialDisplayNippouTollOther { get; set; }
            public List<T_Nippou_Toll_Local> NippouToll { get; set; }
            public List<T_Nippou_Toll_Local> InitialDisplayNippouToll { get; set; }
            public List<T_Nippou_Toll_Local> NippouTollDegitako { get; set; }
            public int User_ID { get; set; }
            public string SelectDay { get; set; }
        }

        /// <summary>
        /// 日報通行料
        /// </summary>
        public partial class T_Nippou_Toll
        {
            [Key]
            public int Nippou_Toll_ID { get; set; }
            public int Nippou_ID { get; set; }
            public int Sort { get; set; }
            [StringLength(25)]
            public string 運行NO { get; set; }
            public DateTime? 読取日 { get; set; }
            public int? 事業所CD { get; set; }
            public DateTime 運行日 { get; set; }
            [StringLength(50)]
            public string 事業所名 { get; set; }
            public int? 車輌CD { get; set; }
            [StringLength(50)]
            public string 車輌名 { get; set; }
            public int? 乗務員CD { get; set; }
            [StringLength(50)]
            public string 乗務員名 { get; set; }
            public int? 対象乗務員区分 { get; set; }
            public DateTime? 開始日時 { get; set; }
            public DateTime? 終了日時 { get; set; }
            [StringLength(20)]
            public string 開始道路番号 { get; set; }
            [StringLength(50)]
            public string 開始道路名 { get; set; }
            [StringLength(50)]
            public string 開始ETC番号 { get; set; }
            [Required]
            [StringLength(50)]
            public string 開始IC名 { get; set; }
            [StringLength(20)]
            public string 終了道路番号 { get; set; }
            [StringLength(50)]
            public string 終了道路名 { get; set; }
            [StringLength(50)]
            public string 終了ETC番号 { get; set; }
            [Required]
            [StringLength(50)]
            public string 終了IC名 { get; set; }
            public int? 精算区分 { get; set; }
            [StringLength(50)]
            public string 精算区分名 { get; set; }
            public decimal? 料金 { get; set; }
            public double? 走行距離 { get; set; }
            public int? 高速車種区分 { get; set; }
            [StringLength(50)]
            public string 高速車種区分名 { get; set; }
            public decimal? 標準料金 { get; set; }
            public int? 料金区分 { get; set; }
            [StringLength(50)]
            public string 料金区分名 { get; set; }
            public int Futan_Kubun { get; set; }
            public DateTime? Insert_Datetime { get; set; }
            public int? Insert_User { get; set; }
            public DateTime? Update_Datetime { get; set; }
            public int? Update_User { get; set; }
            public int Index { get; set; } = -1;
            public string FormattedStartDate { get; set; }
            public string FormattedEndDate { get; set; }
        }

        /// <summary>
        /// 日報通行料その他
        /// </summary>
        public partial class T_Nippou_Toll_Other
        {
            public int Nippou_Toll_Other_ID { get; set; }
            public int Nippou_ID { get; set; }
            public int Sort { get; set; }
            public DateTime Day { get; set; }
            public int Toll_Kubun { get; set; }
            public string Start_Name { get; set; }

            public string End_Name { get; set; }
            public decimal Toll_Fee { get; set; }
            public double? Distance { get; set; }
            public int Futan_Kubun { get; set; }

            public DateTime? Insert_Datetime { get; set; }
            public int? Insert_User { get; set; }
            public DateTime? Update_Datetime { get; set; }
            public int? Update_User { get; set; }
            public IEnumerable<SelectListItem> GroupUserSelectList { set; get; }
            public int Index { get; set; }
            public CodeDataDto CodeDataDto { get; set; }
        }

        /// <summary>
        /// コードデータDTO
        /// </summary>
        public class CodeDataDto
        {
            public string Code_Data { get; set; }
            public string Code_Name { get; set; }
        }
    }

    /// <summary>
    /// 配車データリスト拡張
    /// </summary>
    public static class HaisyaDataListExtension
    {
        /// <summary>
        /// 配車区分ラベル
        /// </summary>
        /// <param name="h">配車データリスト</param>
        /// <returns>配車区分ラベル</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Haisya_kubun_label(this DailyReportModel.HaisyaDataList h)
            => h == null ? "" :
            h.Haisya_Kubun == 1 ? "自車" :
            h.Haisya_Kubun == 2 ? "傭車" :
            h.Haisya_Kubun == 3 ? "専属傭車" :
            h.Haisya_Kubun == 4 ? "自車専属" :
            h.Haisya_Kubun == 5 ? "自車専任" : "";
    }
}
