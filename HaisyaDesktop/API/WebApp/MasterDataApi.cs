using HaisyaDesktop;
using HaisyaDesktop.API;
using HaisyaDesktop.Dto;
using HaisyaDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.API.WebApp
{
    class MasterDataApi : BaseHttpClient
    {

        public MasterDataApi()
        {
        }

        #region V_LoginUser
        /*****************************************************************************
          V_LoginUser
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_LoginUser_Local>> GetLoginUserList(int iCompanyID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/V_LoginUserList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<Dto.V_LoginUser_Local>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="loginID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public async Task<Dto.V_LoginUser_Local> GetLoginUserList(string companyCode, string loginID, int loginUserID = 0)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/V_LoginUser?");
            if (companyCode != null) { url += string.Format("&CompanyCode={0}", companyCode); }
            if (loginID != null) { url += string.Format("&LoginID={0}", loginID); }
            if (loginUserID > 0)  { url += string.Format("&LoginUserID={0}", loginUserID); }
            //データ取得
            return await GetHttpData<Dto.V_LoginUser_Local>(url);
        }
        #endregion V_LoginUser

        #region M_Role
        /*****************************************************************************
          M_Role
          *****************************************************************************/
        /// <summary>
        /// M_Roleリストの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_Role_Local>> GetRoleList(int CompanyID, int Role)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_RoleList?");
            if (CompanyID > 0) { url += string.Format("&CompanyID={0}", CompanyID); }
            if (Role > 0) { url += string.Format("&Role={0}", Role); }
            //データ取得
            return await GetHttpData<List<Dto.M_Role_Local>>(url);
        }
        #endregion M_Role

        #region M_Company
        /*****************************************************************************
          M_Company
          *****************************************************************************/
        /// <summary>
        /// M_Companyの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<M_Company_Local> GetCompanyData(int CompanyID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CompanyData?CompanyID={0}", CompanyID.ToString());
            //データ取得
            return await GetHttpData<M_Company_Local>(url);
        }

        /// <summary>
        /// M_Companyマスタの新規登録と更新処理
        /// 
        /// </summary>
        /// <param name="m_Company"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCompanyData(Dto.M_Company_Local m_Company)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateCompanyData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.M_Company_Local>(m_Company, url);
        }
        #endregion M_Company

        #region M_CompanyUser
        /*****************************************************************************
          M_CompanyUser
          *****************************************************************************/
        /// <summary>
        /// M_CompanyUserリストのデータ返却
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="kubun">"eigyo"：営業担当、"tantou"：配車担当、"etc"</param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_Local>> GetCompanyUserList(int iCompanyID, string kubun = "all")
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CompanyUsersList?CompanyID={0}", iCompanyID);
            if (kubun != null) { url += string.Format("&kubun={0}", kubun); }
            //データ取得
            return await GetHttpData<List<M_CompanyUser_Local>>(url);
        }
        #endregion M_CompanyUser

        #region M_CompanyBranch
        /*****************************************************************************
          M_CompanyBranch
          *****************************************************************************/
        /// <summary>
        /// M_CompanyBranchリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_CompanyBranch_Local>> GetCompanyBranchList(int CompanyID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CompanyBranchList?CompanyID={0}", CompanyID.ToString());
            //データ取得
            return await GetHttpData<List<M_CompanyBranch_Local>>(url);
        }

        /// <summary>
        /// M_CompanyBranchの取得
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public async Task<M_CompanyBranch_Local> GetCompanyBranchData(int BranchID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CompanyBranch?BranchID={0}", BranchID.ToString());
            //データ取得
            return await GetHttpData<M_CompanyBranch_Local>(url);
        }

        /// <summary>
        /// M_CompanyBranchマスタの新規登録と更新処理
        /// 
        /// </summary>
        /// <param name="m_CompanyBranch"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateForCompanyBranch(Dto.M_CompanyBranch_Local m_CompanyBranch)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateForCompanyBranch?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.M_CompanyBranch_Local>(m_CompanyBranch, url);
        }

        /// <summary>
        /// M_CompanyBranchマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSortOrderForCompanyBranch(List<Dto.M_CompanyBranch_Local> m_CompanyBranches)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/UpdateSortOrderForCompanyBranch?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<List<Dto.M_CompanyBranch_Local>>(m_CompanyBranches, url);
        }
        #endregion M_CompanyBranch

        #region M_SyaryoSize
        /*****************************************************************************
          M_SyaryoSize
          *****************************************************************************/
        /// <summary>
        /// M_SyaryoSizeリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoSize_Local>> GetSyaryoSizeList(int iCompanyID, string size = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_SyaryoSizeList?CompanyID={0}", iCompanyID);
            if (size != null) { url += string.Format("&size={0}", size); }
            //データ取得
            return await GetHttpData<List<M_SyaryoSize_Local>>(url);
        }

        /// <summary>
        /// M_SyaryoSizeマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSyaryoSizeListSortOrder(SettingModel.SyaryoSizeListDto dto)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/UpdateSyaryoSizeListSortOrder?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<SettingModel.SyaryoSizeListDto>(dto, url);
        }

        /// <summary>
        /// M_SyaryoSizeマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_SyaryoSize"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoSizeMasterData(Dto.M_SyaryoSize_Local m_SyaryoSize)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateSyaryoSizeMasterData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.M_SyaryoSize_Local>(m_SyaryoSize, url);
        }
        #endregion M_SyaryoSize

        #region M_Syaryo
        /*****************************************************************************
          M_Syaryo
          *****************************************************************************/
        /// <summary>
        /// M_Syaryoマスタの取得
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<M_Syaryo_Local> GetSyaryoData(int iCompanyID, string Syasyu, string Kata)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}",Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            //データ取得
            List<M_Syaryo_Local> result =  await GetHttpData<List<M_Syaryo_Local>>(url);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// M_Syaryoリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_Syaryo_Local>> GetSyaryoList(int iCompanyID, string Syasyu = null, string Kata = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            //データ取得
            return await GetHttpData<List<M_Syaryo_Local>>(url);
        }

        /// <summary>
        /// M_syaryoマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSyaryoListSortOrder(SettingModel.SyaryoListDto dto)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/UpdateSyaryoListSortOrder?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<SettingModel.SyaryoListDto>(dto, url);
        }

        /// <summary>
        /// M_syaryoマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_Syaryo"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoMasterData(Dto.M_Syaryo_Local m_Syaryo)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateSyaryoMasterData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.M_Syaryo_Local>(m_Syaryo, url);
        }


        /// <summary>
        /// M_syaryoマスタの削除処理
        /// データの整合性チェックして問題無ければ物理削除
        /// </summary>
        /// <param name="m_Syaryo"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> DeleteSyaryoMasterData(Dto.M_Syaryo_Local m_Syaryo)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/DeleteSyaryoMasterData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<Dto.M_Syaryo_Local>(m_Syaryo, url);
        }
        #endregion M_Syaryo

        #region M_DefaultMoney
        /*****************************************************************************
          M_DefaultMoney
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Eria"></param>
        /// <param name="SyasyuSize"></param>
        /// <returns></returns>
        public async Task<List<M_DefaultMoney_Local>> GetDefaultMoneyList(string Eria, string SyasyuSize)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_DefaultMoneyList?");
            if (Eria != null) { url += string.Format("&Eria={0}", Eria); }
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            //データ取得
            return await GetHttpData<List<M_DefaultMoney_Local>>(url);
        }

        /// <summary>
        /// M_DefaultMoneyマスタの新規登録と更新処理
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_DefaultMoneyList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateDefaultMoneyData(List<Dto.M_DefaultMoney_Local> m_DefaultMoneys)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateDefaultMoneyData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<List<Dto.M_DefaultMoney_Local>>(m_DefaultMoneys, url);
        }
        #endregion M_DefaultMoney

        #region M_DefaultMoney_WaitTimeForErium
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForErium
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForErium
        /// </summary>
        /// <param name="SyasyuSize"></param>
        /// <param name="Eria"></param>
        /// <returns></returns>
        public async Task<List<M_DefaultMoney_WaitTimeForArea_Local>> GetDefaultMoneyWaitTimeForEriaList(string SyasyuSize, string Eria)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_DefaultMoneyWaitTimeForEriaList?");
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            if (Eria != null) { url += string.Format("&Eria={0}", Eria); }
            //データ取得
            return await GetHttpData<List<M_DefaultMoney_WaitTimeForArea_Local>>(url);
        }

        /// <summary>
        /// M_DefaultMoneyWaitTimeForEriaマスタの新規登録と更新処理
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_DefaultMoneyWaitTimeForEriaList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateDefaultMoneyWaitTimeForEriaData(List<Dto.M_DefaultMoney_WaitTimeForArea_Local> m_DefaultMoneyWaitTimeForEriaList)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateDefaultMoneyWaitTimeForEriaData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<List<Dto.M_DefaultMoney_WaitTimeForArea_Local>>(m_DefaultMoneyWaitTimeForEriaList, url);
        }
        #endregion M_DefaultMoney_WaitTimeForErium

        #region M_DefaultMoney_WaitTimeForCompany
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForCompany
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForCompany
        /// </summary>
        /// <param name="SyasyuSize"></param>
        /// <param name="Company"></param>
        /// <returns></returns>
        public async Task<List<M_DefaultMoney_WaitTimeForCompany_Local>> GetDefaultMoneyWaitTimeForCompanyList(int iCompanyID, string SyasyuSize, string Company)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_DefaultMoneyWaitTimeForCompanyList?CompanyID={0}", iCompanyID);
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            if (Company != null) { url += string.Format("&Company={0}", Company); }
            //データ取得
            return await GetHttpData<List<M_DefaultMoney_WaitTimeForCompany_Local>>(url);
        }

        /// <summary>
        /// M_DefaultMoneyWaitTimeForCompanyマスタの新規登録と更新処理
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="m_DefaultMoneyWaitTimeForCompanyList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateDefaultMoneyWaitTimeForCompanyData(int iCompanyID, List<Dto.M_DefaultMoney_WaitTimeForCompany_Local> m_DefaultMoneyWaitTimeForCompanyList)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateDefaultMoneyWaitTimeForCompanyData?CompanyID={0}", iCompanyID);
            //データ更新
            return await ExecHttpData<List<Dto.M_DefaultMoney_WaitTimeForCompany_Local>>(m_DefaultMoneyWaitTimeForCompanyList, url);
        }
        #endregion M_DefaultMoney_WaitTimeForCompany

        #region M_PersonnelExpense
        /*****************************************************************************
          M_PersonnelExpense
          *****************************************************************************/
        /// <summary>
        /// M_PersonnelExpense
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_PersonnelExpense_Local>> GetPersonnelExpenseList(int iCompanyID, string Syasyu = null, string Kata = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_PersonnelExpenseList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            //データ取得
            return await GetHttpData<List<M_PersonnelExpense_Local>>(url);
        }
        #endregion M_PersonnelExpense

        #region M_FuelCost
        /*****************************************************************************
          M_FuelCost
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<M_FuelCost_Local> GetFuelCostData(int iCompanyID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_FuelCostData?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<M_FuelCost_Local>(url);
        }
        #endregion M_FuelCost

        #region M_SyaryoCost
        /*****************************************************************************
          M_SyaryoCost
          *****************************************************************************/
        /// <summary>
        /// M_SyaryoCost
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoCost_Local>> GetSyaryoCostList(int iCompanyID, string Syasyu = null, string Kata = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_SyaryoCostList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            //データ取得
            return await GetHttpData<List<M_SyaryoCost_Local>>(url);
        }

        /// <summary>
        /// M_SyaryoCostマスタの新規登録と更新処理
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_Syaryo"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoCostData(List<Dto.M_SyaryoCost_Local> m_SyaryoCostList)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/InsertUpdateSyaryoCostData?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<List<Dto.M_SyaryoCost_Local>>(m_SyaryoCostList, url);
        }
        #endregion M_SyaryoCost

        #region M_PostCode
        /*****************************************************************************
          M_PostCode
          *****************************************************************************/
        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="iArea"></param>
        /// <returns></returns>
        public async Task<List<M_PostCode_Local>> GetGetAddressList(int iArea)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/GetAddressList?iArea={0}", iArea);
            //データ取得
            return await GetHttpData<List<M_PostCode_Local>>(url);
        }

        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_PostCode_Local>> M_AddressToShiKuChoList(string AreaList)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_AddressToShiKuChoList?AreaList={0}", AreaList);
            //データ取得
            return await GetHttpData<List<M_PostCode_Local>>(url);
        }
        #endregion M_PostCode

        #region M_PublishGroup
        /*****************************************************************************
          M_PublishGroup
          *****************************************************************************/
        /// <summary>
        /// M_PublishGroup
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="BranchID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<List<M_PublishGroup_Local>> M_PublishGroupList(int CompanyID, int BranchID, int UserID)
        {

            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_PublishGroupList?");
            if (CompanyID > 0 ) { url += string.Format("&CompanyID={0}", CompanyID); }
            if (BranchID > 0) { url += string.Format("&BranchID={0}", BranchID); }
            if (UserID > 0) { url += string.Format("&UserID={0}", UserID); }
            //データ取得
            return await GetHttpData<List<M_PublishGroup_Local>>(url);
        }
        #endregion M_PublishGroup

        #region M_CompanyUser_Group
        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_Group_Local>> M_CompanyUserGroupList(int UserID)
        {

            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CompanyUserGroupList?UserID={0}", UserID);

            return await GetHttpData<List<M_CompanyUser_Group_Local>>(url);

        }
        #endregion M_CompanyUser_Group

        #region M_Customer
        /// <summary>
        /// M_Customer
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Local>> CustomerList(int CompanyID, string key = null, string cd = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/CustomerList?CompanyID={0}", CompanyID.ToString());
            if (cd != null) { url += string.Format("&Cd={0}", cd); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            return await GetHttpData<List<M_Customer_Local>>(url);
        }

        /// <summary>
        /// M_Customer
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<M_Customer_Local> M_CustomerData(int CustomerID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_CustomerData?CustomerID={0}", CustomerID.ToString());
            return await GetHttpData<M_Customer_Local>(url);
        }


        #endregion M_Customer

        #region M_Customer_Tantou
        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Tantou_Local>> M_Customer_TantouList(int CompanyID)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_Customer_TantouList?CompanyID={0}", CompanyID.ToString());
            return await GetHttpData<List<M_Customer_Tantou_Local>>(url);
        }
        #endregion M_Customer_Tantou

        #region M_Anken_Excharge
        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Anken_Excharge_Local>> M_Anken_ExchargeList(int CompanyID, string SyasyuSize)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_Anken_ExchargeList?CompanyID={0}", CompanyID.ToString());
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            return await GetHttpData<List<M_Anken_Excharge_Local>>(url);
        }
        #endregion M_Anken_Excharge

        #region M_SyaryoManagement
        /*****************************************************************************
          M_SyaryoManagement
          *****************************************************************************/
        /// <summary>
        /// M_SyaryoManagement
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoManagement_Local>> GetSyaryoManagementList(int iCompanyID, string Syaban = null)
        {
            string url = PublicObjects.GetWebAPIHosts() + string.Format("MasterData/M_SyaryoManagementList?CompanyID={0}", iCompanyID);
            if (Syaban != null) { url += string.Format("&Syaban={0}", Syaban); }
            //データ取得
            return await GetHttpData<List<M_SyaryoManagement_Local>>(url);
        }
        #endregion M_SyaryoManagement

        #region V_DRIVER


        #endregion V_DRIVER

    }

}
