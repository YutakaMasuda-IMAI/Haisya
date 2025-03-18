using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    public interface ISyaryoRepository : IRepositoryBaseAsync<MSyaryo, HaisyaContext>
    {
        Task<IEnumerable<MSyaryo>> GetByIdsAsync(List<int> ids);
    }
}
