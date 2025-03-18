using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    [Table("T_Anken_Display")]
    [Index(nameof(Anken_ID), nameof(Anken_Key), nameof(Daisuu_Sort), Name = "IX_T_Anken_Display", IsUnique = true)]
    public partial class T_Anken_Display
    {
        [Key]
        public int AnkenDisplay_ID { get; set; }
        public int Company_ID { get; set; }
        public int Anken_ID { get; set; }
        public int Daisuu_Sort { get; set; }
        public int Anken_Key { get; set; }
        [Column(TypeName = "date")]
        public DateTime Day { get; set; }
        public int Display_Kubun { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDatetime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDatetime { get; set; }
        public int Start_Point_Kubun { get; set; }
        [StringLength(255)]
        public string Start_BuildingName { get; set; }
        [StringLength(100)]
        public string Start_BuildingZid { get; set; }
        [StringLength(100)]
        public string Start_BuildingZid_Attr { get; set; }
        [StringLength(50)]
        public string Start_BuildingNameRead { get; set; }
        [StringLength(50)]
        public string Start_Point_KoumokuTitle { get; set; }
        [StringLength(10)]
        public string Start_Point_Type { get; set; }
        [StringLength(50)]
        public string Start_PointName { get; set; }
        [StringLength(100)]
        public string Start_Lng { get; set; }
        [StringLength(100)]
        public string Start_Lat { get; set; }
        [StringLength(10)]
        public string Start_Post_code { get; set; }
        [StringLength(255)]
        public string Start_Address { get; set; }
        [StringLength(255)]
        public string Start_Address1 { get; set; }
        [StringLength(255)]
        public string Start_Address2 { get; set; }
        [StringLength(255)]
        public string Start_Address3 { get; set; }
        [StringLength(255)]
        public string Start_Address4 { get; set; }
        public int End_Point_Kubun { get; set; }
        [StringLength(255)]
        public string End_BuildingName { get; set; }
        [StringLength(100)]
        public string End_BuildingZid { get; set; }
        [StringLength(100)]
        public string End_BuildingZid_Attr { get; set; }
        [StringLength(50)]
        public string End_BuildingNameRead { get; set; }
        [StringLength(50)]
        public string End_Point_KoumokuTitle { get; set; }
        [StringLength(10)]
        public string End_Point_Type { get; set; }
        [StringLength(50)]
        public string End_PointName { get; set; }
        [StringLength(100)]
        public string End_Lng { get; set; }
        [StringLength(100)]
        public string End_Lat { get; set; }
        [StringLength(10)]
        public string End_Post_code { get; set; }
        [StringLength(255)]
        public string End_Address { get; set; }
        [StringLength(255)]
        public string End_Address1 { get; set; }
        [StringLength(255)]
        public string End_Address2 { get; set; }
        [StringLength(255)]
        public string End_Address3 { get; set; }
        [StringLength(255)]
        public string End_Address4 { get; set; }
        [StringLength(50)]
        public string Display1 { get; set; }
        [StringLength(50)]
        public string Display2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Insert_Datetime { get; set; }
        public int? Insert_User { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Update_Datetime { get; set; }
        public int? Update_User { get; set; }
    }
}
