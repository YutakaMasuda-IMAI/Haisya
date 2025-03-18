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
    /// 配車リポジトリクラス
    /// </summary>
    public class HaisyaRepository : RepositoryBaseAsync<THaisya, HaisyaContext>, IHaisyaRepository
    {
        public HaisyaRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDリストに基づいて配車情報を非同期で取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>配車情報のリスト</returns>
        public async Task<IEnumerable<THaisya>> GetByIdsAsync(List<int> ids)
           => await FindByCondition(u => ids.Contains(u.AnkenDisplayId)).ToListAsync();
    }
}
