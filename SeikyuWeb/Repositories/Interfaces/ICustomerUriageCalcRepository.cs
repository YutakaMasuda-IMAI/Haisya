using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 顧客売上計算リポジトリインターフェース
    /// </summary>
    public interface ICustomerUriageCalcRepository : IRepositoryBaseAsync<MCustomerUriageCalc, HaisyaContext>
    {
        /// <summary>
        /// IDで顧客売上計算を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>顧客売上計算</returns>
        Task<MCustomerUriageCalc> GetByIdAsync(int id);
    }
}
