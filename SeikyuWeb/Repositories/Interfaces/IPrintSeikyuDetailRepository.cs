using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 請求明細印刷リポジトリインターフェース
    /// </summary>
    public interface IPrintSeikyuDetailRepository : IRepositoryBaseAsync<TPrintSeikyuDetail, HaisyaContext>
    {
        /// <summary>
        /// 区分IDで請求明細印刷を取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <param name="idKubun">区分ID</param>
        /// <returns>請求明細印刷リスト</returns>
        Task<IEnumerable<TPrintSeikyuDetail>> GetTPrintSeikyuDetailByKubunAsync(List<int> ids, int idKubun);
    }
}
