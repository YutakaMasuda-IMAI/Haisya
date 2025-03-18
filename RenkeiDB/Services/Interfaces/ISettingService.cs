using RenkeiDB.Dto.SettingDto;
using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// 設定サービスインターフェース
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// 設定を非同期で取得します。
        /// </summary>
        /// <param name="groupIds">グループIDの配列</param>
        /// <param name="companyUserId">会社ユーザーID</param>
        /// <returns>設定エントリーDTO</returns>
        Task<SettingEntryDto> GetSettingAsync(int[] groupIds, int companyUserId);
    }
}
