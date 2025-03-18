using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 得意先請求担当を表すクラス
    /// </summary>
    [Table("M_Tokuisaki_SeikyuTantou")]
    public partial class M_Tokuisaki_SeikyuTantou
    {
        /// <summary>
        /// コード
        /// </summary>
        [Key]
        public int コード { get; set; }

        /// <summary>
        /// 担当1
        /// </summary>
        public int? Tantou1 { get; set; }

        /// <summary>
        /// 担当2
        /// </summary>
        public int? Tantou2 { get; set; }

        /// <summary>
        /// 担当3
        /// </summary>
        public int? Tantou3 { get; set; }

        /// <summary>
        /// 担当4
        /// </summary>
        public int? Tantou4 { get; set; }

        /// <summary>
        /// 担当5
        /// </summary>
        public int? Tantou5 { get; set; }
    }
}
