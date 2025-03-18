using System;
using System.Collections.Generic;
using WebApplication.Common;
using WebApplication.Data;

namespace WebApplication.Dto
{
    /// <summary>
    /// 照会発行クラス
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    /// <typeparam name="TInquiryType">照会タイプの型</typeparam>
    public class InquiryPublish<TData, TInquiryType>
        where TData : class, new()
        where TInquiryType : Enum
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// ログインユーザーID
        /// </summary>
        public int LoginUserId { get; set; }

        /// <summary>
        /// 照会タイプ
        /// </summary>
        public TInquiryType InquiryType { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 年度末発行フラグ
        /// </summary>
        public int NendomatsuFlg { get; set; }

        /// <summary>
        /// データリスト
        /// </summary>
        public List<TData> DataList { get; set; }
    }

    /// <summary>
    /// 照会データクラス
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public class InquiryData<TData>
        where TData : class, new()
    {
        /// <summary>
        /// データ
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public DateTime Month { get; set; }

        /// <summary>
        /// 印刷日
        /// </summary>
        public DateTime PrintDate { get; set; }

        /// <summary>
        /// 印刷終了日
        /// </summary>
        public DateTime PrintToDate { get; set; }
    }

    /// <summary>
    /// 請求書発行クラス
    /// </summary>
    public class InvoicePublish : InquiryPublish<InquiryData<V_InvoiceDataList>, InquiryTypes>
    {
        public int Print_Pattern { get; set; }

    }

    /// <summary>
    /// 請求書チェック発行クラス
    /// </summary>
    public class InvoiceCheckPublish : InquiryPublish<InquiryData<V_InvoiceCheckDataList>, InquiryTypes>
    {
        public int Print_Pattern { get; set; }
    }
}