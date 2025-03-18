using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static RenkeiDB.Common.SystemEnums;
using Microsoft.AspNetCore.Http;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 荷物通知設定サービスを提供します。
    /// </summary>
    public class ShareLuggageNotifySettingService : IShareLuggageNotifySettingService
    {
        private readonly ILuggageNotifySettingRepository _luggageNotifySettingRepository;
        private readonly IMasterCodeDataRepository _masterCodeDataRepository;
        private readonly IUserService _userService;
        private readonly ISyaryoRepository _syaryoRepository;
        public ShareLuggageNotifySettingService(ILuggageNotifySettingRepository luggageNotifySettingRepository, IMasterCodeDataRepository masterCodeDataRepository, IUserService userService, ISyaryoRepository syaryoRepository)
        {
            _luggageNotifySettingRepository = luggageNotifySettingRepository;
            _masterCodeDataRepository = masterCodeDataRepository;
            _userService = userService;
            _syaryoRepository = syaryoRepository;
        }

        /// <summary>
        /// 荷物通知設定の作成
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">作成データ</param>
        /// <returns>作成結果</returns>
        public async Task<ApiResponse> CreateShareLuggageNotifySetting(string loginId, CreateShareLuggageNotifySettingDto dto)
        {
            if (dto.isTumi == true && dto.tumis.Length == 0)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "積み")
                };
            }

            if (dto.isOroshi == true && dto.oroshis.Length == 0)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "卸")
                };
            }

            if (dto.isSyasyu == true && dto.syasyu == null)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.RequiredField, "車種")
                };
            }

            UserDto dataUser = await _userService.GetLoginUserInfo(loginId);
            M_Code_Datum masterCode = dto.syasyu != null
                                ? await _masterCodeDataRepository.FindByCondition(m => m.Code_ID == (int)MasterCode.CodeID.SYASYU && m.Code_Data == dto.syasyu.ToString())
                                    .FirstOrDefaultAsync()
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

            T_Share_Luggage_Notify_Setting data = Mapper.ConvertToLuggageNotifySettingEntity(dto, dataUser, masterCode);
            if (int.TryParse(dto.syasyu, out int syasyuId))
            {
                M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(syasyuId);
                data.SyasuyDisplay = syaryo.SyasyuDisplay;
            }

            await _luggageNotifySettingRepository.CreateAsync(data);

            return new() { Code = StatusCodes.Status200OK };
        }

        /// <summary>
        /// 荷物通知設定の更新
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>更新結果</returns>
        public async Task<ApiResponse> UpdateShareLuggageNotifySetting(int id, string loginId, CreateShareLuggageNotifySettingDto dto)
        {
            UserDto dataUser = await _userService.GetLoginUserInfo(loginId);
            M_Code_Datum masterCode = dto.syasyu != null
                                ? await _masterCodeDataRepository.FindByCondition(m => m.Code_ID == (int)MasterCode.CodeID.SYASYU && m.Code_Data == dto.syasyu.ToString()).FirstOrDefaultAsync()
                                : null;
            T_Share_Luggage_Notify_Setting shareLuggageData = await _luggageNotifySettingRepository.FindByCondition(s => s.Share_Luggage_Notify_Setting_ID == id).FirstOrDefaultAsync();

            if (masterCode != null && masterCode.Code_Name_abbr?.Length > 30)
            {
                return new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = string.Format(SystemConstants.Message.InvalidNumberLength, "コード_名_略称")
                };
            }

            if (dataUser == null || shareLuggageData == null)
            {
                return new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
            }
            T_Share_Luggage_Notify_Setting data = Mapper.ConvertToLuggageNotifySettingEntity(dto, dataUser, masterCode);
            data.Share_Luggage_Notify_Setting_ID = shareLuggageData.Share_Luggage_Notify_Setting_ID;
            data.Insert_User = shareLuggageData.Insert_User;
            data.Insert_Datetime = shareLuggageData.Insert_Datetime;
            if (int.TryParse(dto.syasyu, out int syasyuId))
            {
                M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(syasyuId);
                data.SyasuyDisplay = syaryo.SyasyuDisplay;
            }
            await _luggageNotifySettingRepository.UpdateAsync(data);

            return new() { Code = StatusCodes.Status200OK };
        }

        /// <summary>
        /// 荷物通知設定の詳細取得
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <returns>荷物通知設定データ</returns>
        public Task<LuggageNotifySettingDto> GetDetailShareLuggageNotifySetting(int id)
        {
            return _luggageNotifySettingRepository.GetDetailAsync(id);
        }

        /// <summary>
        /// 荷物通知設定の削除
        /// </summary>
        /// <param name="id">通知設定のID</param>
        /// <returns>削除結果</returns>
        public async Task<ApiResponse> DeleteShareLuggageNotifySetting(int id)
        {
            T_Share_Luggage_Notify_Setting shareLuggageData = await _luggageNotifySettingRepository.FindByCondition(s => s.Share_Luggage_Notify_Setting_ID == id).FirstOrDefaultAsync();

            if (shareLuggageData == null)
            {
                return new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
            }

            await _luggageNotifySettingRepository.DeleteAsync(shareLuggageData);
            await _luggageNotifySettingRepository.SaveChangeAsync();

            return new() { Code = StatusCodes.Status200OK };
        }
    }
}
