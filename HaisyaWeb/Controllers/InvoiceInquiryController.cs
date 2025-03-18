using HaisyaWeb.API.WebApp;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.InvoiceModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 請求書問合せ発行コントローラー
    /// </summary>
    public class InvoiceInquiryController : InvoiceController
    {
        public InvoiceInquiryController(
            ILogger<InvoiceInquiryController> logger,
            SignInManager<ApplicationUser> signInManager,
            IOptions<MapApiSettings> mapApiSetting,
            IViewRenderService viewRenderService)
            : base(logger, signInManager, mapApiSetting, viewRenderService) { }

        /// <summary>
        /// 請求書チェックデータのインデックスページを表示
        /// [経理]-[請求書]-[請求書発行処理]
        /// </summary>
        /// <returns>インデックスページのビュー</returns>
        public override async Task<IActionResult> Index()
        {
            // 請求書チェックデータのインデックスページを表示
            try
            {
                return View("~/Views/Invoice/Index.cshtml", await CreateModel<V_InvoiceCheckDataList_Local>());
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 請求書チェックデータの検索結果を表示する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<IActionResult> Search(DataListModel<V_InvoiceDataList_Local> m, DataListSortModel sortParam)
        {
            // 検索条件に基づいて請求書チェックデータリストを取得
            try
            {
                IDataListModel data = await SearchData(m);
                data.SortParam = sortParam;
                return await PartialViewAsJson("../Invoice/_DataList", data);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 業務種別を設定する
        /// </summary>
        /// <param name="m"></param>
        protected override void SetBizType(InvoiceSummModel m) => m.Biz = BizTypes.INQUIRY;

        /// <summary>
        /// 請求書チェックデータを検索する
        /// </summary>
        /// <param name="m"></param>
        /// <returns>請求書チェックデータリスト</returns>
        protected override async Task<IDataListModel> SearchData(IDataListModel m)
        {
            ModelState.Clear();

            if (m.Search != null)
            {
                if (m.Search.Seikyu_Tantou_Name == null)
                    m.Search.Seikyu_Tantou_Name = 0;

                if (m.Search.Customer2 == null)
                {
                    if (m.Search.Customer1 != null)
                        m.Search.Customer2 = m.Search.Customer1;
                    else
                        m.Search.Customer2 = 99999999;
                }

                if (m.Search.Customer1 == null)
                    m.Search.Customer1 = 0;

                if (m.Search.Closing_days == null)
                    m.Search.Closing_days = 0;
            }

            TryValidateModel(m);
            if (!ModelState.IsValid)
                return m;
            SearchModelForInvoiceList s = m.Search;

            // 検索条件に基づいて請求書チェックデータリストを取得
            DataListModel<V_InvoiceCheckDataList_Local> result = new DataListModel<V_InvoiceCheckDataList_Local>()
            {
                Search = s,
                InvoiceDataLists = s.CompanyID == 0 ? new() :
                    await new InvoiceDataApi(_mapApiSettiong).GetInvoiceCheckDataList(
                        s.CompanyID,
                        new DateTime(Convert.ToInt32(s.Year_month[..4]), Convert.ToInt32(s.Year_month[5..7]), 1),
                        s.Closing_days.Value,
                        s.Seikyu_Tantou_Name,
                        s.Customer1,
                        s.Customer2,
                        ParseDate(s.Publish_date).Value,
                        null)
            };

            // Group
            var invoiceCheckDataGropuList = result.InvoiceDataLists.OrderBy(m => m.Customer_Branch_ID).GroupBy(x => new {
                x.Customer_Branch_ID
            });

            List<V_InvoiceCheckDataList_Local> invoiceCheckDataList_Local = new List<V_InvoiceCheckDataList_Local>();
            foreach (var target in invoiceCheckDataGropuList)
            {
                // 付随する運賃IDリスト
                List<int?> Uriage_Unchin_ID_List = new();
                foreach (var invoiceCheckData in target)
                {
                    if (Uriage_Unchin_ID_List.Contains(invoiceCheckData.Uriage_Unchin_ID))
                        continue;
                    Uriage_Unchin_ID_List.Add(invoiceCheckData.Uriage_Unchin_ID);
                }

                var key = target.Key;
                foreach (var invoiceCheckData in target)
                {
                    V_InvoiceCheckDataList_Local groupList = new();
                    groupList = invoiceCheckData;
                    groupList.Uriage_Unchin_ID_LIST = String.Join(",", Uriage_Unchin_ID_List);

                    invoiceCheckDataList_Local.Add(groupList);
                    break;
                }
            }
            result.InvoiceDataLists = invoiceCheckDataList_Local;

            return result;
        }

        /// <summary>
        /// 請求問合わせ変更承認一覧取得
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Dto.V_SeikyuCheckDataList_Local>> GetSeikyuCheckDataList(int iCompanyID, string targetDate)
        {
            try
            {
                using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
                // TODO
                // 請求問合わせ変更承認一覧 得意先で正しくデータ検索されていないため、ハードコードで設定
                // 0 得意先FROM
                // 9999999999999 得意先TO
                return await api.GetSeikyuCheckDataList(iCompanyID, targetDate, "0", "9999999999999");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        /// <summary>
        /// 発行処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override async Task<MsterDataCommonResultValDto_Local> Publish(InvoiceSummModel data)
        {
            // ログインユーザー取得
            _get_login_user ??= GetLoginUser();
            V_LoginUser_Local u = await _get_login_user;

            // 請求問い合わせ発行処理
            using InvoiceDataApi api = new(_mapApiSettiong);
            return await api.PublishInvoiceCheckDataList(new()
            {
                LoginUserId = u.User_ID,
                CompanyId = u.Company_ID,
                InquiryType = (InquiryTypes)data.View,
                DataList = DataList<V_InvoiceCheckDataList_Local>(data),
                Print_Pattern = data.Print_Pattern
            });
        }
    }
}