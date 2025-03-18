using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 日報リポジトリインターフェース
    /// </summary>
    public interface INippouRepository : IRepositoryBaseAsync<TNippou, HaisyaContext>
    {
        /// <summary>
        /// IDリストで日報を取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>日報リスト</returns>
        Task<IEnumerable<TNippou>> GetByIdsAsync(List<int> ids);
    }
}
