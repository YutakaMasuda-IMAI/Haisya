using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EmptyCarDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有車両リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareSyaryoRepository : IRepositoryBaseAsync<T_Share_Syaryo, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたIDに基づいて共有車両の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="includes">含めるプロパティのリスト</param>
        /// <returns>共有車両の詳細を含むタスク</returns>
        Task<T_Share_Syaryo> GetDetailAsync(int id, List<string> includes = null);

        /// <summary>
        /// 指定されたパラメータに基づいて共有車両のリストを取得します。
        /// </summary>
        /// <param name="paramRequests">パラメータリクエスト</param>
        /// <param name="includes">含めるプロパティのリスト</param>
        /// <returns>共有車両のリストを含むタスク</returns>
        Task<IEnumerable<T_Share_Syaryo>> GetListAsync(ShareSyaryoListRequestDto paramRequests, List<string> includes = null);

        /// <summary>
        /// 指定された車両IDと区分に基づいて共有車両を非同期的に更新します。
        /// </summary>
        /// <param name="syaryoId">車両ID</param>
        /// <param name="kubun">区分</param>
        /// <returns>更新された車両IDを含むタスク</returns>
        Task<int?> UpdateWithSpShareSyaryoAsync(int syaryoId, SpShareSyaryoKubun kubun);

        /// <summary>
        /// 共有車両を作成します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="syaryoNo">車両番号</param>
        /// <param name="syaryoStatus">車両ステータス</param>
        /// <param name="syaryoOrder">車両順序</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="tantouId">担当者ID</param>
        /// <returns>作成された車両IDを含むタスク</returns>
        Task<int> CreateWithSpShareSyaryo(SpShareSyaryoKubun kubun, string syaryoNo, int syaryoStatus, int syaryoOrder, int companyId, int branchId, int tantouId);

        /// <summary>
        /// 指定された共有日時とゼロ埋めに基づいて共有番号を取得します。
        /// </summary>
        /// <param name="shareDate">共有日時</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>共有番号を含むタスク</returns>
        Task<string> GetNoWithSpShareNo(DateTime shareDate, ZeroUme zeroUme);

        /// <summary>
        /// 指定された共有日時とゼロ埋めに基づいて連携案件番号を取得します。
        /// </summary>
        /// <param name="shareDate">共有日時</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>連携案件番号を含むタスク</returns>
        Task<string> Run_sp_t_renkei_anken_no(DateTime shareDate, ZeroUme zeroUme);

        /// <summary>
        /// 指定されたIDに基づいて空車情報を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>空車情報のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinEmptyCarDto>> GetJoinDataByIdAsync(int id);

        /// <summary>
        /// 指定された区分、車両ID、および車両ステータスに基づいて共有車両のステータスを更新します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="syaryoId">車両ID</param>
        /// <param name="syaryoStatus">車両ステータス</param>
        /// <returns>更新された車両IDを含むタスク</returns>
        Task<int> UpdateStatusWithSpShareSyaryo(SpShareSyaryoKubun kubun, int? syaryoId, int? syaryoStatus);

        /// <summary>
        /// 指定された会社IDと支店IDに基づいて空車情報を取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>空車情報のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinShareSyaryoDto>> GetKeepEmptyCarAsync(int companyId, int branchId);
    }
}
