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
    /// 請求変更をチェックするリポジトリ
    /// </summary>
    public class CheckSeikyuChangeRepository : RepositoryBaseAsync<TCheckSeikyuChange, HaisyaContext>, ICheckSeikyuChangeRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public CheckSeikyuChangeRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDを持つ変更内容入力のリスト取得
        /// </summary>
        /// <param name="checkSeikyuIds">請求IDのリスト</param>
        /// <param name="uriageUnchinIds">売上運賃IDのリスト</param>
        /// <returns>変更内容入力のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyuChange>> GetByIdsAsync(List<int> checkSeikyuIds, List<int> uriageUnchinIds)
        {
            return await FindByCondition(c => checkSeikyuIds.Contains(c.CheckSeikyuId) && uriageUnchinIds.Contains(c.UriageUnchinId)).ToListAsync();
        }

        /// <summary>
        /// 指定したIDを持つ変更内容入力のリスト取得
        /// </summary>
        /// <param name="checkSeikyuIds">請求IDのリスト</param>
        /// <param name="uriageUnchinIds">売上運賃IDのリスト</param>
        /// <returns>変更内容入力のリスト</returns>
        public async Task<IEnumerable<TCheckSeikyuChange>> GetBySeikyuIdAndUnchinId(List<int> checkSeikyuIds, List<int> uriageUnchinIds)
        {
            IQueryable<TCheckSeikyuChange> query = from change in DbContext.Set<TCheckSeikyuChange>()
                        join unchin in DbContext.Set<TUriageUnchin>()
                            on change.UriageUnchinId equals unchin.UriageUnchinId
                        where uriageUnchinIds.Contains(change.UriageUnchinId) && checkSeikyuIds.Contains(change.CheckSeikyuId)
                        select change;

            return await query.ToListAsync();
        }
    } 
}
