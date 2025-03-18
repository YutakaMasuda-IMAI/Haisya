using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 会社ユーザーグループユーザーリポジトリインターフェース
    /// </summary>
    public interface ICompanyUserGroupUserRepository : IRepositoryBaseAsync<MCompanyUserGroupUser, HaisyaContext>
    {
        /// <summary>
        /// グループIDで会社ユーザーグループユーザーを取得する
        /// </summary>
        /// <param name="id">グループID</param>
        /// <returns>会社ユーザーグループユーザーのリスト</returns>
        Task<IEnumerable<MCompanyUserGroupUser>> GetByGroupIdAsync(int id);
    }
}
