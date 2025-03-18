using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// ポータル情報リポジトリ
    /// </summary>
    public class PortalInfoRepository : RepositoryBaseAsync<T_Portal_Info, ApplicationDbContext>, IPortalInfoRepository
    {
        public PortalInfoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 表示フラグを設定する
        /// </summary>
        /// <param name="info_id">情報ID</param>
        /// <param name="display_Flg">表示フラグ</param>
        /// <param name="user_id">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task Set_display_flg(int info_id, int display_Flg, int user_id)
        {
            T_Portal_Info pi = await FindByCondition(p => p.Portal_Info_ID == info_id, true).FirstOrDefaultAsync();
            if (pi != default)
            {
                // display_flgを設定
                pi.Display_Flg = display_Flg;
                // 更新日時を設定
                pi.Update_User = user_id;
                pi.Update_Datetime = DateTime.Now;
                await SaveChangeAsync();
            }
        }
    }
}
