using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 売上支払リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IUriageShitabaraiRepository : IRepositoryBaseAsync<TUriageShitabarai, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDリストに基づいて売上支払を非同期で取得します。
        /// </summary>
        /// <param name="ids">IDのリスト</param>
        /// <returns>売上支払のリストを含むタスク</returns>
        Task<IEnumerable<TUriageShitabarai>> GetByIdsAsync(List<int> ids);
    }
}
