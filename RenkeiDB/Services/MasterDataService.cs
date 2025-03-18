using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Repositories;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// マスターデータサービス
    /// </summary>
    public class MasterDataService : IMasterDataService
    {
        private readonly IMasterCodeDataRepository _masterCodeDataRepository;
        private readonly IPostCodeRepository _postCodeRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentGroupRepository _equipmentGroupRepository;
        private readonly ISyaryoRepository _syaryoRepository;
        public MasterDataService(
            IMasterCodeDataRepository masterCodeDataRepository,
            IPostCodeRepository postCodeRepository,
            IEquipmentRepository equipmentRepository,
            IEquipmentGroupRepository equipmentGroupRepository,
            ISyaryoRepository syaryoRepository
        )
        {
            _masterCodeDataRepository = masterCodeDataRepository;
            _postCodeRepository = postCodeRepository;
            _equipmentRepository = equipmentRepository;
            _equipmentGroupRepository = equipmentGroupRepository;
            _syaryoRepository = syaryoRepository;
        }

        /// <summary>
        /// マスターコードデータを取得する
        /// </summary>
        /// <param name="codeId">コードID</param>
        /// <returns>マスターコードデータのリスト</returns>
        public async Task<IEnumerable<MasterCodeDataDto>> GetMasterCodeData(int codeId)
        {
            IEnumerable<M_Code_Datum> data = await _masterCodeDataRepository.GetMasterCodeData(codeId);

            List<MasterCodeDataDto> codes = data.Select(m => new MasterCodeDataDto
            {
                id = m.Code_Data,
                name = m.Code_Name_abbr,
            }).ToList();

            return codes;
        }

        /// <summary>
        /// 都道府県名の取得
        /// </summary>
        /// <returns>都道府県名の配列</returns>
        public Task<IEnumerable<string>> GetPostCodeData()
        {
            return _postCodeRepository.GetListKens();
        }

        /// <summary>
        /// 住所情報の取得
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <param name="shikucho">市区町村名</param>
        /// <returns>住所情報の配列</returns>
        public Task<IEnumerable<string>> GetAddressByKenAsync(string ken, string shikucho)
        {
            if (shikucho == null)
            {
                return _postCodeRepository.GetAddressByKenAsync(ken);
            }
            return _postCodeRepository.GetAddressByConditionsAsync(ken, shikucho);
        }

        /// <summary>
        /// 装備品の作成
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">装備品作成DTO</param>
        /// <returns>APIレスポンス</returns>
        public async Task<ApiResponse> CreateEquipmentAsync(int companyId, int userId, CreateEquipmentDto dto)
        {
            try
            {
                await _equipmentRepository.BeginTransactionAsync();

                IEnumerable<M_Equipment> equipments = await _equipmentRepository.GetEquipmentByCompanyIdAsync(companyId);
                equipments = equipments.Where(x => x.Equipment_Group_ID == dto.equipmentGroupId);
                int sortOrder = 1;
                if (equipments.Count() > 0)
                {
                    sortOrder = equipments.Max(x => x.SortOrder) + 1;
                }

                M_Equipment data = Mapper.ConvertToEquipmentEntity(dto, companyId, userId, sortOrder);

                await _equipmentRepository.CreateAsync(data);
                await _equipmentRepository.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (System.Exception)
            {
                await _equipmentRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 装備品グループの取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <returns>装備品グループのリスト</returns>
        public async Task<IEnumerable<EquipmentGroupDto>> GetEquipmentGroupAsync(int companyId)
        {
            IEnumerable<M_Equipment_Group> equipments = await _equipmentGroupRepository.GetEquipmentGroupByCompanyIdAsync(companyId);
            IEnumerable<EquipmentGroupDto> data = Mapper.ConvertToEquipmentGroupEntity(equipments);
            return data;
        }

        /// <summary>
        /// 車両マスター取得
        /// </summary>
        /// <param name="syasu">車種</param>
        /// <param name="kata">型</param>
        /// <returns>車両マスターのリスト</returns>
        public async Task<IList<SyaryoDto>> GetSyaryoAsync(string syasu, string kata)
        {
            IQueryable<M_Syaryo> query = _syaryoRepository.FindAll();

            if (!string.IsNullOrEmpty(syasu))
            {
                query = query.Where(x => x.SYASYU == syasu);
            }

            if (!string.IsNullOrEmpty(kata))
            {
                query = query.Where(x => x.KATA == kata);
            }

            return await query.OrderBy(x => x.SortOrder)
                               .Select(x => Mapper.ConvertToSyaryoEntity(x))
                               .ToListAsync();
        }

        /// <summary>
        /// 住所選択　API（郵便番号取得）
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <param name="shikucho">市区町村名</param>
        /// <param name="choiki">町域名</param>
        /// <returns>郵便番号</returns>
        public async Task<string> GetPostCodeAsync(string ken, string shikucho, string choiki)
        {
            string postCode = await _postCodeRepository.GetPostCodeAsync(ken, shikucho, choiki);
            if (!string.IsNullOrEmpty(postCode))
            {
                postCode = CommonHelper.FormatPostCode(postCode);
            }
            return postCode;
        }

        /// <summary>
        /// 住所リストの取得
        /// </summary>
        /// <param name="word">検索ワード</param>
        /// <returns>住所リスト</returns>
        public async Task<IList<AddressDto>> GetAddressList(string word)
        {
            return await _postCodeRepository.FindByCondition(x => (x.CHO_IKI != null && EF.Functions.Like(x.CHO_IKI, $"%{word}%")) ||
                            (x.SHI_KU_CHO != null && EF.Functions.Like(x.SHI_KU_CHO, $"%{word}%")))
                        .Select(x => Mapper.ConvertEntityPostCodeToDto(x)).ToListAsync();
        }
    }
}
