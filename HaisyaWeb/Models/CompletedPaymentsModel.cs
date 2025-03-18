using HaisyaWeb.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 完了した支払いモデル
    /// </summary>
    public class CompletedPaymentsModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            public IEnumerable<V_SeikyuZumiDataList_Local> SeikyuDataLists { get; set; }

            /// <summary>
            /// 請求入金モデル
            /// </summary>
            public class SeikyuNyukinModel {
                public List<T_Nyukin_Local> NyukinLists { get; set; }
                public List<V_SeikyuDataList_Local2> SeikyuDataLists { get; set; }
            }
            public List<T_Nyukin_Local> NyukinLists { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForFixAmountApprovalList Search { get; set; }

            /// <summary>
            /// 請求担当リスト
            /// </summary>
            public List<SeikyuTantouList> SeikyuTantouLists { get; set; }

            /// <summary>
            /// 得意先リスト
            /// </summary>
            public List<TokuisakiList> TokuisakiLists { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }
            
            [Required]
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 固定額承認リスト検索モデル
        /// </summary>
        public class SearchModelForFixAmountApprovalList : CommonModel
        {
            public bool RangeSearch { get; set; }

            [Required]
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; }

            [Required]
            [Display(Name = "発行区分")]
            public int SelectHakkouKubun { get; set; }

            [Required]
            [Display(Name = "金額変更")]
            public int SelectKingakuHenkou { get; set; }

            [Display(Name = "得意先IDTo")]
            public string SelectTokuisakiIDTo { get; set; }

            [Display(Name = "得意先名To")]
            public string SelectTokuisakiNameTo { get; set; }

            [Display(Name = "得意先名(曖昧)")]
            public string TokuisakiName { get; set; }
        }

        /// <summary>
        /// 請求担当リスト
        /// </summary>
        public class SeikyuTantouList : Dto.M_CompanyUser_Group_Local
        {
            public bool SetReciptFlg { set; get; }

            public SeikyuTantouList(Dto.M_CompanyUser_Group_Local list): base(list)
            {
            }
        }

        /// <summary>
        /// 得意先リスト
        /// </summary>
        public class TokuisakiList : Dto.M_Customer_Local
        {
            public bool SelectFlg { set; get; }

            public TokuisakiList(Dto.M_Customer_Local list) : base(list)
            {
            }
        }

        /// <summary>
        /// 支払い入力ビューモデル
        /// </summary>
        public class PaymentInputViewModel : CommonModel
        {
            public SearchModelForFixAmountApprovalList searchParams
            {
                get => new()
                {
                    SelectMonth = SelectMonth,
                    SelectShimebi = SelectShimebi,
                    SelectTantou = SelectTantou,
                    RangeSearch = RangeSearch,
                    BackMenuAction = BackMenuAction,
                    TokuisakiName = TokuisakiName,
                    SelectTokuisakiID = SelectTokuisakiID,
                    SelectTokuisakiIDTo = SelectTokuisakiIDTo,
                    SelectTokuisakiName = SelectTokuisakiName,
                    SelectTokuisakiNameTo = SelectTokuisakiNameTo,
                };
                set { }
            }

            public bool RangeSearch { get; set; }
            public int SeikyuId { get; set; }
            public string SelectSeikyuTantou { get; set; }
            public string SelectZeiKubun { get; set; }
            public string SelectHakkouKubun { get; set; }
            public string SelectTokuisakiIDTo { get; set; }
            public string SelectTokuisakiNameTo { get; set; }
            public V_SeikyuZumiDataList_Local SeikyuData { get; set; }

            public string TokuisakiName { get; set; }
        }
    }
}
