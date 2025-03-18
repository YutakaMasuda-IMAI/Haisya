using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 会社ユーザーグループリポジトリインターフェース
    /// </summary>
    public interface ICompanyUserGroupRepository : IRepositoryBaseAsync<M_CompanyUser_Group, ApplicationDbContext>
    {
        /// <summary>
        /// IDで会社ユーザーグループを取得する
        /// </summary>
        /// <param name="id">会社ユーザーグループID</param>
        /// <returns>会社ユーザーグループ</returns>
        Task<M_CompanyUser_Group> GetCompanyUserGroupByIdAsync(int id);
    }
}
