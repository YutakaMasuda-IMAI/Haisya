using SeikyuWeb.Common;
using SeikyuWeb.Dto.ValidateRules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// APIから値を取得し、検証するためのDTOクラス
    /// </summary>
    public class RequestUpdateCheckSeikyusDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ステータスチェック
        /// </summary>
        [AllowedValues("1", "2")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "ステータスチェック")]
        public int? checkStatus { get; set; }

        /// <summary>
        /// 請求変更リスト
        /// </summary>
        public List<SeikyuChangeDto> seikyuChanges { get; set; } = new List<SeikyuChangeDto>();
#pragma warning restore IDE1006 // Naming Styles
    }
}
