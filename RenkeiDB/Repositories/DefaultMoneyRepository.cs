using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// デフォルト料金リポジトリ
    /// </summary>
    public class DefaultMoneyRepository : RepositoryBaseAsync<M_DefaultMoney, ApplicationDbContext>, IDefaultMoneyRepository
    {
        public DefaultMoneyRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
        : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// デフォルト料金の取得
        /// </summary>
        /// <returns>デフォルト料金のリスト</returns>
        public async Task<IList<DefaultMoneyDto>> GetDefaultMoneysAsync()
        {
            return await DbContext.Set<M_DefaultMoney>()
                .Select(m => new DefaultMoneyDto
                {
                    area = m.Area,
                    syasyuSize = m.SyasyuSize,
                    fromDistance = m.From_Distance,
                    toDistance = m.To_Distance,
                    Interval = m.Interval,
                    amount = m.Amount,
                    additionAmount = m.AdditionAmount
                })
                .ToListAsync();
        }
    }
}
