using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static SeikyuWeb.Common.SystemConstants;
using System;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 入金リポジトリクラス
    /// </summary>
    public class NyukinRepository : RepositoryBaseAsync<TNyukin, HaisyaContext>, INyukinRepository
    {
        public NyukinRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定した請求ID、請求月、締め日に基づいて入金リストを取得します。
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締め日</param>
        /// <returns>入金リスト</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<IEnumerable<TNyukin>> GetListBySeikyuIdAsync(int Seikyu_ID, DateTime Seikyu_Month, int Shime_Day)
        {
            List<TNyukin> data = (from tn in DbContext.Set<TNyukin>()
                        join ts in DbContext.Set<TSeikyu>() on tn.SeikyuId equals ts.SeikyuId
                        where ts.SeikyuId == Seikyu_ID
                        && tn.ProcessDate > Seikyu_Month.AddMonths(-1).AddDays(Shime_Day)
                        && tn.ProcessDate <= Seikyu_Month.AddDays(Shime_Day)
                        select tn
            ).ToList();
            return data;
        }
    }
}
