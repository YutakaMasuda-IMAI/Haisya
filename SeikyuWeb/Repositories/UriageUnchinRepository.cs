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
    /// 売上運賃リポジトリクラス
    /// </summary>
    public class UriageUnchinRepository : RepositoryBaseAsync<TUriageUnchin, HaisyaContext>, IUriageUnchinRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public UriageUnchinRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDの売上運賃を非同期で取得します。
        /// </summary>
        /// <param name="ids">売上運賃IDのリスト</param>
        /// <returns>売上運賃のリスト</returns>
        public async Task<IEnumerable<TUriageUnchin>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.UriageUnchinId)).ToListAsync();
    } 
}
