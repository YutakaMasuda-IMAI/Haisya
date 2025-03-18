using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 車両通知設定リポジトリ
    /// </summary>
    public class SyaryoNotifySettingRepository : RepositoryBaseAsync<T_Share_Syaryo_Notify_Setting, ApplicationDbContext>, ISyaryoNotifySettingRepository
    {
        public SyaryoNotifySettingRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// IDで車両通知設定詳細を取得する
        /// </summary>
        /// <param name="id">車両通知設定ID</param>
        /// <returns>車両通知設定詳細</returns>
        public async Task<SyaryoNotifySettignDto> GetDetailAsync(int id)
        {
            JoinSyaryoNotifySettingDto data = await (from S in DbContext.Set<T_Share_Syaryo_Notify_Setting>()
                              join UG in DbContext.Set<M_CompanyUser_Group>()
                              on S.Tantou_Group_ID equals UG.Group_ID
                              where id == S.Share_Syaryo_Notify_Setting_ID
                              select new JoinSyaryoNotifySettingDto { S = S, UG = UG }).FirstOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return Mapper.ConvertEntitySyaryoToDTO(data.S, data.UG);
        }

        /// <summary>
        /// グループIDで車両通知設定を取得する
        /// </summary>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>車両通知設定のリスト</returns>
        public async Task<IEnumerable<SyaryoNotifySettignDto>> GetSettingsAsync(int[] groupIds)
        {
            if (groupIds == null || groupIds.Length == 0)
            {
                return Enumerable.Empty<SyaryoNotifySettignDto>();
            }
            List<JoinSyaryoNotifySettingDto> data = await (from S in DbContext.Set<T_Share_Syaryo_Notify_Setting>()
                              join UG in DbContext.Set<M_CompanyUser_Group>()
                              on S.Tantou_Group_ID equals UG.Group_ID
                              where groupIds.Contains(S.Tantou_Group_ID)
                              orderby S.Share_Syaryo_Notify_Setting_ID ascending
                              select new JoinSyaryoNotifySettingDto { S = S, UG = UG }).ToListAsync();

            return data.Select(d =>
            {
                SyaryoNotifySettignDto data = Mapper.ConvertEntitySyaryoToDTO(d.S, d.UG);
                data.dests = data.dests.Where(item => !string.IsNullOrEmpty(item)).ToList();
                data.empties = data.empties.Where(item => !string.IsNullOrEmpty(item)).ToList();
                return data;
            });
        }
    }
}
