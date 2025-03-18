using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeikyuWeb.Dto.ReportDto;
using SeikyuWeb.Models;
using SeikyuWeb.Common;
using SeikyuWeb.Repositories.Interfaces;
using SeikyuWeb.Services.Interfaces;
using static SeikyuWeb.Dto.ReportDto.ReportDto;
using SeikyuWeb.Models.ReportCommons;
using System.Reflection;
using static SeikyuWeb.Common.SystemEnums;
using static SeikyuWeb.Common.SystemConstants;
using Microsoft.EntityFrameworkCore;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// ReportCommonServiceクラスは共通帳票のサービス
    /// </summary>
    public class ReportCommonService : IReportCommonService
    {
        private readonly IReportCommonRepository _reportCommonRepository;
        private readonly ISeikyuRepository _seikyuRepository;
        private readonly INyukinRepository _nyukinRepository;
        private readonly IReportLayoutRepository _reportLayoutRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reportCommonRepository">共通帳票リポジトリ</param>
        /// <param name="seikyuRepository">請求リポジトリ</param>
        /// <param name="nyukinRepository">入金リポジトリ</param>
        public ReportCommonService(IReportCommonRepository reportCommonRepository, ISeikyuRepository seikyuRepository, INyukinRepository nyukinRepository, IReportLayoutRepository reportLayoutRepository)
        {
            _reportCommonRepository = reportCommonRepository;
            _seikyuRepository = seikyuRepository;
            _nyukinRepository = nyukinRepository;
            _reportLayoutRepository = reportLayoutRepository;
        }

        /// <summary>
        /// 共通帳票に帳票一覧を取得
        /// </summary>
        /// <param name="reportKubunId">(ルーターで区分検索帳票のIDパラメータ値</param>
        /// <param name="jsonData">プロシージャのjsonデータ</param>
        /// <param name="userId">ユーザーのId</param>
        /// <param name="companyId">会社のId</param>
        /// <param name="reportType">The type of report</param>
        /// <returns>ReportCommonDtoのリスト</returns>
        public async Task<ReportCommonDto> GetReportCommonAsync(int reportKubunId, Dictionary<string, object> jsonData, int userId, int companyId, int reportType, bool isPdf)
        {
            ReportCommonDto model = new();
            MReportSerchKubun kubun = await _reportCommonRepository.GetReportSearchKubunAsync(reportKubunId)
                ?? throw new BadHttpRequestException(SystemConstants.Message.DataNotFound);
            if (kubun.ClassName == null)
            {
                throw new BadHttpRequestException(string.Format(SystemConstants.Message.InValidParam, "Class_Name"));
            }

            if (reportType == (int)ReportType.SEIKYUSHO)
            {
                if (!SystemConstants.ReportCommon.ReportHtmlSeikyushoList.Any(s => s.Equals(kubun.ReportHtml, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new BadHttpRequestException(SystemConstants.Message.ReportHtmlInvalid);
                }
            }

            model.ReportSearchKubun = kubun;

            if (kubun.ReportHtml.Equals("Invoice-list", StringComparison.OrdinalIgnoreCase) || !isPdf)
            {
                model.ReportOutputItemList = await _reportCommonRepository.GetReportOutputItemListAsync(reportKubunId, userId, companyId, isPdf);

                object instance = CommonHelper.GetInstanceByModelName(kubun.ClassName);

                foreach (var item in model.ReportOutputItemList)
                {
                    string modelProperty = item.ReportOutputItemMaster.ModelProoerty;

                    if (instance.GetType().GetProperty(modelProperty) == null)
                    {
                        throw new BadHttpRequestException(string.Format(SystemConstants.Message.PropertyNotMatching, modelProperty, kubun.ClassName));
                    }
                }
            }
            else
            {
                model.ReportOutputItemList = new List<MReportOutputItem>();
            }

            Dictionary<string, object> data = jsonData;

            //必須フィールドを検証
            foreach (var item in model.ReportSearchKubun.Report_Serch_Item_List)
            {
                if (item.NotNullFlg == 1 && (data[item.ModelProoerty] == null || ((data[item.ModelProoerty] as string) == "")))
                {
                    throw new BadHttpRequestException(string.Format(SystemConstants.Message.RequiredField, item.ModelProoerty));
                }
            }

            Dictionary<string, object> dataParam = getParamForProcedure(model.ReportSearchKubun.Report_Detail_Param_List, jsonData);
            List<string> sortList = new();
            string sortType = "ASC";
            if (model.ReportSearchKubun.DataSort != null)
            {
                List<string> sortParts = model.ReportSearchKubun.DataSort.Split(',').Select(s => s.Trim()).ToList();
                foreach (var part in sortParts)
                {
                    if (part.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
                    {
                        sortList.Add(part[..^5].Trim());
                        sortType = "DESC";
                    }
                    else if (part.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase))
                    {
                        sortList.Add(part[..^4].Trim());
                    }
                    else
                    {
                        sortList.Add(part);
                    }
                }
            }
            IEnumerable<object> dataModel = await _reportCommonRepository.GetReportCommonAsync(kubun.ProcName, kubun.ClassName, dataParam, sortList, sortType);
            foreach (var item in dataModel)
            {
                V_ReportCommon_Local reportItem = new V_ReportCommon_Local();

                // Use reflection to get properties and their values
                foreach (var property in item.GetType().GetProperties())
                {
                    object value = property.GetValue(item);
                    reportItem[property.Name] = value;
                }

                model.ReportCommonList.Add(reportItem);
            }

            model.ShimeDay = DateTime.Now;
            return model;
        }

        /// <summary>
        /// Get Report layout
        /// </summary>
        /// <param name="printKubun"></param>
        /// <param name="searchKububId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TReportLayout> GetReportLayout(int printKubun, int searchKububId)
        {
            return _reportLayoutRepository.FindByCondition(item => item.PrintKubun == printKubun && item.ReportSerchKubunId == searchKububId).FirstOrDefaultAsync();
        }

        /// <summary>
        ///プロシージャのパラメータを取得
        /// </summary>
        /// <param name="reportDetailParamList">帳票詳細パラメーターのリスト</param>
        /// <param name="jsonData">json データ</param>
        /// <returns>ディクショナリオブジェクト</returns>
        private static Dictionary<string, object> getParamForProcedure(ICollection<MReportDetailParam> reportDetailParamList, Dictionary<string, object> jsonData)
        {
            Dictionary<string, object> data = jsonData;
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
                    if (item.ParamModelProoerty == null || item.ParamModelProoerty == "")
                    {
                        dataParam[item.ParamName] = item.ParamVal;
                    }
                    else
                    {
                        if (data.ContainsKey(item.ParamModelProoerty)
                            && data[item.ParamModelProoerty] != null
                            && (data[item.ParamModelProoerty] as string) != "")
                        {
                            object x = data[item.ParamModelProoerty];
                            dataParam[item.ParamName] = data[item.ParamModelProoerty];
                        }
                        else
                        {
                            dataParam[item.ParamName] = item.ParamDefault ?? null;
                        }
                    }
                    if (dataParam[item.ParamName] == null)
                    {
                        dataParam.Remove(item.ParamName);
                        continue;
                    }

                    switch (item.ParamType)
                    {
                        case "int":
                            dataParam[item.ParamName] = int.Parse(dataParam[item.ParamName].ToString());
                            break;
                        case "float":
                            dataParam[item.ParamName] = decimal.Parse(dataParam[item.ParamName].ToString());
                            break;
                        case "string":
                            dataParam[item.ParamName] = dataParam[item.ParamName].ToString();
                            break;
                        case "date":
                            DateTime dateTmp = DateTime.Parse(dataParam[item.ParamName].ToString());
                            string formatDate = item.ParamFormat ?? "yyyy/MM/dd";
                            dataParam[item.ParamName] = dateTmp.ToString(formatDate);
                            break;
                    }
                }
                catch (Exception)
                {
                    dataParam.Remove(item.ParamName);
                }

            }
            return dataParam;
        }

        /// <summary>
        ///モデルPDF帳票を取得
        /// </summary>
        /// <param name="dataReport">帳票データ</param>
        /// <returns>モデルPDF帳票</returns>
        public async Task<PDFReportModel> GetModelPDFReport(ReportCommonDto dataReport, int compamyId, int seikyuId)
        {
            PDFReportModel model = new()
            {
                ReportSearchKubun = dataReport.ReportSearchKubun,
                ReportOutputItemList = dataReport.ReportOutputItemList
                    .OrderBy(x => x.ReportOutputItemMaster.RowOrder)
                    .ThenBy(x => x.SortOrder)
                    .ToList(),
                ReportCommonList = dataReport.ReportCommonList
            };
            switch (model.ReportSearchKubun.ReportHtml.ToLower())
            {
                case SystemConstants.ReportCommon.ReportHtmlType.Bill1:
                case SystemConstants.ReportCommon.ReportHtmlType.Bill2:
                case SystemConstants.ReportCommon.ReportHtmlType.Bill3:
                case SystemConstants.ReportCommon.ReportHtmlType.Bill4:
                    // Get data seikyu
                    TSeikyu seikyu = await _seikyuRepository.GetByIdAsync(seikyuId);
                    DateTime seikyuMonth = seikyu?.SeikyuMonth ?? DateTime.Now;
                    int shimeDay = seikyu?.ShimeDay ?? 0;
                    DateTime printDate = seikyu?.PrintDate ?? DateTime.Now;
                    int zeiKubun = seikyu?.ZeiKubun ?? 0;
                    int customerBranchId = seikyu?.CustomerBranchId ?? 0;

                    // Get param procedure Proc_V_SeikyuCheckDataList
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("COMPANY_ID", compamyId);
                    param.Add("FROM_TOKUISAKI", 0);
                    param.Add("TO_TOKUISAKI", 9999999999999);
                    param.Add("SEIKYU_NENGETSU", seikyuMonth);
                    param.Add("PRINT_DATE", printDate);
                    param.Add("SHIME_DAY", shimeDay);
                    param.Add("ZEI_KUBUN", zeiKubun);

                    List<V_SeikyuCheckDataList> seikyuCheckDataList = await GetSeikyuCheckDataList(param);
                    IEnumerable<TNyukin> nyukins = await _nyukinRepository.GetListBySeikyuIdAsync(seikyuId, seikyuMonth, shimeDay);

                    //billオブジェクトの初期化
                    model.Bill = new PDFBill
                    {
                        SeikyuUnchin = (int?)seikyuCheckDataList?.Where(x => customerBranchId == 0 ? true : customerBranchId == x.Customer_Branch_ID).Sum(x => x.SeikyuUnchin) ?? 0,
                        Nyukin = nyukins.FirstOrDefault()
                    };

                    switch (model.ReportSearchKubun.ReportHtml.ToLower())
                    {
                        case SystemConstants.ReportCommon.ReportHtmlType.Bill1:
                            model.Bill.BillList = MappingData(dataReport.ReportCommonList);
                            if (!model.Bill.BillList.Any())
                            {
                                throw new BadHttpRequestException(SystemConstants.Message.NoDataOutput);
                            }
                            break;
                        case SystemConstants.ReportCommon.ReportHtmlType.Bill2:
                            model.Bill.BillList2 = MappingDataBill2(dataReport.ReportCommonList);
                            if (!model.Bill.BillList2.Any())
                            {
                                throw new BadHttpRequestException(SystemConstants.Message.NoDataOutput);
                            }
                            break;
                        case SystemConstants.ReportCommon.ReportHtmlType.Bill3:
                            model.Bill.BillList3 = MappingDataBill3(dataReport.ReportCommonList);
                            if (!model.Bill.BillList3.Any())
                            {
                                throw new BadHttpRequestException(SystemConstants.Message.NoDataOutput);
                            }
                            break;
                        case SystemConstants.ReportCommon.ReportHtmlType.Bill4:
                            model.Bill.BillList4 = MappingDataBill4(dataReport.ReportCommonList);
                            if (!model.Bill.BillList4.Any())
                            {
                                throw new BadHttpRequestException(SystemConstants.Message.NoDataOutput);
                            }
                            break;
                    }

                    break;

                default:
                    break;
            }
            return model;
        }

        /// <summary>
        /// Get data check seikyu list
        /// </summary>
        /// <param name="paramData">param procedure Proc_V_SeikyuCheckDataList</param>
        /// <returns>Data check seikyu list</returns>
        private async Task<List<V_SeikyuCheckDataList>> GetSeikyuCheckDataList(Dictionary<string, object> paramData)
        {
            IEnumerable<object> dataModel = await _reportCommonRepository.GetReportCommonAsync(ProcName.SeikyuCheckDataList, ProcModelName.SeikyuCheckSeikyuDataList, paramData, new List<string>());
            List<V_SeikyuCheckDataList> data = new List<V_SeikyuCheckDataList>() { };
            foreach (var item in dataModel)
            {
                data.Add((V_SeikyuCheckDataList)item);
            }

            return data;
        }

        /// <summary>
        ///共通帳票データと請求書リストをマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書のデータリスト</returns>
        private static List<V_ReportBillList> MappingData(List<V_ReportCommon_Local> reportCommonList)
            => reportCommonList.Select(x => ConvertTo<V_ReportBillList>(x)).ToList();

        /// <summary>
        ///共通帳票データと請求書リスト２をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト２のデータ</returns>
        private static List<V_ReportBillList2> MappingDataBill2(List<V_ReportCommon_Local> reportCommonList)
            => reportCommonList.Select(x => ConvertTo<V_ReportBillList2>(x)).ToList();

        /// <summary>
        ///共通帳票データと請求書リスト３をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト３のデータ</returns>
        private static List<V_ReportBillList3> MappingDataBill3(List<V_ReportCommon_Local> reportCommonList)
            => reportCommonList.Select(x => ConvertTo<V_ReportBillList3>(x)).ToList();

        /// <summary>
        ///共通帳票データと請求書リスト４をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト４のデータ</returns>
        private static List<V_ReportBillList4> MappingDataBill4(List<V_ReportCommon_Local> reportCommonList)
            => reportCommonList.Select(x => ConvertTo<V_ReportBillList4>(x)).ToList();

        /// <summary>
        ///共通データを変換
        /// </summary>
        /// <typeparam name="T">データ型の変換</typeparam>
        /// <typeparam name="T">データ型の変換</typeparam>
        /// <returns>変換後のデータリスト</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ConvertTo<T>(Dictionary<string, object> source) where T : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            T target = new();

            foreach (var kvp in source)
            {
                PropertyInfo property = typeof(T).GetProperty(kvp.Key);
                if (property != null && kvp.Value != null)
                {
                    property.SetValue(target, Convert.ChangeType(kvp.Value, property.PropertyType));
                }
            }

            return target;
        }
    }
}

