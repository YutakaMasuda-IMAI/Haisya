using RenkeiDB.Dto.OperationInstructionDto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 操作指示サービスインターフェース
    /// </summary>
    public interface IOperationInstructionService
    {
        /// <summary>
        /// 操作指示を印刷します。
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <returns>操作指示印刷DTO</returns>
        Task<OperationInstructionPrintDto> Get_operation_instruction_print(int anken_id);
    }
}
