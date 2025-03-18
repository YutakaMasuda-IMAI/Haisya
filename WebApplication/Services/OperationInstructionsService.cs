using System.Threading.Tasks;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// OperationInstructionsServiceクラスは運行指示書サービスを提供します
    /// </summary>
    public class OperationInstructionsService : IOperationInstructionsService
    {
        private readonly IOperationInstructionsRepository _operationInstructionsRepository;

        public OperationInstructionsService(IOperationInstructionsRepository operationInstructionsRepository)
        {
            _operationInstructionsRepository = operationInstructionsRepository;
        }

        /// <summary>
        /// 指定された案件IDに紐づく運行指示書情報を取得する
        /// </summary>
        /// <param name="ankenId"></param>
        /// <returns></returns>
        public Task<OperationInstructionsModel> GetOperationInstructions(int ankenId)
            => _operationInstructionsRepository.GetOperationInstructions(ankenId);
    }

    public interface IOperationInstructionsService
    {
        Task<OperationInstructionsModel> GetOperationInstructions(int ankenId);
    }
}