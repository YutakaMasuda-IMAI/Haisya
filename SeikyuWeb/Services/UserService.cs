using SeikyuWeb.Common;
using SeikyuWeb.Dto.Contractor;
using SeikyuWeb.Dto.Customer;
using SeikyuWeb.Dto.LoginDto;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly ILoginUserCustomerRepository _loginUserCustomerRepository;
        private readonly ICompanyUserRepository _companyUserRepository;
        public UserService(ILoginUserRepository loginUserRepository, ILoginUserCustomerRepository loginUserCustomerRepository, ICompanyUserRepository companyUserRepository) {
            _loginUserRepository = loginUserRepository;
            _loginUserCustomerRepository = loginUserCustomerRepository;
            _companyUserRepository = companyUserRepository;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="guard"></param>
        /// <returns></returns>
        public async Task<LoginUserResDto> Login(string loginId, string password, int guard)
        {
            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            if (guard == 0)
            {
                MLoginUser loginUser = await _loginUserRepository.GetLoginUser(loginId);

                if (loginUser == null || loginUser.Password != password)
                {
                    return null;
                }

                List<MCompanyUser> companyUser = await _companyUserRepository.GetByIdsAsync(new List<int>() { loginUser.UserId});

                return new LoginUserResDto
                {
                    LoginUser = loginUser,
                    LoginUserCustomer = null,
                    CompanyUser = companyUser.Count > 0 ? companyUser[0] : null,
                    IsCustomer = false,
                };
            }
            else
            {
                MLoginUserCustomer loginUserCustomer = await _loginUserCustomerRepository.GetLoginUser(loginId);

                if (loginUserCustomer == null || loginUserCustomer.Password != password)
                {
                    return null;
                }

                return new LoginUserResDto
                {
                    LoginUser = null,
                    LoginUserCustomer = loginUserCustomer,
                    CompanyUser = null,
                    IsCustomer = true,
                };
            }
        }

        /// <summary>
        /// ログイン情報取得（業者）
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<ContractorDto> GetContractor(string loginId)
        {
            ContractorDto contractor = await _loginUserRepository.GetContractor(loginId);
            return contractor;
        }

        /// <summary>
        /// ログイン情報取得（業者以外）
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<LogisticsUnitDto> GetCustomer(string loginId)
        {
            LogisticsUnitDto entity = await _loginUserRepository.GetCustomer(loginId);
            return entity;
        }
    }
}
