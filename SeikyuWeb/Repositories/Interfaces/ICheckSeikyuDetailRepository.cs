using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckSeikyuDetailエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckSeikyuDetailRepository : IRepositoryBaseAsync<TCheckSeikyuDetail, HaisyaContext>
    {
        /// <summary>
        /// CheckSeikyuIdでTCheckSeikyuDetailを取得します。
        /// </summary>
        /// <param name="id">CheckSeikyuのID</param>
        /// <returns>TCheckSeikyuDetailエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyuDetail>> GetByCheckSeikyuId(int id);
    }
}
