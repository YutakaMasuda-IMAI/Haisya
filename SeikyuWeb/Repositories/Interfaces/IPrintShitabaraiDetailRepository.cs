using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 支払明細印刷リポジトリインターフェース
    /// </summary>
    public interface IPrintShitabaraiDetailRepository : IRepositoryBaseAsync<TPrintShitabaraiDetail, HaisyaContext>
    {
        /// <summary>
        /// 区分IDで支払明細印刷を取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <param name="idKubun">区分ID</param>
        /// <returns>支払明細印刷リスト</returns>
        Task<IEnumerable<TPrintShitabaraiDetail>> GetTShitabaraiDetailByKubunAsync(List<int> ids, int idKubun);
    }
}
