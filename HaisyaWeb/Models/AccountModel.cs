using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// アカウントモデルを表します。
    /// </summary>
    [AllowAnonymous]
    public class AccountModel
    {

        /// <summary>
        /// ログインID
        /// </summary>
        [Required(ErrorMessage = "ログインIDを入れてください")]
        [Display(Name = "ログインID")]
        public string LoginID { set; get; }


        /// <summary>
        /// パスワード
        /// </summary>
        [Required(ErrorMessage = "パスワードを入れてください")]
        [Display(Name = "パスワード")]
        public string Password { set; get; }

        /// <summary>
        /// 会社コード
        /// </summary>
        [Required(ErrorMessage = "会社コードを入れてください")]
        [Display(Name = "会社コード")]
        public string CompanyCode { set; get; } = "100001";

        /// <summary>
        /// 戻り画面のURL
        /// </summary>
        public string ReturnUrl { set; get; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ValidationMessage { set; get; }

        /// <summary>
        /// ログインURL
        /// </summary>
        public string LoginUrl { set; get; }
    }
}
