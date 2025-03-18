using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// ポータル情報を表すエンティティ
    /// </summary>
    [Table("T_Portal_Info")]
    public partial class T_Portal_Info
    {
        /// <summary>
        /// ポータル情報ID
        /// </summary>
        [Key]
        public int Portal_Info_ID { get; set; }
        
        /// <summary>
        /// ポータル区分
        /// </summary>
        public int Portal_Kubun { get; set; }
        
        /// <summary>
        /// 重要度区分
        /// </summary>
        public int Critical_Kubun { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int User_ID { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int Company_ID { get; set; }
        
        /// <summary>
        /// カテゴリー
        /// </summary>
        [StringLength(50)]
        public string Category { get; set; }
        
        /// <summary>
        /// タイトル
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        
        /// <summary>
        /// 詳細
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Detail { get; set; }
        
        /// <summary>
        /// 有効期限
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Limit_Date { get; set; }
        
        /// <summary>
        /// 表示フラグ
        /// </summary>
        public int Display_Flg { get; set; }
        
        /// <summary>
        /// 登録日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime Insert_Datetime { get; set; }
        
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int Insert_User { get; set; }
        
        /// <summary>
        /// 更新日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime Update_Datetime { get; set; }
        
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int Update_User { get; set; }
        
        /// <summary>
        /// コントローラー
        /// </summary>
        [StringLength(50)]
        public string Controller { get; set; }
        
        /// <summary>
        /// アクション
        /// </summary>
        [StringLength(50)]
        public string Action { get; set; }
        
        /// <summary>
        /// パラメータ整数1
        /// </summary>
        public double? Param_Int_1 { get; set; }
        
        /// <summary>
        /// パラメータ整数1の名前
        /// </summary>
        [StringLength(20)]
        public string Param_Int_1_Name { get; set; }
        
        /// <summary>
        /// パラメータ整数2
        /// </summary>
        public double? Param_Int_2 { get; set; }
        
        /// <summary>
        /// パラメータ整数2の名前
        /// </summary>
        [StringLength(20)]
        public string Param_Int_2_Name { get; set; }
        
        /// <summary>
        /// パラメータ整数3
        /// </summary>
        public double? Param_Int_3 { get; set; }
        
        /// <summary>
        /// パラメータ整数3の名前
        /// </summary>
        [StringLength(20)]
        public string Param_Int_3_Name { get; set; }
        
        /// <summary>
        /// パラメータ文字列1
        /// </summary>
        [StringLength(20)]
        public string Param_string_1 { get; set; }
        
        /// <summary>
        /// パラメータ文字列1の名前
        /// </summary>
        [StringLength(20)]
        public string Param_string_1_Name { get; set; }
        
        /// <summary>
        /// パラメータ文字列2
        /// </summary>
        [StringLength(20)]
        public string Param_string_2 { get; set; }
        
        /// <summary>
        /// パラメータ文字列2の名前
        /// </summary>
        [StringLength(20)]
        public string Param_string_2_Name { get; set; }
        
        /// <summary>
        /// パラメータ文字列3
        /// </summary>
        [StringLength(20)]
        public string Param_string_3 { get; set; }
        
        /// <summary>
        /// パラメータ文字列3の名前
        /// </summary>
        [StringLength(20)]
        public string Param_string_3_Name { get; set; }
        
        /// <summary>
        /// 連携案件ID
        /// </summary>
        public int Renkei_Anken_ID { get; set; }
    }
}
