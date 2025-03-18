using SeikyuWeb.Dto.MasterDto;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// マスターサービス
    /// </summary>
    public class MasterService : IMasterService
    {
        private readonly ILoginUserRepository _loginUserRepository;
        private readonly ICodeDataRepository _codeDataRepository;
        public MasterService(ICodeDataRepository codeDataRepository, ILoginUserRepository loginUserRepository)
        {
            _codeDataRepository = codeDataRepository;
            _loginUserRepository = loginUserRepository;
        }

        /// <summary>
        /// マスタ―情報取得
        /// </summary>
        /// <param name="id">マスターID</param>
        /// <returns>CodeDataDtoのリスト</returns>
        public async Task<IEnumerable<CodeDataDto>> GetMasterCodeDataAsync(int id)
        {
            IEnumerable<Models.MCodeDatum> entities = await _codeDataRepository.GetByCodeIdAsync(id);
            return entities.Select(x => new CodeDataDto()
            {
                id = x.CodeData,
                name = x.CodeName,
            });
        }
    }
}
