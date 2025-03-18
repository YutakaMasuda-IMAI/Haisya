using RenkeiDB.Data;
using RenkeiDB.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories.Interfaces
{
    /// <summary>
    /// ログインユーザーリポジトリのインターフェースを定義します。
    /// </summary>
    public interface ILoginUserRepository : IRepositoryBaseAsync<M_LoginUser, ApplicationDbContext>
    {
        /// <summary>
        /// 指定されたログインIDとガードに基づいてログインユーザーを取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="guard">ガード</param>
        /// <returns>ログインユーザーを含むタスク</returns>
        Task<Dto.LoginUserDto> GetLoginUser(string loginId, int guard);

        /// <summary>
        /// 指定された会社IDとユーザーIDに基づいてグループユーザーを取得します。
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>グループユーザーを含むタスク</returns>
        Task<Data.M_CompanyUser_GroupUser> GetGroupUser(int companyId, int userId);

        /// <summary>
        /// 指定されたログインIDに基づいてログインユーザー情報を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ログインユーザー情報のコレクションを含むタスク</returns>
        Task<IEnumerable<Dto.LoginUserInfoDto>> GetLoginUserInfo(string loginId);
    }
}
