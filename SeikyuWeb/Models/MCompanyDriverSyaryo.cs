using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ドライバー車両を表すクラス
    /// </summary>
    public partial class MCompanyDriverSyaryo
    {
        /// <summary>
        /// ドライバー車両ID
        /// </summary>
        public int DriverSyaryoId { get; set; }
        
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        
        /// <summary>
        /// 車両管理ID
        /// </summary>
        public int SyaryoManagementId { get; set; }
        
        /// <summary>
        /// 車両管理ID1
        /// </summary>
        public int? SyaryoManagementId1 { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        
        /// <summary>
        /// 車種区分ID
        /// </summary>
        public int SyasyuKubunId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
