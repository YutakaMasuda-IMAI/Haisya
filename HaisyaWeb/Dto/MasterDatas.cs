using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 案件交換ローカルクラス
    /// </summary>
    public partial class M_Anken_Excharge_Local : WebApplication.Data.M_Anken_Excharge
    {

    }

    /// <summary>
    /// コードローカルクラス
    /// </summary>
    public partial class M_Code_Local : WebApplication.Data.M_Code
    {

    }

    /// <summary>
    /// コードデータローカルクラス
    /// </summary>
    public partial class M_Code_Data_Local : WebApplication.Data.M_Code_Datum
    {

    }

    /// <summary>
    /// ユニットローカルクラス
    /// </summary>
    public partial class M_Unit_Local : WebApplication.Data.M_Unit
    {
    }

    /// <summary>
    /// 会社ローカルクラス
    /// </summary>
    public partial class M_Company_Local : WebApplication.Data.M_Company
    {

    }

    /// <summary>
    /// 会社支店ローカルクラス
    /// </summary>
    public partial class M_CompanyBranch_Local : WebApplication.Data.M_CompanyBranch { }

    /// <summary>
    /// 会社ユーザーローカルクラス
    /// </summary>
    public partial class M_CompanyUser_Local : WebApplication.Data.M_CompanyUser { }

    /// <summary>
    /// コードデータDTOローカルクラス
    /// </summary>
    public partial class CodeDataDto_Local : WebApplication.Model.CodeDataDto { }

    /// <summary>
    /// 日報通行料その他ローカルクラス
    /// </summary>
    public partial class T_Nippou_Toll_Other_Local : WebApplication.Data.T_Nippou_Toll_Other { }

    /// <summary>
    /// 個人運輸区分ローカルクラス
    /// </summary>
    public partial class M_KojinUnsyu_Kubun_Local : WebApplication.Data.M_KojinUnsyu_Kubun { }

    /// <summary>
    /// 売上運輸ローカルクラス
    /// </summary>
    public partial class T_Uriage_Unsyu_Local : WebApplication.Data.T_Uriage_Unsyu 
    {
        /// <summary>
        /// 赤伝黒伝伝票判定
        /// </summary>
        public bool IsCreditSlip { get; set; } = false;

        /// <summary>
        /// 赤伝黒伝伝票締日
        /// </summary>
        public DateTime Shime_Datetime { get; set; }
    }

    /// <summary>
    /// 経費ローカルクラス
    /// </summary>
    public partial class T_Expense_Local : WebApplication.Data.T_Expense { }

    /// <summary>
    /// 経費支払いローカルクラス
    /// </summary>
    public partial class T_Expense_Payment_Local : WebApplication.Data.T_Expense_Payment { }

    /// <summary>
    /// コードデータローカルクラス
    /// </summary>
    public partial class M_Code_Datum_Local : WebApplication.Data.M_Code_Datum { }

    /// <summary>
    /// 会社ドライバーローカルクラス
    /// </summary>
    public partial class M_CompanyDriver_Local : WebApplication.Data.M_CompanyDriver
    {

    }

    /// <summary>
    /// 会社ドライバービューローカルクラス
    /// </summary>
    public partial class V_CompanyDriver_Local : WebApplication.Data.V_CompanyDriver
    {

    }

    /// <summary>
    /// 会社ドライバー車両ローカルクラス
    /// </summary>
    public partial class M_CompanyDriver_Syaryo_Local : WebApplication.Data.M_CompanyDriver_Syaryo
    {

    }

    /// <summary>
    /// 会社ユーザーグループローカルクラス
    /// </summary>
    public partial class M_CompanyUser_Group_Local : WebApplication.Data.M_CompanyUser_Group
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public M_CompanyUser_Group_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">M_CompanyUser_Group_Localオブジェクト</param>
        public M_CompanyUser_Group_Local(Dto.M_CompanyUser_Group_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.M_CompanyUser_Group_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 会社ユーザーグループユーザーローカルクラス
    /// </summary>
    public partial class M_CompanyUser_GroupUser_Local : WebApplication.Data.M_CompanyUser_GroupUser
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public M_CompanyUser_GroupUser_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">M_CompanyUser_GroupUser_Localオブジェクト</param>
        public M_CompanyUser_GroupUser_Local(Dto.M_CompanyUser_GroupUser_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.M_CompanyUser_GroupUser_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 顧客ローカルクラス
    /// </summary>
    public partial class M_Customer_Local : WebApplication.Data.M_Customer
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public M_Customer_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">M_Customer_Localオブジェクト</param>
        public M_Customer_Local(Dto.M_Customer_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.M_Customer_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 顧客支店ローカルクラス
    /// </summary>
    public partial class M_Customer_Branch_Local : WebApplication.Data.M_Customer_Branch
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public M_Customer_Branch_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">M_Customer_Localオブジェクト</param>
        public M_Customer_Branch_Local(Dto.M_Customer_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.M_Customer_Branch_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 顧客担当ローカルクラス
    /// </summary>
    public partial class M_Customer_Tantou_Local : WebApplication.Data.M_Customer_Tantou
    {

    }

    /// <summary>
    /// 顧客担当配車グループローカルクラス
    /// </summary>
    public partial class M_Customer_TantouHaisyaGroup_Local : WebApplication.Data.M_Customer_TantouHaisyaGroup { }

    /// <summary>
    /// 顧客ドライバーローカルクラス
    /// </summary>
    public partial class M_Customer_Driver_Local : WebApplication.Data.M_Customer_Driver { }

    /// <summary>
    /// 顧客車両ビューローカルクラス
    /// </summary>
    public partial class V_Customer_Syaryo_Local : WebApplication.Data.V_Customer_Syaryo { }

    /// <summary>
    /// 顧客ドライバー車両ローカルクラス
    /// </summary>
    public partial class M_Customer_Driver_Syaryo_Local : WebApplication.Data.M_Customer_Driver_Syaryo { }

    /// <summary>
    /// 顧客IC請求区分ローカルクラス
    /// </summary>
    public partial class M_Customer_ICSeikyuKubun_Local : WebApplication.Data.M_Customer_ICSeikyuKubun { }

    /// <summary>
    /// 顧客売上計算ローカルクラス
    /// </summary>
    public partial class M_Customer_Uriage_Calc_Local : WebApplication.Data.M_Customer_Uriage_Calc { }

    /// <summary>
    /// 顧客支払い計算ローカルクラス
    /// </summary>
    public partial class M_Customer_Shiharai_Calc_Local : WebApplication.Data.M_Customer_Shiharai_Calc { }

    /// <summary>
    /// 顧客通行料請求区分ローカルクラス
    /// </summary>
    public partial class M_Customer_TollSeikyuKubun_Local : WebApplication.Data.M_Customer_TollSeikyuKubun { }

    /// <summary>
    /// ログインユーザーロールローカルクラス
    /// </summary>
    public partial class M_LoginUser_Role_Local : WebApplication.Data.M_LoginUser_Role
    {

    }

    /// <summary>
    /// ログインユーザーローカルクラス
    /// </summary>
    public partial class M_LoginUser_Local : WebApplication.Data.M_LoginUser
    {

    }

    /// <summary>
    /// 車種区分ローカルクラス
    /// </summary>
    public partial class M_SyasyuKubun_Local : WebApplication.Data.M_SyasyuKubun
    {

    }

    /// <summary>
    /// 型ローカルクラス
    /// </summary>
    public partial class M_Kata_Local : WebApplication.Data.M_Katum
    {

    }

    /// <summary>
    /// 車両サイズローカルクラス
    /// </summary>
    public partial class M_SyaryoSize_Local : WebApplication.Data.M_SyaryoSize
    {

    }

    /// <summary>
    /// 車両ローカルクラス
    /// </summary>
    public partial class M_Syaryo_Local : WebApplication.Data.M_Syaryo
    {

    }

    /// <summary>
    /// 車両管理ローカルクラス
    /// </summary>
    public partial class M_SyaryoManagement_Local : WebApplication.Data.M_SyaryoManagement
    {

    }

    /// <summary>
    /// 車両コストローカルクラス
    /// </summary>
    public partial class M_SyaryoCost_Local : WebApplication.Data.M_SyaryoCost
    {

    }

    /// <summary>
    /// デフォルト金額ローカルクラス
    /// </summary>
    public partial class M_DefaultMoney_Local : WebApplication.Data.M_DefaultMoney
    {

    }

    /// <summary>
    /// エリア待機時間デフォルト金額ローカルクラス
    /// </summary>
    public partial class M_DefaultMoney_WaitTimeForArea_Local : WebApplication.Data.M_DefaultMoney_WaitTimeForArea
    {

    }

    /// <summary>
    /// 会社待機時間デフォルト金額ローカルクラス
    /// </summary>
    public partial class M_DefaultMoney_WaitTimeForCompany_Local : WebApplication.Data.M_DefaultMoney_WaitTimeForCompany
    {

    }

    /// <summary>
    /// 人件費ローカルクラス
    /// </summary>
    public partial class M_PersonnelExpense_Local : WebApplication.Data.M_PersonnelExpense
    {

    }

    /// <summary>
    /// 燃料費ローカルクラス
    /// </summary>
    public partial class M_FuelCost_Local : WebApplication.Data.M_FuelCost
    {

    }

    /// <summary>
    /// 郵便番号ローカルクラス
    /// </summary>
    public partial class M_PostCode_Local : WebApplication.Data.M_PostCode
    {

    }

    /// <summary>
    /// ロールローカルクラス
    /// </summary>
    public partial class M_Role_Local : WebApplication.Data.M_Role
    {

    }

    /// <summary>
    /// 荷物ビューローカルクラス
    /// </summary>
    public partial class V_Luggage_Local : WebApplication.Data.V_Luggage
    {

    }

    /// <summary>
    /// 荷物ローカルクラス
    /// </summary>
    public partial class M_Luggage_Local : WebApplication.Data.M_Luggage
    {

    }

    /// <summary>
    /// 荷物グループローカルクラス
    /// </summary>
    public partial class M_Luggage_Group_Local : WebApplication.Data.M_Luggage_Group
    {

    }

    /// <summary>
    /// 負担ローカルクラス
    /// </summary>
    public partial class M_Burden_Local : WebApplication.Data.M_Burden
    {

    }

    /// <summary>
    /// 売上負担ローカルクラス
    /// </summary>
    public partial class T_Uriage_Futan_Local : WebApplication.Data.T_Uriage_Futan
    {

    }

    /// <summary>
    /// 負担グループローカルクラス
    /// </summary>
    public partial class M_Burden_Group_Local : WebApplication.Data.M_Burden_Group
    {

    }

    /// <summary>
    /// 設備ローカルクラス
    /// </summary>
    public partial class M_Equipment_Local : WebApplication.Data.M_Equipment
    {

    }

    /// <summary>
    /// 設備グループローカルクラス
    /// </summary>
    public partial class M_Equipment_Group_Local : WebApplication.Data.M_Equipment_Group
    {

    }

    /// <summary>
    /// ログインユーザービューローカルクラス
    /// </summary>
    public partial class V_LoginUser_Local : WebApplication.Data.V_LoginUser
    {

    }

    /// <summary>
    /// 公開グループローカルクラス
    /// </summary>
    public partial class M_PublishGroup_Local : WebApplication.Data.M_PublishGroup
    {

    }

    /// <summary>
    /// マスターデータ共通結果値DTOローカルクラス
    /// </summary>
    public partial class MsterDataCommonResultValDto_Local : WebApplication.Dto.MsterDataCommonResultValDto
    {

    }

    /// <summary>
    /// 経費項目ローカルクラス
    /// </summary>
    public partial class T_Expense_Item_Local : WebApplication.Data.T_Expense_Item { }

    /// <summary>
    /// 案件交換ローカルクラス
    /// </summary>
    public partial class M_Anken_Excharge_Local : WebApplication.Data.M_Anken_Excharge
    {

    }

    /// <summary>
    /// 専属ビューローカルクラス
    /// </summary>
    public partial class V_Senzoku_Local : WebApplication.Data.V_Senzoku
    {

    }

    /// <summary>
    /// 専属ローカルクラス
    /// </summary>
    public partial class M_Senzoku_Local : WebApplication.Data.M_Senzoku
    {

    }

    /// <summary>
    /// 専属ドライバーローカルクラス
    /// </summary>
    public partial class M_Senzoku_Driver_Local : WebApplication.Data.M_Senzoku_Driver
    {

    }

    /// <summary>
    /// 専属ドライバービューローカルクラス
    /// </summary>
    public partial class V_Senzoku_Driver_Local : WebApplication.Data.V_Senzoku_Driver
    {

    }

    /// <summary>
    /// ベンダーローカルクラス
    /// </summary>
    public partial class M_Vender_Local : WebApplication.Data.M_Vender
    {

    }

    /// <summary>
    /// レポート検索ローカルクラス
    /// </summary>
    public partial class M_Report_Serch_Local : WebApplication.Data.M_Report_Serch
    {

    }

    /// <summary>
    /// レポート検索区分ローカルクラス
    /// </summary>
    public partial class M_Report_Serch_Kubun_Local : WebApplication.Data.M_Report_Serch_Kubun
    {

    }

    /// <summary>
    /// レポート出力項目マスターローカルクラス
    /// </summary>
    public partial class M_Report_Output_Item_Master_Local : WebApplication.Data.M_Report_Output_Item_Master
    {

    }

    /// <summary>
    /// レポート出力項目ローカルクラス
    /// </summary>
    public partial class M_Report_Output_Item_Local : WebApplication.Data.M_Report_Output_Item
    {

    }


    /// <summary>
    /// M_Report_Detail_Param_Localクラスは帳票詳細パラメタのdto
    /// </summary>
    public partial class M_Report_Detail_Param_Local : WebApplication.Data.M_Report_Detail_Param
    {
    }

    /// <summary>
    /// レポート出力項目作成クラス
    /// </summary>
    public partial class CreateReportOutputItem
    {
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public List<M_Report_Output_Item_Local> ReportOutputItems { get; set; }
    }

    /// <summary>
    /// レポート検索データクラス
    /// </summary>
    public partial class ReportSearchData
    {
        public string ReportName { get; set; }
        public string DisplayTitle { get; set; }
    }

    /// <summary>
    /// エリアローカルクラス
    /// </summary>
    public partial class M_Area_Local : WebApplication.Data.M_Area
    {

    }

    /// <summary>
    /// エリア県ローカルクラス
    /// </summary>
    public partial class M_Area_Ken_Local : WebApplication.Data.M_Area_Ken
    {

    }

    /// <summary>
    /// ベンダーローカルクラス
    /// </summary>
    public partial class M_Vender_Local : WebApplication.Data.M_Vender
    {

    }

    /// <summary>
    /// 傭車ローカルクラス
    /// </summary>
    public partial class M_Yosya_Local : WebApplication.Data.M_Yosya
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public M_Yosya_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">M_Yosya_Localオブジェクト</param>
        public M_Yosya_Local(Dto.M_Yosya_Local list)
        {
            ////親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(Dto.M_Yosya_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 傭車支店ローカルクラス
    /// </summary>
    public partial class M_Yosya_Branch_Local : WebApplication.Data.M_Yosya_Branch { }

    /// <summary>
    /// 支払チェックローカルクラス
    /// </summary>
    public partial class T_Check_Shitabarai_Local : WebApplication.Data.T_Check_Shitabarai { }

    /// <summary>
    /// 支払明細印刷ローカルクラス
    /// </summary>
    public partial class T_Print_Shitabarai_Detail_Local : WebApplication.Data.T_Print_Shitabarai_Detail
    {
        public String UnitData { get; set; }
    }

    /// <summary>
    /// 支払印刷ローカルクラス
    /// </summary>
    public partial class T_Print_Shitabarai_Local : WebApplication.Data.T_Print_Shitabarai { }

    /// <summary>
    /// 傭車支払ローカルクラス
    /// </summary>
    public partial class T_YosyaShiharai_Local : WebApplication.Data.T_YosyaShiharai { }

    /// <summary>
    /// 支払チェック変更ローカルクラス
    /// </summary>
    public partial class T_Check_Shitabarai_Change_Local : WebApplication.Data.T_Check_Shitabarai_Change
    {
        public String UnitData { get; set; }
    }

    /// <summary>
    /// 売上支払ローカルクラス
    /// </summary>
    public partial class T_Uriage_Shitabarai_Local : WebApplication.Data.T_Uriage_Shitabarai 
    {
        /// <summary>
        /// 赤伝黒伝伝票判定
        /// </summary>
        public bool IsCreditSlip { get; set; } = false;

        /// <summary>
        /// 赤伝黒伝伝票締日
        /// </summary>
        public DateTime Shime_Datetime { get; set; }
    }

    /// <summary>
    /// 支払チェック明細ローカルクラス
    /// </summary>
    public partial class T_Check_Shitabarai_Detail_Local : WebApplication.Data.T_Check_Shitabarai_Detail { }

    /// <summary>
    /// 支払明細ローカルクラス
    /// </summary>
    public partial class T_Shitabarai_Detail_Local : WebApplication.Data.T_Shitabarai_Detail { }

    /// <summary>
    /// 支払ローカルクラス
    /// </summary>
    public partial class T_Shitabarai_Local : WebApplication.Data.T_Shitabarai { }

    /// <summary>
    /// ユニットローカルクラス
    /// </summary>
    public partial class M_Unit_Local : WebApplication.Data.M_Unit
    {

    }

    /// <summary>
    /// 支払バッチ登録モデルローカルクラス
    /// </summary>
    public partial class ShitabaraiBatchRegistrationModel_Local : WebApplication.Model.ShitabaraiBatchRegistrationModel
    {

    }

    /// <summary>
    /// 支払モーダル承認モデルローカルクラス
    /// </summary>
    public partial class ShitabaraiModalApprovalModel_Local : WebApplication.Model.ShitabaraiModalApprovalModel
    {

    }

    /// <summary>
    /// 支払チェック完了ローカルクラス
    /// </summary>
    public partial class T_Check_Shitabarai_Done_Local : WebApplication.Data.T_Check_Shitabarai_Done
    {

    }

    /// <summary>
    /// 支払承認ステータスモデルローカルクラス
    /// </summary>
    public partial class ShitabaraiApprovalStatusModel_Local : WebApplication.Model.ShitabaraiApprovalStatusModel
    {

    }

    /// <summary>
    /// 事故ローカルクラス
    /// </summary>
    public partial class T_Jiko_Local : WebApplication.Data.T_Jiko
    {

    }
}
