using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 顧客担当者リポジトリクラス
    /// </summary>
    public class CustomerTantouRepository : RepositoryBaseAsync<MCustomerTantou, HaisyaContext>, ICustomerTantouRepository
    {
        public CustomerTantouRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }
}
