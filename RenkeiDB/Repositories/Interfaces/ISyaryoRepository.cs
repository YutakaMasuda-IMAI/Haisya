using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 車両リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ISyaryoRepository : IRepositoryBaseAsync<M_Syaryo, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたIDに基づいて車両を取得します。
        /// </summary>
        /// <param name="id">車両ID</param>
        /// <returns>車両を含むタスク</returns>
        Task<M_Syaryo> GetByIdAsync(int id);
    }
}
