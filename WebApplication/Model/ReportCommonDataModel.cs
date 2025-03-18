using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// ReportCommonDataModelクラスは手続きレポート共通のモデルです
    /// </summary>
    public class ReportCommonDataModel
    {

        private readonly ApplicationDbContext _context;

        public ReportCommonDataModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// モデル名でインスタンスを取得します
        /// </summary>
        /// <param name="modelName">手続きのモデル名</param>
        /// <returns>手続きのオブジェクトモデルインスタンス</returns>
        public object GetInstanceByModelName(string modelName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullName = assembly.GetName().Name + ".Data.ReportCommons." + modelName;
            Type modelType = assembly.GetType(fullName);

            if (modelType == null)
            {
                throw new BadHttpRequestException(string.Format(SystemConstants.Message.InValidParam, "Class_Name"));
            }

            object modelInstance = Activator.CreateInstance(modelType);

            return modelInstance;
        }

        /// <summary>
        /// 手続きからデータを取得します
        /// </summary>
        /// <param name="modelName">手続きのモデル名</param>
        /// <param name="procName">手続きの名前</param>
        /// <param name="paramDetails">手続きのパラメータ</param>
        /// <returns>手続きのデータ</returns>
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

            using (System.Data.Common.DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (System.Data.Common.DbDataReader reader = await command.ExecuteReaderAsync())
                {

                    object checkInstance = this.GetInstanceByModelName(modelName);

                    System.Data.DataTable schemaTable = reader.GetSchemaTable();

                    // モデル共通にカラムが存在するか確認
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
                        object modelInstance = this.GetInstanceByModelName(modelName);

                        foreach (var prop in modelInstance.GetType().GetProperties())
                        {
                            // プロパティに値を設定
                            if (schemaTable != null && schemaTable.Rows.Count > 0)
                            {
                                try
                                {
                                    System.Data.DataRow column = schemaTable.Rows.Cast<System.Data.DataRow>().FirstOrDefault(x => x["ColumnName"].ToString() == prop.Name);
                                    if (column != null)
                                    {
                                        object value = reader[prop.Name];
                                        if (value != DBNull.Value)
                                        {
                                            Type targetType = prop.PropertyType;

                                            // プロパティタイプがnullableかどうかを確認
                                            if (Nullable.GetUnderlyingType(targetType) != null)
                                            {
                                                targetType = Nullable.GetUnderlyingType(targetType);
                                            }

                                            object convertedValue = Convert.ChangeType(value, targetType);
                                            prop.SetValue(modelInstance, convertedValue);
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
