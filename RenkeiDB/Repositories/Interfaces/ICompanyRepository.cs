using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 会社リポジトリインターフェース
    /// </summary>
    public interface ICompanyRepository : IRepositoryBaseAsync<M_Company, ApplicationDbContext>
    {
        /// <summary>
        /// IDで会社支店の詳細を取得する
        /// </summary>
        /// <param name="id">会社支店ID</param>
        /// <returns>会社支店の詳細</returns>
        Task<M_CompanyBranch> GetDetailCompanyBranchAsync(int id);

        /// <summary>
        /// IDで会社を取得する
        /// </summary>
        /// <param name="id">会社ID</param>
        /// <returns>会社</returns>
        Task<M_Company> GetByIdAsync(int id);
    }
}
