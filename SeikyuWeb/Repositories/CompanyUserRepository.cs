using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    public class CompanyUserRepository : RepositoryBaseAsync<MCompanyUser, HaisyaContext>, ICompanyUserRepository
    {
        public CompanyUserRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        public async Task<List<MCompanyUser>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.UserId) && u.DelFlg == false).ToListAsync();
    } 
}
