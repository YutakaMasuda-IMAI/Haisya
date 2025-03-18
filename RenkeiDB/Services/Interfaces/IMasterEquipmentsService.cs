using RenkeiDB.Dto.EquipmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// マスター設備サービスインターフェース
    /// </summary>
    public interface IMasterEquipmentsService
    {   
        /// <summary>
        /// 設備グループIDからマスター設備を取得します。
        /// </summary>
        /// <param name="mEquipmentGroupId">マスター設備グループID</param>
        /// <returns>マスター設備DTOの列挙</returns>
        Task<IEnumerable<MasterEquipmentDto>> GetMasterEquipmentsByEqGroupId(int mEquipmentGroupId);
    }
}
