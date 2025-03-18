using RenkeiDB.Data;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有荷物リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareLuggageRepository : IRepositoryBaseAsync<T_Share_Luggage, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたパラメータに基づいて荷物を取得します。
        /// </summary>
        /// <param name="dto">荷物パラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物印刷データのリストを含むタスク</returns>
        Task<List<LuggagePrintDataDto>> Get_luggages(LuggageParamsDto dto, int companyId, int branchId);

        /// <summary>
        /// 指定されたパラメータに基づいて共有荷物を取得します。
        /// </summary>
        /// <param name="dto">荷物パラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>共有荷物のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinShareLuggage>> GetShareLuggageAsync(LuggageParamsDto dto, int companyId, int branchId);

        /// <summary>
        /// 指定されたIDに基づいて共有荷物の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>共有荷物の詳細を含むタスク</returns>
        Task<JoinShareLuggage> GetDetailAsync(int id);

        /// <summary>
        /// 指定された区分と荷物に基づいて共有荷物を作成または更新します。
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="luggage">荷物</param>
        /// <returns>作成または更新された荷物IDを含むタスク</returns>
        Task<int?> CreateOrUpdateWithSpShareLuggage(SpShareLuggageKubun kubun, T_Share_Luggage luggage);

        /// <summary>
        /// 指定されたIDに基づいて共有荷物の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>共有荷物の詳細を含むタスク</returns>
        Task<JoinShareLuggage2> GetDetail2Async(int id);

        /// <summary>
        /// 指定されたIDに基づいて共有荷物のコレクションを取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>共有荷物のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinShareLuggage2>> GetJoinShareLuggage2(int id);

        /// <summary>
        /// 共有荷物を取得します。
        /// </summary>
        /// <returns>共有荷物のコレクションを含むタスク</returns>
        Task<IEnumerable<JoinShareLuggageDto>> GetLuggagesAsync();
    }
}
