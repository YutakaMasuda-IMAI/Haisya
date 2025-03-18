namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払確認完了DTOクラス
    /// </summary>
    public class CheckShitabaraiDoneDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 確認日時
        /// </summary>
        public string checkDatetime { get; set; }

        /// <summary>
        /// 確認ユーザー
        /// </summary>
        public string checkUser { get; set; }

        /// <summary>
        /// 確認結果
        /// </summary>
        public int? checkReault { get; set; }

        /// <summary>
        /// 変更フラグ
        /// </summary>
        public bool? changeFlg { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
