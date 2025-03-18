using System;
using System.ComponentModel.DataAnnotations;
using RenkeiDB.Dto.ValidateRules;

namespace RenkeiDB.Dto.LuggageDto
{
    /// <summary>
    /// 荷物パラメータDTO
    /// </summary>
    public class LuggageParamsDto
    {
#pragma warning disable IDE1006 // Naming スタイル
        public DateTime? tumiFromDate { get; set; }
        public DateTime? tumiToDate { get; set; }
        public string tumi { get; set; }
        public string oroshi { get; set; }
        public int syasyu { get; set; }
#pragma warning restore IDE1006 // Naming スタイル
    }

    /// <summary>
    /// 荷物クエリ情報を表すクラス
    /// </summary>
    public class LuggageQuery
    {
#pragma warning disable IDE1006 // Naming スタイル
        [Display(Name = "積み開始日")]
        public string tumiFromDate { get; set; }

        [FromDateGreaterThanToDate(nameof(tumiFromDate))]
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "積み終了日")]
        public string tumiToDate { get; set; }
        
        public string tumi { get; set; }
        public string oroshi { get; set; }
        public string syasyu { get; set; }
#pragma warning restore IDE1006 // Naming スタイル
    }

    /// <summary>
    /// 荷物印刷データDTO
    /// </summary>
    public class LuggagePrintDataDto
    {
#pragma warning disable IDE1006 // Naming スタイル
        [Display(Name = "荷物公開No")]
        public string shareLuggageNo { get; set; }

        [Display(Name = "車種")]
        public string syasyuDisplay { get; set; }

        [Display(Name = "積日")]
        public string tumiDatetime { get; set; }

        [Display(Name = "積地")]
        public string tumiAddress { get; set; }

        [Display(Name = "卸地")]
        public string oroshiAddress { get; set; }

        [Display(Name = "荷主")]
        public string kokyakuName { get; set; }

        [Display(Name = "装備")]
        public string equipmentDisplay { get; set; }

        [Display(Name = "荷物重量")]
        public double luggageWeight { get; set; }

        [Display(Name = "荷物運賃")]
        public decimal unchin { get; set; }

        [Display(Name = "金額")]
        public decimal amount => unchin + tollMoney;

        [Display(Name = "通行料")]
        public decimal tollMoney { get; set; }

        [Display(Name = "グループ")]
        public string tantouGroupName { get; set; }

        [Display(Name = "荷物")]
        public string luggageDisplay { get; set; }

        [Display(Name = "高速区分")]
        public int tollKubun { get; set; }
#pragma warning restore IDE1006 // Naming スタイル
    }
}
