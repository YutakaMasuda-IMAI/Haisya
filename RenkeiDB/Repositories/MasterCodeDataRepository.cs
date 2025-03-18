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
    /// マスターコードデータリポジトリ
    /// </summary>
    public class MasterCodeDataRepository : RepositoryBaseAsync<M_Code_Datum, ApplicationDbContext>, IMasterCodeDataRepository
    {
        public MasterCodeDataRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// マスターコードデータを取得する
        /// </summary>
        /// <param name="codeId">コードID</param>
        /// <returns>マスターコードデータのリスト</returns>
        public async Task<IEnumerable<M_Code_Datum>> GetMasterCodeData(int codeId)
        {
            return await FindByCondition(x => x.Code_ID.Equals(codeId) && x.Del_Flg == false)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
        }
    }
}
