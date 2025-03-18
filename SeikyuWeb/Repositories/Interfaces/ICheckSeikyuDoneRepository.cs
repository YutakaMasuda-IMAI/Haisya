using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckSeikyuDoneエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckSeikyuDoneRepository : IRepositoryBaseAsync<TCheckSeikyuDone, HaisyaContext>
    {
        /// <summary>
        /// IDでTCheckSeikyuDoneを取得します。
        /// </summary>
        /// <param name="id">TCheckSeikyuDoneのID</param>
        /// <returns>TCheckSeikyuDoneエンティティ</returns>
        Task<TCheckSeikyuDone> GetByIdAsync(int id);
    }
}
