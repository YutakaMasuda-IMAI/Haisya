using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// 共有番号リポジトリ
    /// </summary>
    public class ShareNoRepository : RepositoryBaseAsync<T_Share_No, ApplicationDbContext>, IShareNoRepository
    {
        public ShareNoRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 最新の共有番号を取得する
        /// </summary>
        /// <param name="shareDate">共有日</param>
        /// <param name="zeroUme">ゼロ埋め</param>
        /// <returns>最新の共有番号</returns>
        public async Task<string> GetLatestShareNo(DateTime shareDate, int zeroUme)
        {
            string result = await ExecuteScalarStoredProcedureAsync<string>(
                SystemConstants.StoreProceduresName.SP_T_Share_No,
                new SqlParameter("@SHARE_DATE", shareDate),
                new SqlParameter("@ZERO_UME", zeroUme)
            );
            return result;
        }
    }
}
