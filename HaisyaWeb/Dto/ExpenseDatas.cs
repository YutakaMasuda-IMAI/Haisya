using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 経費データリスト
    /// </summary>
    public class V_ExpenseDataList_Local : WebApplication.Data.V_ExpenseDataList
    {
        public V_ExpenseDataList_Local() { }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="list">コピー元のリスト</param>
        public V_ExpenseDataList_Local(V_ExpenseDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            System.Collections.Generic.List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(V_ExpenseDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }
}
