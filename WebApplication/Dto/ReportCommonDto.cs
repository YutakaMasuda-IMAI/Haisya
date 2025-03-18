using System;
using System.Collections.Generic;
using WebApplication.Data;

namespace WebApplication.Dto
{
    /// <summary>
    /// クラス ReportCommonDtoは共通帳票のDTO
    /// </summary>
    public class ReportCommonDto
    {
        /// <summary>
        /// 帳票検索区分
        /// </summary>
        public M_Report_Serch_Kubun ReportSearchKubun { set; get; }

        /// <summary>
        /// 帳票出力項目リスト
        /// </summary>
        public IEnumerable<M_Report_Output_Item> ReportOutputItemList { set; get; }

        /// <summary>
        /// 共通帳票リスト
        /// </summary>
        public IEnumerable<object> ReportCommonList { set; get; }

        /// <summary>
        /// 締め日
        /// </summary>
        public DateTime? ShimeDay { set; get; }
    }
}
