using RenkeiDB.Dto.MasterLuggageDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 荷物マスターサービス
    /// </summary>
    public class MasterLuggagesService: IMasterLuggagesService
    {
        private readonly IMasterLuggageRepository _masterLuggageRepository;
        public MasterLuggagesService(IMasterLuggageRepository masterLuggageRepository)
        {
            _masterLuggageRepository = masterLuggageRepository;
        }

        /// <summary>
        /// 荷物情報を取得する
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <returns>荷物情報のリスト</returns>
        public async Task<IEnumerable<MasterLuggageGroupsDto>> GetMasterLuggageGroups(int companyId)
        {
            return await _masterLuggageRepository.GetMasterLuggageGroups(companyId);
        }

        /// <summary>
        /// 荷物詳細を取得する
        /// </summary>
        /// <param name="mLuggageGroupId">荷物グループID</param>
        /// <returns>荷物詳細のリスト</returns>
        public async Task<IEnumerable<MasterLuggageDto>> GetMasterLuggagesByLugGroupId(int mLuggageGroupId)
        {
            return await _masterLuggageRepository.GetMasterLuggagesByLuggageGroupId(mLuggageGroupId);
        }
    }
}
