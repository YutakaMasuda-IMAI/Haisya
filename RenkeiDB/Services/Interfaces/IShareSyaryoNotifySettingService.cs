using RenkeiDB.Dto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Dto.SyaryoDto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 共有車両通知設定サービスインターフェース
    /// </summary>
    public interface IShareSyaryoNotifySettingService
    {
        /// <summary>
        /// 共有車両通知設定を作成します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">共有車両通知設定DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> CreateShareSyaryoNotifySetting(string loginId, CreateShareSyaryoNotifySettingDto dto);

        /// <summary>
        /// 共有車両通知設定を更新します。
        /// </summary>
        /// <param name="id">設定ID</param>
        /// <param name="loginId">ログインID</param>
        /// <param name="dto">共有車両通知設定DTO</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateShareSyaryoNotifySetting(int id, string loginId, CreateShareSyaryoNotifySettingDto dto);

        /// <summary>
        /// 共有車両通知設定の詳細を取得します。
        /// </summary>
        /// <param name="id">設定ID</param>
        /// <returns>共有車両通知設定DTO</returns>
        Task<SyaryoNotifySettignDto> GetDetailShareSyaryoNotifySetting(int id);

        /// <summary>
        /// 共有車両通知設定を削除します。
        /// </summary>
        /// <param name="id">設定ID</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> DeleteShareSyaryoNotifySetting(int id);
    }
}
