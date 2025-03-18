using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// レポートレイアウトリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IReportLayoutRepository : IRepositoryBaseAsync<TReportLayout, HaisyaContext>
    {
        /// <summary>
        /// 指定された印刷区分に基づいてレポートレイアウトリストを非同期で取得します。
        /// </summary>
        /// <param name="printKubun">印刷区分</param>
        /// <returns>レポートレイアウトリストを含むタスク</returns>
        Task<IEnumerable<TReportLayout>> GetReportLayoutListAsync(int printKubun);
    }
}
