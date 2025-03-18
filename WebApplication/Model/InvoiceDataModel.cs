using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Services;

namespace WebApplication.Model
{
    /// <summary>
    /// 請求データモデルクラス
    /// </summary>
    public class InvoiceDataModel : BaseModel
    {
        public const int TAX_CATEGORY_TAX = 0;              // 0:課税
        public const int TAX_CATEGORY_FREE = 1;             // 1:非課税

        public const int INVOICE_STATUS_NOT_YET = 0;        // 0：未
        public const int INVOICE_STATUS_WEB_DONE = 1;       // 1：WEB済み
        public const int INVOICE_STATUS_PUBLISH_DONE = 2;   // 2：発行済み

        private readonly IInvoiceService _service;
        public InvoiceDataModel(ApplicationDbContext context, IInvoiceService service) => (_context, _service) = (context, service);

        /// <summary>
        /// 請求書の発行業務プロセス
        /// </summary>
        /// <param name="dto">請求書発行DTO</param>
        /// <returns></returns>
        public Task Publish(InvoicePublish dto) => _service.Publish(dto);

        /// <summary>
        /// 請求書チェックの発行業務プロセス
        /// </summary>
        /// <param name="dto">請求書チェック発行DTO</param>
        /// <returns></returns>
        public Task PublishCheck(InvoiceCheckPublish dto) => _service.PublishCheck(dto);

        /// <summary>
        /// 請求リストを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月</param>
        /// <param name="closing_days">締日</param>
        /// <param name="seikyu_tantou">請求担当</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日</param>
        /// <param name="sale_date">売上年月日（まで）</param>
        /// <returns>請求リスト</returns>
        public async Task<IEnumerable<V_InvoiceDataList>> GetInvoiceDataList(
            int company_id,
            string year_month,
            int closing_days,
            int seikyu_tantou,
            int customer_id1,
            int customer_id2,
            string publish_date,
            string sale_date)
        {
            string sql = string.Format("EXECUTE [dbo].[Proc_V_SeikyuDataList] @COMPANY_ID = {0}", company_id);
            if (year_month != null) { sql += string.Format(", @SEIKYU_NENGETSU='{0}'", year_month.Replace("-", "/")); }
            if (sale_date != null) { sql += string.Format(", @SEIKYUDATE_TO = '{0}'", sale_date.Replace("-", "/")); }
            sql += string.Format(", @FROM_TOKUISAKI = '{0}'", customer_id1.ToString());
            sql += string.Format(", @TO_TOKUISAKI = '{0}'", customer_id2.ToString());

            if (closing_days > 0) { sql += string.Format(", @SHIME_DAY = {0}", closing_days.ToString()); }
            if (seikyu_tantou > 0) { sql += string.Format(", @SEIKYU_TANTOU = {0}", seikyu_tantou.ToString()); }

            IEnumerable<Data.V_SeikyuDataList> resultData = await _context.V_SeikyuDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();

            List<V_InvoiceDataList> result = new();

            foreach(Data.V_SeikyuDataList item in resultData)
            {
                V_InvoiceDataList data = new();
                CopyProperty(data, item);
                result.Add(data);
            }
            return result;
        }

        /// <summary>
        /// 請求書チェックリストを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月</param>
        /// <param name="closing_days">締日</param>
        /// <param name="invoice_person">請求担当</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日</param>
        /// <param name="sale_date">売上年月日（まで）</param>
        /// <returns>請求書チェックリスト</returns>
        public async Task<IEnumerable<V_InvoiceCheckDataList>> GetInvoiceCheckDataList(
            int company_id,
            string year_month,
            int closing_days,
            int invoice_person,
            int customer_id1,
            int customer_id2,
            string publish_date,
            string sale_date)
        {
            using System.Data.Common.DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[Proc_V_SeikyuCheckDataList]";

            //common
            cmd.CommandType = CommandType.StoredProcedure;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", SqlDbType.Int) { Value = company_id });
            cmd.Parameters.Add(new SqlParameter("@SEIKYU_NENGETSU", SqlDbType.Date) { Value = year_month });
            cmd.Parameters.Add(new SqlParameter("@SHIME_DAY", SqlDbType.Int) { Value = closing_days });
            cmd.Parameters.Add(new SqlParameter("@SEIKYU_TANTOU", SqlDbType.Int) { Value = invoice_person });
            cmd.Parameters.Add(new SqlParameter("@FROM_TOKUISAKI", SqlDbType.VarChar) { Value = customer_id1 });
            cmd.Parameters.Add(new SqlParameter("@TO_TOKUISAKI", SqlDbType.VarChar) { Value = customer_id2 });
            cmd.Parameters.Add(new SqlParameter("@PRINT_DATE", SqlDbType.Date) { Value = publish_date });
            cmd.Parameters.Add(new SqlParameter("@SEIKYUDATE_TO", SqlDbType.Date) { Value = string.IsNullOrWhiteSpace(sale_date) ? "1900-01-01" : sale_date });

            System.Data.Common.DbDataReader reader = await cmd.ExecuteReaderAsync();
            List<V_InvoiceCheckDataList> result = new();

            while (reader.Read())
            {
                result.Add(new V_InvoiceCheckDataList
                {
                    Check_Seikyu_ID = reader["Check_Seikyu_ID"] != DBNull.Value ? Convert.ToInt32(reader["Check_Seikyu_ID"]) : 0,
                    Inquiry_Status = reader["Inquiry_Status"] != DBNull.Value ? Convert.ToInt32(reader["Inquiry_Status"]) : 0,
                    Seikyu_Tantou_Name = reader["Seikyu_Tantou"]?.ToString(),
                    Customer_Name = reader["Customer_Name"]?.ToString(),
                    Mail_Address1 = reader["Mail_Address1"]?.ToString(),
                    Mail_Address2 = reader["Mail_Address2"]?.ToString(),
                    Customer_Branch_ID = reader["Customer_Branch_ID"] != DBNull.Value ? Convert.ToInt32(reader["Customer_Branch_ID"]) : 0,
                    Shime_Day = reader["Shime_Day"] != DBNull.Value ? Convert.ToInt32(reader["Shime_Day"]) : 0,
                    Meisai_Count = Convert.ToInt32(reader["Meisai_Count"]),
                    SeikyuUnchin = reader["SeikyuUnchin"] != DBNull.Value ? Convert.ToDecimal(reader["SeikyuUnchin"]) : null,
                    Tatekaekin = reader["Tatekaekin"] != DBNull.Value ? Convert.ToDecimal(reader["Tatekaekin"]) : null,
                    Anken_Count = Convert.ToInt32(reader["Anken_Count"]),
                    Kakutei_Count = Convert.ToInt32(reader["Kakutei_Count"]),
                    Zantei_Count = Convert.ToInt32(reader["Zantei_Count"]),
                    Kari_Count = Convert.ToInt32(reader["Kari_Count"]),
                    Uriage_Unchin_ID = Convert.ToInt32(reader["Uriage_Unchin_ID"]),
                });
            }
            return result;
        }
    }
}
