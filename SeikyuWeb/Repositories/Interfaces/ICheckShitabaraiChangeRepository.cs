using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckShitabaraiChangeエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckShitabaraiChangeRepository : IRepositoryBaseAsync<TCheckShitabaraiChange, HaisyaContext>
    {
        /// <summary>
        /// 複数のCheckShitabaraiIdとUriageShiharaiIdでTCheckShitabaraiChangeを取得します。
        /// </summary>
        /// <param name="checkShitabaraiIds">CheckShitabaraiのIDリスト</param>
        /// <param name="uriageShiharaiIds">UriageShiharaiのIDリスト</param>
        /// <returns>TCheckShitabaraiChangeエンティティのリスト</returns>
        Task<IEnumerable<TCheckShitabaraiChange>> GetByIdsAsync(List<int> checkShitabaraiIds, List<int> uriageShiharaiIds);
    }
}
