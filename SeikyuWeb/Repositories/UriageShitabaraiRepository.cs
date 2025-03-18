using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 売上支払リポジトリクラス
    /// </summary>
    public class UriageShitabaraiRepository : RepositoryBaseAsync<TUriageShitabarai, HaisyaContext>, IUriageShitabaraiRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public UriageShitabaraiRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDの売上支払を非同期で取得します。
        /// </summary>
        /// <param name="ids">売上支払IDのリスト</param>
        /// <returns>売上支払のリスト</returns>
        public async Task<IEnumerable<TUriageShitabarai>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.UriageShiharaiId)).ToListAsync();
    } 
}
