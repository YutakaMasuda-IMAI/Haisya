using RenkeiDB.Data;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 連携案件荷物リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenLuggageRepository : IRepositoryBaseAsync<T_Renkei_Anken_Luggage, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された案件IDと案件順序に基づいて荷物を取得します。
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件順序</param>
        /// <returns>荷物のリストを含むタスク</returns>
        Task<IList<T_Renkei_Anken_Luggage>> GetLuggages(int anken_id, int anken_order);
    }
}
