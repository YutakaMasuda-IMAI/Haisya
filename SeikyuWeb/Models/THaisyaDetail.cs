using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車詳細情報を表すクラス
    /// </summary>
    public partial class THaisyaDetail
    {
        /// <summary>
        /// 配車詳細ID
        /// </summary>
        public int HaisyaDetailId { get; set; }
        /// <summary>
        /// 配車ID
        /// </summary>
        public int HaisyaId { get; set; }
    }
}
