using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class InvoiceDataApi : BaseHttpClient
    {
        public const int INVOICE_STATUS_NOT_YET = 0;        // 0：未
        public const int INVOICE_STATUS_WEB_DONE = 1;       // 1：WEB済み
        public const int INVOICE_STATUS_PUBLISH_DONE = 2;   // 2：発行済み
        private static readonly string[] INVOICE_STATUS_LABELS = { "未", "WEB済み", "発行済み" };

        public const int TAX_CATEGORY_TAX = 0;              // 0:課税
        public const int TAX_CATEGORY_FREE = 1;             // 1:非課税
        private static readonly string[] TAX_CATEGORY_LABELS = { "", "免有" };


        /// <summary>
        /// ラベルを設定するコールバック
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        private static readonly Action<V_InvoiceDataList_Local> _fill_label_callback = (V_InvoiceDataList_Local ri) =>
        {
            ri.Status_label = ri.Inquiry_Status >= 0 && ri.Inquiry_Status < INVOICE_STATUS_LABELS.Length ? INVOICE_STATUS_LABELS[ri.Inquiry_Status] : "";
        };

        public InvoiceDataApi(MapApiSettings mapApiSetting)
            => _baseUrl = mapApiSetting.Api.WebAPIHosts;

        /// <summary>
        /// 請求書の発行業務プロセス
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> PublishInvoiceDataList(InvoicePublish_Local dto) =>
            // データ更新
            await ExecHttpData(dto, string.Concat(_baseUrl, "InvoiceData/Publish"));

        /// <summary>
        /// 請求書問い合わせの発行業務
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<MsterDataCommonResultValDto_Local> PublishInvoiceCheckDataList(InvoiceCheckPublish_Local dto) =>
            // データ更新
            await ExecHttpData(dto, string.Concat(_baseUrl, "InvoiceData/PublishCheck"));

        /// <summary>
        /// 指定された条件に基づいて請求書のリストを取得する
        /// </summary
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月</param>
        /// <param name="closing_days">締日</param>
        /// <param name="invoice_person">請求担当</param>
        /// <param name="tax_category">税区分</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日</param>
        /// <param name="sale_date">売上年月日（まで）</param>
        /// <returns></returns>
        public async Task<List<V_InvoiceDataList_Local>> GetInvoiceDataList(
            int company_id,
            DateTime year_month,
            int closing_days,
            int? invoice_person,
            int? customer_id1,
            int? customer_id2,
            DateOnly publish_date,
            DateOnly? sale_date)
            => await GetDataList(company_id, year_month, closing_days, invoice_person, customer_id1, customer_id2, publish_date, sale_date,
                "InvoiceData/GetInvoiceDataList",
                _fill_label_callback);

        /// <summary>
        /// 指定された条件に基づいて請求書チェックのリストを取得する
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="year_month">年月</param>
        /// <param name="closing_days">締日</param>
        /// <param name="invoice_person">請求担当</param>
        /// <param name="customer_id1">[from]得意先コード</param>
        /// <param name="customer_id2">[to]得意先コード</param>
        /// <param name="publish_date">発行日</param>
        /// <param name="sale_date">売上年月日（まで）</param>
        /// <returns></returns>
        public Task<List<V_InvoiceCheckDataList_Local>> GetInvoiceCheckDataList(
            int company_id,
            DateTime year_month,
            int closing_days,
            int? invoice_person,
            int? customer_id1,
            int? customer_id2,
            DateOnly publish_date,
            DateOnly? sale_date)
            => GetDataList<V_InvoiceCheckDataList_Local>(company_id, year_month, closing_days, invoice_person, customer_id1, customer_id2, publish_date, sale_date,
                "InvoiceData/GetInvoiceCheckDataList",
                _fill_label_callback);

        /// <summary>
        /// 指定された条件に基づいて請求書のリストを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="company_id"></param>
        /// <param name="year_month"></param>
        /// <param name="closing_days"></param>
        /// <param name="invoice_person"></param>
        /// <param name="customer_id1"></param>
        /// <param name="customer_id2"></param>
        /// <param name="publish_date"></param>
        /// <param name="sale_date"></param>
        /// <param name="api_url"></param>
        /// <param name="callback"></param>
        /// <returns>請求書のリスト</returns>
        private async Task<List<T>> GetDataList<T>(
            int company_id,
            DateTime year_month,
            int closing_days,
            int? invoice_person,
            int? customer_id1,
            int? customer_id2,
            DateOnly publish_date,
            DateOnly? sale_date,
            string api_url,
            Action<T> callback)
            where T : V_InvoiceDataList_Local
        {
            StringBuilder b = new StringBuilder(_baseUrl).Append(api_url);
            // URLのクエリパラメータを設定
            b.AppendFormat("?company_id={0}", company_id);
            b.AppendFormat("&year_month={0:yyyy-MM-dd}", year_month);
            b.AppendFormat("&closing_days={0}", closing_days);
            // 請求担当が指定されている場合
            if (invoice_person != null && invoice_person.Value > 0)
                b.AppendFormat("&seikyu_tantou={0}", invoice_person);
            // 得意先コードが指定されている場合
            if (customer_id1 != null && customer_id1.Value > 0)
                b.AppendFormat("&customer_id1={0}", customer_id1);
            // 得意先コードが指定されている場合
            if (customer_id2 != null && customer_id2.Value > 0)
                b.AppendFormat("&customer_id2={0}", customer_id2);
            // 発行日が指定されている場合
            b.AppendFormat("&publish_date={0:yyyy-MM-dd}", publish_date);
            // 売上年月日が指定されている場合
            if (sale_date != null)
                b.AppendFormat("&sale_date={0:yyyy-MM-dd}", sale_date);
            // HTTPリクエストを送信 
            List<T> r = await GetHttpData<List<T>>(b.ToString());
            var i = 1;
            r.ForEach(ri =>
            {
                callback(ri);
                ri.Order = i++;
            });
            return r;
        }
    }
}
