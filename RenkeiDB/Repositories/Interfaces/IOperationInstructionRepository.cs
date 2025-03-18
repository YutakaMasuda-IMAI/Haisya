using RenkeiDB.Data;
using RenkeiDB.Dto.OperationInstructionDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 操作指示リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IOperationInstructionRepository : IRepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された案件IDに基づいて操作指示印刷情報を取得します。
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <returns>操作指示印刷情報を含むタスク</returns>
        Task<OperationInstructionPrintDto> Get_operation_instruction_print(int anken_id);
    }
}
