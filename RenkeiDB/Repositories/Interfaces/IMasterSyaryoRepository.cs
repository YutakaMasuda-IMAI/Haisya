using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// マスター車両リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IMasterSyaryoRepository : IRepositoryBaseAsync<M_Syaryo, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された車両IDに基づいてマスター車両データを取得します。
        /// </summary>
        /// <param name="syaryoId">車両ID</param>
        /// <returns>マスター車両データを含むタスク</returns>
        Task<M_Syaryo> GetMasterSyaryoData(int syaryoId);
    }
}
