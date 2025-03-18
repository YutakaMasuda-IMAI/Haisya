using SeikyuWeb.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// 変更内容入力のためのDTOクラス
    /// </summary>
    public class SeikyuChangeDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 請求チェックID
        /// </summary>
        [DisplayName("seikyuChanges")]
        [Required(ErrorMessage = SystemConstants.Message.AllowValue)]
        public int? checkSeikyuId { get; set; }

        /// <summary>
        /// 売上運賃ID
        /// </summary>
        [DisplayName("seikyuChanges")]
        [Required(ErrorMessage = SystemConstants.Message.AllowValue)]
        public int? uriageUnchinId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double? qty { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal? unitPrice { get; set; }

        /// <summary>
        /// 請求運賃
        /// </summary>
        public decimal? seikyuUnchin { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public decimal? tatekaekin { get; set; }

        /// <summary>
        /// 請求合計
        /// </summary>
        public decimal? seikyuTotal { get; set; }

        /// <summary>
        /// 割増1
        /// </summary>
        public decimal? warimashi1 { get; set; }

        /// <summary>
        /// 割増2
        /// </summary>
        public decimal? warimashi2 { get; set; }

        /// <summary>
        /// 割増3
        /// </summary>
        public decimal? warimashi3 { get; set; }

        /// <summary>
        /// 割増4
        /// </summary>
        public decimal? warimashi4 { get; set; }

        /// <summary>
        /// 割増5
        /// </summary>
        public decimal? warimashi5 { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int? delFlg { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
