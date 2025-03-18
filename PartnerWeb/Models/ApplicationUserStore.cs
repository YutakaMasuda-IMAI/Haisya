using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PartnerWeb.Models.DB;
using Microsoft.Extensions.Options;

namespace PartnerWeb.Models
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>
    {
        private bool disposedValue;

        private readonly MapApiSettings _mapApiSettiong;

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
            if (user == null)
            { Console.WriteLine("aaa"); }
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Task<Dto.V_LoginUser_Local> data = api.GetLoginUserList(null, null, user.LoginUserId);
            //Task<M_LoginUser> data = _context.M_LoginUsers.FirstOrDefaultAsync(u => u.LoginID == user.Id, cancellationToken);
            return Task.Run(() => new PasswordHasher<ApplicationUser>().HashPassword(user, data.Result.Password), cancellationToken);
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
            //M_LoginUser data = _context.M_LoginUsers.FirstOrDefaultAsync(u => u.LoginID == userId, cancellationToken).Result;
            //M_CompanyUser data2 = _context.M_CompanyUsers.FirstOrDefaultAsync(u => u.User_ID == data.User_ID).Result;
            if (userId == null) {
             Console.WriteLine("aaa"); }
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local data2 = api.GetLoginUserList(null, null, int.Parse(userId)).Result;
            return ChangeObject(data2);
        }

        Task<ApplicationUser> IUserStore<ApplicationUser>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //M_LoginUser data = _context.M_LoginUsers.FirstOrDefaultAsync(u => u.User_ID == int.Parse(normalizedUserName), cancellationToken).Result;
            //M_CompanyUser data2 = _context.M_CompanyUsers.FirstOrDefaultAsync(u => u.User_ID == data.User_ID).Result;
            //return data.ChangeObject(data2);
            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local data2 = api.GetLoginUserList(null, null, int.Parse(normalizedUserName)).Result;
            return ChangeObject(data2);
        }

        void IDisposable.Dispose()
        {

        }

        /// <summary>
        /// V_LoginUser_LocalをApplicationUserに変換する
        /// </summary>
        /// <returns></returns>
        public Task<ApplicationUser> ChangeObject(Dto.V_LoginUser_Local loginUser)
        {
            var v = this;

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