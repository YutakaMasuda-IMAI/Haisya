using RenkeiDB.Dto.MasterLuggageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// マスター荷物リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IMasterLuggageRepository
    {
        /// <summary>
        /// 指定された会社IDに基づいてマスター荷物グループを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>マスター荷物グループのコレクションを含むタスク</returns>
        Task<IEnumerable<MasterLuggageGroupsDto>> GetMasterLuggageGroups(int companyId);

        /// <summary>
        /// 指定された荷物グループIDに基づいてマスター荷物を取得します。
        /// </summary>
        /// <param name="mLuggageGroupId">荷物グループID</param>
        /// <returns>マスター荷物のコレクションを含むタスク</returns>
        Task<IEnumerable<MasterLuggageDto>> GetMasterLuggagesByLuggageGroupId(int mLuggageGroupId);
    }
}
