using HaisyaWeb.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// アプリケーションユーザーストアクラス
    /// </summary>
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>
    {
        private readonly MapApiSettings _mapApiSettiong;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public ApplicationUserStore(IOptions<MapApiSettings> mapApiSetting)
        {
            _mapApiSettiong = mapApiSetting.Value;
        }

        Task IUserPasswordStore<ApplicationUser>.SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserPasswordStore<ApplicationUser>.GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => new PasswordHasher<ApplicationUser>().HashPassword(user, user.Password), cancellationToken);
        }

        Task<bool> IUserPasswordStore<ApplicationUser>.HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => string.IsNullOrEmpty(user?.Password), cancellationToken);
        }

        Task<string> IUserStore<ApplicationUser>.GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.LoginUserId.ToString(), cancellationToken);
        }

        Task<string> IUserStore<ApplicationUser>.GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.UserName, cancellationToken);
        }

        Task IUserStore<ApplicationUser>.SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserStore<ApplicationUser>.GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<ApplicationUser>.SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserStore<ApplicationUser>.CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserStore<ApplicationUser>.UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserStore<ApplicationUser>.DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ApplicationUser> IUserStore<ApplicationUser>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local data2 = api.GetV_LoginUserData(null, null, int.Parse(userId)).Result;
            return ChangeObject(data2);
        }

        Task<ApplicationUser> IUserStore<ApplicationUser>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local data2 = api.GetV_LoginUserData(null, null, int.Parse(normalizedUserName)).Result;
            return ChangeObject(data2);
        }

        void IDisposable.Dispose()
        {

        }

        /// <summary>
        /// V_LoginUser_LocalをApplicationUserに変換する
        /// </summary>
        /// <param name="loginUser">ログインユーザー</param>
        /// <returns>ApplicationUser</returns>
        public Task<ApplicationUser> ChangeObject(Dto.V_LoginUser_Local loginUser)
        {
            ApplicationUserStore v = this;

            return Task.Run(() =>
            {
                ApplicationUser user = new()
                {
                    LoginUserId = loginUser.LoginUser_ID,
                    Id = loginUser.LoginID,
                    UserId = loginUser.User_ID,
                    UserName = loginUser.User_Name,
                    Password = loginUser.Password,
                    DelFlg = loginUser.Del_Flg,
                    Role = loginUser.Role,
                    CompanyID = loginUser.Company_ID,

                };

                return user;

            });


        }

    }
}