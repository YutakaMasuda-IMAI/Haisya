using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 車両リポジトリクラス
    /// </summary>
    public class SyaryoRepository : RepositoryBaseAsync<M_Syaryo, ApplicationDbContext>, ISyaryoRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public SyaryoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// IDで車両情報を取得する
        /// </summary>
        /// <param name="id">車両ID</param>
        /// <returns>車両情報</returns>
        public async Task<M_Syaryo> GetByIdAsync(int id)
        {
            return await FindByCondition(x => x.Syaryo_ID.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
