using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 支払詳細リポジトリクラス
    /// </summary>
    public class CheckShitabaraiDetailRepository : RepositoryBaseAsync<TCheckShitabaraiDetail, HaisyaContext>, ICheckShitabaraiDetailRepository
    {
        public CheckShitabaraiDetailRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定した支払チェックIDの支払詳細リストを取得します。
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>支払詳細リスト</returns>
        public async Task<IEnumerable<TCheckShitabaraiDetail>> GetByCheckShitabaraiId(int id)
        {
            IQueryable<TCheckShitabaraiDetail> query = from detail in DbContext.Set<TCheckShitabaraiDetail>()
                        join uriage in DbContext.Set<TUriageShitabarai>()
                            on detail.UriageShiharaiId equals uriage.UriageShiharaiId
                        where detail.CheckShitabaraiId == id
                        orderby detail.UriageShiharaiId
                        select detail;

            return await query.ToListAsync();
        }
    }
}
