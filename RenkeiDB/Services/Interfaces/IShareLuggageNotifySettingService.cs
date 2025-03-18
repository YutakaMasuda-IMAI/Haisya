using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.SettingDto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 共有荷物通知設定サービスインターフェース
    /// </summary>
    public interface IShareLuggageNotifySettingService
    {
        /// <summary>
        /// 共有荷物通知設定を更新します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="loginId">ログインID</param>
        /// <param name="dtos">共有荷物通知設定DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateShareLuggageNotifySetting(int id, string loginId, CreateShareLuggageNotifySettingDto dtos);

        /// <summary>
        /// 共有荷物通知設定を作成します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="dtos">共有荷物通知設定DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateShareLuggageNotifySetting(string loginId, CreateShareLuggageNotifySettingDto dtos);

        /// <summary>
        /// 共有荷物通知設定の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>荷物通知設定DTO</returns>
        Task<LuggageNotifySettingDto> GetDetailShareLuggageNotifySetting(int id);

        /// <summary>
        /// 共有荷物通知設定を削除します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> DeleteShareLuggageNotifySetting(int id);
    }
}
