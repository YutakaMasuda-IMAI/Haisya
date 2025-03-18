using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Data;
using System;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有番号リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareNoRepository : IRepositoryBaseAsync<T_Share_No, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された共有日時とゼロ埋めに基づいて最新の共有番号を取得します。
        /// </summary>
        /// <param name="shareDate">共有日時</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>最新の共有番号を含むタスク</returns>
        Task<string> GetLatestShareNo(DateTime shareDate, int zeroUme);
    }
}
