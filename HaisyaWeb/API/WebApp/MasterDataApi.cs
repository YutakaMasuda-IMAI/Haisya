using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class MasterDataApi : BaseHttpClient
    {
        public MasterDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        #region M_CompanyUser
        /*****************************************************************************
          M_CompanyUser
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="kubun">[all]：全て、[eigyo]：営業担当のみ、[tantou]：配車担当のみ、[seikyu]：Seikyu</param>
        /// <param name="NotDel">0：全て、1：DelFlgがfalseのみ</param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_Local>> GetCompanyUserList(int iCompanyID, string kubun = "all", int NotDel = 0)
        {
            string url = string.Format("MasterData/M_CompanyUsersList?CompanyID={0}&NotDel={1}", iCompanyID, NotDel);
            if (kubun != null) { url += string.Format("&kubun={0}", kubun); }
            //データ取得
            return await GetHttpData<List<M_CompanyUser_Local>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<M_CompanyUser_Local> GetCompanyUserData(int UserID)
        {
            string url = string.Format("MasterData/M_CompanyUsersData?UserID={0}", UserID);
            //データ取得
            return await GetHttpData<M_CompanyUser_Local>(url);
        }

        /// <summary>
        /// M_CompanyUserマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_CompanyUser"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCompanyUserMasterData(Dto.M_CompanyUser_Local m_CompanyUser)
        {
            string url = string.Format("MasterData/InsertUpdateCompanyUserMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_CompanyUser_Local>(m_CompanyUser, url);
        }
        #endregion M_CompanyUser

        #region M_CompanyDriver
        /*****************************************************************************
          M_CompanyDriver
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="NotDel">0：全て、1：DelFlgがfalseのみ</param>
        /// <param name="UserID">0以上の場合、その担当乗務員のみ</param>
        /// <returns></returns>
        public async Task<List<M_CompanyDriver_Local>> GetCompanyDriverList(int CompanyID, int NotDel = 0, int UserID = 0)
        {
            string url = string.Format("MasterData/M_CompanyDriversList?CompanyID={0}&NotDel={1}", CompanyID, NotDel);
            if (UserID > 0) { url += string.Format("&UserID={0}", UserID); }
            //データ取得
            return await GetHttpData<List<M_CompanyDriver_Local>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<M_CompanyDriver_Local> GetCompanyDriverData(int DriverID)
        {
            string url = string.Format("MasterData/M_CompanyDriversData?DriverID={0}", DriverID);
            //データ取得
            return await GetHttpData<M_CompanyDriver_Local>(url);
        }

        /// <summary>
        /// M_CompanyDriverマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_CompanyDriver"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCompanyDriverMasterData(M_CompanyDriver_Local M_CompanyDriver)
        {
            string url = string.Format("MasterData/InsertUpdateCompanyDriverMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_CompanyDriver_Local>(M_CompanyDriver, url);
        }
        #endregion M_CompanyDriver

        #region V_CompanyDriver
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<V_CompanyDriver_Local>> GetVCompanyDriverList(int iCompanyID, int NotDel = 0, int haisyaGroupID = 0, int driverId = 0)
        {
            string url = string.Format("MasterData/GetVCompanyDriversList?CompanyID={0}&NotDel={1}&haisyaGroupID={2}&driverId={3}", iCompanyID, NotDel, haisyaGroupID, driverId);
            //データ取得
            return await GetHttpData<List<V_CompanyDriver_Local>>(url);
        }
        #endregion V_CompanyDriver

        #region M_CompanyDriver_Syaryo
        /*****************************************************************************
          M_CompanyDriver_Syaryo
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="UserID"></param>
        /// <param name="NotDel">0：全て、1：DelFlgがfalseのみ</param>
        /// <returns></returns>
        public async Task<List<M_CompanyDriver_Syaryo_Local>> GetCompanyDriverSyaryoList(int iCompanyID, int DriverID = 0, int NotDel = 0)
        {
            string url = string.Format("MasterData/M_CompanyDriverSyaryosList?CompanyID={0}&NotDel={1}&DriverID={2}", iCompanyID, NotDel, DriverID);
            //データ取得
            return await GetHttpData<List<M_CompanyDriver_Syaryo_Local>>(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SyaryoID"></param>
        /// <returns></returns>
        public async Task<M_CompanyDriver_Syaryo_Local> GetCompanyDriverSyaryoData(int DriverSyaryoID)
        {
            string url = string.Format("MasterData/M_CompanyDriverSyaryosData?DriverSyaryoID={0}", DriverSyaryoID);
            //データ取得
            return await GetHttpData<M_CompanyDriver_Syaryo_Local>(url);
        }

        /// <summary>
        /// M_CompanyDriver_Syaryoマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_CompanyDriver_Syaryo"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCompanyDriverSyaryoMasterData(List<Dto.M_CompanyDriver_Syaryo_Local> M_CompanyDriver_Syaryo)
        {
            string url = string.Format("MasterData/InsertUpdateCompanyDriverSyaryoMasterData");
            //データ更新
            return await ExecHttpData<List<Dto.M_CompanyDriver_Syaryo_Local>>(M_CompanyDriver_Syaryo, url);
        }
        #endregion M_CompanyDriver_Syaryo

        #region M_CompanyUser_Group
        /*****************************************************************************
          M_CompanyUser_Group
          *****************************************************************************/
        /// <summary>
        /// M_CompanyUser_Groupリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_Group_Local>> GetCompanyUserGroupList(int iCompanyID, Service.UserGroupLists GroupKubun, int GroupID = 0)
        {
            string url = string.Format("MasterData/M_CompanyUserGroupList?CompanyID={0}&GroupKubun={1}", iCompanyID, (int)GroupKubun);
            if (GroupID != 0)
            {
                url += string.Format("&GroupID={0}", GroupID);
            }
            //データ取得
            return await GetHttpData<List<M_CompanyUser_Group_Local>>(url);
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの取得
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<M_CompanyUser_Group_Local> GetCompanyUserGroupData(int iCompanyID, int GroupID)
        {
            string url = string.Format("MasterData/M_CompanyUserGroupList?CompanyID={0}&GroupID={1}", iCompanyID, GroupID);
            //データ取得
            List<M_CompanyUser_Group_Local> result = await GetHttpData<List<M_CompanyUser_Group_Local>>(url);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateCompanyUserGroupListSortOrder(SettingModel.CompanyUserGroupDto dto)
        {
            string url = string.Format("MasterData/UpdateCompanyUserGroupListSortOrder");
            //データ更新
            return await ExecHttpData<SettingModel.CompanyUserGroupDto>(dto, url);
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_CompanyUser_Group"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCompanyUserGroupMasterData(SettingModel.CompanyUserGroupModalDto dto)
        {
            string url = string.Format("MasterData/InsertUpdateCompanyUserGroupMasterData");
            //データ更新
            return await ExecHttpData<SettingModel.CompanyUserGroupModalDto>(dto, url);
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタの削除処理
        /// データの整合性チェックして問題無ければ物理削除
        /// </summary>
        /// <param name="M_CompanyUser_Group"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> DeleteCompanyUserGroupMasterData(M_CompanyUser_Group_Local M_CompanyUser_Group)
        {
            string url = string.Format("MasterData/DeleteCompanyUserGroupMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_CompanyUser_Group_Local>(M_CompanyUser_Group, url);
        }


        /// <summary>
        /// M_CompanyUser_Group_Local
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_Group_Local>> GetCompanyUserGroupListFromUserID(int UserID)
        {

            string url = string.Format("MasterData/CompanyUserGroupListFromUserID?UserID={0}", UserID);

            return await GetHttpData<List<M_CompanyUser_Group_Local>>(url);

        }
        #endregion M_CompanyUser_Group

        #region M_CompanyUser_GroupUser
        /// <summary>
        /// M_CompanyUser_GroupUserリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_GroupUser_Local>> GetCompanyUserGroupUserList(int GroupID)
        {
            string url = string.Format("MasterData/M_CompanyUserGroupUserList?GroupID={0}", GroupID);
            //データ取得
            return await GetHttpData<List<M_CompanyUser_GroupUser_Local>>(url);
        }
        #endregion M_CompanyUser_GroupUser

        #region V_LoginUser
        /*****************************************************************************
          V_LoginUser
          *****************************************************************************/
        /// <summary>
        /// V_LoginUserリストの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.V_LoginUser_Local>> GetV_LoginUserList(int iCompanyID)
        {
            string url = string.Format("MasterData/V_LoginUserList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<Dto.V_LoginUser_Local>>(url);
        }

        /// <summary>
        /// V_LoginUserデータの返却
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="loginID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public async Task<Dto.V_LoginUser_Local> GetV_LoginUserData(string companyCode, string loginID, int loginUserID = 0)
        {
            string url = string.Format("MasterData/V_LoginUser?");
            if (companyCode != null) { url += string.Format("&CompanyCode={0}", companyCode); }
            if (loginID != null) { url += string.Format("&LoginID={0}", loginID); }
            if (loginUserID > 0) { url += string.Format("&LoginUserID={0}", loginUserID); }
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
            string url = string.Format("MasterData/M_RoleList?");
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
            string url = string.Format("MasterData/M_CompanyData?CompanyID={0}", CompanyID.ToString());
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
            string url = string.Format("MasterData/InsertUpdateCompanyData");
            //データ更新
            return await ExecHttpData<Dto.M_Company_Local>(m_Company, url);
        }
        #endregion M_Company

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
            string url = string.Format("MasterData/M_CompanyBranchList?CompanyID={0}", CompanyID.ToString());
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
            string url = string.Format("MasterData/M_CompanyBranch?BranchID={0}", BranchID.ToString());
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
            string url = string.Format("MasterData/InsertUpdateForCompanyBranch");
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
            string url = string.Format("MasterData/UpdateSortOrderForCompanyBranch");
            //データ更新
            return await ExecHttpData<List<Dto.M_CompanyBranch_Local>>(m_CompanyBranches, url);
        }
        #endregion M_CompanyBranch

        #region M_Kata
        /*****************************************************************************
          M_Kata
          *****************************************************************************/
        /// <summary>
        /// M_Kataの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<M_Kata_Local> GetKataData(int iCompanyID, string kataID)
        {
            List<M_Kata_Local> list = await GetKataList(iCompanyID);
            M_Kata_Local data = list.FirstOrDefault(m => m.Kata_ID == kataID);
            return data;
        }

        /// <summary>
        /// M_Kataリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Kata_Local>> GetKataList(int iCompanyID)
        {
            string url = string.Format("MasterData/KataList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Kata_Local>>(url);
        }

        /// <summary>
        /// M_Kataマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateKataSortOrder(SettingModel.KataDto dto)
        {
            string url = string.Format("MasterData/UpdateKataSortOrder");
            //データ更新
            return await ExecHttpData<List<Dto.M_Kata_Local>>(dto.KataList, url);
        }

        /// <summary>
        /// M_Kataマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Kata"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateKataMasterData(Dto.M_Kata_Local M_Kata)
        {
            string url = string.Format("MasterData/InsertUpdateKataMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Kata_Local>(M_Kata, url);
        }
        #endregion M_Kata

        #region M_SyaryoSize
        /*****************************************************************************
          M_SyaryoSize
          *****************************************************************************/
        /// <summary>
        /// M_SyaryoSizeの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<M_SyaryoSize_Local> GetSyaryoSizeData(int iCompanyID, string size = null)
        {
            var syaryoSize = await GetSyaryoSizeList(iCompanyID, size);
            return syaryoSize.FirstOrDefault();
        }

        /// <summary>
        /// M_SyaryoSizeリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoSize_Local>> GetSyaryoSizeList(int iCompanyID, string size = null)
        {
            string url = string.Format("MasterData/M_SyaryoSizeList?CompanyID={0}", iCompanyID);
            if (size != null) { url += string.Format("&size={0}", size); }
            //データ取得
            return await GetHttpData<List<M_SyaryoSize_Local>>(url);
        }

        /// <summary>
        /// M_SyaryoSizeマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSyaryoSizeSortOrder(SettingModel.SyaryoSizeDto dto)
        {
            string url = string.Format("MasterData/UpdateSyaryoSizeSortOrder");
            //データ更新
            return await ExecHttpData<SettingModel.SyaryoSizeDto>(dto, url);
        }

        /// <summary>
        /// M_SyaryoSizeマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_SyaryoSize"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoSizeMasterData(Dto.M_SyaryoSize_Local m_SyaryoSize)
        {
            string url = string.Format("MasterData/InsertUpdateSyaryoSizeMasterData");
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
        public async Task<M_Syaryo_Local> GetSyaryoData(int iCompanyID, int SyaryoID)
        {
            string url = string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (SyaryoID > 0) { url += string.Format("&SyaryoID={0}", SyaryoID); }
            //データ取得
            List<M_Syaryo_Local> result = await GetHttpData<List<M_Syaryo_Local>>(url);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// M_Syaryoリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<List<M_Syaryo_Local>> GetSyaryoList(int iCompanyID, string Syasyu = null, string Kata = null, string Size = null)
        {
            string url = string.Format("MasterData/M_SyaryoList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            if (Size != null) { url += string.Format("&Size={0}", Size); }
            //データ取得
            return await GetHttpData<List<M_Syaryo_Local>>(url);
        }

        /// <summary>
        /// M_syaryoマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSyaryoListSortOrder(SettingModel.SyaryoDto dto)
        {
            string url = string.Format("MasterData/UpdateSyaryoListSortOrder");
            //データ更新
            return await ExecHttpData<SettingModel.SyaryoDto>(dto, url);
        }

        /// <summary>
        /// M_syaryoマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_Syaryo"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoMasterData(Dto.M_Syaryo_Local m_Syaryo)
        {
            string url = string.Format("MasterData/InsertUpdateSyaryoMasterData");
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
            string url = string.Format("MasterData/DeleteSyaryoMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Syaryo_Local>(m_Syaryo, url);
        }
        #endregion M_Syaryo

        #region M_SyaryoManagement
        /*****************************************************************************
          M_SyaryoManagement
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_SyaryoManagement_Local>> GetSyaryoManagementList(int CompanyID, string Syaban = null, int UserID = 0, string Syasyu = null, string Kata = null)
        {
            string url = string.Format("MasterData/M_SyaryoManagementList?CompanyID={0}", CompanyID);
            if (Syaban != null) { url += string.Format("&Syaban={0}", Syaban); }
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            if (UserID > 0 ) { url += string.Format("&UserID={0}", UserID); }

            //データ取得
            return await GetHttpData<List<M_SyaryoManagement_Local>>(url);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<M_SyaryoManagement_Local> GetSyaryoManagementData(int SyaryoManagementID)
        {
            string url = string.Format("MasterData/M_SyaryoManagementsData?SyaryoManagementID={0}", SyaryoManagementID);
            //データ取得
            return await GetHttpData<M_SyaryoManagement_Local>(url);
        }

        /// <summary>
        /// M_SyaryoManagementマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_SyaryoManagement"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyaryoManagementMasterData(Dto.M_SyaryoManagement_Local M_SyaryoManagement)
        {
            string url = string.Format("MasterData/InsertUpdateSyaryoManagementMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_SyaryoManagement_Local>(M_SyaryoManagement, url);
        }
        #endregion M_SyaryoManagement

        #region M_DefaultMoney
        /*****************************************************************************
          M_DefaultMoney
          *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="SyasyuSize"></param>
        /// <returns></returns>
        public async Task<List<M_DefaultMoney_Local>> GetDefaultMoneyList(string Area, string SyasyuSize)
        {
            string url = string.Format("MasterData/M_DefaultMoneyList?");
            if (Area != null) { url += string.Format("&Area={0}", Area); }
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
            string url = string.Format("MasterData/InsertUpdateDefaultMoneyData");
            //データ更新
            return await ExecHttpData<List<Dto.M_DefaultMoney_Local>>(m_DefaultMoneys, url);
        }
        #endregion M_DefaultMoney

        #region M_DefaultMoney_WaitTimeForArea
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForArea
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaのリストを返却
        /// </summary>
        /// <param name="SyasyuSize"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public async Task<List<M_DefaultMoney_WaitTimeForArea_Local>> GetDefaultMoneyWaitTimeForAreaList(string SyasyuSize, string Area)
        {
            string url = string.Format("MasterData/M_DefaultMoneyWaitTimeForAreaList?");
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            if (Area != null) { url += string.Format("&Area={0}", Area); }
            //データ取得
            return await GetHttpData<List<M_DefaultMoney_WaitTimeForArea_Local>>(url);
        }

        /// <summary>
        /// M_DefaultMoneyWaitTimeForAreaマスタの新規登録と更新処理
        /// 車種・型キーでのDelete⇒Inset
        /// </summary>
        /// <param name="m_DefaultMoneyWaitTimeForAreaList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateDefaultMoneyWaitTimeForAreaData(List<Dto.M_DefaultMoney_WaitTimeForArea_Local> m_DefaultMoneyWaitTimeForAreaList)
        {
            string url = string.Format("MasterData/InsertUpdateDefaultMoneyWaitTimeForAreaData");
            //データ更新
            return await ExecHttpData<List<Dto.M_DefaultMoney_WaitTimeForArea_Local>>(m_DefaultMoneyWaitTimeForAreaList, url);
        }
        #endregion M_DefaultMoney_WaitTimeForArea

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
            string url = string.Format("MasterData/M_DefaultMoneyWaitTimeForCompanyList?CompanyID={0}", iCompanyID);
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
            string url = string.Format("MasterData/InsertUpdateDefaultMoneyWaitTimeForCompanyData?CompanyID={0}", iCompanyID);
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
            string url = string.Format("MasterData/M_PersonnelExpenseList?CompanyID={0}", iCompanyID);
            if (Syasyu != null) { url += string.Format("&Syasyu={0}", Syasyu); }
            if (Kata != null) { url += string.Format("&Kata={0}", Kata); }
            //データ取得
            return await GetHttpData<List<M_PersonnelExpense_Local>>(url);
        }
        /// <summary>
        /// M_PersonnelExpenseマスタの新規登録と更新処理
        /// 会社IDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="m_PersonnelExpenseList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdatePersonnelExpenseData(int iCompanyID, List<Dto.M_PersonnelExpense_Local> m_PersonnelExpenseList)
        {
            string url = string.Format("MasterData/InsertUpdatePersonnelExpenseData?CompanyID={0}", iCompanyID);
            //データ更新
            return await ExecHttpData<List<Dto.M_PersonnelExpense_Local>>(m_PersonnelExpenseList, url);
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
            string url = string.Format("MasterData/M_FuelCostData?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<M_FuelCost_Local>(url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_FuelCost_Local>> GetFuelCostList(int iCompanyID)
        {
            string url = string.Format("MasterData/M_FuelCostList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_FuelCost_Local>>(url);
        }
        /// <summary>
        /// M_FuelCostマスタの新規登録と更新処理
        /// 会社IDキーでのDelete⇒Inset
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="m_FuelCostList"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateFuelCostData(int iCompanyID, List<Dto.M_FuelCost_Local> m_FuelCostList)
        {
            string url = string.Format("MasterData/InsertUpdateFuelCostData?CompanyID={0}", iCompanyID);
            //データ更新
            return await ExecHttpData<List<Dto.M_FuelCost_Local>>(m_FuelCostList, url);
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
        public async Task<List<M_SyaryoCost_Local>> GetSyaryoCostList(int iCompanyID, int SyaryoID = 0)
        {
            string url = string.Format("MasterData/M_SyaryoCostList?CompanyID={0}", iCompanyID);
            if (SyaryoID == 0) { url += string.Format("&SyaryoID={0}", SyaryoID); }
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
            string url = string.Format("MasterData/InsertUpdateSyaryoCostData");
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
        public async Task<List<M_PostCode_Local>> GetGetAddressList(Service.AddressArea iArea)
        {
            string url = string.Format("MasterData/GetAddressList?iArea={0}", iArea);
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
            string url = string.Format("MasterData/M_AddressToShiKuChoList?AreaList={0}", AreaList);
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

            string url = string.Format("MasterData/M_PublishGroupList?");
            if (CompanyID > 0) { url += string.Format("&CompanyID={0}", CompanyID); }
            if (BranchID > 0) { url += string.Format("&BranchID={0}", BranchID); }
            if (UserID > 0) { url += string.Format("&UserID={0}", UserID); }
            //データ取得
            return await GetHttpData<List<M_PublishGroup_Local>>(url);
        }
        #endregion M_PublishGroup

        #region M_Code
        /// <summary>
        /// M_Codeデータの返却
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public async Task<M_Code_Local> M_CodeData(int CodeID)
        {
            string url = string.Format("MasterData/M_CodeData?CodeID={0}", CodeID);
            return await GetHttpData<M_Code_Local>(url);
        }

        /// <summary>
        /// M_Codeリストの返却
        /// </summary>
        /// <returns></returns>
        public async Task<List<M_Code_Local>> M_CodeList()
        {
            string url = string.Format("MasterData/M_CodeList");
            return await GetHttpData<List<M_Code_Local>>(url);
        }
        #endregion M_Code

        #region M_Code_Data
        /// <summary>
        /// M_Code_Dataリストの返却
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public async Task<List<M_Code_Data_Local>> M_Code_DataList(int CodeID)
        {
            string url = string.Format("MasterData/M_Code_DataList?CodeID={0}", CodeID);
            return await GetHttpData<List<M_Code_Data_Local>>(url);
        }
        #endregion M_Code_Data

        #region M_Customer
        /// <summary>
        /// M_Customer
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Local>> GetCustomerList(int CompanyID, string code = null, string key = null, string phone = null)
        {
            string url = string.Format("MasterData/CustomerList?CompanyID={0}", CompanyID.ToString());
            if (code != null) { url += string.Format("&Cd={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }
            return await GetHttpData<List<M_Customer_Local>>(url);
        }

        /// <summary>
        /// M_Customer
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="CustomerCode"></param>
        /// <returns></returns>
        public async Task<M_Customer_Local> GetCustomerData(int CustomerID, string CustomerCode = null)
        {
            string url = string.Format("MasterData/CustomerData?CustomerID={0}", CustomerID.ToString());
            if (CustomerCode != null) { url += string.Format("&CustomerCode={0}", CustomerCode); }
            return await GetHttpData<M_Customer_Local>(url);
        }

        /// <summary>
        /// M_Customerマスタの新規登録と更新処理
        /// Customer_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerData(Models.CustomerModalDto dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerData");
            //データ更新
            return await ExecHttpData<Models.CustomerModalDto>(dto, url);
        }

        #endregion M_Customer

        #region M_Customer_Branch
        /// <summary>
        /// M_Customer_Branch
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Branch_Local>> GetCustomerBranchList(int CompanyID, int CustomerID = 0, int YosyaFlg = 0, string code = null, string key = null, string phone = null, int SelectRowCount = 100)
        {
            string url = string.Format("MasterData/CustomerBranchList?CompanyID={0}&YosyaFlg={1}", CompanyID.ToString(), YosyaFlg.ToString());
            if (code != null) { url += string.Format("&Cd={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            if (CustomerID > 0) { url += string.Format("&CustomerID={0}", CustomerID); }
            return await GetHttpData<List<M_Customer_Branch_Local>>(url);
        }

        /// <summary>
        /// M_Customer_Branch
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<M_Customer_Branch_Local> GetCustomerBranchData(int Customer_BranchID)
        {
            string url = string.Format("MasterData/CustomerBranchData?Customer_BranchID={0}", Customer_BranchID.ToString());
            return await GetHttpData<M_Customer_Branch_Local>(url);
        }

        /// <summary>
        /// M_Customer_Branchマスタの新規登録と更新処理
        /// Customer_Branch_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Customer_Branch"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerBranchData(Models.CustomerBranchModalDto dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerBranchData");
            //データ更新
            return await ExecHttpData<Models.CustomerBranchModalDto>(dto, url);
        }

        /// <summary>
        /// M_Customer_Branch使用率TOPXXリスト
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Branch_Local>> GetCustomerBranchListForUtilizationRate(int CompanyID, int userId, int SelectRowCount = 30)
        {
            string url = string.Format("MasterData/GetCustomerBranchListForUtilizationRate?CompanyID={0}&userId={1}", CompanyID.ToString(), userId.ToString());
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            return await GetHttpData<List<M_Customer_Branch_Local>>(url);
        }

        /// <summary>
        /// M_Customer_Branch使用履歴TOPXXリスト
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Branch_Local>> GetCustomerBranchListForRireki(int CompanyID, int userId, int SelectRowCount = 30)
        {
            string url = string.Format("MasterData/GetCustomerBranchListForRireki?CompanyID={0}&userId={1}", CompanyID.ToString(), userId.ToString());
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            return await GetHttpData<List<M_Customer_Branch_Local>>(url);
        }
        #endregion M_Customer_Branch

        #region M_Customer_Tantou
        /// <summary>
        /// M_Customer
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Tantou_Local>> GetCustomerTantouList(int CompanyID, int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerTantouList?CompanyID={0}&CustomerBranchID={1}", CompanyID.ToString(), CustomerBranchID.ToString());
            return await GetHttpData<List<M_Customer_Tantou_Local>>(url);
        }

        /// <summary>
        /// M_Customerマスタの新規登録と更新処理
        /// Customer_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerTantouData(Dto.M_Customer_Tantou_Local dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerTantouData");
            //データ更新
            return await ExecHttpData<Dto.M_Customer_Tantou_Local>(dto, url);
        }
        #endregion M_Customer_Tantou

        #region M_Customer_TantouHaisyaGroup
        /*****************************************************************************
          M_Customer_TantouHaisyaGroup
          *****************************************************************************/
        /// <summary>
        /// M_Customerリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_TantouHaisyaGroup_Local>> GetCustomerTantouHaisyaGroupList(int CustomerID)
        {
            string url = string.Format("MasterData/GetCustomerTantouHaisyaGroupList?CustomerID={0}", CustomerID.ToString());
            //データ取得
            return await GetHttpData<List<M_Customer_TantouHaisyaGroup_Local>>(url);
        }
        #endregion M_Customer_TantouHaisyaGroup


        #region M_Customer_Driver
        /// <summary>
        /// M_Customer_Driver
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Driver_Local>> GetCustomerDriverList(int CompanyID, int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerDriverList?CompanyID={0}&CustomerBranchID={1}", CompanyID.ToString(), CustomerBranchID.ToString());
            return await GetHttpData<List<M_Customer_Driver_Local>>(url);
        }

        /// <summary>
        /// M_Customer_Driverマスタの新規登録と更新処理
        /// Customer_Driver_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerDriverData(Dto.M_Customer_Driver_Local dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerDriverData");
            //データ更新
            return await ExecHttpData<Dto.M_Customer_Driver_Local>(dto, url);
        }
        #endregion M_Customer_Driver

        #region M_Customer_Driver_Syaryo
        /// <summary>
        /// M_Customer_Driver_Syaryo
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Driver_Syaryo_Local>> GetCustomerDriverSyaryoList(int CompanyID, int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerDriverSyaryoList?CompanyID={0}&CustomerBranchID={1}", CompanyID.ToString(), CustomerBranchID.ToString());
            return await GetHttpData<List<M_Customer_Driver_Syaryo_Local>>(url);
        }

        /// <summary>
        /// M_Customer_Driver_Syaryoマスタの新規登録と更新処理
        /// Customer_DriverSyaryo_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerDriverSyaryoData(Dto.M_Customer_Driver_Syaryo_Local dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerDriverSyaryoData");
            //データ更新
            return await ExecHttpData<Dto.M_Customer_Driver_Syaryo_Local>(dto, url);
        }
        #endregion M_Customer_Driver_Syaryo

        #region V_Customer_Syaryo
        /// <summary>
        /// V_Customer_Syaryo
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<V_Customer_Syaryo_Local>> GetCustomerSyaryoList(int CompanyID, int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerSyaryoList?CompanyID={0}&CustomerBranchID={1}", CompanyID.ToString(), CustomerBranchID.ToString());
            return await GetHttpData<List<V_Customer_Syaryo_Local>>(url);
        }

        #endregion V_Customer_Syaryo

        #region M_Customer_ICSeikyuKubun
        /// <summary>
        /// M_Customer_ICSeikyuKubunリストの返却
        /// </summary>
        /// <param name="CustomerBranchID">Customer_Branch_ID</param>
        /// <returns></returns>
        public async Task<List<M_Customer_ICSeikyuKubun_Local>> GetCustomerICSeikyuKubunList(int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerICSeikyuKubunList?CustomerBranchID={0}", CustomerBranchID);
            return await GetHttpData<List<M_Customer_ICSeikyuKubun_Local>>(url);
        }

        /// <summary>
        /// M_Customer_ICSeikyuKubunマスタの新規登録と更新処理
        /// Customer_Branch_IDで抽出し、Delete⇒Insert
        /// </summary>
        /// <param name="dto">M_Customer_ICSeikyuKubun_Local</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerICSeikyuKubunData(List<Dto.M_Customer_ICSeikyuKubun_Local> dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerICSeikyuKubunData");
            //データ更新
            return await ExecHttpData<List<Dto.M_Customer_ICSeikyuKubun_Local>>(dto, url);
        }
        #endregion M_Customer_ICSeikyuKubun

        #region M_Customer_TollSeikyuKubun
        /// <summary>
        /// M_Customer_TollSeikyuKubunリストの返却
        /// </summary>
        /// <param name="CustomerBranchID">Customer_Branch_ID</param>
        /// <returns></returns>
        public async Task<List<M_Customer_TollSeikyuKubun_Local>> GetCustomerTollSeikyuKubunList(int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerTollSeikyuKubunList?CustomerBranchID={0}", CustomerBranchID);
            return await GetHttpData<List<M_Customer_TollSeikyuKubun_Local>>(url);
        }

        /// <summary>
        /// M_Customer_TollSeikyuKubunマスタの新規登録と更新処理
        /// Customer_Branch_IDで抽出し、Delete⇒Insert
        /// </summary>
        /// <param name="dto">M_Customer_TollSeikyuKubun_Local</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerTollSeikyuKubunData(List<Dto.M_Customer_TollSeikyuKubun_Local> dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerTollSeikyuKubunData");
            //データ更新
            return await ExecHttpData<List<Dto.M_Customer_TollSeikyuKubun_Local>>(dto, url);
        }
        #endregion M_Customer_TollSeikyuKubun

        #region M_Customer_Uriage_Calc
        /// <summary>
        /// M_Customer_Uriage_Calcデータの返却
        /// </summary>
        /// <param name="CustomerBranchID"></param>
        /// <returns></returns>
        public async Task<M_Customer_Uriage_Calc_Local> GetCustomerUriageCalcData(int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerUriageCalcData?CustomerBranchID={0}", CustomerBranchID);
            return await GetHttpData<M_Customer_Uriage_Calc_Local>(url);
        }

        /// <summary>
        /// M_Customer_Uriage_Calcマスタの新規登録と更新処理
        /// Customer_Branch_IDで検索し、データがありば更新、無ければ追加
        /// </summary>
        /// <param name="dto">M_Customer_Uriage_Calc_Local</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerUriageCalcData(Dto.M_Customer_Uriage_Calc_Local dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerUriageCalcData");
            //データ更新
            return await ExecHttpData<Dto.M_Customer_Uriage_Calc_Local>(dto, url);
        }
        #endregion M_Customer_Uriage_Calc

        #region M_Customer_Shiharai_Calc
        /// <summary>
        /// M_Customer_Shiharai_Calcデータの返却
        /// </summary>
        /// <param name="CustomerBranchID">Customer_Branch_ID</param>
        /// <returns></returns>
        public async Task<M_Customer_Shiharai_Calc_Local> GetCustomerShiharaiCalcData(int CustomerBranchID)
        {
            string url = string.Format("MasterData/CustomerShiharaiCalcData?CustomerBranchID={0}", CustomerBranchID);
            return await GetHttpData<M_Customer_Shiharai_Calc_Local>(url);
        }

        /// <summary>
        /// M_Customer_Shiharai_Calcマスタの新規登録と更新処理
        /// Customer_Branch_IDで検索し、データがありば更新、無ければ追加
        /// </summary>
        /// <param name="dto">M_Customer_Shiharai_Calc_Local</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerShiharaiCalcData(Dto.M_Customer_Shiharai_Calc_Local dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerShiharaiCalcData");
            //データ更新
            return await ExecHttpData<Dto.M_Customer_Shiharai_Calc_Local>(dto, url);
        }
        #endregion M_Customer_Shiharai_Calc

        #region M_Customer TracmateLink
        /// <summary>
        /// トラックメイトからの連携データを更新する
        /// </summary>
        /// <param name="customerTruckmeteModalDto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateCustomerTracmateData(Models.CustomerTruckmeteModalDto dto)
        {
            string url = string.Format("MasterData/InsertUpdateCustomerTracmateData");
            //データ更新
            return await ExecHttpData<Models.CustomerTruckmeteModalDto>(dto, url);
        }

        #endregion M_Customer TracmateLink

        #region M_Unit
        /// <summary>
        /// M_Unitの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Unit_Local>> M_UnitList(int CompanyID)
        {
            string url = string.Format("MasterData/M_Unit?CompanyID={0}", CompanyID);
            return await GetHttpData<List<M_Unit_Local>>(url);
        }
        #endregion M_Unit

        #region M_Anken_Excharge
        /// <summary>
        /// M_PostCode
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Anken_Excharge_Local>> M_Anken_ExchargeList(int CompanyID, string SyasyuSize)
        {
            string url = string.Format("MasterData/M_Anken_ExchargeList?CompanyID={0}", CompanyID.ToString());
            if (SyasyuSize != null) { url += string.Format("&SyasyuSize={0}", SyasyuSize); }
            return await GetHttpData<List<M_Anken_Excharge_Local>>(url);
        }

        /// <summary>
        /// M_Anken_Exchargeマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateAnkenExchargeSortOrder(SettingModel.AnkenExchargeDto dto)
        {
            string url = string.Format("MasterData/UpdateAnkenExchargeSortOrder");
            //データ更新
            return await ExecHttpData<SettingModel.AnkenExchargeDto>(dto, url);
        }

        /// <summary>
        /// M_Anken_Exchargeマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Anken_Excharge"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateAnkenExchargeMasterData(Dto.M_Anken_Excharge_Local M_Anken_Excharge)
        {
            string url = string.Format("MasterData/InsertUpdateAnkenExchargeMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Anken_Excharge_Local>(M_Anken_Excharge, url);
        }


        /// <summary>
        /// M_Anken_Exchargeマスタの削除処理
        /// データの整合性チェックして問題無ければ物理削除
        /// </summary>
        /// <param name="M_Anken_Excharge"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> DeleteAnkenExchargeMasterData(Dto.M_Anken_Excharge_Local M_Anken_Excharge)
        {
            string url = string.Format("MasterData/DeleteAnkenExchargeMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Anken_Excharge_Local>(M_Anken_Excharge, url);
        }
        #endregion M_Anken_Excharge

        #region M_LoginUser_Role
        /*****************************************************************************
          M_LoginUser_Role
          *****************************************************************************/
        /// <summary>
        /// M_LoginUser_Roleリストの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_LoginUser_Role_Local>> GetLoginUserRoleList(int iCompanyID)
        {
            string url = string.Format("MasterData/M_LoginUserRoleList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<Dto.M_LoginUser_Role_Local>>(url);
        }

        /// <summary>
        /// M_LoginUser_Roleデータの返却
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="loginID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public async Task<Dto.M_LoginUser_Role_Local> GetLoginUserRoleData(int iCompanyID, int iRole)
        {
            string url = string.Format("MasterData/M_LoginUserRoleData?CompanyID={0}&Role={1}", iCompanyID, iRole);
            //データ取得
            return await GetHttpData<Dto.M_LoginUser_Role_Local>(url);
        }

        /// <summary>
        /// M_LoginUser_Roleマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_LoginUserRole"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateLoginUserRoleMasterData(Dto.M_LoginUser_Role_Local m_LoginUserRole)
        {
            string url = string.Format("MasterData/InsertUpdateLoginUserRoleMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_LoginUser_Role_Local>(m_LoginUserRole, url);
        }
        #endregion M_LoginUser_Role

        #region M_LoginUser
        /*****************************************************************************
          M_LoginUser
          *****************************************************************************/
        /// <summary>
        /// M_LoginUserリストの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_LoginUser_Local>> GetLoginUserList(int iCompanyID)
        {
            string url = string.Format("MasterData/M_LoginUserList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<Dto.M_LoginUser_Local>>(url);
        }

        /// <summary>
        /// M_LoginUserデータの返却
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="loginID"></param>
        /// <param name="loginUserID"></param>
        /// <returns></returns>
        public async Task<Dto.M_LoginUser_Local> GetLoginUserData(int iLoginUserID)
        {
            string url = string.Format("MasterData/M_LoginUserData?LoginUserID={0}", iLoginUserID);
            //データ取得
            return await GetHttpData<Dto.M_LoginUser_Local>(url);
        }

        /// <summary>
        /// M_LoginUser_Roleマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="m_LoginUser"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateLoginUserMasterData(Dto.M_LoginUser_Local m_LoginUser)
        {
            string url = string.Format("MasterData/InsertUpdateLoginUserMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_LoginUser_Local>(m_LoginUser, url);
        }

        /// <summary>
        /// M_LoginUserマスタのパスワード更新
        /// </summary>
        /// <param name="m_LoginUser"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateLoginUserPass(Dto.M_LoginUser_Local m_LoginUser)
        {
            string url = string.Format("MasterData/UpdateLoginUserPass");
            //データ更新
            return await ExecHttpData<Dto.M_LoginUser_Local>(m_LoginUser, url);
        }
        #endregion M_LoginUser

        #region M_Yosya
        /*****************************************************************************
         M_Yosya
         *****************************************************************************/
        /// <summary>
        /// M_Yosyaリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<Dto.M_Yosya_Local>> GetYosyaList(int CompanyID)
        {
           string url = string.Format("MasterData/M_YosyaList?CompanyID={0}", CompanyID.ToString());
           //データ取得
           return await GetHttpData<List<Dto.M_Yosya_Local>>(url);
        }


        /// <summary>
        /// M_Yosyaデータの取得
        /// </summary>
        /// <param name="YosyaID"></param>
        /// <returns></returns>
        public async Task<Dto.M_Yosya_Local> GetYosyaData(int YosyaID)
        {
           string url = string.Format("MasterData/M_YosyaData?YosyaID={0}", YosyaID.ToString());
           //データ取得
           return await GetHttpData<Dto.M_Yosya_Local>(url);
        }

        /// <summary>
        /// M_Yosyaマスタの新規登録と更新処理
        /// 
        /// </summary>
        /// <param name="M_Yosya"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateYosyaData(YosyaModel.YosyaModalDto dto)
        {
           string url = string.Format("MasterData/InsertUpdateYosyaData");
           //データ更新
           return await ExecHttpData<YosyaModel.YosyaModalDto>(dto, url);
        }
        #endregion M_Yosya

        #region M_Yosya_Tantou
        /*****************************************************************************
          M_Yosya_Tantou
          *****************************************************************************/
        /// <summary>
        /// M_Yosyaリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_Tantou_Local>> GetYosyaTantouList(int YosyaID)
        {
            string url = string.Format("MasterData/M_YosyaTantouList?YosyaID={0}", YosyaID.ToString());
            //データ取得
            return await GetHttpData<List<M_Customer_Tantou_Local>>(url);
        }
        #endregion M_Yosya_Tantou

        #region M_Yosya_TantouHaisyaGroup
        /*****************************************************************************
          M_Yosya_TantouHaisyaGroup
          *****************************************************************************/
        /// <summary>
        /// M_Yosyaリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Customer_TantouHaisyaGroup_Local>> GetYosyaTantouHaisyaGroupList(int YosyaID)
        {
            string url = string.Format("MasterData/GetCustomerTantouHaisyaGroupList?YosyaID={0}", YosyaID.ToString());
            //データ取得
            return await GetHttpData<List<M_Customer_TantouHaisyaGroup_Local>>(url);
        }
        #endregion M_Yosya_TantouHaisyaGroup

        #region M_SyasyuKubun
        /*****************************************************************************
          M_SyasyuKubun
          *****************************************************************************/
        /// <summary>
        /// M_SyasyuKubunマスタの取得
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<M_SyasyuKubun_Local> GetSyasyuKubunData(int iCompanyID, int SyasyuKubunID)
        {
            string url = string.Format("MasterData/SyasyuKubunList?CompanyID={0}", iCompanyID);
            if (SyasyuKubunID > 0) { url += string.Format("&SyasyuKubunID={0}", SyasyuKubunID); }
            //データ取得
            List<M_SyasyuKubun_Local> result = await GetHttpData<List<M_SyasyuKubun_Local>>(url);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// M_SyasyuKubunリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="Size"></param>
        /// <param name="KataID"></param>
        /// <returns></returns>
        public async Task<List<M_SyasyuKubun_Local>> GetSyasyuKubunList(int iCompanyID, string Size = null, string KataID = null)
        {
            string url = string.Format("MasterData/SyasyuKubunList?CompanyID={0}", iCompanyID);
            if (Size != null) { url += string.Format("&Size={0}", Size); }
            if (KataID != null) { url += string.Format("&KataID={0}", KataID); }
            //データ取得
            return await GetHttpData<List<M_SyasyuKubun_Local>>(url);
        }

        /// <summary>
        /// M_SyasyuKubunマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateSyasyuKubunListSortOrder(SettingModel.SyasyuKubunDto dto)
        {
            string url = string.Format("MasterData/UpdateSyasyuKubunListSortOrder");
            //データ更新
            return await ExecHttpData<SettingModel.SyasyuKubunDto>(dto, url);
        }

        /// <summary>
        /// M_SyasyuKubunマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_SyasyuKubun"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSyasyuKubunMasterData(Dto.M_SyasyuKubun_Local M_SyasyuKubun)
        {
            string url = string.Format("MasterData/InsertUpdateSyasyuKubunMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_SyasyuKubun_Local>(M_SyasyuKubun, url);
        }


        /// <summary>
        /// M_SyasyuKubunマスタの削除処理
        /// データの整合性チェックして問題無ければ物理削除
        /// </summary>
        /// <param name="M_SyasyuKubun"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> DeleteSyasyuKubunMasterData(Dto.M_SyasyuKubun_Local M_SyasyuKubun)
        {
            string url = string.Format("MasterData/DeleteSyasyuKubunMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_SyasyuKubun_Local>(M_SyasyuKubun, url);
        }
        #endregion M_SyasyuKubun

        #region M_Senzoku
        /*****************************************************************************
          M_Senzoku
          *****************************************************************************/
        /// <summary>
        /// M_Senzokuリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Senzoku_Local>> GetSenzokuList(int CompanyID)
        {
            string url = string.Format("MasterData/GetSenzokuList?CompanyID={0}", CompanyID.ToString());
            //データ取得
            return await GetHttpData<List<M_Senzoku_Local>>(url);
        }

        /// <summary>
        /// V_Senzokuリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<V_Senzoku_Local>> GetSenzokuViewList(int CompanyID)
        {
            string url = string.Format("MasterData/GetSenzokuViewList?CompanyID={0}", CompanyID.ToString());
            //データ取得
            return await GetHttpData<List<V_Senzoku_Local>>(url);
        }


        /// <summary>
        /// V_Senzokuの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="SenzokuID"></param>
        /// <returns></returns>
        public async Task<V_Senzoku_Local> GetSenzokuViewData(int CompanyID, int SenzokuID)
        {
            string url = string.Format("MasterData/GetSenzokuViewList?CompanyID={0}", CompanyID.ToString());
            List<V_Senzoku_Local> list = await GetHttpData<List<V_Senzoku_Local>>(url);
            //データ取得
            return list.FirstOrDefault(m => m.SenzokuID == SenzokuID);
        }


        /// <summary>
        /// M_Senzokuマスタの新規登録と更新処理
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSenzokuData(SettingModel.SenzokuModalModel dto)
        {
            string url = string.Format("MasterData/InsertUpdateSenzokuData");
            //データ更新
            return await ExecHttpData<Dto.M_Senzoku_Local>(dto.Senzoku, url);
        }

        #endregion M_Senzoku

        #region M_Senzoku_Driver
        /*****************************************************************************
          M_Senzoku_Driver
          *****************************************************************************/
        /// <summary>
        /// M_Senzoku_Driverリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Senzoku_Driver_Local>> GetSenzokuDriverList(int CompanyID, DateTime? targetMonth)
        {
            string url = string.Format("MasterData/GetSenzokuDriverList?CompanyID={0}", CompanyID.ToString());
            if (targetMonth != null) { url += string.Format("&targetMonth={0}", targetMonth); }
            //データ取得
            return await GetHttpData<List<M_Senzoku_Driver_Local>>(url);
        }

        /// <summary>
        /// V_Senzoku_Driverリストの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<V_Senzoku_Driver_Local>> GetSenzokuDriverViewList(int CompanyID, DateTime? targetMonth)
        {
            string url = string.Format("MasterData/GetSenzokuDriverViewList?CompanyID={0}", CompanyID.ToString());
            if (targetMonth != null) { url += string.Format("&targetMonth={0}", targetMonth); }
            //データ取得
            return await GetHttpData<List<V_Senzoku_Driver_Local>>(url);
        }

        /// <summary>
        /// V_Senzoku_Driverの取得
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="SenzokuDriverID"></param>
        /// <returns></returns>
        public async Task<V_Senzoku_Driver_Local> GetSenzokuDriverViewData(int CompanyID, DateTime? targetMonth, int SenzokuDriverID)
        {
            string url = string.Format("MasterData/GetSenzokuDriverViewList?CompanyID={0}", CompanyID.ToString());
            if (targetMonth != null) { url += string.Format("&targetMonth={0}", targetMonth); }
            if (SenzokuDriverID > 0) { url += string.Format("&SenzokuDriverID={0}", SenzokuDriverID); }
            List<V_Senzoku_Driver_Local> list = await GetHttpData<List<V_Senzoku_Driver_Local>>(url);

            //データ取得
            return list.FirstOrDefault(m => m.Senzoku_Driver_ID == SenzokuDriverID);
        }

        /// <summary>
        /// M_Senzoku_Driverマスタの新規登録と更新処理
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateSenzokuDriverData(SettingModel.SenzokuDriverModalModel dto)
        {
            string url = string.Format("MasterData/InsertUpdateSenzokuDriverData");
            //データ更新
            return await ExecHttpData<Dto.M_Senzoku_Driver_Local>(dto.SenzokuDriver, url);
        }
        #endregion M_Senzoku_Driver

        #region M_Luggage
        /*****************************************************************************
          M_Luggage
          *****************************************************************************/
        /// <summary>
        /// M_Luggageリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Luggage_Local>> GetLuggageList(int iCompanyID)
        {
            string url = string.Format("MasterData/LuggageList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Luggage_Local>>(url);
        }

        /// <summary>
        /// M_Luggageマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateLuggageSortOrder(List<Dto.M_Luggage_Local> list)
        {
            string url = string.Format("MasterData/UpdateLuggageSortOrder");
            //データ更新
            return await ExecHttpData<List<Dto.M_Luggage_Local>>(list, url);
        }

        /// <summary>
        /// M_Luggageマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Luggage"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateLuggageMasterData(Dto.M_Luggage_Local M_Luggage)
        {
            string url = string.Format("MasterData/InsertUpdateLuggageMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Luggage_Local>(M_Luggage, url);
        }
        #endregion M_Luggage

        #region M_Luggage_Group
        /*****************************************************************************
          M_Luggage_Group
          *****************************************************************************/
        /// <summary>
        /// M_Luggage_Groupリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Luggage_Group_Local>> GetLuggageGroupList(int iCompanyID)
        {
            string url = string.Format("MasterData/LuggageGroupList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Luggage_Group_Local>>(url);
        }


        /// <summary>
        /// M_Luggage_Groupマスタの並び順変更処理
        /// </summary>
        /// <param name="list">List<Dto.M_Luggage_Group_Local></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateLuggageGroupSortOrder(List<Dto.M_Luggage_Group_Local> list)
        {
            string url = string.Format("MasterData/UpdateLuggageGroupSortOrder");
            //データ更新
            return await ExecHttpData<List<Dto.M_Luggage_Group_Local>>(list, url);
        }

        /// <summary>
        /// M_Luggage_Groupマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="data">M_Luggage_Group</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateLuggageGroupMasterData(Dto.M_Luggage_Group_Local data)
        {
            string url = string.Format("MasterData/InsertUpdateLuggageGroupMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Luggage_Group_Local>(data, url);
        }
        #endregion M_Luggage_Group

        #region V_Luggage
        /*****************************************************************************
          V_Luggage
          *****************************************************************************/
        /// <summary>
        /// V_Luggageリストの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<V_Luggage_Local>> GetLuggageViewList(int iCompanyID)
        {
            string url = string.Format("MasterData/LuggageViewList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<V_Luggage_Local>>(url);
        }
        #endregion V_Luggage

        #region M_Equipment
        /*****************************************************************************
          M_Equipment
          *****************************************************************************/
        /// <summary>
        /// M_Equipmentリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Equipment_Local>> GetEquipmentList(int iCompanyID)
        {
            string url = string.Format("MasterData/EquipmentList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Equipment_Local>>(url);
        }

        /// <summary>
        /// M_Equipmentマスタの並び順変更処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateEquipmentSortOrder(List<Dto.M_Equipment_Local> list)
        {
            string url = string.Format("MasterData/UpdateEquipmentSortOrder");
            //データ更新
            return await ExecHttpData<List<Dto.M_Equipment_Local>>(list, url);
        }

        /// <summary>
        /// M_Equipmentマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Equipment"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateEquipmentMasterData(Dto.M_Equipment_Local M_Equipment)
        {
            string url = string.Format("MasterData/InsertUpdateEquipmentMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Equipment_Local>(M_Equipment, url);
        }
        #endregion M_Equipment

        #region M_Equipment_Group
        /*****************************************************************************
          M_Equipment_Group
          *****************************************************************************/
        /// <summary>
        /// M_Equipment_Groupリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Equipment_Group_Local>> GetEquipmentGroupList(int iCompanyID)
        {
            string url = string.Format("MasterData/EquipmentGroupList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Equipment_Group_Local>>(url);
        }

        /// <summary>
        /// M_Equipment_Groupマスタの並び順変更処理
        /// </summary>
        /// <param name="list">List<Dto.M_Equipment_Group_Local></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> UpdateEquipmentGroupSortOrder(List<Dto.M_Equipment_Group_Local> list)
        {
            string url = string.Format("MasterData/UpdateEquipmentGroupSortOrder");
            //データ更新
            return await ExecHttpData<List<Dto.M_Equipment_Group_Local>>(list, url);
        }

        /// <summary>
        /// M_Equipment_Groupマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="data">M_Equipment_Group</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateEquipmentGroupMasterData(Dto.M_Equipment_Group_Local data)
        {
            string url = string.Format("MasterData/InsertUpdateEquipmentGroupMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Equipment_Group_Local>(data, url);
        }
        #endregion M_Equipment_Group

        #region M_Burden
        /*****************************************************************************
          M_Burden
          *****************************************************************************/
        /// <summary>
        /// M_Burdenリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Burden_Local>> GetBurdenList(int iCompanyID)
        {
            string url = string.Format("MasterData/BurdenList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Burden_Local>>(url);
        }


        /// <summary>
        /// M_Luggageマスタの新規登録と更新処理
        /// SortOrderが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Luggage"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateBurdenMasterData(Dto.M_Burden_Local M_Burden)
        {
            string url = string.Format("MasterData/InsertUpdateBurdenMasterData");
            //データ更新
            return await ExecHttpData<Dto.M_Burden_Local>(M_Burden, url);
        }
        #endregion M_Burden

        #region M_Burden_Group
        /*****************************************************************************
          M_Burden_Group
          *****************************************************************************/
        /// <summary>
        /// M_Burden_Groupリストの取得＆返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<M_Burden_Group_Local>> GetBurdenGroupList(int iCompanyID)
        {
            string url = string.Format("MasterData/BurdenGroupList?CompanyID={0}", iCompanyID);
            //データ取得
            return await GetHttpData<List<M_Burden_Group_Local>>(url);
        }
        #endregion M_Burden_Group

        #region M_Report_Serch
        /*****************************************************************************
          M_Report_Serch
          *****************************************************************************/
        /// <summary>
        /// 案件データの返却
        /// </summary>
        /// <param name="reportNum"></param>
        /// <param name="kubunID"></param>
        /// <returns></returns>
        public async Task<ReportSearchData> GetReportSearchData(int reportNum, int kubunID)
        {
            string url = _baseUrl + string.Format("MasterData/ReportSearchData?reportNum={0}&kubunID={1}", reportNum, kubunID);
            //データ取得
            try
            {
                return await GetHttpData<ReportSearchData>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion M_Report_Serch

        #region M_Report_Output_Item_Master
        /*****************************************************************************
          M_Report_Output_Item_Master
          *****************************************************************************/
        /// <summary>
        /// M_Report_Output_Item_Masterリストの取得＆返却
        /// </summary>
        /// <param name="kubunID"></param>
        /// <returns></returns>
        public async Task<List<M_Report_Output_Item_Master_Local>> GetReportOutputItemMasterList(int kubunID)
        {
            string url = _baseUrl + string.Format("MasterData/ReportOutputItemMasterList?kubunID={0}", kubunID);
            //データ取得
            try
            {
                return await GetHttpData<List<M_Report_Output_Item_Master_Local>>(url);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion M_Report_Output_Item

        #region M_Report_Output_Item
        /*****************************************************************************
          M_Report_Output_Item
          *****************************************************************************/
        /// <summary>
        /// M_Report_Output_Itemリストの取得＆返却
        /// </summary>
        /// <param name="kubunID"></param>
        /// <param name="companyID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<M_Report_Output_Item_Local>> GetReportOutputItemList(int kubunID, int companyID, int userID = 0)
        {
            string url = _baseUrl + string.Format("MasterData/ReportOutputItemList?kubunID={0}&companyID={1}&userID={2}", kubunID, companyID, userID);
            //データ取得
            try
            {
                return await GetHttpData<List<M_Report_Output_Item_Local>>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto">M_ReportOutputItemDto</param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertReportOutputItems(List<M_Report_Output_Item_Local> dtos, int CompanyID, int UserID)
        {
            string url = _baseUrl + string.Format("MasterData/InsertReportOutputItems?jsonString={0}", "A");
            //データ更新
            return await ExecHttpData<CreateReportOutputItem>(new() { CompanyID = CompanyID, UserID = UserID, ReportOutputItems = dtos }, url);
        }

        #endregion M_Report_Output_Item

        #region M_Vender
        /// <summary>
        /// M_Vender
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Vender_Local>> GetVenderList(int CompanyID, string code = null, string key = null, string phone = null, int SelectRowCount = 100)
        {
            string url = string.Format("MasterData/VenderList?CompanyID={0}", CompanyID.ToString());
            if (code != null) { url += string.Format("&Cd={0}", code); }
            if (key != null) { url += string.Format("&Key={0}", key); }
            if (phone != null) { url += string.Format("&Phone={0}", phone); }
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            return await GetHttpData<List<M_Vender_Local>>(url);
        }

        /// <summary>
        /// M_Vender
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<M_Vender_Local> GetVenderData(int VenderID)
        {
            string url = string.Format("MasterData/VenderData?VenderID={0}", VenderID.ToString());
            return await GetHttpData<M_Vender_Local>(url);
        }

        /// <summary>
        /// M_Venderマスタの新規登録と更新処理
        /// Vender_IDが0の場合は新規、0以上は更新処理
        /// </summary>
        /// <param name="M_Vender"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> InsertUpdateVenderData(Models.VenderModalDto dto)
        {
            string url = string.Format("MasterData/InsertUpdateVenderData");
            //データ更新
            return await ExecHttpData<Models.VenderModalDto>(dto, url);
        }

        /// <summary>
        /// M_Vender使用率TOPXXリスト
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Vender_Local>> GetVenderListForUtilizationRate(int CompanyID, int userId, int SelectRowCount = 30)
        {
            string url = string.Format("MasterData/GetVenderListForUtilizationRate?CompanyID={0}&userId={1}", CompanyID.ToString(), userId.ToString());
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            return await GetHttpData<List<M_Vender_Local>>(url);
        }

        /// <summary>
        /// M_Vender使用履歴TOPXXリスト
        /// </summary>
        /// <param name="AreaList"></param>
        /// <returns></returns>
        public async Task<List<M_Vender_Local>> GetVenderListForRireki(int CompanyID, int userId, int SelectRowCount = 30)
        {
            string url = string.Format("MasterData/GetVenderListForRireki?CompanyID={0}&userId={1}", CompanyID.ToString(), userId.ToString());
            if (SelectRowCount > 0) { url += string.Format("&Top={0}", SelectRowCount); }
            return await GetHttpData<List<M_Vender_Local>>(url);
        }
        #endregion M_Vender

        #region M_Area
        /// <summary>
        /// M_Areaデータの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<List<M_Area_Local>> GetAreaList(int CompanyID)
        {
            string url = string.Format("MasterData/GetAreaList?CompanyID={0}", CompanyID);
            return await GetHttpData<List<M_Area_Local>>(url);
        }
        #endregion M_Area

        #region M_Area_Ken
        /// <summary>
        /// M_Area_Kenデータの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="AreaID"></param>
        /// <returns></returns>
        public async Task<List<M_Area_Ken_Local>> GetAreaKenList(int CompanyID, int AreaID = 0)
        {
            string url = string.Format("MasterData/GetAreaKenList?CompanyID={0}", CompanyID);
            if (AreaID > 0) { url += string.Format("&AreaID={0}", AreaID); }
            return await GetHttpData<List<M_Area_Ken_Local>>(url);
        }
        #endregion M_Area_Ken
        #region M_Report_Serch
        /// <summary>
        /// M_Report_Serch by Report_Number詳細取得のAPI
        /// </summary>
        /// <param name="ReportSerchNum">検索帳票番号</param>
        /// <returns>
        /// 200: M_Report_Serch_Localの詳細
        /// </returns>
        public async Task<M_Report_Serch_Local> GetReportSearch(int ReportSerchNum)
        {
            string url = string.Format("MasterData/M_ReportSerch?ReportSerchNum={0}", ReportSerchNum);
            //データ取得
            try
            {
                return await GetHttpData<M_Report_Serch_Local>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new();
            }
        }
        #endregion M_Report_Serch

        #region M_Report_Serch_Kubun
        /// <summary>
        /// M_Report_Serch_Kubun by Report_Serch_ID
        /// </summary>
        /// <param name="ReportSerchID">検索帳票ID</param>
        /// <returns>
        /// 200: M_Report_Serch_Kubun_Localの一覧
        /// </returns>
        public async Task<List<M_Report_Serch_Kubun_Local>> GetReportSearchKubunList(int ReportSerchID)
        {
            string url = string.Format("MasterData/M_ReportSerchKubun?ReportSerchID={0}", ReportSerchID);
            //データ取得
            return await GetHttpData<List<M_Report_Serch_Kubun_Local>>(url);
        }
        #endregion M_Report_Serch_Kubun

        #region M_Report_Serch_Kubun
        /// <summary>
        /// M_Report_Serch_Kubun by Report_Serch_Kubun_IDの一覧取得のAPI
        /// </summary>
        /// <param name="ReportSerchKubunID">検索帳票の区分ID</param>
        /// <returns>
        /// 200: M_Report_Serch_Kubun_Localの一覧
        /// </returns>
        public async Task<M_Report_Serch_Kubun_Local> GetReportSearchKubun(int ReportSerchKubunID)
        {
            string url = string.Format("MasterData/M_ReportSerchKubunDetail?ReportSerchKubunID={0}", ReportSerchKubunID);
            //データ取得
            return await GetHttpData<M_Report_Serch_Kubun_Local>(url);
        }
        #endregion M_Report_Serch_Kubun

        #region M_Report_Serch_Item
        /// <summary>
        /// M_Report_Serch_Item by Report_Serch_Kubun_ID一覧取得のAPI
        /// </summary>
        /// <param name="ReportSerchKubunID">検索帳票の区分ID</param>
        /// <returns>
        /// 200: M_Report_Serch_Item_Localの一覧
        /// </returns>
        public async Task<List<M_Report_Serch_Item_Local>> GetReportSearchItemList(int ReportSerchKubunID)
        {
            string url = string.Format("MasterData/M_ReportSerchItem?ReportSerchKubunID={0}", ReportSerchKubunID);
            //データ取得
            return await GetHttpData<List<M_Report_Serch_Item_Local>>(url);
        }
        #endregion M_Report_Serch_Item

        #region M_Report_Detail_Param
        /// <summary>
        /// M_Report_Detail_Param by Report_Serch_Kubun_ID一覧取得のAPI
        /// </summary>
        /// <param name="ReportSerchKubunID">検索帳票の区分ID</param>
        /// <returns>
        /// 200: M_Report_Detail_Param_Localの一覧
        /// </returns>
        public async Task<List<M_Report_Detail_Param_Local>> GetReportDetailParamList(int ReportSerchKubunID)
        {
            string url = string.Format("MasterData/M_ReportDetailParam?ReportSerchKubunID={0}", ReportSerchKubunID);
            //データ取得
            return await GetHttpData<List<M_Report_Detail_Param_Local>>(url);
        }
        #endregion M_Report_Detail_Param_Local
    }

}
