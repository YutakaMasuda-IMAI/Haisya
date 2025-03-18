namespace SeikyuWeb.Dto.PortalDto
{
    /// <summary>
    /// ポータルのDTO
    /// </summary>
    public class PortalDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>印刷区分</summary>
        public string printKubun { get; set; }
        /// <summary>月</summary>
        public string month { get; set; }
        /// <summary>日付</summary>
        public string date { get; set; }
        /// <summary>合計金額</summary>
        public string totalAmount { get; set; }
        /// <summary>スタッフ名</summary>
        public string staffName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
