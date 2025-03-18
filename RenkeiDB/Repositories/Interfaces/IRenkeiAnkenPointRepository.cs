using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 連携案件ポイントリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenPointRepository : IRepositoryBaseAsync<T_Renkei_Anken_Point, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された案件IDと案件順序に基づいてポイントを取得します。
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件順序</param>
        /// <returns>ポイントのリストを含むタスク</returns>
        Task<IList<T_Renkei_Anken_Point>> GetPoints(int anken_id, int anken_order);

        /// <summary>
        /// 最新のポイントを取得します。
        /// </summary>
        /// <returns>最新のポイントのリストを含むタスク</returns>
        Task<IList<T_Renkei_Anken_Point>> GetPointsMostRecents();

        /// <summary>
        /// 使用率に基づいてポイントを取得します。
        /// </summary>
        /// <returns>使用率に基づいたポイントのリストを含むタスク</returns>
        Task<IList<T_Renkei_Anken_Point>> GetPointsUsageRate();

        /// <summary>
        /// 指定された住所2に基づいてポイントを取得します。
        /// </summary>
        /// <param name="address2">住所2</param>
        /// <returns>ポイントのコレクションを含むタスク</returns>
        Task<IEnumerable<T_Renkei_Anken_Point>> GetPointsAsync(string address2);
    }
}
