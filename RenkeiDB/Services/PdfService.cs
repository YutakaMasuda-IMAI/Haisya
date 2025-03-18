using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.PdfDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// PDFサービスクラス
    /// </summary>
    public class PdfService : IPdfService
    {
        private readonly ICompanyPortalRepository _companyPortalRepo;

        public PdfService(ICompanyPortalRepository companyPortalRepo)
        {
            _companyPortalRepo = companyPortalRepo;
        }

        /// <summary>
        /// 車番連絡票印刷（PDF形式）
        /// </summary>
        /// <param name="companyId">ログインユーザーのcompany_id</param>
        /// <param name="branchId">ログインユーザーのbranch_id</param>
        /// <returns>車番連絡票のグループデータ</returns>
        public async Task<IEnumerable<ContactCarNumberGroupDto>> PrintSyabanNotifyAsync(int companyId, int branchId)
        {
            IEnumerable<JoinCompanyPortalDto> ankens = await _companyPortalRepo.GetIraiAnkensAsync(companyId, branchId);

            int[] iraiAnkenIds = ankens
                .Select(d => d.renkeiAnken.Renkei_Anken_ID).Distinct()
            .ToArray();

            IEnumerable<AnkenSecureDto> ankenSecures = await _companyPortalRepo.GetSecuresAsync(iraiAnkenIds);

            IEnumerable<JoinShareLuggageDto> luggages = await _companyPortalRepo.GetLuggagesAsync(companyId, branchId);

            IEnumerable<ContactCarNumberDto> result = Mapper.MappingDataContactNumberCar(ankens, ankenSecures, luggages);

            List<ContactCarNumberGroupDto> groupData = result.GroupBy(x => new {
                x.kokyakuName, x.haisyaTanto, x.haisyaTantoPhone
            }).Select(g => new ContactCarNumberGroupDto
            {
                kokyakuName = g.Key.kokyakuName,
                haisyaTanto = g.Key.haisyaTanto,
                haisyaTantoPhone = g.Key.haisyaTantoPhone,
                ContactCarNumberGroup = g.Select(u => new ContactCarNumberDto
                {
                    id = u.id,
                    status = u.status,
                    ankenNo = u.ankenNo,
                    shareLuggageNo = u.shareLuggageNo,
                    syasyuDisplay = u.syasyuDisplay,
                    tumiDatetime = u.tumiDatetime,
                    tumiAddress = u.tumiAddress,
                    oroshiDatetime = u.oroshiDatetime,
                    oroshiAddress = u.oroshiAddress,
                    remarks = u.remarks,
                    kokyakuName = u.kokyakuName,
                    vehicleRentalDestination = u.vehicleRentalDestination,
                    syaban = u.syaban,
                    driverName = u.driverName,
                    drivePhone = u.drivePhone,
                    haisyaTanto = u.haisyaTanto,
                    haisyaTantoPhone = u.haisyaTantoPhone,
                }).ToList(),
            }).ToList();

            return groupData;
        }
    }
}
