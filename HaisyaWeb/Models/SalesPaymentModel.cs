using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 売上・支払モデル
    /// </summary>
    public class SalesPaymentModel 
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int Company_ID { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForSalesPaymentList Search { get; set; }

            /// <summary>
            /// 配車データ
            /// </summary>
            public List<HaisyaDataList> DataDataLists { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }

            /// <summary>
            /// 売上区分
            /// </summary>
            public int UriageKubun { set; get; }
            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 売上・支払リスト検索モデル
        /// </summary>
        public class SearchModelForSalesPaymentList : CommonModel
        {
            public bool RangeSearch { get; set; }
            public bool ExpandSearch { get; set; }

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

            /// <summary>
            /// 案件担当
            /// </summary>
            [Display(Name = "請求担当")]
            public int TantouID { get; set; }

            /// <summary>
            /// 担当者
            /// </summary>
            public string TantouName { get; set; }

            /// <summary>
            /// 選択請求担当一覧
            /// </summary>
            public IEnumerable<SelectListItem> SelectSeikyuTantouList { set; get; }

            /// <summary>
            /// 選択請求担当
            /// </summary>
            [Required]
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }

            /// <summary>
            /// 乗務員
            /// </summary>
            [Required]
            [Display(Name = "乗務員")]
            public string JyoumuinString { get; set; }

            /// <summary>
            /// 乗務員名
            /// </summary>
            public string JyoumuinName { get; set; }

            /// <summary>
            /// 乗務員ID
            /// </summary>
            public string JyoumuinID { get; set; }

            /// <summary>
            /// 選択ステータス
            /// </summary>
            public int[] SelectStatus { get; set; }
        }

        /// <summary>
        /// 配車データ
        /// </summary>
        public class HaisyaDataList : Dto.V_UriageDataList
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
            public HaisyaDataList(Dto.V_UriageDataList list)
            {
                // 親クラスのプロパティ情報を一気に取得して使用する。
                List<PropertyInfo> props = list
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                    .ToList();

                props.ForEach(prop =>
                {
                    object propValue = prop.GetValue(list);
                    typeof(Dto.V_UriageDataList).GetProperty(prop.Name).SetValue(this, propValue);
                });
            }
        }

    }
}
