using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApplication.Data;
using WebApplication.Dto;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 下払い問い合わせデータローカルクラス
    /// </summary>
    public class ShitabaraiInquiryData_Local : ShitabaraiInquiryData<V_ShitabaraiCheckDataList_Local> {
        public int Print_Pattern { get; set; }
    }

    /// <summary>
    /// 下払いチェックデータリストローカルクラス
    /// </summary>
    public partial class V_ShitabaraiCheckDataList_Local : V_ShitabaraiCheckData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public V_ShitabaraiCheckDataList_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public V_ShitabaraiCheckDataList_Local(V_ShitabaraiCheckDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                if (!prop.CanWrite) return;
                object propValue = prop.GetValue(list);
                typeof(V_ShitabaraiCheckDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 下払い問い合わせ発行処理DTO
    /// </summary>
    public partial class ShitabaraiCheckInquiryDto_Local : ShitabaraiInquiryPublishDto<ShitabaraiInquiryData_Local>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShitabaraiCheckInquiryDto_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dto">DTO</param>
        public ShitabaraiCheckInquiryDto_Local(Dto.ShitabaraiCheckInquiryDto_Local dto)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = dto
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                if (!prop.CanWrite) return;
                object propValue = prop.GetValue(dto);
                typeof(Dto.ShitabaraiCheckInquiryDto_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }
}
