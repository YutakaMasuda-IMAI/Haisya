using DriverAttendance.Service;
using PartnerWeb.Models;
using PartnerWeb.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace PartnerWeb.Controllers
{
    public class BaseController : Controller
    {
        public IViewRenderService _viewRenderService;

        public MapApiSettings _mapApiSettiong;

        //public ApplicationDbContext _context;

        public SignInManager<ApplicationUser> _signInManager;

        protected async Task<JsonResult> PartialViewAsJson(string viewName, bool partial = false)
        {
            return await PartialViewAsJson(viewName, null, null, partial);
        }

        protected async Task<JsonResult> PartialViewAsJson(string viewName, string exceptionMessage, bool partial = false)
        {
            return await PartialViewAsJson(viewName, null, exceptionMessage, partial);
        }

        protected async Task<JsonResult> PartialViewAsJson(string viewName, object model, bool partial = false, bool modelReturnFlg = false)
        {
            return await PartialViewAsJson(viewName, model, null, partial, modelReturnFlg);
        }

        protected async Task<JsonResult> PartialViewAsJson(string viewName, object model, string exceptionMessage, bool partial = false, bool modelReturnFlg = false)
        {
            ViewData.Model = model;
            var viewAsString = await _viewRenderService.RenderToStringAsync(this, viewName, model, partial);
            if (modelReturnFlg)
            {
                return Json(new { partialView = viewAsString, message = exceptionMessage, model = model });
            } else
            {
                return Json(new { partialView = viewAsString, message = exceptionMessage });
            }
            
        }

        /// <summary>
        /// ログインユーザーを返却
        /// </summary>
        /// <returns></returns>
        protected async Task<Dto.V_LoginUser_Local> GetLoginUser()
        {
            if (_signInManager.Context.User.Claims.ToList().Count == 0) { return null; }
            int loginUserID = int.Parse(_signInManager.Context.User.Claims.ToList()[0].Value);

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            Dto.V_LoginUser_Local loguinUser = await api.GetLoginUserList(null, null, loginUserID);
            return loguinUser;
        }


        /// <summary>
        /// 画面の年月パラメータを日付に変換
        /// </summary>
        /// <param name="searchMonth"></param>
        /// <returns></returns>
        protected static DateTime GetSearchMonthForDateTime(string searchMonth)
        {

            string date = searchMonth.Substring(0, 4) + "/" + searchMonth.Substring(4, 2) + "/01";

            return Convert.ToDateTime(date);

        }

        protected static WebApplication.Model.MapApiSettings MapApiSettingsCopy(MapApiSettings _mapApiSettiong)
        {
            WebApplication.Model.MapApiSettings mapApiSettings = new() { AuthAid = new(), WebUri = new() };

            CopyProperty(mapApiSettings, _mapApiSettiong);

            return mapApiSettings; 
        }

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
                    if (fromProperty.PropertyType.FullName.StartsWith("System.")) {
                        // 名前と型が同じプロパティを取得
                        PropertyInfo target = Array.Find(toProperties, to => to.Name.Equals(fromProperty.Name)
                                                         && to.PropertyType.Equals(fromProperty.PropertyType));
                        // プロパティ値コピー
                        if (target != null)
                            target.SetValue(toObject, fromProperty.GetValue(fromObject));
                    } else
                    {
                        object fromPropertySub = fromProperty.GetValue(fromObject);
                        object toPropertySub = null;
                        try
                        {
                            var checkFlg = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name);
                            if (checkFlg != null)
                            {
                                toPropertySub = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name).GetValue(toObject);
                            }
                        } catch{}
                            
                        if (fromPropertySub != null && toPropertySub != null)
                        {
                            CopyProperty(toPropertySub, fromPropertySub, notExistsPropertyNames);
                        }

                    }
                    

                    

                }

            }

        }

    }
}
