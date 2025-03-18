using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using RenkeiDB.Dto;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 共有車両詳細リポジトリ
    /// </summary>
    public class ShareSyaryoDetailRepository : RepositoryBaseAsync<T_Share_Syaryo_Detail, ApplicationDbContext>, IShareSyaryoDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShareSyaryoDetailRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 共有車両詳細を取得する
        /// </summary>
        /// <param name="shareSyaryoId">共有車両ID</param>
        /// <param name="orderNum">注文番号</param>
        /// <returns>共有車両詳細</returns>
        public async Task<T_Share_Syaryo_Detail> GetDetailAsync(int shareSyaryoId, int orderNum = 0)
        {
            IQueryable<T_Share_Syaryo_Detail> builder = FindByCondition(s => s.Share_Syaryo_ID == shareSyaryoId);
            builder = orderNum != 0
                ? builder.Where(s => s.Share_Syaryo_Order == orderNum)
                : builder.OrderByDescending(s => s.Share_Syaryo_Order);
            return await builder.FirstOrDefaultAsync();
        }
    }
}
