using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 装備品マスターサービス
    /// </summary>
    public class MasterEquipmentsService: IMasterEquipmentsService
    {
        private readonly IMasterEquipmentRepository _masterEquipmentRepository;
        public MasterEquipmentsService(IMasterEquipmentRepository masterEquipmentRepository)
        {
            _masterEquipmentRepository = masterEquipmentRepository;
        }


        /// <summary>
        /// 装備品情報を取得する
        /// </summary>
        /// <param name="mEquipmentGroupId">装備品グループID</param>
        /// <returns>装備品情報のリスト</returns>
        public async Task<IEnumerable<MasterEquipmentDto>> GetMasterEquipmentsByEqGroupId(int mEquipmentGroupId)
        {
            return await _masterEquipmentRepository.GetMasterEquipmentsByEquipmentGroupId(mEquipmentGroupId);
        }
    }
}
