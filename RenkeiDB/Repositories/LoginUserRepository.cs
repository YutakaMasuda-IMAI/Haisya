using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using RenkeiDB.Dto;
using System;
using System.Collections.Generic;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// ログインユーザーリポジトリ
    /// </summary>
    public class LoginUserRepository : RepositoryBaseAsync<M_LoginUser, ApplicationDbContext>, ILoginUserRepository
    {
        public LoginUserRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// ログインユーザー情報を取得
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <param name="guard">ガード</param>
        /// <returns>ログインユーザー情報</returns>
        public async Task<Dto.LoginUserDto> GetLoginUser(string loginId, int guard)
        {
            LoginUserDto data = await (from loginUser in DbContext.Set<M_LoginUser>()
                              join companyUser in DbContext.Set<M_CompanyUser>()
                              on loginUser.User_ID equals companyUser.User_ID
                              join company in DbContext.Set<M_Company>()
                              on companyUser.Company_ID equals company.Renkei_Company_ID
                              where EF.Functions.Collate(loginUser.LoginID, "Latin1_General_BIN") == loginId
                                  && loginUser.Del_Flg == false
                                  && companyUser.Del_Flg == false
                                  && company.Del_Flg == false
                              select new LoginUserDto
                              {
                                  LoginUser = loginUser,
                                  CompanyUser = companyUser,
                                  Company = company,
                              }).FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// グループユーザー情報を取得
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <returns>グループユーザー情報</returns>
        public async Task<Data.M_CompanyUser_GroupUser> GetGroupUser(int companyId, int userId)
        {
            M_CompanyUser_GroupUser data = await (from company in DbContext.Set<M_Company>()
                              join companyUserGroup in DbContext.Set<M_CompanyUser_Group>()
                              on company.Renkei_Company_ID equals companyUserGroup.Company_ID
                              join companyUserGroupUser in DbContext.Set<M_CompanyUser_GroupUser>()
                              on companyUserGroup.Group_ID equals companyUserGroupUser.Group_ID
                              where company.Renkei_Company_ID == companyId && company.Del_Flg == false && companyUserGroupUser.User_ID == userId && companyUserGroupUser.Del_Flg == false
                              select new Data.M_CompanyUser_GroupUser
                              {
                                  Group_ID = companyUserGroupUser.Group_ID,
                                  User_ID = companyUserGroupUser.User_ID,
                              }).FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// ログインユーザー情報を取得
        /// </summary>
        /// <param name="loginId">ログインID</param>
        /// <returns>ログインユーザー情報のリスト</returns>
        public async Task<IEnumerable<Dto.LoginUserInfoDto>> GetLoginUserInfo(string loginId)
        {
            List<LoginUserInfoDto> data = await (from LU in DbContext.Set<M_LoginUser>()

                            join CU in DbContext.Set<M_CompanyUser>()
                            on LU.User_ID equals CU.User_ID

                            join C in DbContext.Set<M_Company>()
                            on CU.Company_ID equals C.Renkei_Company_ID

                            join CB in DbContext.Set<M_CompanyBranch>()
                            on new { CU.Company_ID, CU.Branch_ID }
                            equals new { CB.Company_ID, CB.Branch_ID }

                            join CUGU in DbContext.Set<M_CompanyUser_GroupUser>()
                            on LU.User_ID equals CUGU.User_ID

                            join CUG1 in DbContext.Set<M_CompanyUser_Group>()
                            on new { CUGU.Group_ID, CU.Company_ID }
                            equals new { CUG1.Group_ID, CUG1.Company_ID }

                            join CUG2 in DbContext.Set<M_CompanyUser_Group>()
                            on new { CUG1.Group_Kubun, CU.Company_ID }
                            equals new { CUG2.Group_Kubun, CUG2.Company_ID }

                            where LU.LoginID == loginId && LU.Del_Flg == false && LU.Lock_Flg == false
                                && CU.Del_Flg == false
                                && C.Del_Flg == false
                                && CB.Del_Flg == false
                                && CUGU.Del_Flg == false
                                && CUG1.Del_Flg == false
                                && CUG2.Del_Flg == false

                            select new LoginUserInfoDto
                            {
                                LoginUser = LU,
                                CompanyUser = CU,
                                Company = C,
                                BranchCompany = CB,
                                CompanyUserGroup = CUG2,
                            })
                            .OrderBy(x => x.CompanyUserGroup.Group_Kubun)
                            .ThenBy(x => x.CompanyUserGroup.SortOrder)
                            .Distinct()
                            .ToListAsync();
            return data;
        }
    }
}
