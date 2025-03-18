namespace SeikyuWeb.Dto.Contractor
{
    /// <summary>
    /// 会社ユーザーDTO
    /// </summary>
    public class CompanyUserDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 従業員番号
        /// </summary>
        public int? employeeNumber { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string diaplayName { get; set; }

        /// <summary>
        /// 会社情報
        /// </summary>
        public CompanyDto company { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}