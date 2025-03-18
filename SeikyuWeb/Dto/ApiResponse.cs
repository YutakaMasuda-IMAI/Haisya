namespace SeikyuWeb.Dto
{
    /// <summary>
    /// APIレスポンスクラス
    /// </summary>
    public class ApiResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ステータスコード
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string message { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
