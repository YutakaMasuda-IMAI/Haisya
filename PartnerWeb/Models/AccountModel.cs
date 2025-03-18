using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerWeb.Models
{
    [AllowAnonymous]
    public class AccountModel
    {

        //public List<Dto.M_LoginUser_Local> UserList { set; get; }

        //public List<Dto.V_LoginUser_Local> LoginUserList { set; get; }


        [Required(ErrorMessage = "ログインIDを入れてください")]
        [Display(Name = "ログインID")]
        public string LoginID { set; get; }


        [Required(ErrorMessage = "パスワードを入れてください")]
        [Display(Name = "パスワード")]
        public string Password { set; get; }

        [Required(ErrorMessage = "会社コードを入れてください")]
        [Display(Name = "会社コード")]
        public string CompanyCode { set; get; }

        /// <summary>
        /// 戻り画面のURL
        /// </summary>
        public string ReturnUrl { set; get; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ValidationMessage { set; get; }


    }




}
