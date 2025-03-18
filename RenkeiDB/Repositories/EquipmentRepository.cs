using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 装備品リポジトリ
    /// </summary>
    public class EquipmentRepository : RepositoryBaseAsync<M_Equipment, ApplicationDbContext>, IEquipmentRepository
    {
        public EquipmentRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 装備品リストの取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>装備品リスト</returns>
        public async Task<IEnumerable<M_Equipment>> GetEquipmentByCompanyIdAsync(int companyId)
        {
            return await FindByCondition(x => x.Company_ID.Equals(companyId) && x.Del_Flg == false).ToListAsync();
        }
    }
}
