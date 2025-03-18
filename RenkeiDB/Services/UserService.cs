using RenkeiDB.Common;
using Microsoft.AspNetCore.Mvc;
using RenkeiDB.Dto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// ユーザーサービスクラス
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loginUserRepository">ログインユーザーリポジトリ</param>
        public UserService(ILoginUserRepository loginUserRepository) {
            _loginUserRepository = loginUserRepository;
        }

        /// <summary>
        /// ログイン処理を行います。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="password">パスワード</param>
        /// <param name="guard">ガード</param>
        /// <returns>ログインユーザーDTO</returns>
        public async Task<Dto.LoginUserDto> Login(string loginId, string password, int guard)
        {
            LoginUserDto loginUser = await _loginUserRepository.GetLoginUser(loginId, guard);
            if (loginUser == null)
            {
                return null;
            }
            if (password != loginUser.LoginUser.Password)
            {
                return null;
            }
            if (loginUser.Company.Owner_Flg != guard)
            {
                return null;
            }
            return loginUser;
        }

        /// <summary>
        /// ログインユーザー情報を取得します。
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ユーザーDTO</returns>
        public async Task<Dto.UserDto> GetLoginUserInfo(string loginId)
        {
            IEnumerable<LoginUserInfoDto> data = await _loginUserRepository.GetLoginUserInfo(loginId);
            if (data.Count() == 0)
            {
                return null;
            }
            GroupsLoginUserInfoDto result = data.GroupBy(d => new
            {
                d.LoginUser,
                d.CompanyUser,
                d.Company,
                d.BranchCompany
            })
            .Select(g => new GroupsLoginUserInfoDto
            {
                LoginUser = g.Key.LoginUser,
                CompanyUser = g.Key.CompanyUser,
                Company = g.Key.Company,
                BranchCompany = g.Key.BranchCompany,
                CompanyUserGroups = g.Select(d => d.CompanyUserGroup).ToList()
            }).FirstOrDefault();

            UserDto userDto = new()
            {
                Id = result.LoginUser.LoginUser_ID,
                LoginId = result.LoginUser.LoginID,
                CompanyUser = new CompanyUserDto
                {
                    Id = result.CompanyUser.User_ID,
                    LastName = result.CompanyUser.Last_Name,
                    FirstName = result.CompanyUser.First_Name,
                    DisplayName = result.CompanyUser.Display_Name,
                    Company = new CompanyDto
                    {
                        Id = result.Company.Renkei_Company_ID,
                        CompanyName = result.Company.Company_Name,
                        CompanyNameDisplay = result.Company.Company_Name_Display,
                        OwnerFlg = result.Company.Owner_Flg == 1 ? true : false
                    },
                    Branch = new BranchDto
                    {
                        Id = result.BranchCompany.Branch_ID,
                        BranchCode = result.BranchCompany.Branch_Code,
                        BranchName = result.BranchCompany.Branch_Name,
                        BranchNameAbbr = result.BranchCompany.Branch_Name_Abbr
                    },
                    Groups = result.CompanyUserGroups.Select(g => new GroupDto
                    {
                        Id = g.Group_ID,
                        GroupKubun = g.Group_Kubun,
                        GroupName = g.Group_Name,
                        DisplayName = g.Display_Name,
                        SortOrder = g.SortOrder
                    }).OrderBy(x => x.GroupKubun).ThenBy(x => x.SortOrder).ToList(),
                }
            };
            return userDto;
        }
    }
}