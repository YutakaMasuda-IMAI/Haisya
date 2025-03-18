using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 日報リポジトリクラス
    /// </summary>
    public class NippouRepository : RepositoryBaseAsync<TNippou, HaisyaContext>, INippouRepository
    {
        public NippouRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDリストに基づいて日報情報を非同期で取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>日報情報のリスト</returns>
        public async Task<IEnumerable<TNippou>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.NippouId)).ToListAsync();
    }
}
