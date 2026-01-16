using System;
using System.Collections.Generic;
using WebApplication.Data;

#nullable disable

namespace WebApplication.Model
{
    public class SalesModel : SalesModel<
        CodeDataDto,
        M_CompanyUser,
        T_Nippou_Toll_Other,
        T_Anken_Point,
        T_Nippou_Toll,
        M_KojinUnsyu_Kubun,
        T_Uriage_Unsyu,
        T_Uriage_Futan,
        T_Uriage_Unchin,
        T_Uriage_Shitabarai,
        T_Uriage,
        BaggageGroupDto>
    { }

    /// <summary>
    /// 販売モデル
    /// </summary>
    public class SalesModel<
        CodeDataItemDataType,
        CompanyUserItemDataType,
        NippouTollOtherItemDataType,
        AnkenPointItemDataType,
        NippouTollItemDataType,
        KojinUnsyuKubunItemDataType,
        UriageUnsyuItemDataType,
        UriageFutanItemDataType,
        UriageUnchinItemDataType,
        UriageShitabaraiItemDataType,
        UriageDataType,
        BaggageGroupDataType>
        where CodeDataItemDataType : CodeDataDto
        where CompanyUserItemDataType : M_CompanyUser
        where NippouTollOtherItemDataType : T_Nippou_Toll_Other
        where AnkenPointItemDataType : T_Anken_Point
        where NippouTollItemDataType : T_Nippou_Toll
        where KojinUnsyuKubunItemDataType : M_KojinUnsyu_Kubun
        where UriageUnsyuItemDataType : T_Uriage_Unsyu
        where UriageFutanItemDataType : T_Uriage_Futan
        where UriageUnchinItemDataType : T_Uriage_Unchin
        where UriageShitabaraiItemDataType : T_Uriage_Shitabarai
        where UriageDataType : T_Uriage
        where BaggageGroupDataType : BaggageGroupDto
    {
        /// <summary>
        /// 請求区分
        /// </summary>
        public List<CodeDataItemDataType> SeikyuKubun { get; set; }

        /// <summary>
        /// 課税区分
        /// </summary>
        public List<CodeDataItemDataType> KazeiKubun { get; set; }

        /// <summary>
        /// 前払金超過区分
        /// </summary>
        public List<CodeDataItemDataType> AdvanceOverpaymentKubun { get; set; }

        /// <summary>
        /// 払金超過区分
        /// </summary>
        public List<CodeDataItemDataType> OverpaymentKubun { get; set; }

        /// <summary>
        /// 販売区分
        /// </summary>
        public List<CodeDataItemDataType> SalesKubun { get; set; }

        /// <summary>
        /// 通行料金区分
        /// </summary>
        public List<CodeDataItemDataType> TollKubun { get; set; }

        /// <summary>
        /// 営業部門
        /// </summary>
        public List<CodeDataItemDataType> SalesBusinessSegment { get; set; }

        /// <summary>
        /// ユーザグループ
        /// </summary>
        public List<CodeDataItemDataType> UserGroup { get; set; }

        /// <summary>
        /// 有料ユーザー
        /// </summary>
        public List<CompanyUserItemDataType> PaidUser { get; set; }

        /// <summary>
        /// 荷物グループDTO
        /// </summary>
        public List<BaggageGroupDataType> BaggageGroupDto { get; set; }

        /// <summary>
        /// 通行料金DTO
        /// </summary>
        public List<AddTollDto> AddTollDto { get; set; }

        /// <summary>
        /// 得意先DTO
        /// </summary>
        public List<AddCustomerDto> AddCustomerDto { get; set; }

        /// <summary>
        /// 得意先DTO（下払）
        /// </summary>
        public List<AddCustomerDto> AddCustomerShitabaraiDto { get; set; }

        /// <summary>
        /// 乗務員DTO
        /// </summary>
        public List<AddDriverDto> AddDriverDto { get; set; }

        /// <summary>
        /// 得意先担当DTO
        /// </summary>
        public AddCustomerTantouDto AddCustomerTantouDto { get; set; }

        /// <summary>
        /// ユニット
        /// </summary>
        public List<CodeDataItemDataType> Unit { get; set; }

        /// <summary>
        /// 案件ポイント
        /// </summary>
        public List<AnkenPointItemDataType> AnkenPoint { get; set; }

        /// <summary>
        /// 運賃売上
        /// </summary>
        public List<UriageUnchinItemDataType> UriageUnchin { get; set; }

        /// <summary>
        /// 運売上
        /// </summary>
        public List<UriageUnsyuItemDataType> UriageUnsyu { get; set; }

        /// <summary>
        /// 負担売上
        /// </summary>
        public List<UriageFutanItemDataType> UriageFutan { get; set; }

        /// <summary>
        /// 負担売上（追加用）
        /// 負担売上が未登録の場合は、以下を表示し⇒負担売上に登録する
        /// ・通行料金日報(SalesData.NippouToll)から生成された会社負担
        /// ・他通行料金日報(Model.NippouTollOther)から生成された会社負担
        /// </summary>
        public List<UriageFutanItemDataType> UriageFutanAdd { get; set; }

        /// <summary>
        /// 下払い売上
        /// </summary>
        public List<UriageShitabaraiItemDataType> UriageShitabarai { get; set; }

        /// <summary>
        /// 売上
        /// </summary>
        public UriageDataType Uriage { get; set; }

        /// <summary>
        /// 専属
        /// </summary>
        public M_Senzoku Senzoku { get; set; }

        /// <summary>
        /// 専属乗務員
        /// </summary>
        public M_Senzoku_Driver SenzokuDriver { get; set; }

        /// <summary>
        /// 回送日報
        /// </summary>
        public T_Nippou_Kaiso NippouKaiso { get; set; }

        /// <summary>
        /// 泊まり日報
        /// </summary>
        public T_Nippou_Stay NippouStay { get; set; }

        /// <summary>
        /// デジタコ泊まり日報
        /// </summary>
        public T_Nippou_Stay_Degitako NippouStayDegitako { get; set; }

        /// <summary>
        /// デジタコ回送日報
        /// </summary>
        public T_Nippou_Kaiso_Degitako NippouKaisoDegitako { get; set; }

        /// <summary>
        /// 傭車
        /// </summary>
        public M_Yosya_Branch Yosya { get; set; }

        /// <summary>
        /// 他通行料金日報
        /// </summary>
        public List<NippouTollOtherItemDataType> NippouTollOther { get; set; }
        
        /// <summary>
        /// 個人運収区分
        /// </summary>
        public List<KojinUnsyuKubunItemDataType> KojinUnsyuKubun { get; set; }
        
        /// <summary>
        /// 個人運収ルート
        /// </summary>
        public List<M_KojinUnsyu_Route> KojinUnsyuRoute { get; set; }

        /// <summary>
        /// 通行料金日報
        /// </summary>
        public List<NippouTollItemDataType> NippouToll { get; set; }

        /// <summary>
        /// 日報
        /// </summary>
        public T_Nippou Nippou { get; set; }

        /// <summary>
        /// 得意先請求
        /// </summary>
        public M_Customer_Uriage_Calc CustomerSeikyu { get; set; }

        /// <summary>
        /// 得意先
        /// </summary>
        public M_Customer_Branch Customer { get; set; }

        /// <summary>
        /// 案件詳細
        /// </summary>
        public T_Anken_Detail AnkenDetail { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 専属車輌マスタ
        /// </summary>
        public string MasterSenzokuSyaryo { get; set; }

        /// <summary>
        /// 専属乗務員マスタ
        /// </summary>
        public string MasterSenzokuDriver { get; set; }

        /// <summary>
        /// 専属傭車マスタ
        /// </summary>
        public string MasterSenzokuYosya { get; set; }

        /// <summary>
        /// 請求先
        /// </summary>
        public string CustomerSeikyusaki { get; set; }
        public M_Customer_Branch Seikyu { get; set; }
        /// <summary>
        /// IsSeikyuCommitted
        /// 赤黒伝票（請求）判定
        /// </summary>
        public bool IsSeikyuCommitted { get; set; }
        /// <summary>
        /// IsShitabaraiCommitted
        /// 赤黒伝票（下払）判定
        /// </summary>
        public bool IsShitabaraiCommitted { get; set; }

        /// <summary>
        /// 請求明細（売上運賃の請求済情報）
        /// </summary>
        public List<T_Seikyu_Detail> SeikyuDetail { get; set; }

        /// <summary>
        /// 売上運賃（コミット）
        /// </summary>
        public List<T_Commit_Seikyu> CommitSeikyu { get; set; }
        
        /// <summary>
        /// 売上下払（コミット）
        /// </summary>
        public List<T_Commit_Shitabarai> CommitShitabarai { get; set; }
        
        /// <summary>
        /// 売上運収（コミット）
        /// </summary>
        public List<T_Commit_Unsyu> CommitUnsyu { get; set; }

    }

    /// <summary>
    /// 荷物グループDTO
    /// </summary>
    public class BaggageGroupDto
    {
        /// <summary>
        /// アイテム
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 番号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// ユニット
        /// </summary>
        public string Unit { get; set; }
    }

    /// <summary>
    /// 通行料金DTO
    /// </summary>
    public class AddTollDto
    {
        /// <summary>
        /// アイテム
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 番号
        /// </summary>
        public decimal Number { get; set; }
    }

    /// <summary>
    /// 得意先DTO
    /// </summary>
    public class AddCustomerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// コード
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 得意先ID
        /// </summary>
        public int CustomerId { get; set; }
    }

    /// <summary>
    /// 乗務員
    /// </summary>
    public class AddDriverDto
    {
        /// <summary>
        /// 乗務員 ID
        /// </summary>
        public int Driver_ID { get; set; }

        /// <summary>
        /// 乗務員 CD
        /// </summary>
        public int? Employee_Number { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string Display_Name { get; set; }

        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban_Number { get; set; }
    }

    /// <summary>
    /// 得意先担当
    /// </summary>
    public class AddCustomerTantouDto
    {
        /// <summary>
        /// コード
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 専属モデル
    /// </summary>
    public class SenzokuModel
    {
        /// <summary>
        /// 専属
        /// </summary>
        public M_Senzoku Senzoku { get; set; }

        /// <summary>
        /// 専属乗務員
        /// </summary>
        public M_Senzoku_Driver SenzokuDriver { get; set; }

        /// <summary>
        /// 得意先
        /// </summary>
        public M_Customer_Branch Customer { get; set; }

        /// <summary>
        /// 専属データ一覧
        /// </summary>
        public List<SenzokuData> SenzokuDataList { get; set; }

        /// <summary>
        /// 売上運収入合計額
        /// </summary>
        public List<TotalUriageUnsyu> TotalUriageUnsyuList { get; set; }

        /// <summary>
        /// 売上下払い合計額
        /// </summary>
        public List<TotalUriageShitabarai> TotalUriageShitabaraiList { get; set; }

        /// <summary>
        /// 売上負担合計額
        /// </summary>
        public List<TotalUriageFutan> TotalUriageFutanList { get; set; }

        /// <summary>
        /// 得意先売上計算データ
        /// </summary>
        public M_Customer_Uriage_Calc CustomerUriageCalcData { get; set; }
        
        /// <summary>
        /// 請求運賃合計額
        /// </summary>
        public decimal TotalSeikyuUnchin { get; set; }

        /// <summary>
        /// 立替金合計額
        /// </summary>
        public decimal TotalTatekaekin { get; set; }

        /// <summary>
        /// 個人負担合計額
        /// </summary>
        public decimal TotalKojinFutan { get; set; }

        /// <summary>
        /// 割増1合計額
        /// </summary>
        public decimal TotalWarimashi1 { get; set; }

        /// <summary>
        /// 割増2合計額
        /// </summary>
        public decimal TotalWarimashi2 { get; set; }

        /// <summary>
        /// 割増3合計額
        /// </summary>
        public decimal TotalWarimashi3 { get; set; }

        /// <summary>
        /// 割増4合計額
        /// </summary>
        public decimal TotalWarimashi4 { get; set; }

        /// <summary>
        /// 割増5合計額
        /// </summary>
        public decimal TotalWarimashi5 { get; set; }

        /// <summary>
        /// 税区分合計
        /// </summary>
        public int TotalZeiKubun { get; set; }

        /// <summary>
        /// 請求合計額
        /// </summary>
        public decimal TotalSeikyuTotal { get; set; }

        /// <summary>
        /// 負担額
        /// </summary>
        public decimal TotalFutanPrice { get; set; }

        /// <summary>
        /// 稼働日数
        /// </summary>
        public int OperationDateCount { get; set; }

        /// <summary>
        /// 専属車輌マスタ
        /// </summary>
        public string MasterSenzokuSyaryo { get; set; }

        /// <summary>
        /// 専属乗務員マスタ
        /// </summary>
        public string MasterSenzokuDriver { get; set; }

        /// <summary>
        /// 専属傭車マスタ
        /// </summary>
        public string MasterSenzokuYosya { get; set; }

    }

    /// <summary>
    /// 専属データ
    /// </summary>
    public class SenzokuData
    {
        /// <summary>
        /// 売上
        /// </summary>
        public T_Uriage Uriage { get; set; }

        /// <summary>
        /// 売上運賃
        /// </summary>
        public T_Uriage_Unchin UriageUnchin { get; set; }

        /// <summary>
        /// 運収売上
        /// </summary>
        public T_Uriage_Unsyu UriageUnsyu { get; set; }

        /// <summary>
        /// 負担売上
        /// </summary>
        public T_Uriage_Futan UriageFutan { get; set; }

        /// <summary>
        /// 下払い売上
        /// </summary>
        public T_Uriage_Shitabarai UriageShitabarai { get; set; }

        /// <summary>
        /// 案件データ
        /// </summary>
        public V_AnkenDataList AnkenData { get; set; }

        /// <summary>
        /// 乗務員
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public int? Kubun { get; set; }

        /// <summary>
        /// 配車日
        /// </summary>
        public DateOnly? Haisya_Date { get; set; }

        /// <summary>
        /// 日報 ID
        /// </summary>
        public int? Nippou_ID { get; set; }

        public int? Driver_Name { get; set; }
    }

    /// <summary>
    /// 運収売上合計額
    /// </summary>
    public class TotalUriageUnsyu
    {
        public int UriageId { get; set; }
        /// <summary>
        /// 乗務員番号
        /// </summary>
        public int? Employee_Number { get; set; }

        /// <summary>
        /// 車番
        /// </summary>
        public string Syaban_Number { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string Display_Name { get; set; }

        /// <summary>
        /// 運収区分
        /// </summary>
        public string Unsyu_Kubun { get; set; }

        /// <summary>
        /// 日カウント
        /// </summary>
        public decimal Total_Date_Count { get; set; }

        /// <summary>
        /// 生産
        /// </summary>
        public decimal Seisan { get; set; }

        /// <summary>
        /// 個人負担
        /// </summary>
        public decimal KojinFutan { get; set; }

        /// <summary>
        /// 個人運収
        /// </summary>
        public decimal KojinUnsyu { get; set; }

        /// <summary>
        /// 手当てルート	
        /// </summary>
        public decimal Route_Teate { get; set; }

        /// <summary>
        /// 残業ルート
        /// </summary>
        public decimal Route_OverTime { get; set; }

        /// <summary>
        /// 夜中ルート
        /// </summary>
        public decimal Route_Midnight { get; set; }
    }

    /// <summary>
    /// 下払い売上合計額
    /// </summary>
    public class TotalUriageShitabarai
    {
        /// <summary>
        /// 傭車コード
        /// </summary>
        public string Yosya_Code { get; set; }

        /// <summary>
        /// 傭車先
        /// </summary>
        public string Yosya_Name_Abbr { get; set; }

        /// <summary>
        /// 下払い価格
        /// </summary>
        public decimal ShiharaiPrice { get; set; }

        /// <summary>
        /// 割増価格
        /// </summary>
        public decimal WarimashiPrice { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public decimal Tatekaekin { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int Zei_Kubun { get; set; }

        /// <summary>
        /// 下払い合計額
        /// </summary>
        public decimal Shitabarai_Total { get; set; }
    }

    /// <summary>
    /// 売上負担合計額
    /// </summary>
    public class TotalUriageFutan
    {
        /// <summary>
        /// 負担区分
        /// </summary>
        public int futan_Kubun { get; set; }

        /// <summary>
        /// 負担価格
        /// </summary>
        public decimal FutanPrice { get; set; }
    }

    /// <summary>
    /// 売上モデル
    /// </summary>
    public class PostUriageDataModel
    {
        /// <summary>
        /// 売上一覧
        /// </summary>
        public List<T_Uriage> UriageList { get; set; }

        /// <summary>
        /// 売上運賃一覧
        /// </summary>
        public List<T_Uriage_Unchin> UriageUnchinList { get; set; }
    }

}