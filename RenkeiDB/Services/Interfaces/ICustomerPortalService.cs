using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 顧客ポータルサービスインターフェース
    /// </summary>
    public interface ICustomerPortalService
    {
        /// <summary>
        /// ポータルを非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>案件DTOの列挙</returns>
        Task<IEnumerable<AnkenDto>> GetPortalsAsync(int companyId, int branchId);

        /// <summary>
        /// カレンダーを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <returns>日ごとのカウントの列挙</returns>
        Task<IEnumerable<CountByDateString>> GetCalendars(int companyId, int branchId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// ポータルカウントを取得します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>顧客ポータルカウントDTO</returns>
        Task<CustomerPortalCountsDto> Get_portal_counts(int company_id, int branch_id, DateTime date);
    }
}
