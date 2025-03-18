namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// ユーザー支払DTOクラス
    /// </summary>
    public class UserShitabaraiDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string displayName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
