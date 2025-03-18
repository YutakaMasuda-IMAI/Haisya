using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 請求詳細をチェックするリポジトリ
    /// </summary>
    public class CheckSeikyuDetailRepository : RepositoryBaseAsync<TCheckSeikyuDetail, HaisyaContext>, ICheckSeikyuDetailRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public CheckSeikyuDetailRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定した請求IDを持つ請求詳細を取得する
        /// </summary>
        /// <param name="id">請求ID</param>
        /// <returns>請求詳細のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyuDetail>> GetByCheckSeikyuId(int id)
        {
            IQueryable<TCheckSeikyuDetail> query = from detail in DbContext.Set<TCheckSeikyuDetail>()
                        where detail.CheckSeikyuId == id
                        select detail;

            return await query.ToListAsync();
        }
    }
}
