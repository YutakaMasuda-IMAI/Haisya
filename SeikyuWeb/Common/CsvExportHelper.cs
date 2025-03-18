using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeikyuWeb.Common
{
    /// <summary>
    /// CSV出力ヘルパークラス
    /// </summary>
    public class CsvExportHelper
    {
        /// <summary>
        /// バイト形式への変換
        /// </summary>
        /// <param name="records">出力するデータ</param>
        /// <param name="headers">データのヘッダー</param>
        /// <param name="fieldOrder">帳票出力項目のデータマスタ一覧</param>
        /// <param name="fileName">CSVのファイル名</param>
        /// <returns>バイト配列形式のCSVデータ</returns>
        public static byte[] ExportToCsv(List<V_ReportCommon_Local> records, string headers, List<MReportOutputItemMaster> fieldOrder, string fileName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream, Encoding.GetEncoding("shift_jis")))
                {
                    writer.WriteLine(headers);

                    foreach (var item in records)
                    {
                        string dataRow = "";
                        foreach (var headerItem in fieldOrder)
                        {
                            bool isFormat = headerItem.DisplayFormat != null && headerItem.DisplayFormat != "";
                            string value = item.getValueDynamic(headerItem, false, isFormat)?.ToString();
                            value = "\"" + value + "\"";
                            dataRow += value + ",";
                        }
                        dataRow = dataRow.TrimEnd(',');
                        writer.WriteLine(dataRow);
                    }

                    writer.Flush();
                    memoryStream.Position = 0;
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
