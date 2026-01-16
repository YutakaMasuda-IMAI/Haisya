using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 下払い承認モデル
    /// </summary>
    public class ShitabaraiApprovalModel
    {
        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            /// <summary>
            /// 請求明細リスト（上下）
            /// </summary>
            public List<SeikyuMeisai> SeikyuMeisaiList { get; set; }

            public List<T_Print_Shitabarai_Detail_Local> PrintShitabaraiDetailList { get; set; }

            public List<T_YosyaShiharai_Local> YosyaShiharaiList { get; set; }

            public List<T_Check_Shitabarai_Change_Local> CheckShitabaraiChangeList { get; set; }

            public List<T_Uriage_Shitabarai_Local> UriageShitabaraiList { get; set; }

            /// <summary>
            /// 集計データ
            /// </summary>
            public T_Print_Shitabarai_Local PrintShitabarai { get; set; }

            /// <summary>
            /// 入金リスト
            /// </summary>
            public List<T_Nyukin_Local> NyukinList { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForShitabaraiCheckDataList Search { get; set; }

            public SearchDataList Search2 { get; set; }

            /// <summary>
            /// 遷移前画面のデータ
            /// </summary>
            public V_ShitabaraiCheckDataList_Local ShitabaraiCheckData { get; set; }

            /// <summary>
            /// レイアウトオプション
            /// </summary>
            public List<SelectListItem> LayoutOption { get; set; }

            public T_Check_Shitabarai_Local CheckShitabarai { get; set; }
            public int Check_Shitabarai_ID { get; set; }
            public T_Check_Shitabarai_Detail_Local CheckShitabaraiDetail { get; set; }
        }

        /// <summary>
        /// 検索データリスト
        /// </summary>
        public class SearchDataList : CommonModel
        {
            public bool RangeSearch { get; set; }
            public string Customer1 { get; set; }
            public string Customer_name1 { get; set; }
            public string Customer2 { get; set; }
            public string Customer_name2 { get; set; }

            /// <summary>
            /// 締め日
            /// </summary>
            [Required]
            [Display(Name = "締め日")]
            public int SelectShimebiInt { set; get; }

            [Display(Name = "下払担当")]
            public string SelectShitabaraiTantou { set; get; }

            /// <summary>
            /// 税区分
            /// </summary>
            [Required]
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; }

            /// <summary>
            /// 発行区分
            /// </summary>
            [Required]
            [Display(Name = "発行区分")]
            public int SelectHakkouKubun { set; get; }

            /// <summary>
            /// 金額変更
            /// </summary>
            [Required]
            [Display(Name = "金額変更")]
            public int SelectKingakuHenkou { set; get; }

            /// <summary>
            /// 傭車先
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public string YousyasakiString { get; set; }
            public string YousyasakiName { get; set; }
            public string YousyasakiCode { get; set; }
            public string YousyasakiID { get; set; }

            /// <summary>
            /// 傭車先To
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public string YousyasakiStringTo { get; set; }
            public string YousyasakiNameTo { get; set; }
            public string YousyasakiCodeTo { get; set; }
            public string YousyasakiIDTo { get; set; }
        }

        public class SearchModelForShitabaraiCheckDataList : CommonModel
        {
            public int Check_Shitabarai_ID { get; set; }
            public int Print_Shitabarai_ID { get; set; }

            [Display(Name = "下払担当")]
            public string SelectShitabaraiTantou { set; get; }

            /// <summary>
            /// 税区分
            /// </summary>
            [Required]
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; }

            /// <summary>
            /// 傭車先
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public string YousyasakiString { get; set; }
            public string YousyasakiName { get; set; }
            public string YousyasakiCode { get; set; }
            public string YousyasakiID { get; set; }

            /// <summary>
            /// 傭車先To
            /// </summary>
            [Required]
            [Display(Name = "傭車先")]
            public string YousyasakiStringTo { get; set; }
            public string YousyasakiNameTo { get; set; }
            public string YousyasakiCodeTo { get; set; }
            public string YousyasakiIDTo { get; set; }

            /// <summary>
            /// 発行日
            /// </summary>
            [Display(Name = "発行日")]
            public string PublishDay { get; set; }

            /// <summary>
            /// 下払年月日（まで）
            /// </summary>
            [Display(Name = " 下払年月日（まで）")]
            public string ShitabaraiDayTo { get; set; }
        }

        /// <summary>
        /// 下払いチェックデータリスト
        /// </summary>
        public class ShitabaraiCheckData : Dto.V_ShitabaraiCheckDataList_Local
        {
            public ShitabaraiCheckData()
            {
            }

            public ShitabaraiCheckData(Dto.V_ShitabaraiCheckDataList_Local list) : base(list)
            {
            }
        }

        /// <summary>
        /// 請求明細
        /// </summary>
        public class SeikyuMeisai
        {
            /// <summary>
            /// 画面上段
            /// </summary>
            public T_Print_Shitabarai_Detail_Local Up { get; set; }

            /// <summary>
            /// 画面下段
            /// </summary>
            public T_Check_Shitabarai_Change_Local Down { get; set; }

            public T_Check_Shitabarai_Detail_Local CheckShitabaraiDetail { get; set; }
        }

        public class IndexModalModel
        {
            public int Check_Shitabarai_ID { get; set; }
            public int Uriage_Shiharai_ID { get; set; }
            public int? Change_Flg { get; set; }
            public int Check_Kubun { get; set; }
            public IEnumerable<SelectListItem> UnitDto { set; get; }

            public List<SelectListItem> ZeiKubunOptions { get; set; } = new()
            {
                new() { Value = "0", Text = "課税" },
                new() { Value = "1", Text = "非課税" }
            };

            public DateOnly DisplayDate { get; set; }

            public string Remarks { get; set; }

            public string Syaban { get; set; }
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
            public decimal ShiharaiPrice { get; set; }
            public int PreviousUnit { get; set; }
            public decimal PreviousUnitPrice { get; set; }
            public decimal PreviousCalcPrice { get; set; }
            public decimal PreviousTatekaekin { get; set; }
            public decimal PreviousWarimashi1 { get; set; }
            public decimal PreviousWarimashi2 { get; set; }
            public decimal PreviousWarimashi3 { get; set; }
            public decimal PreviousWarimashi4 { get; set; }
            public decimal PreviousWarimashi5 { get; set; }
            public decimal PreviousSeikyuTotal { get; set; }
        }
    }
}
