using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 支払帳票リポジトリクラス
    /// </summary>
    public class PrintShitabaraiRepository : RepositoryBaseAsync<TPrintShitabarai, HaisyaContext>, IPrintShitabaraiRepository
    {
        public PrintShitabaraiRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 支払チェックIDで支払帳票を取得します。
        /// </summary>
        /// <param name="id">支払チェックIDです</param>
        /// <returns>支払帳票のリストです</returns>
        public async Task<IEnumerable<TPrintShitabarai>> GetByCheckShitabaraiIdAsync(int id)
            => await FindByCondition(x => x.CheckShitabaraiId.Equals(id) && x.DelDatetime == null).ToListAsync();

        /// <summary>
        /// 全ての支払帳票を取得します。
        /// </summary>
        /// <returns>支払帳票のリストです</returns>
        public async Task<IEnumerable<TPrintShitabarai>> GetAllAsync()
            => await FindByCondition(x => x.DelDatetime == null).ToListAsync();
    }
}
