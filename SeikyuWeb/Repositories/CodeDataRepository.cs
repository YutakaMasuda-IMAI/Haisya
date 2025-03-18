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
    /// コードデータリポジトリクラス
    /// </summary>
    public class CodeDataRepository : RepositoryBaseAsync<MCodeDatum, HaisyaContext>, ICodeDataRepository
    {
        public CodeDataRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// マスタ情報の取得
        /// </summary>
        /// <param name="id">コードID</param>
        /// <returns>マスタ情報のリスト</returns>
        public async Task<IEnumerable<MCodeDatum>> GetByCodeIdAsync(int id)
        {
            return await FindByCondition(x => x.CodeId.Equals(id) &&
            x.DelFlg.Equals(false)).OrderBy(x => x.SortOrder)
                .ToListAsync();
        }
    }
}
