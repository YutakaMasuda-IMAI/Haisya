using SeikyuWeb.Dto.Contractor;
using SeikyuWeb.Dto.Customer;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// ログインユーザーリポジトリインターフェース
    /// </summary>
    public interface ILoginUserRepository : IRepositoryBaseAsync<MLoginUser, HaisyaContext>
    {
        /// <summary>
        /// ログインIDでログインユーザーを取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ログインユーザー</returns>
        Task<MLoginUser> GetLoginUser(string loginId);

        /// <summary>
        /// ログインIDで契約者を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>契約者DTO</returns>
        Task<ContractorDto> GetContractor(string loginId);

        /// <summary>
        /// ログインIDで顧客を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>顧客DTO</returns>
        Task<LogisticsUnitDto> GetCustomer(string loginId);
    }
}
