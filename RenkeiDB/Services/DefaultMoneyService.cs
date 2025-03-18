using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// デフォルト金額サービスを提供します。
    /// </summary>
    public class DefaultMoneyService : IDefaultMoneyService
    {
        private readonly IDefaultMoneyRepository _defaultMoneyRepository;
        public DefaultMoneyService(IDefaultMoneyRepository defaultMoneyRepository)
        {
            _defaultMoneyRepository = defaultMoneyRepository;
        }
        /// <summary>
        /// マスター情報取得（M_DefaultMoney）
        /// </summary>
        /// <returns>デフォルト金額のリストを含むタスク</returns>
        public async Task<IEnumerable<DefaultMoneyDto>> GetDefaultMoneysAsync()
        {
            IList<DefaultMoneyDto> data = await _defaultMoneyRepository.GetDefaultMoneysAsync();
            if (data is null)
            {
                return null;
            }

            return data;
        }
    }
}
