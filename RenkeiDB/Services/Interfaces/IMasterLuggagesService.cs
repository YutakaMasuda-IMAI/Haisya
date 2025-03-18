using RenkeiDB.Dto.MasterLuggageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// マスター荷物サービスインターフェース
    /// </summary>
    public interface IMasterLuggagesService
    {
        /// <summary>
        /// マスター荷物グループを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>マスター荷物グループDTOの列挙</returns>
        Task<IEnumerable<MasterLuggageGroupsDto>> GetMasterLuggageGroups(int companyId);

        /// <summary>
        /// 荷物グループIDからマスター荷物を取得します。
        /// </summary>
        /// <param name="mLuggageGroupId">マスター荷物グループID</param>
        /// <returns>マスター荷物DTOの列挙</returns>
        Task<IEnumerable<MasterLuggageDto>> GetMasterLuggagesByLugGroupId(int mLuggageGroupId);
    }
}
