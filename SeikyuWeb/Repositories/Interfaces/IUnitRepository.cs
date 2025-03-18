using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// 会社ユーザーリポジトリのインターフェースを定義します。
    /// </summary>
    public interface ICompanyUserRepository : IRepositoryBaseAsync<MCompanyUser, HaisyaContext>
    {
        /// <summary>
        /// 指定されたIDリストに基づいて会社ユーザーを非同期で取得します。
        /// </summary>
        /// <param name="ids">IDのリスト</param>
        /// <returns>会社ユーザーのリストを含むタスク</returns>
        Task<List<MCompanyUser>> GetByIdsAsync(List<int> ids);
    }
}
