using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 下払い問い合わせ修正リストモデル
    /// </summary>
    public class ShitabaraiInquiryModifyListModel
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
            public SearchDataList Search { get; set; }

            /// <summary>
            /// 検索結果
            /// </summary>
            public List<ResultDataList> Result { get; set; }

            /// <summary> 
            /// ソート順
            /// </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 検索データリスト
        /// </summary>
        public class SearchDataList : CommonModel
        {
            public bool RangeSearch { get; set; }

            /// <summary>
            /// 締め日
            /// </summary>
            [Required]
            [Display(Name = "締め日")]
            public int SelectShimebiInt { set; get; }

            [Display(Name = "下払担当")]
            public string SelectShitabaraiTantou { set; get; }

            /// <summary>
            /// 税区分(0：課税、1：非課税)
            /// </summary>
            [Required]
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; }

            /// <summary>
            /// 発行区分(1：WEB、2：帳票)
            /// </summary>
            [Required]
            [Display(Name = "発行区分")]
            public int SelectHakkouKubun { set; get; }

            /// <summary>
            /// 金額変更(0：なし、1：あり)
            /// </summary>
            [Required]
            [Display(Name = "金額変更")]
            public int SelectKingakuHenkou { set; get; }

            /// <summary>
            /// 傭車先（得意先1）
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public int? Customer1 { get; set; }

            /// <summary>
            /// 傭車先（得意先1）
            /// </summary>
            public string Customer_name1 { get; set; }

            /// <summary>
            /// 傭車先（得意先2）
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public int? Customer2 { get; set; }

            /// <summary>
            /// 傭車先（得意先2）
            /// </summary>
            public string Customer_name2 { get; set; }
        }

        /// <summary>
        /// 下払いチェックデータリスト
        /// </summary>
        public class ResultDataList : Dto.V_ShitabaraiCheckDataList_Local
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list">下払いチェックデータリスト</param>
            public ResultDataList(Dto.V_ShitabaraiCheckDataList_Local list) : base(list)
            {
            }
        }
    }
}
