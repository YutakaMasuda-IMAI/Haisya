using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Model;
using static HaisyaWeb.Models.SalesPaymentModel;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 売上モデル
    /// </summary>
    public partial class SalesModel : SalesModel<
        CodeDataDto_Local,
        M_CompanyUser_Local,
        T_Nippou_Toll_Other_Local,
        T_Anken_Point_Local,
        T_Nippou_Toll_Local,
        M_KojinUnsyu_Kubun_Local,
        T_Uriage_Unsyu_Local,
        T_Uriage_Futan_Local,
        T_Uriage_Unchin_Local,
        T_Uriage_Shitabarai_Local,
        T_Uriage_Local,
        BaggageGroupDto_Local>
    {
        public SearchModelForSalesPaymentList Search { get; set; }

        /// <summary>
        /// 配車
        /// </summary>
        public V_HaisyaDataList_Local HaisyaData { get; set; }
        /// <summary>
        /// 配車一覧
        /// </summary>
        public List<V_HaisyaDataList_Local> HaisyaDataList { get; set; }
        /// <summary>
        /// 荷主負担合計
        /// </summary>
        public decimal? NinushiFutanTotal { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
        /// <summary>
        /// ユーザID
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// 売上運賃区分
        /// </summary>
        public int Uriage_Unchin_Kubun { get; set; }
        /// <summary>
        /// 売上支払い区分
        /// </summary>
        public int Uriage_Shiharai_Kubun { get; set; }
        /// <summary>
        /// 売上運収区分
        /// </summary>
        public int Uriage_Unsyu_Kubun { get; set; }
        /// <summary>
        /// 会社負担合計
        /// </summary>
        public decimal? KaishaFutanTotal { get; set; }
        /// <summary>
        /// 会社負担合計（日報情報の高速代[会社負担]とその他有料代[会社負担]の加算値）
        /// </summary>
        public decimal? KaishaFutanTotalTollFee { get; set; } = 0;
        /// <summary>
        /// 個人負担合計
        /// </summary>
        public decimal? KojinFutanTotal { get; set; }
        /// <summary>
        /// 立替金合計
        /// </summary>
        public decimal? TatekaekinTotal { get; set; }
        /// <summary>
        /// 積日
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 卸日
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 請求区分
        /// </summary>
        public IEnumerable<SelectListItem> SeikyuKubunDto { set; get; }
        /// <summary>
        /// 課税幾分
        /// </summary>
        public IEnumerable<SelectListItem> KazeiKubunDto { set; get; }
        /// <summary>
        /// 個人運収区分
        /// </summary>
        public IEnumerable<SelectListItem> KojinUnsyuKubunDto { set; get; }
        /// <summary>
        /// 売上区分
        /// </summary>
        public IEnumerable<SelectListItem> SalesKubunDto { set; get; }
        /// <summary>
        /// 売上営業
        /// </summary>
        public IEnumerable<SelectListItem> SalesBusinessSegmentDto { set; get; }
        /// <summary>
        /// ユニット
        /// </summary>
        public IEnumerable<SelectListItem> UnitDto { set; get; }
        /// <summary>
        /// ユーザグループ
        /// </summary>
        public IEnumerable<SelectListItem> UserGroupDto { set; get; }
        /// <summary>
        /// List of paid user
        /// </summary>
        public IEnumerable<SelectListItem> PaidUserDto { set; get; }
        /// <summary>
        /// List of prepayment excess amount category
        /// </summary>
        public IEnumerable<SelectListItem> AdvanceOverpaymentKubunDto { set; get; }
        /// <summary>
        /// List of overpayment category
        /// </summary>
        public IEnumerable<SelectListItem> OverpaymentKubunDto { set; get; }
        /// <summary>
        /// ローカウント
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// ExtraChargeList
        /// </summary>
        public List<ExtraChargeDto_Local> ExtraChargeList { set; get; }
        /// <summary>
        /// 処理区分
        /// </summary>
        public string ProcessingCategory { set; get; }
        /// <summary>
        /// NumOfInq
        /// </summary>
        public int NumOfInq { get; set; }
        /// <summary>
        /// フィルター数
        /// </summary>
        public int NumOfFilter { get; set; }
        /// <summary>
        /// 選択ユニットID
        /// </summary>
        public int SelectedUnitId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// IsRequestReadOnly
        /// </summary>
        public bool IsRequestReadOnly { get; set; } = false;
        /// <summary>
        /// IsUnderlingReadOnly
        /// </summary>
        public bool IsUnderlingReadOnly { get; set; } = false;
        public int ShimeDay { get; set; } = 1;
        public bool IsDeletable { get; set; } = true;
        public M_Customer_Uriage_Calc_Local CustomerUriageCalcData { set; get; }
        /// <summary>
        /// 売上負担一覧
        /// </summary>
        public List<Dto.T_Uriage_Futan_Local> UriageFutanList { get; set; }
        /// <summary>
        /// BurdenList
        /// </summary>
        public List<Dto.M_Burden_Local> BurdenList { get; set; }

        public M_Customer_Branch_Local YosyaData { get; set; }
        /// <summary>
        /// 個人負担を計算する
        /// </summary>
        /// <param name="driverId">ドライバーID</param>
        /// <returns>個人負担額</returns>
        public decimal? CalculatePersonalExpense(int? driverId)
        {
            decimal? personalTolls = this.NippouToll
                .Where(t => t.Driver_ID == driverId && t.Futan_Kubun == 1)
                .Sum(t => t.料金);

            decimal personalTollsOther = this.NippouTollOther
                .Where(t => t.Driver_ID == driverId && t.Futan_Kubun == 1)
                .Sum(t => t.Toll_Fee);

            return personalTolls + personalTollsOther;
        }

    }

    /// <summary>
    /// コードデータ
    /// </summary>
    public class CodeDataDto : WebApplication.Model.CodeDataDto
    {
    }

    public partial class BaggageGroupDto_Local : BaggageGroupDto
    {
    }

    /// <summary>
    /// ユニットを取得する
    /// </summary>
    public partial class M_Unit
    {
        /// <summary>
        /// ユニットID
        /// </summary>
        public int Unit_ID { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
        /// <summary>
        /// ソートオーダー 
        /// </summary>
        public int Sort_Order { get; set; }
        /// <summary>
        /// ユニット表示
        /// </summary>
        public string Unit_Display { get; set; }
        /// <summary>
        /// ユニットフォーマット
        /// </summary>
        public string Unit_Format { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int Del_Flg { get; set; }
    }

    public class ExtraChargeDto_Local : WebApplication.Model.AnkenModel.ExtraChargeDto { }

    /// <summary>
    /// ドライブルートリスト
    /// </summary>
    public class DriveRouteListDto_Local
    {
        /// <summary>
        /// ルートの追加料金
        /// </summary>
        public List<ExtraChargeDto_Local> ExchargeDataList { get; set; }
        /// <summary>
        /// DriveRouteListDisplayList
        /// </summary>
        public List<DriveRouteListDisplay_Local> DriveRouteListDisplayList { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrrMessage { get; set; }
    }

    /// <summary>
    /// 専属モデル
    /// </summary>
    public partial class SenzokuModel : WebApplication.Model.SenzokuModel
    {
        public SearchModelForSalesPaymentList Search { get; set; }
        /// <summary>
        /// Burden
        /// </summary>
        public List<Dto.M_Burden_Local> BurdenList { get; set; }
        /// <summary>
        /// ユーザID
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
        public DateOnly SeikyuDate { get; set; }
    }

    public partial class PostUriageDataModel : WebApplication.Model.PostUriageDataModel
    {

    }

    /// <summary>
    /// 請求情報リクエストDTO
    /// </summary>
    public class BillingInfoRequestDto
    {
        public int RowCount { get; set; }
        public string Model { get; set; }
        public string Unit { get; set; }
        public int SelectedUnitId { get; set; }
        public int UriageKubun { get; set; }
        public string ProcessingCategory { get; set; }
        public string Tsumi { get; set; }
        public string Oroshi { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public bool IsCreditSlip { get; set; }
        public bool IsFirstItem { get; set; }
        public int ShimeDay { get; set; }
        public DateTime? SeikyuDate { get; set; }
        public string CustomerUriageCalcData { get; set; }
    }

    /// <summary>
    /// 未払い情報リクエストDTO
    /// </summary>
    public class UnderPaymentInfoRequestDto : BillingInfoRequestDto
    {
        public string Baggage { get; set; }
        public int? YosyaBranchId { get; set; }
        public int? YosyaShiharaiShimeday { get; set; }
        public string YosyaBranchCode { get; set; }
        public string YosyaBranchNameAbbr { get; set; }
        public int? HaisyaKubun { get; set; }
        public DateTime? ShiharaiDate { get; set; }
        public int? Syaban { get; set; }
    }

    /// <summary>
    /// 売上DTOを取得する
    /// </summary>
    public class GetSalesDto
    {
        // 案件ID
        public int Anken_ID { get; set; }
        // 処理カテゴリ
        public int processingCategory { get; set; } = 2;
        // 区分
        public int kubun { get; set; }
        // 売上ID
        public int Uriage_ID { get; set; }

        public SearchModelForSalesPaymentList Search { get; set; }
    }
}
