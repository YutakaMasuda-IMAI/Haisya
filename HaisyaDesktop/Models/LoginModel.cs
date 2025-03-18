using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Models
{

    public class LoginModel
    {

        /// <summary>
        /// ユーザID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Password { get; set; }

        /// <summary>
        /// パスワード保存フラグ
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsSavePassword { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        public LoginModel()
        {

            //// ' ログイン画面表示時に表示するユーザID、パスワードを取得する。
            //var objAuthLogic = LogicFactory.Create<AuthLogic>();
            //var objUserRow = objAuthLogic.GetShowLoginUser();
            //if (!objUserRow == null)
            //{
            //    this.UserId = objUserRow.login_id;
            //    this.Password = objUserRow.password;
            //    this.IsSavePassword = objUserRow.show_login;
            //}
        }

        /// <summary>
        /// 認証処理
        /// </summary>
        /// <returns>認証OKの場合、Trueを返却する。</returns>
        /// <remarks></remarks>
        public bool Authenticate()
        {

            //// ' ユーザID、パスワードによる認証処理を行う。
            //return AuthUser(UserId, Password, IsSavePassword);
            return false;
        }
    }

}
