using SeikyuWeb.Common;
using System.ComponentModel.DataAnnotations;

namespace SeikyuWeb.Dto.InfoDto
{
    /// <summary>
    /// ポータル情報の表示フラグ値の取得と更新
    /// </summary>
    public class PortalInfoUpdateDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 表示フラグ
        /// </summary>
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        public bool? displayFlg { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
