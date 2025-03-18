using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EmptyCarDto;
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
    /// 共有車両リポジトリ
    /// </summary>
    public class ShareSyaryoRepository : RepositoryBaseAsync<T_Share_Syaryo, ApplicationDbContext>, IShareSyaryoRepository
    {

        public ShareSyaryoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 共有車両詳細を取得する
        /// </summary>
        /// <param name="id">共有車両ID</param>
        /// <param name="includes">関連エンティティのリスト</param>
        /// <returns>共有車両詳細</returns>
        public async Task<T_Share_Syaryo> GetDetailAsync(int id, List<string> includes = null)
        {
            includes ??= new List<string>();

            IQueryable<T_Share_Syaryo> query = FindByCondition(s => s.Share_Syaryo_ID == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 共有車両リストを取得する
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータ</param>
        /// <param name="includes">関連エンティティのリスト</param>
        /// <returns>共有車両リスト</returns>
        public async Task<IEnumerable<T_Share_Syaryo>> GetListAsync(ShareSyaryoListRequestDto paramRequests, List<string> includes = null)
        {
            includes ??= new List<string>();

            IQueryable<T_Share_Syaryo> query = FindAll().Include("Share_Syaryo_Detail");

            foreach (var include in includes)
            {
                if (include != "Share_Syaryo_Detail")
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(s => s.Share_Syaryo_Status == 0 || s.Share_Syaryo_Status == 2);

            if (paramRequests.emptyFromDate != null)
            {
                query = query.Where(s => s.Share_Syaryo_Detail.Empty_Car_Day >= DateTime.Parse(paramRequests.emptyFromDate));
            }

            if (paramRequests.emptyToDate != null)
            {
                query = query.Where(s => s.Share_Syaryo_Detail.Empty_Car_Day <= DateTime.Parse(paramRequests.emptyToDate));
            }

            if (paramRequests.emptyAddress != null)
            {
                query = query.Where(s => s.Share_Syaryo_Detail.Empty_Address.StartsWith(paramRequests.emptyAddress));
            }

            if (paramRequests.destAddress != null)
            {
                query = query.Where(s => s.Share_Syaryo_Detail.Dest_Address.StartsWith(paramRequests.destAddress));
            }

            if (paramRequests.syasyu != null)
            {
                query = query.Where(s => s.Share_Syaryo_Detail.Syasyu.ToString() == paramRequests.syasyu);
            }

            if (paramRequests.syaryoWeight != null && paramRequests.syaryoWeightType != null)
            {
                double syaryoWeight = double.Parse(paramRequests.syaryoWeight);

                switch (int.Parse(paramRequests.syaryoWeightType))
                {
                    case (int)TypeCompare.GreaterOrEqual:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Weight >= syaryoWeight);
                        break;
                    case (int)TypeCompare.LessOrEqual:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Weight <= syaryoWeight);
                        break;
                    case (int)TypeCompare.Equal:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Weight == syaryoWeight);
                        break;
                }
            }

            if (paramRequests.syaryoTotalWeight != null && paramRequests.syaryoTotalWeightType != null)
            {
                double syaryoTotalWeight = double.Parse(paramRequests.syaryoTotalWeight);

                switch (int.Parse(paramRequests.syaryoTotalWeightType))
                {
                    case (int)TypeCompare.GreaterOrEqual:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Total_Weight >= syaryoTotalWeight);
                        break;
                    case (int)TypeCompare.LessOrEqual:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Total_Weight <= syaryoTotalWeight);
                        break;
                    case (int)TypeCompare.Equal:
                        query = query.Where(s => s.Share_Syaryo_Detail.Syaryo_Total_Weight == syaryoTotalWeight);
                        break;
                }
            }

            query = query.OrderBy(s => s.Share_Syaryo_No);

            return await query.ToListAsync();
        }

        /// <summary>
        /// SP_T_Share_Syaryoの実行
        /// </summary>
        /// <param name="syaryoId">Share_Syaryo_ID</param>
        /// <param name="kubun">type update</param>
        /// <returns>
        /// kubun = 1: 車両ID
        /// kubun = 2: 車両共有の最新受注
        /// kubun = 3: 1 (true)
        /// kubun = 4: 1 (true)
        /// </returns>
        public async Task<int?> UpdateWithSpShareSyaryoAsync(int syaryoId, SpShareSyaryoKubun kubun)
        {
            int? result = await ExecuteScalarStoredProcedureAsync<int?>(
                SystemConstants.StoreProceduresName.SP_T_Share_Syaryo,
                new SqlParameter("@KUBUN", kubun),
                new SqlParameter("@SYARYO_ID", syaryoId)
            );
            return result;
        }

        /// <summary>
        /// SP_T_Share_Syaryoの実行
        /// </summary>
        /// <param name="kubun">更新区分</param>
        /// <param name="syaryoNo">共有車両番号</param>
        /// <param name="syaryoStatus">共有車両ステータス</param>
        /// <param name="syaryoOrder">共有車両注文</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="tantouId">担当ID</param>
        /// <returns>結果</returns>
        public async Task<int> CreateWithSpShareSyaryo(SpShareSyaryoKubun kubun, string syaryoNo, int syaryoStatus, int syaryoOrder, int companyId, int branchId, int tantouId)
        {
            int result = await ExecuteScalarStoredProcedureAsync<int>(
                SystemConstants.StoreProceduresName.SP_T_Share_Syaryo,
                new SqlParameter("@KUBUN", kubun),
                new SqlParameter("@SYARYO_NO", syaryoNo),
                new SqlParameter("@SYARYO_STATUS", syaryoStatus),
                new SqlParameter("@SYARYO_ORDER", syaryoOrder),
                new SqlParameter("@COMPANY_ID", companyId),
                new SqlParameter("@BRANCH_ID", branchId),
                new SqlParameter("@TANTOU_ID", tantouId)
            );
            return result;
        }

        /// <summary>
        /// SP_T_Share_Noの実行
        /// </summary>
        /// <param name="shareDate">共有日</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>共有番号</returns>
        public async Task<string> GetNoWithSpShareNo(DateTime shareDate, ZeroUme zeroUme)
        {
            string result = await ExecuteScalarStoredProcedureAsync<string>(
                SystemConstants.StoreProceduresName.SP_T_Share_No,
                new SqlParameter("@SHARE_DATE", shareDate),
                new SqlParameter("@ZERO_UME", zeroUme)
            );
            return result;
        }

        /// <summary>
        /// SP_T_Renkei_Anken_Noの実行
        /// </summary>
        /// <param name="shareDate">共有日</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>案件番号</returns>
        public async Task<string> Run_sp_t_renkei_anken_no(DateTime shareDate, ZeroUme zeroUme)
            => await ExecuteScalarStoredProcedureAsync<string>
            (
                SystemConstants.StoreProceduresName.SP_T_Renkei_Anken_No,
                new SqlParameter("@FROM_DATETIME", shareDate),
                new SqlParameter("@ZERO_UME", zeroUme)
            );

        /// <summary>
        /// SP_T_Share_Syaryoの実行
        /// </summary>
        /// <param name="kubun">更新区分</param>
        /// <param name="syaryoId">共有車両ID</param>
        /// <param name="syaryoStatus">共有車両ステータス</param>
        /// <returns>結果</returns>
        public async Task<int> UpdateStatusWithSpShareSyaryo(SpShareSyaryoKubun kubun, int? syaryoId, int? syaryoStatus)
        {
            int result = await ExecuteScalarStoredProcedureAsync<int>(
                SystemConstants.StoreProceduresName.SP_T_Share_Syaryo,
                new SqlParameter("@KUBUN", kubun),
                new SqlParameter("@SYARYO_ID", syaryoId),
                new SqlParameter("@SYARYO_STATUS", syaryoStatus)
            );
            return result;
        }

        /// <summary>
        /// 車両共有の取得
        /// </summary>
        /// <param name="id">共有車両ID</param>
        /// <returns>車両共有のリスト</returns>
        public async Task<IEnumerable<JoinEmptyCarDto>> GetJoinDataByIdAsync(int id)
        {
            List<JoinEmptyCarDto> data = await (from TSS in DbContext.Set<T_Share_Syaryo>()

                              join C in DbContext.Set<M_Company>()
                                    on TSS.Company_ID equals C.Renkei_Company_ID

                              join TSSD in DbContext.Set<T_Share_Syaryo_Detail>()
                                    on new { id = TSS.Share_Syaryo_ID, order = TSS.Share_Syaryo_Latest_Order } equals new { id = TSSD.Share_Syaryo_ID, order = TSSD.Share_Syaryo_Order }

                              join S in DbContext.Set<M_Syaryo>()
                                    on TSSD.Syasyu equals S.Syaryo_ID

                              join TSSS in DbContext.Set<T_Share_Syaryo_Secure>()
                                    on TSS.Share_Syaryo_ID equals TSSS.Share_Syaryo_ID
                                    into TSSSs
                              from TSSS in TSSSs.DefaultIfEmpty()

                              join TRA in DbContext.Set<T_Renkei_Anken>()
                                    on TSSS.Renkei_Anken_ID equals TRA.Renkei_Anken_ID
                                    into TRAs
                              from TRA in TRAs.DefaultIfEmpty()

                              where TSS.Share_Syaryo_ID.Equals(id)

                              select new JoinEmptyCarDto
                              {
                                  shareSyaryo = TSS,
                                  shareSyaryoDetail = TSSD,
                                  shareSyaryoSecure = TSSS,
                                  renkeiAnken = TRA,
                                  mCompany = C,
                                  mSyaryo = S,
                              }
                             ).ToListAsync();
            return data;
        }

        /// <summary>
        /// 空車車両確保の取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>空車車両確保のリスト</returns>
        public async Task<IEnumerable<JoinShareSyaryoDto>> GetKeepEmptyCarAsync(int companyId, int branchId)
        {
            IQueryable<JoinShareSyaryoDto> builder = from tss in DbContext.Set<T_Share_Syaryo>()
                          join tssd in DbContext.Set<T_Share_Syaryo_Detail>()
                             on new { id = tss.Share_Syaryo_ID, order = tss.Share_Syaryo_Latest_Order }
                             equals new { id = tssd.Share_Syaryo_ID, order = tssd.Share_Syaryo_Order }

                          join tsss in DbContext.Set<T_Share_Syaryo_Secure>()
                             on tss.Share_Syaryo_ID equals tsss.Share_Syaryo_ID

                          join c in DbContext.Set<M_Company>()
                            on tss.Company_ID equals c.Renkei_Company_ID into company
                          from companyNullable in company.DefaultIfEmpty()

                          where tss.Cancel_Datetime == null
                             && tss.Share_Syaryo_Status != 2
                             && tsss.Company_ID == companyId
                             && tsss.Branch_ID == branchId
                             && tsss.Cancel_Datetime == null
                          orderby tss.Share_Syaryo_No ascending
                          select new JoinShareSyaryoDto
                          {
                              shareSyaryo = tss,
                              shareSyaryoDetail = tssd,
                              shareSyaryoSecure = tsss,
                              company = companyNullable
                          };
            List<JoinShareSyaryoDto> result = await builder.ToListAsync();
            return result;
        }
    }
}
