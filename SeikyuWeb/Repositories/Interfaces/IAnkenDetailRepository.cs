using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 案件詳細リポジトリインターフェース
    /// </summary>
    public interface IAnkenDetailRepository : IRepositoryBaseAsync<TAnkenDetail, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDの案件詳細を非同期で取得します。
        /// </summary>
        /// <param name="ids">案件詳細IDのリスト</param>
        /// <returns>案件詳細のリスト</returns>
        Task<IEnumerable<TAnkenDetail>> GetByIdsAsync(List<int> ids);

        /// <summary>
        /// 指定されたIDの最新の案件詳細を非同期で取得します。
        /// </summary>
        /// <param name="ids">案件詳細IDのリスト</param>
        /// <returns>最新の案件詳細のリスト</returns>
        Task<IEnumerable<TAnkenDetail>> GetLatestByIdsAsync(List<int> ids);
    }
}
