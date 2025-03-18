using System.ComponentModel.DataAnnotations;
using static SeikyuWeb.Common.SystemConstants;
using static SeikyuWeb.Common.SystemEnums;

namespace SeikyuWeb.Dto.ReportDto
{
    /// <summary>
    /// 検索エクスポートのDTO
    /// </summary>
    public class SearchExportDto
    {
        /// <summary>レポートタイプ</summary>
        [Required(ErrorMessage = Message.RequiredField)]
        [EnumDataType(typeof(ReportType), ErrorMessage = Message.AllowValue)]
        [Display(Name = "reportType")]
        public int? reportType { get; set; }
        /// <summary>検索区分ID</summary>
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "searchKubunID")]
        public int? searchKubunID { get; set; }
        /// <summary>請求ID</summary>
        [Display(Name = "seikyuID")]
        public int? seikyuID { get; set; }
    }
}
