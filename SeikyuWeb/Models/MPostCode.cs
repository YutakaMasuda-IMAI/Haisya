using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 郵便番号情報を表すクラス
    /// </summary>
    public partial class MPostCode
    {
        /// <summary>
        /// 公共団体コード
        /// </summary>
        public string PublicSectorCode { get; set; }
        
        /// <summary>
        /// 旧郵便番号
        /// </summary>
        public string PostalCodeOld { get; set; }
        
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// 県名（カナ）
        /// </summary>
        public string KenKana { get; set; }
        
        /// <summary>
        /// 市区町村名（カナ）
        /// </summary>
        public string ShiKuChoKana { get; set; }
        
        /// <summary>
        /// 町域名（カナ）
        /// </summary>
        public string ChoIkiKana { get; set; }
        
        /// <summary>
        /// 県名
        /// </summary>
        public string Ken { get; set; }
        
        /// <summary>
        /// 市区町村名
        /// </summary>
        public string ShiKuCho { get; set; }
        
        /// <summary>
        /// 町域名
        /// </summary>
        public string ChoIki { get; set; }
        
        /// <summary>
        /// 町域複数フラグ
        /// </summary>
        public int? ChoIkiMultFlg { get; set; }
        
        /// <summary>
        /// 小字フラグ
        /// </summary>
        public int? KoazaFlg { get; set; }
        
        /// <summary>
        /// 丁目フラグ
        /// </summary>
        public int? ChomeFlg { get; set; }
        
        /// <summary>
        /// 複数フラグ
        /// </summary>
        public int? MultiFlg { get; set; }
        
        /// <summary>
        /// 更新フラグ
        /// </summary>
        public int? UpdateFlg { get; set; }
        
        /// <summary>
        /// 変更区分
        /// </summary>
        public int? ChangeKubun { get; set; }
        
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
    }
}
