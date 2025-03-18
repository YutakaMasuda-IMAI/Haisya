using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 日報データを管理するリポジトリクラスです。
    /// </summary>
    public class DailyReportRepository: IDailyReportRepository 
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        public DailyReportRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// T_Nippouテーブルから全ての日報レコードを非同期で取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトのリストを表すTask。
        /// リストには、データベース内の全ての日報レコードが含まれます。
        /// </returns>
        public async Task<List<T_Nippou>> GetTNippous()
        {
            List<T_Nippou> existingRecords = await _context.T_Nippous.ToListAsync();

            return existingRecords;


        }


        /// <summary>
        /// 指定された案件配車用IDに基づいて日報データを取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">日報データを取得する案件配車用ID。</param>
        /// <returns>日報データを返します。</returns> 
        public async Task<T_Nippou> GetNippou(int AnkenDisplay_ID)
        {

            T_Nippou nippou = await _context.T_Nippous
                .Where(n => n.AnkenDisplay_ID == AnkenDisplay_ID)
                .FirstOrDefaultAsync();

            return nippou;

        }
    }

    /// <summary>
    /// 日報データを管理するリポジトリインターフェースです。
    /// </summary>
    public interface IDailyReportRepository
    {
        /// <summary>
        /// T_Nippouテーブルから全ての日報レコードを非同期で取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトのリストを表すTask。
        /// リストには、データベース内の全ての日報レコードが含まれます。
        /// </returns>
        public Task<List<T_Nippou>> GetTNippous();

        /// <summary>
        /// 指定された案件配車用IDに基づいて日報データを取得します。
        /// </summary>
        /// <param name="AnkenDisplay_ID">日報データを取得する案件配車用ID。</param>
        /// <returns>日報データを返します。</returns> 
        public Task<T_Nippou> GetNippou(int AnkenDisplay_ID);
    }
}