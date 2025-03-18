using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 郵便番号リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IPostCodeRepository : IRepositoryBaseAsync<M_PostCode, ApplicationDbContext>
    {
        /// <summary>
        /// 県のリストを取得します。
        /// </summary>
        /// <returns>県のリストを含むタスク</returns>
        Task<IEnumerable<string>> GetListKens();

        /// <summary>
        /// 指定された県、市区町村、および町域に基づいて郵便番号を取得します。
        /// </summary>
        /// <param name="ken">県</param>
        /// <param name="shikucho">市区町村</param>
        /// <param name="choiki">町域</param>
        /// <returns>郵便番号を含むタスク</returns>
        Task<string> GetPostCodeAsync(string ken, string shikucho, string choiki);

        /// <summary>
        /// 指定された県に基づいて住所を取得します。
        /// </summary>
        /// <param name="ken">県</param>
        /// <returns>住所のリストを含むタスク</returns>
        Task<IEnumerable<string>> GetAddressByKenAsync(string ken);

        /// <summary>
        /// 指定された県と市区町村に基づいて住所を取得します。
        /// </summary>
        /// <param name="ken">県</param>
        /// <param name="shikucho">市区町村</param>
        /// <returns>住所のリストを含むタスク</returns>
        Task<IEnumerable<string>> GetAddressByConditionsAsync(string ken, string shikucho);
    }
}
