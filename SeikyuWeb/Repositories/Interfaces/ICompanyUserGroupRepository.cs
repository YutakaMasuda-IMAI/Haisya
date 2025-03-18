using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    public interface ICompanyUserGroupRepository : IRepositoryBaseAsync<MCompanyUserGroup, HaisyaContext>
    {
        Task<MCompanyUserGroup> GetByIdAsync(int id);
    }
}
