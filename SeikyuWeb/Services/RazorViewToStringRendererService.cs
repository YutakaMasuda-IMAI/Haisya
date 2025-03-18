using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using SeikyuWeb.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeikyuWeb.Services
{
    /// <summary>
    /// Razorビューを文字列にレンダリングするサービス
    /// </summary>
    public class RazorViewToStringRendererService : IRazorViewToStringRendererService
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public RazorViewToStringRendererService(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// ビューを文字列にレンダリングする
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewPath">ビューのパス</param>
        /// <param name="model">モデル</param>
        /// <returns>HTML文字列</returns>
        /// <exception cref="ArgumentNullException">ビューが見つからない場合にスローされる例外</exception>
        public async Task<string> RenderViewToStringAsync<TModel>(string viewPath, TModel model)
        {
            ActionContext actionContext = new ActionContext(
                new DefaultHttpContext { RequestServices = _serviceProvider },
                new Microsoft.AspNetCore.Routing.RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            ViewEngineResult viewResult = _viewEngine.GetView(executingFilePath: null, viewPath, isMainPage: false);


            if (!viewResult.Success)
            {
                string searchedLocations = string.Join("\n", viewResult.SearchedLocations);
                throw new ArgumentNullException($"View '{viewPath}' Notfound. \nSearched at locations:\n{searchedLocations}");
            }

            ViewDataDictionary<TModel> viewDictionary = new ViewDataDictionary<TModel>(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = model
            };

            using (StringWriter sw = new StringWriter())
            {
                TempDataDictionary tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
                ViewContext viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    tempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
          
        }

        /// <summary>
        /// JavaScriptを実行した後にビューを文字列にレンダリングする
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewPath">ビューのパス</param>
        /// <param name="model">モデル</param>
        /// <returns>HTML文字列</returns>
        public async Task<string> RenderViewAfterExecuseJSToStringAsync<TModel>(string viewPath, TModel model)
        {
            string htmlView = await RenderViewToStringAsync(viewPath, model);
            return htmlView;
            // get html after js execution
            //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Chrome", "Win64-129.0.6668.100", "chrome-win64", "chrome.exe");
            //await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            //{
            //    Headless = true,
            //    ExecutablePath = path
            //});
            //await using var page = await browser.NewPageAsync();

            //await page.SetContentAsync(htmlView);
            //await page.WaitForSelectorAsync("html");

            //string containerHtml = await page.GetContentAsync();

            //return containerHtml;
        }
    }
}
