using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Keyless]
    [Table("担当者一段階目")]
    public partial class 担当者一段階目
    {
        public double? コード { get; set; }
        [StringLength(255)]
        public string 略称 { get; set; }
        [StringLength(255)]
        public string 担当者 { get; set; }
        public double? 検索２ { get; set; }
        public double? 検索３ { get; set; }
    }
}
