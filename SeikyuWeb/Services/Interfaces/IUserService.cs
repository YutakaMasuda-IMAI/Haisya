using SeikyuWeb.Dto.Contractor;
using SeikyuWeb.Dto.Customer;
using SeikyuWeb.Dto.LoginDto;
using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginUserResDto> Login(string loginId, string password, int guard);
        Task<ContractorDto> GetContractor(string loginId);
        Task<LogisticsUnitDto> GetCustomer(string loginId);
    }
}
