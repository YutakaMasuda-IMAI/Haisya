using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemEnums;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 支払チェックリポジトリクラス
    /// </summary>
    public class CheckShitabaraiRepository : RepositoryBaseAsync<TCheckShitabarai, HaisyaContext>, ICheckShitabaraiRepository
    {
        public CheckShitabaraiRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 指定したIDの支払チェック詳細を取得します。
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>支払チェック詳細</returns>
        public async Task<TCheckShitabarai> GetDetailCheckShibaraiById(int id)
        {
            TCheckShitabarai data = await FindByCondition(c => c.CheckShitabaraiId == id)
                .Include("CheckShitabaraiDetails")
                .FirstOrDefaultAsync();

            return data;
        }

        /// <summary>
        /// 指定したIDの支払チェックを非同期で取得します。
        /// </summary>
        /// <param name="id">支払チェックID</param>
        /// <returns>支払チェック</returns>
        public async Task<TCheckShitabarai> GetByIdAsync(int id)
        {
            return await FindByCondition(x => x.CheckShitabaraiId.Equals(id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// ログイン情報取得（業者）からログインした場合：
        /// T_Check_Shitabarai．Company_ID＝M_CompanyUser．Company_ID（シート4で取得した値）
        /// T_Cehck_Shitabarai．Del_Datetime＝Null
        /// T_Check_Shitabarai．Check_Status≠4：承認済
        /// </summary>
        /// <param name="id">会社ID</param>
        /// <returns>支払チェックのリスト</returns>
        public async Task<IEnumerable<TCheckShitabarai>> GetByCompanyIdAsync(int id)
        {
            IQueryable<TCheckShitabarai> query = from check in DbContext.Set<TCheckShitabarai>()
                        join company in DbContext.Set<MCompanyUser>()
                            on check.CompanyId equals company.CompanyId
                        where check.CompanyId == id
                              && check.DelDatetime == null
                              && check.CheckStatus != 4
                        select check;
            return await query.ToListAsync();
        }

        /// <summary>
        /// ログイン情報取得（業者以外）からログインした場合：
        /// T_Check_Shitabarai．Yosya_Branch_ID＝
        /// 　　M_Customer_Tantou．Tantou_ID（シート3で取得した値）に該当するCustomer_Branch_ID
        /// T_Cehck_Shitabarai．Del_Datetime＝Null
        /// T_Check_Shitabarai．Check_Status≠4：承認済
        /// </summary>
        /// <param name="id">支店ID</param>
        /// <returns>支払チェックのリスト</returns>
        public async Task<IEnumerable<TCheckShitabarai>> GetByBranchIdAsync(int id)
        {
            IQueryable<TCheckShitabarai> query = from check in DbContext.Set<TCheckShitabarai>()
                        join customer in DbContext.Set<MCustomerTantou>()
                            on check.YosyaBranchId equals customer.TantouId
                        where check.YosyaBranchId == id
                              && check.DelDatetime == null
                              && check.CheckStatus != 4
                        select check;
            return await query.ToListAsync();
        }

        /// <summary>
        /// 支払問合せ一覧を取得します。
        /// </summary>
        /// <param name="customerBranchId">顧客支店ID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="status">ステータス配列</param>
        /// <returns>支払チェックのリスト</returns>
        public async Task<IEnumerable<TCheckShitabarai>> GetListCheckShiharaisAsync(int customerBranchId, int companyId, int[] status)
        {
            IQueryable<TCheckShitabarai> builder = DbContext.TCheckShitabarais
                .Include(x => x.CheckShitabaraiDone)
                .ThenInclude(x => x.CustomerTantou)
                .Include(x => x.CheckShitabaraiDetails)
                .ThenInclude(x => x.CheckShitabaraiChange)
                .Include(x => x.CustomerBranch)
                .ThenInclude(x => x.CustomerUriageCalc)
                .Include(x => x.CustomerBranch)
                .ThenInclude(x => x.ShiharaiTantou)
                .ThenInclude(x => x.CompanyUserGroupUsers)
                .ThenInclude(x => x.CompanyUser)
                .Include(x => x.CustomerBranch)
                .ThenInclude(x => x.SeikuTantou)
                .ThenInclude(x => x.CompanyUserGroupUsers)
                .ThenInclude(x => x.CompanyUser)
                .Where(s => s.CheckKubun == (int)CheckKubun.WEB && s.DelDatetime == null && status.Contains(s.CheckStatus) && (s.CheckShitabaraiDone == null || s.CheckShitabaraiDone.CustomerTantou.DelFlg != true));

            if (companyId != 0)
            {
                builder = builder.Where(s => s.CompanyId == companyId);
            }
            else if (customerBranchId != 0)
            {
                builder = builder.Where(s => s.YosyaBranchId == customerBranchId);
            }

            return await builder.OrderBy(s => s.ShiharaiMonth).ToListAsync();
        }

        /// <summary>
        /// Webチェックカテゴリに関連し、削除されておらず、"発行済"または"確認中"のチェック支払リストを取得します。
        /// </summary>
        /// <returns>チェック支払リスト</returns>
        public async Task<IEnumerable<TCheckShitabarai>> GetTCheckSeikyuAsync()
        {
            return await FindByCondition(
                x => x.CheckKubun.Equals(SystemConstants.CheckKubun.WEB)
                && x.DelDatetime == null &&
                (x.CheckStatus.Equals(SystemConstants.CheckStatus.発行済)
                || x.CheckStatus.Equals(SystemConstants.CheckStatus.確認中))).ToListAsync();
        }
    }
}
