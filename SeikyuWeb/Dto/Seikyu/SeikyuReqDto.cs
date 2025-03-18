using SeikyuWeb.Common;
using SeikyuWeb.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求リクエストDTOクラス
    /// </summary>
    public class SeikyuReqDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [CustomDateFormat("yyyy/MM", "年月自")]
        public string fromYm { get; set; }

        [CustomDateFormat("yyyy/MM", "年月至")]
        [FromDateGreaterThanToDate(nameof(fromYm))]
        public string toYm { get; set; }

        [EnumDataType(typeof(SystemEnums.ZeiKubun), ErrorMessage = Message.AllowValue)]
        [Display(Name = "税区分")]
        public int? zeiKubun { get; set; }

        [EnumDataType(typeof(SystemEnums.SeikyuStatus), ErrorMessage = Message.AllowValue)]
        [Display(Name = "ステータス")]
        public int? status { get; set; }

        public int? shimeDay { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
