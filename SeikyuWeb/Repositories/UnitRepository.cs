using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// ユニットリポジトリクラス
    /// </summary>
    public class UnitRepository : RepositoryBaseAsync<MUnit, HaisyaContext>, IUnitRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public UnitRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定されたIDリストに基づいてユニットを非同期で取得する
        /// </summary>
        /// <param name="ids">ユニットIDのリスト</param>
        /// <returns>ユニットのリスト</returns>
        public async Task<List<MUnit>> GetByIdsAsync(List<int> ids)
        {
            return await FindByCondition(u => ids.Contains(u.UnitId) 
            && u.DelFlg.Equals(SystemConstants.DelFlag.NONE)).ToListAsync();
        }
    } 
}
