namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 顧客支店支払DTOクラス
    /// </summary>
    public class CustomerBranchShitabaraiDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 売上計算DTO
        /// </summary>
        public CustomerUriageCalcShitabaraiDto uriageCalc { get; set; }

        /// <summary>
        /// 請求担当DTO
        /// </summary>
        public SeikyuTantouShitabaraiDto seikyuTantou { get; set; }

        /// <summary>
        /// 支払担当DTO
        /// </summary>
        public ShiharaiTantouShitabaraiDto shiharaiTantou { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
