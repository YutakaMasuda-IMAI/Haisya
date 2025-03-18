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
    /// 車両管理リポジトリクラス
    /// </summary>
    public class SyaryoManagementRepository : RepositoryBaseAsync<MSyaryoManagement, HaisyaContext>, ISyaryoManagementRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public SyaryoManagementRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDリストに基づいて車両管理情報を非同期で取得する
        /// </summary>
        /// <param name="ids">車両管理IDのリスト</param>
        /// <returns>車両管理情報のリスト</returns>
        public async Task<IEnumerable<MSyaryoManagement>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.SyaryoManagementId)).ToListAsync();
    }
}
