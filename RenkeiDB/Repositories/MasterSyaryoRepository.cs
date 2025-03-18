using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// マスター車両リポジトリ
    /// </summary>
    public class MasterSyaryoRepository : RepositoryBaseAsync<M_Syaryo, ApplicationDbContext>, IMasterSyaryoRepository
    {
        public MasterSyaryoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {

        }

        /// <summary>
        /// 車両情報の取得
        /// </summary>
        /// <param name="syaryoId">車両ID</param>
        /// <returns>車両情報</returns>
        public async Task<M_Syaryo> GetMasterSyaryoData(int syaryoId)
        {
            return await FindByCondition(x => x.Syaryo_ID.Equals(syaryoId) && x.DEL_FLG == false).FirstOrDefaultAsync();
        }
    }
}
