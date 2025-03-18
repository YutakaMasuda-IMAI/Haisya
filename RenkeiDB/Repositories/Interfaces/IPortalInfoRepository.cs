using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// ポータル情報リポジトリのインターフェースを定義します。
    /// </summary>
    public interface IPortalInfoRepository : IRepositoryBaseAsync<T_Portal_Info, ApplicationDbContext>
    {
        /// <summary>
        /// 指定された情報ID、表示フラグ、およびユーザーIDに基づいて表示フラグを設定します。
        /// </summary>
        /// <param name="info_id">情報ID</param>
        /// <param name="display_flg">表示フラグ</param>
        /// <param name="user_id">ユーザーID</param>
        /// <returns>タスク</returns>
        Task Set_display_flg(int info_id, int display_flg, int user_id);
    }
}
