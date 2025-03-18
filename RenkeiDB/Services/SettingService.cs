using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Services
{
    public class SettingService : ISettingService
    {
        private readonly ILuggageNotifySettingRepository _luggageRepository;
        private readonly ISyaryoNotifySettingRepository _syaryoRepository;
        private readonly ICompanyUserRepository _companyUserRepository;

        public SettingService(ILuggageNotifySettingRepository luggageNotifySettingRepository, 
            ISyaryoNotifySettingRepository syaryoNotifySettingRepository,
            ICompanyUserRepository companyUserRepository)
        {
            _luggageRepository = luggageNotifySettingRepository;
            _syaryoRepository = syaryoNotifySettingRepository;
            _companyUserRepository = companyUserRepository;
        }

        public async Task<SettingEntryDto> GetSettingAsync(int[] groupIds, int companyUserId)
        {
            IEnumerable<LuggageNotifySettingDto> luggageSettings = await _luggageRepository.GetSettingsAsync(groupIds);
            IEnumerable<SyaryoNotifySettignDto> syaryoSettings = await _syaryoRepository.GetSettingsAsync(groupIds);
            M_CompanyUser companyUser = await _companyUserRepository.GetByIdAsync(companyUserId);
            SettingEntryDto settingEntryDTO = null;
            if ((luggageSettings != null && luggageSettings.Any()) ||
                (syaryoSettings != null && syaryoSettings.Any()) || companyUser != null)
            {
                settingEntryDTO = new SettingEntryDto()
                {
                    notifyType = Enum.IsDefined(typeof(NotifyType), companyUser.Haisya_Send_Kubun) ? (NotifyType)companyUser.Haisya_Send_Kubun : NotifyType.NOTIFY_WHEN_EACH_CONFIRMED,
                    shareLuggageNotifySettings = luggageSettings,
                    shareSyaryoNotifySettigns = syaryoSettings,
                };
            }
            return settingEntryDTO;
        }
    }
}
