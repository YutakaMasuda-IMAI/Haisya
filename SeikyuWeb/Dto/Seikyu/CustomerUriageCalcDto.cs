using SeikyuWeb.Models;

﻿namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 顧客売上計算のDTOクラス
    /// </summary>
    public class CustomerUriageCalcDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 請求運賃名
        /// </summary>
        public string seikyuUnchinName { get; set; }
        /// <summary>
        /// 立替金名
        /// </summary>
        public string tatekaekinName { get; set; }
        /// <summary>
        /// 割増1名
        /// </summary>
        public string warimashi1Name { get; set; }
        /// <summary>
        /// 割増2名
        /// </summary>
        public string warimashi2Name { get; set; }
        /// <summary>
        /// 割増3名
        /// </summary>
        public string warimashi3Name { get; set; }
        /// <summary>
        /// 割増4名
        /// </summary>
        public string warimashi4Name { get; set; }
        /// <summary>
        /// 割増5名
        /// </summary>
        public string warimashi5Name { get; set; }
        /// <summary>
        /// 請求合計名
        /// </summary>
        public string seikyuTotalname { get; set; }
        /// <summary>
        /// 割増1表示フラグ
        /// </summary>
        public bool warimashi1Visible { get; set; }
        /// <summary>
        /// 割増2表示フラグ
        /// </summary>
        public bool warimashi2Visible { get; set; }
        /// <summary>
        /// 割増3表示フラグ
        /// </summary>
        public bool warimashi3Visible { get; set; }
        /// <summary>
        /// 割増4表示フラグ
        /// </summary>
        public bool warimashi4Visible { get; set; }
        /// <summary>
        /// 割増5表示フラグ
        /// </summary>
        public bool warimashi5Visible { get; set; }
        /// <summary>
        /// 割増1計算式
        /// </summary>
        public string warimashi1Calc { get; set; }
        /// <summary>
        /// 割増2計算式
        /// </summary>
        public string warimashi2Calc { get; set; }
        /// <summary>
        /// 割増3計算式
        /// </summary>
        public string warimashi3Calc { get; set; }
        /// <summary>
        /// 割増4計算式
        /// </summary>
        public string warimashi4Calc { get; set; }
        /// <summary>
        /// 割増5計算式
        /// </summary>
        public string warimashi5Calc { get; set; }
        /// <summary>
        /// 請求合計計算式
        /// </summary>
        public string seikyuTotalCalc { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからCustomerUriageCalcDtoを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>CustomerUriageCalcDto</returns>
        public static CustomerUriageCalcDto FromEntity(MCustomerUriageCalc entity) => new()
        {
            id = entity.CustomerBranchId,
            seikyuUnchinName = entity.SeikyuUnchinName,
            tatekaekinName = entity.TatekaekinName,
            warimashi1Name = entity.Warimashi1Name,
            warimashi2Name = entity.Warimashi2Name,
            warimashi3Name = entity.Warimashi3Name,
            warimashi4Name = entity.Warimashi4Name,
            warimashi5Name = entity.Warimashi5Name,
            seikyuTotalname = entity.SeikyuTotalName,
            warimashi1Visible = entity.Warimashi1Visible ?? false,
            warimashi2Visible = entity.Warimashi2Visible ?? false,
            warimashi3Visible = entity.Warimashi3Visible ?? false,
            warimashi4Visible = entity.Warimashi4Visible ?? false,
            warimashi5Visible = entity.Warimashi5Visible ?? false,
            warimashi1Calc = entity.Warimashi1Calc,
            warimashi2Calc = entity.Warimashi2Calc,
            warimashi3Calc = entity.Warimashi3Calc,
            warimashi4Calc = entity.Warimashi4Calc,
            warimashi5Calc = entity.Warimashi5Calc,
            seikyuTotalCalc = entity.SeikyuTotalCalc,
        };
    }
}
