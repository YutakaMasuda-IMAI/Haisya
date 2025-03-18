using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 連携案件装備品リポジトリ
    /// </summary>
    public class RenkeiAnkenEquipmentRepository : RepositoryBaseAsync<T_Renkei_Anken_Equipment, ApplicationDbContext>, IRenkeiAnkenEquipmentRepository
    {
        public RenkeiAnkenEquipmentRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 案件詳細取得
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件注文</param>
        /// <returns>Anken</returns>
        public async Task<IList<T_Renkei_Anken_Equipment>> GetEquipments(int anken_id, int anken_order)
        {
            IQueryable<T_Renkei_Anken_Equipment> builder =
                from trae in DbContext.Set<T_Renkei_Anken_Equipment>()
                    .Include(trae => trae.Equiptment)
                    .ThenInclude(equipment => equipment.Equipment_Group)
                where
                    trae.Renkei_Anken_ID == anken_id && trae.Renkei_Anken_Order == anken_order
                select trae;

            return await builder.ToListAsync();
        }
    }
}
