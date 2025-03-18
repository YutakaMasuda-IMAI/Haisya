using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 共有車両詳細リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IShareSyaryoDetailRepository : IRepositoryBaseAsync<T_Share_Syaryo_Detail, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された共有車両IDと順序番号に基づいて共有車両の詳細を取得します。
        /// </summary>
        /// <param name="shareSyaryoId">共有車両ID</param>
        /// <param name="orderNum">順序番号</param>
        /// <returns>共有車両の詳細を含むタスク</returns>
        Task<T_Share_Syaryo_Detail> GetDetailAsync(int shareSyaryoId, int orderNum = 0);
    }
}
