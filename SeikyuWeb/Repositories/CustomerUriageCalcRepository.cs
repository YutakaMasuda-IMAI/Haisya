using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 顧客売上計算リポジトリクラス
    /// </summary>
    public class CustomerUriageCalcRepository : RepositoryBaseAsync<MCustomerUriageCalc, HaisyaContext>, ICustomerUriageCalcRepository
    {
        public CustomerUriageCalcRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの顧客売上計算情報を非同期で取得します。
        /// </summary>
        /// <param name="id">顧客支店ID</param>
        /// <returns>顧客売上計算情報</returns>
        public async Task<MCustomerUriageCalc> GetByIdAsync(int id)
        {
            return await FindByCondition(c => c.CustomerBranchId.Equals(id)).FirstOrDefaultAsync();
        }
    } 
}
