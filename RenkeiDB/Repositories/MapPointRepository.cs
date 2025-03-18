using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.MapPointDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 地図ポイントリポジトリ
    /// </summary>
    public class MapPointRepository : RepositoryBaseAsync<T_Point, ApplicationDbContext>, IMapPointRepository
    {
        public MapPointRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {

        }

        /// <summary>
        /// 地図ポイント一覧取得
        /// </summary>
        /// <param name="userId">ログインしているユーザーのID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="address2">住所2</param>
        /// <returns>地図ポイントのリスト</returns>
        public async Task<IEnumerable<MapPointDto>> GetListPointByUserIdAndAddress2(int userId, int? groupId, string address2)
        {
            List<T_Point> rs = new List<T_Point>();
            if (userId != 0)
            {
                rs = await DbContext.Set<T_Point>().Where(p => p.User_ID == userId ||
                       DbContext.Set<M_CompanyUser_GroupUser>().Where(m => m.User_ID == userId)
                       .Select(m => m.Group_ID).Contains(p.Group_ID)).ToListAsync();
            }
            if (groupId != null)
            {
                rs = await DbContext.Set<T_Point>().Where(p => p.Group_ID == groupId).ToListAsync();
            }
            if (!string.IsNullOrEmpty(address2))
            {
                rs = rs.Where(r => r.Address2.Contains(address2)).ToList();
            }
            return rs.Select(x => Mapper.ConvertEntityMapPointDto(x));
        }
    }
}
