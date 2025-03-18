using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 支払完了リポジトリクラス
    /// </summary>
    public class CheckShitabaraiDoneRepository : RepositoryBaseAsync<TCheckShitabaraiDone, HaisyaContext>, ICheckShitabaraiDonerepository
    {
        public CheckShitabaraiDoneRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの支払完了を非同期で取得します。
        /// </summary>
        /// <param name="id">支払完了ID</param>
        /// <returns>支払完了</returns>
        public async Task<TCheckShitabaraiDone> GetByIdAsync(int id) => await FindByCondition(x => x.CheckShitabaraiId.Equals(id)).FirstOrDefaultAsync();
    }
}
