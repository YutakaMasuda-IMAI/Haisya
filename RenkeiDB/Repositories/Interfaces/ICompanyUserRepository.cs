using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 会社ユーザーリポジトリインターフェース
    /// </summary>
    public interface ICompanyUserRepository : IRepositoryBaseAsync<M_CompanyUser, ApplicationDbContext>
    {
        /// <summary>
        /// IDで会社ユーザーを取得する
        /// </summary>
        /// <param name="id">会社ユーザーID</param>
        /// <returns>会社ユーザー</returns>
        Task<M_CompanyUser> GetByIdAsync(int id);
    }
}
