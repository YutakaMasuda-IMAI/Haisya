using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 事故項目を表します。
    /// </summary>
    public partial class MJikoItem
    {
        /// <summary>
        /// 事故項目ID
        /// </summary>
        public int JikoItemsId { get; set; }

        /// <summary>
        /// 事故項目名
        /// </summary>
        public string JikoItemsName { get; set; }

        /// <summary>
        /// 事故項目プロパティ名
        /// </summary>
        public string JikoItemsPropName { get; set; }

        /// <summary>
        /// 事故項目タイプ
        /// </summary>
        public string JikoItemsType { get; set; }
    }
}
