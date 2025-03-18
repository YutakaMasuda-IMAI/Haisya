using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    public partial class TSeikyu
    {
        public int SeikyuId { get; set; }
        public int CompanyId { get; set; }
        public int SeikyuKubun { get; set; }
        public int PrintKubun { get; set; }
        public int PrintPattern { get; set; }
        public int CustomerBranchId { get; set; }
        public int ZeiKubun { get; set; }
        public DateTime SeikyuMonth { get; set; }
        public int ShimeDay { get; set; }
        public DateTime? DelDatetime { get; set; }
        public DateTime PrintDatetime { get; set; }
        public DateTime PrintDate { get; set; }
        public DateTime? PrintToDate { get; set; }
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public int NendomatsuFlg { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? SeikyudateTo { get; set; }
        public DateTime InsertDatetime { get; set; }
        public int InsertUser { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public int UpdateUser { get; set; }
    }
}
