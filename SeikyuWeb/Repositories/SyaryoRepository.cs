using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    public class SyaryoRepository : RepositoryBaseAsync<MSyaryo, HaisyaContext>, ISyaryoRepository
    {
        public SyaryoRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<MSyaryo>> GetByIdsAsync(List<int> ids)
            => await FindByCondition(u => ids.Contains(u.SyaryoId)).ToListAsync();
    }
}
