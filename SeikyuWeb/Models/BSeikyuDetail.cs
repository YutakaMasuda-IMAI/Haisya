using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求詳細クラス
    /// </summary>
    public partial class BSeikyuDetail
    {
        public int BakSeikyuId { get; set; }
        public int DataKubun { get; set; }
        public int DataSort { get; set; }
        public int UriageUnchinId { get; set; }
        public int AnkenId { get; set; }
        public int AnkenIdDetail { get; set; }
        public DateTime? DisplayDate { get; set; }
        public string Syaban { get; set; }
        public string SyasyuKataName { get; set; }
        public string DriverName { get; set; }
        public string Tsumi { get; set; }
        public string Oroshi { get; set; }
        public string Luggage { get; set; }
        public string WorkName { get; set; }
        public double Qty { get; set; }
        public int Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CalcPrice { get; set; }
        public decimal SeikyuUnchin { get; set; }
        public decimal Tatekaekin { get; set; }
        public decimal Warimashi1 { get; set; }
        public decimal Warimashi2 { get; set; }
        public decimal Warimashi3 { get; set; }
        public decimal Warimashi4 { get; set; }
        public decimal Warimashi5 { get; set; }
        public decimal SeikyuTotal { get; set; }
        public int? ZeiKubun { get; set; }
        public int YosyaDriverId { get; set; }
        public int YosyaBranchId { get; set; }
        public string YosyaName { get; set; }
        public string YosyaDriverName { get; set; }
        public int? RemarksId { get; set; }
        public string Remaks { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
