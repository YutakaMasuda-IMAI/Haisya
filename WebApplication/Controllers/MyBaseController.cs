using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    public class MyBaseController : ControllerBase
    {

        protected ApplicationDbContext _context;
        protected ApplicationDbContextKintai _contextKintai;

        protected MapApiSettings _mapApiSettings;


        /// <summary>
        /// 指定キーのMultipartFormDataContentに設定されたコンテンツを取得して返却
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetMultipartFormDataContentData<T>(string key = "name")
        {

            try {
                Microsoft.AspNetCore.Http.IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey(key)) { new Exception("name無し"); }

                bool oo = form.TryGetValue(key, out Microsoft.Extensions.Primitives.StringValues abc);
                return System.Text.Json.JsonSerializer.Deserialize<T>(abc);
            } catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
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
    }
}
