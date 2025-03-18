using RenkeiDB.Dto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 配車通知設定サービスインターフェース
    /// </summary>
    public interface IHaisyaNotifySettingService
    {
        /// <summary>
        /// データを非同期で更新します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="notifyType">通知タイプ</param>
        /// <returns>APIレスポンス</returns>
        Task<ApiResponse> UpdateDataAsync(int userId, int notifyType);
    }
}
