using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 事故リストを管理するリポジトリクラス
    /// </summary>
    public class AccidentListRepository: IAccidentListRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        public AccidentListRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

     

        /// <summary>
        /// T_Jiko一覧を取得する
        /// </summary>
        /// <param name=""></param>
        /// <returns>jikoList</returns>
        public async Task<List<T_Jiko>> GetJikoList(
            int companyId,
            int jikoKubun, 
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            int driverId = 0,
            int syaryoManagementId = 0)
        {

            IQueryable<T_Jiko> query = _context.T_Jikos.AsQueryable();

            // ・ COMPANY_ID＝ログイン者のCompany_ID
            // ・事故区分＝T_Jiko．Jiko_Kubun
            query = query.Where(e => e.Jiko_Kubun == jikoKubun && e.Company_ID == companyId);

            // ・事故名＝T_Jiko．Jiko_Display （曖昧検索）
            if (!string.IsNullOrEmpty(jikoDisplay))
            {
                string pattern = $"%{jikoDisplay}%"; // パターンを作成
                query = query.Where(e => EF.Functions.Like(e.Jiko_Display, pattern));

            }

            // ・事故発生日From~To＝T_Jiko．Jiko_Dateが検索条件の範囲内
            if (fromDate != DateTime.MinValue)
            {
                query = query.Where(e => e.Jiko_Date >= fromDate);
            }
            // ・事故発生日From~To＝T_Jiko．Jiko_Dateが検索条件の範囲内
            if (toDate != DateTime.MinValue)
            {
                query = query.Where(e => e.Jiko_Date <= toDate);
            }
            // ・乗務員名＝T_Jiko．Driver_IDのDisplay_Name
            if (driverId != 0)
            {
                query = query.Where(e => e.Driver_ID == driverId);
            }
            // ・車番＝T_Jiko．SyaryouManagement_IDのSyaban_Number
            if (syaryoManagementId != 0)
            {
                query = query.Where(e => e.SyaryoManagement_ID == syaryoManagementId);
            }

            List<T_Jiko> jikoList = await query.ToListAsync();

            return jikoList;
        }


        /// <summary>
        /// M_CompanyUser_GroupUserを取得する
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<List<M_CompanyUser_GroupUser>> GetM_CompanyUser_GroupUserByUser_ID(int userId)
        {
            return await _context.M_CompanyUser_GroupUsers.Where(e => e.User_ID == userId).ToListAsync();
        }        

        /// <summary>
        /// GetMCompanyUserGroupを取得する
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<M_CompanyUser_Group> GetMCompanyUserGroup(int groupId)
        {
            return await _context.M_CompanyUser_Groups.Where(e => e.Group_ID == groupId).FirstOrDefaultAsync();
        }    

        /// <summary>
        /// T_Jiko_WorkFlow_Routeリストを取得する
        /// </summary>
        /// <param name="jikoId"></param>
        /// <returns></returns>
        public async Task<List<T_Jiko_WorkFlow_Route>> GetTJikoWorkFlowRouteByGroupId(int groupId)
        {
            return await _context.T_Jiko_WorkFlow_Routes.Where(e => e.Jiko_WorkFlow_Group_ID == groupId).ToListAsync();
        }    

        /// <summary>
        /// T_Jiko_WorkFlow_Routeリストを取得する
        /// </summary>
        /// <param name="jikoId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<T_Jiko_WorkFlow_Route>> GetTJikoWorkFlowRouteByJikoIdAndGroupId(int jikoId, int groupId)
        {
            return await _context.T_Jiko_WorkFlow_Routes.Where(e => e.Jiko_ID == jikoId && e.Jiko_WorkFlow_Group_ID == groupId).ToListAsync();
        }

        /// <summary>
        /// T_Jiko_WorkFlow_Routeを取得する
        /// </summary>
        /// <param name="jikoWorkFlowID"></param>
        /// <returns></returns>
        public async Task<T_Jiko_WorkFlow_Route> GetTJikoWorkFlowRoute(int jikoWorkFlowID)
        {
            return await _context.T_Jiko_WorkFlow_Routes.Where(e => e.Jiko_WorkFlow_ID == jikoWorkFlowID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// T_Jiko_WorkFlow_Statusを取得する
        /// </summary>
        /// <param name="jikoWorkFlowID"></param>
        /// <returns></returns>
        public async Task<T_Jiko_WorkFlow_Status> GetTJikoWorkFlowStatus(int jikoWorkFlowID)
        {
            return await _context.T_Jiko_WorkFlow_Statuses.Where(e => e.Jiko_WorkFlow_ID == jikoWorkFlowID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// T_Jiko_WorkFlow_Status（未処理）リストを取得する
        /// approvalKubunが「0:未処理/3:差戻未処理」
        /// </summary>
        /// <returns>T_Jiko_WorkFlow_Status（未処理）リスト</returns>
        public async Task<List<T_Jiko_WorkFlow_Status>> GetTJikoWorkFlowStatusByApprovalUnprocessed()
        {
            return await _context.T_Jiko_WorkFlow_Statuses.Where(e => (e.Approval_Kubun == 0 || e.Approval_Kubun == 3)).ToListAsync();
        }

        /// <summary>
        /// 曖昧検索を使ってM_CompanyDriverを取得する
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public async Task<M_CompanyDriver> GetMCompanyDriver(string displayName, int driverId)
        {
            // displayNameはnullなら曖昧検索が起こらない
            if(string.IsNullOrEmpty(displayName))
            {
                return null;
            }
            // 曖昧検索をする
            string pattern = $"%{displayName}%"; // 曖昧検索パターンを作成
            return await _context.M_CompanyDrivers.Where(e => EF.Functions.Like(e.Display_Name, pattern) && e.Driver_ID == driverId).FirstOrDefaultAsync();
        }  

        /// <summary>
        /// 曖昧検索を使ってM_SyaryoManagementを取得する
        /// </summary>
        /// <param name="syabanNumber"></param>
        /// <returns></returns>
        public async Task<M_SyaryoManagement> GetMSyaryoManagement(string syabanNumber, int syaryoManagementId)
        {
            // syabanNumberはnullか"0"なら曖昧検索が起こらない
            if(string.IsNullOrEmpty(syabanNumber) || syabanNumber == "0")
            {
                return null;
            }
            // 曖昧検索をする
            string pattern = $"%{syabanNumber}%"; // 曖昧検索パターンを作成
            return await _context.M_SyaryoManagements.Where(e => EF.Functions.Like(e.Syaban_Number, pattern) && e.SyaryoManagement_ID == syaryoManagementId).FirstOrDefaultAsync();
        }  

        /// <summary>
        /// M_CompanyDriverを取得する
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<M_CompanyDriver> GetMCompanyDriver(int driverId)
        {
            return await _context.M_CompanyDrivers.Where(e => e.Driver_ID == driverId).FirstOrDefaultAsync();
        }  

        /// <summary>
        /// M_SyaryoManagementを取得する
        /// </summary>
        /// <param name="syaryoManagement"></param>
        /// <returns></returns>
        public async Task<M_SyaryoManagement> GetMSyaryoManagement(int syaryoManagement)
        {
            return await _context.M_SyaryoManagements.Where(e => e.SyaryoManagement_ID == syaryoManagement).FirstOrDefaultAsync();
        }  

        /// <summary>
        /// List<M_Code_Datum>を取得する
        /// </summary>
        /// <param name="codeId"></param>
        /// <returns></returns>
        public async Task<List<M_Code_Datum>> GetMCodeDataList(int codeId)
        {
            return await _context.M_Code_Data.Where(e => e.Code_ID == codeId).ToListAsync();
        }  

        /// <summary>
        /// M_Code_Datumを取得する
        /// </summary>
        /// <param name="codeId"></param>
        /// <returns></returns>
        public async Task<M_Code_Datum> GetMCode(int codeId, int jikoKubun)
        {
            return await _context.M_Code_Data.Where(e => e.Code_ID == codeId && e.SortOrder == jikoKubun).FirstOrDefaultAsync();
        }  
    }

    public interface IAccidentListRepository
    {
        Task<List<T_Jiko>> GetJikoList(
            int companyId,
            int jikoKubun, 
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            int driverId = 0,
            int syaryoManagementId = 0);
        Task<List<M_CompanyUser_GroupUser>> GetM_CompanyUser_GroupUserByUser_ID(int userId);
        Task<M_CompanyUser_Group> GetMCompanyUserGroup(int groupId);

        Task<List<T_Jiko_WorkFlow_Route>> GetTJikoWorkFlowRouteByGroupId(int groupId);
        Task<List<T_Jiko_WorkFlow_Route>> GetTJikoWorkFlowRouteByJikoIdAndGroupId(int jikoId, int groupId);
        Task<T_Jiko_WorkFlow_Route> GetTJikoWorkFlowRoute(int jikoWorkFlowID);
        Task<T_Jiko_WorkFlow_Status> GetTJikoWorkFlowStatus(int jikoWorkFlowID);
        Task<List<T_Jiko_WorkFlow_Status>> GetTJikoWorkFlowStatusByApprovalUnprocessed();
        Task<M_CompanyDriver> GetMCompanyDriver(string displayName, int driverId);
        Task<M_SyaryoManagement> GetMSyaryoManagement(string syabanNumber, int syaryoManagementId); 
        Task<M_CompanyDriver> GetMCompanyDriver(int driverId);
        Task<M_SyaryoManagement> GetMSyaryoManagement(int syaryoManagement);
        Task<List<M_Code_Datum>> GetMCodeDataList(int codeId);
        Task<M_Code_Datum> GetMCode(int codeId, int jikoKubun);
    }
}