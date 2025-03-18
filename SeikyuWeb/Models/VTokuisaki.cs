#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 得意先情報を表すクラス
    /// </summary>
    public partial class VTokuisaki
    {
        /// <summary>
        /// コード
        /// </summary>
        public string コード { get; set; }
        /// <summary>
        /// 社名
        /// </summary>
        public string 社名 { get; set; }
        /// <summary>
        /// 検索カナ
        /// </summary>
        public string 検索カナ { get; set; }
        /// <summary>
        /// 略称
        /// </summary>
        public string 略称 { get; set; }
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string 郵便番号 { get; set; }
        /// <summary>
        /// 住所1
        /// </summary>
        public string 住所１ { get; set; }
        /// <summary>
        /// 住所2
        /// </summary>
        public string 住所２ { get; set; }
        /// <summary>
        /// 電話番号
        /// </summary>
        public string 電話番号 { get; set; }
        /// <summary>
        /// Fax番号
        /// </summary>
        public string Fax番号 { get; set; }
        /// <summary>
        /// 締日1
        /// </summary>
        public byte 締日１ { get; set; }
        /// <summary>
        /// 締日2
        /// </summary>
        public byte 締日２ { get; set; }
        /// <summary>
        /// 締日3
        /// </summary>
        public byte 締日３ { get; set; }
        /// <summary>
        /// 回収区分
        /// </summary>
        public byte 回収区分 { get; set; }
        /// <summary>
        /// 回収サイト
        /// </summary>
        public byte 回収サイト { get; set; }
        /// <summary>
        /// 回収日1
        /// </summary>
        public byte 回収日１ { get; set; }
        /// <summary>
        /// 回収日2
        /// </summary>
        public byte 回収日２ { get; set; }
        /// <summary>
        /// 回収日3
        /// </summary>
        public byte 回収日３ { get; set; }
        /// <summary>
        /// 消費税計算区分
        /// </summary>
        public short 消費税計算区分 { get; set; }
        /// <summary>
        /// 運賃計算区分
        /// </summary>
        public byte 運賃計算区分 { get; set; }
        /// <summary>
        /// 補助検索キー
        /// </summary>
        public string 補助検索キー { get; set; }
        /// <summary>
        /// 検索キー1
        /// </summary>
        public byte 検索キー１ { get; set; }
        /// <summary>
        /// 検索キー2
        /// </summary>
        public byte 検索キー２ { get; set; }
        /// <summary>
        /// 検索キー3
        /// </summary>
        public byte 検索キー３ { get; set; }
        /// <summary>
        /// 検索キー4
        /// </summary>
        public byte 検索キー４ { get; set; }
        /// <summary>
        /// 検索キー5
        /// </summary>
        public byte 検索キー５ { get; set; }
        /// <summary>
        /// 掛率
        /// </summary>
        public decimal 掛率 { get; set; }
        /// <summary>
        /// 丸め区分
        /// </summary>
        public byte 丸め区分 { get; set; }
        /// <summary>
        /// 親子区分
        /// </summary>
        public byte 親子区分 { get; set; }
        /// <summary>
        /// 営業所コード
        /// </summary>
        public int 営業所コード { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public byte 削除フラグ { get; set; }
        /// <summary>
        /// 更新番号
        /// </summary>
        public short 更新番号 { get; set; }
    }
}
