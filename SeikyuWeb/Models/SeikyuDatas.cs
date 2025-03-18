using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求データに関するクラスです。
    /// </summary>
    public class SeikyuDatas
    {
    }

    /// <summary>
    /// V_SeikyuCheckDataListの取得
    /// </summary>
    public partial class V_SeikyuCheckDataList_Local : SeikyuWeb.Models.VSeikyuCheckDataList
    {
        public V_SeikyuCheckDataList_Local() { }
        public V_SeikyuCheckDataList_Local(V_SeikyuCheckDataList_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(V_SeikyuCheckDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Check_Seikyuの取得
    /// </summary>
    public partial class T_Check_Seikyu_Local : SeikyuWeb.Models.TCheckSeikyu
    {
        public T_Check_Seikyu_Local() { }

        public T_Check_Seikyu_Local(T_Check_Seikyu_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Uriage_Unchinの取得
    /// </summary>
    public partial class T_Uriage_Unchin_Local : SeikyuWeb.Models.TUriageUnchin
    {

        public T_Uriage_Unchin_Local() { }

        public T_Uriage_Unchin_Local(T_Uriage_Unchin_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(T_Uriage_Unchin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// T_Nyukinの取得
    /// </summary>
    public partial class T_Nyukin_Local : SeikyuWeb.Models.TNyukin
    {

        public T_Nyukin_Local() { }

        public T_Nyukin_Local(T_Nyukin_Local list)
        {
            // 親クラスのプロパティ情報を一気に取得して使用する。
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(T_Nyukin_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }
}
