using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("M_PostCode")]
    public partial class M_PostCode
    {
        [StringLength(5)]
        public string PUBLIC_SECTOR_CODE { get; set; }
        [StringLength(5)]
        public string POSTAL_CODE_OLD { get; set; }
        [Required]
        [StringLength(7)]
        public string POSTAL_CODE { get; set; }
        [StringLength(100)]
        public string KEN_KANA { get; set; }
        [StringLength(100)]
        public string SHI_KU_CHO_KANA { get; set; }
        [StringLength(100)]
        public string CHO_IKI_KANA { get; set; }
        [StringLength(200)]
        public string KEN { get; set; }
        [StringLength(255)]
        public string SHI_KU_CHO { get; set; }
        [StringLength(200)]
        public string CHO_IKI { get; set; }
        public int? CHO_IKI_MULT_FLG { get; set; }
        public int? KOAZA_FLG { get; set; }
        public int? CHOME_FLG { get; set; }
        public int? MULTI_FLG { get; set; }
        public int? UPDATE_FLG { get; set; }
        public int? CHANGE_KUBUN { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
