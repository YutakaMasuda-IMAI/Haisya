using RenkeiDB.Data;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 荷物通知設定リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ILuggageNotifySettingRepository : IRepositoryBaseAsync<T_Share_Luggage_Notify_Setting, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたIDに基づいて荷物通知設定の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>荷物通知設定の詳細を含むタスク</returns>
        Task<LuggageNotifySettingDto> GetDetailAsync(int id);

        /// <summary>
        /// 指定されたグループIDに基づいて荷物通知設定を取得します。
        /// </summary>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>荷物通知設定のコレクションを含むタスク</returns>
        Task<IEnumerable<LuggageNotifySettingDto>> GetSettingsAsync(int[] groupIds);
    }
}
