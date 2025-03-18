namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求チェック完了のDTOクラス
    /// </summary>
    public class CheckSeikyuDoneDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// チェック日時
        /// </summary>
        public string checkDatetime { get; set; }
        /// <summary>
        /// チェックユーザー
        /// </summary>
        public string checkUser { get; set; }
        /// <summary>
        /// 変更フラグ
        /// </summary>
        public bool? changeFlg { get; set; }
        /// <summary>
        /// チェック結果
        /// </summary>
        public int? checkReault { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
