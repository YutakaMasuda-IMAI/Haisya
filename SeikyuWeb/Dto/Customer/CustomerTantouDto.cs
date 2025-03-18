namespace SeikyuWeb.Dto.Customer
{
    /// <summary>
    /// 顧客担当者DTO
    /// </summary>
    public class CustomerTantouDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 部署名
        /// </summary>
        public string bushoName { get; set; }

        /// <summary>
        /// 担当者名
        /// </summary>
        public string tantouName { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int customerBranchId { get; set; }

        /// <summary>
        /// 顧客情報
        /// </summary>
        public CustomerDto customer { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}