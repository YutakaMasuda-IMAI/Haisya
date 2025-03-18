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
    /// 連携案件設備リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IRenkeiAnkenEquipmentRepository : IRepositoryBaseAsync<T_Renkei_Anken_Equipment, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された案件IDと案件順序に基づいて設備を取得します。
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件順序</param>
        /// <returns>設備のリストを含むタスク</returns>
        Task<IList<T_Renkei_Anken_Equipment>> GetEquipments(int anken_id, int anken_order);
    }
}
