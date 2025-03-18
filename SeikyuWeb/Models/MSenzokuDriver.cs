using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 専属ドライバー情報を表すクラス
    /// </summary>
    public partial class MSenzokuDriver
    {
        /// <summary>
        /// 専属ドライバーID
        /// </summary>
        public int SenzokuDriverId { get; set; }
        
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int DriverSyaryoId { get; set; }
        
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        
        /// <summary>
        /// 傭車ドライバーID
        /// </summary>
        public int YosyaDriverId { get; set; }
        
        /// <summary>
        /// 傭車ドライバー車両ID
        /// </summary>
        public int YosyaDriverSyaryoId { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime FromDate { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
