using System;
using System.Collections.Generic;
using WebApplication.Data;

namespace WebApplication.Dto
{
    /// <summary>
    /// 支払照会発行DTO
    /// </summary>
    public class ShitabaraiInquiryPublishDto : ShitabaraiInquiryPublishDto<ShitabaraiInquiryData> { }

    /// <summary>
    /// 支払照会発行DTO（ジェネリック）
    /// </summary>
    /// <typeparam name="ShitabaraiInquiryDataType">支払照会データの型</typeparam>
    public class ShitabaraiInquiryPublishDto<ShitabaraiInquiryDataType>
        where ShitabaraiInquiryDataType : class
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 会社ID
        /// </summary>
        public int companyID { get; set; }

        /// <summary>
        /// ログインユーザーID
        /// </summary>
        public int loginUserId { get; set; }

        /// <summary>
        /// 照会タイプ
        /// 0:WEB一括問い合わせ
        /// 1:メール
        /// 2:印刷
        /// 3:Preview
        /// </summary>
        public int inquiryType { get; set; }

        /// <summary>
        /// 支払照会データリスト
        /// </summary>
        public List<ShitabaraiInquiryDataType> shitabaraiInquiryDataList { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public int Print_Pattern { get; set; }
    }

    /// <summary>
    /// 支払照会データ
    /// </summary>
    public class ShitabaraiInquiryData: ShitabaraiInquiryData<V_ShitabaraiCheckData> { }

    /// <summary>
    /// 支払照会データ（ジェネリック）
    /// </summary>
    /// <typeparam name="ShitabaraiCheckDataType">支払チェックデータの型</typeparam>
    public class ShitabaraiInquiryData<ShitabaraiCheckDataType>
        where ShitabaraiCheckDataType : V_ShitabaraiCheckData
    {
        /// <summary>
        /// 支払チェックデータ
        /// </summary>
        public ShitabaraiCheckDataType ShitabaraiCheckData { get; set; }

#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払月
        /// </summary>
        public DateOnly shitabaraiMonth { get; set; }

        /// <summary>
        /// 印刷日
        /// </summary>
        public DateOnly printDate { get; set; }

        /// <summary>
        /// 印刷終了日
        /// </summary>
        public DateOnly printToDate { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
