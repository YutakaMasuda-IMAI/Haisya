using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 印刷パラメータリポジトリインターフェース
    /// </summary>
    public interface IPrintParameterRepository : IRepositoryBaseAsync<TPrintParameter, HaisyaContext>
    {
        /// <summary>
        /// IDで印刷パラメータを取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>印刷パラメータ</returns>
        Task<TPrintParameter> GetByIdAsync(int id);

        /// <summary>
        /// コード区分で印刷パラメータを取得します。
        /// </summary>
        /// <returns>印刷パラメータリスト</returns>
        Task<IEnumerable<TPrintParameter>> GetByCodeKubunsAsync();

        /// <summary>
        /// 請求IDで印刷パラメータリストを取得します。
        /// </summary>
        /// <param name="id">請求ID</param>
        /// <returns>印刷パラメータリスト</returns>
        Task<IEnumerable<TPrintParameter>> GetListBySeikyuIdAsync(int id);

        /// <summary>
        /// チェック請求IDで印刷パラメータリストを取得します。
        /// </summary>
        /// <param name="id">チェック請求ID</param>
        /// <returns>印刷パラメータリスト</returns>
        Task<IEnumerable<TPrintParameter>> GetListByCheckSeikyuIdAsync(int id);

        /// <summary>
        /// チェック支払IDで印刷パラメータを取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック支払ID</param>
        /// <returns>印刷パラメータリスト</returns>
        Task<IEnumerable<TPrintParameter>> GetByCheckShitabaraiIdAsync(int checkShitabaraiId);
    }
}
