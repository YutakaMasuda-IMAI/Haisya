using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 休日モデルクラス
    /// </summary>
    [AllowAnonymous]
    public class HolidayModel
    {
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ドライバー名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 車両番号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 車両タイプ
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 休日設定
        /// </summary>
        public string HolidaySetting { get; set; }

        /// <summary>
        /// 休日理由
        /// </summary>
        public string HolidayReason { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}