using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    public class CompanyUserGroupRepository : RepositoryBaseAsync<M_CompanyUser_Group, ApplicationDbContext>, ICompanyUserGroupRepository
    {
        public CompanyUserGroupRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// カンパニーユーザーグループの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<M_CompanyUser_Group> GetCompanyUserGroupByIdAsync(int id)
        {
            return await FindByCondition(x => x.Group_ID.Equals(id) && x.Del_Flg == false).FirstOrDefaultAsync();
        }
    }
}
