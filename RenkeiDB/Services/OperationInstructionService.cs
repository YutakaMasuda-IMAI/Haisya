using RenkeiDB.Dto.OperationInstructionDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 運行指示書印刷のサービス
    /// </summary>
    public class OperationInstructionService : IOperationInstructionService
    {
        private readonly IOperationInstructionRepository _repository;
        public OperationInstructionService(IOperationInstructionRepository repository) => _repository = repository;

        /// <summary>
        /// 印刷用の運行指示書取得
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <returns>運行指示書の印刷データ</returns>
        public Task<OperationInstructionPrintDto> Get_operation_instruction_print(int anken_id)
            => _repository.Get_operation_instruction_print(anken_id);
    }
}
