using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SeikyuWeb.Dto.ReportDto.ReportDto;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// レポート共通サービスインターフェース
    /// </summary>
    public interface IReportCommonService
    {
        /// <summary>
        /// レポート共通情報を取得します。
        /// </summary>
        /// <param name="reportKubunId">レポート区分ID</param>
        /// <param name="jsonData">JSONデータ</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="reportType">レポートタイプ</param>
        /// <returns>レポート共通情報</returns>
        Task<ReportCommonDto> GetReportCommonAsync(int reportKubunId, Dictionary<string, object> jsonData, int userId, int companyId, int reportType, bool isPdf);

        /// <summary>
        /// PDFレポートモデルを取得します。
        /// </summary>
        /// <param name="dataReport">データレポート</param>
        /// <param name="compamyId">会社ID</param>
        /// <param name="seikyuId">請求ID</param>
        /// <returns>PDFレポートモデル</returns>
        Task<PDFReportModel> GetModelPDFReport(ReportCommonDto dataReport, int compamyId, int seikyuId);
        /// <summary>
        /// Get Report Layout
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="searchKububId"></param>
        /// <returns></returns>
        Task<TReportLayout> GetReportLayout(int printKubun, int searchKububId);
    }
}
