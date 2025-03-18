using RenkeiDB.Data;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// デフォルト金額リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IDefaultMoneyRepository : IRepositoryBaseAsync<M_DefaultMoney, ApplicationDbContext>
    {
        /// <summary>
        /// デフォルト金額を非同期的に取得します。
        /// </summary>
        /// <returns>デフォルト金額のリストを含むタスク</returns>
        Task<IList<DefaultMoneyDto>> GetDefaultMoneysAsync();
    }
}
