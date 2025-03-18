using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 荷物リポジトリ
    /// </summary>
    public class LuggageRepository : RepositoryBaseAsync<M_Luggage, ApplicationDbContext>, ILuggageRepository
    {
        public LuggageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }
    }
}
