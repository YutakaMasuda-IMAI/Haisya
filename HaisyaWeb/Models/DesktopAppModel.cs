namespace HaisyaWeb.Models
{
    /// <summary>
    /// デスクトップアプリケーションモデル
    /// </summary>
    public class DesktopAppModel
    {
        /// <summary>
        /// マップAPI設定
        /// </summary>
        public MapApiSettings MapApiSettings { set; get; }
        /// <summary>マップAPIのURL（JS用）</summary>
        public string MapsApiForJSUrl { get; set; }
        /// <summary>
        /// WebViewフラグ
        /// </summary>
        public int WebViewFlg { set; get; }
    }
}
