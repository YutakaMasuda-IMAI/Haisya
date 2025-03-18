using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerWeb.Models.DB
{

    public class ApplicationUser : IdentityUser
    {
        [Key]
        [Column("LOGIN_USER_ID")]
        public int LoginUserId { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("LOGIN_ID")]
        [StringLength(50)]
        public override string Id { get; set; }

        [Column("USER_NAME")]
        [StringLength(100)]
        public override string UserName { get; set; }

        [StringLength(6)]
        public string Password { get; set; }

        [Column("DEL_FLG")]
        public bool DelFlg { get; set; }

        [Column("ROLE")]
        public int Role { get; set; }

        [Column("COMPANY_CODE")]
        public int CompanyCode { get; set; }

        [Column("COMPANY_ID")]
        public int CompanyID { get; set; }

        [Column("Branch_ID")]
        public int BranchID { get; set; }


        //[NotMapped] public override string Id { get; set; }
        //[NotMapped] public override string UserName { get; set; }
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
