using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 共有荷物セキュアリポジトリ
    /// </summary>
    public class ShareLuggageSecureRepository : RepositoryBaseAsync<T_Share_Luggage_Secure, ApplicationDbContext>, IShareLuggageSecureRepository
    {
        public ShareLuggageSecureRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }
}
