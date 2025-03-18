using System.Linq;
using System.Reflection;
using WebApplication.Data;
using WebApplication.Dto;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 請求データリストビューローカルクラス
    /// </summary>
    public class V_InvoiceDataList_Local : V_InvoiceDataList
    {
        public int Order { get; set; }
        public string Status_label { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public V_InvoiceDataList_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">V_InvoiceDataList_Localオブジェクト</param>
        public V_InvoiceDataList_Local(V_InvoiceDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            System.Collections.Generic.List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(V_InvoiceDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 請求チェックデータリストビューローカルクラス
    /// </summary>
    public class V_InvoiceCheckDataList_Local : V_InvoiceDataList_Local
    {
        public int Check_Seikyu_ID { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public V_InvoiceCheckDataList_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">V_InvoiceCheckDataList_Localオブジェクト</param>
        public V_InvoiceCheckDataList_Local(V_InvoiceCheckDataList_Local list) : base(list)
            => Uriage_Unchin_ID = list.Uriage_Unchin_ID;
    }

    /// <summary>
    /// 問い合わせ公開ローカルクラス
    /// </summary>
    /// <typeparam name="TData">データ型</typeparam>
    public class InquiryPublish_Local<TData> : InquiryPublish<TData, InquiryTypes>
        where TData : class, new()
    {
    }

    /// <summary>
    /// 問い合わせデータローカルクラス
    /// </summary>
    /// <typeparam name="TData">データ型</typeparam>
    public partial class InquiryData_Local<TData> : InquiryData<TData>
        where TData : V_InvoiceDataList_Local, new()
    {
    }

    /// <summary>
    /// 請求公開ローカルクラス
    /// </summary>
    public partial class InvoicePublish_Local : InquiryPublish_Local<InquiryData_Local<V_InvoiceDataList_Local>> { 
        public int Print_Pattern { get; set; }
    }

    /// <summary>
    /// 請求チェック公開ローカルクラス
    /// </summary>
    public partial class InvoiceCheckPublish_Local : InquiryPublish_Local<InquiryData_Local<V_InvoiceCheckDataList_Local>> {
        public int Print_Pattern { get; set; }
    }
}
