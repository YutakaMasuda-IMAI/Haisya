using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// ログインユーザ顧客リポジトリクラス
    /// </summary>
    public class LoginUserCustomerRepository : RepositoryBaseAsync<MLoginUserCustomer, HaisyaContext>, ILoginUserCustomerRepository
    {
        public LoginUserCustomerRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// ログインユーザの取得
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ログインユーザ</returns>
        public async Task<MLoginUserCustomer> GetLoginUser(string loginId)
        {
            MLoginUserCustomer data = await (from loginUser in DbContext.Set<MLoginUserCustomer>()
                              where EF.Functions.Collate(loginUser.LoginId, "Latin1_General_BIN") == loginId && loginUser.DelFlg == false
                              select loginUser
                              ).FirstOrDefaultAsync();
            return data;
        }
    }
}
