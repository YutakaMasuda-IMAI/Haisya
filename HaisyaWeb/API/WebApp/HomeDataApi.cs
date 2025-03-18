using HaisyaWeb.Common;
using HaisyaWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaisyaWeb.API.WebApp
{
    class HomeDataApi : BaseHttpClient
    {

        public HomeDataApi(MapApiSettings mapApiSetting)
        {
            _baseUrl = mapApiSetting.Api.WebAPIHosts;
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<List<Dto.T_Admin_Info_Local>> GetAdminInfos(DateTime dateTime)
        {
            string url = "HomeData/GetAdminInfos";
            url += string.Format("?dateTime={0}", dateTime.ToString("yyyy-MM-dd HH:mm"));
            return await GetHttpData<List<Dto.T_Admin_Info_Local>>(url);
        }

        public async Task<List<Dto.T_Portal_Info_Local>> GetPortalInfos(DateTime dateTime, int companyId, int userId,
                                                                SystemEnums.PortalKubun portalKubun = SystemEnums.PortalKubun.配車WEB)
        {
            string url = "HomeData/GetPortalInfos";
            url += string.Format("?dateTime={0}", dateTime.ToString("yyyy-MM-dd HH:mm"));
            url += string.Format("&companyId={0}", companyId.ToString());
            url += string.Format("&userId={0}", userId.ToString());
            url += string.Format("&portalKubun={0}", ((int)portalKubun).ToString());
            return await GetHttpData<List<Dto.T_Portal_Info_Local>>(url);
        }

        public async Task<Dto.T_Portal_Info_Local> GetPortalInfo(int infoId) {
            string url = "HomeData/GetPortalInfo?";
            url += string.Format("infoId={0}", infoId.ToString());
            return await GetHttpData<Dto.T_Portal_Info_Local>(url);
        }
    }
}
