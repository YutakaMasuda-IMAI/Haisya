using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 印刷ダウンロード情報を表します。
    /// </summary>
    public partial class TPrintDownload
    {
        /// <summary>
        /// 印刷履歴ID
        /// </summary>
        public int PrintRirekiId { get; set; }
        /// <summary>
        /// 印刷ID
        /// </summary>
        public int PrintId { get; set; }
        /// <summary>
        /// ダウンロード日時
        /// </summary>
        public DateTime DownloadDatetime { get; set; }
        /// <summary>
        /// ダウンロードユーザー
        /// </summary>
        public int DownloadUser { get; set; }
        /// <summary>
        /// ダウンロードIPアドレス
        /// </summary>
        public int DownloadIpAddress { get; set; }
        /// <summary>
        /// ダウンロードウェブブラウザ
        /// </summary>
        public string DownloadWebBrowser { get; set; }
        /// <summary>
        /// メールキックオフフラグ
        /// </summary>
        public bool MailKickOffFlg { get; set; }
    }
}
