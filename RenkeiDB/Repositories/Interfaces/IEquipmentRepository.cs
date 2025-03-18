using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 設備リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IEquipmentRepository : IRepositoryBaseAsync<M_Equipment, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された会社IDに基づいて設備を非同期的に取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>設備のコレクションを含むタスク</returns>
        Task<IEnumerable<M_Equipment>> GetEquipmentByCompanyIdAsync(int companyId);
    }
}
