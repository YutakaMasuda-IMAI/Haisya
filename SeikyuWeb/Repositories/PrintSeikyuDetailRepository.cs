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
    /// 請求書詳細リポジトリクラス
    /// </summary>
    public class PrintSeikyuDetailRepository : RepositoryBaseAsync<TPrintSeikyuDetail, HaisyaContext>, IPrintSeikyuDetailRepository
    {
        public PrintSeikyuDetailRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 区分IDで請求書詳細を取得します。
        /// </summary>
        /// <param name="ids">請求書IDのリストです</param>
        /// <param name="idKubun">区分IDです</param>
        /// <returns>請求書詳細のリストです</returns>
        public async Task<IEnumerable<TPrintSeikyuDetail>> GetTPrintSeikyuDetailByKubunAsync(List<int> ids, int idKubun)
            => await FindByCondition(x => x.DataKubun.Equals(idKubun) && ids.Contains(x.PrintSeikyuId)).OrderBy(x => x.DataSort).ToListAsync();
    }
}
