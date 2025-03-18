using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 人件費情報を表すクラス
    /// </summary>
    public partial class MPersonnelExpense
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        
        /// <summary>
        /// 基本給
        /// </summary>
        public decimal? Base { get; set; }
        
        /// <summary>
        /// 日給
        /// </summary>
        public decimal? Day { get; set; }
        
        /// <summary>
        /// 深夜手当
        /// </summary>
        public decimal? Midnight { get; set; }
        
        /// <summary>
        /// 休日手当
        /// </summary>
        public decimal? Holiday { get; set; }
        
        /// <summary>
        /// 休日深夜手当
        /// </summary>
        public decimal? HolidayMidnight { get; set; }
        
        /// <summary>
        /// 福利厚生費
        /// </summary>
        public double? BenefitsCosts { get; set; }
        
        /// <summary>
        /// 間接費用
        /// </summary>
        public double? IndirectCosts { get; set; }
    }
}
