using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 請求リポジトリクラス
    /// </summary>
    public class SeikyuRepository : RepositoryBaseAsync<TSeikyu, HaisyaContext>, ISeikyuRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public SeikyuRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDに基づいて請求情報を非同期で取得する
        /// </summary>
        /// <param name="id">請求ID</param>
        /// <returns>請求情報</returns>
        public async Task<TSeikyu> GetByIdAsync(int id)
             => await FindByCondition(x => x.SeikyuId.Equals(id) && x.DelDatetime == null).FirstOrDefaultAsync();

        /// <summary>
        /// 指定された会社IDに基づいて請求情報を非同期で取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>請求情報のリスト</returns>
        public async Task<IEnumerable<TSeikyu>> GetByCompanyAsync(int companyId)
            => await FindByCondition(x => x.CompanyId == companyId && x.DelDatetime == null).ToListAsync();

        /// <summary>
        /// 指定された担当IDに基づいて請求情報を非同期で取得する
        /// </summary>
        /// <param name="customerBranchId">支店ID</param>
        /// <returns>請求情報のリスト</returns>
        public async Task<IEnumerable<TSeikyu>> GetByBranchAsync(int customerBranchId)
            => await FindByCondition(x => x.CustomerBranchId == customerBranchId && x.DelDatetime == null).ToListAsync();

        /// <summary>
        /// 請求書一覧の取得
        /// </summary>
        /// <param name="printSeikyuId">プリント請求ID</param>
        /// <returns>請求書一覧</returns>
        public async Task<IEnumerable<InvoiceQueryDto>> GetInvoiceListByPrintSeikyuIdAsync(int printSeikyuId)
        {
            List<TPrintSeikyu> invoices = await (from tps in DbContext.Set<TPrintSeikyu>()
                                  join ts in DbContext.Set<TSeikyu>() on tps.SeikyuId equals ts.SeikyuId
                                  select tps
                              ).ToListAsync();
            List<InvoiceQueryDto> data = (from tps in invoices
                        join tpsd in DbContext.Set<TPrintSeikyuDetail>() on tps.PrintSeikyuId equals tpsd.PrintSeikyuId
                        join tpr in DbContext.Set<TPrintRireki>() on tps.PrintSeikyuId equals tpr.DataId into printRireki
                        from tpr in printRireki.DefaultIfEmpty(new TPrintRireki { PrintRirekiId = 0 })
                        join mct in DbContext.Set<MCustomerTantou>() on tpr.PrintRirekiId equals mct.TantouId into customerTantou
                        from mct in customerTantou.DefaultIfEmpty(new MCustomerTantou { CustomerBranchId = 0 })
                        where
                           tpsd.PrintSeikyuId == printSeikyuId
                        orderby tpsd.DataSort ascending, tpsd.DisplayDate ascending

                        //orderby tpsd.DisplayDate ascending

                        select new InvoiceQueryDto
                        {
                            printSeikyu = tps,
                            printSeikyuDetail = tpsd,
                            printRireki = tpr,
                            customerTantou = mct,
                        }
                        ).ToList();

            return data;
        }

        /// <summary>
        /// 非同期でTSeikyuのコレクションを取得する
        /// </summary>
        /// <returns>TSeikyuのコレクション</returns>
        public async Task<IEnumerable<TSeikyu>> GetTSeikyuAsync()
        {
            List<TSeikyu> data = await (from ts in DbContext.Set<TSeikyu>()
                              join tps in DbContext.Set<TPrintSeikyu>() on ts.SeikyuId equals tps.SeikyuId
                              where tps.DelDatetime == null
                              join tpr in DbContext.Set<TPrintRireki>() on tps.PrintSeikyuId equals tpr.DataId into printRireki
                              from tpr in printRireki.DefaultIfEmpty()
                              where ts.SeikyuKubun.Equals(SeikyuKubun.WEB)
                                    && ts.DelDatetime == null
                                    && tpr.DataId != tps.PrintSeikyuId
                              select ts).ToListAsync();
            return data;
        }

    }
}
