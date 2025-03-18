using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;

namespace RenkeiDB.Repositories
{
    public class ShareSyaryoSecureRepository : RepositoryBaseAsync<T_Share_Syaryo_Secure, ApplicationDbContext>, IShareSyaryoSecureRepository
    {
        public ShareSyaryoSecureRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
}
