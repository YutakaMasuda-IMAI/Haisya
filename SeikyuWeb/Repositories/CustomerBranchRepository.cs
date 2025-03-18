using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 顧客支店リポジトリクラス
    /// </summary>
    public class CustomerBranchRepository : RepositoryBaseAsync<MCustomerBranch, HaisyaContext>, ICustomerBranchRepository
    {
        public CustomerBranchRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの顧客支店情報を非同期で取得します。
        /// </summary>
        /// <param name="id">顧客支店ID</param>
        /// <returns>顧客支店情報</returns>
        public async Task<MCustomerBranch> GetByIdAsync(int id) => await FindByCondition(x => x.CustomerBranchId.Equals(id)).FirstOrDefaultAsync();
    }
}
