using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 顧客支店リポジトリインターフェース
    /// </summary>
    public interface ICustomerBranchRepository : IRepositoryBaseAsync<MCustomerBranch, HaisyaContext>
    {
        /// <summary>
        /// IDで顧客支店を取得する
        /// </summary>
        /// <param name="id">顧客支店ID</param>
        /// <returns>顧客支店</returns>
        Task<MCustomerBranch> GetByIdAsync(int id);
    }
}
