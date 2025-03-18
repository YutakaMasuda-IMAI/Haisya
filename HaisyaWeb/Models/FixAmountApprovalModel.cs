using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 固定額承認モデル
    /// </summary>
    public class FixAmountApprovalModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            public List<SeikyuCheckDataList> SeikyuCheckDataLists { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForFixAmountApprovalList Search { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<SeikyuTantouList> SeikyuTantouLists { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<TokuisakiList> TokuisakiLists { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SyoriKubun { get; set; }
            
            [Required]
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 請求チェックデータリスト
        /// </summary>
        public class SeikyuCheckDataList : Dto.V_SeikyuCheckDataList_Local
        {
            public SeikyuCheckDataList() : base()
            {
            }

            public SeikyuCheckDataList(Dto.V_SeikyuCheckDataList_Local list): base(list)
            {
            }
        }

        /// <summary>
        /// 固定額承認リストの検索モデル
        /// </summary>
        public class SearchModelForFixAmountApprovalList : CommonModel
        {
            [Display(Name = "請求担当")]
            public string SelectSeikyuTantou { get; set; }

            [Required]
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; }

            [Required]
            [Display(Name = "発行区分")]
            public int SelectHakkouKubun { get; set; }

            [Required]
            [Display(Name = "金額変更")]
            public int? SelectKingakuHenkou { get; set; }

            [Display(Name = "得意先IDTo")]
            public string SelectTokuisakiIDTo { get; set; }

            [Display(Name = "得意先名To")]
            public string SelectTokuisakiNameTo { get; set; }
            
            //請求担当
            public IEnumerable<SelectListItem> SelectSeikyuTantouList { set; get; }
            //Status of Select Tokuisaki
            public bool IsSelectTokuisaki { get; set; }
        }

        /// <summary>
        /// 請求担当リスト
        /// </summary>
        public class SeikyuTantouList : Dto.M_CompanyUser_Group_Local
        {

            /// <summary>
            /// 
            /// </summary>
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

            /// <summary>
            /// 
            /// </summary>
            public bool SelectFlg { set; get; }


            public TokuisakiList(Dto.M_Customer_Local list) : base(list)
            {
            }

        }

    }
}
