using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 車両情報リポジトリ
    /// </summary>
    public class CarInfoRepository : RepositoryBaseAsync<T_Share_Syaryo, ApplicationDbContext>, ICarInfoRepository
    {
        public CarInfoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 車番から最新の車両情報の取得
        /// </summary>
        /// <param name="carNo">車番</param>
        /// <param name="groupIds">担当グループIDリスト</param>
        /// <returns>最新の車両情報</returns>
        public async Task<JoinCarInfoDto> GetCarInfoAsync(string carNo, int[] groupIds)
        {
            return await (from tss in DbContext.Set<T_Share_Syaryo>()
                           join tssd in DbContext.Set<T_Share_Syaryo_Detail>()
                           on new { id = tss.Share_Syaryo_ID, order = tss.Share_Syaryo_Latest_Order }
                           equals new { id = tssd.Share_Syaryo_ID, order = tssd.Share_Syaryo_Order }
                           where
                                groupIds.Contains(tss.Tantou_Group_ID)
                                && tssd.Syaban == carNo
                           orderby tssd.Insert_Datetime descending
                           select new JoinCarInfoDto
                           {
                               shareSyaryo = tss,
                               shareSyaryoDetail = tssd,
                           }
                           ).FirstOrDefaultAsync();
        }
    }
}
