using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckShitabaraiDoneエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckShitabaraiDonerepository : IRepositoryBaseAsync<TCheckShitabaraiDone, HaisyaContext>
    {
        /// <summary>
        /// IDでTCheckShitabaraiDoneを取得します。
        /// </summary>
        /// <param name="id">TCheckShitabaraiDoneのID</param>
        /// <returns>TCheckShitabaraiDoneエンティティ</returns>
        Task<TCheckShitabaraiDone> GetByIdAsync(int id);
    }
}
