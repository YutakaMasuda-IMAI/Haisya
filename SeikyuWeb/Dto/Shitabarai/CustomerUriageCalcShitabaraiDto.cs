namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 顧客売上計算支払DTOクラス
    /// </summary>
    public class CustomerUriageCalcShitabaraiDto
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
        /// 割増1計算
        /// </summary>
        public string warimashi1Calc { get; set; }

        /// <summary>
        /// 割増2計算
        /// </summary>
        public string warimashi2Calc { get; set; }

        /// <summary>
        /// 割増3計算
        /// </summary>
        public string warimashi3Calc { get; set; }

        /// <summary>
        /// 割増4計算
        /// </summary>
        public string warimashi4Calc { get; set; }

        /// <summary>
        /// 割増5計算
        /// </summary>
        public string warimashi5Calc { get; set; }

        /// <summary>
        /// 請求合計計算
        /// </summary>
        public string seikyuTotalCalc { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
