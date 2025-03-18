using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 会社ポータルリポジトリインターフェース
    /// </summary>
    public interface ICompanyPortalRepository
    {
        /// <summary>
        /// カレンダーを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>カレンダーのリスト</returns>
        Task<IList<CountByDate>> Get_calendars(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// 空車数をカウントする
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>空車数のリスト</returns>
        Task<IList<CountByDate>> Count_sharesyaryo(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// 共有荷物数をカウントする
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>共有荷物数のリスト</returns>
        Task<IList<CountByDate>> Count_shareluggage(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// 依頼案件をカウントする
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>依頼案件のリスト</returns>
        Task<IList<CountByDate>> Count_iraianken(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// 受注案件数をカウントする
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="branch_id">支店ID</param>
        /// <param name="from_date">開始日</param>
        /// <param name="to_date">終了日</param>
        /// <returns>受注案件数のリスト</returns>
        Task<IList<CountByDate>> Count_juchuanken(int company_id, int branch_id, DateTime from_date, DateTime to_date);

        /// <summary>
        /// 依頼案件を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>依頼案件のリスト</returns>
        Task<IEnumerable<JoinCompanyPortalDto>> GetIraiAnkensAsync(int companyId, int branchId);

        /// <summary>
        /// 受注案件を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>受注案件のリスト</returns>
        Task<IEnumerable<JoinCompanyPortalDto>> GetJuchuAnkensAsync(int companyId, int branchId);

        /// <summary>
        /// 安全情報を取得する
        /// </summary>
        /// <param name="renkeiAnkenIDs">連携案件IDの配列</param>
        /// <returns>安全情報のリスト</returns>
        Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs);

        /// <summary>
        /// 空車情報を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>空車情報のリスト</returns>
        Task<IEnumerable<JoinShareSyaryoDto>> GetSyaryosAsync(int companyId, int branchId);

        /// <summary>
        /// 荷物情報を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物情報のリスト</returns>
        Task<IEnumerable<JoinShareLuggageDto>> GetLuggagesAsync(int companyId, int branchId);

        /// <summary>
        /// 空車数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>空車数</returns>
        Task<int> GetShareSyaryoCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 空車確保数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>空車確保数</returns>
        Task<int> GetSyaSyaryoKakuhoCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 共有荷物数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物数</returns>
        Task<int> GetShareLuggageCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 共有荷物確保数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物確保数</returns>
        Task<int> GetShareLuggageKakuhoCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 共有荷物未登録数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>共有荷物未登録数</returns>
        int GetShareLuggageMitourokuCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 依頼案件数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件数</returns>
        Task<int> GetIraiAnkenCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 依頼案件車番確定数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件車番確定数</returns>
        Task<int> GetIraiAnkenSyabanKakuteiCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 依頼案件車番未確定数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>依頼案件車番未確定数</returns>
        Task<int> GetIraiAnkenSyabanMikakuteiCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 受注案件数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件数</returns>
        Task<int> GetJuchuAnkenCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 受注案件車番登録数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件車番登録数</returns>
        Task<int> GetJuchuAnkenSyabanTourokuCnt(int companyId, int branchId, DateTime date);

        /// <summary>
        /// 受注案件車番未登録数を取得する
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="date">日付</param>
        /// <returns>受注案件車番未登録数</returns>
        Task<int> GetJuchuAnkenSyabanMitourokuCnt(int companyId, int branchId, DateTime date);
    }
}
