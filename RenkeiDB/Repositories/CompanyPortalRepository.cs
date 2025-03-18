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
    /// 会社ポータルリポジトリ
    /// </summary>
    public class CompanyPortalRepository : RepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>, ICompanyPortalRepository
    {
        public CompanyPortalRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// カレンダーを取得する
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="from_date">開始日（含む）</param>
        /// <param name="to_date">終了日（含まない）</param>
        /// <returns></returns>
        public async Task<IList<CountByDate>> Get_calendars(int company_id, int branch_id, DateTime from_date, DateTime to_date)
        {
            // 空車数、共有荷物数、受注案件数、依頼案件数を取得し、
            // 空車数を増加、共有荷物数を増加、受注案件数から安全な空車数を減少、依頼案件数から安全な荷物数を減少する
            IList<CountByDate> sharesyaryo = await Count_sharesyaryo(company_id, branch_id, from_date, to_date);
            IList<CountByDate> shareluggage = await Count_shareluggage(company_id, branch_id, from_date, to_date);
            IList<CountByDate> juchuanken = await Count_juchuanken(company_id, branch_id, from_date, to_date);
            IList<CountByDate> iraianken = await Count_iraianken(company_id, branch_id, from_date, to_date);

            return sharesyaryo
                .Increase(shareluggage)
                .Increase(juchuanken)
                .Increase(iraianken)
                .OrderBy(item => item.Date)
                .ToList();
        }

        /// <summary>
        /// 空車数をカウント
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="from_date">開始日（含む）</param>
        /// <param name="to_date">終了日（含まない）</param>
        /// <returns>空車数のリスト</returns>
        public async Task<IList<CountByDate>> Count_sharesyaryo(int company_id, int branch_id, DateTime from_date, DateTime to_date)
        {
            // 会社IDと支店IDに紐づく共有車両の詳細を取得し、共有車両の詳細の空車日がfrom_date以上でto_date未満のものを取得し、
            // 空車日でグループ化し、空車日の昇順で並べ替え、共有車両の数をカウントする
            IQueryable<CountByDate> b =
                from t1 in DbContext.Set<T_Share_Syaryo>()
                join t2 in DbContext.Set<T_Share_Syaryo_Detail>()
                    on new { id = t1.Share_Syaryo_ID, order = t1.Share_Syaryo_Latest_Order }
                    equals new { id = t2.Share_Syaryo_ID, order = t2.Share_Syaryo_Order }
                where t1.Share_Syaryo_Status == 0
                    && t1.Cancel_Datetime == null
                    && t2.Empty_Car_Day >= from_date
                    && t2.Empty_Car_Day < to_date
                group t1 by t2.Empty_Car_Day into g
                orderby g.Key ascending
                select new CountByDate { Date = g.Key, Count = g.Count() };
            return await b.ToListAsync();
        }

        /// <summary>
        /// 共有荷物数
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="from_date">開始日（含む）</param>
        /// <param name="to_date">終了日（含まない）</param>
        /// <returns>共有荷物数のリスト</returns>
        public async Task<IList<CountByDate>> Count_shareluggage(int company_id, int branch_id, DateTime from_date, DateTime to_date)
        {
            // 会社IDと支店IDに紐づく共有荷物の詳細を取得し、共有荷物の詳細の積日がfrom_date以上でto_date未満のものを取得し、
            // 積日でグループ化し、積日の昇順で並べ替え、共有荷物の数をカウントする
            IQueryable<CountByDate> b =
                from t1 in DbContext.Set<T_Share_Luggage>()
                join t2 in DbContext.Set<T_Share_Luggage_Detail>()
                    on new { id = t1.Share_Luggage_ID, order = t1.Share_Luggage_Latest_Order }
                    equals new { id = t2.Share_Luggage_ID, order = t2.Share_Luggage_Order }
                where t1.Share_Luggage_Status == 0
                    && t1.Cancel_Datetime == null
                    && t2.Tumi_Datetime >= from_date
                    && t2.Tumi_Datetime < to_date
                group t1 by t2.Tumi_Datetime into g
                orderby g.Key ascending
                select new CountByDate { Date = g.Key.Value, Count = g.Count() };
            return await b.ToListAsync();
        }

        /// <summary>
        /// 受注案件数
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="from_date">開始日（含む）</param>
        /// <param name="to_date">終了日（含まない）</param>
        /// <returns>受注案件数のリスト</returns>
        public async Task<IList<CountByDate>> Count_juchuanken(int company_id, int branch_id, DateTime from_date, DateTime to_date)
        {
            IEnumerable<JoinCompanyPortalDto> juchuAnken = await GetJuchuAnkensAsync(company_id, branch_id);
            IEnumerable<CountByDate> countJuchuAnken = from j in juchuAnken
                                  where j.renkeiAnkenPointS.PointDate >= from_date
                                  && j.renkeiAnkenPointS.PointDate < to_date
                                  group j by j.renkeiAnkenPointS.PointDate into g
                                  orderby g.Key ascending
                                  select new CountByDate { Date = g.Key.Value, Count = g.Count() };

            return countJuchuAnken.ToList();
        }

        /// <summary>
        /// 依頼案件数
        /// </summary>
        /// <param name="company_id">ログインしているユーザーの会社ID</param>
        /// <param name="branch_id">ログインしているユーザーの支店ID</param>
        /// <param name="from_date">開始日（含む）</param>
        /// <param name="to_date">終了日（含まない）</param>
        /// <returns>依頼案件数のリスト</returns>
        public async Task<IList<CountByDate>> Count_iraianken(int company_id, int branch_id, DateTime from_date, DateTime to_date)
        {
            IEnumerable<JoinCompanyPortalDto> iraiAnken = await GetJuchuAnkensAsync(company_id, branch_id);
            IEnumerable<CountByDate> countIraiAnken = from i in iraiAnken
                                  where i.renkeiAnkenPointS.PointDate >= from_date
                                  && i.renkeiAnkenPointS.PointDate < to_date
                                  group i by i.renkeiAnkenPointS.PointDate into g
                                  orderby g.Key ascending
                                  select new CountByDate { Date = g.Key.Value, Count = g.Count() };

            return countIraiAnken.ToList();
        }

        /// <summary>
        /// 依頼案件情報の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>依頼案件情報のリスト</returns>
        public async Task<IEnumerable<JoinCompanyPortalDto>> GetIraiAnkensAsync(int companyId, int branchId)
        {
            IQueryable<JoinCompanyPortalDto> builder = from tra in DbContext.Set<T_Renkei_Anken>()
                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                             on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                             equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }
                          where tra.Renkei_Anken_Status != 3

                          // T_Renkei_Anken_PointとSEKubun == "S"で結合
                          join trapS in DbContext.Set<T_Renkei_Anken_Point>()
                            on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                            equals new { id = trapS.Renkei_Anken_ID, order = trapS.Renkei_Anken_Order }
                          where trapS.SEKubun == "S"

                          // T_Renkei_Anken_PointとSEKubun == "E"で結合
                          join trapE in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapE.Renkei_Anken_ID, order = trapE.Renkei_Anken_Order }
                          where trapE.SEKubun == "E"

                          join trac in DbContext.Set<T_Renkei_Anken_Check>()
                             on tra.Renkei_Anken_ID equals trac.Renkei_Anken_ID into renkeiAnkenCheck
                          from tracNullable in renkeiAnkenCheck.DefaultIfEmpty()

                          join cug in DbContext.Set<M_CompanyUser_Group>()
                              on new { id = trad.TantouID, delFlg = false } equals new { id = cug.Group_ID, delFlg = cug.Del_Flg } into cugs
                          from cug in cugs.DefaultIfEmpty()

                          join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                              on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID into trass
                          from trasNullable in trass.DefaultIfEmpty()
                          where trasNullable == null || (trasNullable != null && trasNullable.Del_Datetime == null)

                          join c1 in DbContext.Set<M_Company>()
                              on tra.Company_ID equals c1.Renkei_Company_ID into c1s
                          from c1Nullable in c1s.DefaultIfEmpty()

                          join sss in DbContext.Set<T_Share_Syaryo_Secure>()
                                on tra.Renkei_Anken_ID equals sss.Renkei_Anken_ID into ssss
                          from sssNullable in ssss.DefaultIfEmpty()
                          where sssNullable == null || (sssNullable != null && sssNullable.Cancel_Datetime == null)

                          join c2 in DbContext.Set<M_Company>()
                              on sssNullable.Company_ID equals c2.Renkei_Company_ID into c2s
                          from c2Nullable in c2s.DefaultIfEmpty()

                          join sls in DbContext.Set<T_Share_Luggage_Secure>()
                              on tra.Renkei_Anken_ID equals sls.Renkei_Anken_ID into slss
                          from slsNullable in slss.DefaultIfEmpty()
                          where slsNullable == null || (slsNullable != null && slsNullable.Cancel_Datetime == null)

                          join sl in DbContext.Set<T_Share_Luggage>()
                              on slsNullable.Share_Luggage_ID equals sl.Share_Luggage_ID into sls
                          from slNullable in sls.DefaultIfEmpty()

                          join c3 in DbContext.Set<M_Company>()
                              on slsNullable.Company_ID equals c3.Renkei_Company_ID into c3s
                          from c3Nullable in c3s.DefaultIfEmpty()

                          where
                             (tra.Renkei_Anken_Kubun == 0 && tra.Company_ID == companyId && tra.Branch_ID == branchId) ||
                             (tra.Renkei_Anken_Kubun == 1 && sssNullable.Company_ID == companyId && sssNullable.Branch_ID == branchId)

                          orderby tra.Renkei_Anken_No ascending
                          select new JoinCompanyPortalDto
                          {
                              renkeiAnken = tra,
                              renkeiAnkenDetail = trad,
                              renkeiAnkenCheck = tracNullable,
                              renkeiAnkenPointE = trapE,
                              renkeiAnkenPointS = trapS,
                              renkeiAnkenSecure = trasNullable,
                              companyUserGroup = cug,
                              company1 = c1Nullable,
                              company2 = c2Nullable,
                              company3 = c3Nullable,
                              shareSyaryoSecure = sssNullable,
                              shareLuggageSecure = slsNullable,
                              shareLuggage = slNullable,
                          };
            return await builder.ToListAsync();
        }

        /// <summary>
        /// 受注案件情報の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>受注案件情報のリスト</returns>
        public async Task<IEnumerable<JoinCompanyPortalDto>> GetJuchuAnkensAsync(int companyId, int branchId)
        {
            IQueryable<JoinCompanyPortalDto> builder = from tra in DbContext.Set<T_Renkei_Anken>()
                          join trad in DbContext.Set<T_Renkei_Anken_Detail>()
                             on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                             equals new { id = trad.Renkei_Anken_ID, order = trad.Renkei_Anken_Order }

                          join trapS in DbContext.Set<T_Renkei_Anken_Point>()
                            on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                            equals new { id = trapS.Renkei_Anken_ID, order = trapS.Renkei_Anken_Order }
                          where trapS.SEKubun == "S"

                          // T_Renkei_Anken_PointとSEKubun == "S"で結合
                          join trapE in DbContext.Set<T_Renkei_Anken_Point>()
                              on new { id = tra.Renkei_Anken_ID, order = tra.Renkei_Anken_Latest_Order }
                              equals new { id = trapE.Renkei_Anken_ID, order = trapE.Renkei_Anken_Order }
                          where trapE.SEKubun == "E"

                          join trac in DbContext.Set<T_Renkei_Anken_Check>()
                             on tra.Renkei_Anken_ID equals trac.Renkei_Anken_ID into renkeiAnkenCheck
                          from tracNullable in renkeiAnkenCheck.DefaultIfEmpty()

                          join c1 in DbContext.Set<M_Company>()
                              on tra.Company_ID equals c1.Renkei_Company_ID into c1s
                          from c1Nullable in c1s.DefaultIfEmpty()

                          join sss in DbContext.Set<T_Share_Syaryo_Secure>()
                                on tra.Renkei_Anken_ID equals sss.Renkei_Anken_ID into ssss
                          from sssNullable in ssss.DefaultIfEmpty()
                          where sssNullable == null || (sssNullable != null && sssNullable.Cancel_Datetime == null)

                          join c2 in DbContext.Set<M_Company>()
                              on sssNullable.Company_ID equals c2.Renkei_Company_ID into c2s
                          from c2Nullable in c2s.DefaultIfEmpty()

                          join sls in DbContext.Set<T_Share_Luggage_Secure>()
                              on tra.Renkei_Anken_ID equals sls.Renkei_Anken_ID into slss
                          from slsNullable in slss.DefaultIfEmpty()
                          where slsNullable == null || (slsNullable != null && slsNullable.Cancel_Datetime == null)

                          join sl in DbContext.Set<T_Share_Luggage>()
                              on slsNullable.Share_Luggage_ID equals sl.Share_Luggage_ID into sls
                          from slNullable in sls.DefaultIfEmpty()

                          join c3 in DbContext.Set<M_Company>()
                              on slsNullable.Company_ID equals c3.Renkei_Company_ID into c3s
                          from c3Nullable in c3s.DefaultIfEmpty()

                          where
                             tra.Renkei_Anken_Status != 3 &&
                             ((tra.Renkei_Anken_Kubun == 1 && tra.Company_ID == companyId && tra.Branch_ID == branchId) ||
                             (tra.Renkei_Anken_Kubun == 0 && slsNullable.Company_ID == companyId && slsNullable.Branch_ID == branchId))

                          orderby tra.Renkei_Anken_No ascending
                          select new JoinCompanyPortalDto
                          {
                              renkeiAnken = tra,
                              renkeiAnkenDetail = trad,
                              renkeiAnkenCheck = tracNullable,
                              renkeiAnkenPointE = trapE,
                              renkeiAnkenPointS = trapS,
                              company1 = c1Nullable,
                              company2 = c2Nullable,
                              company3 = c3Nullable,
                              shareSyaryoSecure = sssNullable,
                              shareLuggageSecure = slsNullable,
                              shareLuggage = slNullable,
                          };
            return await builder.ToListAsync();
        }

        /// <summary>
        /// 案件秘密情報のデータ取得
        /// </summary>
        /// <param name="renkeiAnkenIDs">renkei_anken_idリスト</param>
        /// <returns>案件秘密情報のリスト</returns>
        public async Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs)
        {
            IQueryable<AnkenSecureDto> builder = from tras in DbContext.Set<T_Renkei_Anken_Secure>()
                          join trasc in DbContext.Set<T_Renkei_Anken_Secure_Check>()
                            on tras.Renkei_Anken_Secure_ID equals trasc.Renkei_Anken_Secure_ID into ankenSecureCheck
                          from trascNullable in ankenSecureCheck.DefaultIfEmpty()

                          join trasp in DbContext.Set<T_Renkei_Anken_Secure_Print>()
                            on tras.Renkei_Anken_Secure_ID equals trasp.Renkei_Anken_Secure_ID into ankenSecurePrint
                          from traspNullable in ankenSecurePrint.DefaultIfEmpty()
                          where
                            renkeiAnkenIDs.Contains(tras.Renkei_Anken_ID)
                            && tras.Del_Datetime == null
                          select new AnkenSecureDto
                          {
                              renkeiAnkenSecure = tras,
                              renkeiAnkenSecureCheck = trascNullable,
                              renkeiAnkenSecurePrint = traspNullable,
                          };
            List<AnkenSecureDto> result = await builder.ToListAsync();
            return result;
        }

        /// <summary>
        /// 車両データ取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>車両データのリスト</returns>
        public async Task<IEnumerable<JoinShareSyaryoDto>> GetSyaryosAsync(int companyId, int branchId)
        {
            IQueryable<JoinShareSyaryoDto> builder = from tss in DbContext.Set<T_Share_Syaryo>()
                          join tssd in DbContext.Set<T_Share_Syaryo_Detail>()
                             on new { id = tss.Share_Syaryo_ID, order = tss.Share_Syaryo_Latest_Order }
                             equals new { id = tssd.Share_Syaryo_ID, order = tssd.Share_Syaryo_Order }

                          join tsss in DbContext.Set<T_Share_Syaryo_Secure>().Where(t => t.Cancel_Datetime == null)
                             on tss.Share_Syaryo_ID equals tsss.Share_Syaryo_ID into shareSyaryoSecure
                          from tsssNullable in shareSyaryoSecure.DefaultIfEmpty()

                          join c in DbContext.Set<M_Company>()
                            on tss.Company_ID equals c.Renkei_Company_ID into company
                          from companyNullable in company.DefaultIfEmpty()

                          join cug in DbContext.Set<M_CompanyUser_Group>()
                            on new { id = tss.Tantou_Group_ID, delFlg = false }
                            equals new { id = cug.Group_ID, delFlg = cug.Del_Flg } into companyUserGroup
                          from companyUserGroupNullable in companyUserGroup.DefaultIfEmpty()

                          join cb in DbContext.Set<M_CompanyBranch>()
                            on new { id = tss.Company_ID, branchId = tss.Branch_ID }
                            equals new { id = cb.Company_ID, branchId = cb.Branch_ID } into companyBranch
                          from companyBranchNullable in companyBranch.DefaultIfEmpty()

                          where tss.Cancel_Datetime == null
                             && tss.Share_Syaryo_Status != 2
                          orderby tss.Share_Syaryo_No ascending
                          select new JoinShareSyaryoDto
                          {
                              shareSyaryo = tss,
                              shareSyaryoDetail = tssd,
                              shareSyaryoSecure = tsssNullable,
                              company = companyNullable,
                              companyUserGroup = companyUserGroupNullable,
                              companyBranch = companyBranchNullable
                          };
            List<JoinShareSyaryoDto> result = await builder.ToListAsync();
            return result;
        }

        /// <summary>
        /// 荷物データ取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>荷物データのリスト</returns>
        public async Task<IEnumerable<JoinShareLuggageDto>> GetLuggagesAsync(int companyId, int branchId)
        {
            IQueryable<JoinShareLuggageDto> builder = from tsl in DbContext.Set<T_Share_Luggage>()
                          join tsld in DbContext.Set<T_Share_Luggage_Detail>()
                             on new { id = tsl.Share_Luggage_ID, order = tsl.Share_Luggage_Latest_Order }
                             equals new { id = tsld.Share_Luggage_ID, order = tsld.Share_Luggage_Order }

                          join tsls in DbContext.Set<T_Share_Luggage_Secure>()
                             on tsl.Share_Luggage_ID equals tsls.Share_Luggage_ID into shareLuggageSecure
                          from tslsNulllable in shareLuggageSecure.DefaultIfEmpty()

                          join c in DbContext.Set<M_Company>()
                            on tsl.Company_ID equals c.Renkei_Company_ID into company
                          from companyNullable in company.DefaultIfEmpty()

                          join cug in DbContext.Set<M_CompanyUser_Group>()
                            on new { id = tsl.Tantou_Group_ID, delFlg = false }
                            equals new { id = cug.Group_ID, delFlg = cug.Del_Flg } into companyUserGroup
                          from companyUserGroupNullable in companyUserGroup.DefaultIfEmpty()

                          join cb in DbContext.Set<M_CompanyBranch>()
                            on new { id = tsl.Company_ID, branchId = tsl.Branch_ID }
                            equals new { id = cb.Company_ID, branchId = cb.Branch_ID } into companyBranch
                          from companyBranchNullable in companyBranch.DefaultIfEmpty()

                          where
                             tsl.Cancel_Datetime == null

                          orderby tsl.Share_Luggage_No ascending
                          select new JoinShareLuggageDto
                          {
                              shareLuggage = tsl,
                              shareLuggageDetail = tsld,
                              shareLuggageSecure = tslsNulllable,
                              company = companyNullable,
                              companyUserGroup = companyUserGroupNullable,
                              companyBranch = companyBranchNullable,
                          };
            List<JoinShareLuggageDto> result = await builder.ToListAsync();
            return result;
        }

        /// <summary>
        /// 共有車両数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>共有車両数</returns>
        public async Task<int> GetShareSyaryoCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tss in DbContext.Set<T_Share_Syaryo>()
                               join tssd in DbContext.Set<T_Share_Syaryo_Detail>()
                                  on new { id = tss.Share_Syaryo_ID, order = tss.Share_Syaryo_Latest_Order }
                                  equals new { id = tssd.Share_Syaryo_ID, order = tssd.Share_Syaryo_Order }
                               where
                                 tss.Company_ID == companyId
                                 && tss.Branch_ID == branchId
                                 && tss.Share_Syaryo_Status != 2
                                 && tssd.Empty_Car_Day == date

                               select tss).CountAsync();
            return count;
        }

        /// <summary>
        /// 共有車両確保数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>共有車両確保数</returns>
        public async Task<int> GetSyaSyaryoKakuhoCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tss in DbContext.Set<T_Share_Syaryo>()
                               join tssd in DbContext.Set<T_Share_Syaryo_Detail>()
                                  on new { id = tss.Share_Syaryo_ID, order = tss.Share_Syaryo_Latest_Order }
                                  equals new { id = tssd.Share_Syaryo_ID, order = tssd.Share_Syaryo_Order }
                               join tsss in DbContext.Set<T_Share_Syaryo_Secure>()
                                  on tss.Share_Syaryo_ID equals tsss.Share_Syaryo_ID
                               where tss.Company_ID == companyId
                                  && tss.Branch_ID == branchId
                                  && tss.Share_Syaryo_Status != 2
                                  && tssd.Empty_Car_Day == date
                               select tsss.Share_Syaryo_Secure_ID).CountAsync();
            return count;
        }

        /// <summary>
        /// 共有荷物数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物数</returns>
        public async Task<int> GetShareLuggageCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tsl in DbContext.Set<T_Share_Luggage>()
                               join tsld in DbContext.Set<T_Share_Luggage_Detail>()
                                  on new { id = tsl.Share_Luggage_ID, order = tsl.Share_Luggage_Latest_Order }
                                  equals new { id = tsld.Share_Luggage_ID, order = tsld.Share_Luggage_Order }
                               where
                                 tsl.Company_ID == companyId
                                 && tsl.Branch_ID == branchId
                                 && tsl.Share_Luggage_Status != 2
                                 && tsld.Tumi_Datetime != null && tsld.Tumi_Datetime.Value.Date == date

                               select tsl).CountAsync();
            return count;
        }

        /// <summary>
        /// 共有荷物確保数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物確保数</returns>
        public async Task<int> GetShareLuggageKakuhoCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tsl in DbContext.Set<T_Share_Luggage>()
                               join tsld in DbContext.Set<T_Share_Luggage_Detail>()
                                  on new { id = tsl.Share_Luggage_ID, order = tsl.Share_Luggage_Latest_Order }
                                  equals new { id = tsld.Share_Luggage_ID, order = tsld.Share_Luggage_Order }
                               join tsls in DbContext.Set<T_Share_Luggage_Secure>()
                                  on tsl.Share_Luggage_ID equals tsls.Share_Luggage_ID
                               where tsls.Company_ID == companyId
                                  && tsls.Branch_ID == branchId
                                  && tsl.Share_Luggage_Status == 1 
                                  && tsld.Tumi_Datetime != null && tsld.Tumi_Datetime.Value.Date == date.Date
                                  && tsls.Cancel_Datetime == null
                                  && tsl.Cancel_Datetime == null
                               select tsls.Share_Luggage_Secure_ID).CountAsync();
            return count;
        }

        /// <summary>
        /// 共有荷物未登録数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物未登録数</returns>
        public int GetShareLuggageMitourokuCnt(int companyId, int branchId, DateTime date)
        {
            return 0;
        }

        /// <summary>
        /// 依頼案件数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件数</returns>
        public async Task<int> GetIraiAnkenCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 0
                                  && tra.Renkei_Anken_Status != 3

                               select tra).CountAsync();
            return count;
        }

        /// <summary>
        /// 依頼案件車番確定数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件車番確定数</returns>
        public async Task<int> GetIraiAnkenSyabanKakuteiCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                                 on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 0
                                  && tra.Renkei_Anken_Status != 3
                                  && tras.Renkei_Anken_Secure_Status == 0

                               select tra).CountAsync();
            return count;
        }

        /// <summary>
        /// 依頼案件車番未確定数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件車番未確定数</returns>
        public async Task<int> GetIraiAnkenSyabanMikakuteiCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                                 on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 0
                                  && tra.Renkei_Anken_Status != 3
                                  && tras.Renkei_Anken_Secure_Status != 0

                               select tra).CountAsync();
            return count;
        }

        /// <summary>
        /// 受注案件数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件数</returns>
        public async Task<int> GetJuchuAnkenCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 1
                                  && tra.Renkei_Anken_Status != 3

                               select tra).CountAsync();
            return count;
        }

        /// <summary>
        /// 受注案件車番登録数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件車番登録数</returns>
        public async Task<int> GetJuchuAnkenSyabanTourokuCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                                 on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 1
                                  && tra.Renkei_Anken_Status != 3
                                  && tras != null
                                  && tras.Syasyu != null

                               select tra).CountAsync();
            return count;
        }

        /// <summary>
        /// 受注案件車番未登録数の取得
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件車番未登録数</returns>
        public async Task<int> GetJuchuAnkenSyabanMitourokuCnt(int companyId, int branchId, DateTime date)
        {
            int count = await (from tra in DbContext.Set<T_Renkei_Anken>()
                               join tras in DbContext.Set<T_Renkei_Anken_Secure>()
                                 on tra.Renkei_Anken_ID equals tras.Renkei_Anken_ID into renkeiAnkenSecure
                               from trasNullable in renkeiAnkenSecure.DefaultIfEmpty()
                               where
                                  tra.Company_ID == companyId
                                  && tra.Branch_ID == branchId
                                  && tra.Renkei_Anken_Kubun == 1
                                  && tra.Renkei_Anken_Status != 3
                                  && (trasNullable == null || (trasNullable != null && trasNullable.Syasyu == null))

                               select tra).CountAsync();
            return count;
        }
    }
}
