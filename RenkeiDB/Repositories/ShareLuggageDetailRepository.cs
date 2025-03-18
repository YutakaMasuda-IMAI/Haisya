using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 共有荷物詳細リポジトリ
    /// </summary>
    public class ShareLuggageDetailRepository : RepositoryBaseAsync<T_Share_Luggage_Detail, ApplicationDbContext>, IShareLuggageDetailRepository
    {
        public ShareLuggageDetailRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }
}
