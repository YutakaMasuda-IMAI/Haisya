using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemEnums;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 請求チェックリポジトリクラス
    /// </summary>
    public class CheckSeikyuRepository : RepositoryBaseAsync<TCheckSeikyu, HaisyaContext>, ICheckSeikyuRepository
    {
        public CheckSeikyuRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 請求書の取得（業者）
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>請求書のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyu>> GetBillingForCompany(int companyId)
        {
            return await FindByCondition(x => x.CompanyId.Equals(companyId)).ToListAsync();
        }

        /// <summary>
        /// 請求書の取得（荷主）
        /// </summary>
        /// <param name="tantouId">担当者ID</param>
        /// <returns>請求書のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyu>> GetBillingForCustomer(int tantouId)
        {
            MCustomerTantou company = await (from CT in DbContext.Set<MCustomerTantou>()
                                 where CT.TantouId == tantouId
                                 select CT).FirstOrDefaultAsync();

            return await FindByCondition(x => x.CustomerBranchId.Equals(company.CustomerBranchId)).ToListAsync();
        }

        /// <summary>
        /// 請求書問合せ一覧の取得
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="status">ステータス配列</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>請求書問合せ一覧</returns>
        public async Task<IEnumerable<SeikyuDto>> GetBillingInqueryList(int kubun, int[] status, int idInt, bool isCompany = false)
        {
            IEnumerable<TCheckSeikyu> billings = isCompany ? await GetBillingForCompany(idInt) : await GetBillingForCustomer(idInt);

            List<SeikyuDto> data = (from tcs in billings
                        join tcsd in DbContext.Set<TCheckSeikyuDetail>() on tcs.CheckSeikyuId equals tcsd.CheckSeikyuId
                        join tcsc in DbContext.Set<TCheckSeikyuChange>()
                            on new { tcsd.CheckSeikyuId, tcsd.UriageUnchinId }
                            equals new { tcsc.CheckSeikyuId, tcsc.UriageUnchinId } into seikyuChange
                        from tcsc in seikyuChange.DefaultIfEmpty()
                        join tuu in DbContext.Set<TUriageUnchin>() on tcsd.UriageUnchinId equals tuu.UriageUnchinId
                        join tcd in DbContext.Set<TCheckSeikyuDone>()
                            on tcs.CheckSeikyuId equals tcd.CheckSeikyuId into seikyuDone
                        from tcd in seikyuDone.DefaultIfEmpty(new TCheckSeikyuDone { CheckUser = 0 })
                        join mct in DbContext.Set<MCustomerTantou>() on tcd.CheckUser equals mct.TantouId into customerTantou
                        from mct in customerTantou.DefaultIfEmpty(new MCustomerTantou { CustomerBranchId = 0 })
                        join mcb in DbContext.Set<MCustomerBranch>() on mct.CustomerBranchId equals mcb.CustomerBranchId into customerBranch
                        from mcb in customerBranch.DefaultIfEmpty(new MCustomerBranch { CustomerBranchId = 0 })
                        join mcuc in DbContext.Set<MCustomerUriageCalc>() on tcs.CustomerBranchId equals mcuc.CustomerBranchId into customerUriageCalc
                        from mcuc in customerUriageCalc.DefaultIfEmpty(new MCustomerUriageCalc { CustomerBranchId = 0 })
                        where
                            tcs.CheckKubun == (int)CheckKubun.WEB
                            && tcs.DelDatetime == null
                            && status.Contains(tcs.CheckStatus)
                            && tcsd.SeikyuUnchin is not null
                            && tcsd.Tatekaekin is not null
                            && (mct != null && mct.DelFlg == false)
                        orderby tcs.SeikyuMonth ascending
                        select new SeikyuDto
                        {
                            checkSeikyu = tcs,
                            detail = tcsd,
                            change = tcsc,
                            uriageUnchin = tuu,
                            done = tcd,
                            customerTantou = mct,
                            customerBranch = mcb,
                            customerUriageCalc = mcuc,
                        })
                        .ToList();

            return data;
        }

        /// <summary>
        /// 請求先担当者の取得
        /// </summary>
        /// <param name="checkSeikyuIds">請求チェックID配列</param>
        /// <returns>請求先担当者データ</returns>
        public async Task<IEnumerable<SeikyuTantouQueryDto>> GetSeikyuTantou(int[] checkSeikyuIds)
        {
            List<SeikyuTantouQueryDto> data = await (from tcs in DbContext.Set<TCheckSeikyu>()
                              join mcb in DbContext.Set<MCustomerBranch>() on tcs.CustomerBranchId equals mcb.CustomerBranchId
                              join mcug in DbContext.Set<MCompanyUserGroup>() on mcb.SeikyuTantouId equals mcug.GroupId
                              join mcugu in DbContext.Set<MCompanyUserGroupUser>() on mcug.GroupId equals mcugu.GroupId
                              join mcu in DbContext.Set<MCompanyUser>() on mcugu.UserId equals mcu.UserId

                              where checkSeikyuIds.Contains(tcs.CheckSeikyuId)
                                  && mcug.DelFlg == false
                                  && mcugu.DelFlg == false
                                  && mcu.DelFlg == false

                              select new SeikyuTantouQueryDto
                              {
                                  checkSeikyu = tcs,
                                  mCustomerBranch = mcb,
                                  mCompanyUserGroup = mcug,
                                  mCompanyUserGroupUser = mcugu,
                                  mCompanyUser = mcu,
                              })
                    .ToListAsync();
            return data;
        }

        /// <summary>
        /// 支払先担当者の取得
        /// </summary>
        /// <param name="checkSeikyuIds">請求チェックID配列</param>
        /// <returns>支払先担当者データ</returns>
        public async Task<IEnumerable<ShiharaiTantouQueryDto>> GetShiharaiTantou(int[] checkSeikyuIds)
        {
            List<ShiharaiTantouQueryDto> data = await (from tcs in DbContext.Set<TCheckSeikyu>()
                              join mcb in DbContext.Set<MCustomerBranch>() on tcs.CustomerBranchId equals mcb.CustomerBranchId
                              join mcug in DbContext.Set<MCompanyUserGroup>() on mcb.ShiharaiTantouId equals mcug.GroupId
                              join mcugu in DbContext.Set<MCompanyUserGroupUser>() on mcug.GroupId equals mcugu.GroupId
                              join mcu in DbContext.Set<MCompanyUser>() on mcugu.UserId equals mcu.UserId

                              where checkSeikyuIds.Contains(tcs.CheckSeikyuId)
                                  && mcug.DelFlg == false
                                  && mcugu.DelFlg == false
                                  && mcu.DelFlg == false

                              select new ShiharaiTantouQueryDto
                              {
                                  checkSeikyu = tcs,
                                  mCustomerBranch = mcb,
                                  mCompanyUserGroup = mcug,
                                  mCompanyUserGroupUser = mcugu,
                                  mCompanyUser = mcu,
                              })
                .ToListAsync();
            return data;
        }

        public async Task<TCheckSeikyu> GetByIdAsync(int id)
            => await FindByCondition(x => x.CheckSeikyuId.Equals(id)).FirstOrDefaultAsync();

        /// <summary>
        /// ログイン情報取得（業者）からログインした場合：
        /// T_Check_Seikyu．Company_ID＝M_CompanyUser．Company_ID（シート4で取得した値）
        /// T_Check_Seikyu．Del_Datetime＝Null
        /// T_Check_Seikyu．Check_Status≠4：承認済
        /// </summary>
        /// <param name="id">会社ID</param>
        /// <returns>請求書のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyu>> GetByCompanyIdAsync(int id)
            => await FindByCondition(x => x.CompanyId == id && x.DelDatetime == null && x.CheckStatus != 4).ToListAsync();

        /// <summary>
        /// ログイン情報取得（業者以外）からログインした場合：
        /// T_Check_Seikyu．Cusomer_Branch_ID＝
        /// 　　M_Customer_Tantou．Tantou_ID（シート3で取得した値）に該当するCustomer_Branch_ID
        /// T_Check_Seikyu．Del_Datetime＝Null
        /// T_Check_Seikyu．Check_Status≠4：承認済
        /// </summary>
        /// <param name="customerBranchId">支店ID</param>
        /// <returns>請求書のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyu>> GetByCustomerBranchIdAsync(int customerBranchId)
            => await FindByCondition(x => x.CustomerBranchId == customerBranchId && x.DelDatetime == null && x.CheckStatus != 4).ToListAsync();

        /// <summary>
        /// CheckSeikyu取得
        /// </summary>
        /// <param name="id">CheckSeikyuId</param>
        /// <returns>CheckSeikyuデータ</returns>
        public async Task<TCheckSeikyu> GetCheckSeikyuByIdAsync(int id)
        {
            return await (from tcc in DbContext.Set<TCheckSeikyu>()
                          join mcuc in DbContext.Set<MCustomerUriageCalc>()
                          on tcc.CustomerBranchId equals mcuc.CustomerBranchId into customerUriageCalc
                          from mcuc in customerUriageCalc.DefaultIfEmpty()
                          where tcc.CheckSeikyuId == id
                          select tcc).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get the list of TCheckSeikyu asynchronously.
        /// </summary>
        /// <returns>The list of TCheckSeikyu.</returns>
        public async Task<IEnumerable<TCheckSeikyu>> GetTCheckSeikyuAsync()
        {
            return await FindByCondition(x => x.CheckKubun.Equals(SystemConstants.CheckKubun.WEB)
                && x.DelDatetime == null && (x.CheckStatus.Equals(SystemConstants.CheckStatus.発行済)
                || x.CheckStatus.Equals(SystemConstants.CheckStatus.確認中))).ToListAsync();
        }
    }
}
