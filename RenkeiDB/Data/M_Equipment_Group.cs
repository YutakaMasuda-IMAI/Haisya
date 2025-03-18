using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 装備品グループ情報を表すエンティティ
    /// </summary>
    [Table("M_Equipment_Group")]
    public partial class M_Equipment_Group
    {
        /// <summary>
        /// 装備品グループID
        /// </summary>
        [Key]
        public int Equipment_Group_ID { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 装備品グループ名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Equipment_GroupName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(50)]
        public string Remarks { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool Del_Flg { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }

        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? Insert_User { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }

        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? Update_User { get; set; }
    }
}
