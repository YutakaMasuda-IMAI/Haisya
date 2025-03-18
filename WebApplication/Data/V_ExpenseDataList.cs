using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 経費データリストを表します。
    /// </summary>
    [Keyless]
    public class V_ExpenseDataList
    {
        /// <summary>
        /// 経費ID
        /// </summary>
        public int Expense_ID { get; set; }

        /// <summary>
        /// 経費区分
        /// </summary>
        [Range(1, 3)]
        public int Expense_Kubun { get; set; }

        /// <summary>
        /// 経費区分名
        /// </summary>
        public string Expense_Kubun_name
            => Expense_Kubun == 1 ? "乗務員" : Expense_Kubun == 2 ? "車輌" : Expense_Kubun == 3 ? "事故" : "";

        // 車番
        // M_SyaryoManagement > Syaban_Number
        [RegularExpression(@"^(|\d{4})$")]
        public string Syaban_Number { get; set; }

        // 車種
        public string SyasyuDisplay { get; set; }

        #region ※Not displayed if the Expense Category is Vehicle
        // 乗務員CD
        // M_CompanyDriver > Driver_ID
        public int? Driver_ID { get; set; }

        // 乗務員CD
        // M_CompanyDriver > Employee_Number
        public int? DRIVER_CODE { get; set; }

        // 乗務員名
        // M_CompanyDriver > Display_Name
        public string DRIVER_NAME { get; set; }
        #endregion

        #region ※Displayed if Expense Category is Accident
        // 事故日
        // T_Jiko > Jiko_Date
        [Column(TypeName = "datetime")]
        public DateTime? Jiko_Date { get; set; }

        // 事故名
        // T_Jiko > Jiko_Display
        public string Jiko_Display { get; set; }

        /// <summary>
        /// 事故負担額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Jiko_Futan_Money { get; set; }
        #endregion
    }
}
