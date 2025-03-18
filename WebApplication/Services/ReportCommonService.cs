using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Model;
using WebApplication.Repositories;
using static WebApplication.Common.SystemConstants;

namespace WebApplication.Services
{
    /// <summary>
    /// ReportCommonServiceクラスは共通帳票のサービス
    /// </summary>
    public class ReportCommonService : IReportCommonService
    {
        private readonly IReportCommonRepository _reportCommonRepository;
        private readonly ReportCommonDataModel _reportCommonDataModel;

        public ReportCommonService(IReportCommonRepository reportCommonRepository, ReportCommonDataModel reportCommonDataModel)
        {
            _reportCommonRepository = reportCommonRepository;
            _reportCommonDataModel = reportCommonDataModel;
        }

        /// <summary>
        /// 共通帳票に帳票一覧を取得
        /// </summary>
        /// <param name="reportKubunId">ルーターで区分検索帳票のIDパラメータ値</param>
        /// <param name="jsonData">プロシージャのjsonデータ</param>
        /// <param name="userId">ユーザーのId</param>
        /// <param name="companyId">会社のId</param>
        /// <returns>ReportCommonDtoのリスト</returns>
        public async Task<ReportCommonDto> GetReportCommonAsync(int reportKubunId, string jsonData, int userId, int companyId)
        {
            ReportCommonDto model = new ReportCommonDto();
            M_Report_Serch_Kubun kubun = await _reportCommonRepository.GetReportSearchKubunAsync(reportKubunId);
            if (kubun == null)
            {
                throw new BadHttpRequestException(Message.DataNotFound);
            }

            if (kubun.Class_Name == null)
            {
                throw new BadHttpRequestException(string.Format(SystemConstants.Message.InValidParam, "Class_Name"));
            }

            model.ReportSearchKubun = kubun;

            //帳票一覧のhtml共通帳票を検証
            if (kubun.Report_Html.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                model.ReportOutputItemList = await _reportCommonRepository.GetReportOutputItemListAsync(reportKubunId, userId, companyId, true);

                object instance = _reportCommonDataModel.GetInstanceByModelName(kubun.Class_Name);

                foreach (var item in model.ReportOutputItemList)
                {
                    string modelProperty = item.Report_Output_Item_Master.Model_Prooerty;
                }
            }
            else if (kubun.Report_Html.Equals("Invoice-list", StringComparison.OrdinalIgnoreCase))
            {
                model.ReportOutputItemList = await _reportCommonRepository.GetReportOutputItemListAsync(reportKubunId, userId, companyId, true);

                object instance = _reportCommonDataModel.GetInstanceByModelName(kubun.Class_Name);

                foreach (var item in model.ReportOutputItemList)
                {
                    string modelProperty = item.Report_Output_Item_Master.Model_Prooerty;

                    if (instance.GetType().GetProperty(modelProperty) == null)
                    {
                        throw new BadHttpRequestException(string.Format(Message.PropertyNotMatching, modelProperty, kubun.Class_Name));
                    }
                }
            }
            else
            {
                model.ReportOutputItemList = new List<M_Report_Output_Item>();
            }

            Dictionary<string, string> data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);

            //必須フィールドを検証
            foreach (var item in model.ReportSearchKubun.Report_Serch_Item_List)
            {
                if (item.NotNull_Flg == 1 && (data[item.Model_Prooerty] == null || data[item.Model_Prooerty] == ""))
                {
                   // throw new BadHttpRequestException(string.Format(Message.RequiredField, item.Model_Prooerty));
                }
            }

            Dictionary<string, object> dataParam = getParamForProcedure(model.ReportSearchKubun.Report_Detail_Param_List, jsonData);
            string companyIdKey = dataParam.Keys.FirstOrDefault(k => k.Equals(SystemConstants.ReportCommonVariable.COMPANY_ID, StringComparison.OrdinalIgnoreCase));
            if (companyIdKey != null)
            {
                dataParam[companyIdKey] = companyId;
            }

            string userIdKey = dataParam.Keys.FirstOrDefault(k => k.Equals(SystemConstants.ReportCommonVariable.USER_ID, StringComparison.OrdinalIgnoreCase));
            if (userIdKey != null)
            {
                dataParam[userIdKey] = userId;
            }

            List<string> sortList = new List<string>();
            string sortType = "ASC";
            if (model.ReportSearchKubun.Data_Sort != null)
            {
                List<string> sortParts = model.ReportSearchKubun.Data_Sort.Split(',').Select(s => s.Trim()).ToList();
                foreach (var part in sortParts)
                {
                    if (part.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
                    {
                        sortList.Add(part.Substring(0, part.Length - 5).Trim());
                        sortType = "DESC";
                    }
                    else if (part.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase))
                    {
                        sortList.Add(part.Substring(0, part.Length - 4).Trim());
                    }
                    else
                    {
                        sortList.Add(part);
                    }
                }
            }

            model.ReportCommonList = await _reportCommonRepository.GetReportCommonAsync(kubun.Proc_Name, kubun.Class_Name, dataParam, sortList, sortType);
            model.ShimeDay = DateTime.Now;
            return model;
        }

        /// <summary>
        /// プロシージャのパラメータを取得
        /// </summary>
        /// <param name="reportDetailParamList">帳票詳細パラメーターのリスト</param>
        /// <param name="jsonData">json データ</param>
        /// <returns>ディクショナリオブジェクト</returns>
        private Dictionary<string, object> getParamForProcedure(ICollection<M_Report_Detail_Param> reportDetailParamList, string jsonData)
        {
            Dictionary<string, string> data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);
            Dictionary<string, object> dataParam = new Dictionary<string, object>();

            //1.現在の項目の Param_Model_Prooerty プロパティが null又は空の文字列であるかを確認
            //2.データ ディクショナリに Param_Model_Prooerty の値と一致するキーが含まれているか、及び値が null又は空の文字列であるかを確認
            //3.dataParam ディクショナリに該当するキーの値が nullであるかを確認
            //4.現在の項目の Param_Type プロパティに基づいて型変換を実行
            //5.try ブロックの操作中に例外が発生すると、dataParam ディクショナリからキーと値のペアを削除
            foreach (var item in reportDetailParamList)
            {
                try
                {
                    if (item.Param_Model_Prooerty == null || item.Param_Model_Prooerty == "")
                    {
                        dataParam[item.Param_Name] = item.Param_Val;
                    }
                    else
                    {
                        if (data.ContainsKey(item.Param_Model_Prooerty)
                            && data[item.Param_Model_Prooerty] != null
                            && data[item.Param_Model_Prooerty] != "")
                        {
                            string x = data[item.Param_Model_Prooerty];
                            dataParam[item.Param_Name] = data[item.Param_Model_Prooerty];
                        }
                        else
                        {
                            dataParam[item.Param_Name] = item.Param_Default == null ? null : item.Param_Default;
                        }
                    }
                    if (dataParam[item.Param_Name] == null)
                    {
                        dataParam.Remove(item.Param_Name);
                        continue;
                    }

                    switch (item.Param_Type)
                    {
                        case "int":
                            dataParam[item.Param_Name] = int.Parse(dataParam[item.Param_Name].ToString());
                            break;
                        case "float":
                            dataParam[item.Param_Name] = decimal.Parse(dataParam[item.Param_Name].ToString());
                            break;
                        case "string":
                            dataParam[item.Param_Name] = dataParam[item.Param_Name].ToString();
                            break;
                        case "date":
                            DateTime dateTmp = DateTime.Parse(dataParam[item.Param_Name].ToString());
                            string formatDate = item.Param_Format != null ? item.Param_Format : "yyyy/MM/dd";
                            dataParam[item.Param_Name] = dateTmp.ToString(formatDate);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    dataParam.Remove(item.Param_Name);
                }

            }
            return dataParam;
        }
    }

    /// <summary>
    /// インターフェース・クラス ReportCommonService は共通帳票のサービス
    /// </summary>
    public interface IReportCommonService
    {
        /// <summary>
        /// 共通帳票に帳票一覧を取得
        /// </summary>
        /// <param name="reportKubunId">ルーターで区分検索帳票のIDパラメータ値</param>
        /// <param name="jsonData">プロシージャのjsonデータ</param>
        /// <param name="userId">ユーザーのId</param>
        /// <param name="companyId">会社のId</param>
        /// <returns>ReportCommonDtoのリスト</returns>
        public Task<ReportCommonDto> GetReportCommonAsync(int reportKubunId, string jsonData, int userId, int companyId);
    }
}

