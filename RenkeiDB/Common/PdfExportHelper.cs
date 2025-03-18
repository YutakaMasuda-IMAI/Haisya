using DinkToPdf;
using DinkToPdf.Contracts;

namespace RenkeiDB.Common
{
    /// <summary>
    /// Pdf 出力ヘルパー
    /// </summary>
    public static class PdfExportHelper
    {
        /// <summary>
        /// html-string を pdf-bytes に変換
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="html"></param>
        /// <param name="orientation"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static byte[] ExportToPdf(this IConverter converter, string html,
            Orientation orientation = Orientation.Landscape,
            MarginSettings margin = null)
        {
            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    DPI = 96,
                    PaperSize = PaperKind.A4,
                    Orientation = orientation,
                    ColorMode = ColorMode.Color,
                    Margins = margin ?? new MarginSettings() { Left = 20, Bottom = 20, Right = 15, Top = 20 },
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        FooterSettings = new FooterSettings
                        {
                            FontSize = 9,
                            Center = "頁 [page]",
                        },
                    }
                }
            };
            return converter.Convert(doc);
        }
    }
}
