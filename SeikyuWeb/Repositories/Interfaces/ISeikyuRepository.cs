using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 請求リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ISeikyuRepository : IRepositoryBaseAsync<TSeikyu, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDに基づいて請求を非同期で取得します。
        /// </summary>
        /// <param name="id">請求ID</param>
        /// <returns>請求を含むタスク</returns>
        Task<TSeikyu> GetByIdAsync(int id);

        /// <summary>
        /// 指定された会社IDに基づいて請求を非同期で取得します。
        /// </summary>
        /// <param name="id">会社ID</param>
        /// <returns>請求のリストを含むタスク</returns>
        Task<IEnumerable<TSeikyu>> GetByCompanyAsync(int id);

        /// <summary>
        /// 指定された支店IDに基づいて請求を非同期で取得します。
        /// </summary>
        /// <param name="customerBranchId">支店ID</param>
        /// <returns>請求のリストを含むタスク</returns>
        Task<IEnumerable<TSeikyu>> GetByBranchAsync(int customerBranchId);

        /// <summary>
        /// 指定された印刷請求IDに基づいて請求書リストを非同期で取得します。
        /// </summary>
        /// <param name="printSeikyuId">印刷請求ID</param>
        /// <returns>請求書リストを含むタスク</returns>
        Task<IEnumerable<InvoiceQueryDto>> GetInvoiceListByPrintSeikyuIdAsync(int printSeikyuId);

        /// <summary>
        /// 全ての請求を非同期で取得します。
        /// </summary>
        /// <returns>請求のリストを含むタスク</returns>
        Task<IEnumerable<TSeikyu>> GetTSeikyuAsync();
    }
}
