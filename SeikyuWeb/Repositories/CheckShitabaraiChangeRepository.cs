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
    /// 支払変更リポジトリクラス
    /// </summary>
    public class CheckShitabaraiChangeRepository : RepositoryBaseAsync<TCheckShitabaraiChange, HaisyaContext>, ICheckShitabaraiChangeRepository
    {
        public CheckShitabaraiChangeRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDを持つ変更内容入力のリスト取得
        /// </summary>
        /// <param name="checkShitabaraiIds">支払チェックIDリスト</param>
        /// <param name="uriageShiharaiIds">売上支払IDリスト</param>
        /// <returns>変更内容入力のリスト</returns>
        public async Task<IEnumerable<TCheckShitabaraiChange>> GetByIdsAsync(List<int> checkShitabaraiIds, List<int> uriageShiharaiIds)
            => await FindByCondition(c => checkShitabaraiIds.Contains(c.CheckShitabaraiId) && uriageShiharaiIds.Contains(c.UriageShiharaiId)).ToListAsync();
    }
}
