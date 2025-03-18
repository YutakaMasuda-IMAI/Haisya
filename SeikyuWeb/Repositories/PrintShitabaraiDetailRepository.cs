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
    /// 支払帳票詳細リポジトリクラス
    /// </summary>
    public class PrintShitabaraiDetailRepository : RepositoryBaseAsync<TPrintShitabaraiDetail, HaisyaContext>, IPrintShitabaraiDetailRepository
    {
        public PrintShitabaraiDetailRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 区分IDで支払帳票詳細を取得します。
        /// </summary>
        /// <param name="ids">支払帳票IDのリストです</param>
        /// <param name="idKubun">区分IDです</param>
        /// <returns>支払帳票詳細のリストです</returns>
        public async Task<IEnumerable<TPrintShitabaraiDetail>> GetTShitabaraiDetailByKubunAsync(List<int> ids, int idKubun)
            => await FindByCondition(x => x.DataKubun.Equals(idKubun) && ids.Contains(x.PrintShitabaraiId)).OrderBy(x => x.DataSort).ToListAsync();
    }
}
