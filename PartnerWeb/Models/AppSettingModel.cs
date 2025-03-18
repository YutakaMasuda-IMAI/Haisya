using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerWeb.Models
{
    public class AppSettingModel
    {
    }

    public class MapApiSettings
    {
        public const string MapApiSetting = "MapApiSetting";

        public WebUriSetting WebUri { set; get; }

        public AuthAidSetting AuthAid { set; get; }

        public string EnvKubun { set; get; }

        public ApiSetting Api { set; get; }


    }

    public class WebUriSetting
    {
        public string JavaScriptAPI { set; get; }

        public string WebAPI { set; get; }
    }

    public class AuthAidSetting
    {

        public string Uid { set; get; }

        public string Pwd { set; get; }

        public string Sid { set; get; }

        public string Device_flag { set; get; }

        public string AuthCode { set; get; }
    }

    public class ApiSetting
    {
        //public string MapAPIHosts { set; get; }

        public string WebAPIHosts { set; get; }
    }



}
