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
    /// 荷物通知設定リポジトリ
    /// </summary>
    public class LuggageNotifySettingRepository : RepositoryBaseAsync<T_Share_Luggage_Notify_Setting, ApplicationDbContext>, ILuggageNotifySettingRepository
    {
        public LuggageNotifySettingRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// idで荷物通知設定詳細取得
        /// </summary>
        /// <param name="id">荷物のidを通知設定</param>
        /// <returns>
        /// データがある場合: 荷物通知設定データ
        /// データがない場合: null
        /// </returns>
        public async Task<LuggageNotifySettingDto> GetDetailAsync(int id)
        {
            JoinLuggageNotifySettingDto data = await (from S in DbContext.Set<T_Share_Luggage_Notify_Setting>()
                              join UG in DbContext.Set<M_CompanyUser_Group>()
                              on S.Tantou_Group_ID equals UG.Group_ID
                              where S.Share_Luggage_Notify_Setting_ID == id
                              orderby S.Share_Luggage_Notify_Setting_ID ascending
                              select new JoinLuggageNotifySettingDto
                              { 
                                  S = S,
                                  UG = UG 
                              }).FirstOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return Mapper.ConvertEntityLuggageNotifyToDTO(data.S, data.UG);
        }

        /// <summary>
        /// グループIDで荷物通知設定を取得
        /// </summary>
        /// <param name="groupIds">グループIDの配列</param>
        /// <returns>荷物通知設定のリスト</returns>
        public async Task<IEnumerable<LuggageNotifySettingDto>> GetSettingsAsync(int[] groupIds)
        {
            if (groupIds == null || groupIds.Length == 0)
            {
                return Enumerable.Empty<LuggageNotifySettingDto>();
            }
            List<JoinLuggageNotifySettingDto> data = await (from S in DbContext.Set<T_Share_Luggage_Notify_Setting>()
                              join UG in DbContext.Set<M_CompanyUser_Group>()
                              on S.Tantou_Group_ID equals UG.Group_ID
                              where groupIds.Contains(S.Tantou_Group_ID)
                              orderby S.Share_Luggage_Notify_Setting_ID ascending
                              select new JoinLuggageNotifySettingDto
                              {
                                  S = S,
                                  UG = UG
                              }).ToListAsync();

            return data.Select(d =>
            {
                LuggageNotifySettingDto data = Mapper.ConvertEntityLuggageNotifyToDTO(d.S, d.UG);
                data.oroshis = data.oroshis.Where(item => !string.IsNullOrEmpty(item)).ToList();
                data.tumis = data.tumis.Where(item => !string.IsNullOrEmpty(item)).ToList();
                return data;
            });
        }
    }
}
