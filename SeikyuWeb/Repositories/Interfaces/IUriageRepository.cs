using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 売上リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IUriageRepository : IRepositoryBaseAsync<TUriage, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDリストに基づいて売上を非同期で取得します。
        /// </summary>
        /// <param name="ids">IDのリスト</param>
        /// <returns>売上のリストを含むタスク</returns>
        Task<IEnumerable<TUriage>> GetByIdsAsync(List<int> ids);
    }
}
