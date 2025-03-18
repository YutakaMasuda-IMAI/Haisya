using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 下払い問い合わせ修正リストローカルクラス
    /// </summary>
    public partial class V_ShitabaraiInquiryModifyList_Local : WebApplication.Data.V_ShitabaraiCheckDataList
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public V_ShitabaraiInquiryModifyList_Local() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">リスト</param>
        public V_ShitabaraiInquiryModifyList_Local(Dto.V_ShitabaraiInquiryModifyList_Local list)
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
                typeof(Dto.V_ShitabaraiInquiryModifyList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }
}
