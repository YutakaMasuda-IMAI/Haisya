using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 顧客ポータルリポジトリインターフェース
    /// </summary>
    public interface ICustomerPortalRepository
    {
        /// <summary>
        /// ポータル情報を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>ポータル情報のリスト</returns>
        Task<IEnumerable<JoinCustomerPortalDto>> GetPortalsAsync(int companyId, int branchId);

        /// <summary>
        /// 案件秘密情報を取得する
        /// </summary>
        /// <param name="renkeiAnkenIDs">案件IDリスト</param>
        /// <returns>案件秘密情報のリスト</returns>
        Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs);

        /// <summary>
        /// ポータルのカウントを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="date">カウント日</param>
        /// <returns>ポータルのカウント</returns>
        Task<CustomerPortalCountsDto> Get_portal_counts(int company_id, int branch_id, DateTime date);

        /// <summary>
        /// カレンダーを取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <returns>カレンダーのリスト</returns>
        Task<IList<CountByDate>> GetCalendars(int companyId, int branchId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 受注件数または依頼中案件数をカウントする
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <param name="anken_kubun">案件区分</param>
        /// <returns>カウントリスト</returns>
        Task<IList<CountByDate>> Count_order_or_request(int companyId, int branchId, DateTime fromDate, DateTime toDate, int anken_kubun);

        /// <summary>
        /// 依頼中案件数をカウントする
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <returns>カウントリスト</returns>
        Task<IList<CountByDate>> Count_request(int companyId, int branchId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 取消案件数をカウントする
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <returns>カウントリスト</returns>
        Task<IList<CountByDate>> Count_cancel(int companyId, int branchId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 受注件数をカウントする
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="fromDate">開始日</param>
        /// <param name="toDate">終了日</param>
        /// <returns>カウントリスト</returns>
        Task<IList<CountByDate>> Count_order(int companyId, int branchId, DateTime fromDate, DateTime toDate);
    }
}
