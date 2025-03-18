using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 専属情報を表すクラス
    /// </summary>
    public partial class MSenzoku
    {
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 専属名
        /// </summary>
        public string SenzokuName { get; set; }
        
        /// <summary>
        /// 専属名略称
        /// </summary>
        public string SenzokuNameAbbr { get; set; }
        
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int KokyakuId { get; set; }
        
        /// <summary>
        /// 顧客担当ID
        /// </summary>
        public int KokyakuTantouId { get; set; }
        
        /// <summary>
        /// 請求区分
        /// </summary>
        public int SeikyuKubun { get; set; }
        
        /// <summary>
        /// 計算区分
        /// </summary>
        public int CalcKubun { get; set; }
        
        /// <summary>
        /// 月額料金
        /// </summary>
        public decimal MonthlyFee { get; set; }
        
        /// <summary>
        /// 日額料金
        /// </summary>
        public decimal DailyFee { get; set; }
        
        /// <summary>
        /// 配車グループID
        /// </summary>
        public int HaisyaGroupId { get; set; }
        
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        
        /// <summary>
        /// 車種サイズ
        /// </summary>
        public string SyasyuSize { get; set; }
        
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
    }
}
