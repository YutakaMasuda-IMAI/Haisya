using SeikyuWeb.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.LoginDto
{
    /// <summary>
    /// ログインユーザーのリクエストDTO
    /// </summary>
    public class LoginUserReqDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ログインID
        /// </summary>
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "ログインId")]
        public string loginId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "パスワード")]
        public string password { get; set; }

        /// <summary>
        /// ガード
        /// </summary>
        [Required(ErrorMessage = Message.RequiredField)]
        [AllowedValues(DefaultValue.GuardCompany, DefaultValue.GuardCustomer)]
        [Display(Name = "guard")]
        public string guard { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
