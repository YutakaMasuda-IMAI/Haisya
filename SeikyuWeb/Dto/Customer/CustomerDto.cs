namespace SeikyuWeb.Dto.Customer
{
    /// <summary>
    /// 顧客DTO
    /// </summary>
    public class CustomerDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 許可フラグ
        /// </summary>
        public bool yosyaFlg { get; set; }

        /// <summary>
        /// 顧客名
        /// </summary>
        public string customerName { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}