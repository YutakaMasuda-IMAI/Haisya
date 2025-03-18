using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故の詳細を表します。
    /// </summary>
    public partial class TJikoDetail
    {
        /// <summary>
        /// 事故ID
        /// </summary>
        public int JikoId { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }

        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int InsertUser { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }

        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }

        /// <summary>
        /// 事故の天候区分
        /// </summary>
        public int JikoWeatherKubun { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 住所コード
        /// </summary>
        public string AddressCode { get; set; }

        /// <summary>
        /// 住所レベル
        /// </summary>
        public string AddressLevel { get; set; }

        /// <summary>
        /// 経度
        /// </summary>
        public string Lng { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// 住所3
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// 住所4
        /// </summary>
        public string Address4 { get; set; }

        /// <summary>
        /// 住所備考
        /// </summary>
        public string AddressRemarks { get; set; }
    }
}
