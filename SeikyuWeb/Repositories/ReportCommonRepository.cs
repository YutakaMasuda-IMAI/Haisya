using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Common;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    /// <summary>
    /// 帳票共通リポジトリクラス
    /// </summary>
    public class ReportCommonRepository : RepositoryBaseAsync<MReportSerchKubun, HaisyaContext>, IReportCommonRepository
    {
        public ReportCommonRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 帳票検索データを取得します。
        /// </summary>
        /// <param name="reportSearchId">帳票検索のIdです</param>
        /// <param name="reportNumber">帳票番号です</param>
        /// <returns>M_Report_Serchの詳細情報です</returns>
        public async Task<MReportSerch> GetReportSearchAsync(int reportSearchId)
        {
            MReportSerch result = await DbContext.MReportSerches
                .Where(c => c.ReportSerchId == reportSearchId)
                .Include(c => c.Report_Serch_Kubun_List.Where(k => k.SortOrder == 0))
                .ThenInclude(c => c.Report_Output_Item_List)
                .ThenInclude(c => c.ReportOutputItemMaster)
                .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// 区分検索帳票の詳細を取得します。
        /// </summary>
        /// <param name="reportKubunId">区分検索帳票のIdです</param>
        /// <returns>詳細なM_Report_Serch_Kubun情報です</returns>
        public async Task<MReportSerchKubun> GetReportSearchKubunAsync(int reportKubunId)
        {
            MReportSerchKubun result = await DbContext.MReportSerchKubuns
                .Where(c => c.ReportSerchKubunId == reportKubunId)
                .Include(c => c.Report_Serch)
                .Include(c => c.Report_Serch_Item_List)
                .Include(c => c.Report_Detail_Param_List)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                if (result.Report_Serch != null)
                {
                    result.Report_Serch.Report_Serch_Kubun_List = new HashSet<MReportSerchKubun>();
                }
                foreach (var item in result.Report_Serch_Item_List)
                {
                    item.Report_Serch_Kubun = null;
                }
                foreach (var item in result.Report_Detail_Param_List)
                {
                    item.Report_Serch_Kubun = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 区分帳票のidで出力項目帳票リストを取得します。
        /// </summary>
        /// <param name="reportKubunId">区分検索帳票のIdです</param>
        /// <param name="userId">ユーザーのIdです</param>
        /// <param name="companyId">会社のIdです</param>
        /// <param name="isPdfReport">PDF帳票かどうかを示します</param>
        /// <returns>出力項目帳票リストです</returns>
        public async Task<IEnumerable<MReportOutputItem>> GetReportOutputItemListAsync(int reportKubunId, int userId, int companyId, bool isPdfReport = true)
        {
            List<MReportOutputItem> result = await DbContext.MReportOutputItems
                .Where(c => c.ReportSerchKubunId == reportKubunId)
                .Where(c => c.UserId == userId)
                .Include(c => c.ReportOutputItemMaster)
                .Where(c => c.ReportOutputItemMaster.ReportItemFlg == (isPdfReport ? 1 : 0))
                .ToListAsync();

            if (result.Count == 0)
            {
                result = await DbContext.MReportOutputItems
                    .Where(c => c.ReportSerchKubunId == reportKubunId)
                    .Where(c => c.UserId == 0)
                    .Include(c => c.ReportOutputItemMaster)
                    .Where(c => c.ReportOutputItemMaster.ReportItemFlg == (isPdfReport ? 1 : 0))
                    .ToListAsync();
            }

            foreach (var item in result)
            {
                if (item.ReportOutputItemMaster != null)
                {
                    item.ReportOutputItemMaster.Report_Output_Item_List = new HashSet<MReportOutputItem>();
                }
            }
            return result;
        }

        /// <summary>
        /// 共通帳票のプロシージャーデータを取得します。
        /// </summary>
        /// <param name="procedureName">プロシージャ名です</param>
        /// <param name="className">プロシージャのクラス名です</param>
        /// <param name="paramDetail">パラメーターの詳細です</param>
        /// <param name="sortFields">ソートフィールドです</param>
        /// <param name="sortType">ソートタイプです</param>
        /// <returns>帳票のプロシージャデータリストです</returns>
        public async Task<IEnumerable<object>> GetReportCommonAsync(
            string procedureName,
            string className,
            Dictionary<string, object> paramDetail,
            List<string> sortFields,
            string sortType = "ASC"
        )
        {
            IEnumerable<object> resultData = await GetDataFromProcedure(className, procedureName, paramDetail);
            object instance = CommonHelper.GetInstanceByModelName(className);
            IOrderedEnumerable<object> orderedResultData = resultData.OrderBy(x => 0);
            foreach (var field in sortFields)
            {
                PropertyInfo propertyInfo = instance.GetType().GetProperty(field);
                if (propertyInfo != null)
                {
                    if (sortType == "ASC")
                    {
                        orderedResultData = orderedResultData.ThenBy(x => propertyInfo.GetValue(x));
                    }
                    else
                    {
                        orderedResultData = orderedResultData.ThenByDescending(x => propertyInfo.GetValue(x));
                    }
                }
            }
            return orderedResultData;
        }

        /// <summary>
        /// プロシージャからデータを取得します。
        /// </summary>
        /// <param name="modelName">プロシージャのモデル名です</param>
        /// <param name="procName">プロシージャ名です</param>
        /// <param name="paramDetails">プロシージャのパラメーターです</param>
        /// <returns>プロシージャのデータです</returns>
        public async Task<IEnumerable<object>> GetDataFromProcedure(string modelName, string procName, Dictionary<string, object> paramDetails)
        {
            string sql = string.Format("EXECUTE [dbo].[{0}]", procName).ToString();

            foreach (var item in paramDetails)
            {
                if (item.Value == null)
                {
                    sql += string.Format("@{0} = NULL,", item.Key);
                }
                else
                {
                    sql += string.Format("@{0} = '{1}',", item.Key, item.Value);
                }
            }
            sql = sql.TrimEnd(',');

            List<object> resultData = new List<object>();

            using (DbCommand command = DbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                await DbContext.Database.OpenConnectionAsync();

                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {

                    object checkInstance = CommonHelper.GetInstanceByModelName(modelName);

                    DataTable schemaTable = reader.GetSchemaTable();

                    // check column exists in ModelCommon
                    foreach (var column in schemaTable.Rows.Cast<System.Data.DataRow>())
                    {
                        string columnName = column["ColumnName"].ToString();
                        if (checkInstance.GetType().GetProperty(columnName) == null)
                        {
                            //    throw new BadHttpRequestException(string.Format(SystemConstants.Message.PropertyNotMatching, columnName, modelName));
                        }
                    }

                    while (await reader.ReadAsync())
                    {
                        object modelInstance = CommonHelper.GetInstanceByModelName(modelName);

                        foreach (var prop in modelInstance.GetType().GetProperties())
                        {
                            // set value for property
                            if (schemaTable != null && schemaTable.Rows.Count > 0)
                            {
                                try
                                {
                                    DataRow column = schemaTable.Rows.Cast<System.Data.DataRow>().FirstOrDefault(x => x["ColumnName"].ToString() == prop.Name);
                                    if (column != null)
                                    {
                                        object value = reader[prop.Name];
                                        if (value != DBNull.Value)
                                        {
                                            Type targetType = prop.PropertyType;

                                            // Check if the property type is nullable
                                            if (Nullable.GetUnderlyingType(targetType) != null)
                                            {
                                                targetType = Nullable.GetUnderlyingType(targetType);
                                            }

                                            object convertedValue = Convert.ChangeType(value, targetType);
                                            prop.SetValue(modelInstance, convertedValue);
                                            //prop.SetValue(modelInstance, value);
                                        }
                                    }
                                    else
                                    {
                                        //    throw new BadHttpRequestException(string.Format(SystemConstants.Message.PropertyNotMatching, prop.Name, procName));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new BadHttpRequestException(ex.Message);
                                }

                            }
                        }
                        resultData.Add(modelInstance);
                    }
                }
            }

            return resultData;
        }
    }
}
