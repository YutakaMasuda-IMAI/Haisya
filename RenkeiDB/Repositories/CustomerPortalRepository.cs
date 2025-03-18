using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 顧客ポータルリポジトリ
    /// </summary>
    public class CustomerPortalRepository : RepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>, ICustomerPortalRepository
    {
        public CustomerPortalRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// ポータル情報（荷主）の取得
        /// </summary>
        /// <param name="companyId">ログインユーザのcompany id</param>
        /// <param name="branchId">ログインユーザのbranch id</param>
        /// <returns>ポータル情報のリスト</returns>
        public async Task<IEnumerable<JoinCustomerPortalDto>> GetPortalsAsync(int companyId, int branchId)
        {
            IQueryable<JoinCustomerPortalDto> builder = from tra in DbContext.Set<T_Renkei_Anken>()
                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                             on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                             equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }

                          join trac in DbContext.Set<T_Renkei_Anken_Check>()
                             on tra.Renkei_Anken_ID equals trac.Renkei_Anken_ID into renkeiAnkenCheck
                          from trac in renkeiAnkenCheck.DefaultIfEmpty()

                          join trap in DbContext.Set<T_Renkei_Anken_Point>()
                             on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                             equals new { id = trap.Renkei_Anken_ID, order = trap.Renkei_Anken_Order } into renkeiAnkenPoint
                          from trap in renkeiAnkenPoint.DefaultIfEmpty()

                          where
                             tra.Company_ID == companyId
                             && tra.Branch_ID == branchId
                          orderby tra.Renkei_Anken_No ascending
                          select new JoinCustomerPortalDto
                          {
                              renkeiAnken = tra,
                              renkeiAnkenDetail = trad,
                              renkeiAnkenCheck = trac,
                              renkeiAnkenPoint = trap,
                          };
            return await builder.ToListAsync();
        }

        /// <summary>
        /// 案件秘密情報のデータ取得
        /// </summary>
        /// <param name="renkeiAnkenIDs">案件IDリスト</param>
        /// <returns>案件秘密情報のリスト</returns>
        public async Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs)
        {
            IQueryable<AnkenSecureDto> builder = from tras in DbContext.Set<T_Renkei_Anken_Secure>()
                          join trasc in DbContext.Set<T_Renkei_Anken_Secure_Check>()
                            on tras.Renkei_Anken_Secure_ID equals trasc.Renkei_Anken_Secure_ID into ankenSecureCheck
                          from trasc in ankenSecureCheck.DefaultIfEmpty()

                          join trasp in DbContext.Set<T_Renkei_Anken_Secure_Print>()
                            on tras.Renkei_Anken_Secure_ID equals trasp.Renkei_Anken_Secure_ID into ankenSecurePrint
                          from trasp in ankenSecurePrint.DefaultIfEmpty()
                          where
                            renkeiAnkenIDs.Contains(tras.Renkei_Anken_ID)
                            && tras.Del_Datetime == null
                          select new AnkenSecureDto
                          {
                              renkeiAnkenSecure = tras,
                              renkeiAnkenSecureCheck = trasc,
                              renkeiAnkenSecurePrint = trasp,
                          };
            List<AnkenSecureDto> result = await builder.ToListAsync();
            return result;
        }

        /// <summary>
        /// ポータルのカウントを取得する
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">カウント日</param>
        /// <returns>ポータルのカウント</returns>
        public async Task<CustomerPortalCountsDto> Get_portal_counts(int company_id, int branch_id, DateTime date)
        {
            int requestcnt = await Count_request(company_id, branch_id, date);
            int cancelcnt = await Count_cancel(company_id, branch_id, date);
            int ordercnt = await Count_order(company_id, branch_id, date);
            int haisyakakuteicnt = await Count_haisyaKakutei(company_id, branch_id, date);
            int haisyatyucnt = await Count_haisyatyu(company_id, branch_id, date);
            int yusouhenkourenrakucnt = await Count_yusouhenkourenraku(company_id, branch_id, date);
            int yusouhenkoukakunincnt = await Count_yusouhenkoukakunin(company_id, branch_id, date);
            int yusoucanceliraicnt = await Count_yusoucancelirai(company_id, branch_id, date);
            int yusouCancelCnt = await Count_yusoucancel(company_id, branch_id, date);

            return new()
            {
                requestCnt = requestcnt,
                cancelCnt = cancelcnt,
                orderCnt = ordercnt,
                haisyaKakuteiCnt = haisyakakuteicnt,
                haisyatyuCnt = haisyatyucnt,
                yusouhenkouRenrakuCnt = yusouhenkourenrakucnt,
                yusouhenkouKakuninCnt = yusouhenkoukakunincnt,
                yusouCancelIraiCnt = yusoucanceliraicnt,
                yusouCancelCnt = yusouCancelCnt,
            };
        }

        /// <summary>
        /// 依頼中案件数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>依頼中案件数</returns>
        private async Task<int> Count_request(int company_id, int branch_id, DateTime date)
            => await Count_anken(company_id, branch_id, date, 0);

        /// <summary>
        /// 注文数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>注文数</returns>
        private async Task<int> Count_order(int company_id, int branch_id, DateTime date)
            => await Count_anken(company_id, branch_id, date, 1);

        /// <summary>
        /// 依頼案件をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <param name="kubun">案件区分（0: 依頼案件, 1: 受注案件）</param>
        /// <returns>依頼案件数</returns>
        private async Task<int> Count_anken(int company_id, int branch_id, DateTime date, int kubun)
        {
            IQueryable<int> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id
                    && t1.Renkei_Anken_Kubun == kubun
                    && t1.Renkei_Anken_Status != 3

                    && t2.PointDate == date
                    && t2.SEKubun == "S"
                select t1.Renkei_Anken_ID;
            return await b.CountAsync();
        }

        /// <summary>
        /// キャンセル数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>キャンセル数</returns>
        private async Task<int> Count_cancel(int company_id, int branch_id, DateTime date)
        {
            IQueryable<int> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id
                    && t1.Renkei_Anken_Status == 3
                    && (t1.Renkei_Anken_Kubun == 0 || t1.Renkei_Anken_Kubun == 1)

                    && t2.PointDate == date
                    && t2.SEKubun == "S"
                select t1.Renkei_Anken_ID;
            return await b.CountAsync();
        }

        /// <summary>
        /// 配車確定数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>配車確定数</returns>
        private async Task<int> Count_haisyaKakutei(int company_id, int branch_id, DateTime date)
        {
            IQueryable<object> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                join t3 in DbContext.Set<T_Renkei_Anken_Secure>()
                    on t1.Renkei_Anken_ID equals t3.Renkei_Anken_ID
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id

                    && t2.PointDate == date
                    && t2.SEKubun == "S"

                    && t3.Renkei_Anken_Secure_Status == 0
                group t1 by t1.Renkei_Anken_ID into t1G
                select new {
                    Renkei_Anken_ID = t1G.Key,
                };
            return await b.CountAsync();
        }

        /// <summary>
        /// 配車中数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>配車中数</returns>
        private async Task<int> Count_haisyatyu(int company_id, int branch_id, DateTime date)
        {
            IQueryable<object> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                join t3 in DbContext.Set<T_Renkei_Anken_Secure>()
                    on t1.Renkei_Anken_ID equals t3.Renkei_Anken_ID
                join t4 in DbContext.Set<T_Renkei_Anken_Secure_Print>()
                    on t3.Renkei_Anken_Secure_ID equals t4.Renkei_Anken_Secure_ID
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id

                    && t2.PointDate == date
                    && t2.SEKubun == "S"

                    && t4.Print_Kubun == 1
                group t1 by t1.Renkei_Anken_ID into t1G
                select new
                {
                    Renkei_Anken_ID = t1G.Key,
                };
            return await b.CountAsync();
        }

        /// <summary>
        /// 輸送変更連絡数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>輸送変更連絡数</returns>
        private async Task<int> Count_yusouhenkourenraku(int company_id, int branch_id, DateTime date)
            => await Count_yusou(company_id, branch_id, date, 7);

        /// <summary>
        /// ステータスに応じた輸送数をカウント
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <param name="secure_status">ステータス</param>
        /// <returns>輸送数</returns>
        private async Task<int> Count_yusou(int company_id, int branch_id, DateTime date, int secure_status)
        {
            IQueryable<object> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                join t3 in DbContext.Set<T_Renkei_Anken_Secure>()
                    on t1.Renkei_Anken_ID equals t3.Renkei_Anken_ID
                join t4 in DbContext.Set<T_Renkei_Anken_Secure_Check>()
                    on t3.Renkei_Anken_Secure_ID equals t4.Renkei_Anken_Secure_ID into tt
                from t4 in tt.DefaultIfEmpty()
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id

                    && t2.PointDate == date
                    && t2.SEKubun == "S"

                    && t3.Renkei_Anken_Secure_Status == secure_status

                    && t4 == null
                group t1 by t1.Renkei_Anken_ID into t1G
                select new
                {
                    Renkei_Anken_ID = t1G.Key,
                };
            return await b.Distinct().CountAsync();
        }

        /// <summary>
        /// ステータスに応じた輸送数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <param name="secure_status">ステータス</param>
        /// <returns>輸送数</returns>
        private async Task<int> Count_yusou_with_check(int company_id, int branch_id, DateTime date, int secure_status)
        {
            IQueryable<object> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                join t3 in DbContext.Set<T_Renkei_Anken_Secure>()
                    on t1.Renkei_Anken_ID equals t3.Renkei_Anken_ID
                join t4 in DbContext.Set<T_Renkei_Anken_Secure_Check>()
                    on t3.Renkei_Anken_Secure_ID equals t4.Renkei_Anken_Secure_ID
                where t1.Company_ID == company_id
                    && t1.Branch_ID == branch_id

                    && t2.PointDate == date
                    && t2.SEKubun == "S"

                    && t3.Renkei_Anken_Secure_Status == secure_status
                group t1 by t1.Renkei_Anken_ID into t1G
                select new
                {
                    Renkei_Anken_ID = t1G.Key,
                };
            return await b.CountAsync();
        }

        /// <summary>
        /// 輸送変更確認数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>輸送変更確認数</returns>
        private async Task<int> Count_yusouhenkoukakunin(int company_id, int branch_id, DateTime date)
            => await Count_yusou_with_check(company_id, branch_id, date, 7);

        /// <summary>
        /// 輸送取消依頼数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>輸送取消依頼数</returns>
        private async Task<int> Count_yusoucancelirai(int company_id, int branch_id, DateTime date)
            => await Count_yusou(company_id, branch_id, date, 6);

        /// <summary>
        /// 輸送取消確認数をカウントする
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="date">対象日</param>
        /// <returns>輸送取消確認数</returns>
        private async Task<int> Count_yusoucancel(int company_id, int branch_id, DateTime date)
            => await Count_yusou_with_check(company_id, branch_id, date, 6);

        /// <summary>
        /// カレンダーを取得する
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <param name="branchId">ログインしているユーザーの支店ID</param>
        /// <param name="fromDate">開始日（含む）</param>
        /// <param name="toDate">終了日（含まない）</param>
        /// <returns>カウントリスト</returns>
        public async Task<IList<CountByDate>> GetCalendars(int companyId, int branchId, DateTime fromDate, DateTime toDate)
        {
            IList<CountByDate> orderCnt = await Count_order(companyId, branchId, fromDate, toDate);
            IList<CountByDate> cancelCnt = await Count_cancel(companyId, branchId, fromDate, toDate);
            IList<CountByDate> requestCnt = await Count_request(companyId, branchId, fromDate, toDate);
            return orderCnt
                .Increase(requestCnt)
                .Decrease(cancelCnt);
        }
        /// <summary>
        /// 受注件数または依頼中案件数をカウント
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <param name="branchId">ログインしているユーザーの支店ID</param>
        /// <param name="fromDate">開始日（含む）</param>
        /// <param name="toDate">終了日（含まない）</param>
        /// <param name="anken_kubun">案件区分（0: 依頼案件, 1: 受注案件）</param>
        /// <returns>カウントリスト</returns>
        public async Task<IList<CountByDate>> Count_order_or_request(int companyId, int branchId, DateTime fromDate, DateTime toDate,int anken_kubun)
        {
            IQueryable<CountByDate> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                where t1.Company_ID == companyId
                    && t1.Branch_ID == branchId
                    && t2.SEKubun == "S"
                    && t2.PointDate >= fromDate
                    && t2.PointDate < toDate
                    && t1.Renkei_Anken_Kubun == anken_kubun
                    && t1.Renkei_Anken_Status != 3
                group t1 by t2.PointDate into g
                orderby g.Key ascending
                select new CountByDate { Date = g.Key.Value, Count = g.Count() };
            return await b.ToListAsync();
        }

        /// <summary>
        /// 取消案件数をカウント
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <param name="branchId">ログインしているユーザーの支店ID</param>
        /// <param name="fromDate">開始日（含む）</param>
        /// <param name="toDate">終了日（含まない）</param>
        /// <returns>取消案件数</returns>
        public async Task<IList<CountByDate>> Count_cancel(int companyId, int branchId, DateTime fromDate, DateTime toDate)
        {
            IQueryable<CountByDate> b =
                from t1 in DbContext.Set<T_Renkei_Anken>()
                join t2 in DbContext.Set<T_Renkei_Anken_Point>()
                    on new { id = t1.Renkei_Anken_ID, order = t1.Renkei_Anken_Latest_Order }
                    equals new { id = t2.Renkei_Anken_ID, order = t2.Renkei_Anken_Order }
                where t1.Company_ID == companyId
                    && t1.Branch_ID == branchId
                    && t2.SEKubun == "S"
                    && t2.PointDate >= fromDate
                    && t2.PointDate < toDate
                    && t1.Renkei_Anken_Kubun == 0
                    && t1.Renkei_Anken_Status == 3
                group t1 by t2.PointDate into g
                orderby g.Key ascending
                select new CountByDate { Date = g.Key.Value, Count = g.Count() };
            return await b.ToListAsync();
        }

        /// <summary>
        /// 受注件数をカウント
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <param name="branchId">ログインしているユーザーの支店ID</param>
        /// <param name="fromDate">開始日（含む）</param>
        /// <param name="toDate">終了日（含まない）</param>
        /// <returns>カウントリスト</returns>
        public async Task<IList<CountByDate>> Count_order(int companyId, int branchId, DateTime fromDate, DateTime toDate)
            => await Count_order_or_request(companyId, branchId, fromDate, toDate, 1);

        /// <summary>
        /// 依頼中案件数をカウント
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <param name="branchId">ログインしているユーザーの支店ID</param>
        /// <param name="fromDate">開始日（含む）</param>
        /// <param name="toDate">終了日（含まない）</param>
        /// <returns>カウントリスト</returns>
        public async Task<IList<CountByDate>> Count_request(int companyId, int branchId, DateTime fromDate, DateTime toDate)
            => await Count_order_or_request(companyId, branchId, fromDate, toDate, 0);
    }
}
