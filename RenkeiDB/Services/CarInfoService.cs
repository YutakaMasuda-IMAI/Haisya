using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    public class CarInfoService : ICarInfoService
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyUserGroupRepository _companyUserGroupRepository;
        public CarInfoService(ICarInfoRepository carInfoRepository, ICompanyRepository companyRepository, ICompanyUserGroupRepository companyUserGroupRepository)
        {
            _carInfoRepository = carInfoRepository;
            _companyRepository = companyRepository;
            _companyUserGroupRepository = companyUserGroupRepository;
        }

        /// <summary>
        /// 車番から最新の車両情報の取得
        /// </summary>
        /// <param name="carNo">車両番号</param>
        /// <param name="groupIds">担当グループIDのリスト</param>
        /// <returns>車両情報を含むタスク</returns>
        public async Task<CarInfoDto> GetCarInfoAsync(string carNo, int[] groupIds)
        {
            JoinCarInfoDto data = await _carInfoRepository.GetCarInfoAsync(carNo, groupIds);
            if (data is null)
            {
                return null;
            }
            M_CompanyBranch companyBranch = await _companyRepository.GetDetailCompanyBranchAsync(data.shareSyaryo.Branch_ID);
            M_CompanyUser_Group companyUserGroupByTantouGroupId = await _companyUserGroupRepository.GetCompanyUserGroupByIdAsync(data.shareSyaryo.Tantou_Group_ID);
            return Mapper.ConvertToCarInfoEntity(data.shareSyaryo, data.shareSyaryoDetail, companyBranch, companyUserGroupByTantouGroupId);
        }
    }
}
