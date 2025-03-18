using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 共有荷物リポジトリ
    /// </summary>
    public class ShareLuggageRepository : RepositoryBaseAsync<T_Share_Luggage, ApplicationDbContext>, IShareLuggageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ShareLuggageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork) => _dbContext = dbContext;

        /// <summary>
        /// 荷物一覧取得
        /// </summary>
        /// <param name="q">クエリパラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物一覧</returns>
        public async Task<List<LuggagePrintDataDto>> Get_luggages(LuggageParamsDto q, int companyId, int branchId)
        {
            IQueryable<LuggagePrintDataDto> b =
                from s in _dbContext.T_Share_Luggages
                join d in _dbContext.T_Share_Luggage_Details
                    on new { i = s.Share_Luggage_ID, o = s.Share_Luggage_Latest_Order } equals new { i = d.Share_Luggage_ID, o = d.Share_Luggage_Order }
                join c in _dbContext.M_CompanyBranches
                    on new { companyID = s.Company_ID, branchID = s.Branch_ID } equals new { companyID = c.Company_ID, branchID = c.Branch_ID }
                join g in _dbContext.M_CompanyUser_Groups
                    on s.Tantou_Group_ID equals g.Group_ID into tmp
                from cug in tmp.DefaultIfEmpty()
                where
                    (s.Share_Luggage_Status == 0 || s.Share_Luggage_Status == 2)   // public
                    && s.Company_ID == companyId
                    && s.Branch_ID == branchId
                    && (q.tumiFromDate == null || d.Tumi_Datetime >= q.tumiFromDate.Value.Date)
                    && (q.tumiToDate == null || d.Tumi_Datetime < q.tumiToDate.Value.Date.AddDays(1))
                    && (q.tumi == null || d.Tumi_Address.StartsWith(q.tumi))
                    && (q.oroshi == null || d.Oroshi_Address.StartsWith(q.oroshi))
                    && (q.syasyu == 0 || d.Syasyu == q.syasyu)
                orderby s.Share_Luggage_No ascending
                select new LuggagePrintDataDto
                {
                    shareLuggageNo = s.Share_Luggage_No,
                    kokyakuName = d.KokyakuName,
                    unchin = d.Unchin,
                    tollMoney = d.Toll_Money,
                    tumiDatetime = d.Tumi_Datetime == null ? null : d.Tumi_Datetime.Value.ToString("yyyy/MM/dd"),
                    tumiAddress = d.Tumi_Address,
                    oroshiAddress = d.Oroshi_Address,
                    luggageWeight = d.Luggage_Weight,
                    syasyuDisplay = d.SyasyuDisplay,
                    equipmentDisplay = d.EquipmentDisplay,
                    tantouGroupName = cug == null ? null : cug.Display_Name,
                    luggageDisplay = d.LuggageDisplay,
                    tollKubun = d.Toll_Kubun
                };
            return await b.ToListAsync();
        }

        /// <summary>
        /// 荷物共有一覧取得
        /// </summary>
        /// <param name="dto">クエリパラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物共有一覧</returns>
        public async Task<IEnumerable<JoinShareLuggage>> GetShareLuggageAsync(LuggageParamsDto dto, int companyId, int branchId)
        {
            IQueryable<JoinShareLuggage> builder = 
                from s in _dbContext.T_Share_Luggages
                join d in _dbContext.T_Share_Luggage_Details
                    on new { id = s.Share_Luggage_ID, order = s.Share_Luggage_Latest_Order } equals new { id = d.Share_Luggage_ID, order = d.Share_Luggage_Order }
                join c in _dbContext.M_CompanyBranches
                    on new { companyID = s.Company_ID, branchID = s.Branch_ID } equals new { companyID = c.Company_ID, branchID = c.Branch_ID }
                join g in _dbContext.M_CompanyUser_Groups
                    on s.Tantou_Group_ID equals g.Group_ID into tmp
                from cug in tmp.DefaultIfEmpty()
                select new JoinShareLuggage
                {
                    ShareLuggage = s,
                    ShareLuggageDetail = d,
                    CompanyBranch = c,
                    CompanyUserGroup = cug
                };

            builder = builder.Where(item => (item.ShareLuggage.Share_Luggage_Status == 0 || item.ShareLuggage.Share_Luggage_Status == 2)
                                                            && item.ShareLuggage.Company_ID == companyId && item.ShareLuggage.Branch_ID == branchId);

            DateTime? fromDate = dto.tumiFromDate?.Date;
            DateTime? toDate = dto.tumiToDate?.Date.AddDays(1);

            if (dto.tumiFromDate != null)
            {
                builder = builder.Where(item => item.ShareLuggageDetail.Tumi_Datetime >= fromDate);
            }

            if (dto.tumiToDate != null)
            {
                builder = builder.Where(item => item.ShareLuggageDetail.Tumi_Datetime < toDate);
            }

            if (dto.tumi != null)
            {
                builder = builder.Where(item => item.ShareLuggageDetail.Tumi_Address.StartsWith(dto.tumi));
            }

            if (dto.oroshi != null)
            {
                builder = builder.Where(item => item.ShareLuggageDetail.Oroshi_Address.StartsWith(dto.oroshi));
            }

            if (dto.syasyu != 0)
            {
                builder = builder.Where(item => item.ShareLuggageDetail.Syasyu == dto.syasyu);
            }

            return await builder.OrderBy(item => item.ShareLuggage.Share_Luggage_No).ToListAsync();
        }

        /// <summary>
        /// 荷物共有詳細取得
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <returns>荷物共有詳細</returns>
        public async Task<JoinShareLuggage> GetDetailAsync(int id)
        {
            return await (from s in DbContext.Set<T_Share_Luggage>()
                          join d in DbContext.Set<T_Share_Luggage_Detail>()
                          on new { id = s.Share_Luggage_ID, order = s.Share_Luggage_Latest_Order } equals new { id = d.Share_Luggage_ID, order = d.Share_Luggage_Order }
                          join c in DbContext.Set<M_CompanyBranch>()
                          on new { companyID = s.Company_ID, branchID = s.Branch_ID } equals new { companyID = c.Company_ID, branchID = c.Branch_ID }
                          join e in DbContext.Set<M_CompanyUser_Group>()
                          on new { groupID = s.Tantou_Group_ID, delFlag = false } equals new { groupID = e.Group_ID, delFlag = e.Del_Flg } into groupsGroup
                          from companyUsergroupsnullable in groupsGroup.DefaultIfEmpty()
                          select new JoinShareLuggage { ShareLuggage = s, ShareLuggageDetail = d, CompanyBranch = c, CompanyUserGroup = companyUsergroupsnullable })
                    .Where(item => item.ShareLuggage.Share_Luggage_ID == id)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// SP_T_Share_LuggageでT_Share_Luggage作成・更新
        /// </summary>
        /// <param name="kubun">更新区分</param>
        /// <param name="luggage">荷物共有</param>
        /// <returns>荷物IDまたは最新受注</returns>
        public async Task<int?> CreateOrUpdateWithSpShareLuggage(SpShareLuggageKubun kubun, T_Share_Luggage luggage)
        {
            int result = await ExecuteScalarStoredProcedureAsync<int>(
                RenkeiDB.Common.SystemConstants.StoreProceduresName.SP_T_Share_Luggage,
                new SqlParameter("@KUBUN", kubun),
                new SqlParameter("@LUGGAGE_ID", luggage.Share_Luggage_ID),
                new SqlParameter("@LUGGAGE_NO", luggage.Share_Luggage_No),
                new SqlParameter("@LUGGAGE_STATUS", luggage.Share_Luggage_Status),
                new SqlParameter("@LUGGAGE_ORDER", luggage.Share_Luggage_Latest_Order),
                new SqlParameter("@COMPANY_ID", luggage.Company_ID),
                new SqlParameter("@BRANCH_ID", luggage.Branch_ID),
                new SqlParameter("@TANTOU_ID", luggage.Tantou_Group_ID),
                new SqlParameter("@CANCEL_DATETIME", luggage.Cancel_Datetime)
            );
            return result;
        }

        /// <summary>
        /// 荷物共有ステータス更新のための詳細取得
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <returns>荷物共有詳細</returns>
        public Task<JoinShareLuggage2> GetDetail2Async(int id)
        {
            return (from l in _dbContext.T_Share_Luggages
                    join d in _dbContext.T_Share_Luggage_Details
                    on new { id = l.Share_Luggage_ID, order = l.Share_Luggage_Latest_Order }
                    equals new { id = d.Share_Luggage_ID, order = d.Share_Luggage_Order }

                    join ls in _dbContext.T_Share_Luggage_Secures
                    on l.Share_Luggage_ID equals ls.Share_Luggage_ID into lss
                    from ls in lss.DefaultIfEmpty()

                    join c in _dbContext.M_Companies
                    on l.Company_ID equals c.Renkei_Company_ID

                    join a in _dbContext.T_Renkei_Ankens
                    on ls != null ? ls.Renkei_Anken_ID : -1 equals a.Renkei_Anken_ID into ankens
                    from a in ankens.DefaultIfEmpty()

                    join sya in _dbContext.M_Syaryos
                    on d.Syasyu equals sya.Syaryo_ID into syas
                    from sya in syas.DefaultIfEmpty()

                    select new JoinShareLuggage2
                    {
                        ShareLuggage = l,
                        ShareLuggageDetail = d,
                        ShareLuggageSecure = ls,
                        RenkeiAnken = a,
                        Company = c,
                        Syaryo = sya
                    })
                    .Where(item => item.ShareLuggage.Share_Luggage_ID == id)
                    .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 荷物共有ステータス更新のための詳細取得
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>result</returns>
        public async Task<IEnumerable<JoinShareLuggage2>> GetJoinShareLuggage2(int id)
        {
            IEnumerable<JoinShareLuggage2> data = await (from l in _dbContext.T_Share_Luggages
                                                         join d in _dbContext.T_Share_Luggage_Details
                                                         on new { id = l.Share_Luggage_ID, order = l.Share_Luggage_Latest_Order }
                                                         equals new { id = d.Share_Luggage_ID, order = d.Share_Luggage_Order }

                                                         join ls in _dbContext.T_Share_Luggage_Secures
                                                         on l.Share_Luggage_ID equals ls.Share_Luggage_ID into lss
                                                         from ls in lss.DefaultIfEmpty()

                                                         join c in _dbContext.M_Companies
                                                         on l.Company_ID equals c.Renkei_Company_ID

                                                         join a in _dbContext.T_Renkei_Ankens
                                                         on ls != null ? ls.Renkei_Anken_ID : -1 equals a.Renkei_Anken_ID into ankens
                                                         from a in ankens.DefaultIfEmpty()

                                                         join sya in _dbContext.M_Syaryos
                                                         on d.Syasyu equals sya.Syaryo_ID into syas
                                                         from sya in syas.DefaultIfEmpty()

                                                         where l.Share_Luggage_ID == id && ls.Cancel_Datetime == null

                                                         select new JoinShareLuggage2
                                                         {
                                                             ShareLuggage = l,
                                                             ShareLuggageDetail = d,
                                                             ShareLuggageSecure = ls,
                                                             RenkeiAnken = a,
                                                             Company = c,
                                                             Syaryo = sya
                                                         }).ToListAsync();

            return data;
        }
        /// <summary>
        /// 荷物データ取得
        /// </summary>
        /// <returns>荷物データのリスト</returns>
        public async Task<IEnumerable<JoinShareLuggageDto>> GetLuggagesAsync()
        {
            IQueryable<JoinShareLuggageDto> builder = from tsl in DbContext.Set<T_Share_Luggage>()

                          join tsld in DbContext.Set<T_Share_Luggage_Detail>()
                             on new { id = tsl.Share_Luggage_ID, order = tsl.Share_Luggage_Latest_Order }
                             equals new { id = tsld.Share_Luggage_ID, order = tsld.Share_Luggage_Order }

                          join tsls in DbContext.Set<T_Share_Luggage_Secure>()
                             on tsl.Share_Luggage_ID equals tsls.Share_Luggage_ID into shareLuggageSecure
                          from tslsNulllable in shareLuggageSecure.DefaultIfEmpty()

                          where
                             tsl.Cancel_Datetime == null
                             && tsl.Share_Luggage_Status == 0
                             && tslsNulllable.Cancel_Datetime == null

                          orderby tsl.Share_Luggage_No ascending
                          select new JoinShareLuggageDto
                          {
                              shareLuggage = tsl,
                              shareLuggageDetail = tsld,
                              shareLuggageSecure = tslsNulllable,
                          };
            List<JoinShareLuggageDto> result = await builder.ToListAsync();
            return result;
        }
    }
}
