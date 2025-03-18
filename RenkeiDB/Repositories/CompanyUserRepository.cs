using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 会社ユーザーリポジトリクラス
    /// </summary>
    public class CompanyUserRepository : RepositoryBaseAsync<M_CompanyUser, ApplicationDbContext>, ICompanyUserRepository
    {
        public CompanyUserRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// カンパニーユーザの取得
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>カンパニーユーザ</returns>
        public async Task<M_CompanyUser> GetByIdAsync(int id)
        {
            return await FindByCondition(x=>x.User_ID.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
