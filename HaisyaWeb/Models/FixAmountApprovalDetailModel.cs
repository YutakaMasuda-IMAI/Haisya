using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 固定額承認詳細モデル
    /// </summary>
    public class FixAmountApprovalDetailModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            /// <summary>
            /// 受け渡し用パラメータ
            /// </summary>
            public SearchModelForFixAmountApprovalDetail Search { get; set; }

            /// <summary>
            /// 遷移前画面のデータ
            /// </summary>
            public V_SeikyuCheckDataList_Local SeikyuCheckData { get; set; }

            /// <summary>
            /// 集計データ
            /// </summary>
            public T_Print_Seikyu_Local SyuukeiData { get; set; }

            /// <summary>
            /// 入金リスト
            /// </summary>
            public List<Nyukin> NyukinList { get; set; }

            /// <summary>
            /// 請求明細リスト（上下）
            /// </summary>
            public List<SeikyuMeisai> SeikyuMeisaiList { get; set; }

            /// <summary>
            /// UriageUnchin更新用
            /// </summary>
            public List<T_Uriage_Unchin_Local> UriageUnchinList { get; set; }

            public List<M_Unit_Local> UnitList { get; set; }
            public M_Customer_Uriage_Calc_Local CustomerUriageCalc { get; set; }
            public T_Check_Seikyu_Detail_Local CheckSeikyuDetail { get; set; }
            public T_Check_Seikyu_Local CheckSeikyu { get; set; }
            public int Check_Seikyu_ID { get; set; }
            public int Print_Seikyu_ID { get; set; }
        }

        /// <summary>
        /// インデックスモーダルモデル
        /// </summary>
        public class IndexModalModel
        {
            public int Check_Seikyu_ID { get; set; }
            public int Uriage_Unchin_ID { get; set; }
            public int? Change_Flg { get; set; }
            public int Check_Kubun { get; set; }
            public IEnumerable<SelectListItem> UnitDto { set; get; }

            public List<SelectListItem> ZeiKubunOptions { get; set; } = new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = "課税" },
                    new SelectListItem { Value = "1", Text = "非課税" }
                };

            public DateTime DisplayDate { get; set; }
            public int ShimeDay { get; set; }
            public int CustomerId { get; set; }
            public string Customer_Name { get; set; }
            public string Customer_Code { get; set; }
            public string Tsumi { get; set; }
            public string Oroshi { get; set; }
            public string Luggage { get; set; }
            public int ZeiKubun { get; set; }
            public double? Qty { get; set; }
            public int? Unit { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal? CalcPrice { get; set; }
            public decimal? SeikyuUnchin { get; set; }
            public decimal? Tatekaekin { get; set; }
            public decimal? Warimashi1 { get; set; }
            public decimal? Warimashi2 { get; set; }
            public decimal? Warimashi3 { get; set; }
            public decimal? Warimashi4 { get; set; }
            public decimal? Warimashi5 { get; set; }
            public decimal? SeikyuTotal { get; set; }

            // 変更前の情報
            public double PreviousQty { get; set; }
            public int PreviousUnit { get; set; }
            public decimal PreviousUnitPrice { get; set; }
            public decimal PreviousCalcPrice { get; set; }
            public decimal PreviousSeikyuUnchin { get; set; }
            public decimal PreviousTatekaekin { get; set; }
            public decimal PreviousWarimashi1 { get; set; }
            public decimal PreviousWarimashi2 { get; set; }
            public decimal PreviousWarimashi3 { get; set; }
            public decimal PreviousWarimashi4 { get; set; }
            public decimal PreviousWarimashi5 { get; set; }
            public decimal PreviousSeikyuTotal { get; set; }
            public bool? Warimashi1_Visible { get; set; }
            public bool? Warimashi2_Visible { get; set; }
            public bool? Warimashi3_Visible { get; set; }
            public bool? Warimashi4_Visible { get; set; }
            public bool? Warimashi5_Visible { get; set; }
            public string Warimashi1_Name { get; set; }
            public string Warimashi2_Name { get; set; }
            public string Warimashi3_Name { get; set; }
            public string Warimashi4_Name { get; set; }
            public string Warimashi5_Name { get; set; }
        }

        /// <summary>
        /// コードデータDTO
        /// </summary>
        public class CodeDataDto
        {
            public string Code_Data { get; set; }
            public string Code_Name { get; set; }
        }

        /// <summary>
        /// 入金情報
        /// </summary>
        public class Nyukin : T_Nyukin_Local
        {
            /// <summary>
            /// Cash_Amount, Transfer_Amount, Draft_Amount, Unchin_Offset, General_Offset, Adjustment_Amountの合計
            /// </summary>
            public string NyukinItem { get; set; }
        }

        /// <summary>
        /// 請求明細
        /// </summary>
        public class SeikyuMeisai
        {
            /// <summary>
            /// 画面上段
            /// </summary>
            public SeikyuMeisaiUp Up { get; set; }

            /// <summary>
            /// 画面下段
            /// </summary>
            public SeikyuMeisaiDown Down { get; set; }

            public class SeikyuMeisaiUp : Dto.T_Print_Seikyu_Detail_Local
            {
                public string UnitData { get; set; }
            }

            public class SeikyuMeisaiDown : Dto.T_Check_Seikyu_Change_Local
            {
                public string UnitData { get; set; }
            }
            public T_Check_Seikyu_Detail_Local CheckDetail { get; set; }
        }

        /// <summary>
        /// 固定額承認詳細のパラメータモデル
        /// </summary>
        public class ParamModelForFixAmountApprovalDetail : V_SeikyuCheckDataList_Local
        {
            /// <summary>
            /// 一覧行モデル
            /// </summary>
            public V_SeikyuCheckDataList_Local modelData { set; get; }

            /// <summary>
            /// 検索条件モデル
            /// </summary>
            public SearchModelForFixAmountApprovalDetail searchParams { set; get; }

            public int Print_Seikyu_ID { get; set; }

        }

        /// <summary>
        /// 固定額承認詳細の検索モデル
        /// </summary>
        public class SearchModelForFixAmountApprovalDetail : CommonModel
        {
            public int Customer_ID { get; set; }
            public int Check_Seikyu_ID { get; set; }
            public int Print_Seikyu_ID { get; set; }
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
            public int SelectKingakuHenkou { get; set; }

            [Display(Name = "得意先IDTo")]
            public string SelectTokuisakiIDTo { get; set; }

            [Display(Name = "得意先名To")]
            public string SelectTokuisakiNameTo { get; set; }
            //Status of Select Tokuisaki
            public bool IsSelectTokuisaki { get; set; }
        }

    }
}
