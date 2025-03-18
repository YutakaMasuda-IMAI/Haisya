using System.Collections.Generic;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 住所履歴
    /// </summary>
    public class AddressHistoryDto
    {
        /// <summary>
        /// 住所履歴の使用率リスト
        /// </summary>
        public List<Item> UsageRate { get; set; } = new List<Item>();
        /// <summary>
        /// 住所履歴の最新リスト
        /// </summary>
        public List<Item> MostRecents { get; set; } = new List<Item>();
    }

    /// <summary>
    /// 住所履歴の情報
    /// </summary>
    public class Item
    {
        /// <summary>
        /// 住所履歴の名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 住所履歴の住所
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 住所履歴の郵便番号
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 住所履歴のLng
        /// </summary>
        public string Lng { get; set; }
        /// <summary>
        /// 住所履歴のLat
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 都道府県
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// 市区町村
        /// </summary>
        public string Address3 { get; set; }
    }
}
