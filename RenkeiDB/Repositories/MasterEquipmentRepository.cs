using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// マスター装備品リポジトリ
    /// </summary>
    public class MasterEquipmentRepository: RepositoryBaseAsync<M_Equipment_Group, ApplicationDbContext>, IMasterEquipmentRepository
    {
        public MasterEquipmentRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
        : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 装備品情報を取得する
        /// </summary>
        /// <param name="mEquipmentGroupId">装備品グループID</param>
        /// <returns>装備品情報のリスト</returns>
        public async Task<IEnumerable<MasterEquipmentDto>> GetMasterEquipmentsByEquipmentGroupId(int mEquipmentGroupId)
        {
            List<M_Equipment> b = await (from t1 in DbContext.Set<M_Equipment_Group>()
                           join t2 in DbContext.Set<M_Equipment>()
                               on new { id = t1.Equipment_Group_ID }
                               equals new { id = t2.Equipment_Group_ID }
                           where t1.Equipment_Group_ID == mEquipmentGroupId
                               && t1.Del_Flg == false
                           orderby t2.SortOrder ascending
                           select t2).ToListAsync();

            // データが見つからない場合は、nullを返す
            if (b == null)
            {
                return null;
            }
            return Mapper.ConvertEntityMasterEquipmentDto(b);
        }
    }
}
