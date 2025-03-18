using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// レポートレイアウトリポジトリクラス
    /// </summary>
    public class ReportLayoutRepository : RepositoryBaseAsync<TReportLayout, HaisyaContext>, IReportLayoutRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト</param>
        /// <param name="unitOfWork">ユニットオブワーク</param>
        public ReportLayoutRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// プリント区分からレポートレイアウトの取得
        /// </summary>
        /// <param name="printKubun">プリント区分</param>
        /// <returns>レポートレイアウトのリスト</returns>
        public async Task<IEnumerable<TReportLayout>> GetReportLayoutListAsync(int printKubun)
            => await FindByCondition(r => r.PrintKubun == printKubun).ToListAsync();
    }
}
