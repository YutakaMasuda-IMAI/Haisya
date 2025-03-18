using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 経費モデル
    /// </summary>
    public class ExpenseModel
    {
        public const int ALL_YEAR = 0;
        public const int EXPENSE_CATEGORY_VEHICLE = 2;  // 2:車輌/Vehicle
        public const int EXPENSE_CATEGORY_EMPLOYEE = 1; // 1:乗務員/Employee
        public const int EXPENSE_CATEGORY_ACCIDENT = 3; // 3:事故/Accident

        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public SearchModelForExpenseList Search { get; set; }
            public List<V_ExpenseDataList_Local> ExpenseDataLists { get; set; }
            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 経費区分/Expense_category (*)
        /// 車番/Vehicle_number
        /// 乗務員CD/Driver_code
        /// 乗務員名/Driver_name
        /// 事故年度/Accident_year (*)
        /// </summary>
        public class SearchModelForExpenseList
        {
            public int CompanyID { set; get; }
            public IEnumerable<SelectListItem> YearSelectList { set; get; }

            [Display(Name = "経費区分")]
            public int Expense_category { get; set; } = EXPENSE_CATEGORY_VEHICLE;
            [Display(Name = "車番")]
            [RegularExpression(@"^(|\d{4})$")]
            public string Vehicle_number { get; set; }
            [Display(Name = "乗務員CD")]
            public string Driver_code { get; set; }
            [Display(Name = "乗務員名")]
            public string Driver_name { get; set; }
            [Display(Name = "事故年度")]
            public int Accident_year { get; set; } = ALL_YEAR;
        }
    }
}
