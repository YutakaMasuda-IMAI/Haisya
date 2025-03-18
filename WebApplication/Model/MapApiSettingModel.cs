namespace WebApplication.Model
{
    /// <summary>
    /// マップAPI設定モデル
    /// </summary>
    public class MapApiSettingModel
    {
    }

    /// <summary>
    /// マップAPI設定
    /// </summary>
    public class MapApiSettings
    {
        public const string MapApiSetting = "MapApiSetting";

        /// <summary>
        /// Web URI設定
        /// </summary>
        public WebUriSetting WebUri { set; get; }

        /// <summary>
        /// 認証設定
        /// </summary>
        public AuthAidSetting AuthAid { set; get; }

        /// <summary>
        /// 環境区分
        /// </summary>
        public string EnvKubun { set; get; }
    }

    /// <summary>
    /// Web URI設定
    /// </summary>
    public class WebUriSetting
    {
        /// <summary>
        /// JavaScript API
        /// </summary>
        public string JavaScriptAPI { set; get; }

        /// <summary>
        /// Web API
        /// </summary>
        public string WebAPI { set; get; }
    }

    /// <summary>
    /// 認証設定
    /// </summary>
    public class AuthAidSetting
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string Uid { set; get; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Pwd { set; get; }

        /// <summary>
        /// サービスID
        /// </summary>
        public string Sid { set; get; }

        /// <summary>
        /// デバイスフラグ
        /// </summary>
        public string Device_flag { set; get; }

        /// <summary>
        /// 認証コード
        /// </summary>
        public string AuthCode { set; get; }
    }
}
