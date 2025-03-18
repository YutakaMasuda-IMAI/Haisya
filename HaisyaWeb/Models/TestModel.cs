using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Models
{
    public class TestModel
    {
        public class DataListModel
        {
            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForTestList Search { get; set; }

            public List<AnkenDataList> DataDataLists { get; set; }
        }

        public class SearchModelForTestList : CommonModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string SelectTab { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectGroup { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectFilter { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class AnkenDataList : Dto.V_AnkenDataList_Local
        {
            public AnkenDataList()
            {
            }

            public AnkenDataList(Dto.V_AnkenDataList_Local list)
            {
                // 親クラスのプロパティ情報を一気に取得して使用する。
                List<PropertyInfo> props = list
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                    .ToList();

                props.ForEach(prop =>
                {
                    object propValue = prop.GetValue(list);
                    typeof(Dto.V_AnkenDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
                });
            }
        }
    }
}
