using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 連携案件荷物リポジトリ
    /// </summary>
    public class RenkeiAnkenLuggageRepository : RepositoryBaseAsync<T_Renkei_Anken_Luggage, ApplicationDbContext>, IRenkeiAnkenLuggageRepository
    {
        public RenkeiAnkenLuggageRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 案件詳細取得
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <param name="anken_order">案件注文</param>
        /// <returns>案件荷物リスト</returns>
        public async Task<IList<T_Renkei_Anken_Luggage>> GetLuggages(int anken_id, int anken_order)
        {
            IQueryable<T_Renkei_Anken_Luggage> builder =
                from tral in DbContext.Set<T_Renkei_Anken_Luggage>()
                    .Include(tral => tral.Luggage)
                    .ThenInclude(luggage => luggage.Luggage_Group)
                where
                    tral.Renkei_Anken_ID == anken_id && tral.Renkei_Anken_Order == anken_order
                select tral;

            return await builder.ToListAsync();
        }
    }
}
