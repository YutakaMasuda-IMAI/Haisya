using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// 支払い変更DTO
    /// </summary>
    public class CheckShiharaisChangeDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払いチェックID
        /// </summary>
        public int checkShitabaraiId { get; set; }

        /// <summary>
        /// 売上支払いID
        /// </summary>
        public int uriageShiharaiId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double? qty { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        public int? unit { get; set; }

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
#pragma warning restore IDE1006 // Naming Styles

        public static CheckShiharaisChangeDto FromEntity(TCheckShitabaraiChange entity)
            => new()
            {
                checkShitabaraiId = entity.CheckShitabaraiId,
                uriageShiharaiId = entity.UriageShiharaiId,
                qty = entity.Qty,
                unit = entity.Unit,
                calcPrice = entity.CalcPrice,
                shiharaiUnchin = entity.ShiharaiUnchin,
                tatekaekin = entity.Tatekaekin,
                warimashi1 = entity.Warimashi1,
                warimashi2 = entity.Warimashi2,
                warimashi3 = entity.Warimashi3,
                warimashi4 = entity.Warimashi4,
                warimashi5 = entity.Warimashi5,
                shiharaiTotal = entity.ShiharaiTotal,
            };
    }
}
