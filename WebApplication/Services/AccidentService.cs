using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 事故サービスクラス
    /// </summary>
    public class AccidentService : IAccidentService
    {
        private readonly IAccidentRepository _accidentRepository;

        public AccidentService(IAccidentRepository accidentRepository)
        {
            _accidentRepository = accidentRepository;
        }

        /// <summary>
        /// 事故情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ログインユーザーID</param>
        /// <returns>AccidentModel</returns>
        public async Task<AccidentModel> GetAccident(int Jiko_ID, int User_ID)
        {
            // ユーザーのグループ ID を非同期で取得
            List<int> loggedInUserGroupIdList = await _accidentRepository.GetGroupUsersByGroupIdListsAsync(User_ID);
            // グループコードデータを非同期で取得、存在しない場合は空のリストを使用
            List<CodeDataDto> WeatherKubun = await _accidentRepository.GetGroupCodeData(14) ?? new List<CodeDataDto>();
            List<CodeDataDto> WorkKubun = await _accidentRepository.GetGroupCodeData(15) ?? new List<CodeDataDto>();
            List<CodeDataDto> ListKubun = await _accidentRepository.GetGroupCodeData(16) ?? new List<CodeDataDto>();
            List<CodeDataDto> JikoTypeKubun = await _accidentRepository.GetGroupCodeData(17) ?? new List<CodeDataDto>();
            List<CodeDataDto> JikoKubun = await _accidentRepository.GetGroupCodeData(18) ?? new List<CodeDataDto>();

            // ワークフローリストを非同期で取得、存在しない場合は空のリストを使用
            List<CodeDataDto> WorkFlowList = await _accidentRepository.GetWorlFlowList() ?? new List<CodeDataDto>();
            // ユーザーグループデータを非同期で取得、存在しない場合は空のリストを使用
            List<CodeDataDto> UserGroupData = await _accidentRepository.GetUserGroupData() ?? new List<CodeDataDto>();
            // 事故データを非同期で取得、存在しない場合は新しい T_Jiko オブジェクトを使用
            T_Jiko Jiko = await _accidentRepository.GetJiko(Jiko_ID) ?? new T_Jiko();
            // 事故アイテムリストを非同期で取得、存在しない場合は空のリストを使用
            List<JikoItem> JikoItemList = await _accidentRepository.GetJikoItem(Jiko_ID) ?? new List<JikoItem>();
            // 事故タイプリストを非同期で取得、存在しない場合は空のリストを使用
            List<T_Jiko_Type> JikoTypeList = await _accidentRepository.GetJikoType(Jiko_ID) ?? new List<T_Jiko_Type>();
            // 事故ワークフローリストを非同期で取得、存在しない場合は空のリストを使用
            List<WorkFlow> JikoWorkFlowList = await _accidentRepository.GetJikoWorlFlowList(Jiko_ID, User_ID) ?? new List<WorkFlow>();
            // 顧客データを非同期で取得、存在しない場合は新しい CustomerData オブジェクトを使用
            CustomerData CustomerData = await _accidentRepository.GetCustomerNameForJikoItem(Jiko_ID) ?? new CustomerData();

            // 初期値：承認ボタン、差戻しボタン押下不可
            bool isDisabled = true;
            bool ApprovalIsDisabled = true;

            // 最新の処理中([Approval_User_ID]が0のデータ)データ
            int? Jiko_WorkFlow_Status_ID = 0;
            WorkFlow latestApprovalProcessing = JikoWorkFlowList
            .Where(w => w.WorkFlowStatus != null && w.WorkFlowStatus.Approval_User_ID == 0)
            .FirstOrDefault();
            Jiko_WorkFlow_Status_ID = latestApprovalProcessing?.WorkFlowStatus?.Jiko_WorkFlow_Status_ID ?? 0;

            // 最新の承認データがログイン者のグループであるか判定
            WorkFlow latestApproval = JikoWorkFlowList
            .Where(w => w.WorkFlowStatus != null && w.WorkFlowStatus.Approval_User_ID == 0 && loggedInUserGroupIdList.AsQueryable().Contains(w.WorkFlowRoute.Jiko_WorkFlow_Group_ID))
            .FirstOrDefault();

            // 最新の承認データに対してグループ所属してなければ押下不可
            if (latestApproval != null)
            {
                isDisabled = false;
                ApprovalIsDisabled = false;
            }

            // 差し戻しから差し戻し未処理のWorkFlowStatusのオブジェクトをnullにする
            int startIndex = JikoWorkFlowList.FindIndex(w => w.WorkFlowStatus?.Approval_Kubun == 2);
            int endIndex = JikoWorkFlowList.FindIndex(w => w.WorkFlowStatus?.Approval_Kubun == 3);

            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
            {
                // 2 から 3 の間のオブジェクトの WorkFlowStatus を null にする
                for (int i = startIndex; i < endIndex; i++)
                {
                    JikoWorkFlowList[i].WorkFlowStatus = null;
                }
            }
            return new AccidentModel
            {
                WeatherKubun = WeatherKubun,
                WorkKubun = WorkKubun,
                ListKubun = ListKubun,
                JikoTypeKubun = JikoTypeKubun,
                JikoKubun = JikoKubun,
                WorkFlowList = WorkFlowList,
                UserGroupData = UserGroupData,
                Jiko = Jiko,
                JikoItemList = JikoItemList,
                JikoTypeList = JikoTypeList,
                JikoWorkFlowList = JikoWorkFlowList,
                IsDisabled = isDisabled,
                ApprovalIsDisabled = ApprovalIsDisabled,
                CustomerData = CustomerData,
                JikoWorkFlowStatusID = Jiko_WorkFlow_Status_ID,
                Driver_Display_Name = (await _accidentRepository.GetCompanyDriver(Jiko.Driver_ID))?.Display_Name,
                SyaryoManagement_Number = (await _accidentRepository.Get_M_SyaryoManagement(Jiko.SyaryoManagement_ID))?.Syaban_Number,
                SyaryoManagement_Number1 = (await _accidentRepository.Get_M_SyaryoManagement(Jiko.SyaryoManagement_ID1))?.Syaban_Number,
            };
        }

        /// <summary>
        /// 事故ステータスの更新
        /// </summary>
        /// <param name="data">ステータス変更データ</param>
        /// <returns>更新結果</returns>
        public async Task<bool> ChangeStatus(ChangeStatus data)
        {
            bool result = await _accidentRepository.ChangeStatus(data);
            return result;
        }

        /// <summary>
        /// ワークフロー情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="Jiko_WorkFlow_Base_ID">ワークフローベースID</param>
        /// <param name="User_ID">ログインユーザーID</param>
        /// <returns>WorkFlowList</returns>
        public async Task<WorkFlowList> GetWorkflowList(int Jiko_ID, int Jiko_WorkFlow_Base_ID, int User_ID)
        {
            List<WorkFlowRoute> WorkFlowRouteList = await _accidentRepository.GetWorkFlowRoute(Jiko_WorkFlow_Base_ID) ?? new List<WorkFlowRoute>();
            List<WorkFlowLog> WorkFlowLogList = await _accidentRepository.GetWorkFlowLog(Jiko_ID, User_ID) ?? new List<WorkFlowLog>();
             return new WorkFlowList
            {
                WorkFlowRouteList = WorkFlowRouteList,
                WorkFlowLogList = WorkFlowLogList
            };
        }

        /// <summary>
        /// 差し戻し情報の取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">事故ワークフローステータスID</param>
        /// <returns>RemandDataModel</returns>
        public async Task<RemandDataModel> GetRemandData(int User_ID, int Jiko_WorkFlow_Status_ID)
        {
            List<RemandModel> RemandListModel = await _accidentRepository.GetRemandData(User_ID, Jiko_WorkFlow_Status_ID) ?? new List<RemandModel>();

            return new RemandDataModel
            {
                RemandListModel = RemandListModel,
            };
        }

        /// <summary>
        /// 事故情報の登録および更新
        /// </summary>
        /// <param name="PostAccident">事故情報</param>
        /// <returns>Jiko_ID</returns>
        public async Task<int> PostAccident(PostAccident PostAccident)
        {
            int Jiko_ID = await _accidentRepository.PostAccident(PostAccident);
            return Jiko_ID;
        }

        /// <summary>
        /// 差し戻し情報の登録および更新
        /// </summary>
        /// <param name="data">差し戻しデータ</param>
        /// <returns></returns>
        public async Task PostRemand(PostRemand data)
        {
            await _accidentRepository.PostRemand(data.Jiko_WorkFlow_ID, data.User_ID, data.Jiko_WorkFlow_Status_ID, data.Reject_Reason);
        }

        /// <summary>
        /// 承認情報の登録および更新
        /// </summary>
        /// <param name="data">承認データ</param>
        /// <returns></returns>
        public async Task PostApproval(PostRemand data)
        {
            await _accidentRepository.PostApproval(data.User_ID, data.Jiko_WorkFlow_Status_ID);
        }
    }

    /// <summary>
    /// 事故サービスインターフェース
    /// </summary>
    public interface IAccidentService
    {
        /// <summary>
        /// 事故情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ログインユーザーID</param>
        /// <returns>AccidentModel</returns>
        Task<AccidentModel> GetAccident(int Jiko_ID, int User_ID);

        /// <summary>
        /// 事故ステータスの更新
        /// </summary>
        /// <param name="data">ステータス変更データ</param>
        /// <returns>更新結果</returns>
        Task<bool> ChangeStatus(ChangeStatus data);

        /// <summary>
        /// ワークフロリスト情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="Jiko_WorkFlow_Base_ID">ワークフローベースID</param>
        /// <param name="User_ID">ログインユーザーID</param>
        /// <returns>WorkFlowList</returns>
        Task<WorkFlowList> GetWorkflowList(int Jiko_ID, int Jiko_WorkFlow_Base_ID, int User_ID);

        /// <summary>
        /// 事故情報の登録および更新
        /// </summary>
        /// <param name="PostAccident">事故情報</param>
        /// <returns>Jiko_ID</returns>
        Task<int> PostAccident(PostAccident PostAccident);

        /// <summary>
        /// 差し戻し情報の取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">事故ワークフローステータスID</param>
        /// <returns>RemandDataModel</returns>
        Task<RemandDataModel> GetRemandData(int User_ID, int Jiko_WorkFlow_Status_ID);

        /// <summary>
        /// 差し戻し情報の登録および更新
        /// </summary>
        /// <param name="data">差し戻しデータ</param>
        /// <returns></returns>
        Task PostRemand(PostRemand data);

        /// <summary>
        /// 承認情報の登録および更新
        /// </summary>
        /// <param name="data">承認データ</param>
        /// <returns></returns>
        Task PostApproval(PostRemand data);
    }
}