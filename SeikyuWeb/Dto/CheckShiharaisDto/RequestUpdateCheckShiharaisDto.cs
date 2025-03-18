using SeikyuWeb.Common;
using SeikyuWeb.Dto.ValidateRules;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
#pragma warning disable IDE1006 // Naming Styles
    /// <summary>
    /// 支払いチェック更新リクエストDTO
    /// </summary>
    public class RequestUpdateCheckShiharaisDto
    {
        /// <summary>
        /// ステータスチェック
        /// </summary>
        [AllowedValues("1", "2")]
        [Required(ErrorMessage = SystemConstants.Message.RequiredField)]
        [Display(Name = "ステータスチェック")]
        public int? checkStatus { get; set; }

        /// <summary>
        /// 支払い変更情報
        /// </summary>
        public ShitabaraichangeDto[] shitabaraiChanges { get; set; }
    }

    /// <summary>
    /// 支払い変更DTO
    /// </summary>
    public class ShitabaraichangeDto
    {
        /// <summary>
        /// 支払い変更ID
        /// </summary>
        [DisplayName("shitabaraiChanges")]
        [Required(ErrorMessage = SystemConstants.Message.AllowValue)]
        public int? checkShitabaraiId { get; set; }

        /// <summary>
        /// 売上支払いID
        /// </summary>
        [DisplayName("shitabaraiChanges")]
        [Required(ErrorMessage = SystemConstants.Message.AllowValue)]
        public int? uriageShiharaiId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double? qty { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public decimal? unitPrice { get; set; }

        /// <summary>
        /// 計算価格
        /// </summary>
        public decimal? calcPrice { get; set; }

        /// <summary>
        /// 支払い運賃
        /// </summary>
        public decimal? shiharaiUnchin { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        public decimal? tatekaekin { get; set; }

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
        /// 支払い合計
        /// </summary>
        public decimal? shiharaiTotal { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int? delFlg { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
