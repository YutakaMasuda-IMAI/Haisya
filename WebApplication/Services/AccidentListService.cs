using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Repositories;
using WebApplication.Model;

namespace WebApplication.Services
{
    /// <summary>
    /// 事故リストサービスクラス
    /// </summary>
    public class AccidentListService: IAccidentListService
    {
        private readonly IAccidentListRepository _accidentListRepository;

        public AccidentListService(IAccidentListRepository accidentListRepository)
        {
            _accidentListRepository = accidentListRepository;
        }

        /// <summary>
        /// 指定された条件に基づいて事故リストを取得します。
        /// </summary>
        /// <param name="companyId">会社のID。</param>
        /// <param name="userId">ユーザーのID。</param>
        /// <param name="jikoKubun">事故区分。</param>
        /// <param name="loginUser">ログインユーザーかどうかを示すブール値。</param>
        /// <param name="jikoDisplay">事故の表示名（オプション）。</param>
        /// <param name="fromDate">検索開始日（オプション）。</param>
        /// <param name="toDate">検索終了日（オプション）。</param>
        /// <param name="displayName">表示名（オプション）。</param>
        /// <param name="syabanNumber">車輛番号（オプション）。</param>
        /// <returns>指定された条件に基づいて取得した <see cref="List{AccidentListModel.AccidentListItem}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定された条件に基づいて事故リストを非同期に取得します。
        /// ログインユーザーのグループに関連する事故データをフィルタリングし、条件に一致する事故情報をリストとして返します.
        /// 事故の表示名、車輛番号、およびその他の条件に基づいてデータを絞り込みます.
        /// </remarks>
        public async Task<List<AccidentListModel.AccidentListItem>> GetAccidentList(   
            int companyId,
            int userId,
            int jikoKubun, 
            bool loginUser,         
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            string displayName = "",
            string syabanNumber  = "")
        {
            // 会社ID、事故区分、表示名、日付範囲などを基に事故リストを取得
            List<T_Jiko> jikoList = await _accidentListRepository.GetJikoList(companyId, jikoKubun, jikoDisplay, fromDate, toDate, 0, 0);

            // 結果を格納するリストを初期化
            List<AccidentListModel.AccidentListItem> jikoDataLists = new List<AccidentListModel.AccidentListItem>();

            // ログインユーザーのグループIDのリスト作成
            List<M_CompanyUser_GroupUser> companyUserGroupUser = await _accidentListRepository.GetM_CompanyUser_GroupUserByUser_ID(userId);
            List<int> Group_ID_list = new List<int>();
            foreach (M_CompanyUser_GroupUser groupUserLocal in companyUserGroupUser)
			{
                if (groupUserLocal.Del_Flg)
                    continue;
                if (!Group_ID_list.Contains(groupUserLocal.Group_ID))
                    Group_ID_list.Add(groupUserLocal.Group_ID);
            }

            // 事故IDのリスト作成
            List<int> Jiko_ID_list = new List<int>();
            if (loginUser)
            {
                // [ログイン者]
                // 事故WorkFlowIDのリスト作成
                // 未処理データ取得(approvalKubun：0:未処理/3:差戻未処理)
                List<T_Jiko_WorkFlow_Status> jikoWorkFlowStatus_list = await _accidentListRepository.GetTJikoWorkFlowStatusByApprovalUnprocessed();

                List<int> Jiko_WorkFlow_ID_list = jikoWorkFlowStatus_list.Select(x => x.Jiko_WorkFlow_ID).Distinct().ToList();

                foreach (int jikoWorkFlowID in Jiko_WorkFlow_ID_list)
                {
                    T_Jiko_WorkFlow_Route jikoWorkFlowRoute = await _accidentListRepository.GetTJikoWorkFlowRoute(jikoWorkFlowID);
                    if (!Group_ID_list.Contains(jikoWorkFlowRoute.Jiko_WorkFlow_Group_ID))
                        continue;

                    if (!Jiko_ID_list.Contains(jikoWorkFlowRoute.Jiko_ID))
                        Jiko_ID_list.Add(jikoWorkFlowRoute.Jiko_ID);
                }
            }
            else
            {
                // [全て]
                foreach (int groupID in Group_ID_list)
                {
                    List<T_Jiko_WorkFlow_Route> jikoWorkFlowRoute_list = await _accidentListRepository.GetTJikoWorkFlowRouteByGroupId(groupID);
                    foreach (T_Jiko_WorkFlow_Route jikoWorkFlowRouteLocal in jikoWorkFlowRoute_list)
                    {
                        if (!Jiko_ID_list.Contains(jikoWorkFlowRouteLocal.Jiko_ID))
                            Jiko_ID_list.Add(jikoWorkFlowRouteLocal.Jiko_ID);
                    }
                }
            }

            // ログイン者のGroup_ID＝T_Jiko_WorkFlow_Route．WorkFlow_Group_IDに該当する全てのデータ
            foreach (var jiko in jikoList)
            {

                // ワークフロールートに事故が含まれているか確認
                foreach (int jikoId in Jiko_ID_list)
                {
                    if (jiko.Jiko_ID == jikoId)
                    {
                        // テーブル		T_Jiko			カラム		Driver_ID					M_CompanyDriver．Driver_IDのDisplay_Name										
                        // テーブル		T_Jiko			カラム		SyaryoManagement_ID					M_SyaryoManagement．SyaryouManagement_IDのSyaban_Number										
                        M_CompanyDriver companyDriver = await _accidentListRepository.GetMCompanyDriver(displayName, jiko.Driver_ID);
                        if (!string.IsNullOrEmpty(displayName) && companyDriver == null)
                        {
                            break;
                        } 
                        // 車両管理IDと車番で車両情報を取得
                        M_SyaryoManagement syaryoManagement = await _accidentListRepository.GetMSyaryoManagement(syabanNumber, jiko.SyaryoManagement_ID);

                        if (!string.IsNullOrEmpty(syabanNumber) && syaryoManagement == null)
                        {
                            if(syabanNumber != "0") break;
                        }

                        // テーブル		T_Jiko			カラム		Jiko_No															
                        // テーブル		T_Jiko			カラム		Jiko_Date															
                        // テーブル		T_Jiko			カラム		Jiko_Kubun															
                        // テーブル		T_Jiko			カラム		Jiko_Display															
                        AccidentListModel.AccidentListItem newItem = new AccidentListModel.AccidentListItem
                        {
                            Jiko_ID = jiko.Jiko_ID,
                            Jiko_No = jiko.Jiko_No,
                            Jiko_Date = jiko.Jiko_Date,
                            Jiko_Kubun = jiko.Jiko_Kubun,
                            Jiko_Display = jiko.Jiko_Display,
                        };

                        // ドライバーと車両の詳細を取得してアイテムに設定
                        M_CompanyDriver companyD = await _accidentListRepository.GetMCompanyDriver(jiko.Driver_ID);
                        newItem.Display_Name = companyD?.Display_Name ?? "";
                        M_SyaryoManagement syaryoM = await _accidentListRepository.GetMSyaryoManagement(jiko.SyaryoManagement_ID);
                        newItem.Syaban_Number = syaryoM?.Syaban_Number ?? "";
                        // コードの名前を取得してアイテムに設定
                        M_Code_Datum codes = await _accidentListRepository.GetMCode(18, newItem.Jiko_Kubun);

                        newItem.Code_Name = codes.Code_Name;
                        // 新しいアイテムを結果リストに追加
                        jikoDataLists.Add(newItem);
                        break; // マッチするとループから出す
                    }
                }
            }

            return jikoDataLists;
        }

        /// <summary>
        /// 指定されたコードIDに基づいてM_Codeデータリストを取得します。
        /// </summary>
        /// <param name="codeId">取得するコードのID。</param>
        /// <returns>指定されたコードIDに基づいて取得した <see cref="List{M_Code_Datum}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、指定されたコードIDに基づいて M_Code データを非同期に取得します.
        /// 指定されたIDに関連する M_Code のデータリストを返します.
        /// </remarks> 
        public async Task<List<M_Code_Datum>> GetMCodeList(int codeId)
        {
            // M_Code：18に該当するデータを表示するようにお願いします
            return await _accidentListRepository.GetMCodeDataList(codeId);
        }
    }

    /// <summary>
    /// 事故リストサービスインターフェース
    /// </summary>
    public interface IAccidentListService
    {
        /// <summary>
        /// 指定された条件に基づいて事故リストを取得します。
        /// </summary>
        /// <param name="companyId">会社のID。</param>
        /// <param name="userId">ユーザーのID。</param>
        /// <param name="jikoKubun">事故区分。</param>
        /// <param name="loginUser">ログインユーザーかどうかを示すブール値。</param>
        /// <param name="jikoDisplay">事故の表示名（オプション）。</param>
        /// <param name="fromDate">検索開始日（オプション）。</param>
        /// <param name="toDate">検索終了日（オプション）。</param>
        /// <param name="displayName">表示名（オプション）。</param>
        /// <param name="syabanNumber">車輛番号（オプション）。</param>
        /// <returns>指定された条件に基づいて取得した <see cref="List{AccidentListModel.AccidentListItem}"/> のリスト。</returns>
        Task<List<AccidentListModel.AccidentListItem>> GetAccidentList(   
            int companyId,
            int userId,
            int jikoKubun, 
            bool loginUser,         
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            string displayName = "",
            string syabanNumber  = "");

        /// <summary>
        /// 指定されたコードIDに基づいてM_Codeデータリストを取得します。
        /// </summary>
        /// <param name="codeId">取得するコードのID。</param>
        /// <returns>指定されたコードIDに基づいて取得した <see cref="List{M_Code_Datum}"/> のリスト。</returns>
        Task<List<M_Code_Datum>> GetMCodeList(int codeId);
    }
}