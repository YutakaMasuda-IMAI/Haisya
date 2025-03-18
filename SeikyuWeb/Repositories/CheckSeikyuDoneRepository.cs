using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 請求完了リポジトリクラス
    /// </summary>
    public class CheckSeikyuDoneRepository : RepositoryBaseAsync<TCheckSeikyuDone, HaisyaContext>, ICheckSeikyuDoneRepository
    {
        public CheckSeikyuDoneRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの請求完了を非同期で取得します。
        /// </summary>
        /// <param name="id">請求完了ID</param>
        /// <returns>請求完了</returns>
        public async Task<TCheckSeikyuDone> GetByIdAsync(int id)
        {
            return await FindByCondition(x => x.CheckSeikyuId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
