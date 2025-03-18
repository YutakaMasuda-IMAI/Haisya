using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.MasterLuggageDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    public class MasterLuggageRepository : RepositoryBaseAsync<M_Luggage_Group, ApplicationDbContext>, IMasterLuggageRepository
    {
        public MasterLuggageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 荷物情報を取得する
        /// </summary>
        /// <param name="companyId">ログインしているユーザーの会社ID</param>
        /// <returns>荷物情報のリスト</returns>
        public async Task<IEnumerable<MasterLuggageGroupsDto>> GetMasterLuggageGroups(int companyId)
        {
            // 荷物情報を取得する
            List<M_Luggage_Group> b = await (from t1 in DbContext.Set<M_Luggage_Group>()
                            where t1.Company_ID == companyId
                                && t1.Del_Flg == false
                            orderby t1.SortOrder ascending
                            select t1).ToListAsync();
            // 取得した荷物情報をDtoに変換する
            // 取得した荷物情報がnullの場合はnullを返す
            if (b == null)
            {
                return null;
            }
            return Mapper.ConvertEntityLuggageGroupToDto(b);
        }

        /// <summary>
        /// 荷物詳細を取得する
        /// </summary>
        /// <param name="mLuggageGroupId">荷物グループID</param>
        /// <returns>荷物詳細のリスト</returns>
        public async Task<IEnumerable<MasterLuggageDto>> GetMasterLuggagesByLuggageGroupId(int mLuggageGroupId)
        {
            List<M_Luggage> b = await (from t1 in DbContext.Set<M_Luggage_Group>()
                          join t2 in DbContext.Set<M_Luggage>()
                              on new { id = t1.Luggage_Group_ID }
                              equals new { id = t2.Luggage_Group_ID }
                          where t1.Luggage_Group_ID == mLuggageGroupId
                          orderby t2.SortOrder ascending
                          select t2).ToListAsync();
            if (b == null)
            {
                return null;
            }
            return Mapper.ConvertEntityLuggageToDto(b);
        }
    }
}
