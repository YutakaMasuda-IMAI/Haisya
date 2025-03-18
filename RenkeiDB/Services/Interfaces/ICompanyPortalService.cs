using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 会社ポータルサービスインターフェース
    /// </summary>
    public interface ICompanyPortalService
    {
        /// <summary>
        /// カレンダーを取得します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>日付ごとのカウントの列挙</returns>
        Task<IEnumerable<CountByDateString>> get_calendars(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// ポータルカウントを非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>会社ポータルカウントDTO</returns>
        Task<CompanyPortalCountsDto> GetPortalCountsAsync(int companyId, int branchId, string date);

        /// <summary>
        /// ポータルを非同期で取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>会社ポータルDTO</returns>
        Task<CompanyPortalsDto> GetPortalsAsync(int companyId, int branchId);
    }
}
