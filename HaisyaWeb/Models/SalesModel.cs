using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 売上モデル
    /// </summary>
    public class SalesModel
    {
        /// <summary>
        /// 負担選択DTO
        /// </summary>
        public class SelectBurdenDto
        {
            public List<Dto.T_Uriage_Futan_Local> AnkenLuggageList { get; set; }

            public List<Dto.M_Burden_Local> BurdenList { get; set; }

            public List<Dto.M_Burden_Group_Local> BurdenGroupList { get; set; }

            public int CompanyID { set; get; }

            public int UserID { set; get; }

            public int AnkenID { set; get; }
        }

        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForSalesList Search { get; set; }

            /// <summary>
            /// 案件データリスト
            /// </summary>
            public List<AnkenDataList> AnkenDataLists { get; set; }

            /// <summary>
            /// 得意先リスト
            /// </summary>
            public List<TokuisakiList> TokuisakiLists { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }
        }

        /// <summary>
        /// 売上リスト検索モデル
        /// </summary>
        public class SearchModelForSalesList : CommonModel
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int CompanyID { set; get; }

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
            /// 車輛
            /// </summary>
            [Display(Name = "車輛")]
            public int SelectSyaban { get; set; }

            /// <summary>
            /// 乗務員
            /// </summary>
            [Display(Name = "乗務員")]
            public string SelectDriverId { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SelectSyoriKubun { get; set; }
        }

        /// <summary>
        /// 案件データリスト
        /// </summary>
        public class AnkenDataList : Dto.V_AnkenDataList_Local
        {
            /// <summary>
            /// 領収書フラグ
            /// </summary>
            public bool SetReciptFlg { set; get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list">案件データリスト</param>
            public AnkenDataList(Dto.V_AnkenDataList_Local list) : base(list)
            {
            }
        }

        /// <summary>
        /// 得意先リスト
        /// </summary>
        public class TokuisakiList : Dto.M_Customer_Local
        {
            /// <summary>
            /// 選択フラグ
            /// </summary>
            public bool SelectFlg { set; get; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list">得意先リスト</param>
            public TokuisakiList(Dto.M_Customer_Local list) : base(list)
            {
            }
        }
    }
}
