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
    /// 売上リポジトリクラス
    /// </summary>
    public class UriageRepository : RepositoryBaseAsync<TUriage, HaisyaContext>, IUriageRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public UriageRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDの売上を非同期で取得します。
        /// </summary>
        /// <param name="ids">売上IDのリスト</param>
        /// <returns>売上のリスト</returns>
        public async Task<IEnumerable<TUriage>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.UriageId)).ToListAsync();
    }
}
