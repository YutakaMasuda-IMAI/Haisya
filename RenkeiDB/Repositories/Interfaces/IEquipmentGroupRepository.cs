using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 設備グループリポジトリのインターフェースを定義します。
    /// </summary>
    public interface IEquipmentGroupRepository : IRepositoryBaseAsync<M_Equipment_Group, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された会社IDに基づいて設備グループを非同期的に取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>設備グループのコレクションを含むタスク</returns>
        Task<IEnumerable<M_Equipment_Group>> GetEquipmentGroupByCompanyIdAsync(int companyId);
    }
}
