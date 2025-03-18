using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckShitabaraiDetailエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckShitabaraiDetailRepository : IRepositoryBaseAsync<TCheckShitabaraiDetail, HaisyaContext>
    {
        /// <summary>
        /// CheckShitabaraiIdでTCheckShitabaraiDetailを取得します。
        /// </summary>
        /// <param name="id">CheckShitabaraiのID</param>
        /// <returns>TCheckShitabaraiDetailエンティティのリスト</returns>
        Task<IEnumerable<TCheckShitabaraiDetail>> GetByCheckShitabaraiId(int id);
    }
}
