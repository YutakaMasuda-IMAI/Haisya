using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HaisyaWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public const string SessionKeyAddress = "_objAddress";
        public const string SessionKeyRole = "_objRole";
        public const string SessionLoginUserInfo = "_objLoginUserInfo";

        public IViewRenderService _viewRenderService;

        public MapApiSettings _mapApiSettiong;

        public SignInManager<ApplicationUser> _signInManager;

        protected async Task<JsonResult> PartialViewAsJson(string viewName, bool partial = false)
            => await PartialViewAsJson(viewName, null, null, partial);

        protected async Task<JsonResult> PartialViewAsJson(string viewName, string exceptionMessage, bool partial = false)
            => await PartialViewAsJson(viewName, null, exceptionMessage, partial);

        protected async Task<JsonResult> PartialViewAsJson(string viewName, object model, bool partial = false, bool modelReturnFlg = false)
            => await PartialViewAsJson(viewName, model, null, partial, modelReturnFlg);

        protected async Task<JsonResult> PartialViewAsJson(string viewName, object model, string exceptionMessage, bool partial = false, bool modelReturnFlg = false)
        {
            ViewData.Model = model;
            string viewAsString = await _viewRenderService.RenderToStringAsync(this, viewName, model, partial);
            if (modelReturnFlg)
            {
                return Json(new { partialView = viewAsString, message = exceptionMessage, model = model });
            }
            else
            {
                return Json(new { partialView = viewAsString, message = exceptionMessage });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetErrorRequestId() => "強制エラー処理";

        /// <summary>
        /// MapApiサーバーがローカルかサーバーかを判断して返却
        /// True:サーバー；False：ローカル
        /// </summary>
        /// <returns></returns>
        protected bool GetMapsServerLocalFlg() => !_mapApiSettiong.Api.WebAPIHosts.Contains("localhost");

        protected string GetMapApiUrlPram()
        {
            if (GetMapsServerLocalFlg())
            {
                return string.Format("?key={0}&type=special", _mapApiSettiong.AuthAid.AuthCode);
            }
            return "";
        }

        protected int GetWebViewFlg() => GetMapsServerLocalFlg() ? 1 : 0;

        /// <summary>
        /// SESSION情報のクリア
        /// </summary>
        /// <remarks></remarks>
        protected void InitializeSession()
        {
            HttpContext.Session.Remove(SessionLoginUserInfo);
            HttpContext.Session.Remove(SessionKeyRole);
        }

        /// <summary>
        /// 全国の郵便情報をセッションに保持する
        /// </summary>
        /// <returns></returns>
        protected async Task<Context.AddressList> SetAddressList()
        {
            Context.AddressList addressList = new();
            addressList = HttpContext.Session.GetObject<Context.AddressList>(SessionKeyAddress);

            if (addressList == null)
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                addressList = new()
                {
                    HokkaidoAddressItem = await api.GetGetAddressList(AddressArea.Hokkaido),
                    TohokuAddressItem = await api.GetGetAddressList(AddressArea.Tohoku),
                    HokurikuAddressItem = await api.GetGetAddressList(AddressArea.Hokuriku),
                    KantoAddressItem = await api.GetGetAddressList(AddressArea.Kanto),
                    ChubuAddressItem = await api.GetGetAddressList(AddressArea.Chubu),
                    KinkiAddressItem = await api.GetGetAddressList(AddressArea.Kinki),
                    ChugokuAddressItem = await api.GetGetAddressList(AddressArea.Chugoku),
                    ShikokuAddressItem = await api.GetGetAddressList(AddressArea.Shikoku),
                    KyusyuAddressItem = await api.GetGetAddressList(AddressArea.Kyusyu),
                    OkinawaAddressItem = await api.GetGetAddressList(AddressArea.Okinawa)
                };

                HttpContext.Session.SetObject(SessionKeyAddress, addressList);

                return addressList;
            }

            return addressList;
        }

        /// <summary>
        /// ログインユーザーを返却
        /// </summary>
        /// <returns></returns>
        protected async Task<Dto.V_LoginUser_Local> GetLoginUser()
        {
            if (!HttpContext.User.Identity.IsAuthenticated) {
                
                RedirectToAction("Logout", "Account");
                throw new Common.SessionTimeOutException("ログイン情報が切れています。一度メニュー画面まで戻ってください。", Common.SystemConstants.SettionTimeOutException);
            }

            int loginUserID = int.Parse(HttpContext.User.Claims.ToList()[0].Value);

            if (HttpContext.Session.GetObject<object>(SessionLoginUserInfo) == null)
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.V_LoginUser_Local loguinUser = await api.GetV_LoginUserData(null, null, loginUserID);
                HttpContext.Session.SetObject(SessionLoginUserInfo, loguinUser);
                return loguinUser;
            }
            else
            {
                return HttpContext.Session.GetObject<Dto.V_LoginUser_Local>(SessionLoginUserInfo);
            }
        }

        /// <summary>
        /// ログインユーザーのロール情報を返却する
        /// </summary>
        /// <returns></returns>
        protected async Task<List<Dto.M_Role_Local>> GetRoleInfo(string controller = null, string action = null)
        {
            if (HttpContext.Session.GetObject<object>(SessionKeyRole) == null)
            //if (Context.ContextManager.Instance.M_RoleList == null || Context.ContextManager.Instance.M_RoleList.Count == 0)
            {
                Dto.V_LoginUser_Local user = await GetLoginUser();

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Role_Local> list = await api.GetRoleList(user.Company_ID, user.Role);
                HttpContext.Session.SetObject(SessionKeyRole, list);
            }

            List<Dto.M_Role_Local> result = new();

            if (!(HttpContext.Session.GetObject<object>(SessionKeyRole) == null || HttpContext.Session.GetObject<List<Dto.M_Role_Local>>(SessionKeyRole).Count == 0))
            {
                result = HttpContext.Session.GetObject<List<Dto.M_Role_Local>>(SessionKeyRole);
                if (controller != null) { result = result.Where(m => m.Controller == controller).ToList(); }
                if (action != null) { result = result.Where(m => m.Action == action).ToList(); }
            }

            return result;
        }

        /// <summary>
        /// プロパティーの値をコピーする
        /// </summary>
        /// <param name="toObject"></param>
        /// <param name="fromObject"></param>
        /// <param name="notExistsPropertyNames"></param>
        public static void CopyProperty(object toObject, object fromObject, string notExistsPropertyNames = null)
        {
            // コピー元、コピー先のプロパティ情報を取得
            PropertyInfo[] fromProperties = fromObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] toProperties = toObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fromProperty in fromProperties)
            {
                bool flgCopy = true;

                if (notExistsPropertyNames != null)
                {
                    foreach (string s in notExistsPropertyNames.Split(","))
                    {
                        if (s.Equals(fromProperty.Name)) { flgCopy = false; continue; }
                    }
                }

                if (flgCopy)
                {
                    if (fromProperty.PropertyType.FullName.StartsWith("System."))
                    {
                        // 名前と型が同じプロパティを取得
                        PropertyInfo target = Array.Find(toProperties, to => to.Name.Equals(fromProperty.Name)
                                                         && to.PropertyType.Equals(fromProperty.PropertyType));
                        // プロパティ値コピー
                        target?.SetValue(toObject, fromProperty.GetValue(fromObject));
                    }
                    else
                    {
                        object fromPropertySub = fromProperty.GetValue(fromObject);
                        object toPropertySub = null;
                        try
                        {
                            PropertyInfo checkFlg = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name);
                            if (checkFlg != null)
                            {
                                toPropertySub = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name).GetValue(toObject);
                            }
                        }
                        catch { }

                        if (fromPropertySub != null && toPropertySub != null)
                        {
                            CopyProperty(toPropertySub, fromPropertySub, notExistsPropertyNames);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// エラーを表示する
        /// </summary>
        /// <param name="x"></param>
        /// <returns>エラーページ</returns>
        protected virtual IActionResult Error(Exception x, string layout = "") => View("Error", new ErrorViewModel()
        {
            RequestId = x.HelpLink ?? GetErrorRequestId(),
            Message = x.Message,
            Layout = layout,
        });

        protected virtual async Task<IActionResult> JsonError(Exception x) => await PartialViewAsJson("Error", new ErrorViewModel()
        {
            RequestId = x.HelpLink ?? GetErrorRequestId(),
            Message = x.Message
        }, x.Message, true);
    }

    // セッションにオブジェクトを設定・取得する拡張メソッドを用意する
    public static class SessionExtensions
    {
        // セッションにオブジェクトを書き込む
        public static void SetObject<TObject>(this ISession session, string key, TObject obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            session.SetString(key, json);
        }

        // セッションからオブジェクトを読み込む
        public static TObject GetObject<TObject>(this ISession session, string key)
        {
            string json = session.GetString(key);
            return string.IsNullOrEmpty(json)
                ? default
                : JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}
