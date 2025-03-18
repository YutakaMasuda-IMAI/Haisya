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
    /// 郵便番号リポジトリ
    /// </summary>
    public class PostCodeReponsitory : RepositoryBaseAsync<M_PostCode, ApplicationDbContext>, IPostCodeRepository
    {
        public PostCodeReponsitory(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 都道府県名リストの取得
        /// </summary>
        /// <returns>都道府県名の配列</returns>
        public async Task<IEnumerable<string>> GetListKens()
        {
            return await FindAll().GroupBy(p => p.KEN).OrderBy(p => p.Min(x => x.ID)).Select(p => p.Key).ToListAsync();
        }

        /// <summary>
        /// 住所選択　API（郵便番号取得）
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <param name="shikucho">市区町村名</param>
        /// <param name="choiki">町域名</param>
        /// <returns>郵便番号を取得</returns>
        public async Task<string> GetPostCodeAsync(string ken, string shikucho, string choiki)
        {
            string model = await FindByCondition(x => x.KEN == ken &&
                                           x.SHI_KU_CHO == shikucho &&
                                           x.CHO_IKI == choiki)
                        .Select(x => x.POSTAL_CODE)
                        .FirstOrDefaultAsync();
            return model ?? string.Empty;
        }

        /// <summary>
        /// 住所リストの取得
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <returns>住所リスト</returns>
        public async Task<IEnumerable<string>> GetAddressByKenAsync(string ken)
        {
            return await FindByCondition(p => p.KEN.Equals(ken)).Select(p => p.SHI_KU_CHO).Distinct().ToListAsync();
        }

        /// <summary>
        /// 住所リストの取得
        /// </summary>
        /// <param name="ken">都道府県名</param>
        /// <param name="shikucho">市区町村名</param>
        /// <returns>住所リスト</returns>
        public async Task<IEnumerable<string>> GetAddressByConditionsAsync(string ken, string shikucho)
        {
            return await FindByCondition(p => p.KEN.Equals(ken) && p.SHI_KU_CHO.Equals(shikucho)).Select(p => p.CHO_IKI).Distinct().ToListAsync();
        }
    }
}
