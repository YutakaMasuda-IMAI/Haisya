using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// ポータル情報リポジトリインターフェース
    /// </summary>
    public interface IPortalInfoRepository : IRepositoryBaseAsync<TPortalInfo, HaisyaContext>
    {
        /// <summary>
        /// 会社IDで通知契約者を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>通知契約者リスト</returns>
        Task<IEnumerable<TPortalInfo>> GetNotificationContractor(int companyId);

        /// <summary>
        /// ユーザーIDで通知契約者以外を取得します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>通知契約者以外リスト</returns>
        Task<IEnumerable<TPortalInfo>> GetNotificationWithoutContractor(int userId);

        /// <summary>
        /// 印刷IDと会社IDでポータル情報を取得します。
        /// </summary>
        /// <param name="printParameterIds">印刷パラメータIDリスト</param>
        /// <param name="companyId">会社ID</param>
        /// <returns>ポータル情報リスト</returns>
        Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintIdAndCompanyId(List<int> printParameterIds, int companyId);

        /// <summary>
        /// 印刷IDと顧客担当者IDでポータル情報を取得します。
        /// </summary>
        /// <param name="printParameterIds">印刷パラメータIDリスト</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>ポータル情報リスト</returns>
        Task<IEnumerable<TPortalInfo>> GetTPortalInfoByPrintIdAndCustomerTantouId(List<int> printParameterIds, int userId);

        /// <summary>
        /// IDリストでポータル情報を取得します。
        /// </summary>
        /// <param name="ids">IDリスト</param>
        /// <returns>ポータル情報リスト</returns>
        Task<IEnumerable<TPortalInfo>> GetAllByIdsAsync(List<int> ids);

        /// <summary>
        /// IDで通知を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>通知</returns>
        Task<TPortalInfo> GetNotificationById(int id);
    }
}
