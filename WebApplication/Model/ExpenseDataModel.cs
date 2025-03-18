using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 経費データモデルクラス
    /// </summary>
    public class ExpenseDataModel : BaseModel
    {
        public const int ALL_YEAR = 0;
        public const int EXPENSE_CATEGORY_VEHICLE = 2;  // 2:車輌/Vehicle
        public const int EXPENSE_CATEGORY_EMPLOYEE = 1; // 1:乗務員/Employee
        public const int EXPENSE_CATEGORY_ACCIDENT = 3; // 3:事故/Accident

        public ExpenseDataModel(ApplicationDbContext context) => _context = context;

        /// <summary>
        /// 経費データリストを取得します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="expense_category">経費カテゴリ</param>
        /// <param name="vehicle_number">車両番号</param>
        /// <param name="driver_code">ドライバーコード</param>
        /// <param name="driver_name">ドライバー名</param>
        /// <param name="accident_year">事故年</param>
        /// <returns>経費データリスト</returns>
        public async Task<IEnumerable<V_ExpenseDataList>> GetExpenseDataList(
            int company_id,
            int expense_category,
            int? vehicle_number,
            string driver_code,
            string driver_name,
            int? accident_year)
        {
            if (expense_category == EXPENSE_CATEGORY_ACCIDENT && accident_year == null)
            {
                throw new ArgumentException($"Required {nameof(accident_year)}", nameof(accident_year));
            }

            StringBuilder sql = new StringBuilder()
                .AppendFormat("EXECUTE [dbo].[Proc_V_ExpenseDataList] @COMPANY_ID = {0}, @KEIHI_KUBUN = {1}", company_id, expense_category);
            if (vehicle_number != null)
                sql.AppendFormat(", @SYABAN = {0}", vehicle_number);
            else
               sql.Append(", @SYABAN = NULL");

            if (!string.IsNullOrEmpty(driver_code))
                sql.AppendFormat(", @DRIVER_CODE = {0}", driver_code);
            else
               sql.Append(", @DRIVER_CODE = NULL");

            if (!string.IsNullOrEmpty(driver_name))
                sql.AppendFormat(", @DRIVER_NAME = {0}", driver_name);
            else
               sql.Append(", @DRIVER_NAME = NULL");

            if (accident_year != null && accident_year.Value != ALL_YEAR)
                sql.AppendFormat(", @NENDO = {0}", accident_year);
            else
                sql.AppendFormat(", @NENDO = 0");

            return await _context.V_ExpenseDataLists.FromSqlRaw(sql.ToString()).AsNoTracking().ToListAsync();
        }
    }
}
