using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Dto.SyaryoDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 空車車両通知設定サービスを提供します。
    /// </summary>
    public class ShareSyaryoNotifySettingSerrvice : IShareSyaryoNotifySettingService
    {
        private readonly ISyaryoNotifySettingRepository _syaryoNotifySettingRepository;
        private readonly IMasterCodeDataRepository _masterCodeDataRepository;
        private readonly IUserService _userService;
        private readonly ISyaryoRepository _syaryoRepository;

        public ShareSyaryoNotifySettingSerrvice(ISyaryoNotifySettingRepository shareSyaryoNotifySettingRepository, IMasterCodeDataRepository masterCodeDataRepository, IUserService userService, ISyaryoRepository syaryoRepository)
        {
            _syaryoNotifySettingRepository = shareSyaryoNotifySettingRepository;
            _masterCodeDataRepository = masterCodeDataRepository;
            _userService = userService;
            _syaryoRepository = syaryoRepository;
        }

        /// <summary>
        /// 車両通知設定の作成
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">作成データ</param>
        /// <returns>作成結果</returns>
        public async Task<ApiResponse> CreateShareSyaryoNotifySetting(string loginId, CreateShareSyaryoNotifySettingDto dto)
        {
            if (dto.isEmpty == true && dto.empties.Length == 0)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "空車")
                };
            }

            if (dto.isDest == true && dto.dests.Length == 0)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "積地")
                };
            }

            if (dto.isSyasyu == true && string.IsNullOrEmpty(dto.syasyu))
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "車種")
                };
            }

            if (string.IsNullOrEmpty(dto.fromDate) && string.IsNullOrEmpty(dto.toDate))
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.MissingParams, "開始日", "終了日")
                };
            }

            UserDto dataUser = await _userService.GetLoginUserInfo(loginId);
            M_Code_Datum masterCode = dto.syasyu != null
                                ? await _masterCodeDataRepository.FindByCondition(m => m.Code_ID == 13 && m.Code_Data == dto.syasyu.ToString()).FirstOrDefaultAsync()
                                : null;

            if (masterCode != null && masterCode.Code_Name_abbr?.Length > 30)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.InvalidNumberLength, "コード_名_略称")
                };
            }

            if (dataUser == null)
            {
                return new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
            }

            T_Share_Syaryo_Notify_Setting data = Mapper.ConvertToShareSyaryoNotifySettingEntity(dto, dataUser, masterCode);
            if (int.TryParse(dto.syasyu, out int syasyuId))
            {
                M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(syasyuId);
                data.SyasuyDisplay = syaryo.SyasyuDisplay;
            }

            await _syaryoNotifySettingRepository.CreateAsync(data);

            return new() { Code = StatusCodes.Status200OK };
        }

        /// <summary>
        /// 車両通知設定の削除
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <returns>削除結果</returns>
        public async Task<ApiResponse> DeleteShareSyaryoNotifySetting(int id)
        {
            T_Share_Syaryo_Notify_Setting data = await _syaryoNotifySettingRepository.FindByCondition(s => s.Share_Syaryo_Notify_Setting_ID == id).FirstOrDefaultAsync();

            if (data == null)
            {
                return new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
            }

            await _syaryoNotifySettingRepository.DeleteAsync(data);
            await _syaryoNotifySettingRepository.SaveChangeAsync();

            return new() { Code = StatusCodes.Status200OK };
        }

        /// <summary>
        /// IDで車両通知設定の詳細取得
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <returns>車両通知設定データ</returns>
        public Task<SyaryoNotifySettignDto> GetDetailShareSyaryoNotifySetting(int id)
        {
            return _syaryoNotifySettingRepository.GetDetailAsync(id);
        }

        /// <summary>
        /// 車両通知設定の更新
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>更新結果</returns>
        public async Task<ApiResponse> UpdateShareSyaryoNotifySetting(int id, string loginId, CreateShareSyaryoNotifySettingDto dto)
        {
            UserDto dataUser = await _userService.GetLoginUserInfo(loginId);
            M_Code_Datum masterCode = dto.syasyu != null
                                ? await _masterCodeDataRepository.FindByCondition(m => m.Code_ID == 13 && m.Code_Data == dto.syasyu.ToString()).FirstOrDefaultAsync()
                                : null;
            T_Share_Syaryo_Notify_Setting shareSyaryoData = await _syaryoNotifySettingRepository.FindByCondition(s => s.Share_Syaryo_Notify_Setting_ID == id).FirstOrDefaultAsync();

            // Validate max length Code_Name_abbr
            if (masterCode != null && masterCode.Code_Name_abbr?.Length > 30)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.InvalidNumberLength, "コード_名_略称")
                };
            }

            if (dataUser == null || shareSyaryoData == null)
            {
                return new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
            }

            T_Share_Syaryo_Notify_Setting data = Mapper.ConvertToShareSyaryoNotifySettingEntity(dto, dataUser, masterCode);
            data.Share_Syaryo_Notify_Setting_ID = shareSyaryoData.Share_Syaryo_Notify_Setting_ID;
            data.Insert_User = shareSyaryoData.Insert_User;
            data.Insert_Datetime = shareSyaryoData.Insert_Datetime;
            if (int.TryParse(dto.syasyu, out int syasyuId))
            {
                M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(syasyuId);
                data.SyasuyDisplay = syaryo.SyasyuDisplay;
            }

            await _syaryoNotifySettingRepository.UpdateAsync(data);

            return new() { Code = StatusCodes.Status200OK };
        }
    }
}
