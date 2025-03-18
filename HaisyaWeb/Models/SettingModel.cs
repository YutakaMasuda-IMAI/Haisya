using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaisyaWeb.Models
{
    public class SettingModel: CommonModel
    {
        #region 会社情報メニュー
        public class CompanyDto
        {
            public M_Company_Local M_Company { set; get; }

            public List<M_CompanyBranch_Local> M_CompanyBranchList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class CompanyBranchModalDto
        {
            public M_Company_Local M_Company { set; get; }

            public M_CompanyBranch_Local M_CompanyBranch { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }
        }
        #endregion 会社情報メニュー

        #region 従業員メニュー
        public class EmployeeMenuDto
        {
            public int CompanyID { set; get; }

            public List<M_CompanyUser_Local> CompanyUserList { set; get; }

            public List<M_CompanyBranch_Local> M_CompanyBranchList { set; get; }

            public bool EditEnabled { set; get; }
        }

        #region M_CompanyUser
        public class CompanyUserModalDto
        {
            public M_CompanyUser_Local CompanyUser { set; get; }

            public string EmployeeNumber { set; get; }

            public List<M_CompanyBranch_Local> M_CompanyBranchList { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }
        }
        #endregion M_CompanyUser
        #endregion 従業員メニュー

        #region ログインユーザーメニュー
        public class LoginUserMenuDto
        {
            public int CompanyID { set; get; }

            public List<V_LoginUser_Local> LoginUserList { set; get; }

            public List<M_LoginUser_Role_Local> LoginUserRoleList { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class LoginUserRoleDto
        {
            public int CompanyID { set; get; }

            public List<M_LoginUser_Role_Local> LoginUserRoleList { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class LoginUserRoleModalDto
        {
            public M_LoginUser_Role_Local LoginUserRole { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }
        }

        public class LoginUserModalDto
        {
            public M_LoginUser_Local LoginUser { set; get; }

            public List<M_Area_Local> AreaList { set; get; }

            public List<M_CompanyUser_Local> CompanyUserList { set; get; }

            public List<M_LoginUser_Role_Local> LoginUserRoleList { set; get; }

            public IEnumerable<SelectListItem> SyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> KataSelectList { get; set; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }
        }
        #endregion ログインユーザーメニュー

        #region 型マスタ
        public class KataDto
        {
            public List<M_Kata_Local> KataList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class KataModalDto
        {
            public M_Kata_Local KataData { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 型マスタ

        #region 車輌情報
        public class M_SyaryoDto : M_Syaryo_Local
        {
            public string CARDETAILINFO_Disp { set; get; }

            public string TOLL_TYPE_Disp { set; get; }
        }
        
        public class SyaryoDto
        {
            public List<M_SyaryoDto> M_SyaryoList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class SyaryoModalDto
        {
            public M_Syaryo_Local M_Syaryo { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }

            public IEnumerable<SelectListItem> KataSelectList { get; set; }

            public IEnumerable<SelectListItem> TollSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> DetailSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> SyaryoSizeSelectList { get; set; }
        }
        #endregion 車輌情報

        #region 車輌管理
        public class SyaryoManagementDto
        {
            /// <summary>
            /// 検索項目
            /// </summary>
            public CommonModel Search { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectFilter { get; set; }

            public int CompanyID { set; get; }

            public List<M_CompanyDriver_Syaryo_Local> CompanyDriverSyaryoList { set; get; }

            public List<M_CompanyDriver_Local> CompanyDriverList { set; get; }

            public List<M_SyaryoManagement_Local> SyaryoManagementList { set; get; }

            public List<M_Syaryo_Local> SyaryoList { set; get; }

            public List<M_CompanyBranch_Local> CompanyBranchList { set; get; }

            public IEnumerable<SelectListItem> GroupSelectList { get; set; }

            public bool EditEnabled { set; get; }
        }

        public class SyaryoManagementModalDto
        {
            public M_SyaryoManagement_Local SyaryoManagement { set; get; }

            public List<M_CompanyBranch_Local> CompanyBranchList { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }

            public IEnumerable<SelectListItem> GroupList { get; set; }

            public List<SelectListItem> SyaryoList { set; get; }

            public IEnumerable<SelectListItem> TollSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> DetailSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> SyaryoManagementSizeSelectList { get; set; }
        }
        #endregion 車輌管理

        #region 車輌サイズ
        public class SyaryoSizeDto
        {
            public List<M_SyaryoSize_Local> M_SyaryoSizeList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class SyaryoSizeModalDto
        {
            public M_SyaryoSize_Local M_SyaryoSize { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 車輌サイズ

        #region 車輌コスト
        public class SyaryoCostDto
        {
            public List<M_Syaryo_Local> M_SyaryoList { set; get; }

            public List<M_SyaryoCost_Local> M_SyaryoCostList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class SyaryoCostModalDto
        {
            public int CompanyID { set; get; }

            public M_Syaryo_Local M_Syaryo { set; get; }

            public List<M_SyaryoCost_Local> M_SyaryoCost { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 車輌コスト

        #region 標準運賃
        public class DefaultMoneyDto
        {
            public List<M_Area_Local> M_AreaList { set; get; }
            
            public List<M_SyaryoSize_Local> M_SyaryoSizeList { set; get; }

            public List<M_DefaultMoney_Local> M_DefaultMoneyList { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class DefaultMoneyModalDto
        {
            public M_SyaryoSize_Local M_SyaryoSize { set; get; }

            public List<M_DefaultMoney_Local> M_DefaultMoneyList { set; get; }

            public string Area { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }


        public class M_DefaultMoney_WaitTimeForAreaDto : M_DefaultMoney_WaitTimeForArea_Local
        {
            [Column(TypeName = "money")]
            public decimal? CompnyAmount { get; set; }
        }


        public class DefaultMoneyWaitTimeForAreaDto
        {
            public List<M_Area_Local> M_AreaList { set; get; }

            public List<M_SyaryoSize_Local> M_SyaryoSizeList { set; get; }

            public List<M_DefaultMoney_WaitTimeForArea_Local> DefaultMoneyWaitTimeList { set; get; }

            public List<M_DefaultMoney_WaitTimeForCompany_Local> DefaultMoneyWaitTimeForCompanyList { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabledUnyu { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class DefaultMoneyWaitTimeForAreaModalDto
        {
            public M_SyaryoSize_Local M_SyaryoSize { set; get; }

            public List<M_DefaultMoney_WaitTimeForArea_Local> M_DefaultMoneyWaitTimeList { set; get; }

            public string Area { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 標準運賃

        #region 人件費
        public class PersonnelExpensesDto
        {
            public List<M_Syaryo_Local> M_SyaryoList { set; get; }

            public List<M_PersonnelExpense_Local> M_PersonnelExpenseList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 人件費

        #region 燃料費
        public class FuelCostsDto
        {

            public List<M_FuelCost_Local> M_FuelCostList { set; get; }

            public List<SelectListItem> BranchList { get; set; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 燃料費

        #region 案件追加費用
        public class AnkenExchargeDto
        {

            public List<M_Anken_Excharge_Local> M_AnkenExchargeList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class AnkenExchargeModalDto
        {
            public int CompanyID { set; get; }

            public M_Anken_Excharge_Local M_Anken_Excharge { set; get; }

            public IEnumerable<SelectListItem> SyaryoSizeSelectList { get; set; }

            public M_SyaryoSize_Local M_SyaryoSize { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 案件追加費用

        #region 配車グループ
        public class M_CompanyUserGroupDto : M_CompanyUser_Group_Local
        {
        }

        public class CompanyUserGroupDto
        {
            public List<M_CompanyUserGroupDto> CompanyUserGroupList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

            public int GroupKubun { set; get; }

            public string Title { set; get; }
        }

        public class CompanyUserGroupModalDto
        {
            public M_CompanyUser_Group_Local CompanyUserGroupData { set; get; }

            public List<M_CompanyUser_GroupUser_Local> CompanyUserGroupUserList { set; get; }

            public IEnumerable<SelectListItem> CompanyUserList { set; get; }

            public int CompanyID { set; get; }

            public int GroupKubun { set; get; }

            public int GroupID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }

            public IEnumerable<SelectListItem> TollSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> DetailSyasyuSelectList { get; set; }

            public IEnumerable<SelectListItem> CompanyUserGroupSizeSelectList { get; set; }
        }
        #endregion 配車グループ

        #region 車種区分
        public class SyasyuKubunDto
        {
            public List<M_SyasyuKubun_Local> SyasyuKubunList { set; get; }

            public List<M_SyaryoSize_Local> SyaryoSizeList { set; get; }

            public List<Dto.M_Syaryo_Local> SyaryoList { set; get; }

            public List<M_Kata_Local> KataList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

        }

        public class SyasyuKubunModalDto
        {
            public M_SyasyuKubun_Local SyasyuKubunData { set; get; }

            public List<M_SyasyuKubun_Local> SyasyuKubunList { set; get; }

            public IEnumerable<SelectListItem> KataSelectList { set; get; }

            public int CompanyID { set; get; }

            public int SyasyuKubunID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }

        }
        #endregion 車種区分

        #region 案件荷物グループ
        public class AnkenLuggageGroupDto
        {

            public List<M_Luggage_Group_Local> LuggageGroupList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class AnkenLuggageGroupModalDto
        {
            public int CompanyID { set; get; }

            public int GroupID { set; get; }

            public M_Luggage_Group_Local LuggageGroup { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 案件荷物グループ

        #region 案件装備グループ
        public class AnkenEquipmentGroupDto
        {

            public List<M_Equipment_Group_Local> EquipmentGroupList { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class AnkenEquipmentGroupModalDto
        {
            public int CompanyID { set; get; }

            public int GroupID { set; get; }

            public M_Equipment_Group_Local EquipmentGroup { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion 案件装備グループ

        #region 専属
        public class SenzokuListModel
        {
            /// <summary> 企業ID </summary>
            public int CompanyID { set; get; }

            public List<Dto.V_Senzoku_Local> SenzokuList { set; get; }

            public List<Dto.V_Senzoku_Driver_Local> SenzokuDriverList { set; get; }

            public bool EditEnabled { set; get; }
        }

        public class SenzokuDriverListModel
        {

            
        }

        public class SenzokuModalModel
        {
            /// <summary> 企業ID </summary>
            public int CompanyID { set; get; }

            /// <summary> ログインユーザID </summary>
            public int UserID { set; get; }

            public Dto.M_Senzoku_Local Senzoku { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public IEnumerable<SelectListItem> SeikyuKubunList { set; get; }

            public IEnumerable<SelectListItem> CalcKubunList { set; get; }

            public IEnumerable<SelectListItem> GroupList { get; set; }

            /// <summary> 顧客表示名 </summary>
            public string KokyakuName { set; get; }

            /// <summary> 顧客担当表示名 </summary>
            public string KokyakuTantouName { set; get; }
        }

        public class SenzokuDriverModalModel
        {
            /// <summary> 企業ID </summary>
            public int CompanyID { set; get; }

            /// <summary> ログインユーザID </summary>
            public int UserID { set; get; }

            public Dto.M_Senzoku_Driver_Local SenzokuDriver { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            /// <summary> 乗務員表示名 </summary>
            public string DriverDisplay { set; get; }

            /// <summary> 乗務員車輌表示名 </summary>
            public string DriverSyaryoDisplay { set; get; }
        }
        #endregion　専属

    }
}
