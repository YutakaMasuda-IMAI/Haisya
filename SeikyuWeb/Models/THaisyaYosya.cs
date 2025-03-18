using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車傭車情報を表すクラス
    /// </summary>
    public partial class THaisyaYosya
    {
        /// <summary>
        /// 配車ID
        /// </summary>
        public int HaisyaId { get; set; }
        /// <summary>
        /// 傭車ソート
        /// </summary>
        public int YosyaSort { get; set; }
        /// <summary>
        /// 傭車数
        /// </summary>
        public int YosyaCount { get; set; }
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 傭車担当者ID
        /// </summary>
        public int YosyaTantouId { get; set; }
        /// <summary>
        /// 傭車ドライバーID
        /// </summary>
        public int YosyaDriverId { get; set; }
        /// <summary>
        /// 傭車ドライバー車両ID
        /// </summary>
        public int YosyaDriverSyaryoId { get; set; }
        /// <summary>
        /// 運送フラグ
        /// </summary>
        public int UnsoFlg { get; set; }
        /// <summary>
        /// 傭車支払い区分
        /// </summary>
        public int YosyaShiharaiKubun { get; set; }
        /// <summary>
        /// 傭車支払い金額
        /// </summary>
        public decimal YosyaShiharaiMoney { get; set; }
        /// <summary>
        /// 傭車No2会社名
        /// </summary>
        public string YosyaNo2CompanyName { get; set; }
        /// <summary>
        /// 傭車No2ドライバー名
        /// </summary>
        public string YosyaNo2DriverName { get; set; }
        /// <summary>
        /// 傭車No2車両番号
        /// </summary>
        public string YosyaNo2CarNumber { get; set; }
        /// <summary>
        /// 傭車No2電話番号
        /// </summary>
        public string YosyaNo2Phone { get; set; }
        /// <summary>
        /// 傭車No2車種
        /// </summary>
        public string YosyaNo2Syasyu { get; set; }
    }
}
