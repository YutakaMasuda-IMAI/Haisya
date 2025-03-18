using SeikyuWeb.Models;
using System.Linq;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求書詳細のDTOクラス
    /// </summary>
    public class InvoiceDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 表示日付
        /// </summary>
        public string displayDate { get; set; }
        /// <summary>
        /// 車番
        /// </summary>
        public string syaban { get; set; }
        /// <summary>
        /// 車種型名
        /// </summary>
        public string syasyuKataName { get; set; }
        /// <summary>
        /// 積み地
        /// </summary>
        public string tsumi { get; set; }
        /// <summary>
        /// 降ろし地
        /// </summary>
        public string oroshi { get; set; }
        /// <summary>
        /// 作業名
        /// </summary>
        public string workName { get; set; }
        /// <summary>
        /// 荷物
        /// </summary>
        public string luggage { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string qty { get; set; }
        /// <summary>
        /// 単位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 単価
        /// </summary>
        public decimal unitPrice { get; set; }
        /// <summary>
        /// 請求運賃
        /// </summary>
        public decimal seikyuUnchin { get; set; }
        /// <summary>
        /// 割増1
        /// </summary>
        public decimal warimashi1 { get; set; }
        /// <summary>
        /// 割増2
        /// </summary>
        public decimal warimashi2 { get; set; }
        /// <summary>
        /// 割増3
        /// </summary>
        public decimal warimashi3 { get; set; }
        /// <summary>
        /// 割増4
        /// </summary>
        public decimal warimashi4 { get; set; }
        /// <summary>
        /// 割増5
        /// </summary>
        public decimal warimashi5 { get; set; }
        /// <summary>
        /// 立替金
        /// </summary>
        public decimal tatekaekin { get; set; }
        /// <summary>
        /// 請求合計
        /// </summary>
        public decimal seikyuTotal { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string remarks { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからInvoiceDetailDtoを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>InvoiceDetailDto</returns>
        public static InvoiceDetailDto FromEntity(TPrintSeikyuDetail entity) => new()
        {
            displayDate = entity.DisplayDate?.ToString(DateFormat.DATE_JP),
            syaban = entity.Syaban,
            syasyuKataName = entity.SyasyuKataName,
            tsumi = entity.Tsumi,
            oroshi = entity.Oroshi,
            workName = entity.WorkName,
            luggage = entity.Luggage,
            qty = entity.Qty.ToString(),
            unit = entity.Unit.ToString(),
            unitPrice = entity.UnitPrice ?? 0,
            seikyuUnchin = entity.SeikyuUnchin ?? 0,
            warimashi1 = entity.Warimashi1 ?? 0,
            warimashi2 = entity.Warimashi2 ?? 0,
            warimashi3 = entity.Warimashi3 ?? 0,
            warimashi4 = entity.Warimashi4 ?? 0,
            warimashi5 = entity.Warimashi5 ?? 0,
            tatekaekin = entity.Tatekaekin ?? 0,
            seikyuTotal = entity.SeikyuTotal ?? 0,
            remarks = string.Join(" ", new[] { entity.Remarks1, entity.Remarks2 }.Where(s => !string.IsNullOrWhiteSpace(s))),
        };
    }
}
