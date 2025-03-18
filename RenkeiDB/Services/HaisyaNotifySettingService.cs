using Microsoft.AspNetCore.Http;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 配車通知設定サービスを提供します。
    /// </summary>
    public class HaisyaNotifySettingService : IHaisyaNotifySettingService
    {
        private readonly ICompanyUserRepository _companyUserRepository;
        public HaisyaNotifySettingService(ICompanyUserRepository companyUserRepository)
        {
            _companyUserRepository = companyUserRepository;
        }

        /// <summary>
        /// 設定（配車通知）
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="notifyType">通知タイプ（1:案件単位で配車が確定次第通知する、0:すべての依頼案件の配車が確定次第通知する）</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> UpdateDataAsync(int userId, int notifyType)
        {
            M_CompanyUser user = _companyUserRepository.FindByCondition(u => u.User_ID == userId).FirstOrDefault();

            user.Haisya_Send_Kubun = notifyType;
            user.Update_User = userId;
            user.Update_Datetime = DateTime.Now;

            await _companyUserRepository.UpdateAsync(user);

            return new ApiResponse() { Code = StatusCodes.Status200OK };
        }
    }
}
