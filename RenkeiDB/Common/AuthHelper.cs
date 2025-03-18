using System;
using System.Security.Claims;

namespace RenkeiDB.Common
{
    /// <summary>
    /// 認証ヘルパークラス
    /// </summary>
    public static class AuthHelper
    {
        /// <summary>
        /// ログインしているユーザーのIDを取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <returns>ユーザーID</returns>
        public static int RequiredUserID(this ClaimsPrincipal identity)
            => identity.RequiredInfos("UserID")[0];

        /// <summary>
        /// ログインしているユーザーの会社IDを取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <returns>会社ID</returns>
        public static int RequiredCompanyId(this ClaimsPrincipal identity)
            => identity.RequiredInfos("CompanyId")[0];

        /// <summary>
        /// ログインしているユーザーの会社IDと支店IDを取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <returns>会社IDと支店ID</returns>
        public static (int CompanyId, int BrandId) RequiredCompanyIdBranchId(this ClaimsPrincipal identity)
        {
            int[] a = identity.RequiredInfos("CompanyId", "BranchId");
            return (a[0], a[1]);
        }

        /// <summary>
        /// ログインしているユーザーの会社IDとユーザーIDを取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <returns>会社IDとユーザーID</returns>
        public static (int CompanyId, int UserId) RequiredCompanyIdUserID(this ClaimsPrincipal identity)
        {
            int[] a = identity.RequiredInfos("CompanyId", "UserID");
            return (a[0], a[1]);
        }

        /// <summary>
        /// ログインしているユーザーの会社IDと支店IDとユーザーIDを取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <returns>会社IDと支店IDとユーザーID</returns>
        public static (int UserId, int CompanyId, int BrandId) RequiredUserIdCompanyIdBranchId(this ClaimsPrincipal identity)
        {
            var a = identity.RequiredInfos("UserID", "CompanyId", "BranchId");
            return (a[0], a[1], a[2]);
        }

        /// <summary>
        /// ログインしているユーザーの情報を取得する
        /// </summary>
        /// <param name="identity">クレームプリンシパル</param>
        /// <param name="infos">取得する情報のキー</param>
        /// <returns>情報の配列</returns>
        /// <exception cref="UnauthorizedAccessException">認証されていない場合にスローされる例外</exception>
        private static int[] RequiredInfos(this ClaimsPrincipal identity, params string[] infos)
        {
            int[] a = new int[infos.Length];
            for (var i = 0; i < a.Length; i++)
            {
                string sid = identity.FindFirst(infos[i])?.Value;
                if (sid == null
                    || !int.TryParse(sid, out int id) || id < 1)
                {
                    throw new UnauthorizedAccessException();
                }
                a[i] = id;
            }
            return a;
        }
    }
}
