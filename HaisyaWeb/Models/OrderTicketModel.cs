using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 受注札選択モデル
    /// </summary>
    public class OrderTicketModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForDailyReportList Search { get; set; }

            /// <summary>
            /// データリスト
            /// </summary>
            public List<HaisyaDataList> DataDataLists { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 日報リスト検索モデル
        /// </summary>
        public class SearchModelForDailyReportList : CommonModel
        {
            /// <summary>
            /// 選択タブ
            /// </summary>
            public string SelectTab { get; set; }

            /// <summary>
            /// 選択グループ
            /// </summary>
            public int SelectGroup { get; set; }

            /// <summary>
            /// 選択フィルター
            /// </summary>
            public int SelectFilter { get; set; }

            /// <summary>案件担当</summary>
            [Display(Name = "請求担当")]
            public int TantouID { get; set; }
            public string TantouName { get; set; }

            public string SelectTantou2 { get; set; }

            public IEnumerable<SelectListItem> SelectSeikyuTantouList { set; get; }

            [Required]
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }
        }

        /// <summary>
        /// 配車データリスト
        /// </summary>
        public class HaisyaDataList : V_HaisyaDataList_Local
        {
            /// <summary>
            /// 領収書フラグ
            /// </summary>
            public bool SetReciptFlg { set; get; }

            /// <summary>
            /// 日報
            /// </summary>
            public T_Nippou_Local TNippou { set; get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public HaisyaDataList() { }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list">配車データリスト</param>
            public HaisyaDataList(V_HaisyaDataList_Local list)
            {
                // 親クラスのプロパティ情報を一気に取得して使用する。
                List<PropertyInfo> props = list
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                    .ToList();

                props.ForEach(prop =>
                {
                    object propValue = prop.GetValue(list);
                    typeof(V_HaisyaDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
                });
            }
        }
    }
}
