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
    /// 装備品グループリポジトリ
    /// </summary>
    public class EquipmentGroupRepository : RepositoryBaseAsync<M_Equipment_Group, ApplicationDbContext>, IEquipmentGroupRepository
    {
        public EquipmentGroupRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 装備品グループの取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>装備品グループのリスト</returns>
        public async Task<IEnumerable<M_Equipment_Group>> GetEquipmentGroupByCompanyIdAsync(int companyId)
        {
            return await FindByCondition(x => x.Company_ID.Equals(companyId) && x.Del_Flg == false).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
