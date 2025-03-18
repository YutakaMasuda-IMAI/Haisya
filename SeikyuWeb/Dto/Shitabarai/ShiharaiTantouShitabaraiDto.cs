using System.Collections.Generic;

namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払担当DTOクラス
    /// </summary>
    public class ShiharaiTantouShitabaraiDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// グループ名
        /// </summary>
        public string groupName { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// ユーザーリスト
        /// </summary>
        public List<UserShitabaraiDto> users { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
