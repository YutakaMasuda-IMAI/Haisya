using System.Collections.Generic;
using WebApplication.Data;

namespace WebApplication.Dto
{
    /// <summary>
    /// マスタデータDTO
    /// </summary>
    public class MasterDataDto
    {

    }

    /// <summary>
    /// 住所エリア
    /// </summary>
    public enum AddressArea
    {
        Hokkaido = 1,
        Tohoku = 2,
        Hokuriku = 3,
        Chubu = 4,
        Kanto = 5,
        Kinki = 6,
        Chugoku = 7,
        Shikoku = 8,
        Kyusyu = 9,
        Okinawa = 10,
    }

    /// <summary>
    /// マスタデータ共通結果値DTO
    /// </summary>
    public class MsterDataCommonResultValDto
    {
        /// <summary>
        /// 戻りフラグ
        /// </summary>
        public bool RetrunFlg { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrrMessage { get; set; }

        /// <summary>
        /// 事故ID
        /// </summary>
        public int Jiko_ID { set; get; }
    }

    /// <summary>
    /// 車種区分リストDTO
    /// </summary>
    public class SyasyuKubunListDto
    {
        /// <summary>
        /// 車種区分リスト
        /// </summary>
        public List<Data.M_SyasyuKubun> SyasyuKubunList { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 車両リストDTO
    /// </summary>
    public class SaryoListDto
    {
        /// <summary>
        /// 車両リスト
        /// </summary>
        public List<Data.M_Syaryo> M_SyaryoList { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 車両サイズリストDTO
    /// </summary>
    public class SaryoSizeListDto
    {
        /// <summary>
        /// 車両サイズリスト
        /// </summary>
        public List<Data.M_SyaryoSize> M_SyaryoSizeList { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 案件交換リストDTO
    /// </summary>
    public class AnkenExchargeListDto
    {
        /// <summary>
        /// 案件交換リスト
        /// </summary>
        public List<Data.M_Anken_Excharge> M_AnkenExchargeList { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 会社ユーザーグループDTO
    /// </summary>
    public class CompanyUserGroupDto
    {
        /// <summary>
        /// 会社ユーザーグループリスト
        /// </summary>
        public List<Data.M_CompanyUser_Group> CompanyUserGroupList { set; get; }

        /// <summary>
        /// 会社ユーザーグループデータ
        /// </summary>
        public Data.M_CompanyUser_Group CompanyUserGroupData { set; get; }

        /// <summary>
        /// 会社ユーザーグループユーザーリスト
        /// </summary>
        public List<Data.M_CompanyUser_GroupUser> CompanyUserGroupUserList { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 傭車モーダルDTO
    /// </summary>
    public class YosyaModalDto
    {
        /// <summary>
        /// 顧客運転手データ
        /// </summary>
        public Data.M_Customer_Driver CustomerDriverData { set; get; }

        /// <summary>
        /// 顧客運転手車両データ
        /// </summary>
        public Data.M_Customer_Driver_Syaryo CustomerDriverSyaryoData { set; get; }

        /// <summary>
        /// 顧客データ担当者リスト
        /// </summary>
        public List<Data.M_Customer_Tantou> CustomerDataTantouList { set; get; }

        /// <summary>
        /// 顧客データ担当者配車グループリスト
        /// </summary>
        public List<Data.M_Customer_TantouHaisyaGroup> CustomerDataTantouHaisyaGroupList { set; get; }

        /// <summary>
        /// 顧客運転手車両ID
        /// </summary>
        public int CustomerDriverSyaryoID { set; get; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 顧客モーダルDTO
    /// </summary>
    public class CustomerModalDto
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// 顧客データ
        /// </summary>
        public Data.M_Customer CustomerData { set; get; }

        /// <summary>
        /// 顧客支店リスト
        /// </summary>
        public List<Data.M_Customer_Branch> CustomerBranchList { set; get; }
    }

    /// <summary>
    /// 帳票検索データDTO
    /// </summary>
    public class ReportSearchDataDto
    {
        /// <summary>
        /// 帳票名
        /// </summary>
        public string ReportName { set; get; }

        /// <summary>
        /// 表示タイトル
        /// </summary>
        public string DisplayTitle { set; get; }
    }

    /// <summary>
    /// 帳票出力項目作成DTO
    /// </summary>
    public class CreateReportOutputItemDto
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 帳票出力項目リスト
        /// </summary>
        public List<M_Report_Output_Item> ReportOutputItems { get; set; }
    }

    /// <summary>
    /// 顧客支店・担当者マスタ
    /// </summary>
    public class CustomerBranchModalDto
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerID { set; get; }

        /// <summary>
        /// 顧客支店データ
        /// </summary>
        public Data.M_Customer_Branch CustomerBranchData { set; get; }

        /// <summary>
        /// 顧客担当者リスト
        /// </summary>
        public List<Data.M_Customer_Tantou> CustomerTantouList { set; get; }

        /// <summary>
        /// 顧客通行料請求区分リスト
        /// </summary>
        public List<Data.M_Customer_TollSeikyuKubun> CustomerTollSeikyuKubunList { set; get; }

        /// <summary>
        /// 顧客売上計算データ
        /// </summary>
        public Data.M_Customer_Uriage_Calc CustomerUriageCalcData { set; get; }

        /// <summary>
        /// 顧客支払計算データ
        /// </summary>
        public Data.M_Customer_Shiharai_Calc CustomerShiharaiCalcData { set; get; }
    }

    /// <summary>
    /// トラックメイト連携の登録処理
    /// </summary>
    public class CustomerTruckmeteModalDto
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// 顧客データ
        /// </summary>
        public Data.M_Customer CustomerData { set; get; }

        /// <summary>
        /// 顧客支店データ
        /// </summary>
        public Data.M_Customer_Branch CustomerBranchData { set; get; }

        /// <summary>
        /// 顧客担当者リスト
        /// </summary>
        public List<Data.M_Customer_Tantou> CustomerTantouList { set; get; }

        /// <summary>
        /// 親顧客名
        /// </summary>
        public string Customer_Name_Oya { set; get; }
    }

    /// <summary>
    /// 販売業者マスタ
    /// </summary>
    public class VenderModalDto
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyID { set; get; }

        /// <summary>
        /// 販売業者データ
        /// </summary>
        public Data.M_Vender VenderData { set; get; }
    }
}
