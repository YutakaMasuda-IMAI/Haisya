using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// ログインユーザー顧客リポジトリインターフェース
    /// </summary>
    public interface ILoginUserCustomerRepository : IRepositoryBaseAsync<MLoginUserCustomer, HaisyaContext>
    {
        /// <summary>
        /// ログインIDでログインユーザー顧客を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ログインユーザー顧客</returns>
        Task<MLoginUserCustomer> GetLoginUser(string loginId);
    }
}
