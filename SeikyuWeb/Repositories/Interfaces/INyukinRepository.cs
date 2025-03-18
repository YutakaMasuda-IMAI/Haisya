using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 入金リポジトリインターフェース
    /// </summary>
    public interface INyukinRepository : IRepositoryBaseAsync<TNyukin, HaisyaContext>
    {
        /// <summary>
        /// 請求ID、請求月、締め日で入金リストを取得します。
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締め日</param>
        /// <returns>入金リスト</returns>
        Task<IEnumerable<TNyukin>> GetListBySeikyuIdAsync(int Seikyu_ID, DateTime Seikyu_Month, int Shime_Day);
    }
}
