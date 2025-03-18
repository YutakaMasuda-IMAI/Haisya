using RenkeiDB.Data;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// 車両通知設定リポジトリのインターフェースを定義します。
    /// </summary>
    public interface ISyaryoNotifySettingRepository : IRepositoryBaseAsync<T_Share_Syaryo_Notify_Setting, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたグループIDに基づいて車両通知設定を取得します。
        /// </summary>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>車両通知設定のコレクションを含むタスク</returns>
        Task<IEnumerable<SyaryoNotifySettignDto>> GetSettingsAsync(int[] groupIds);

        /// <summary>
        /// 指定されたIDに基づいて車両通知設定の詳細を取得します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>車両通知設定の詳細を含むタスク</returns>
        Task<SyaryoNotifySettignDto> GetDetailAsync(int id);
    }
}
