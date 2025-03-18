using System;
using System.Collections.Generic;
using WebApplication.Data;
using WebApplication.Data.ReportCommons;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// M_Report_Serch_Item_Localクラスは項目検索帳票のdtoである
    /// </summary>
    public partial class M_Report_Serch_Item_Local : M_Report_Serch_Item
    {
        /// <summary>
        /// 項目区分を取得する
        /// </summary>
        public int Item_Kubun
        {
            get
            {  //検索ボタンタイプ
                if (this.SelectBtn == 1)
                {
                    return 1;
                }

                //テキストボックス文字列のタイプ
                if (this.Inputbox_Enabled == 1 && this.Inputbox_Type == "string")
                {
                    return 2;
                }

                //テキストボックス数字のタイプ
                if (this.Inputbox_Enabled == 1 && (this.Inputbox_Type == "int" || this.Inputbox_Type == "float"))
                {
                    return 3;
                }

                // 日付のタイプ
                if (this.Calender_Flg == 1)
                {
                    return 4;
                }

                return 0;
            }
            set { }
        }
    }

    /// <summary>
    /// M_Report_Detail_Param_Localクラスは帳票詳細パラメタのdto
    /// </summary>
    public partial class M_Report_Detail_Param_Local : M_Report_Detail_Param
    {
    }

    /// <summary>
    /// ReportCommonDtoLocalクラスは共通帳票のdto
    /// </summary>
    public partial class ReportCommonDtoLocal
    {
        public M_Report_Serch_Kubun_Local ReportSearchKubun { set; get; }
        public List<M_Report_Output_Item_Local> ReportOutputItemList { set; get; } = new();
        public List<V_ReportCommon_Local> ReportCommonList { set; get; } = new();
        public DateTime? ShimeDay { set; get; }
    }

    /// <summary>
    /// V_ReportCommon_Localクラス は、共通帳票の dto ディクショナリ
    /// </summary>
    public class V_ReportCommon_Local : Dictionary<string, object>
    {
        /// <summary>
        /// 帳票出力項目マスターによってにダイナミック値を取得
        /// </summary>
        /// <param name="key">キー・モデルのプロパティ</param>
        /// <returns>帳票共通項目の値</returns>
        public object getValueDynamic(M_Report_Output_Item_Master itemMaster, bool isPDF = true, bool isFormat = true)
        {
            if (!this.ContainsKey(itemMaster.Model_Prooerty))
            {
                return null;
            }

            object value = this[itemMaster.Model_Prooerty];

            if (value == null )
            {
                return "";
            }

            if (!isFormat)
            {
                return value;
            }

            if (itemMaster.Display_Type == "date")
            {
                if (value.ToString() == "0001/01/01 0:00:00")
                {
                    return "";
                }
                string formatDate = !string.IsNullOrEmpty(itemMaster.Display_Format) ? itemMaster.Display_Format : "yyyy/MM/dd";
                return DateTime.Parse(value.ToString()).ToString(formatDate);
            }

            if (!isPDF && (itemMaster.Display_Type == "int" || itemMaster.Display_Type == "float"))
            {
                decimal data = decimal.Parse(value.ToString());

                string[] formatSplit = itemMaster.Display_Format.Split('.');
                int decimalLength = 0;
                if (formatSplit.Length > 1 && formatSplit[1].Length > 0)
                {
                    decimalLength = formatSplit[1].Length <= 11 ? formatSplit[1].Length : 11;
                }
                else
                {
                    if ((itemMaster.Display_Format == "" || itemMaster.Display_Format == null) && data % 1 != 0)
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

    /// <summary>
    ///M_Report_Output_Item_Localクラス は帳票出力項目の dto
    /// </summary>
    public partial class M_Report_Output_Item_Local : M_Report_Output_Item
    {
    }

    /// <summary>
    /// V_ReportBillList_Local
    /// </summary>
    public partial class V_ReportBillList_Local : V_ReportBillList
    {
    }
    /// <summary>
    /// V_ReportBillList3_Local
    /// </summary>
    public partial class V_ReportBillList3_Local : V_ReportBillList3
    {
    }
    /// <summary>
    /// V_ReportBillList_Local
    /// </summary>
    public partial class V_ReportBillList2_Local : V_ReportBillList2
    {
    }
    /// <summary>
    /// V_ReportBillList3_Local
    /// </summary>
    public partial class V_ReportBillList4_Local : V_ReportBillList4
    {
    }
    /// <summary>
    ///車番連絡シートリスト
    /// </summary>
    public partial class V_ReportCarNumberContactSheetList_Local : V_ReportCarNumberContactSheetList
    {
    }

    /// <summary>
    /// V_ReportOrderTagList_Local
    /// </summary>
    public partial class V_ReportOrderTagList_Local : V_ReportOrderTagList
    {
    }

    /// <summary>
    /// transport order sheet リスト
    /// </summary>
    public partial class V_ReportTransportOrderSheetList_Local : V_ReportTransportOrderSheetList
    {
    }

    /// <summary>
    /// transport instructions sheetリスト
    /// </summary>
    public partial class V_ReportTransportInstructionsSheetList_Local : V_ReportTransportInstructionsSheetList
    {
    }

    /// <summary>
    /// Invoice procedure param
    /// </summary>
    public class InvoiceProcedureParram
    {
        public int seikyuId { set; get; } = 0;
        public int checkSeikyuId { set; get; } = 0;
        public DateTime seikyuMonth { set; get; } = DateTime.MinValue;
        public int shimeDay { set; get; } = 0;
        public int customerBranchID { set; get; } = 0;
        public int zeiKubun { set; get; } = 0;
        public void getDataFromJson (string jsonData)
        {
            Dictionary<string, string> data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);
                                
            this.seikyuId = int.Parse(data.GetValueOrDefault("seikyuId", "0"));
            this.checkSeikyuId = int.Parse(data.GetValueOrDefault("checkSeikyuId", "0"));
            this.seikyuMonth = DateTime.Parse(data.GetValueOrDefault("seikyuMonth", "1900-01-01"));
            this.shimeDay = int.Parse(data.GetValueOrDefault("shimeDay", "0"));
            this.customerBranchID = int.Parse(data.GetValueOrDefault("customerBranchId", "0"));
            this.zeiKubun = int.Parse(data.GetValueOrDefault("zeiKubun", "0"));
        }
    }
}
