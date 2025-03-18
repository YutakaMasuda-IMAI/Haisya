using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;

namespace WebApplication.Repositories
{
    /// <summary>
    /// クラスReportCommonRepositoryは、共通帳票のリポジトリ
    /// </summary>
    public class ReportCommonRepository : IReportCommonRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ReportCommonDataModel _reportCommonDataModel;

        public ReportCommonRepository(ApplicationDbContext context, ReportCommonDataModel reportCommonDataModel)
        {
            _context = context;
            _reportCommonDataModel = reportCommonDataModel;
        }

        /// <summary>
        /// 区分検索帳票の詳細を取得
        /// </summary>
        /// <param name="reportKubunId">区分検索帳票のId</param>
        /// <returns>詳細 M_Report_Serch_Kubun</returns>
        public async Task<M_Report_Serch_Kubun> GetReportSearchKubunAsync(int reportKubunId)
        {
            M_Report_Serch_Kubun result = await _context.M_Report_Serch_Kubuns
                .Where(c => c.Report_Serch_Kubun_ID == reportKubunId)
                .Include(c => c.Report_Serch)
                .Include(c => c.Report_Serch_Item_List)
                .Include(c => c.Report_Detail_Param_List)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                if (result.Report_Serch != null)
                {
                    result.Report_Serch.Report_Serch_Kubun_List = new HashSet<M_Report_Serch_Kubun>();
                }
                foreach (var item in result.Report_Serch_Item_List)
                {
                    item.Report_Serch_Kubun = null;
                }
                foreach (var item in result.Report_Detail_Param_List)
                {
                    item.Report_Serch_Kubun = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 区分帳票のidで出力項目帳票リストを取得
        /// </summary>
        /// <param name="reportKubunId">区分検索帳票のId</param>
        /// <param name="userId">ユーザーのId</param>
        /// <param name="companyId">会社のId</param>
        /// <param name="isPdfReport">PDF帳票かどうか</param>
        /// <returns>リスト M_Report_Output_Item</returns>
        public async Task<IEnumerable<M_Report_Output_Item>> GetReportOutputItemListAsync(int reportKubunId, int userId, int companyId, bool isPdfReport = true)
        {
            List<M_Report_Output_Item> result = await _context.M_Report_Output_Items
                .Where(c => c.Report_Serch_Kubun_ID == reportKubunId)
                .Where(c => c.User_ID == userId)
                .Where(c => c.Company_ID == companyId)
                .Include(c => c.Report_Output_Item_Master)
                .Where(c => c.Report_Output_Item_Master.Report_ItemFlg == (isPdfReport ? 1 : 0))
                .ToListAsync();
            if (result.Count == 0)
            {
                result = await _context.M_Report_Output_Items
                    .Where(c => c.Report_Serch_Kubun_ID == reportKubunId)
                    .Where(c => c.User_ID == 0)
                    .Where(c => c.Company_ID == companyId)
                    .Include(c => c.Report_Output_Item_Master)
                    .Where(c => c.Report_Output_Item_Master.Report_ItemFlg == (isPdfReport ? 1 : 0))
                    .ToListAsync();
            }
            foreach (var item in result)
            {
                if (item.Report_Output_Item_Master != null)
                {
                    item.Report_Output_Item_Master.Report_Output_Item_List = new HashSet<M_Report_Output_Item>();
                }
            }
            return result;
        }

        /// <summary>
        /// 共通帳票のプロシージャーデータを取得
        /// </summary>
        /// <param name="procedureName">プロシージャ名</param>
        /// <param name="className">プロシージャのクラス名</param>
        /// <param name="paramDetail">パラメーターの詳細</param>
        /// <param name = "sortFields" > ソートフィールド </ param >
        /// < param name="sortType">ソートタイプ</param>
        /// <returns>帳票のプロシージャデータリスト</returns>
        public async Task<IEnumerable<object>> GetReportCommonAsync(
            string procedureName,
            string className,
            Dictionary<string, object> paramDetail,
            List<string> sortFields,
            string sortType = "ASC"
        )
        {
            IEnumerable<object> resultData = await _reportCommonDataModel.GetDataFromProcedure(className, procedureName, paramDetail);
            object instance = _reportCommonDataModel.GetInstanceByModelName(className);
            IOrderedEnumerable<object> orderedResultData = resultData.OrderBy(x => 0);
            foreach (var field in sortFields)
            {
                PropertyInfo propertyInfo = instance.GetType().GetProperty(field);
                if (propertyInfo != null)
                {
                    if (sortType == "ASC")
                    {
                        orderedResultData = orderedResultData.ThenBy(x => propertyInfo.GetValue(x));
                    }
                    else
                    {
                        orderedResultData = orderedResultData.ThenByDescending(x => propertyInfo.GetValue(x));
                    }
                }
            }
            return orderedResultData;
        }
    }

    /// <summary>
    /// ReportCommonRepositoryクラスのインターフェースは、共通帳票のリポジトリである
    /// </summary>
    public interface IReportCommonRepository
    {
        Task<M_Report_Serch_Kubun> GetReportSearchKubunAsync(int reportKubunId);
        Task<IEnumerable<M_Report_Output_Item>> GetReportOutputItemListAsync(int reportKubunId, int userId, int companyId, bool isPdfReport = true);
        Task<IEnumerable<object>> GetReportCommonAsync(string procedureName, string className, Dictionary<string, object> paramDetail, List<string> sortFields, string sortType = "ASC");
    }
}