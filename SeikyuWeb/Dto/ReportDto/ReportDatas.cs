using SeikyuWeb.Models;
using System;
using System.Collections.Generic;

namespace SeikyuWeb.Dto.ReportDto
{
    /// <summary>
    /// クラス ReportCommonDtoは共通帳票のDTO
    /// </summary>
    public class ReportCommonDto
    {
        /// <summary>レポート検索</summary>
        public MReportSerch ReportSearch { set; get; }
        /// <summary>レポート検索区分</summary>
        public MReportSerchKubun ReportSearchKubun { set; get; }
        /// <summary>レポート出力項目リスト</summary>
        public IEnumerable<MReportOutputItem> ReportOutputItemList { set; get; } = new List<MReportOutputItem>();
        /// <summary>共通帳票リスト</summary>
        public List<V_ReportCommon_Local> ReportCommonList { set; get; } = new List<V_ReportCommon_Local>();
        /// <summary>締め日</summary>
        public DateTime? ShimeDay { set; get; }
    }

    /// <summary>
    /// V_ReportCommon_Localクラスは、共通帳票のDTOディクショナリ
    /// </summary>
    public class V_ReportCommon_Local : Dictionary<string, object>
    {
        /// <summary>
        /// 帳票出力項目マスターによってダイナミック値を取得
        /// </summary>
        /// <param name="key">キー・モデルのプロパティ</param>
        /// <returns>帳票共通項目の値</returns>
        public object getValueDynamic(MReportOutputItemMaster itemMaster, bool isPDF = true, bool isFormat = true)
        {
            if (!this.ContainsKey(itemMaster.ModelProoerty))
            {
                return null;
            }

            object value = this[itemMaster.ModelProoerty];

            if (value == null)
            {
                return "";
            }

            if (!isFormat)
            {
                return value;
            }

            if (itemMaster.DisplayType == "date")
            {
                if (value.ToString() == "0001/01/01 0:00:00")
                {
                    return "";
                }
                string formatDate = !string.IsNullOrEmpty(itemMaster.DisplayFormat) ? itemMaster.DisplayFormat : "yyyy/MM/dd";
                return DateTime.Parse(value.ToString()).ToString(formatDate);
            }

            if (itemMaster.DisplayType == "time")
            {
                if (value.ToString() == "0001/01/01 0:00:00")
                {
                    return "";
                }
                string formatDate = !string.IsNullOrEmpty(itemMaster.DisplayFormat) ? itemMaster.DisplayFormat : "HH:mm";
                return DateTime.Parse(value.ToString()).ToString(formatDate);
            }

            if (!isPDF && (itemMaster.DisplayType == "int" || itemMaster.DisplayType == "float"))
            {
                decimal data = decimal.Parse(value.ToString());

                string[] formatSplit = itemMaster.DisplayFormat.Split('.');
                int decimalLength = 0;
                if (formatSplit.Length > 1 && formatSplit[1].Length > 0)
                {
                    decimalLength = formatSplit[1].Length <= 11 ? formatSplit[1].Length : 11;
                }
                else
                {
                    decimal x = data % 1;
                    if ((itemMaster.DisplayFormat == "" || itemMaster.DisplayFormat == null) && data % 1 != 0)
                    {
                        decimalLength = 2;
                    }
                }
                string format = (decimalLength > 0 ? "0." + new string('0', decimalLength) : "");
                if (formatSplit[0] == "#,###")
                {
                    format = "#,###" + format;
                }

                return data.ToString(format);
            }

            return isPDF ? value.ToString().Replace(" ", "&nbsp;").Replace("　", "&emsp;") : value.ToString();
        }
    }
}
