using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories.Interfaces
{
    /// <summary>
    /// レポート共通リポジトリインターフェース
    /// </summary>
    public interface IReportCommonRepository : IRepositoryBaseAsync<MReportSerchKubun, HaisyaContext>
    {
        /// <summary>
        /// レポート検索区分を取得します。
        /// </summary>
        /// <param name="reportKubunId">レポート区分ID</param>
        /// <returns>レポート検索区分</returns>
        Task<MReportSerchKubun> GetReportSearchKubunAsync(int reportKubunId);

        /// <summary>
        /// レポート出力項目リストを取得します。
        /// </summary>
        /// <param name="reportKubunId">レポート区分ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="isPdfReport">PDFレポートかどうか</param>
        /// <returns>レポート出力項目リスト</returns>
        Task<IEnumerable<MReportOutputItem>> GetReportOutputItemListAsync(int reportKubunId, int userId, int companyId, bool isPdfReport = true);

        /// <summary>
        /// レポート共通情報を取得します。
        /// </summary>
        /// <param name="procedureName">プロシージャ名</param>
        /// <param name="className">クラス名</param>
        /// <param name="paramDetail">パラメータ詳細</param>
        /// <param name="sortFields">ソートフィールド</param>
        /// <param name="sortType">ソートタイプ</param>
        /// <returns>レポート共通情報</returns>
        Task<IEnumerable<object>> GetReportCommonAsync(string procedureName, string className, Dictionary<string, object> paramDetail, List<string> sortFields, string sortType = "ASC");

        /// <summary>
        /// レポート検索を取得します。
        /// </summary>
        /// <param name="reportSearchId">レポート検索ID</param>
        /// <returns>レポート検索</returns>
        Task<MReportSerch> GetReportSearchAsync(int reportSearchId);

        /// <summary>
        /// プロシージャからデータを取得します。
        /// </summary>
        /// <param name="modelName">モデル名</param>
        /// <param name="procName">プロシージャ名</param>
        /// <param name="paramDetails">パラメータ詳細</param>
        /// <returns>データ</returns>
        Task<IEnumerable<object>> GetDataFromProcedure(string modelName, string procName, Dictionary<string, object> paramDetails);
    }
}
