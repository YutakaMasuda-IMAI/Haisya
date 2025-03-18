using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Dto.Contractor;
using SeikyuWeb.Dto.Customer;
using SeikyuWeb.Infrastructure;
using SeikyuWeb.Infrastructure.Interfaces;
using SeikyuWeb.Models;
using SeikyuWeb.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeikyuWeb.Repositories
{
    public class LoginUserRepository : RepositoryBaseAsync<MLoginUser, HaisyaContext>, ILoginUserRepository
    {
        public LoginUserRepository(HaisyaContext dbContext, IUnitOfWork<HaisyaContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// ログインユーザの取得
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns>
        /// loginUser
        /// </returns>
        public async Task<MLoginUser> GetLoginUser(string loginId)
        {
            MLoginUser data = await (from loginUser in DbContext.Set<MLoginUser>()
                              where EF.Functions.Collate(loginUser.LoginId, "Latin1_General_BIN") == loginId && loginUser.DelFlg == false
                              select loginUser).FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// ログイン情報取得（業者）
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<ContractorDto> GetContractor(string loginId)
        {
            ContractorDto query = await (from loginUser in DbContext.Set<MLoginUser>()
                               join cu in DbContext.Set<MCompanyUser>()
                               on loginUser.UserId equals cu.UserId
                               join c in DbContext.Set<MCompany>()
                               on cu.CompanyId equals c.CompanyId
                               where EF.Functions.Collate(loginUser.LoginId, "Latin1_General_BIN") == loginId
                                     && loginUser.DelFlg == false
                                     && cu.DelFlg == false
                                     && c.DelFlg == false
                               select new ContractorDto
                               {
                                   id = loginUser.LoginUserId,
                                   loginId = loginId,
                                   companyUser = new SeikyuWeb.Dto.Contractor.CompanyUserDto
                                   {
                                       id = cu.UserId,
                                       employeeNumber = cu.EmployeeNumber,
                                       lastName = cu.LastName,
                                       firstName = cu.FirstName,
                                       diaplayName = cu.DisplayName,
                                       company = new SeikyuWeb.Dto.Contractor.CompanyDto
                                       {
                                           id = c.CompanyId,
                                           companyCode = c.CompanyCode,
                                           companyName = c.CompanyName
                                       }
                                   },
                                   LockFlg = loginUser.LockFlg
                               }).FirstOrDefaultAsync();

            return query;
        }

        /// <summary>
        /// ログイン情報取得（荷主）
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<LogisticsUnitDto> GetCustomer(string loginId)
        {
            LogisticsUnitDto query = await (from LUC in DbContext.Set<MLoginUserCustomer>()
                               join CU in DbContext.Set<MCustomerTantou>()
                               on LUC.TantouId equals CU.TantouId
                               join C in DbContext.Set<MCustomer>()
                               on CU.CustomerId equals C.CustomerId
                               where EF.Functions.Collate(LUC.LoginId, "Latin1_General_BIN") == loginId
                                     && LUC.DelFlg == false
                                     && CU.DelFlg == false
                                     && C.DelFlg == false
                               select new LogisticsUnitDto
                               {
                                   id = LUC.LoginUserCustomerId,
                                   loginId = loginId,
                                   customerTantou = new SeikyuWeb.Dto.Customer.CustomerTantouDto
                                   {
                                       id = CU.TantouId,
                                       bushoName = CU.BusyoName,
                                       tantouName = CU.TantouName,
                                       customerBranchId = CU.CustomerBranchId,
                                       customer = new SeikyuWeb.Dto.Customer.CustomerDto
                                       {
                                           id = C.CustomerId,
                                           yosyaFlg = Convert.ToBoolean(C.YosyaFlg),
                                           customerName = C.CustomerName
                                       }
                                   },
                                   LockFlg = LUC.LockFlg
                               }).FirstOrDefaultAsync();
            return query;
        }
    }
}
