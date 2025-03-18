using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    public class PortalInfoRepository : RepositoryBaseAsync<TPortalInfo, HaisyaContext>, IPortalInfoRepository
    {
        public PortalInfoRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// ログイン情報に該当するお知らせ情報を取得する（業者）
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TPortalInfo>> GetNotificationContractor(int companyId)
        {
            return await FindByCondition(x => x.CompanyId.Equals(companyId)
            && x.PortalKubun.Equals(SystemConstants.PortalKubun.請求WEB)
            && x.LimitDate.Value.Date >= DateTime.Now.Date
            && x.DisplayFlg.Equals(SystemConstants.DisplayFlag.NONE)).ToListAsync();
        }

        /// <summary>
        ///  ログイン情報に該当するお知らせ情報を取得する（業者以外）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TPortalInfo>> GetNotificationWithoutContractor(int userId)
        {
            return await FindByCondition(x => x.UserId.Equals(userId)
            && x.PortalKubun.Equals(SystemConstants.PortalKubun.請求WEB)
            && x.LimitDate.Value.Date >= DateTime.Now.Date
            && x.DisplayFlg.Equals(SystemConstants.DisplayFlag.NONE)).ToListAsync();
        }

        /// <summary>
        /// ポータル情報の取得（業者）
        /// </summary>
        /// <param name="printParameterIds"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintIdAndCompanyId(List<int> printParameterIds, int companyId)
        {
            return await FindByCondition(x => printParameterIds.Contains(x.PrintId)
            && x.PortalKubun.Equals(SystemConstants.PortalKubun.請求WEB)
            && x.LimitDate >= DateTime.Now.Date
            //&& x.DelDateTime == null
            && x.CompanyId.Equals(companyId)).ToListAsync();
        }

        /// <summary>
        /// ポータル情報の取得（荷主）
        /// </summary>
        /// <param name="printParameterIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintIdAndCustomerTantouId(List<int> printParameterIds, int userId)
        {
            return await FindByCondition(x => printParameterIds.Contains(x.PrintId)
            && x.PortalKubun.Equals(SystemConstants.PortalKubun.請求WEB)
            && x.LimitDate >= DateTime.Now.Date
            //&& x.DelDateTime == null
            && x.UserId.Equals(userId)).ToListAsync();
        }

        /// <summary>
        /// ポータル情報リストの取得
        /// </summary>
        /// <param name="ids">List ids</param>
        /// <returns></returns>
        public async Task<IEnumerable<TPortalInfo>> GetAllByIdsAsync(List<int> ids)
        {
            return await FindByCondition(x => ids.Contains(x.PortalInfoId)).ToListAsync();
        }

        /// <summary>
        /// ポータル情報 ID とパラメータ値でポータル情報を取得
        /// </summary>
        /// <param name="id">ポータル情報id</param>
        /// <returns>TPortalInfo</returns>
        public async Task<TPortalInfo> GetNotificationById(int id)
        {
            return await FindByCondition(x => x.PortalInfoId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
