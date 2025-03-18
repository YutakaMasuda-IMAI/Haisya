using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 会社ユーザーグループリポジトリクラス
    /// </summary>
    public class CompanyUserGroupRepository : RepositoryBaseAsync<MCompanyUserGroup, HaisyaContext>, ICompanyUserGroupRepository
    {
        public CompanyUserGroupRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの会社ユーザーグループを非同期で取得します。
        /// </summary>
        /// <param name="id">グループID</param>
        /// <returns>会社ユーザーグループ</returns>
        public async Task<MCompanyUserGroup> GetByIdAsync(int id)
        {
            return await FindByCondition(x => x.GroupId.Equals(id) 
            && x.DelFlg == false)
                .FirstOrDefaultAsync();
        }
    }
}
