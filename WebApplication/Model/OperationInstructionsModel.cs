using System;

#nullable disable

namespace WebApplication.Model
{
    /// <summary>
    /// 操作指示
    /// </summary>
    public class OperationInstructionsModel
    {
        /// <summary>
        /// 案件 ID
        /// </summary>
        public int AnkenID { get; set; }

        /// <summary>
        /// 得意先
        /// </summary>
        public string KokyakuName { get; set; }

        /// <summary>
        /// 車種
        /// </summary>
        public string SyasyuDisplay { get; set; }

        /// <summary>
        /// 台数
        /// </summary>
        public int? Daisuu { get; set; }

        /// <summary>
        /// 荷物
        /// </summary>
        public string LuggageDisplay { get; set; }

        /// <summary>
        /// 装置
        /// </summary>
        public string EquipmentDisplay { get; set; }

        /// <summary>
        /// 出荷日
        /// </summary>
        public Date SyukkaDate { get; set; }

        /// <summary>
        /// 納品日
        /// </summary>
        public Date NohinsDate { get; set; }

        /// <summary>
        /// 出荷先
        /// </summary>
        public Place SyukkaPlace { get; set; }

        /// <summary>
        /// 納品先
        /// </summary>
        public Place NohinsPlace { get; set; }

        /// <summary>
        /// 傭車
        /// </summary>
        public string YosyaName { get; set; }

        /// <summary>
        /// 傭車運転手
        /// </summary>
        public string YosyaDriverName { get; set; }

        /// <summary>
        /// 傭車メール
        /// </summary>
        public string YosyaMailAddress { get; set; }

        /// <summary>
        /// 傭車先
        /// </summary>
        public string YosyaAddress { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyUserName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 場所
    /// </summary>
    public class Place
    {
        /// <summary>
        /// 建物名
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// アドレス
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// 日付
    /// </summary>
    public class Date
    {
        /// <summary>
        /// ポイント日付
        /// </summary>
        public DateOnly? PointDate { get; set; }

        /// <summary>
        /// ポイント時間
        /// </summary>
        public string PointTime { get; set; }

        /// <summary>
        /// ポイントステータス区分
        /// </summary>
        public int? PointStatusKubun { get; set; }
    }
}