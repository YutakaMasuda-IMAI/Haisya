using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    public interface IUnitRepository : IRepositoryBaseAsync<MUnit, HaisyaContext>
    {
        Task<List<MUnit>> GetByIdsAsync(List<int> ids);
    }
}
