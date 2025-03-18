using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Uriage")]
    [Index(nameof(Anken_ID), nameof(Reg_Kubun), Name = "IX_T_Uriage")]
    public partial class T_Uriage
    {
        [Key]
        public int Uriage_ID { get; set; }
        public int Company_ID { get; set; }
        public int Reg_Status { get; set; }
        public int Reg_Status_Shitabarai { get; set; }
        public int Anken_ID { get; set; }
        public int AnkenDisplay_ID { get; set; }
        public int Reg_Kubun { get; set; }
        [Column(TypeName = "date")]
        public DateTime Haisya_Date { get; set; }
        public int Nippou_ID { get; set; }
        public int SeikyuDate_Kubun { get; set; }
        public int Seikyu_Kubun { get; set; }
        public int SenzokuID { get; set; }
        public int Tatekae_Over_Kubun { get; set; }
        [StringLength(255)]
        public string Tatekae_Over_Reason { get; set; }
        public int Shiharai_Over_Kubun { get; set; }
        [StringLength(255)]
        public string Shiharai_Over_Reason { get; set; }
        public int Tatekae_Over_Approval { get; set; }
        public int Shiharai_Over_Approval { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        public int Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        public int Update_User { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Direct_Uriage_Date { get; set; }
        public int Direct_Customer_Branch_ID { get; set; }
        public int Direct_Customer_Tantou_ID { get; set; }
        public int Direct_Uriage_Kubun { get; set; }
        public int Direct_Uriage_Bumon { get; set; }
        public int Direct_Seikyu_Group_ID { get; set; }
        public bool Del_Flg { get; set; }
    }
}
