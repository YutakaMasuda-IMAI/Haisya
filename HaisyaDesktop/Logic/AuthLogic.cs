using HaisyaDesktop.Context;
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


namespace HaisyaDesktop.Logic
{



    public class AuthLogic
    {

        /// <summary>
        /// 認証ユーザの取得
        /// </summary>
        /// <param name="strUserId">ユーザID</param>
        /// <param name="strPasswd">パスワード</param>
        /// <param name="blnSaveUser">ユーザ情報保存指定</param>
        /// <returns>認証ユーザ</returns>
        /// <remarks></remarks>
        private delegate User GetAuthUser(string strUserId, string strPasswd, bool blnSaveUser);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks></remarks>
        protected internal AuthLogic()
        {
        }


        //private bool authUser(string strUserId, string strPasswd, bool blnSaveUser)
        //{
        //    // ' 引数を元に認証処理を行う。
        //    GetAuthUser dlgGetAuthUser = null;
        //    // ' ユーザ情報を取得し、ユーザ情報が有効な場合、
        //    // ' ユーザ保存指定の場合、コンテクストに保存する。
        //    User objUser = dlgGetAuthUser.Invoke(strUserId, strPasswd, blnSaveUser);
        //    if (!objUser == null)
        //    {
        //        if (blnSaveUser)
        //            ContextManager.Instance.SetUserOnce(objUser);
        //        return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 認証処理を行う。
        ///// </summary>
        ///// <param name="strUserId">ユーザID</param>
        ///// <param name="strPasswd">パスワード</param>
        ///// <param name="blnSaveUser">ユーザ情報保存指定</param>
        ///// <param name="blnAutoLocalLogin">自動ローカルログイン</param>
        ///// <returns>ユーザ情報</returns>
        ///// <remarks></remarks>
        //public bool Authenticate(string strUserId, string strPasswd, bool blnSaveUser = true, bool blnAutoLocalLogin = true)
        //{

        //    // ' ユーザ情報保存指定の場合、ユーザ情報のクリア
        //    if (blnSaveUser)
        //        ContextManager.Instance.ClearUserInfo();


        //}

        /// <summary>
        /// 認証ユーザの取得（WEBサービス）
        /// </summary>
        /// <param name="strUserId">ユーザID</param>
        /// <param name="strPasswd">パスワード</param>
        /// <param name="blnSaveUser">ユーザ情報保存指定</param>
        /// <returns>認証ユーザ</returns>
        /// <remarks></remarks>
        private User GetAuthUserService(string strUserId, string strPasswd, bool blnSaveUser)
        {
            return null;
        }

        /// <summary>
        /// 認証ユーザの取得（ローカル）
        /// </summary>
        /// <param name="strUserId">ユーザID</param>
        /// <param name="strPasswd">パスワード</param>
        /// <param name="blnSaveUser">ユーザ情報保存指定</param>
        /// <returns>認証ユーザ</returns>
        /// <remarks></remarks>
        private User GetAuthUserLocal(string strUserId, string strPasswd, bool blnSaveUser)
        {

            return null;
        }

        ///// <summary>
        ///// ログイン画面に表示するユーザ情報を取得する。
        ///// </summary>
        ///// <returns>ユーザ情報</returns>
        ///// <remarks></remarks>
        //public MUserRow GetShowLoginUser()
        //{
        //    return null;
        //}

        /// <summary>
        /// ログイン画面表示時に表示するユーザの更新
        /// </summary>
        /// <remarks></remarks>
        public void UpdateShowLoginUser()
        {
            //int intUserId = ContextManager.Instance.User.id;

        }


    }

}
