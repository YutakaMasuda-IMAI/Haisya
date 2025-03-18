using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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


    }
}
