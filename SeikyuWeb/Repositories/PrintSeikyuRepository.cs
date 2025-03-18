using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 請求書リポジトリクラス
    /// </summary>
    public class PrintSeikyuRepository : RepositoryBaseAsync<TPrintSeikyu, HaisyaContext>, IPrintSeikyuRepository
    {
        public PrintSeikyuRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 顧客の請求書を取得します。
        /// </summary>
        /// <param name="customerBranchId">支店ID</param>
        /// <param name="fromYm">開始年月です</param>
        /// <param name="toYm">終了年月です</param>
        /// <param name="zeiKubun">税区分の配列です</param>
        /// <param name="shimeDay">締め日です</param>
        /// <returns>請求書のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyu>> GetInvoiceForCustomer(int customerBranchId, DateTime? fromYm, DateTime? toYm, int[] zeiKubun, int? shimeDay)
        {
            List<TPrintSeikyu> data = await (from tps in DbContext.Set<TPrintSeikyu>()
                              join ts in DbContext.Set<TSeikyu>() on tps.SeikyuId equals ts.SeikyuId
                              where tps.CustomerBranchId == customerBranchId
                                && tps.DelDatetime == null
                                && (fromYm == null || tps.SeikyuMonth >= fromYm)
                                && (toYm == null || tps.SeikyuMonth <= toYm)
                                && (zeiKubun == null || zeiKubun.Contains(tps.ZeiKubun))
                                && (shimeDay == null ||
                                    (shimeDay == ShimeDate.QueryAll || (shimeDay != ShimeDate.QueryAll && tps.ShimeDay == shimeDay))
                                )
                              orderby tps.SeikyuMonth ascending, tps.PrintSeikyuId ascending
                              select tps
                              ).ToListAsync();
            if (shimeDay == ShimeDate.QueryAll)
            {
                data = data.Where(c => c.ShimeDay == DateTime.DaysInMonth(c.SeikyuMonth.Year, c.SeikyuMonth.Month)).ToList();
            }
            return data;
        }

        /// <summary>
        /// 会社の請求書を取得します。
        /// </summary>
        /// <param name="fromYm">開始年月です</param>
        /// <param name="toYm">終了年月です</param>
        /// <param name="zeiKubun">税区分の配列です</param>
        /// <param name="shimeDay">締め日です</param>
        /// <returns>請求書のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyu>> GetInvoiceForCompany(DateTime? fromYm, DateTime? toYm,
            int[] zeiKubun, int? shimeDay)
        {
            List<TPrintSeikyu> data = await (from tps in DbContext.Set<TPrintSeikyu>()
                    join ts in DbContext.Set<TSeikyu>() on tps.SeikyuId equals ts.SeikyuId
                    where tps.DelDatetime == null
                          && (fromYm == null || tps.SeikyuMonth >= fromYm)
                          && (toYm == null || tps.SeikyuMonth <= toYm)
                          && (zeiKubun == null || zeiKubun.Contains(tps.ZeiKubun))
                          && (shimeDay == null ||
                              (shimeDay == ShimeDate.QueryAll || (shimeDay != ShimeDate.QueryAll && tps.ShimeDay == shimeDay))
                          )
                    orderby tps.SeikyuMonth ascending, tps.PrintSeikyuId ascending
                    select tps
                ).ToListAsync();
            if (shimeDay == ShimeDate.QueryAll)
            {
                data = data.Where(c => c.ShimeDay == DateTime.DaysInMonth(c.SeikyuMonth.Year, c.SeikyuMonth.Month)).ToList();
            }
            return data;
        }
        /// <summary>
        /// 請求書一覧を取得します。
        /// </summary>
        /// <param name="idInt">ID（会社ID）/（担当ID）</param>
        /// <param name="isCompany">True:（会社ID）/False:（担当ID）</param>
        /// <param name="fromYm">開始年月です</param>
        /// <param name="toYm">終了年月です</param>
        /// <param name="zeiKubun">税区分の配列です</param>
        /// <param name="shimeDay">締め日です</param>
        /// <returns>請求書詳細のリストです</returns>
        public async Task<IEnumerable<InvoiceQueryDto>> GetInvoiceList(int idInt, bool isCompany, DateTime? fromYm, DateTime? toYm, int[] zeiKubun, int? shimeDay)
        {
            int customerBranchId = 0;
			if (!isCompany)
                customerBranchId = await GetBranchIdByCusomerTantouAsync(idInt);

            IEnumerable<TPrintSeikyu> invoices = isCompany ? await GetInvoiceForCompany(fromYm, toYm, zeiKubun, shimeDay) : await GetInvoiceForCustomer(customerBranchId, fromYm, toYm, zeiKubun, shimeDay);

            List<InvoiceQueryDto> data = (from tps in invoices
                        join tpsd in DbContext.Set<TPrintSeikyuDetail>() on tps.PrintSeikyuId equals tpsd.PrintSeikyuId
                        join ts in DbContext.Set<TSeikyu>() on tps.SeikyuId equals ts.SeikyuId
                        //join tsd in DbContext.Set<TSeikyuDetail>() on ts.SeikyuId equals tsd.SeikyuId
                        join tpr in DbContext.Set<TPrintRireki>() on tps.PrintSeikyuId equals tpr.DataId into printRireki
                        from tpr in printRireki.DefaultIfEmpty(new TPrintRireki { PrintRirekiId = 0 })
                        //join mcd in DbContext.Set<MCodeDatum>() on tps.PrintPattern.ToString() equals mcd.CodeData
                        join mcd2 in DbContext.Set<MCodeDatum>() on tps.ZeiKubun.ToString() equals mcd2.CodeData
                        join mct in DbContext.Set<MCustomerTantou>() on tpr.PrintUserId equals mct.TantouId into customerTantou
                        from mct in customerTantou.DefaultIfEmpty(new MCustomerTantou { CustomerBranchId = 0 })
                        where ts.SeikyuKubun == CheckKubun.WEB
                            && ts.DelDatetime == null
                            && (isCompany ? ts.CompanyId == idInt : ts.CustomerBranchId == customerBranchId)
							//&& mcd.CodeId == CodeId.TEN
							//&& new[] { CodeData.請求書.ToString(), CodeData.FIFTEEN.ToString() }.Contains(mcd.CodeData)
							&& mcd2.CodeId == CodeId.FIVE
                            && new[] { CodeData.ZERO.ToString(), CodeData.ONE.ToString() }.Contains(mcd2.CodeData)

                        select new InvoiceQueryDto
                        {
                            printSeikyu = tps,
                            printSeikyuDetail = tpsd,
                            seikyu = ts,
                            printRireki = tpr,
                            //codeDatum = mcd,
                            customerTantou = mct,
                        }
                        ).ToList();
            return data;
        }

        /// <summary>
        /// 担当IDから支店ID取得
        /// </summary>
        /// <param name="tantoID">担当ID</param>
        /// <returns>支店ID</returns>
        public async Task<int> GetBranchIdByCusomerTantouAsync(int tantoID)
        {
            int customerBranchId = 0;
            // 担当者IDから支店ID取得
            List<MCustomerTantou> customerTantous = await (from tps in DbContext.Set<MCustomerTantou>()
                                                           where tps.TantouId == tantoID
                                                           select tps
                                            ).ToListAsync();
            if (customerTantous.Count() > 0)
                customerBranchId = customerTantous.FirstOrDefault().CustomerBranchId;
            return customerBranchId;
        }

        /// <summary>
        /// 全ての請求書を取得します。
        /// </summary>
        /// <returns>請求書のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyu>> GetAllAsync()
        {
            IQueryable<TPrintSeikyu> q = from ps in DbContext.Set<TPrintSeikyu>()
                    join ts in DbContext.Set<TSeikyu>()
                        on ps.SeikyuId equals ts.SeikyuId
                    where ps.DelDatetime == null
                    select ps;
            return await q.ToListAsync();
        }

        /// <summary>
        /// チェック請求IDで請求書を取得します。
        /// </summary>
        /// <param name="checkSeikyuID">チェック請求IDです</param>
        /// <returns>請求書のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyu>> GetBycheckSeikyuIdAsync(int checkSeikyuID)
        {
            return await FindByCondition(x => x.CheckSeikyuId.Equals(checkSeikyuID) && x.DelDatetime == null).ToListAsync();
        }

        /// <summary>
        /// 印刷請求IDで請求書を取得します。
        /// </summary>
        /// <param name="printSeikyuID">印刷請求IDです</param>
        /// <returns>請求書のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyu>> GetByprintSeikyuIdAsync(int printSeikyuID)
        {
            return await FindByCondition(x => x.PrintSeikyuId.Equals(printSeikyuID) && x.DelDatetime == null).ToListAsync();
        }
    }
}
