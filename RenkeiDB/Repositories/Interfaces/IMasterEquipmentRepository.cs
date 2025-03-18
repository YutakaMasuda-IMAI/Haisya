using RenkeiDB.Dto.EquipmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// マスター設備リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IMasterEquipmentRepository
    {
        /// <summary>
        /// 指定された設備グループIDに基づいてマスター設備を取得します。
        /// </summary>
        /// <param name="mEquipmentGroupId">設備グループID</param>
        /// <returns>マスター設備のコレクションを含むタスク</returns>
        Task<IEnumerable<MasterEquipmentDto>> GetMasterEquipmentsByEquipmentGroupId(int mEquipmentGroupId);
    }
}
