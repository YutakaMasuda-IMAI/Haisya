using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.LoginDto
{
    /// <summary>
    /// ログインユーザーのレスポンスDTO
    /// </summary>
    public class LoginUserResDto
    {
        /// <summary>
        /// ログインユーザー情報
        /// </summary>
        public MLoginUser LoginUser { get; set; }

        /// <summary>
        /// ログインユーザーの顧客情報
        /// </summary>
        public MLoginUserCustomer LoginUserCustomer { get; set; }

        /// <summary>
        /// 会社ユーザー情報
        /// </summary>
        public MCompanyUser CompanyUser { get; set; }

        /// <summary>
        /// 顧客フラグ
        /// </summary>
        public bool IsCustomer { get; set; }
    }
}
