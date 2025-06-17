using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.ReportLayoutDto
{
    /// <summary>
    /// TReportLayoutのためのDTOクラス
    /// </summary>
    public class ReportLayoutDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>印刷区分</summary>
        public int printKubun { get; set; }
        /// <summary>税区分</summary>
        public int zeiKubun { get; set; }
        /// <summary>レポートソート</summary>
        public int reportSort { get; set; }
        /// <summary>レポート名</summary>
        public string reportName { get; set; }
        /// <summary>レポート説明</summary>
        public string reportExplan { get; set; }
        /// <summary>レポート備考</summary>
        public string reportRemarks { get; set; }
        /// <summary>CSV出力フラグ</summary>
        public bool csvOutputFlg { get; set; }
        /// <summary>レポート検索ID</summary>
        public int reportSerchId { get; set; }
        /// <summary>レポート検索区分ID</summary>
        public int reportSerchKubunId { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからDTOを生成
        /// </summary>
        /// <param name="entity">TReportLayoutエンティティ</param>
        /// <returns>生成されたReportLayoutDto</returns>
        public static ReportLayoutDto FromEntity(TReportLayout entity) => new()
        {
            printKubun = entity.PrintKubun,
            zeiKubun = entity.ZeiKubun,
            reportSort = entity.ReportSort,
            reportName = entity.ReportName,
            reportExplan = entity.ReportExplan,
            reportRemarks = entity.ReportRemarks,
            csvOutputFlg = entity.CsvOutputFlg == 1,
            reportSerchId = entity.ReportSerchId,
            reportSerchKubunId = entity.ReportSerchKubunId,
        };
    }
}
