using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// TCheckSeikyuエンティティのリポジトリインターフェース
    /// </summary>
    public interface ICheckSeikyuRepository : IRepositoryBaseAsync<TCheckSeikyu, HaisyaContext>
    {
        /// <summary>
        /// 請求照会リストを取得します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="status">ステータス配列</param>
        /// <param name="idInt">ID</param>
        /// <param name="isCompany">会社フラグ</param>
        /// <returns>SeikyuDtoのリスト</returns>
        Task<IEnumerable<SeikyuDto>> GetBillingInqueryList(int kubun, int[] status, int idInt, bool isCompany);

        /// <summary>
        /// 会社の請求を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>TCheckSeikyuエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyu>> GetBillingForCompany(int companyId);

        /// <summary>
        /// 顧客の請求を取得します。
        /// </summary>
        /// <param name="tantouId">担当者ID</param>
        /// <returns>TCheckSeikyuエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyu>> GetBillingForCustomer(int tantouId);

        /// <summary>
        /// 請求担当者を取得します。
        /// </summary>
        /// <param name="seikyuTantouId">請求担当者ID配列</param>
        /// <returns>SeikyuTantouQueryDtoのリスト</returns>
        Task<IEnumerable<SeikyuTantouQueryDto>> GetSeikyuTantou(int[] seikyuTantouId);

        /// <summary>
        /// 支払担当者を取得します。
        /// </summary>
        /// <param name="shitabaraiTantouId">支払担当者ID配列</param>
        /// <returns>ShiharaiTantouQueryDtoのリスト</returns>
        Task<IEnumerable<ShiharaiTantouQueryDto>> GetShiharaiTantou(int[] shitabaraiTantouId);

        /// <summary>
        /// IDでTCheckSeikyuを取得します。
        /// </summary>
        /// <param name="id">TCheckSeikyuのID</param>
        /// <returns>TCheckSeikyuエンティティ</returns>
        Task<TCheckSeikyu> GetByIdAsync(int id);

        /// <summary>
        /// IDでCheckSeikyuを取得します。
        /// </summary>
        /// <param name="id">CheckSeikyuのID</param>
        /// <returns>TCheckSeikyuエンティティ</returns>
        Task<TCheckSeikyu> GetCheckSeikyuByIdAsync(int id);

        /// <summary>
        /// 会社IDでTCheckSeikyuを取得します。
        /// </summary>
        /// <param name="id">会社ID</param>
        /// <returns>TCheckSeikyuエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyu>> GetByCompanyIdAsync(int id);

        /// <summary>
        /// 顧客支店IDでTCheckSeikyuを取得します。
        /// </summary>
        /// <param name="customerBranchId">顧客支店ID</param>
        /// <returns>TCheckSeikyuエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyu>> GetByCustomerBranchIdAsync(int customerBranchId);

        /// <summary>
        /// 全てのTCheckSeikyuを取得します。
        /// </summary>
        /// <returns>TCheckSeikyuエンティティのリスト</returns>
        Task<IEnumerable<TCheckSeikyu>> GetTCheckSeikyuAsync();
    }
}
