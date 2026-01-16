using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 事故情報を管理するリポジトリクラス
    /// </summary>
    public class AccidentRepository : IAccidentRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションDBコンテキスト</param>
        /// <param name="contextKintai">勤怠DBコンテキスト</param>
        public AccidentRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// ドライバー情報を取得する
        /// </summary>
        /// <param name="driver_id">ドライバーID</param>
        /// <returns>driverInfo</returns>
        public async Task<Data.V_CompanyDriver> GetCompanyDriver(int driver_id)
            => driver_id > 0 ? await _context.V_CompanyDrivers.FirstOrDefaultAsync(d => d.Driver_ID == driver_id) : null;

        /// <summary>
        /// 車両管理情報を取得する
        /// </summary>
        /// <param name="syaryo_management_id">車両管理ID</param>
        /// <returns>車両管理情報</returns>
        public async Task<M_SyaryoManagement> Get_M_SyaryoManagement(int syaryo_management_id)
            => syaryo_management_id > 0 ? await _context.M_SyaryoManagements.FirstOrDefaultAsync(d => d.SyaryoManagement_ID == syaryo_management_id) : null;

        /// <summary>
        /// コードデータ情報の取得
        /// </summary>
        /// <param name="Code_ID">コードID</param>
        /// <returns>コードデータリスト</returns>
        public async Task<List<CodeDataDto>> GetGroupCodeData(int Code_ID)
        {
            List<CodeDataDto> CodeDataList = new List<CodeDataDto>
            {
                new CodeDataDto
                {
                    Code_Data = string.Empty,
                    Code_Name = string.Empty
                }
            };

            // データベースから取得したリストを追加
            List<CodeDataDto> databaseCodeData = await _context.M_Code_Data
                // 指定された Code_ID と一致し、削除フラグが false (または 0) のレコードをフィルタリング
                .Where(m => m.Code_ID == Code_ID && (m.Del_Flg ? 1 : 0) == 0)
                // フィルタリングされたデータを CodeDataDto 型に変換
                .Select(m => new CodeDataDto
                {
                    Code_Data = m.Code_Data,
                    Code_Name = m.Code_Name
                })
                .ToListAsync();

            // データベースから取得したリストを CodeDataList に追加
            CodeDataList.AddRange(databaseCodeData);

            return CodeDataList;
        }


        /// <summary>
        /// ワークフローリストの取得
        /// </summary>
        /// <returns>ワークフローリスト</returns>
        public async Task<List<CodeDataDto>> GetWorlFlowList()
        {
            List<CodeDataDto> WorkFlowList = new List<CodeDataDto>
            {
                new CodeDataDto
                {
                    Code_Data = string.Empty,
                    Code_Name = string.Empty
                }
            };

            // データベースから取得したリストを追加
            List<CodeDataDto> databaseWorkFlows = await _context.M_Jiko_WorkFlows
                .Where(m => m.Del_Flg == 0)
                .Select(m => new CodeDataDto
                {
                    Code_Data = m.Jiko_WorkFlow_Base_ID.ToString(),
                    Code_Name = m.Jiko_WorkFlow_Name
                })
                .ToListAsync();

            // データベースから取得したリストを WorkFlowList に追加
            WorkFlowList.AddRange(databaseWorkFlows);

            return WorkFlowList;
        }


        /// <summary>
        /// ユーザーグループ情報の取得
        /// </summary>
        /// <returns>ユーザーグループデータリスト</returns>
        public async Task<List<CodeDataDto>> GetUserGroupData()
        {
            List<CodeDataDto> CodeDataList = new List<CodeDataDto>
            {
                new CodeDataDto
                {
                    Code_Data = string.Empty,
                    Code_Name = string.Empty
                }
            };

            // データベースから取得したリストを追加（Group_Kubun が 1 で、削除フラグが false (または 0) で、ユーザーが存在するグループをフィルタリング）
            List<M_CompanyUser_Group> databaseGroupData = await _context.M_CompanyUser_Groups
                .Where(g => g.Group_Kubun == 1 && g.Del_Flg == false && _context.M_CompanyUser_GroupUsers.Any(gu => gu.Group_ID == g.Group_ID))
                .ToListAsync();

            // groupIdsのリストを作成
            List<int> groupIds = databaseGroupData.Select(g => g.Group_ID).ToList();

            // groupIdsに一致するユーザーを取得
            List<M_CompanyUser> users = await _context.M_CompanyUsers
                .Where(user => _context.M_CompanyUser_GroupUsers.Any(groupUser => groupUser.User_ID == user.User_ID && groupIds.AsQueryable().Contains(groupUser.Group_ID)))
                .ToListAsync();
            if (users != null)
            {
                CodeDataList = users.Select(user => new CodeDataDto {
                    Code_Data = user.User_ID.ToString(), 
                    Code_Name = user.Display_Name
                }).ToList(); 
            }

            // データベースから取得したリストを CodeDataList に追加
            return CodeDataList;
        }

        /// <summary>
        /// 事故情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>事故情報</returns>
        public async Task<T_Jiko> GetJiko(int Jiko_ID)
        {
            T_Jiko Jiko = await _context.T_Jikos
                .Where(j => j.Jiko_ID == Jiko_ID)
                .FirstOrDefaultAsync();

            return Jiko;
        }

        /// <summary>
        /// 事故アイテム情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>事故アイテムのリスト</returns>
        public async Task<List<JikoItem>> GetJikoItem(int Jiko_ID)
        {
            // M_Jiko_Items テーブルから全てのレコードを非同期で取得（トラッキングなし）
            List<M_Jiko_Item> mJikoItems = await _context.M_Jiko_Items
                .AsNoTracking()
                .ToListAsync();

            // T_Jiko_Items テーブルから指定された Jiko_ID に一致するレコードを非同期で取得（トラッキングなし）
            List<T_Jiko_Item> tJikoItems = await _context.T_Jiko_Items
                .AsNoTracking()
                .Where(j => j.Jiko_ID == Jiko_ID)
                .ToListAsync();

            // 結果を格納するためのリストを初期化
            List<JikoItem> result = new List<JikoItem>();

            // M_Jiko_Items の各アイテムに対して処理を実行
            foreach (var mItem in mJikoItems)
            {
                // T_Jiko_Items の中から対応するアイテムを検索
                T_Jiko_Item tItem = tJikoItems.FirstOrDefault(t => t.Jiko_Items_ID == mItem.Jiko_Items_ID);

                // 値を格納する変数を初期化
                string value = "";
                if (tItem != null)
                {
                    // mItem の Jiko_Items_Type に応じて適切な値を設定
                    switch (mItem.Jiko_Items_Type.ToLower())
                    {
                        case "string":
                            value = tItem.Jiko_Items_Val_String;
                            break;
                        case "int":
                            value = tItem.Jiko_Items_Val_Int?.ToString();
                            break;
                        case "double":
                            value = tItem.Jiko_Items_Val_Double?.ToString();
                            break;
                        case "money":
                            value = tItem.Jiko_Items_Val_Money?.ToString("#,###");
                            break;
                        case "datetime":
                            value = tItem.Jiko_Items_Val_Datetime?.ToString("yyyy-MM-dd");
                            break;
                    }
                }

                // JikoItem オブジェクトを作成し、リストに追加
                result.Add(new JikoItem
                {
                    Jiko_Items_Prop_Name = mItem.Jiko_Items_Prop_Name,
                    Jiko_Items_ID = mItem.Jiko_Items_ID,
                    Jiko_Items_Type = mItem.Jiko_Items_Type,
                    Jiko_Items_Val = value,
                    Jiko_ID = tItem?.Jiko_ID ?? Jiko_ID
                });
            }

            return result;
        }


        /// <summary>
        /// 事故タイプ情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>事故タイプのリスト</returns>
        public async Task<List<T_Jiko_Type>> GetJikoType(int Jiko_ID)
        {
            List<T_Jiko_Type> Jiko = await _context.T_Jiko_Types
                .Where(j => j.Jiko_ID == Jiko_ID)
                .ToListAsync();

            return Jiko;
        }

        /// <summary>
        /// ワークフローリストの取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>ワークフローリスト</returns>
        public async Task<List<WorkFlow>> GetJikoWorlFlowList(int Jiko_ID, int User_ID)
        {
            List<int> groupIds = await GetGroupUsersByGroupIdListsAsync(User_ID);

            T_Jiko_WorkFlow_Status workflowStatuses = await (from status in _context.T_Jiko_WorkFlow_Statuses.Where(status => status.Approval_User_ID == 0)
                                          join route in _context.T_Jiko_WorkFlow_Routes on new { status.Jiko_WorkFlow_ID } equals new { route.Jiko_WorkFlow_ID }
                                          where route.Jiko_ID.Equals(Jiko_ID)
                                          select status
                                        ).FirstOrDefaultAsync();
            int? current_Jiko_WorkFlow_ID = workflowStatuses?.Jiko_WorkFlow_ID ?? 0;

            // T_Jiko_WorkFlow_Routes テーブルから指定された Jiko_ID に一致するレコードを取得し、ソート
            List<WorkFlow> workFlows = await _context.T_Jiko_WorkFlow_Routes
                .Where(j => j.Jiko_ID == Jiko_ID) // 指定した Jiko_ID に基づいてルートをフィルタリング
                .OrderByDescending(j => j.Jiko_WorkFlow_Sort) // ワークフローの順序に基づいて降順に並べ替え
                .Select(route => new WorkFlow
                {
                    // WorkFlowRoute プロパティに現在のルートを設定
                    WorkFlowRoute = route,

                    // WorkFlowStatus プロパティに最新のステータスを設定
                    WorkFlowStatus = _context.T_Jiko_WorkFlow_Statuses
                    .OrderByDescending(status => status.Jiko_WorkFlow_Status_ID) // 最新のステータスを取得するために降順に並べ替え
                        .FirstOrDefault(status => status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID && (current_Jiko_WorkFlow_ID == 0 || status.Jiko_WorkFlow_ID <= current_Jiko_WorkFlow_ID)),

                    // WorkFlowFlg プロパティにフラグの有無を設定
                    WorkFlowFlg = _context.T_Jiko_WorkFlow_Statuses
                        .Any(status =>
                            status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID &&
                            (status.Approval_Kubun == 0 || status.Approval_Kubun == 3) && // 承認区分が 0 または 3 のステータスを対象
                            groupIds.AsQueryable().Contains(route.Jiko_WorkFlow_Group_ID)), // ログインユーザー所属のグループIDに含むかを確認

                    //Display_Name プロパティに表示名を設定
                    //1.処理中（status.Approval_User_ID == 0）個所がログインユーザーのGroupに所属するか確認
                    //2.処理済（status.Approval_User_ID != 0）個所のユーザー情報
                    //3.ステータスに関連付けられたユーザーとグループのグループ名
                    //4.ステータスに関連付けられたグループのグループ名
                    Display_Name =
                       _context.T_Jiko_WorkFlow_Statuses
                      .Where(status => status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID &&
                            (status.Approval_User_ID == 0) && // 処理中の箇所がログインユーザーのGroupに所属するか確認
                            groupIds.AsQueryable().Contains(route.Jiko_WorkFlow_Group_ID) // グループIDに一致するユーザーをフィルタリング
                       )
                      .Select(status => _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
                          .Where(user => user.User_ID == User_ID)
                          .Select(user => user.Display_Name)
                          .FirstOrDefault()
                      )
                      .FirstOrDefault() ?? // ステータスに関連付けられたユーザーが存在しない場合
                       _context.T_Jiko_WorkFlow_Statuses
                      .Where(status => status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID)
                      .OrderByDescending(j => j.Jiko_WorkFlow_Status_ID)　// 複数の場合は後の方のユーザー名が優先
                      .Select(status => _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
                          .Where(user => user.User_ID == status.Approval_User_ID && (current_Jiko_WorkFlow_ID == 0 || status.Jiko_WorkFlow_ID <= current_Jiko_WorkFlow_ID))
                          .Select(user => user.Display_Name)
                          .FirstOrDefault()
                      )
					  .FirstOrDefault() ?? // ステータスに関連付けられたユーザーが存在しない場合
					  _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
						  .Join(_context.M_CompanyUser_Groups.Where(e => e.Del_Flg == false),
								user => user.User_ID,
								group => group.Group_ID,
								(user, group) => new { user, group })
						  .Where(ug => ug.group.Group_ID == route.Jiko_WorkFlow_Group_ID) 
						  .Select(ug => ug.group.Display_Name)
						  .FirstOrDefault() ??
                      _context.M_CompanyUser_Groups.Where(e => e.Del_Flg == false)
                          .Where(ug => ug.Group_ID == route.Jiko_WorkFlow_Group_ID)
                          .Select(ug => ug.Display_Name)
                          .FirstOrDefault()
                })
                .ToListAsync();

            return workFlows;
        }

        /// <summary>
        /// グループユーザー情報の取得
        /// </summary>
        /// <param name="groupIds">グループIDリスト</param>
        /// <returns>グループユーザーリスト</returns>
        public async Task<List<M_CompanyUser_GroupUser>> GetGroupUsersByGroupIdsAsync(List<int> groupIds)
        {
            return await _context.M_CompanyUser_GroupUsers
                .Where(gu => groupIds.AsQueryable().Contains(gu.Group_ID))
                .ToListAsync();
        }

        /// <summary>
        /// グループIDの取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>グループID</returns>
        public async Task<int> GetGroupUsersByGroupIdsAsync(int User_ID)
        {
            return await _context.M_CompanyUser_GroupUsers.Where(e => e.Del_Flg == false)
                 .Where(c => c.User_ID == User_ID) // User_IDが合っている
                 .Select(c => c.Group_ID) // Group_IDだけを返却
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// ユーザーが所属するグループIDのリスト取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>グループIDリスト</returns>
        public async Task<List<int>> GetGroupUsersByGroupIdListsAsync(int User_ID)
        {
            IQueryable<int> query = (from uu in _context.M_CompanyUser_GroupUsers.Where(e => e.Del_Flg == false)
                         .Where(c => c.User_ID == User_ID) // User_IDが合っている
                         select uu.Group_ID
                        ).AsQueryable();

            List<int> userCompanyGroupIdList = await query.ToListAsync();
            return userCompanyGroupIdList;
        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>顧客情報</returns>
        public async Task<CustomerData> GetCustomerNameForJikoItem(int Jiko_ID)
        {
            // M_Jiko_Items テーブルから 'KokyakuId' というプロパティ名に一致する Jiko_Items_ID を非同期で取得
            int Jiko_Items_ID = await _context.M_Jiko_Items
            .Where(ji => ji.Jiko_Items_Prop_Name == "KokyakuId")
            .Select(ji => ji.Jiko_Items_ID)
           .FirstOrDefaultAsync();

            //  T_Jiko_Itemsから該当のレコードを取得
            T_Jiko_Item jikoItem = await _context.T_Jiko_Items
                .FirstOrDefaultAsync(ji => ji.Jiko_ID == Jiko_ID && ji.Jiko_Items_ID == Jiko_Items_ID);

            // jikoItem が null の場合、結果が見つからなかったことを示すために null を返す
            if (jikoItem == null || jikoItem?.Jiko_Items_Val_Int == null)
            {
                return null;
            }

            // 取得したJiko_Items_Val_IntをCustomer_IDとして使用
            int customerBranchId = jikoItem.Jiko_Items_Val_Int.Value;

            // M_Customer_Branches テーブルから、取得した customerBranchId に一致する Customer_ID を非同期で取得
            int customerId = await _context.M_Customer_Branches
                .Where(c => c.Customer_Branch_ID == customerBranchId)
                .Select(c => c.Customer_ID)
                .FirstOrDefaultAsync();

            // M_Customers テーブルから、取得した customerId に一致するレコードを非同期で取得
            M_Customer customer = await _context.M_Customers
                .FirstOrDefaultAsync(c => c.Customer_ID == customerId);

            return new CustomerData
            {
                // 顧客情報が見つかった場合は Customer_Name を設定し、見つからなかった場合は null になる
                Customer_Name = customer?.Customer_Name,
                // 顧客の支店 ID を設定
                Customer_ID = customerBranchId

            };
        }

        /// <summary>
        /// 事故ステータスの更新
        /// </summary>
        /// <param name="data">ステータス変更データ</param>
        /// <returns>更新結果</returns>
        public async Task<bool> ChangeStatus(ChangeStatus data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                T_Jiko jiko = await _context.T_Jikos
                    .FirstOrDefaultAsync(ji => ji.Jiko_ID == data.Jiko_ID);

                if (jiko != null)
                {
                    jiko.Jiko_Status = data.Jiko_Status;

                    // すべての変更をデータベースに保存
                    await _context.SaveChangesAsync();
                }


                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ChangeStatus:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        /// <summary>
        /// ワークフロールートの取得
        /// </summary>
        /// <param name="Jiko_WorkFlow_Base_ID">ワークフローベースID</param>
        /// <returns>ワークフロールートリスト</returns>
        public async Task<List<WorkFlowRoute>> GetWorkFlowRoute(int Jiko_WorkFlow_Base_ID)
        {
            // M_Jiko_WorkFlow_Routes テーブルから指定された Jiko_WorkFlow_Base_ID に一致するレコードを取得し、ソート
            List<WorkFlowRoute> routes = await _context.M_Jiko_WorkFlow_Routes
            .Where(route => route.Jiko_WorkFlow_Base_ID == Jiko_WorkFlow_Base_ID)
            .OrderBy(route => route.Jiko_WorkFlow_Sort)
            .Join(
                // M_CompanyUser_Groups テーブルとジョインし、グループ情報を取得
                _context.M_CompanyUser_Groups.Where(e => e.Del_Flg == false),
                route => route.Jiko_WorkFlow_Group_ID, // ルートのグループ ID と
                group => group.Group_ID,               // グループの ID を結びつける
                (route, group) => new WorkFlowRoute
                {
                    // WorkFlowRoute オブジェクトを作成し、ルート名と表示名を設定
                    Route_Name = route.Jiko_WorkFlow_Display,
                    Display_Name = group.Display_Name
                })
            .ToListAsync();

            return routes;
        }

        /// <summary>
        /// ワークフローログの取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>ワークフローログリスト</returns>
        public async Task<List<WorkFlowLog>> GetWorkFlowLog(int Jiko_ID, int User_ID)
        {
            List<int> groupIds = await GetGroupUsersByGroupIdListsAsync(User_ID);
            // T_Jiko_WorkFlow_Statuses テーブルから取得
            List<T_Jiko_WorkFlow_Status> workflowStatuses = await (from status in _context.T_Jiko_WorkFlow_Statuses
                                          join route in _context.T_Jiko_WorkFlow_Routes on new { status.Jiko_WorkFlow_ID } equals new { route.Jiko_WorkFlow_ID }
                                        where route.Jiko_ID.Equals(Jiko_ID)
                                        select status
                                        ).OrderBy(status => status.Jiko_WorkFlow_Status_ID)
                                        .AsQueryable().ToListAsync();
            // 結果を格納するためのリストを初期化
            List<WorkFlowLog> result = new List<WorkFlowLog>();
            // 各ルートについて処理を実行
            foreach (var status in workflowStatuses)
            {
                // T_Jiko_WorkFlow_Routes テーブルから、ルートに関連する最新のステータスを非同期で取得
                T_Jiko_WorkFlow_Route route = await _context.T_Jiko_WorkFlow_Routes
                    .Where(s => s.Jiko_WorkFlow_ID == status.Jiko_WorkFlow_ID) // ルートのワークフローIDに関連するステータスをフィルタリング
                    .FirstOrDefaultAsync(); // 最初の（最新の）ステータスを非同期で取得
                // ステータスが存在する場合
                if (status != null)
                {
                    // ログインユーザーか否か確認
                    bool isLoginUser = _context.T_Jiko_WorkFlow_Statuses
                        .Any(status =>
                            status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID &&
                            (status.Approval_User_ID == 0) &&
                            groupIds.AsQueryable().Contains(route.Jiko_WorkFlow_Group_ID)); // ログインユーザー所属のグループIDに含むかを確認

                    string Display_Name = "";
                    //Display_Name プロパティに表示名を設定
                    //1.処理中（status.Approval_User_ID == 0）個所がログインユーザーのGroupに所属するか確認
                    //2.処理済（status.Approval_User_ID != 0）個所のユーザー情報
                    //3.ステータスに関連付けられたユーザーとグループのグループ名
                    //4.ステータスに関連付けられたグループのグループ名
                    Display_Name =
                       _context.T_Jiko_WorkFlow_Routes
                      .Where(route => route.Jiko_WorkFlow_ID == status.Jiko_WorkFlow_ID &&
                            (status.Approval_User_ID == 0) && // 処理中の箇所がログインユーザーのGroupに所属するか確認
                            groupIds.AsQueryable().Contains(route.Jiko_WorkFlow_Group_ID) // グループIDに一致するユーザーをフィルタリング
                       )
                      .Select(status => _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
                          .Where(user => user.User_ID == User_ID)
                          .Select(user => user.Display_Name)
                          .FirstOrDefault()
                      )
                      .FirstOrDefault() ?? // ステータスに関連付けられたユーザーが存在しない場合
                       _context.T_Jiko_WorkFlow_Routes
                      .Where(route => route.Jiko_WorkFlow_ID == status.Jiko_WorkFlow_ID)
                      .Select(route => _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
                          .Where(user => user.User_ID == status.Approval_User_ID)
                          .Select(user => user.Display_Name)
                          .FirstOrDefault()
                      )
                      .FirstOrDefault() ?? // ステータスに関連付けられたユーザーが存在しない場合
                      _context.M_CompanyUsers.Where(e => e.Del_Flg == false)
                          .Join(_context.M_CompanyUser_Groups.Where(e => e.Del_Flg == false),
                                user => user.User_ID,
                                group => group.Group_ID,
                                (user, group) => new { user, group })
                          .Where(ug => ug.group.Group_ID == route.Jiko_WorkFlow_Group_ID)
                          .Select(ug => ug.group.Display_Name)
                          .FirstOrDefault() ??
                      _context.M_CompanyUser_Groups.Where(e => e.Del_Flg == false)
                          .Where(ug => ug.Group_ID == route.Jiko_WorkFlow_Group_ID)
                          .Select(ug => ug.Display_Name)
                          .FirstOrDefault();

                    // WorkFlowLog オブジェクトを作成し、リストに追加
                    result.Add(new WorkFlowLog
                    {
                        Display_Name = Display_Name ?? "", // ユーザーが存在する場合は表示名を設定、存在しない場合は空文字
                        Approval_Name = GetApprovalKubunString(status.Approval_Kubun, isLoginUser), // 承認区分を文字列に変換して設定
                        Approval_Datetime = status.Approval_Datetime  // ステータスの承認日時を設定
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// approvalKubunによって正しいstringを返す
        /// </summary>
        /// <param name="approvalKubun"></param>
        /// <returns></returns> 
        private string GetApprovalKubunString(int approvalKubun, bool isLoginUser = true)
        {
            switch (approvalKubun)
            {
                case 0:
					if (isLoginUser)
                        return "処理中";
                    return "未読";
                case 1: return "起案/承認";
                case 2: return "差戻し";
                case 3:
                    //return "差戻し未処理";
                    if (isLoginUser)
                        return "処理中";
                    return "未読";
                default: return "";
            }
        }

        /// <summary>
        /// 事故番号の更新
        /// </summary>
        /// <param name="Jiko_Date">事故日</param>
        /// <returns>新しい事故番号</returns>
        public async Task<int> PostJikoNo(DateTime Jiko_Date)
        {
            // Jiko_Date の年を取得
            int nendo = Jiko_Date.Year;

            // T_Jiko_No テーブルから NENDO が一致するレコードを取得
            T_Jiko_No jikoNoRecord = await _context.T_Jiko_Nos
                .FirstOrDefaultAsync(t => t.NENDO == nendo);

            int newNo;
            // jikoNoRecord が null でない場合（つまり、レコードが存在する場合）
            if (jikoNoRecord != null)
            {
                // レコードの NO フィールドの値を 1 増加させる
                jikoNoRecord.NO += 1;    
                // 新しい NO の値を設定
                newNo = jikoNoRecord.NO;
                // 更新されたレコードをデータベースに保存
                _context.T_Jiko_Nos.Update(jikoNoRecord);
            }
            else
            {
                // jikoNoRecord が null の場合（つまり、レコードが存在しない場合）
                // 新しい NO の値を 1 に設定
                newNo = 1;
                // 新しい T_Jiko_No レコードを作成
                jikoNoRecord = new T_Jiko_No
                {
                    NENDO = nendo,
                    NO = newNo
                };

                await _context.T_Jiko_Nos.AddAsync(jikoNoRecord);
            }

            await _context.SaveChangesAsync();

            return newNo;
        }

        /// <summary>
        /// 事故情報の登録または更新
        /// </summary>
        /// <param name="Jiko">事故情報</param>
        /// <param name="NO">事故番号</param>
        /// <returns>事故ID</returns>
        public async Task<int> PostJiko(T_Jiko Jiko, int NO)
        {
            if (Jiko.Jiko_ID == 0)
            {
                // 新規登録の場合

                // T_Jiko_No から NENDO と NO を取得
                int nendo = Jiko.Jiko_Date.Year;
                string jikoNo = $"Jiko{nendo}{NO:D6}";  // 例: "Jiko2024000001"


                // T_Jiko の新規登録
                T_Jiko newJiko = new T_Jiko
                {
                    Jiko_No = jikoNo,
                    Company_ID = Jiko.Company_ID,
                    Branch_ID = 0,
                    Jiko_WorkFlow_Base_ID = Jiko.Jiko_WorkFlow_Base_ID,
                    Jiko_WorkFlow_Status = Jiko.Jiko_WorkFlow_Status,
                    Jiko_Date = Jiko.Jiko_Date,
                    Driver_ID = Jiko.Driver_ID,
                    Haisya_Group_ID = Jiko.Haisya_Group_ID,
                    Jiko_Kubun = Jiko.Jiko_Kubun,
                    Jiko_Display = Jiko.Jiko_Display,
                    Jiko_Futan_Money = Jiko.Jiko_Futan_Money,
                    SyaryoManagement_ID = Jiko.SyaryoManagement_ID,
                    SyaryoManagement_ID1 = Jiko.SyaryoManagement_ID1,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = Jiko.Insert_User,
                    Update_Datetime = DateTime.Now,
                    Update_User = Jiko.Update_User
                };
                // 新しい T_Jiko レコードを非同期でデータベースに追加
                await _context.T_Jikos.AddAsync(newJiko);
                await _context.SaveChangesAsync();

                // 新しく作成された Jiko_ID を返す
                return newJiko.Jiko_ID;
            }
            else
            {
                // 既存のレコードを更新する場合
                T_Jiko existingJiko = await _context.T_Jikos.FindAsync(Jiko.Jiko_ID);

                if (existingJiko != null)
                {
                    existingJiko.Driver_ID = Jiko.Driver_ID;
                    existingJiko.Haisya_Group_ID = Jiko.Haisya_Group_ID;
                    existingJiko.Jiko_Kubun = Jiko.Jiko_Kubun;
                    existingJiko.Jiko_Status = Jiko.Jiko_Status;
                    existingJiko.Jiko_Date = Jiko.Jiko_Date;
                    existingJiko.Jiko_Display = Jiko.Jiko_Display;
                    existingJiko.SyaryoManagement_ID = Jiko.SyaryoManagement_ID;
                    existingJiko.SyaryoManagement_ID1 = Jiko.SyaryoManagement_ID1;
                    existingJiko.Jiko_Futan_Money = Jiko.Jiko_Futan_Money;
                    existingJiko.Update_Datetime = DateTime.Now;
                    existingJiko.Update_User = Jiko.Update_User;

                    _context.T_Jikos.Update(existingJiko);
                    await _context.SaveChangesAsync();

                    // 更新された Jiko_ID を返す
                    return existingJiko.Jiko_ID;
                }
                return 0;
            }
        }

        /// <summary>
        /// 事故アイテムの登録および保存
        /// </summary>
        /// <param name="JikoItemList">事故アイテムリスト</param>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostJikoItems(List<JikoItem> JikoItemList, int Jiko_ID)
        {
            foreach (var jikoItem in JikoItemList)
            {
                // Jiko_ID と Jiko_Items_ID に一致するレコードを検索
                T_Jiko_Item existingItem = await _context.T_Jiko_Items
                    .FirstOrDefaultAsync(t => t.Jiko_ID == Jiko_ID && t.Jiko_Items_ID == jikoItem.Jiko_Items_ID);

                if (existingItem != null)
                {
                    // 更新するプロパティを設定
                    existingItem.Jiko_ID = jikoItem.Jiko_ID;
                    existingItem.Jiko_Items_ID = jikoItem.Jiko_Items_ID;
                    // jikoItem のタイプが "string" の場合、既存の文字列値を更新
                    existingItem.Jiko_Items_Val_String = jikoItem.Jiko_Items_Type.ToLower() == "string" 
                        ? jikoItem.Jiko_Items_Val // jikoItem の値を設定
                        : existingItem.Jiko_Items_Val_String; // それ以外の場合は、既存の値を維持

                    // jikoItem のタイプが "int" の場合、値を整数に変換して設定
                    existingItem.Jiko_Items_Val_Int = jikoItem.Jiko_Items_Type.ToLower() == "int" 
                        && int.TryParse(jikoItem.Jiko_Items_Val, out int intValue) // jikoItem の値を整数に変換できるか確認
                        ? (int?)intValue // 成功した場合は変換した整数値を設定
                        : existingItem.Jiko_Items_Val_Int; // それ以外の場合は、既存の整数値を維持

                    // jikoItem のタイプが "double" の場合、値を倍精度浮動小数点数に変換して設定
                    existingItem.Jiko_Items_Val_Double = jikoItem.Jiko_Items_Type.ToLower() == "double" 
                        && double.TryParse(jikoItem.Jiko_Items_Val, out double doubleValue) // jikoItem の値を倍精度浮動小数点数に変換できるか確認
                        ? (double?)doubleValue // 成功した場合は変換した倍精度浮動小数点数値を設定
                        : existingItem.Jiko_Items_Val_Double; // それ以外の場合は、既存の倍精度浮動小数点数値を維持

                    // jikoItem のタイプが "money" の場合、値を通貨に変換して設定
                    existingItem.Jiko_Items_Val_Money = jikoItem.Jiko_Items_Type.ToLower() == "money" 
                        && decimal.TryParse(jikoItem.Jiko_Items_Val, out decimal moneyValue) // jikoItem の値を通貨（decimal型）に変換できるか確認
                        ? (decimal?)moneyValue // 成功した場合は変換した通貨値を設定
                        : existingItem.Jiko_Items_Val_Money; // それ以外の場合は、既存の通貨値を維持

                    // jikoItem のタイプが "datetime" の場合、値を DateTime に変換して設定
                    existingItem.Jiko_Items_Val_Datetime = jikoItem.Jiko_Items_Type.ToLower() == "datetime" 
                        && DateTime.TryParse(jikoItem.Jiko_Items_Val, out DateTime datetimeValue) // jikoItem の値を DateTime に変換できるか確認
                        ? (DateTime?)datetimeValue // 成功した場合は変換した DateTime 値を設定
                        : existingItem.Jiko_Items_Val_Datetime; // それ以外の場合は、既存の DateTime 値を維持

                    // エンティティをトラッキング状態にして更新
                    _context.T_Jiko_Items.Update(existingItem);
                }
                else
                {
                    // レコードが見つからなかった場合は新規登録
                    T_Jiko_Item newItem = new T_Jiko_Item
                    {

                        Jiko_ID = Jiko_ID,
                        Jiko_Items_ID = jikoItem.Jiko_Items_ID,
                        Jiko_Items_Val_String = jikoItem.Jiko_Items_Type.ToLower() == "string" 
                            ? jikoItem.Jiko_Items_Val // jikoItem のタイプが "string" の場合、値をそのまま設定
                            : null, // それ以外の場合は null を設定

                        Jiko_Items_Val_Int = jikoItem.Jiko_Items_Type.ToLower() == "int" 
                            && int.TryParse(jikoItem.Jiko_Items_Val, out int intValue) // jikoItem のタイプが "int" で、値が整数に変換できるか確認
                            ? (int?)intValue // 変換に成功した場合は、その整数値を設定
                            : null, // 変換に失敗した場合やタイプが "int" でない場合は null を設定

                        Jiko_Items_Val_Double = jikoItem.Jiko_Items_Type.ToLower() == "double" 
                            && double.TryParse(jikoItem.Jiko_Items_Val, out double doubleValue) // jikoItem のタイプが "double" で、値が倍精度浮動小数点数に変換できるか確認
                            ? (double?)doubleValue // 変換に成功した場合は、その倍精度浮動小数点数値を設定
                            : null, // 変換に失敗した場合やタイプが "double" でない場合は null を設定

                        Jiko_Items_Val_Money = jikoItem.Jiko_Items_Type.ToLower() == "money" 
                            && decimal.TryParse(jikoItem.Jiko_Items_Val, out decimal moneyValue) // jikoItem のタイプが "money" で、値が通貨（decimal型）に変換できるか確認
                            ? (decimal?)moneyValue // 変換に成功した場合は、その通貨値を設定
                            : null, // 変換に失敗した場合やタイプが "money" でない場合は null を設定

                        Jiko_Items_Val_Datetime = jikoItem.Jiko_Items_Type.ToLower() == "datetime" 
                            && DateTime.TryParse(jikoItem.Jiko_Items_Val, out DateTime datetimeValue) // jikoItem のタイプが "datetime" で、値が DateTime に変換できるか確認
                            ? (DateTime?)datetimeValue // 変換に成功した場合は、その DateTime 値を設定
                            : null // 変換に失敗した場合やタイプが "datetime" でない場合は null を設定
                    };

                    await _context.T_Jiko_Items.AddAsync(newItem);
                }
            }

            // 変更をデータベースに保存
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// ワークフローの登録および保存
        /// </summary>
        /// <param name="Jiko_WorkFlow_Base_ID">ワークフローベースID</param>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostJikoWorkFlow(int Jiko_WorkFlow_Base_ID, int Jiko_ID, int User_ID)
        {
            // ルート情報を取得
            List<M_Jiko_WorkFlow_Route> routes = await _context.M_Jiko_WorkFlow_Routes
                .Where(route => route.Jiko_WorkFlow_Base_ID == Jiko_WorkFlow_Base_ID)
                .OrderBy(route => route.Jiko_WorkFlow_Sort)
                .ToListAsync();

            // 最初に登録する WorkFlow の ID を保持する変数
            int firstWorkFlowID = 0;

            for (int i = 0; i < routes.Count; i++)
            {
                M_Jiko_WorkFlow_Route route = routes[i];

                // T_Jiko_WorkFlow_Route への新規登録
                T_Jiko_WorkFlow_Route newWorkFlowRoute = new T_Jiko_WorkFlow_Route
                {
                    Jiko_ID = Jiko_ID,
                    Jiko_WorkFlow_Sort = route.Jiko_WorkFlow_Sort,
                    Jiko_WorkFlow_Display = route.Jiko_WorkFlow_Display,
                    Jiko_WorkFlow_Group_ID = route.Jiko_WorkFlow_Group_ID,
                };

                _context.T_Jiko_WorkFlow_Routes.Add(newWorkFlowRoute);
                await _context.SaveChangesAsync();

                if (i == 0)
                {
                    // 最初に登録した WorkFlow の ID を保持
                    firstWorkFlowID = newWorkFlowRoute.Jiko_WorkFlow_ID;

                    // T_Jiko_WorkFlow_Status への最初の新規登録
                    T_Jiko_WorkFlow_Status initialStatus = new T_Jiko_WorkFlow_Status
                    {
                        Jiko_WorkFlow_ID = firstWorkFlowID,
                        Approval_Kubun = 1,
                        Approval_Datetime = DateTime.Now,
                        Approval_User_ID = User_ID,
                        Reject_Reason = null,
                    };

                    _context.T_Jiko_WorkFlow_Statuses.Add(initialStatus);
                    await _context.SaveChangesAsync();
                }
                else if (i == 1)
                {
                    // 二番目に登録する WorkFlow の ID を保持
                    int secondWorkFlowID = newWorkFlowRoute.Jiko_WorkFlow_ID;

                    // T_Jiko_WorkFlow_Status への二番目の新規登録
                    T_Jiko_WorkFlow_Status secondStatus = new T_Jiko_WorkFlow_Status
                    {
                        Jiko_WorkFlow_ID = secondWorkFlowID,
                        Approval_Kubun = 0,
                        Approval_Datetime = null,
                        Approval_User_ID = 0,
                        Reject_Reason = null,
                    };

                    _context.T_Jiko_WorkFlow_Statuses.Add(secondStatus);
                    await _context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// 事故タイプの登録および保存
        /// </summary>
        /// <param name="JikoType">事故タイプリスト</param>
        /// <param name="Jiko_ID">事故ID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostJikoType(List<T_Jiko_Type> JikoType, int Jiko_ID)
        {
            // 指定された Jiko_ID に基づいて既存の Jiko_Types をデータベースから非同期で取得
            List<T_Jiko_Type> existingJikoTypes = await _context.T_Jiko_Types
                .Where(j => j.Jiko_ID == Jiko_ID)
                .ToListAsync();

            // 既存の Jiko_Types が存在する場合
            if (existingJikoTypes.Any())
            {
                // 既存の Jiko_Types を削除
                _context.T_Jiko_Types.RemoveRange(existingJikoTypes);
                // 削除内容をデータベースに保存
                await _context.SaveChangesAsync();
            }
            // 新しい Jiko_Types を追加
            foreach (var jikoType in JikoType)
            {
                // 新しい T_Jiko_Type オブジェクトを作成し、プロパティに値を設定
                T_Jiko_Type newJikoType = new T_Jiko_Type
                {
                    Jiko_ID = Jiko_ID, // 指定された Jiko_ID を設定
                    Jiko_Type_ID = jikoType.Jiko_Type_ID, // 新しい Jiko_Type_ID を設定
                    Jiko_Type_Remarks = jikoType.Jiko_Type_Remarks, // 新しい Jiko_Type_Remarks を設定
                };
                // 新しい Jiko_Type をデータベースコンテキストに追加
                _context.T_Jiko_Types.Add(newJikoType);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 差し戻し情報の取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">ワークフローステータスID</param>
        /// <returns>差し戻し情報リスト</returns>
        public async Task<List<RemandModel>> GetRemandData(int User_ID, int Jiko_WorkFlow_Status_ID)
        {
            // 結果を格納するためのリストを初期化
            List<RemandModel> result = new List<RemandModel>();

            // 現在のワークフロー状態 ID に基づいて、関連するワークフロー ID を非同期で取得
            int currentWorkflowId = await _context.T_Jiko_WorkFlow_Statuses
                .Where(status => status.Jiko_WorkFlow_Status_ID == Jiko_WorkFlow_Status_ID)
                .Select(status => status.Jiko_WorkFlow_ID)
                .FirstOrDefaultAsync();

            // 現在のワークフロー ID が 0 の場合、結果が見つからなかったことを示すために空のリストを返す
            if (currentWorkflowId == 0)
            {
                return result;
            }

            // 現在のワークフロー ID に基づいて関連する Jiko_ID を非同期で取得
            int jikoId = await _context.T_Jiko_WorkFlow_Routes
                .Where(route => route.Jiko_WorkFlow_ID == currentWorkflowId)
                .Select(route => route.Jiko_ID)
                .FirstOrDefaultAsync();

            // Jiko_ID に基づいて関連するルートを取得し、ソート
            List<T_Jiko_WorkFlow_Route> relevantRoutes = await _context.T_Jiko_WorkFlow_Routes
                .Where(route => route.Jiko_ID == jikoId)
                .OrderBy(route => route.Jiko_WorkFlow_Sort)
                .ToListAsync();

            // 現在のワークフロー ID に一致するルートを取得
            T_Jiko_WorkFlow_Route currentRoute = relevantRoutes.FirstOrDefault(r => r.Jiko_WorkFlow_ID == currentWorkflowId);
            if (currentRoute == null)
            {
                // 現在のルートが見つからなかった場合、結果が見つからなかったことを示すために空のリストを返す
                return result;
            }

            // 現在のルートよりも前のルートをフィルタリング
            List<T_Jiko_WorkFlow_Route> previousRoutes = relevantRoutes
                .Where(route => route.Jiko_WorkFlow_Sort < currentRoute.Jiko_WorkFlow_Sort)
                .ToList();

            // 前のルートについて処理を実行
            foreach (var route in previousRoutes)
            {
                // 各ルートに関連する最新のワークフロー状態を非同期で取得
                T_Jiko_WorkFlow_Status latestStatus = await _context.T_Jiko_WorkFlow_Statuses
                    .Where(status => status.Jiko_WorkFlow_ID == route.Jiko_WorkFlow_ID)
                    .OrderByDescending(status => status.Jiko_WorkFlow_Status_ID)
                    .FirstOrDefaultAsync();
                // 最新のワークフロー状態が存在する場合
                if (latestStatus != null)
                {
                    // M_CompanyUsers テーブルから、承認ユーザーの表示名を非同期で取得
                    string user = await _context.M_CompanyUsers
                        .Where(u => u.User_ID == latestStatus.Approval_User_ID)
                        .Select(u => u.Display_Name)
                        .FirstOrDefaultAsync();
                    // RemandModel オブジェクトを作成し、リストに追加
                    result.Add(new RemandModel
                    {
                        Route_Name = route.Jiko_WorkFlow_Display,
                        Display_Name = user ?? string.Empty,
                        Jiko_WorkFlow_ID = latestStatus.Jiko_WorkFlow_ID
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 事故情報の登録および更新
        /// </summary>
        /// <param name="PostAccident">事故情報</param>
        /// <returns>事故ID</returns>
        public async Task<int> PostAccident(PostAccident PostAccident)
        {
            // データベーストランザクションの開始
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int NO = await PostJikoNo(PostAccident.Jiko.Jiko_Date);

                int Jiko_ID = await PostJiko(PostAccident.Jiko, NO);

                await PostJikoItems(PostAccident.JikoItemList, Jiko_ID);

                if (PostAccident.Jiko.Jiko_ID == 0)
                {
                    await PostJikoWorkFlow(PostAccident.Jiko.Jiko_WorkFlow_Base_ID, Jiko_ID, PostAccident.User_ID);
                }

                await PostJikoType(PostAccident.JikoTypeList, Jiko_ID);

                // 差戻し(Approval_Kubun = 3)のデータ有無 
                T_Jiko_WorkFlow_Status workflowStatuses = await (from status in _context.T_Jiko_WorkFlow_Statuses.Where(status => status.Approval_Kubun == 3)
                                              join route in _context.T_Jiko_WorkFlow_Routes on new { status.Jiko_WorkFlow_ID } equals new { route.Jiko_WorkFlow_ID }
                                              where route.Jiko_ID.Equals(Jiko_ID)
                                              select status
                                            ).FirstOrDefaultAsync();
				if (workflowStatuses?.Jiko_WorkFlow_ID>0)
				{
                    // 差戻しデータの変更
                    workflowStatuses.Approval_Kubun = 1;
                    workflowStatuses.Approval_Datetime = DateTime.Now;
                    workflowStatuses.Approval_User_ID = PostAccident.User_ID;

                    // 次のワークフロー作成
                    T_Jiko_WorkFlow_Route JikoWorkFlowRoutes = await _context.T_Jiko_WorkFlow_Routes
                        .Where(routes => routes.Jiko_WorkFlow_ID == workflowStatuses.Jiko_WorkFlow_ID)
                        .FirstOrDefaultAsync();
                    T_Jiko_WorkFlow_Route JikoWorkFlowRoutesNext = await _context.T_Jiko_WorkFlow_Routes
                        .Where(routes => routes.Jiko_ID == JikoWorkFlowRoutes.Jiko_ID && routes.Jiko_WorkFlow_Sort == (JikoWorkFlowRoutes.Jiko_WorkFlow_Sort + 1))
                        .FirstOrDefaultAsync();
                    if (JikoWorkFlowRoutesNext != null)
                    {
                        // 新しいレコードを作成
                        T_Jiko_WorkFlow_Status newStatus = new T_Jiko_WorkFlow_Status
                        {
                            Jiko_WorkFlow_ID = JikoWorkFlowRoutesNext.Jiko_WorkFlow_ID,
                            Approval_Kubun = 0,
                            Approval_Datetime = null,
                            Approval_User_ID = 0,
                            Reject_Reason = null
                        };
                        // 新しいレコードを追加
                        await _context.T_Jiko_WorkFlow_Statuses.AddAsync(newStatus);
                    }
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
                return Jiko_ID;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PostAccident:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return 0;
            }
        }

        /// <summary>
        /// 差し戻しの登録および保存
        /// </summary>
        /// <param name="Jiko_WorkFlow_ID">ワークフローID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">ワークフローステータスID</param>
        /// <param name="Reject_Reason">却下理由</param>
        /// <returns>非同期タスク</returns>
        public async Task PostRemand(int Jiko_WorkFlow_ID, int User_ID, int Jiko_WorkFlow_Status_ID, string Reject_Reason)
        {
            // 現在のワークフロー状態 ID に基づいて、関連するワークフロー状態を非同期で取得
            T_Jiko_WorkFlow_Status jikoWorkFlowStatus = await _context.T_Jiko_WorkFlow_Statuses
                    .Where(status => status.Jiko_WorkFlow_Status_ID == Jiko_WorkFlow_Status_ID)
                    .FirstOrDefaultAsync();

            // ワークフロー状態が見つからなかった場合、処理を終了
            if (jikoWorkFlowStatus == null)
            {
                return;
            }
            // 現在のワークフロー状態を却下として更新
            jikoWorkFlowStatus.Approval_Kubun = 2;
            jikoWorkFlowStatus.Approval_Datetime = DateTime.Now;
            jikoWorkFlowStatus.Approval_User_ID = User_ID;
            jikoWorkFlowStatus.Reject_Reason = Reject_Reason;

            // 新しいレコードを作成
            T_Jiko_WorkFlow_Status newStatus = new T_Jiko_WorkFlow_Status
            {
                Jiko_WorkFlow_ID = Jiko_WorkFlow_ID,
                Approval_Kubun = 3,
                Approval_Datetime = null,
                Approval_User_ID = 0,
                Reject_Reason = null
            };

            // 新しいレコードを追加
            await _context.T_Jiko_WorkFlow_Statuses.AddAsync(newStatus);

            // 変更をデータベースに保存
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// 承認の登録および保存
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">ワークフローステータスID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostApproval(int User_ID, int Jiko_WorkFlow_Status_ID)
        {
            // 指定された Jiko_WorkFlow_Status_ID に基づいて、対応するワークフローステータスをデータベースから非同期で取得
            T_Jiko_WorkFlow_Status jikoWorkFlowStatus = await _context.T_Jiko_WorkFlow_Statuses
                .Where(status => status.Jiko_WorkFlow_Status_ID == Jiko_WorkFlow_Status_ID)
                .FirstOrDefaultAsync();
            // 指定された ID のワークフローステータスが存在しない場合、処理を終了
            if (jikoWorkFlowStatus == null)
            {
                return;
            }
            // ステータスの承認区分を "承認済み" に設定
            jikoWorkFlowStatus.Approval_Kubun = 1;
            // 承認日時を現在の日時に設定
            jikoWorkFlowStatus.Approval_Datetime = DateTime.Now;
            // 承認ユーザーIDを指定されたユーザーIDに設定
            jikoWorkFlowStatus.Approval_User_ID = User_ID;
            // 拒否理由を null に設定（承認の場合は不要）
            jikoWorkFlowStatus.Reject_Reason = null;

            // 次のワークフロー作成
            T_Jiko_WorkFlow_Route JikoWorkFlowRoutes = await _context.T_Jiko_WorkFlow_Routes
                .Where(routes => routes.Jiko_WorkFlow_ID == jikoWorkFlowStatus.Jiko_WorkFlow_ID)
                .FirstOrDefaultAsync();
            T_Jiko_WorkFlow_Route JikoWorkFlowRoutesNext = await _context.T_Jiko_WorkFlow_Routes
                .Where(routes => routes.Jiko_ID == JikoWorkFlowRoutes.Jiko_ID && routes.Jiko_WorkFlow_Sort == (JikoWorkFlowRoutes.Jiko_WorkFlow_Sort + 1))
                .FirstOrDefaultAsync();
			if (JikoWorkFlowRoutesNext != null)
			{
                // 新しいレコードを作成
                T_Jiko_WorkFlow_Status newStatus = new T_Jiko_WorkFlow_Status
                {
                    // 新しいワークフローIDを設定
                    Jiko_WorkFlow_ID = JikoWorkFlowRoutesNext.Jiko_WorkFlow_ID,
                    Approval_Kubun = 0,
                    Approval_Datetime = null,
                    Approval_User_ID = 0,
                    Reject_Reason = null
                };
                // 新しいレコードを追加
                await _context.T_Jiko_WorkFlow_Statuses.AddAsync(newStatus);
            }

            // 変更をデータベースに保存
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 事故リポジトリインターフェース
    /// </summary>
    public interface IAccidentRepository
    {
        Task<Data.V_CompanyDriver> GetCompanyDriver(int driver_id);
        Task<M_SyaryoManagement> Get_M_SyaryoManagement(int syaryo_management_id);

        Task<List<CodeDataDto>> GetGroupCodeData(int Code_ID);
        Task<List<CodeDataDto>> GetWorlFlowList();
        Task<List<CodeDataDto>> GetUserGroupData();
        Task<T_Jiko> GetJiko(int Jiko_ID);
        Task<List<JikoItem>> GetJikoItem(int Jiko_ID);
        Task<List<T_Jiko_Type>> GetJikoType(int Jiko_ID);
        Task<List<WorkFlow>> GetJikoWorlFlowList(int Jiko_ID, int User_ID);
        Task<List<M_CompanyUser_GroupUser>> GetGroupUsersByGroupIdsAsync(List<int> groupIds);
        Task<int> GetGroupUsersByGroupIdsAsync(int User_ID);
        Task<List<int>> GetGroupUsersByGroupIdListsAsync(int User_ID);
        Task<CustomerData> GetCustomerNameForJikoItem(int Jiko_ID);
        Task<List<WorkFlowRoute>> GetWorkFlowRoute(int Jiko_WorkFlow_Base_ID);
        Task<List<WorkFlowLog>> GetWorkFlowLog(int Jiko_ID, int User_ID);
        Task<bool> ChangeStatus(ChangeStatus data);
        Task<int> PostJikoNo(DateTime Jiko_Date);
        Task<int> PostJiko(T_Jiko Jiko, int NO);
        Task PostJikoItems(List<JikoItem> JikoItemList, int Jiko_ID);
        Task PostJikoWorkFlow(int Jiko_WorkFlow_Base_ID, int Jiko_ID, int User_ID);
        Task PostJikoType(List<T_Jiko_Type> JikoType, int Jiko_ID);
        Task<List<RemandModel>> GetRemandData(int User_ID, int Jiko_WorkFlow_Status_ID);
        Task<int> PostAccident(PostAccident PostAccident);
        Task PostRemand(int Jiko_WorkFlow_ID, int User_ID, int Jiko_WorkFlow_Status_ID, string Reject_Reason);
        Task PostApproval(int User_ID, int Jiko_WorkFlow_Status_ID);
    }
}