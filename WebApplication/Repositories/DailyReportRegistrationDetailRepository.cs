using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    /// 日報登録詳細データを管理するリポジトリクラスです。
    /// </summary>
    public class DailyReportRegistrationDetailRepository : IDailyReportRegistrationDetailRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        public DailyReportRegistrationDetailRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 指定されたAnken_IDに基づいてT_Ankenを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値</param>
        /// <returns>T_Ankenのインスタンス</returns>
        public async Task<T_Anken> GetAnken(int Anken_ID)
        {
            T_Anken anken = await _context.T_Ankens
                .Where(anken => anken.Anken_ID == Anken_ID)
                .FirstOrDefaultAsync();

            return anken;
        }


        /// <summary>
        /// 指定されたAnkenDisplay_IDに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">AnkenDisplay_IDの値</param>
        /// <returns>T_Anken_Displayのインスタンス</returns>
        public async Task<T_Anken_Display> GetAnkenDisplay(int AnkenDisplay_ID)
        {
            T_Anken_Display ankenDisplay = await _context.T_Anken_Displays
                .Where(anken => anken.AnkenDisplay_ID == AnkenDisplay_ID)
                .FirstOrDefaultAsync();

            return ankenDisplay;
        }

        /// <summary>
        /// 指定されたAnken_IDに基づいてT_Anken_Detailを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値</param>
        /// <param name="Anken_Latest_Order">Anken_Latest_Orderの値</param>
        /// <returns>T_Anken_Detailのインスタンス</returns>
        public async Task<T_Anken_Detail> GetAnkenDetail(int Anken_ID, int Anken_Latest_Order)
        {
            T_Anken_Detail ankenDetail = await _context.T_Anken_Details
                .Where(anken => anken.Anken_ID == Anken_ID && anken.Anken_Order == Anken_Latest_Order)
                .FirstOrDefaultAsync();

            return ankenDetail;
        }

        /// <summary>
        /// 指定されたAnken_IDに基づいてT_Anken_Pointを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値</param>
        /// <param name="Anken_Latest_Order">Anken_Latest_Orderの値</param>
        /// <returns>T_Anken_Pointのインスタンス</returns>
        public async Task<List<T_Anken_Point>> GetAnkenPoint(int Anken_ID, int Anken_Latest_Order)
        {
            List<T_Anken_Point> ankenPoint = await _context.T_Anken_Points
                .Where(anken => anken.Anken_ID == Anken_ID && anken.Anken_Order == Anken_Latest_Order)
                .ToListAsync();

            return ankenPoint;
        }



        /// <summary>
        /// 指定されたKokyakuIdに基づいてSeikyuRemarksを非同期に取得します。
        /// </summary>
        /// <param name="KokyakuId">顧客ID</param>
        /// <returns>SeikyuRemarksの文字列</returns>
        public async Task<string> GetSeikyuRemarks(int? KokyakuId)
        {
            string SeikyuRemarks = await _context.M_Customer_Branches
                .Where(customer => customer.Customer_Branch_ID == KokyakuId)
                .Select(customer => customer.SeikyuRemarks)
                .FirstOrDefaultAsync();

            return SeikyuRemarks;
        }

        /// <summary>
        /// 指定されたKokyakuIdに基づいてTollSeikyuKubunのリストを非同期に取得します。
        /// </summary>
        /// <param name="KokyakuId">顧客ID</param>
        /// <returns>TollSeikyuKubunのリスト</returns>
        public async Task<List<M_Customer_TollSeikyuKubun>> GetTollSeikyuKubun(int? KokyakuId)
        {
            List<M_Customer_TollSeikyuKubun> TollSeikyuKubuns = await _context.M_Customer_TollSeikyuKubuns
                .Where(customer => customer.Customer_Branch_ID == KokyakuId)
                .OrderBy(customer => customer.Sort)
                .ToListAsync();

            return TollSeikyuKubuns;
        }

        /// <summary>
        /// 特定の日報承認レコードを取得します。
        /// </summary>
        /// <param name="nippouId">日報のIDです。</param>
        /// <returns>日報承認のオブジェクトを返します。</returns>
        public async Task<T_Nippou_Approval> GetNippouApproval(int nippouId)
        {
            T_Nippou_Approval NippouApproval = await _context.T_Nippou_Approvals
                .Where(n => n.Nippou_ID == nippouId)
                .FirstOrDefaultAsync();

            return NippouApproval;
        }

        /// <summary>
        /// 指定された案件ID、区分、運転手IDに基づいて表示名を取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件配車用IDです。</param>
        /// <param name="kubun">区分です¥ 。</param>
        /// <param name="Driver_ID">運転手IDです。</param>
        /// <returns>表示名を返します。</returns>
        public async Task<string> GetDisplayName(int AnkenDisplay_ID, int? kubun, int? Driver_ID)
        {
            string displayName = string.Empty;

            if (kubun == 1 || kubun == 4)
            {
                // T_HaisyaテーブルからDriverIDを取得
                T_Haisya haisya = await _context.T_Haisyas
                    .FirstOrDefaultAsync(h => h.AnkenDisplay_ID == AnkenDisplay_ID
                                              && h.Driver_ID == Driver_ID);

                if (haisya != null)
                {
                    // M_CompanyDriverテーブルからDisplay_Nameを取得
                    M_CompanyDriver companyDriver = await _context.M_CompanyDrivers
                        .FirstOrDefaultAsync(cd => cd.Driver_ID == haisya.Driver_ID);

                    displayName = companyDriver?.Display_Name;
                }
            }
            else if (kubun == 2)
            {
                // T_Haisyaテーブルからレコードを取得
                T_Haisya haisya = await _context.T_Haisyas
                    .FirstOrDefaultAsync(h => h.AnkenDisplay_ID == AnkenDisplay_ID);

                if (haisya != null)
                {
                    // T_Haisya_YosyaテーブルからYosya_IDを取得
                    T_Haisya_Yosya haisyaYosya = await _context.T_Haisya_Yosyas
                        .FirstOrDefaultAsync(hy => hy.Haisya_ID == haisya.Haisya_ID);

                    if (haisyaYosya != null)
                    {
                        // M_Yosya_DriverテーブルからDisplay_Nameを取得    ?????masuda
                        M_Yosya_Driver yosyaDriver = await _context.M_Yosya_Drivers
                            .FirstOrDefaultAsync(yd => yd.Yosya_Driver_ID == haisyaYosya.YosyaDriver_ID);

                        displayName = yosyaDriver?.Display_Name;
                    }
                }
            }

            return displayName;
        }

        /// <summary>
        /// 区分、車両管理ID、配車IDに基づいて車番を取得します。
        /// </summary>
        /// <param name="kubun">区分です。</param>
        /// <param name="SyaryoManagement_ID">車両管理IDです。</param>
        /// <param name="Haisya_ID">配車IDです。</param>
        /// <returns>車番を返します。</returns>
        public async Task<string> GetSyabanNumber(int? kubun, int? SyaryoManagement_ID, int? Haisya_ID)
        {
            string syabanNumber = string.Empty;

            // T_Haisya_Yosyaテーブルからレコードを取得
            T_Haisya_Yosya haisya = await _context.T_Haisya_Yosyas
                .FirstOrDefaultAsync(h => h.Haisya_ID == Haisya_ID);

            if (kubun == 1 || kubun == 4)
            {
                // M_SyaryoManagementテーブルからSyaban_Numberを取得
                M_SyaryoManagement syaryoManagement = await _context.M_SyaryoManagements
                    .FirstOrDefaultAsync(sm => sm.SyaryoManagement_ID == SyaryoManagement_ID);

                syabanNumber = syaryoManagement?.Syaban_Number;

            }
            else if (kubun == 2)
            {
                if (haisya != null)
                {
                    // M_Yosya_Driver_Syaryoテーブルから該当するレコードを取得
                    M_Customer_Driver_Syaryo yosyaDriverSyaryo = await _context.M_Customer_Driver_Syaryos
                        .FirstOrDefaultAsync(yds => yds.Customer_DriverSyaryo_ID == haisya.YosyaDriverSyaryo_ID);

                    syabanNumber = yosyaDriverSyaryo?.Syaban_Number;
                }
            }

            return syabanNumber;
        }

        /// <summary>
        /// AnkenDisplay_IDに関連するデジタコデータを取得します。指定した期間内のデータを取得するか、
        /// 取得する範囲は案件開始日～案件終了日の全ての時間
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件配車用ID。対象の案件を指定します。</param>
        /// <param name="DriverCd">乗務員CD。</param>
        /// <param name="Syaban">車番。</param>
        /// <param name="StartDateTime">データを取得する開始日時。</param>
        /// <param name="EndDateTime">データを取得する終了日時。</param>
        /// <returns>指定された期間内または案件に関連するデジタコデータのリストを返します。</returns>
        public async Task<List<T_KUDGIVT>> GetDegitakoData(int AnkenDisplay_ID, int DriverCd, int Syaban, DateTime StartDateTime, DateTime EndDateTime)
        {
            List<T_KUDGIVT> degitakoData = new();

            IQueryable<T_KUDGIVT> query = _contextKintai.T_KUDGIVTs.Where(m => m.乗務員CD == DriverCd && m.車輌CD == Syaban);

            // T_AnkenDisplayテーブルからAnkenDisplay_IDが一致するレコードが存在するかを確認
            T_Anken_Display ankendisp = await _context.T_Anken_Displays.FirstOrDefaultAsync(n => n.AnkenDisplay_ID == AnkenDisplay_ID);

            if (ankendisp != null)
            {
                // StartDateTime〜EndDateTimeの間に該当するデータを取得
                query = query.Where(k => k.開始日時 >= DateTime.Parse(ankendisp.StartDatetime.ToString("yyyy/MM/dd")).AddDays(-1) && k.終了日時 < DateTime.Parse(ankendisp.EndDatetime.ToString("yyyy/MM/dd")).AddDays(2));
                degitakoData = await query.ToListAsync();
            }
            else
            {
                // StartDateTime〜EndDateTimeの間に該当するデータを取得
                query = query.Where(k => k.開始日時 >= DateTime.Parse(StartDateTime.ToString("yyyy/MM/dd")).AddDays(-1) && k.終了日時 < DateTime.Parse(EndDateTime.ToString("yyyy/MM/dd")).AddDays(2));
                degitakoData = await query.ToListAsync();
            }

            return degitakoData;
        }

        /// <summary>
        /// 高速道路に関連するデジタコデータを取得します。
        /// 将来的にDBデータを返すように拡張できます。
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件ID。対象の案件を指定します。</param>
        /// <param name="DriverCd">乗務員配車用CD。</param>
        /// <param name="Syaban">車番。</param>
        /// <param name="StartDateTime">データを取得する開始日時。</param>
        /// <param name="EndDateTime">データを取得する終了日時。</param>
        /// <returns>高速道路デジタコデータのリストを返します。</returns>
        public async Task<List<T_KUDGSIR>> GetHighwayData(int AnkenDisplay_ID, int DriverCd, int Syaban, DateTime StartDateTime, DateTime EndDateTime)
        {
            List<T_KUDGSIR> degitakoData = new();

            IQueryable<T_KUDGSIR> query = _contextKintai.T_KUDGSIRs.Where(m => m.乗務員CD == DriverCd && m.車輌CD == Syaban);

            // T_NippouテーブルからAnkenDisplay_IDが一致するレコードが存在するかを確認
            T_Nippou nippou = await _context.T_Nippous.FirstOrDefaultAsync(n => n.AnkenDisplay_ID == AnkenDisplay_ID);

            if (nippou != null)
            {
                // T_Nippou_KaisoテーブルからEnd_DateTimeを取得
                T_Nippou_Kaiso nippouKaiso = await _context.T_Nippou_Kaisos.FirstOrDefaultAsync(nk => nk.Nippou_ID == nippou.Nippou_ID);
                if (nippouKaiso != null)
                {
                    // End_DateTimeをDateTime型に変換
                    if (nippouKaiso.End_Datetime != null)
                    {
                        // T_KUDGIVTの終了日時カラムがEnd_DateTime以降のレコードを取得
                        query = query.Where(k => k.終了日時 >= nippouKaiso.End_Datetime);
                    }
                    degitakoData = await query.ToListAsync();
                }
            }
            else
            {
                // StartDateTime〜EndDateTimeの間に該当するデータを取得
                query = query.Where(k => k.開始日時 >= DateTime.Parse(StartDateTime.ToString("yyyy/MM/dd")).AddDays(-1) && k.終了日時 <= DateTime.Parse(EndDateTime.ToString("yyyy/MM/dd")).AddDays(2));
                degitakoData = await query.ToListAsync();
            }

            return degitakoData;
        }

        /// <summary>
        /// 日報のIDに基づいて、T_Nippou_Toll_Otherテーブルから該当するデータを取得します。
        /// </summary>
        /// <param name="nippouId">日報ID。対象の日報のIDを指定します。</param>
        /// <returns>指定された日報IDに関連するT_Nippou_Toll_Otherデータのリストを返します。</returns>
        public async Task<List<T_Nippou_Toll_Other>> GetNippouTollOther(int nippouId)
        {
            List<T_Nippou_Toll_Other> codeData = await _context.T_Nippou_Toll_Others
                .Where(m => m.Nippou_ID == nippouId)
                .ToListAsync();

            return codeData;
        }

        /// <summary>
        /// 指定された日報IDに関連する通行料データを取得します。
        /// </summary>
        /// <param name="nippouId">通行料データを取得する日報のID。</param>
        /// <returns>通行料データのリストを返します。</returns>
        public async Task<List<T_Nippou_Toll>> GetNippouToll(int nippouId)
        {
            List<T_Nippou_Toll> codeData = await _context.T_Nippou_Tolls
                .Where(m => m.Nippou_ID == nippouId)
                .ToListAsync();

            return codeData;
        }


        /// <summary>
        /// 指定された日報の承認データを取得します。
        /// </summary>
        /// <returns>日報の承認データを返します。</returns>
        public async Task<T_Nippou_Approval> GetNippouApprovalData(int Nippou_Approval_ID)
        {
            T_Nippou_Approval nippouApprovalData = await _context.T_Nippou_Approvals
                .Where(m => m.Nippou_Approval_ID == Nippou_Approval_ID)
                .FirstOrDefaultAsync();
            return nippouApprovalData;
        }

        /// <summary>
        /// Code_IDが9で、削除されていないコードデータとコード名のリストを取得します。
        /// </summary>
        /// <returns>コードデータとコード名のリストを返します。</returns>
        public async Task<List<CodeDataDto>> GetGroupCodeData()
        {
            List<CodeDataDto> groupUserList = await _context.M_Code_Data
                .Where(m => m.Code_ID == 9 && (m.Del_Flg ? 1 : 0) == 0)
                .Select(m => new CodeDataDto
                {
                    Code_Data = m.Code_Data,
                    Code_Name = m.Code_Name
                })
                .ToListAsync();

            return groupUserList;
        }

        /// <summary>
        /// 指定された案件配車用IDに基づいて日報データを取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">日報データを取得する案件配車用ID。</param>
        /// <returns>日報データを返します。</returns> 
        public async Task<T_Nippou> GetNippou(int AnkenDisplay_ID)
        {

            T_Nippou nippou = await _context.T_Nippous
                .Where(n => n.AnkenDisplay_ID == AnkenDisplay_ID)
                .FirstOrDefaultAsync();

            return nippou;

        }

        /// <summary>
        /// 指定された案件IDに基づいて売上データを取得します。
        /// </summary>
        /// <param name="Anken_ID">売上データを取得する案件のID。</param>
        /// <returns>売上データを返します。</returns> 
        public async Task<T_Uriage> GetUriage(int Anken_ID)
        {

            T_Uriage uraige = await _context.T_Uriages
                .Where(n => n.Anken_ID == Anken_ID)
                .FirstOrDefaultAsync();

            return uraige;

        }

        /// <summary>
        /// 指定された日報IDに関連する滞在データを取得します。
        /// </summary>
        /// <param name="Nippou_ID">滞在データを取得する日報のID。</param>
        /// <returns>滞在データを返します。</returns>
        public async Task<T_Nippou_Stay> GetNippouStay(int Nippou_ID)
        {
            T_Nippou_Stay nippouStay = await _context.T_Nippou_Stays
                .FirstOrDefaultAsync(n => n.Nippou_ID == Nippou_ID);

            return nippouStay;

        }

        /// <summary>
        /// 指定された日報IDに関連する滞在デジタコIDのリストを取得します。
        /// </summary>
        /// <param name="Nippou_ID">滞在デジタコIDを取得する日報のID。</param>
        /// <returns>滞在デジタコIDのリストを返します。</returns>
        public async Task<List<int>> GetNippouStayDegitako(int Nippou_ID)
        {
            List<int> nippouStayDegotako = await _context.T_Nippou_Stay_Degitakos
            .Where(n => n.Nippou_ID == Nippou_ID)
            .Select(n => n.KUDGIVT_ID)
            .ToListAsync();


            return nippouStayDegotako;
        }

        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Kaiso エンティティを取得します。
        /// </summary>
        /// <param name="Nippou_ID">取得する T_Nippou_Kaiso エンティティの ID。</param>
        /// <returns>T_Nippou_Kaiso エンティティ。</returns>
        public async Task<T_Nippou_Kaiso> GetNippouKaiso(int Nippou_ID)
        {
            T_Nippou_Kaiso nippouStay = await _context.T_Nippou_Kaisos
                .FirstOrDefaultAsync(n => n.Nippou_ID == Nippou_ID);

            return nippouStay;

        }
        /// <summary>
        /// 指定された Nippo_ID に基づいて関連する KUDGIVT_ID のリストを取得します。
        /// </summary>
        /// <param name="Nippou_ID">関連する KUDGIVT_ID を取得するための Nippo_ID。</param>
        /// <returns>KUDGIVT_ID のリスト。</returns>
        public async Task<List<int>> GetNippouKaisoDegitako(int Nippou_ID)
        {
            List<int> nippouStayDegotako = await _context.T_Nippou_Kaiso_Degitakos
            .Where(n => n.Nippou_ID == Nippou_ID)
            .Select(n => n.KUDGIVT_ID)
            .ToListAsync();

            return nippouStayDegotako;
        }

        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Anken エンティティを取得します。
        /// </summary>
        /// <param name="Nippou_ID">取得する T_Nippou_Anken エンティティの ID。</param>
        /// <returns>T_Nippou_Anken エンティティ。</returns>
        public async Task<T_Nippou_Anken> GetNippouAnken(int Nippou_ID)
        {
            T_Nippou_Anken nippouStay = await _context.T_Nippou_Ankens
                .FirstOrDefaultAsync(n => n.Nippou_ID == Nippou_ID);

            return nippouStay;

        }
        /// <summary>
        /// 指定された Nippo_ID に基づいて関連する KUDGIVT_ID のリストを取得します。
        /// </summary>
        /// <param name="Nippou_ID">関連する KUDGIVT_ID を取得するための Nippo_ID。</param>
        /// <returns>KUDGIVT_ID のリスト。</returns>
        public async Task<List<int>> GetNippouAnkenDegitako(int Nippou_ID)
        {
            List<int> nippouStayDegotako = await _context.T_Nippou_Anken_Degitakos
            .Where(n => n.Nippou_ID == Nippou_ID)
            .Select(n => n.KUDGIVT_ID)
            .ToListAsync();

            return nippouStayDegotako;
        }

        /// <summary>
        /// グループの ID と表示名のリストを取得します。グループ区分が 1 または 2 のもののみを対象とします。
        /// </summary>
        /// <returns>グループ ID と表示名のリスト。</returns>
        public async Task<List<GroupUserDto>> GetGroupUser()
        {
            List<GroupUserDto> groupUserList = await _context.M_CompanyUser_Groups
                .Where(m => m.Group_Kubun == 1 || m.Group_Kubun == 2)
                .Select(m => new GroupUserDto
                {
                    Group_ID = m.Group_ID,
                    Display_Name = m.Display_Name
                })
                .ToListAsync();

            return groupUserList;
        }

        /// <summary>
        /// 指定された T_Nippou_Approval データに基づいて、対応するグループの表示名を取得します。
        /// </summary>
        /// <param name="NippouApprovalData">グループ表示名を取得するための T_Nippou_Approval データ。</param>
        /// <returns>対応するグループの表示名。</returns>
        public async Task<string> GetSelectGroupName(T_Nippou_Approval NippouApprovalData)
        {
            string groupName = string.Empty;
            if (NippouApprovalData != null)
            {
                int approvalGroupId = NippouApprovalData.Approval_Group_ID;
                groupName = await _context.M_CompanyUser_Groups
                 .Where(g => g.Group_ID == approvalGroupId)
                 .Select(g => g.Display_Name)
                .FirstOrDefaultAsync();
            }

            return groupName;
        }
        /// <summary>
        /// 指定された AnkenDisplay_ID に基づいてコメントを取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">コメントを取得するための AnkenDisplay_ID。</param>
        /// <returns>取得したコメント。</returns>
        public async Task<string> GetComment(int AnkenDisplay_ID)
        {
            string comment = await _context.T_Nippous
                 .Where(c => c.AnkenDisplay_ID == AnkenDisplay_ID)
                 .Select(c => c.Commnet)
                .FirstOrDefaultAsync();

            return comment;
        }

        /// <summary>
        /// CertificationRequestModelリクエストをデータベースに保存します。既存の承認がある場合は更新し、新しい承認が必要な場合は追加します。
        /// </summary>
        /// <param name="data">保存または更新するための認証リクエストデータ。</param>
        /// <returns>処理結果を示すブール値。成功した場合は true。</returns>
        public async Task<bool> PostCertificationRequest(CertificationRequestModel data)
        {
            T_Nippou_Approval existingApproval = _context.T_Nippou_Approvals
                .FirstOrDefault(a => a.Nippou_Approval_ID == data.NippouApprovalData.Nippou_Approval_ID);
            if (DateTime.TryParse(data.FormattedLimitDate, out DateTime limitDateTime))
            {

                if (existingApproval == null)
                {
                    T_Nippou_Approval newApproval = new T_Nippou_Approval
                    {
                        Nippou_ID = data.Nippou_ID,
                        Approval_Group_ID = data.NippouApprovalData.Approval_Group_ID,
                        Limit_DateTime = limitDateTime,
                        Approval_Result = 0,
                        Order_Memo = data.Comment,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = data.User_ID
                    };

                    _context.T_Nippou_Approvals.Add(newApproval);
                }
                else
                {
                    // 既存レコードの更新
                    existingApproval.Approval_Group_ID = data.NippouApprovalData.Approval_Group_ID;
                    existingApproval.Limit_DateTime = limitDateTime;
                    existingApproval.Order_Memo = data.Comment;
                    existingApproval.Update_Datetime = DateTime.Now;
                    existingApproval.Update_User = data.User_ID;
                }

                await _context.SaveChangesAsync();
            }

            return true;

        }

        /// <summary>
        /// CertificationRequestModelリクエストをデータベースに保存します。既存の承認がある場合は更新し、新しい承認が必要な場合は追加します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou エンティティの ID。</param>
        /// <param name="data">保存または更新するための認証リクエストデータ。</param>
        /// <param name="UserID">更新または挿入を行うユーザーの ID。</param>
        /// <returns>処理結果を示すブール値。成功した場合は true。</returns>
        public async Task<bool> SaveNippouApproval(int nippouID, T_Nippou_Approval data, int UserID)
        {
            T_Nippou_Approval existingApproval = _context.T_Nippou_Approvals.FirstOrDefault(a => a.Nippou_Approval_ID == data.Nippou_Approval_ID);

            if (existingApproval == null)
            {
                T_Nippou_Approval newApproval = new T_Nippou_Approval
                {
                    Nippou_ID = nippouID,
                    Approval_Group_ID = data.Approval_Group_ID,
                    Limit_DateTime = data.Limit_DateTime,
                    Approval_Result = 0,
                    Order_Memo = data.Order_Memo,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = UserID
                };

                _context.T_Nippou_Approvals.Add(newApproval);
            }
            else
            {
                // 既存レコードの更新
                existingApproval.Approval_Group_ID = data.Approval_Group_ID;
                existingApproval.Limit_DateTime = data.Limit_DateTime;
                existingApproval.Order_Memo = data.Order_Memo;
                existingApproval.Update_Datetime = DateTime.Now;
                existingApproval.Update_User = UserID;
            }

            await _context.SaveChangesAsync();

            return true;

        }

        /// <summary>
        /// 指定された Nippo_ID に基づいてT_Nippou エンティティを更新または新規作成します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou エンティティの ID。</param>
        /// <param name="renkeiStatus">連携ステータス。</param>
        /// <param name="distance">距離。オプションの値。</param>
        /// <param name="loginUserId">更新または挿入を行うユーザーの ID。</param>
        /// <param name="approvalStatus">承認ステータス。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task<int> UpdateOrInsertNippou(int nippouId, int renkeiStatus, double? distance, int loginUserId, int approvalStatus, T_Anken_Display ankenDisplay)
        {
            T_Nippou nippou = await _context.T_Nippous.FindAsync(nippouId);

            if (nippou == null)
            {
                // 新しいレコードを作成
                nippou = new T_Nippou
                {
                    Anken_ID = ankenDisplay.Anken_ID,
                    AnkenDisplay_ID = ankenDisplay.AnkenDisplay_ID,
                    ApprovalStatus = approvalStatus,
                    RenkeiStatus = renkeiStatus,
                    Distance = distance,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = loginUserId,
                    Update_Datetime = DateTime.Now,
                    Update_User = loginUserId,
                    DegitakoLink_Result = 1,
                    Receipt_Date = DateOnly.FromDateTime(DateTime.Now),
                };

                await _context.T_Nippous.AddAsync(nippou);
                await _context.SaveChangesAsync();
            }
            else
            {
                // 既存のレコードを更新
                nippou.ApprovalStatus = approvalStatus;
                nippou.RenkeiStatus = renkeiStatus;
                nippou.Distance = distance;
                nippou.Update_Datetime = DateTime.Now;
                nippou.Update_User = loginUserId;

                _context.T_Nippous.Update(nippou);
                await _context.SaveChangesAsync();
            }

            // 新しく作られた、もしくは既存のNippou_IDを返す
            return nippou.Nippou_ID;
        }


        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Stay_Degitako レコードを更新します。
        /// 新しい KUDGIVT_ID があれば追加し、存在しない KUDGIVT_ID のレコードは削除します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou エンティティの ID。</param>
        /// <param name="kudgivtIds">関連する KUDGIVT_ID のリスト。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task UpdateNippouStayDegitako(int nippouId, List<int> kudgivtIds)
        {
            // 既存のレコードを取得
            List<T_Nippou_Stay_Degitako> existingRecords = await _context.T_Nippou_Stay_Degitakos
                .Where(n => n.Nippou_ID == nippouId)
                .ToListAsync();

            // T_KUDGIVTから対応するデータを取得
            List<T_KUDGIVT> kudgivtData = await _contextKintai.T_KUDGIVTs
                .Where(k => kudgivtIds.AsQueryable().Contains(k.ID))
                .ToListAsync();

            // 追加するレコード
            IEnumerable<T_Nippou_Stay_Degitako> recordsToAdd = kudgivtData
                .Where(k => !existingRecords.Any(r => r.KUDGIVT_ID == k.ID))
                .Select(k => new T_Nippou_Stay_Degitako
                {
                    Nippou_ID = nippouId,
                    KUDGIVT_ID = k.ID,
                    運行NO = k.運行NO,
                    読取日 = k.読取日,
                    事業所名 = k.事業所名,
                    車輌CD = k.車輌CD,
                    車輌名 = k.車輌名,
                    乗務員CD = k.乗務員CD,
                    乗務員名 = k.乗務員名,
                    開始日時 = k.開始日時,
                    イベント名 = k.イベント名,
                    終了日時 = k.終了日時,
                    開始走行距離 = k.開始走行距離,
                    終了走行距離 = k.終了走行距離,
                    区間時間 = k.区間時間,
                    区間距離 = k.区間距離,
                    開始市町村名 = k.開始市町村名,
                    終了市町村名 = k.終了市町村名,
                    開始場所名 = k.開始場所名,
                    終了場所名 = k.終了場所名,
                    早朝深夜_休憩 = k.早朝深夜_休憩,
                });

            // 削除するレコード
            IEnumerable<T_Nippou_Stay_Degitako> recordsToRemove = existingRecords
                .Where(r => !kudgivtIds.AsQueryable().Contains(r.KUDGIVT_ID));

            // 新しいレコードを追加
            await _context.T_Nippou_Stay_Degitakos.AddRangeAsync(recordsToAdd);

            // 不要なレコードを削除
            _context.T_Nippou_Stay_Degitakos.RemoveRange(recordsToRemove);

            // 変更を保存
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された Nippou_ID に基づいて T_Nippou_Kaiso_Degitako レコードを更新します。
        /// 新しい KUDGIVT_ID があれば追加し、存在しない KUDGIVT_ID のレコードは削除します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou_Kaiso_Degitako エンティティの ID。</param>
        /// <param name="kudgivtIds">関連する KUDGIVT_ID のリスト。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task UpdateNippouKaisoDegitako(int nippouId, List<int> kudgivtIds)
        {
            // 既存のレコードを取得
            List<T_Nippou_Kaiso_Degitako> existingRecords = await _context.T_Nippou_Kaiso_Degitakos
                .Where(n => n.Nippou_ID == nippouId)
                .ToListAsync();

            // T_KUDGIVTから対応するデータを取得
            List<T_KUDGIVT> kudgivtData = await _contextKintai.T_KUDGIVTs
                .Where(k => kudgivtIds.AsQueryable().Contains(k.ID))
                .ToListAsync();

            // 追加するレコード
            IEnumerable<T_Nippou_Kaiso_Degitako> recordsToAdd = kudgivtData
                .Where(k => !existingRecords.Any(r => r.KUDGIVT_ID == k.ID))
                .Select(k => new T_Nippou_Kaiso_Degitako
                {
                    Nippou_ID = nippouId,
                    KUDGIVT_ID = k.ID,
                    運行NO = k.運行NO,
                    読取日 = k.読取日,
                    事業所名 = k.事業所名,
                    車輌CD = k.車輌CD,
                    車輌名 = k.車輌名,
                    乗務員CD = k.乗務員CD,
                    乗務員名 = k.乗務員名,
                    開始日時 = k.開始日時,
                    イベント名 = k.イベント名,
                    終了日時 = k.終了日時,
                    開始走行距離 = k.開始走行距離,
                    終了走行距離 = k.終了走行距離,
                    区間時間 = k.区間時間,
                    区間距離 = k.区間距離,
                    開始市町村名 = k.開始市町村名,
                    終了市町村名 = k.終了市町村名,
                    開始場所名 = k.開始場所名,
                    終了場所名 = k.終了場所名,
                    早朝深夜_休憩 = k.早朝深夜_休憩,
                });

            // 削除するレコード
            IEnumerable<T_Nippou_Kaiso_Degitako> recordsToRemove = existingRecords
                .Where(r => !kudgivtIds.AsQueryable().Contains(r.KUDGIVT_ID));

            // 新しいレコードを追加
            await _context.T_Nippou_Kaiso_Degitakos.AddRangeAsync(recordsToAdd);

            // 不要なレコードを削除
            _context.T_Nippou_Kaiso_Degitakos.RemoveRange(recordsToRemove);

            // 変更を保存
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Stay エンティティを保存または更新します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou_Stay エンティティの ID。</param>
        /// <param name="viewModel">T_Nippou_Stay エンティティの新しいデータ。</param>
        /// <param name="loginUserId">操作を行うユーザーの ID。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task SaveOrUpdateNippouStay(int nippouId, T_Nippou_Stay viewModel, int loginUserId)
        {
            T_Nippou_Stay existingNippouStay = await _context.T_Nippou_Stays
                .FirstOrDefaultAsync(ns => ns.Nippou_ID == nippouId);

            if (existingNippouStay != null)
            {
                if (viewModel.Start_Datetime != null)
                {
                    // 既存のレコードを削除
                    List<T_Nippou_Stay> existingRecords = await _context.T_Nippou_Stays
                        .Where(ns => ns.Nippou_ID == nippouId)
                        .ToListAsync();
                    _context.T_Nippou_Stays.RemoveRange(existingRecords);
                }
                else
                {
                    existingNippouStay.Day = viewModel.Day;
                    existingNippouStay.Start_Datetime = viewModel.Start_Datetime;
                    existingNippouStay.End_Datetime = viewModel.End_Datetime;
                    existingNippouStay.Start_ShikuName = viewModel.Start_ShikuName;
                    existingNippouStay.End_ShikuName = viewModel.End_ShikuName;
                    existingNippouStay.Interval_Time = viewModel.Interval_Time;
                    existingNippouStay.Dllowance = viewModel.Dllowance;
                    existingNippouStay.Insert_Datetime = DateTime.Now;
                    existingNippouStay.Insert_User = loginUserId;
                    existingNippouStay.Update_Datetime = DateTime.Now;
                    existingNippouStay.Update_User = loginUserId;

                    _context.T_Nippou_Stays.Update(existingNippouStay);
                }
            }
            else
            {
                if (viewModel.Start_Datetime != null)
                {
                    T_Nippou_Stay newNippouStay = new T_Nippou_Stay
                    {
                        Nippou_ID = nippouId,
                        Day = viewModel.Day,
                        Start_Datetime = viewModel.Start_Datetime,
                        End_Datetime = viewModel.End_Datetime,
                        Start_ShikuName = viewModel.Start_ShikuName,
                        End_ShikuName = viewModel.End_ShikuName,
                        Start_PointName = null,
                        End_PointName = null,
                        Interval_Time = viewModel.Interval_Time,
                        Dllowance = viewModel.Dllowance,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = loginUserId,
                        Update_Datetime = DateTime.Now,
                        Update_User = loginUserId
                    };

                    await _context.T_Nippou_Stays.AddAsync(newNippouStay);
                }
            }

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Kaiso エンティティを保存または更新します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou_Kaiso エンティティの ID。</param>
        /// <param name="viewModel">T_Nippou_Kaiso エンティティの新しいデータ。</param>
        /// <param name="loginUserId">操作を行うユーザーの ID。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task SaveOrUpdateNippouKaiso(int nippouId, T_Nippou_Kaiso viewModel, int loginUserId)
        {
            T_Nippou_Kaiso existingNippouKaiso = await _context.T_Nippou_Kaisos
                .FirstOrDefaultAsync(nk => nk.Nippou_ID == nippouId);

            if (existingNippouKaiso != null)
            {
                if (viewModel.Start_Datetime == null)
                {
                    // 既存のレコードを削除
                    List<T_Nippou_Kaiso> existingRecords = await _context.T_Nippou_Kaisos
                        .Where(nk => nk.Nippou_ID == nippouId)
                        .ToListAsync();
                    _context.T_Nippou_Kaisos.RemoveRange(existingRecords);
                }
                else
                {
                    // レコードがあると更新 
                    existingNippouKaiso.Day = viewModel.Day;
                    existingNippouKaiso.Start_Datetime = viewModel.Start_Datetime;
                    existingNippouKaiso.End_Datetime = viewModel.End_Datetime;
                    existingNippouKaiso.Start_ShikuName = viewModel.Start_ShikuName;
                    existingNippouKaiso.End_ShikuName = viewModel.End_ShikuName;
                    existingNippouKaiso.Distance = viewModel.Distance;
                    existingNippouKaiso.Dllowance = viewModel.Dllowance;
                    existingNippouKaiso.Commnet = viewModel.Commnet;
                    existingNippouKaiso.Insert_Datetime = DateTime.Now;
                    existingNippouKaiso.Insert_User = loginUserId;
                    existingNippouKaiso.Update_Datetime = DateTime.Now;
                    existingNippouKaiso.Update_User = loginUserId;

                    _context.T_Nippou_Kaisos.Update(existingNippouKaiso);
                }
            }
            else
            {
                if (viewModel.Start_Datetime != null)
                {
                    // レコードがないと作成
                    T_Nippou_Kaiso newNippouKaiso = new T_Nippou_Kaiso
                    {
                        Nippou_ID = nippouId,
                        Day = viewModel.Day,
                        Start_Datetime = viewModel.Start_Datetime,
                        End_Datetime = viewModel.End_Datetime,
                        Start_ShikuName = viewModel.Start_ShikuName,
                        End_ShikuName = viewModel.End_ShikuName,
                        Start_PointName = null,
                        End_PointName = null,
                        Distance = viewModel.Distance,
                        Dllowance = viewModel.Dllowance,
                        // Comment = viewModel.Comment,
                        Commnet = viewModel.Commnet,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = loginUserId,
                        Update_Datetime = DateTime.Now,
                        Update_User = loginUserId
                    };

                    await _context.T_Nippou_Kaisos.AddAsync(newNippouKaiso);
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された Nippo_ID に基づいて T_Nippou_Anken エンティティを保存または更新します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou_Anken エンティティの ID。</param>
        /// <param name="viewModel">T_Nippou_Anken エンティティの新しいデータ。</param>
        /// <param name="loginUserId">操作を行うユーザーの ID。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task SaveOrUpdateNippouAnken(int nippouId, T_Nippou_Anken viewModel, int loginUserId)
        {
            T_Nippou_Anken existingNippouAnken = await _context.T_Nippou_Ankens
                .FirstOrDefaultAsync(nk => nk.Nippou_ID == nippouId);

            if (existingNippouAnken != null)
            {
                if (viewModel.Start_Datetime == null)
                {
                    // 既存のレコードを削除
                    List<T_Nippou_Anken> existingRecords = await _context.T_Nippou_Ankens
                        .Where(nk => nk.Nippou_ID == nippouId)
                        .ToListAsync();
                    _context.T_Nippou_Ankens.RemoveRange(existingRecords);
                }
                else
                {
                    // レコードがあると更新 
                    existingNippouAnken.Day = viewModel.Day;
                    existingNippouAnken.Start_Datetime = viewModel.Start_Datetime;
                    existingNippouAnken.End_Datetime = viewModel.End_Datetime;

                    existingNippouAnken.Start_Degitako_Id = viewModel.Start_Degitako_Id;
                    existingNippouAnken.End_Degitako_Id = viewModel.End_Degitako_Id;
                    existingNippouAnken.Distance = viewModel.Distance;
                    existingNippouAnken.Dllowance = viewModel.Dllowance;

                    existingNippouAnken.BreakTime = viewModel.BreakTime;
                    existingNippouAnken.WorkTime = viewModel.WorkTime;
                    existingNippouAnken.ActualWorkTime = viewModel.ActualWorkTime;

                    existingNippouAnken.Insert_Datetime = DateTime.Now;
                    existingNippouAnken.Insert_User = loginUserId;
                    existingNippouAnken.Update_Datetime = DateTime.Now;
                    existingNippouAnken.Update_User = loginUserId;

                    _context.T_Nippou_Ankens.Update(existingNippouAnken);
                }
            }
            else
            {
                if (viewModel.Start_Datetime != null)
                {
                    // レコードがないと作成
                    T_Nippou_Anken newNippouAnken = new T_Nippou_Anken
                    {
                        Nippou_ID = nippouId,
                        Day = viewModel.Day,
                        Start_Datetime = viewModel.Start_Datetime,
                        End_Datetime = viewModel.End_Datetime,
                        Start_Degitako_Id = viewModel.Start_Degitako_Id,
                        End_Degitako_Id = viewModel.End_Degitako_Id,
     
                        Distance = viewModel.Distance,
                        Dllowance = viewModel.Dllowance,

                        BreakTime = viewModel.BreakTime,
                        WorkTime = viewModel.WorkTime,
                        ActualWorkTime = viewModel.ActualWorkTime,

                        Insert_Datetime = DateTime.Now,
                        Insert_User = loginUserId,
                        Update_Datetime = DateTime.Now,
                        Update_User = loginUserId
                    };

                    await _context.T_Nippou_Ankens.AddAsync(newNippouAnken);
                }
            }

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 指定された Nippou_ID に基づいて T_Nippou_Anken_Degitako レコードを更新します。
        /// 新しい KUDGIVT_ID があれば追加し、存在しない KUDGIVT_ID のレコードは削除します。
        /// </summary>
        /// <param name="nippouId">対象の T_Nippou_Anken_Degitako エンティティの ID。</param>
        /// <param name="kudgivtIds">関連する KUDGIVT_ID のリスト。</param>
        /// <returns>非同期操作の結果を示すタスク。</returns>
        public async Task UpdateNippouAnkenDegitako(int nippouId, List<int> kudgivtIds)
        {
            // 既存のレコードを取得
            List<T_Nippou_Anken_Degitako> existingRecords = await _context.T_Nippou_Anken_Degitakos
                .Where(n => n.Nippou_ID == nippouId)
                .ToListAsync();

            int startId = kudgivtIds[0];
            int endId = kudgivtIds.Count > 1 ? kudgivtIds[1] : startId;

            // T_KUDGIVTから対応するデータを取得
            List<T_KUDGIVT> kudgivtData = await _contextKintai.T_KUDGIVTs
                .Where(k => k.ID >= startId && k.ID <= endId).ToListAsync();
            //.Where(k => kudgivtIds.AsQueryable().Contains(k.ID))
            //.ToListAsync();

            // 追加するレコード
            IEnumerable<T_Nippou_Anken_Degitako> recordsToAdd = kudgivtData
                .Where(k => !existingRecords.Any(r => r.KUDGIVT_ID == k.ID))
                .Select(k => new T_Nippou_Anken_Degitako
                {
                    Nippou_ID = nippouId,
                    KUDGIVT_ID = k.ID,
                    運行NO = k.運行NO,
                    読取日 = k.読取日,
                    事業所名 = k.事業所名,
                    車輌CD = k.車輌CD,
                    車輌名 = k.車輌名,
                    乗務員CD = k.乗務員CD,
                    乗務員名 = k.乗務員名,
                    開始日時 = k.開始日時,
                    イベント名 = k.イベント名,
                    終了日時 = k.終了日時,
                    開始走行距離 = k.開始走行距離,
                    終了走行距離 = k.終了走行距離,
                    区間時間 = k.区間時間,
                    区間距離 = k.区間距離,
                    開始市町村名 = k.開始市町村名,
                    終了市町村名 = k.終了市町村名,
                    開始場所名 = k.開始場所名,
                    終了場所名 = k.終了場所名,
                    早朝深夜_休憩 = k.早朝深夜_休憩,
                });

            // 削除するレコード
            IEnumerable<T_Nippou_Anken_Degitako> recordsToRemove = existingRecords
                .Where(r => !kudgivtIds.AsQueryable().Contains(r.KUDGIVT_ID));

            // 新しいレコードを追加
            await _context.T_Nippou_Anken_Degitakos.AddRangeAsync(recordsToAdd);

            // 不要なレコードを削除
            _context.T_Nippou_Anken_Degitakos.RemoveRange(recordsToRemove);

            // 変更を保存
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された日報IDに関連するT_Nippou_Toll_Otherレコードを保存します。
        /// </summary>
        /// <param name="nippouId">通行料レコードが属する日報のID。</param>
        /// <param name="nippouTollOthers">保存するT_Nippou_Toll_Otherオブジェクトのリスト。</param>
        /// <param name="loginUserId">操作を行うユーザーのID。</param>
        /// <param name="ankenDisplay">案件の表示情報を含むT_Anken_Displayオブジェクト。</param>
        /// <returns>非同期操作を表すTask。</returns>
        /// <remarks>
        /// このメソッドは、指定された日報IDに関連する既存の通行料レコードを削除し、新しいレコードを追加します。
        /// </remarks>
        public async Task SaveNippouTollOther(int nippouId, List<T_Nippou_Toll_Other> nippouTollOthers, int loginUserId, T_Anken_Display ankenDisplay)
        {
            // 既存のレコードを削除
            List<T_Nippou_Toll_Other> existingRecords = await _context.T_Nippou_Toll_Others
                .Where(n => n.Nippou_ID == nippouId)
                .ToListAsync();

            _context.T_Nippou_Toll_Others.RemoveRange(existingRecords);

            // T_Haisyaテーブルからレコードを取得
            T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(h => h.Anken_ID == ankenDisplay.Anken_ID && h.AnkenDisplay_ID == ankenDisplay.AnkenDisplay_ID);

            // T_Haisya_Yoshaテーブルからレコードを取得（haisyaがnullでない場合のみ）
            T_Haisya_Yosya haisyaYosha = null;
            if (haisya != null)
            {
                haisyaYosha = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(hy => hy.Haisya_ID == haisya.Haisya_ID);
            }

            foreach (var item in nippouTollOthers)
            {
                T_Nippou_Toll_Other newRecord = new T_Nippou_Toll_Other
                {
                    Nippou_ID = nippouId,
                    Sort = item.Sort,

                    // [T_Haisya_Yosya]の有無により設定
                    DriverSyaryo_ID = haisyaYosha == null ? haisya.DriverSyaryo_ID : 0,
                    Driver_ID = haisyaYosha == null ? haisya.Driver_ID : 0,
                    // [T_Haisya_Yosya]の有無により設定
                    YosyaDriverSyaryo_ID = haisyaYosha == null ? 0 : haisyaYosha.YosyaDriverSyaryo_ID,
                    YosyaDriver_ID = haisyaYosha == null ? 0 : haisyaYosha.YosyaDriver_ID,
                    Yosya_Branch_ID = haisyaYosha == null ? 0 : haisyaYosha.Yosya_Branch_ID,

                    Day = DateOnly.FromDateTime(ankenDisplay.StartDatetime),
                    Toll_Kubun = item.Toll_Kubun,
                    Start_Name = item.Start_Name,
                    End_Name = item.End_Name,
                    Toll_Fee = item.Toll_Fee,
                    Futan_Kubun = item.Futan_Kubun,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = loginUserId,
                    Update_Datetime = DateTime.Now,
                    Update_User = loginUserId
                };

                await _context.T_Nippou_Toll_Others.AddAsync(newRecord);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された日報IDに関連するT_Nippou_Tollレコードを保存します。
        /// </summary>
        /// <param name="nippouId">通行料レコードが属する日報のID。</param>
        /// <param name="nippouToll">保存するT_Nippou_Tollオブジェクトのリスト。</param>
        /// <param name="loginUserId">操作を行うユーザーのID。</param>
        /// <param name="ankenDisplay">案件の表示情報を含むT_Anken_Displayオブジェクト。</param>
        /// <param name="nippouTollFlg">通行料フラグのリスト。</param>
        /// <param name="KUDGSIRList">対象外のIDリスト。</param>
        /// <returns>非同期操作を表すTask。</returns>
        /// <remarks>
        /// このメソッドは、指定された日報IDに関連する既存の通行料レコードを削除し、新しいレコードを追加します。また、T_HaisyaおよびT_Haisya_Yoshaテーブルからデータを取得して処理を行います。
        /// </remarks
        public async Task SaveNippouToll(int nippouId, List<T_Nippou_Toll> nippouToll, int loginUserId, T_Anken_Display ankenDisplay,
                                        List<T_Nippou_Toll> nippouTollFlg, List<int> KUDGSIRList)
        {
            // 既存のレコードを削除
            List<T_Nippou_Toll> existingRecords = await _context.T_Nippou_Tolls.Where(n => n.Nippou_ID == nippouId).ToListAsync();
            _context.T_Nippou_Tolls.RemoveRange(existingRecords);

            // T_Haisyaテーブルからレコードを取得
            T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(h => h.Anken_ID == ankenDisplay.Anken_ID && h.AnkenDisplay_ID == ankenDisplay.AnkenDisplay_ID);

            // T_Haisya_Yoshaテーブルからレコードを取得（haisyaがnullでない場合のみ）
            T_Haisya_Yosya haisyaYosha = null;
            if (haisya != null)
            {
                haisyaYosha = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(hy => hy.Haisya_ID == haisya.Haisya_ID);
            }

            foreach (var info in nippouToll)
            {
                T_Nippou_Toll newRecord = new T_Nippou_Toll
                {
                    Nippou_ID = nippouId,
                    Sort = info.Sort,
                    Futan_Kubun = info.Futan_Kubun,

                    // [T_Haisya_Yosya]の有無により設定
                    DriverSyaryo_ID = haisyaYosha == null ? haisya.DriverSyaryo_ID : 0,
                    Driver_ID = haisyaYosha == null ? haisya.Driver_ID : 0,
                    // [T_Haisya_Yosya]の有無により設定
                    YosyaDriverSyaryo_ID = haisyaYosha == null ? 0 : haisyaYosha.YosyaDriverSyaryo_ID,
                    YosyaDriver_ID = haisyaYosha == null ? 0 : haisyaYosha.YosyaDriver_ID,
                    Yosya_Branch_ID = haisyaYosha == null ? 0 : haisyaYosha.Yosya_Branch_ID,

                    運行日 = DateOnly.FromDateTime(info.開始日時?.Date ?? DateTime.MinValue),
                    開始日時 = info.開始日時,
                    終了日時 = info.終了日時,
                    開始道路番号 = info.開始道路番号,
                    開始道路名 = info.開始道路名,
                    終了道路番号 = info.終了道路番号,
                    終了道路名 = info.終了道路名,
                    開始IC名 = info.開始IC名,
                    終了IC名 = info.終了IC名,
                    開始ETC番号 = info.開始ETC番号,
                    終了ETC番号 = info.終了ETC番号,
                    料金 = info.料金,
                    走行距離 = info.走行距離,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = loginUserId,
                    Update_Datetime = DateTime.Now,
                    Update_User = loginUserId
                };

                await _context.T_Nippou_Tolls.AddAsync(newRecord);
            }

            if (nippouTollFlg.Count == 0)
            {



                //var highwayData = await GetHighwayData(ankenDisplay.Anken_ID, ankenDisplay.StartDatetime, ankenDisplay.EndDatetime);
                //foreach (var data in highwayData)
                //{
                //    if (!KUDGSIRList.AsQueryable().Contains(data.ID))
                //    {
                //        var newRecord = new T_Nippou_Toll
                //        {
                //            Nippou_ID = nippouId,
                //            // ソート順、負担区分をデフォルト値で設定します。
                //            Sort = 0,
                //            Futan_Kubun = 0,

                //            // 運行区分に基づいてドライバー車両IDとドライバーIDを設定します。
                //            // 運行区分が2の場合は対応するIDを設定し、それ以外の場合は0にします。
                //            DriverSyaryo_ID = haisya?.Haisya_Kubun == 2 ? haisya.DriverSyaryo_ID : 0,
                //            Driver_ID = haisya?.Haisya_Kubun == 2 ? haisya.Driver_ID : 0,

                //            // 運行区分に基づいて予想ドライバー車両IDと予想ドライバーIDを設定します。
                //            // 運行区分が1の場合は対応するIDを設定し、それ以外の場合は0にします。
                //            YosyaDriverSyaryo_ID = haisya?.Haisya_Kubun == 1 ? haisya.DriverSyaryo_ID : 0,
                //            YosyaDriver_ID = haisya?.Haisya_Kubun == 1 && haisyaYosha != null ? haisyaYosha.YosyaDriver_ID : 0,

                //            // 運行区分が1で、haisyaYoshaがnullでない場合に予想支店IDを設定します。
                //            // それ以外の場合は0にします。
                //            Yosya_Branch_ID = haisya?.Haisya_Kubun == 1 && haisyaYosha != null ? haisyaYosha.Yosya_Branch_ID : 0,

                //            // 運行日を設定します。開始日時が設定されている場合はその日付を使用し、設定されていない場合は最小日時を設定します。
                //            運行日 = data.開始日時?.Date ?? DateTime.MinValue,

                //            事業所名 = data.事業所名,
                //            車輌CD = data.車輌CD,
                //            車輌名 = data.車輌名,
                //            乗務員CD = data.乗務員CD,
                //            乗務員名 = data.乗務員名,
                //            運行NO = data.運行NO,
                //            読取日 = data.読取日,
                //            開始日時 = data.開始日時,
                //            終了日時 = data.終了日時,
                //            開始道路名 = data.開始道路名,
                //            終了道路名 = data.終了道路名,
                //            開始IC名 = data.開始IC名,
                //            終了IC名 = data.終了IC名,
                //            料金 = data.料金,
                //            走行距離 = data.走行距離,
                //            Insert_Datetime = DateTime.Now,
                //            Insert_User = loginUserId
                //        };

                //        await _context.T_Nippou_Tolls.AddAsync(newRecord);
                //    }
                //}
            }

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 日報の登録を行います。
        /// </summary>
        /// <param name="data">登録する日報の詳細モデル。</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        public async Task<bool> ProvisionalRegistration(DailyReportRegistrationDetailModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int nippouId = await UpdateOrInsertNippou(data.Nippou_ID, data.Nippou.RenkeiStatus, data.Nippou.Distance, data.User_ID, data.Nippou.ApprovalStatus, data.AnkenDisplay);
                await UpdateNippouAnkenDegitako(nippouId, data.NippouAnkenDegitakoIdList);
                await UpdateNippouStayDegitako(nippouId, data.NippouStayDegitakoIdList);
                await UpdateNippouKaisoDegitako(nippouId, data.NippouKaisoDegitakoIdList);
                await SaveOrUpdateNippouAnken(nippouId, data.Nippou_Anken, data.User_ID);
                await SaveOrUpdateNippouStay(nippouId, data.Nippou_Stay, data.User_ID);
                await SaveOrUpdateNippouKaiso(nippouId, data.Nippou_Kaiso, data.User_ID);
                await SaveNippouTollOther(nippouId, data.NippouTollOther, data.User_ID, data.AnkenDisplay);
                await SaveNippouToll(nippouId, data.NippouToll, data.User_ID, data.AnkenDisplay, data.InitialDisplayNippouToll, data.KUDGSIRIdList);
                if (data.NippouApproval != null && data.NippouApproval.Limit_DateTime != DateTime.MinValue)
                {
                    await SaveNippouApproval(nippouId, data.NippouApproval, data.User_ID);
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateOrCreateHoliday:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// 日報登録詳細データを管理するリポジトリインターフェースです。
    /// </summary>
    public interface IDailyReportRegistrationDetailRepository
    {
        /// <summary>
        /// 指定されたAnken_IDに基づいてT_Ankenを非同期に取得します。
        /// </summary>
        /// <param name="Anken_ID">Anken_IDの値</param>
        /// <returns>T_Ankenのインスタンス</returns>
        Task<T_Anken> GetAnken(int Anken_ID);

        /// <summary>
        /// 指定されたAnkenDisplay_IDに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">AnkenDisplay_IDの値</param>
        /// <returns>T_Anken_Displayのインスタンス</returns>
        Task<T_Anken_Display> GetAnkenDisplay(int AnkenDisplay_ID);

        Task<T_Anken_Detail> GetAnkenDetail(int Anken_ID, int Anken_Latest_Order);
        Task<List<T_Anken_Point>> GetAnkenPoint(int Anken_ID, int Anken_Latest_Order);
        Task<string> GetSeikyuRemarks(int? KokyakuId);
        Task<List<M_Customer_TollSeikyuKubun>> GetTollSeikyuKubun(int? KokyakuId);
        Task<string> GetDisplayName(int AnkenDisplay_ID, int? kubun, int? Driver_ID);
        Task<string> GetSyabanNumber(int? kubun, int? SyaryoManagement_ID, int? Haisya_ID);
        Task<List<T_KUDGIVT>> GetDegitakoData(int AnkenDisplay_ID, int DriverCd, int Syaban, DateTime StartDateTime, DateTime EndDateTime);
        Task<List<T_KUDGSIR>> GetHighwayData(int AnkenDisplay_ID, int DriverCd, int Syaban, DateTime StartDateTime, DateTime EndDateTime);
        Task<T_Nippou_Approval> GetNippouApprovalData(int Nippou_Approval_ID);
        Task<List<GroupUserDto>> GetGroupUser();
        Task<string> GetSelectGroupName(T_Nippou_Approval NippouApprovalData);
        Task<string> GetComment(int AnkenDisplay_ID);
        Task<bool> PostCertificationRequest(CertificationRequestModel data);
        Task<T_Nippou> GetNippou(int AnkenDisplay_ID);
        Task<T_Uriage> GetUriage(int Anken_ID);
        Task<T_Nippou_Anken> GetNippouAnken(int Nippou_ID);
        Task<List<int>> GetNippouAnkenDegitako(int Nippou_ID);
        Task<T_Nippou_Stay> GetNippouStay(int Nippou_ID);
        Task<List<int>> GetNippouStayDegitako(int Nippou_ID);
        Task<T_Nippou_Kaiso> GetNippouKaiso(int Nippou_ID);
        Task<List<int>> GetNippouKaisoDegitako(int Nippou_ID);
        Task<T_Nippou_Approval> GetNippouApproval(int nippouId);
        Task<List<T_Nippou_Toll_Other>> GetNippouTollOther(int nippouId);
        Task<List<T_Nippou_Toll>> GetNippouToll(int nippouId);
        Task<List<CodeDataDto>> GetGroupCodeData();
        Task<int> UpdateOrInsertNippou(int nippouId, int renkeiStatus, double? distance, int loginUserId, int approvalStatus, T_Anken_Display ankenDisplay);
        Task UpdateNippouStayDegitako(int nippouId, List<int> kudgivtIds);
        Task UpdateNippouKaisoDegitako(int nippouId, List<int> kudgivtIds);
        Task SaveOrUpdateNippouStay(int nippouId, T_Nippou_Stay viewModel, int loginUserId);
        Task SaveOrUpdateNippouKaiso(int nippouId, T_Nippou_Kaiso viewModel, int loginUserId);
        Task SaveOrUpdateNippouAnken(int nippouId, T_Nippou_Anken viewModel, int loginUserId);
        Task SaveNippouTollOther(int nippouId, List<T_Nippou_Toll_Other> nippouTollOthers, int loginUserId, T_Anken_Display ankenDisplay);
        Task SaveNippouToll(int nippouId, List<T_Nippou_Toll> nippouToll, int loginUserId, T_Anken_Display ankenDisplay, List<T_Nippou_Toll> nippouTollFlg, List<int> KUDGSIRList);
        Task<bool> ProvisionalRegistration(DailyReportRegistrationDetailModel data);
    }
}