using DinkToPdf;
using DinkToPdf.Contracts;
using HaisyaWeb.API.WebApp;
using HaisyaWeb.Common;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Common.SystemConstants.ReportCommon;
using static HaisyaWeb.Common.SystemEnums;
using static HaisyaWeb.Models.ReportModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// Class ReportControllerは共通帳票ページに利用
    /// </summary>
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IConverter _converter;
        private readonly ILogger<HaisyaController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        /// <returns></returns>
        public ReportController(ILogger<HaisyaController> logger,
                SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting, IConverter converter)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _converter = converter;
        }

        /// <summary>
        /// 共通帳票ページ
        /// </summary>
        /// <param name="report_type">検索帳票番号</param>
        /// <returns>
        /// 200: 共通帳票ページ
        /// </returns>
        [HttpGet]
        [Route("Report/{report_type}")]
        public async Task<IActionResult> Detail(string report_type)
        {
            if (!report_type.TryParser<ReportType>(out ReportType rpt))
            {
                return NotFound();
            }
            try
            {
                using MasterDataApi api = new(_mapApiSettiong);

                ReportSearchModel model = new()
                {
                    Report_Serch = await api.GetReportSearch((int)rpt),
                };
                if (model.Report_Serch == null)
                {
                    return NotFound();
                }
                model.Report_Serch_Kubun_List = await api.GetReportSearchKubunList(model.Report_Serch.Report_Serch_ID);

                V_LoginUser_Local loguinUser = await GetLoginUser();

                model.Company_ID = loguinUser.Company_ID;

                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 共通帳票データ
        /// </summary>
        /// <param name="report_type">検索帳票番号</param>
        /// <returns>
        /// 200: 共通帳票データ
        /// </returns>
        [HttpGet]
        [Route("Report/ReportSearchByReportType/{report_type}")]
        public async Task<IActionResult> GetReportSearchByReportType(string report_type)
        {
            if (!report_type.TryParser<ReportType>(out ReportType rpt))
            {
                return NotFound();
            }
            try
            {
                using MasterDataApi api = new(_mapApiSettiong);

                ReportSearchModel model = new()
                {
                    Report_Serch = await api.GetReportSearch((int)rpt),
                };
                if (model.Report_Serch == null)
                {
                    return NotFound();
                }
                model.Report_Serch_Kubun_List = await api.GetReportSearchKubunList(model.Report_Serch.Report_Serch_ID);

                V_LoginUser_Local loguinUser = await GetLoginUser();

                model.Company_ID = loguinUser.Company_ID;

                return Ok(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 一覧項目検索帳票のJSON HTML
        /// </summary>
        /// <param name="param">項目検索のパラメータ</param>
        /// <returns>
        /// 200: 一覧項目検索帳票のJSON HTML
        /// </returns>
        public async Task<IActionResult> JsonGetDataSearchItemList(SearchItem param)
        {
            try
            {
                ReportSearchModel model = new();
                using MasterDataApi api = new(_mapApiSettiong);
                List<M_Report_Serch_Item_Local> ReportSearchItemList = await api.GetReportSearchItemList(param.Report_Serch_Kubun_ID);

                model.Report_Serch_Item_List = ReportSearchItemList
                    .Where(x => x.Report_Serch_Kubun_ID == param.Report_Serch_Kubun_ID)
                    .OrderBy(x => x.Row_Order)
                    .ThenBy(x => x.Sort_Order)
                    .ToList();

                //Row_Orderで項目のグループ化
                model.Group_Items = new List<GroupItem>();
                foreach (M_Report_Serch_Item_Local item in model.Report_Serch_Item_List)
                {
                    if (model.Group_Items.Count == 0)
                    {
                        model.Group_Items.Add(new GroupItem()
                        {
                            Display_Title = item.Display_Title,
                            Display_Message = item.Display_Message,
                            Row_Order = item.Row_Order,
                            NotNull_Flg = item.NotNull_Flg,
                            Items = new List<M_Report_Serch_Item_Local>() { item }
                        });
                    }
                    else
                    {
                        GroupItem group = model.Group_Items.Find(x => x.Row_Order == item.Row_Order);
                        if (group == null)
                        {
                            model.Group_Items.Add(new GroupItem()
                            {
                                Display_Title = item.Display_Title,
                                Display_Message = item.Display_Message,
                                Row_Order = item.Row_Order,
                                NotNull_Flg = item.NotNull_Flg,
                                Items = new List<M_Report_Serch_Item_Local>() { item }
                            });
                        }
                        else
                        {
                            group.NotNull_Flg = item.NotNull_Flg == 1 ? item.NotNull_Flg : group.NotNull_Flg;

                            group.Items.Add(item);
                        }
                    }
                }
                return await PartialViewAsJson("SearchItemList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// Report_Serch_Kubun_IDでPDF帳票ページをプレビュー
        /// </summary>
        /// <param name="template"> テンプレートpdf</param>
        /// <param name="paramSearch">検索出力のパラメーター</param>
        /// <returns>pdf共通帳票をビュー</returns>
        [HttpPost]
        [Route("Report/PrintPDF/{template}")]
        public async Task<IActionResult> PreviewPDFReport(string template, [FromForm] SearchExportParam searchParam)
        {
            try
            {
                API.WebApp.ReportCommonDataApi apiReport = new(_mapApiSettiong);
                PDFReportModel model = new();

                try
                {
                    Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                    ReportCommonDtoLocal dataReport = await apiReport.GetDataReport(searchParam.SerchKubunID, searchParam.searchJson, loguinUser.User_ID, loguinUser.Company_ID);
                    model = await GetModelPDFReport(dataReport, HttpUtility.UrlDecode(searchParam.searchJson));
                }
                catch (Exception ex)
                {
                    return Error(ex);
                }

                if (model.ReportSearchKubun == null)
                {
                    return NotFound();
                }

                if (!ReportCommon.ReportHtmlList.Any(s => s.Equals(model.ReportSearchKubun.Report_Html, StringComparison.OrdinalIgnoreCase))
                || !model.ReportSearchKubun.Report_Html.Equals(template.Replace(".pdf", ""), StringComparison.OrdinalIgnoreCase))
                {
                    ErrorViewModel errorModel = new();
                    errorModel.RequestId = GetErrorRequestId();
                    errorModel.Message = "Report_Htmlパラメーターが不正です";
                    return View("Error", errorModel);
                }

                if (model.ReportSearchKubun.Report_Serch.Pdf_Flg == 0)
                {
                    ErrorViewModel errorModel = new();
                    errorModel.RequestId = GetErrorRequestId();
                    errorModel.Message = "PDFパラメーターが不正です";
                    return View("Error", errorModel);
                }

                string ReportHtml = ConvertReportHtml(model.ReportSearchKubun.Report_Html);

                return View(ReportHtml, model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Preview PDF帳票をプレビューする
        /// </summary>
        /// <param name="report_type">帳票タイプ</param>
        /// <param name="searchJson">search Json</param>
        /// <returns>
        /// PDF帳票
        /// </returns>
        [HttpPost]
        [Route("Report/PreviewPDFReportByReportType/{report_type}")]
        public async Task<IActionResult> PreviewPDFReportByReportType(string report_type, [FromForm] string searchJson)
        {
            if (!report_type.TryParser<ReportType>(out ReportType rpt))
            {
                return NotFound();
            }
            try
            {
                using MasterDataApi api = new(_mapApiSettiong);

                ReportSearchModel reportSearchData = new()
                {
                    Report_Serch = await api.GetReportSearch((int)rpt),
                };
                if (reportSearchData.Report_Serch == null)
                {
                    return NotFound();
                }
                reportSearchData.Report_Serch_Kubun_List = await api.GetReportSearchKubunList(reportSearchData.Report_Serch.Report_Serch_ID);

                V_LoginUser_Local loguinUser = await GetLoginUser();

                reportSearchData.Company_ID = loguinUser.Company_ID;

                API.WebApp.ReportCommonDataApi apiReport = new(_mapApiSettiong);
                PDFReportModel model = new();

                M_Report_Serch_Kubun_Local reportSerchKubun = reportSearchData.Report_Serch_Kubun_List.FirstOrDefault();

                var parameterProcedure = Json(new { 
                });

                try
                {
                    ReportCommonDtoLocal dataReport = await apiReport.GetDataReport(reportSerchKubun.Report_Serch_Kubun_ID, searchJson, loguinUser.User_ID, loguinUser.Company_ID);
                    model = await GetModelPDFReport(dataReport, HttpUtility.UrlDecode(null));
                }
                catch (Exception ex)
                {
                    return Error(ex);
                }

                if (model.ReportSearchKubun == null)
                {
                    return NotFound();
                }

                if (!ReportCommon.ReportHtmlList.Any(s => s.Equals(model.ReportSearchKubun.Report_Html, StringComparison.OrdinalIgnoreCase))
                || !model.ReportSearchKubun.Report_Html.Equals(reportSerchKubun.Report_Html.Replace(".pdf", ""), StringComparison.OrdinalIgnoreCase))
                {
                    ErrorViewModel errorModel = new();
                    errorModel.RequestId = GetErrorRequestId();
                    errorModel.Message = "Report_Htmlパラメーターが不正です";
                    return View("Error", errorModel);
                }

                if (model.ReportSearchKubun.Report_Serch.Pdf_Flg == 0)
                {
                    ErrorViewModel errorModel = new();
                    errorModel.RequestId = GetErrorRequestId();
                    errorModel.Message = "PDFパラメーターが不正です";
                    return View("Error", errorModel);
                }

                string ReportHtml = ConvertReportHtml(model.ReportSearchKubun.Report_Html);

                return View(ReportHtml, model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }


        /// <summary>
        /// CSV 帳票を出力
        /// </summary>
        /// <param name="searchParam">検索出力のパラメーター</param>
        /// <returns>csvファイルを出力</returns>
        [HttpPost]
        [Route("Report/ExportCSV")]
        public async Task<IActionResult> ExportCSV([FromForm] SearchExportParam searchParam)
        {
            API.WebApp.ReportCommonDataApi apiReport = new(_mapApiSettiong);

            PDFReportModel model = new();

            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                ReportCommonDtoLocal dataReport = await apiReport.GetDataReport(searchParam.SerchKubunID, searchParam.searchJson, loguinUser.User_ID, loguinUser.Company_ID);
                model = await GetModelPDFReport(dataReport, HttpUtility.UrlDecode(searchParam.searchJson));
                if (model.ReportOutputItemList.Count == 0)
                {
                    throw new BadHttpRequestException(Message.NoDataOutput);
                }
                using MemoryStream memoryStream = new MemoryStream();
                using StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8);
                IOrderedEnumerable<M_Report_Output_Item_Local> listHeader = model.ReportOutputItemList
                   .OrderBy(x => x.Sort_Order);
                string header = listHeader.Select(x => "\"" + (x.Csv_Title != null && x.Csv_Title != "" ? x.Csv_Title : x.Report_Output_Item_Master.Display_Title) + "\"")
                   .Aggregate((x, y) => x + "," + y);
                writer.WriteLine(header);

                foreach (var item in model.ReportCommonList)
                {
                    string dataRow = "";
                    foreach (var headerItem in listHeader)
                    {
                        bool isFormat = headerItem.Report_Output_Item_Master.Display_Format != null && headerItem.Report_Output_Item_Master.Display_Format != "";
                        string value = item.getValueDynamic(headerItem.Report_Output_Item_Master, false, isFormat).ToString();
                        value = "\"" + value + "\"";
                        dataRow += value + ",";
                    }
                    dataRow = dataRow.TrimEnd(',');
                    writer.WriteLine(dataRow);
                }

                writer.Flush();
                memoryStream.Position = 0;
                string fileName = model.ReportSearchKubun.Report_Serch.Csv_FileName == null || model.ReportSearchKubun.Report_Serch.Csv_FileName == ""
                    ? "report" : model.ReportSearchKubun.Report_Serch.Csv_FileName;
                return File(memoryStream.ToArray(), "text/csv", fileName + ".csv");
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// PDF帳票を作成
        /// </summary>
        /// <param name="fileHtml">html文字列</param>
        /// <returns>stream形式でpdfファイル</returns>
        [HttpPost]
        [Route("Report/CreatePdf")]
        public async Task<IActionResult> CreatePdf(IFormFile fileHtml)
        {
            try
            {
                string html;
                using (StreamReader reader = new StreamReader(fileHtml.OpenReadStream()))
                {
                    html = await reader.ReadToEndAsync();
                }

                HtmlToPdfDocument doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                          ColorMode = ColorMode.Color,
                          Orientation = Orientation.Landscape,
                          PaperSize = PaperKind.A4,
                          DPI = 96,
                          Margins = new MarginSettings() { Left = 14 },
                      },
                    Objects = {
                          new ObjectSettings() {
                              HtmlContent = html,
                              WebSettings = { DefaultEncoding = "utf-8" },
                          }
                      }
                };

                byte[] pdf = _converter.Convert(doc);

                using MemoryStream stream = new MemoryStream();
                stream.Write(pdf, 0, pdf.Length);
                return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
            }
            catch (Exception x)
            {
                return Json(new { retrunFlg = false, errorMessage = x.Message });
            }
        }

        /// <summary>
        /// PDF Report Billを作成
        /// </summary>
        /// <param name="fileHtml">html文字列</param>
        /// <returns>stream形式でpdfファイル</returns>
        [HttpPost]
        [Route("Report/CreatePdfBill")]
        public async Task<IActionResult> CreatePdfBill(IFormFile fileHtml)
        {
            try
            {
                string html;
                using (StreamReader reader = new StreamReader(fileHtml.OpenReadStream()))
                {
                    html = await reader.ReadToEndAsync();
                }

                HtmlToPdfDocument doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                          ColorMode = ColorMode.Color,
                          Orientation = Orientation.Landscape,
                          PaperSize = PaperKind.A4,
                          DPI = 96,
                      },
                    Objects = {
                          new ObjectSettings() {
                              HtmlContent = html,
                              WebSettings = { DefaultEncoding = "utf-8" },
                              FooterSettings = new FooterSettings
                                {
                                    FontSize = 9,
                                    Center = "頁 [page]",
                                }
                          }
                      }
                };

                byte[] pdf = _converter.Convert(doc);

                using MemoryStream stream = new MemoryStream();
                stream.Write(pdf, 0, pdf.Length);
                return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
            }
            catch (Exception x)
            {
                return Json(new { retrunFlg = false, errorMessage = x.Message });
            }
        }

        /// <summary>
        /// PDF 帳票ポートレートを作成
        /// </summary>
        /// <param name="fileHtml">html文字列</param>
        /// <returns>stream形式でpdfファイル</returns>
        [HttpPost]
        [Route("Report/CreatePdfPortrait")]
        public async Task<IActionResult> CreatePdfPortrait(IFormFile fileHtml)
        {
            try
            {
                string html;
                using (StreamReader reader = new StreamReader(fileHtml.OpenReadStream()))
                {
                    html = await reader.ReadToEndAsync();
                }

                HtmlToPdfDocument doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                          ColorMode = ColorMode.Color,
                          Orientation = Orientation.Portrait,
                          PaperSize = PaperKind.A4,
                          DPI = 96,
                          //Margins = new MarginSettings() { Top = 10, Bottom = 10 },
                      },
                    Objects = {
                          new ObjectSettings() {
                              HtmlContent = html,
                              WebSettings = { DefaultEncoding = "utf-8" },
                          }
                      }
                };

                byte[] pdf = _converter.Convert(doc);

                using MemoryStream stream = new MemoryStream();
                stream.Write(pdf, 0, pdf.Length);
                return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
            }
            catch (Exception x)
            {
                return Json(new { retrunFlg = false, errorMessage = x.Message });
            }
        }

        /// <summary>
        /// PDF帳票を作成
        /// </summary>
        /// <param name="fileHtml">html文字列</param>
        /// <returns>stream形式でpdfファイル</returns>
        [HttpPost]
        [Route("Report/CreatePdfOrderTag")]
        public async Task<IActionResult> CreatePdfOrderTag(IFormFile fileHtml)
        {
            try
            {
                string html;
                using (StreamReader reader = new StreamReader(fileHtml.OpenReadStream()))
                {
                    html = await reader.ReadToEndAsync();
                }

                HtmlToPdfDocument doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                          ColorMode = ColorMode.Color,
                          Orientation = Orientation.Landscape,
                          PaperSize = PaperKind.A4,
                          DPI = 96,
                          Margins = new MarginSettings() { Left = 20, Bottom = 20, Right = 15, Top = 20},
                      },
                    Objects = {
                          new ObjectSettings() {
                              HtmlContent = html,
                              WebSettings = { DefaultEncoding = "utf-8" },
                          }
                      }
                };

                byte[] pdf = _converter.Convert(doc);

                using MemoryStream stream = new MemoryStream();
                stream.Write(pdf, 0, pdf.Length);
                return File(stream.ToArray(), "application/pdf", "GeneratedDocument.pdf");
            }
            catch (Exception x)
            {
                return Json(new { retrunFlg = false, errorMessage = x.Message });
            }
        }

        /// <summary>
        /// ReportHtmlでテンプレートcshtmlを変換
        /// </summary>
        /// <param name="ReportHtml">帳票Htmlフィールド</param>
        /// <returns>pdfテンプレート名</returns>
        private static string ConvertReportHtml(string ReportHtml)
        {
            string result = "";
            for (int i = 0; i < ReportHtml.Length; i++)
            {
                if (i == 0)
                {
                    result += ReportHtml[i].ToString().ToUpper();
                }
                else if (ReportHtml[i] == '-')
                {
                    i++;
                    result += ReportHtml[i].ToString().ToUpper();
                }
                else
                {
                    result += ReportHtml[i].ToString();
                }
            }
            return "PDFReport" + result;
        }

        /// <summary>
        ///モデルPDF帳票を取得
        /// </summary>
        /// <param name="dataReport">帳票データ</param>
        /// <returns>モデルPDF帳票</returns>
        private async Task<PDFReportModel> GetModelPDFReport(ReportCommonDtoLocal dataReport, string searchJson)
        {
            PDFReportModel model = new PDFReportModel();
            model.ReportSearchKubun = dataReport.ReportSearchKubun;
            model.ReportOutputItemList = dataReport.ReportOutputItemList
                .OrderBy(x => x.Report_Output_Item_Master.Row_Order)
                .ThenBy(x => x.Sort_Order)
                .ToList();
            model.ReportCommonList = dataReport.ReportCommonList;
            switch (model.ReportSearchKubun.Report_Html.ToLower())
            {
                case ReportHtmlType.InvoiceList:
                    model.Group_Headers = new List<GroupHeader>();
                    model.ShimeDay = dataReport?.ShimeDay;
                    foreach (var item in model.ReportOutputItemList)
                    {
                        if (model.Group_Headers.Count == 0)
                        {
                            model.Group_Headers.Add(new GroupHeader()
                            {
                                Row_Order = item.Report_Output_Item_Master.Row_Order,
                                Items = new List<M_Report_Output_Item_Local>() { item }
                            });
                        }
                        else
                        {
                            GroupHeader group = model.Group_Headers.Find(x => x.Row_Order == item.Report_Output_Item_Master.Row_Order);
                            if (group == null)
                            {
                                model.Group_Headers.Add(new GroupHeader()
                                {
                                    Row_Order = item.Report_Output_Item_Master.Row_Order,
                                    Items = new List<M_Report_Output_Item_Local>() { item }
                                });
                            }
                            else
                            {
                                group.Items.Add(item);
                            }
                        }
                    }
                    break;
                case ReportHtmlType.Bill1:

                    InvoiceProcedureParram procParram = new InvoiceProcedureParram();
                    procParram.getDataFromJson(searchJson);

                    Dto.T_Print_Seikyu_Local printSeikyu = await GetPrintSeikyu(procParram.seikyuId, procParram.checkSeikyuId);
                    IEnumerable<Dto.T_Nyukin_Local> nyukins = procParram.seikyuId != 0 ? await GetNyukinDataListBySeikyuId(procParram.seikyuId, procParram.seikyuMonth, procParram.shimeDay) : await GetNyukinDataList(new T_Check_Seikyu_Local() { Customer_Branch_ID = procParram.customerBranchID, Seikyu_Month = procParram.seikyuMonth, Shime_Day = procParram.shimeDay });
                    IEnumerable<Dto.V_SeikyuCheckDataList_Local> seikyuCheckDataList = await GetSeikyuCheckDataList(procParram.seikyuMonth.ToString("yyyyMMdd"));
                    if (printSeikyu != null)
                    {
                        seikyuCheckDataList = seikyuCheckDataList.Where(x => x.Zei_Kubun == procParram.zeiKubun && x.Shime_Day == procParram.shimeDay && x.Customer_Branch_ID == procParram.customerBranchID).ToList();
                    }

                    //billオブジェクトの初期化
                    model.Bill = new PDFBill
                    {
                        SeikyuUnchin = (int?)seikyuCheckDataList?.Sum(x => x.SeikyuUnchin) ?? 0,
                        BillList = MappingData(dataReport.ReportCommonList),
                        Nyukin = nyukins?.FirstOrDefault(),
                        PrintSeikyu = printSeikyu,
                    };
                    if (!model.Bill.BillList.Any())
                    {
                        throw new BadHttpRequestException(Message.NoDataOutput);
                    }
                    break;
                case ReportHtmlType.Bill2:
                    InvoiceProcedureParram procParram2 = new InvoiceProcedureParram();
                    procParram2.getDataFromJson(searchJson);

                    Dto.T_Print_Seikyu_Local printSeikyu2 = await GetPrintSeikyu(procParram2.seikyuId, procParram2.checkSeikyuId);

                    IEnumerable<Dto.T_Nyukin_Local> nyukins2 = procParram2.seikyuId != 0 ? await GetNyukinDataListBySeikyuId(procParram2.seikyuId, procParram2.seikyuMonth, procParram2.shimeDay) : await GetNyukinDataList(new T_Check_Seikyu_Local() { Customer_Branch_ID = procParram2.customerBranchID, Seikyu_Month = procParram2.seikyuMonth, Shime_Day = procParram2.shimeDay });
                    IEnumerable<Dto.V_SeikyuCheckDataList_Local> seikyuCheckDataList2 = await GetSeikyuCheckDataList(procParram2.seikyuMonth.ToString("yyyyMMdd"));
                    if (printSeikyu2 != null)
                    {
                        seikyuCheckDataList2 = seikyuCheckDataList2.Where(x => x.Zei_Kubun == procParram2.zeiKubun && x.Shime_Day == procParram2.shimeDay && x.Customer_Branch_ID == procParram2.customerBranchID).ToList();
                    }

                    //billオブジェクトの初期化
                    model.Bill = new PDFBill();
                    model.Bill.SeikyuUnchin = (int?)seikyuCheckDataList2?.Sum(x => x.SeikyuUnchin) ?? 0;
                    model.Bill.BillList2 = MappingDataBill2(dataReport.ReportCommonList);
                    model.Bill.Nyukin = nyukins2?.FirstOrDefault();
                    model.Bill.PrintSeikyu = printSeikyu2;
                    if (!model.Bill.BillList2.Any())
                    {
                        throw new BadHttpRequestException(Message.NoDataOutput);
                    }
                    break;
                case ReportHtmlType.Bill3:
                    InvoiceProcedureParram procParram3 = new InvoiceProcedureParram();
                    procParram3.getDataFromJson(searchJson);

                    Dto.T_Print_Seikyu_Local printSeikyu3 = await GetPrintSeikyu(procParram3.seikyuId, procParram3.checkSeikyuId);
                    IEnumerable<Dto.T_Nyukin_Local> nyukins3 = procParram3.seikyuId != 0 ? await GetNyukinDataListBySeikyuId(procParram3.seikyuId, procParram3.seikyuMonth, procParram3.shimeDay) : await GetNyukinDataList(new T_Check_Seikyu_Local() { Customer_Branch_ID = procParram3.customerBranchID, Seikyu_Month = procParram3.seikyuMonth, Shime_Day = procParram3.shimeDay });
                    IEnumerable<Dto.V_SeikyuCheckDataList_Local> seikyuCheckDataList3 = await GetSeikyuCheckDataList(procParram3.seikyuMonth.ToString("yyyyMMdd"));
                    if (printSeikyu3 != null)
                    {
                        seikyuCheckDataList3 = seikyuCheckDataList3.Where(x => x.Zei_Kubun == procParram3.zeiKubun && x.Shime_Day == procParram3.shimeDay && x.Customer_Branch_ID == procParram3.customerBranchID).ToList();
                    }

                    //billオブジェクトの初期化
                    model.Bill = new PDFBill
                    {
                        SeikyuUnchin = (int?)seikyuCheckDataList3?.Sum(x => x.SeikyuUnchin) ?? 0,
                        BillList3 = MappingDataBill3(dataReport.ReportCommonList),
                        Nyukin = nyukins3?.FirstOrDefault(),
                        PrintSeikyu = printSeikyu3,
                    };
                    if (!model.Bill.BillList3.Any())
                    {
                        throw new BadHttpRequestException(Message.NoDataOutput);
                    }
                    break;
                case ReportHtmlType.Bill4:
                    InvoiceProcedureParram procParram4 = new InvoiceProcedureParram();
                    procParram4.getDataFromJson(searchJson);

                    Dto.T_Print_Seikyu_Local printSeikyu4 = await GetPrintSeikyu(procParram4.seikyuId, procParram4.checkSeikyuId);
                    IEnumerable<Dto.T_Nyukin_Local> nyukins4 = procParram4.seikyuId != 0 ? await GetNyukinDataListBySeikyuId(procParram4.seikyuId, procParram4.seikyuMonth, procParram4.shimeDay) : await GetNyukinDataList(new T_Check_Seikyu_Local() { Customer_Branch_ID = procParram4.customerBranchID, Seikyu_Month = procParram4.seikyuMonth, Shime_Day = procParram4.shimeDay });
                    IEnumerable<Dto.V_SeikyuCheckDataList_Local> seikyuCheckDataList4 = await GetSeikyuCheckDataList(procParram4.seikyuMonth.ToString("yyyyMMdd"));
                    if (printSeikyu4 != null)
                    {
                        seikyuCheckDataList4 = seikyuCheckDataList4.Where(x => x.Zei_Kubun == printSeikyu4.Zei_Kubun && x.Shime_Day == procParram4.shimeDay && x.Customer_Branch_ID == procParram4.customerBranchID).ToList();
                    }

                    //billオブジェクトの初期化
                    model.Bill = new PDFBill
                    {
                        SeikyuUnchin = (int?)seikyuCheckDataList4?.Sum(x => x.SeikyuUnchin) ?? 0,
                        BillList4 = MappingDataBill4(dataReport.ReportCommonList),
                        Nyukin = nyukins4?.FirstOrDefault(),
                        PrintSeikyu = printSeikyu4,
                    };
                    if (!model.Bill.BillList4.Any())
                    {
                        throw new BadHttpRequestException(Message.NoDataOutput);
                    }
                    break;
                case ReportHtmlType.CarNumberContactSheetList:
                    V_ReportCommon_Local firstDictionary = model.ReportCommonList.FirstOrDefault();
                    model.CarNumberContactSheet = new PDFCarNumberContactSheet();
                    PDFCarNumberContactSheetInfo commonInfo = new PDFCarNumberContactSheetInfo();
                    if (firstDictionary != null)
                    {
                        foreach (var keyValuePair in firstDictionary)
                        {
                            string key = keyValuePair.Key;
                            object value = keyValuePair.Value;

                            PropertyInfo propertyInfo = model.CarNumberContactSheet.CommonInfo.GetType().GetProperty(key);

                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(model.CarNumberContactSheet.CommonInfo, value);
                            }
                        }
                    }
                    model.CarNumberContactSheet.CarNumberContactSheetList = MappingDataNumberContactSheet(model.ReportCommonList);

                    break;
                case ReportHtmlType.OrderTag:
                    model.OrderTag = new PDFOrderTag();
                    model.OrderTag.HaisyaDataList = MappingDataOrderTag(dataReport.ReportCommonList);
                    break;
                case ReportHtmlType.TransportOrderSheet:
                    model.TransportOrderSheet = new PDFTransportOrderSheet();
                    model.TransportOrderSheet.TransportOrderSheetList = MappingDataTransportOrderSheet(model.ReportCommonList);
                    break;
                case ReportHtmlType.TransportInstructionsSheet:
                    model.TransportInstructionsSheet = new PDFTransportInstructionsSheet();
                    model.TransportInstructionsSheet.base64Image = ConvertImgToBase64(ReportConstants.TransportInstructionsSheetLogo);
                    model.TransportInstructionsSheet.TransportInstructionsSheetList = MappingDataTransportInstructionsSheet(model.ReportCommonList);
                    break;
            }
            return model;
        }

        /// <summary>
        ///共通帳票データと車番連絡表をマッピング
        /// </summary>
        /// <param name="reportCommonList">共通帳票のデータリスト</param>
        /// <returns>車番連絡表のデータ</returns>
        private static List<V_ReportCarNumberContactSheetList_Local> MappingDataNumberContactSheet(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportCarNumberContactSheetList_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportCarNumberContactSheetList_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データとorder tagリストをマッピング
        /// </summary>
        /// <param name="reportCommonList">共通帳票のデータリスト</param>
        /// <returns>Order tagのデータリスト</returns>
        private static List<V_ReportOrderTagList_Local> MappingDataOrderTag(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportOrderTagList_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportOrderTagList_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データと請求書リストをマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書のデータリスト</returns>
        private static List<V_ReportBillList_Local> MappingData(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportBillList_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportBillList_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データと請求書リスト２をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト２のデータ</returns>
        private static List<V_ReportBillList2_Local> MappingDataBill2(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportBillList2_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportBillList2_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データと請order sheet リストをマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns> order sheetのデータリスト</returns>
        private static List<V_ReportTransportOrderSheetList_Local> MappingDataTransportOrderSheet(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportTransportOrderSheetList_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportTransportOrderSheetList_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データと請求書リスト３をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト３のデータ</returns>
        private static List<V_ReportBillList3_Local> MappingDataBill3(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportBillList3_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportBillList3_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データと請求書リスト４をマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <returns>請求書帳票リスト４のデータ</returns>
        private static List<V_ReportBillList4_Local> MappingDataBill4(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportBillList4_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportBillList4_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///共通帳票データとtransport instruction sheetリストをマッピング
        /// </summary>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        /// <param name="reportCommonList">請求書帳票のデータリスト</param>
        private static List<V_ReportTransportInstructionsSheetList_Local> MappingDataTransportInstructionsSheet(List<V_ReportCommon_Local> reportCommonList)
        {
            List<V_ReportTransportInstructionsSheetList_Local> a = reportCommonList.Select(x => ConvertTo<V_ReportTransportInstructionsSheetList_Local>(x)).ToList();
            return a;
        }

        /// <summary>
        ///imgをbase64形式に変換
        /// </summary>
        /// <param name="fileName">ファイル名の文字列値</param>
        /// <returns> Base64値</returns>
        private static string ConvertImgToBase64(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
            string base64Image = "";
            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                base64Image = Convert.ToBase64String(imageBytes);
            }
            return base64Image;
        }

        /// <summary>
        ///共通データを変換
        /// </summary>
        /// <typeparam name="T">データ型の変換</typeparam>
        /// <typeparam name="T">データ型の変換</typeparam>
        /// <returns>変換後のデータリスト</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static T ConvertTo<T>(Dictionary<string, object> source) where T : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            T target = new T();

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

        /// <summary>
        ///条件付きでHaisyaデータリストを取得
        /// </summary>
        /// <param name="iCcompanyID">会社のId of company</param>
        /// <param name="targetDate">日付で絞り込む</param>
        /// <param name="targetDateFrom">日付で絞り込む</param>
        /// <param name="targetDateTo">日付で絞り込む</param>
        /// <param name="iCustomerID">顧客のId</param>
        /// <returns>案件リストのデータ</returns>
        public async Task<IEnumerable<Dto.V_HaisyaDataList_Local>> GetHaisyaDataList(int iCcompanyID, string targetDate, string targetDateFrom, string targetDateTo, int iCustomerID)
        {
            try
            {
                using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                IEnumerable<Dto.V_HaisyaDataList_Local> ankenDataList = await api.GetHaisyaDataList(iCcompanyID, targetDate, targetDateFrom, targetDateTo, iCustomerID);
                return ankenDataList;
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return null;
            }
        }

        /// <summary>
        /// 出力設定Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Report/SettingReportOutput")]
        public async Task<IActionResult> SettingReportOutput(int ReportNum, int KubunID)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                SettingReportOutputModel model = await CreateModel(new SearchReportOutputModel()
                {
                    KubunID = KubunID,
                    CompanyID = loguinUser.Company_ID,
                    UserID = loguinUser.User_ID
                });

                Dto.ReportSearchData reportSearch = await GetReportSearchData(ReportNum, KubunID);

                if (reportSearch != null)
                {
                    model.DisplayTitle = reportSearch.DisplayTitle;
                    model.ReportName = reportSearch.ReportName;
                    model.Search = new() { KubunID = KubunID, CompanyID = loguinUser.Company_ID };
                }
                else
                {
                    return NotFound();
                }

                return View(model);
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        /// <summary>
        /// JSON：HTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataReportOutput(SearchReportOutputModel param)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                param.CompanyID = loguinUser.Company_ID;
                param.UserID = param.SettingType == 1 ? loguinUser.User_ID : 0;

                SettingReportOutputModel model = await CreateModel(param);

                return await PartialViewAsJson("ReportOutputItems", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// DataModelの作成＆返却
        /// </summary>
        /// <returns></returns>
        private async Task<SettingReportOutputModel> CreateModel(SearchReportOutputModel param)
        {
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            SettingReportOutputModel model = new();
            model.ReportOutputMasterItems = await GetReportOutputMasterItemLists(param.KubunID);
            model.ReportOutputItems = await GetReportOutputItemLists(param.KubunID, param.CompanyID, param.UserID);

            model.ReportOutputItems.ForEach(item =>
            {
                M_ReportOutputItemMasterDto itemMaster = model.ReportOutputMasterItems.Find(m => m.Report_Output_Item_ID == item.Report_Output_Item_ID);
                itemMaster.IsAdded = true;
                item.Display_Title = itemMaster.Display_Title;
                item.Report_ItemFlg = itemMaster.Report_ItemFlg;
                item.User_ID = param.SettingType == 1 ? loguinUser.User_ID : 0;
            });

            // 存在しない場合、マスターデータからデータ（会社で設定）を取得
            if (param.SettingType == 2 && model.ReportOutputItems.Count == 0)
            {
                model.ReportOutputMasterItems.ForEach(item =>
                {
                    if (item.Report_ItemFlg == 1)
                    {
                        item.IsAdded = true;
                        model.ReportOutputItems.Add(new()
                        {
                            Report_Serch_Kubun_ID = item.Report_Serch_Kubun_ID,
                            Report_Output_Item_ID = item.Report_Output_Item_ID,
                            Display_Title = item.Display_Title,
                            Report_ItemFlg = item.Report_ItemFlg,
                            Company_ID = loguinUser.Company_ID,
                            User_ID = 0,
                            Sort_Order = model.ReportOutputItems.Count + 1,
                            Csv_Title = "",
                        });
                    }

                });
            }

            return model;
        }

        /// <summary>
        /// 帳票出力項目を追加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddReportOutputItem(SettingReportOutputModel model)
        {
            try
            {
                M_ReportOutputItemMasterDto data = model.ReportOutputMasterItems.Find(item => item.Report_Output_Item_ID == model.ItemMasterSelected && !item.IsAdded);
                if (data != null)
                {
                    data.IsAdded = true;

                    Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                    model.ReportOutputItems.Add(new M_ReportOutputItemDto()
                    {
                        Report_Serch_Kubun_ID = data.Report_Serch_Kubun_ID,
                        Report_Output_Item_ID = data.Report_Output_Item_ID,
                        Display_Title = data.Display_Title,
                        Report_ItemFlg = data.Report_ItemFlg,
                        Company_ID = loguinUser.Company_ID,
                        User_ID = model.Search.SettingType == 1 ? loguinUser.User_ID : 0,
                        Sort_Order = model.ReportOutputItems.Count + 1,
                        Csv_Title = "",
                    });

                    model.DataUnsaved = 1;
                }

                return await PartialViewAsJson("ReportOutputItems", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 帳票出力項目を削除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteReportOutputItem(SettingReportOutputModel model)
        {
            try
            {
                M_ReportOutputItemDto data = model.ReportOutputItems.Find(item => item.Report_Output_Item_ID == model.ItemSelected);
                if (data != null)
                {
                    M_ReportOutputItemMasterDto itemMaster = model.ReportOutputMasterItems.Find(item => item.Report_Output_Item_ID == data.Report_Output_Item_ID);

                    if (itemMaster != null && itemMaster.Report_ItemFlg != 1)
                    {
                        itemMaster.IsAdded = false;
                        model.ReportOutputItems.Remove(data);
                        model.DataUnsaved = 1;
                    }
                }

                return await PartialViewAsJson("ReportOutputItems", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        ///並び順を更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateSortOrder(SettingReportOutputModel model, List<int> SortOrders)
        {
            try
            {
                model.DataUnsaved = 1;
                model.ReportOutputItems.ForEach(item =>
                {
                    item.Sort_Order = SortOrders.FindIndex(i => i == item.Report_Output_Item_ID) + 1;
                });

                model.ReportOutputItems = model.ReportOutputItems.OrderBy(item => item.Sort_Order).ToList();

                return await PartialViewAsJson("ReportOutputItems", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        ///出力項目を作成
        /// </summary>
        /// <param name="model">データを作成</param>
        /// <returns>データとエラーメッセージ</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReportOutputItem(SettingReportOutputModel model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                using MasterDataApi apiM = new(_mapApiSettiong);

                int userID = model.Search.SettingType == 1 ? loguinUser.User_ID : 0;
                int companyID = loguinUser.Company_ID;
                int sortOrder = 1;

                List<M_Report_Output_Item_Local> items = new();
                model.ReportOutputItems.ForEach(item =>
                {
                    items.Add(new()
                    {
                        Sort_Order = sortOrder++,
                        User_ID = userID,
                        Company_ID = companyID,
                        Report_Output_Item_ID = item.Report_Output_Item_ID,
                        Report_Serch_Kubun_ID = item.Report_Serch_Kubun_ID,
                        Csv_Title = item.Csv_Title,
                    });
                });

                MsterDataCommonResultValDto_Local data = await apiM.InsertReportOutputItems(items, companyID, userID);

                return Json(new { data, errorMessage = data.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        ///検索データ機能からデータを取得
        /// </summary>
        /// <param name="reportNum">帳票番号の値</param>
        /// <param name="reportNum">帳票番号の値</param>
        /// <returns>項目マスタの出力リスト</returns>
        private async Task<Dto.ReportSearchData> GetReportSearchData(int reportNum, int kubunID)
        {
            using MasterDataApi apiM = new(_mapApiSettiong);
            Dto.ReportSearchData data = await apiM.GetReportSearchData(reportNum, kubunID);
            return data;
        }

        /// <summary>
        ///項目リストのマスターから出力帳票を取得
        /// </summary>
        /// <param name="kubunID">Kubun idの値</param>
        /// <returns>項目マスタの出力リスト</returns>
        private async Task<List<M_ReportOutputItemMasterDto>> GetReportOutputMasterItemLists(int kubunID)
        {
            using MasterDataApi apiM = new(_mapApiSettiong);
            List<Dto.M_Report_Output_Item_Master_Local> list = await apiM.GetReportOutputItemMasterList(kubunID) ?? new();

            List<M_ReportOutputItemMasterDto> targetList = new()
            {
            };

            list.ToList().ForEach(item =>
            {
                targetList.Add(new()
                {
                    Report_Output_Item_ID = item.Report_Output_Item_ID,
                    Report_Serch_Kubun_ID = item.Report_Serch_Kubun_ID,
                    Display_Title = item.Display_Title,
                    Sort_Order = item.Sort_Order,
                    Report_ItemFlg = item.Report_ItemFlg,
                    IsAdded = false,
                });
            });
            return targetList;
        }

        /// <summary>
        ///項目リストから出力帳票を取得
        /// </summary>
        /// <param name="kubunID">Kubun idの値</param>
        /// <param name="companyID">会社idの値</param>
        /// <param name="userID">User idの値</param>
        /// <returns>出力項目のリスト</returns>
        private async Task<List<M_ReportOutputItemDto>> GetReportOutputItemLists(int kubunID, int companyID, int userID)
        {
            using MasterDataApi apiM = new(_mapApiSettiong);
            List<Dto.M_Report_Output_Item_Local> list = await apiM.GetReportOutputItemList(kubunID, companyID, userID) ?? new();

            List<M_ReportOutputItemDto> targetList = new()
            {
            };

            list.ToList().ForEach(item =>
            {
                targetList.Add(new()
                {
                    Report_Output_Item_ID = item.Report_Output_Item_ID,
                    Report_Serch_Kubun_ID = item.Report_Serch_Kubun_ID,
                    Company_ID = item.Company_ID,
                    User_ID = item.User_ID,
                    Sort_Order = item.Sort_Order,
                    Csv_Title = item.Csv_Title,
                });
            });
            return targetList;
        }

        // <summary>
        /// T_Check_Seikyuデータの返却
        /// </summary>
        /// <param name="Check_Seikyu_ID"></param>
        /// <returns></returns>
        private async Task<Dto.T_Check_Seikyu_Local> GetSeikyu(int? Check_Seikyu_ID)
        {
            Dto.T_Check_Seikyu_Local seikyuData = new T_Check_Seikyu_Local();
            try
            {
                using SeikyuDataApi api = new(_mapApiSettiong);
                seikyuData = await api.GetT_Check_Seikyu(Check_Seikyu_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSeikyu");
            }
            return seikyuData;
        }

        /// <summary>
        /// T_Print_Seikyuデータの返却
        /// </summary>
        /// <param name="Seikyu_ID">id of seikyu</param>
        /// <returns></returns>
        public async Task<Dto.T_Print_Seikyu_Local> GetPrintSeikyu(int Seikyu_ID, int CheckSeikyu_ID)
        {
            Dto.T_Print_Seikyu_Local seikyuData = new T_Print_Seikyu_Local();
            try
            {
                using SeikyuDataApi api = new(_mapApiSettiong);
                seikyuData = Seikyu_ID != 0 ? await api.GetT_PrintSeikyuBySeikyuId(Seikyu_ID) : await api.GetT_PrintSeikyu(CheckSeikyu_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSeikyu");
            }
            return seikyuData;
        }

        /// <summary>
        /// T_Nyukin_Local
        /// </summary>
        /// <param name="CheckSeikyu"></param>
        /// <returns></returns>
        private async Task<IEnumerable<T_Nyukin_Local>> GetNyukinDataList(T_Check_Seikyu_Local CheckSeikyu)
        {
            IEnumerable<T_Nyukin_Local> nyukinDataList = new List<T_Nyukin_Local>();
            try
            {
                using SeikyuDataApi api = new(_mapApiSettiong);
                nyukinDataList = await api.GetNyuukinDataList(CheckSeikyu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNyukinDataList");
            }
            return nyukinDataList;
        }

        /// <summary>
        /// T_Nyukin_Local
        /// </summary>
        /// <param name="seikyuId">id of seikyu</param>
        /// <param name="seikyuMonth">year and month of seikyu</param>
        /// <param name="shimeDay">締日</param>
        /// <returns>list data nyukin</returns>
        public async Task<IEnumerable<T_Nyukin_Local>> GetNyukinDataListBySeikyuId(int seikyuId, DateTime seikyuMonth, int shimeDay)
        {
            IEnumerable<T_Nyukin_Local> nyukinDataList = new List<T_Nyukin_Local>();
            try
            {
                using SeikyuDataApi api = new(_mapApiSettiong);
                nyukinDataList = await api.GetNyuukinDataListBySeikyuID(seikyuId, seikyuMonth, shimeDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetNyukinDataList");
            }
            return nyukinDataList;
        }

        /// <summary>
        /// JSON：案件一覧データの返却
        /// </summary>
        /// <param name="targetMonthDate"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_SeikyuCheckDataList_Local>> GetSeikyuCheckDataList(string targetDate)
        {
            SeikyuDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            // TODO
            // 請求問合わせ変更承認一覧 得意先で正しくデータ検索されていないため、ハードコードで設定
            // 0 得意先FROM
            // 9999999999999 得意先TO
            IEnumerable<Dto.V_SeikyuCheckDataList_Local> seikyuCheckDataList = await api.GetSeikyuCheckDataList(loguinUser.Company_ID, targetDate, "0", "9999999999999");
            return seikyuCheckDataList;
        }
    }
}
