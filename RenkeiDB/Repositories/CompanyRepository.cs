using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    public class CompanyRepository : RepositoryBaseAsync<M_Company, ApplicationDbContext>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<M_CompanyBranch> GetDetailCompanyBranchAsync(int id)
        {
            return await DbContext.M_CompanyBranches.Where(c => c.Branch_ID == id && c.Del_Flg == false).FirstOrDefaultAsync();
        }

        public async Task<M_Company> GetByIdAsync(int id)
        {
            return await FindByCondition(x => x.Renkei_Company_ID.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
