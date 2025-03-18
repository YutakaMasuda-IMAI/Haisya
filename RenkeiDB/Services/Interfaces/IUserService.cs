using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// ユーザーサービスインターフェース
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// ログイン処理を行います。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="password">パスワード</param>
        /// <param name="guard">ガード</param>
        /// <returns>ログインユーザーDTO</returns>
        Task<Dto.LoginUserDto> Login(string loginId, string password, int guard);

        /// <summary>
        /// ログインユーザー情報を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ユーザーDTO</returns>
        Task<Dto.UserDto> GetLoginUserInfo(string loginId);
    }
}
