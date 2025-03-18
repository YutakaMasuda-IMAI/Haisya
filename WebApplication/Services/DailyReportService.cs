using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class DailyReportService: IDailyReportService
    {
        private readonly IDailyReportRepository _dailyReportRepository;

        public DailyReportService(IDailyReportRepository dailyReportRepository)
        {
            _dailyReportRepository = dailyReportRepository;
        }

        /// <summary>
        /// T_Nippouテーブルから日報のリストを取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトのリストを表すTask。
        /// </returns>
        /// <remarks>
        /// このメソッドは、リポジトリからT_Nippouテーブルに格納されている全ての日報データを取得して返します。
        /// </remarks> 
        public Task<List<T_Nippou>> GetTNippous() 
        {
            return _dailyReportRepository.GetTNippous();
        }

        /// <summary>
        /// 指定AnkenDisplay_IDからT_Nippouテーブルから日報のリストを取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトを表すTask。
        /// </returns>
        /// <remarks>
        /// このメソッドは、リポジトリからT_Nippouテーブルに格納されているAnkenDisplay_IDのデータを取得して返します。
        /// </remarks> 
        public Task<T_Nippou> GetNippou(int AnkenDisplay_ID)
        {
            return _dailyReportRepository.GetNippou(AnkenDisplay_ID);
        }
    }

    public interface IDailyReportService
    {
        /// <summary>
        /// T_Nippouテーブルから日報のリストを取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトのリストを表すTask。
        /// </returns>
        Task<List<T_Nippou>> GetTNippous();

        /// <summary>
        /// 指定AnkenDisplay_IDからT_Nippouテーブルから日報のリストを取得します。
        /// </summary>
        /// <returns>
        /// T_Nippouオブジェクトを表すTask。
        /// </returns>
        Task<T_Nippou> GetNippou(int AnkenDisplay_ID);
    }
    
}