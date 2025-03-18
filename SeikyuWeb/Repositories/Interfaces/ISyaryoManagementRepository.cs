using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 車両管理リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ISyaryoManagementRepository : IRepositoryBaseAsync<MSyaryoManagement, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDリストに基づいて車両管理を非同期で取得します。
        /// </summary>
        /// <param name="ids">IDのリスト</param>
        /// <returns>車両管理のリストを含むタスク</returns>
        Task<IEnumerable<MSyaryoManagement>> GetByIdsAsync(List<int> ids);
    }
}
