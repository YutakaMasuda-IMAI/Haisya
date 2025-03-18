using SeikyuWeb.Dto.ValidateRules;
using SeikyuWeb.Common;
using System.ComponentModel.DataAnnotations;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求チェックリクエストのDTOクラス
    /// </summary>
    public class CheckSeikyuReqDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 区分
        /// </summary>
        [NotEmpty]
        [EnumDataType(typeof(SystemEnums.CheckSeikyeKubun), ErrorMessage = Message.AllowValue)]
        [Display(Name = "区分")]
        public int kubun { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    public class CheckSeikyuCsvReqDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [NotEmpty]
        [Required(ErrorMessage =Message.RequiredField)]
        [EnumDataType(typeof(SystemEnums.CheckSeikyeKubun), ErrorMessage = Message.AllowValue)]
        [Display(Name = "区分")]
        public int kubun { get; set; }

        [NotEmpty]
        [Required(ErrorMessage = Message.RequiredField)]

        public int checkId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
