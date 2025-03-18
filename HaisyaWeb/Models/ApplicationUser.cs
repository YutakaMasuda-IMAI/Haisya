using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaisyaWeb.Models.DB
{
    /// <summary>
    /// アプリケーションユーザーを表します。
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// ログインユーザーID
        /// </summary>
        [Key]
        [Column("LOGIN_USER_ID")]
        public int LoginUserId { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column("USER_ID")]
        public int UserId { get; set; }

        /// <summary>
        /// ログインID
        /// </summary>
        [Column("LOGIN_ID")]
        [StringLength(50)]
        public override string Id { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        [Column("USER_NAME")]
        [StringLength(100)]
        public override string UserName { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [StringLength(6)]
        public string Password { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [Column("DEL_FLG")]
        public bool DelFlg { get; set; }

        /// <summary>
        /// ロール
        /// </summary>
        [Column("ROLE")]
        public int Role { get; set; }

        /// <summary>
        /// 会社コード
        /// </summary>
        [Column("COMPANY_CODE")]
        public int CompanyCode { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        [Column("COMPANY_ID")]
        public int CompanyID { get; set; }

        /// <summary>
        /// 支店ID
        /// </summary>
        [Column("Branch_ID")]
        public int BranchID { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName { get; set; }

        [NotMapped] public override string NormalizedUserName { get; set; }
        [NotMapped] public override string Email { get; set; }
        [NotMapped] public override string NormalizedEmail { get; set; }
        [NotMapped] public override bool EmailConfirmed { get; set; }
        [NotMapped] public override string PasswordHash { get; set; }
        [NotMapped] public override string SecurityStamp { get; set; }
        [NotMapped] public override string ConcurrencyStamp { get; set; }
        [NotMapped] public override string PhoneNumber { get; set; }
        [NotMapped] public override bool PhoneNumberConfirmed { get; set; }
        [NotMapped] public override bool TwoFactorEnabled { get; set; }
        [NotMapped] public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped] public override bool LockoutEnabled { get; set; }
        [NotMapped] public override int AccessFailedCount { get; set; }
    }
}
