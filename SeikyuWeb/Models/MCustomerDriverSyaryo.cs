using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客ドライバー車両を表すクラス
    /// </summary>
    public partial class MCustomerDriverSyaryo
    {
        /// <summary>
        /// 顧客ドライバー車両ID
        /// </summary>
        public int CustomerDriverSyaryoId { get; set; }
        
        /// <summary>
        /// 顧客ドライバーID
        /// </summary>
        public int CustomerDriverId { get; set; }
        
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        
        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        
        /// <summary>
        /// 車番地域
        /// </summary>
        public string SyabanChiiki { get; set; }
        
        /// <summary>
        /// 車番分類
        /// </summary>
        public string SyabanBunrui { get; set; }
        
        /// <summary>
        /// 車番カナ
        /// </summary>
        public string SyabanKana { get; set; }
        
        /// <summary>
        /// 車番番号
        /// </summary>
        public string SyabanNumber { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        
        /// <summary>
        /// 挿入ユーザー
        /// </summary>
        public int InsertUser { get; set; }
        
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }
        
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
