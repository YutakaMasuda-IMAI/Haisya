using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求書クエリ用のDTOクラス
    /// </summary>
    public class InvoiceQueryDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 請求書印刷情報
        /// </summary>
        public TPrintSeikyu printSeikyu { get; set; }
        /// <summary>
        /// 請求書印刷詳細情報
        /// </summary>
        public TPrintSeikyuDetail printSeikyuDetail { get; set; }
        /// <summary>
        /// 請求情報
        /// </summary>
        public TSeikyu seikyu { get; set; }
        /// <summary>
        /// 印刷履歴
        /// </summary>
        public TPrintRireki printRireki { get; set; }
        /// <summary>
        /// コードデータ
        /// </summary>
        public MCodeDatum codeDatum { get; set; }
        /// <summary>
        /// 顧客担当者情報
        /// </summary>
        public MCustomerTantou customerTantou { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
