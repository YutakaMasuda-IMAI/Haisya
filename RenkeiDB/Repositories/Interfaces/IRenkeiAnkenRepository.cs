using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.AnkenDto.AnkenChangeHistoryDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 連携案件リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenRepository : IRepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された会社IDと卸住所に基づいて案件を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="oroshiAddress">卸住所</param>
        /// <returns>案件のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinAnken>> GetAnkensAsync(int companyId, string oroshiAddress);

        /// <summary>
        /// 指定されたIDに基づいて案件の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>案件の詳細を含むタスク</returns>
        Task<JoinRenkeiAnken> GetDetailAsync(int id);

        /// <summary>
        /// 指定された会社IDと支店IDに基づいて依頼案件を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>依頼案件のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinRenkeiAnken>> GetIraiAnkensAsync(int companyId, int branchId);

        /// <summary>
        /// 連携案件を作成または更新します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="anken">案件</param>
        /// <returns>作成または更新された案件のID</returns>
        public int? CreateOrUpdateWithSpRenkeiAnken(int kubun, T_Renkei_Anken anken);

        /// <summary>
        /// 指定された日時とゼロ埋めに基づいて連携案件番号を取得します。
        /// </summary>
        /// <param name="fromDateTime">日時</param>
        /// <param name="zeroUME">ゼロ埋め</param>
        /// <returns>連携案件番号</returns>
        public string GetSpRenkeiAnkenNo(DateTime fromDateTime, int zeroUME);

        /// <summary>
        /// 連携案件を作成します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="ankenNo">案件番号</param>
        /// <param name="ankenStatus">案件ステータス</param>
        /// <param name="ankenOrder">案件順序</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>作成された案件のIDを含むタスク</returns>
        Task<int> CreateWithSpRenkeiAnken(RenkeiAnkenKubun kubun, string ankenNo, int ankenStatus, int ankenOrder, int companyId, int branchId);

        /// <summary>
        /// 指定された共有日時とゼロ埋めに基づいて連携案件番号を取得します。
        /// </summary>
        /// <param name="shareDate">共有日時</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>連携案件番号を含むタスク</returns>
        Task<string> GetNoSPTRenkeiAnkenNo(DateTime shareDate, SystemEnums.ZeroUme zeroUme);

        /// <summary>
        /// 指定されたIDに基づいて案件変更履歴情報を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>案件変更履歴情報を含むタスク</returns>
        Task<JoinAnkenChangeHistoryDto> GetHistoryChangeAnkenInfo(int id);

        /// <summary>
        /// 指定された会社IDと支店IDに基づいて連携案件を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>連携案件のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinAnkenCsv>> GetRenkeiAnkensAsync(int companyId, int branchId);

        /// <summary>
        /// 指定された会社IDと支店IDに基づいてプロジェクト注文を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>プロジェクト注文のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinCompanyPortalDto>> GetProjectOrdersAsync(int companyId, int branchId);

        /// <summary>
        /// 指定された連携案件IDに基づいてセキュア情報を取得します。
        /// </summary>
        /// <param name="renkeiAnkenIDs">連携案件IDの配列</param>
        /// <returns>セキュア情報のコレクションを含むタスク</returns>
        Task<IEnumerable<AnkenSecureDto>> GetSecuresAsync(int[] renkeiAnkenIDs);

        /// <summary>
        /// 指定されたIDに基づいて連携案件を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>連携案件を含むタスク</returns>
        Task<T_Renkei_Anken> GetRenkeiAnkenById(int id);
    }
}
