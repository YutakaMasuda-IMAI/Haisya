namespace SeikyuWeb.Common
{
    /// <summary>
    /// パスワードヘルパークラス
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// パスワードの暗号化
        /// </summary>
        /// <param name="password">暗号化するパスワード</param>
        /// <returns>暗号化されたパスワード</returns>
        public static string EncryptPassword(string password)
        {
            return password;
        }

        /// <summary>
        /// パスワードのハッシュ変換
        /// </summary>
        /// <param name="password">ハッシュ変換するパスワード</param>
        /// <returns>ハッシュ変換されたパスワード</returns>
        public static string HashPassword(string password)
        {
            return password;
        }
    }
}
