using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 支払チェックリポジトリインターフェース
    /// </summary>
    public interface ICheckShitabaraiRepository : IRepositoryBaseAsync<TCheckShitabarai, HaisyaContext>
    {
        /// <summary>
        /// IDで支払チェックの詳細を取得する
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>支払チェックの詳細</returns>
        Task<TCheckShitabarai> GetDetailCheckShibaraiById(int id);

        /// <summary>
        /// 顧客支店ID、会社ID、ステータスで支払チェックのリストを取得する
        /// </summary>
        /// <param name="customerBranchId">顧客支店ID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="status">ステータス配列</param>
        /// <returns>支払チェックのリスト</returns>
        Task<IEnumerable<TCheckShitabarai>> GetListCheckShiharaisAsync(int customerBranchId, int companyId, int[] status);

        /// <summary>
        /// IDで支払チェックを取得する
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>支払チェック</returns>
        Task<TCheckShitabarai> GetByIdAsync(int id);

        /// <summary>
        /// 会社IDで支払チェックのリストを取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>支払チェックのリスト</returns>
        Task<IEnumerable<TCheckShitabarai>> GetByCompanyIdAsync(int companyId);

        /// <summary>
        /// 支店IDで支払チェックのリストを取得する
        /// </summary>
        /// <param name="customerBranchId">支店ID</param>
        /// <returns>支払チェックのリスト</returns>
        Task<IEnumerable<TCheckShitabarai>> GetByBranchIdAsync(int customerBranchId);

        /// <summary>
        /// 支払請求のリストを取得する
        /// </summary>
        /// <returns>支払請求のリスト</returns>
        Task<IEnumerable<TCheckShitabarai>> GetTCheckSeikyuAsync();
    }
}
