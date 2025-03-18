using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 配車リポジトリインターフェース
    /// </summary>
    public interface IHaisyaRepository : IRepositoryBaseAsync<THaisya, HaisyaContext>
    {
        /// <summary>
        /// IDリストで配車を取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>配車リスト</returns>
        Task<IEnumerable<THaisya>> GetByIdsAsync(List<int> ids);
    }
}
