using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RenkeiDB.Common
{
    /// <summary>
    /// CSV出力ヘルパー
    /// </summary>
    public class CsvExportHelper
    {
        /// <summary>
        /// バイト形式への変換
        /// </summary>
        /// <typeparam name="T">レコードの型</typeparam>
        /// <param name="records">レコードのコレクション</param>
        /// <param name="headers">CSVヘッダー</param>
        /// <param name="fieldOrder">フィールドの順序</param>
        /// <returns>CSVデータのバイト配列</returns>
        public static byte[] ExportToCsv<T>(IEnumerable<T> records, string headers, List<string> fieldOrder)
        {
            StringBuilder csvData = new();
            Type type = typeof(T);

            IEnumerable<PropertyInfo> properties = fieldOrder.Select(fieldName => type.GetProperty(fieldName)).ToList();

            csvData.AppendLine(headers);

            foreach (var record in records)
            {
                IEnumerable<string> values = properties.Select(p => string.Format("\"{0}\"", p?.GetValue(record, null)));
                csvData.AppendLine(string.Join(",", values));
            }

            return Encoding.GetEncoding("shift_jis").GetBytes(csvData.ToString());
        }
    }
}
