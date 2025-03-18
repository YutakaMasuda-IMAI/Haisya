using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// コードデータリポジトリインターフェース
    /// </summary>
    public interface ICodeDataRepository : IRepositoryBaseAsync<MCodeDatum, HaisyaContext>
    {
        /// <summary>
        /// コードIDでコードデータを取得する
        /// </summary>
        /// <param name="id">コードID</param>
        /// <returns>コードデータのリスト</returns>
        Task<IEnumerable<MCodeDatum>> GetByCodeIdAsync(int id);
    }
}
