using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 支払印刷リポジトリインターフェース
    /// </summary>
    public interface IPrintShitabaraiRepository : IRepositoryBaseAsync<TPrintShitabarai, HaisyaContext>
    {
        /// <summary>
        /// 支払IDで支払印刷を取得します。
        /// </summary>
        /// <param name="id">支払ID</param>
        /// <returns>支払印刷リスト</returns>
        Task<IEnumerable<TPrintShitabarai>> GetByCheckShitabaraiIdAsync(int id);

        /// <summary>
        /// すべての支払印刷を取得します。
        /// </summary>
        /// <returns>支払印刷リスト</returns>
        Task<IEnumerable<TPrintShitabarai>> GetAllAsync();
    }
}
