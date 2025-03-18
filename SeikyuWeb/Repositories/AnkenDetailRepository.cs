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
    /// 案件詳細リポジトリ
    /// </summary>
    public class AnkenDetailRepository : RepositoryBaseAsync<TAnkenDetail, HaisyaContext>, IAnkenDetailRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public AnkenDetailRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDを持つ案件詳細のリストを取得する
        /// </summary>
        /// <param name="ids">案件IDのリスト</param>
        /// <returns>案件詳細のリスト</returns>
        public async Task<IEnumerable<TAnkenDetail>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.AnkenId)).ToListAsync();

        /// <summary>
        /// 指定したIDを持つ最新の案件詳細のリストを取得する
        /// </summary>
        /// <param name="ids">案件IDのリスト</param>
        /// <returns>最新の案件詳細のリスト</returns>
        public async Task<IEnumerable<TAnkenDetail>> GetLatestByIdsAsync(List<int> ids) {
            List<TAnkenDetail> data = await FindByCondition(u => ids.Contains(u.AnkenId)).ToListAsync();
            return data
                .GroupBy(u => u.AnkenId)
                .Select(g => g.OrderByDescending(u => u.AnkenOrder).FirstOrDefault());
        }
    }
}
