using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 会社ユーザーグループユーザーリポジトリクラス
    /// </summary>
    public class CompanyUserGroupUserRepository : RepositoryBaseAsync<MCompanyUserGroupUser, HaisyaContext>, ICompanyUserGroupUserRepository
    {
        public CompanyUserGroupUserRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したグループIDのユーザーリストを非同期で取得します。
        /// </summary>
        /// <param name="id">グループID</param>
        /// <returns>ユーザーリスト</returns>
        public async Task<IEnumerable<MCompanyUserGroupUser>> GetByGroupIdAsync(int id)
            => await FindByCondition(x => x.GroupId.Equals(id) && x.DelFlg == false).ToListAsync();
    }
}
