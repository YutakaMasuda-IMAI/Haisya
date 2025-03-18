using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RenkeiDB.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// Razorビューを文字列にレンダリングするサービスを提供します。
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
        /// 指定されたビューを文字列にレンダリングします。
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewPath">ビューのパス</param>
        /// <param name="model">モデル</param>
        /// <returns>レンダリングされたビューの文字列</returns>
        public async Task<string> RenderViewToStringAsync<TModel>(string viewPath, TModel model)
        {
            ActionContext actionContext = new(
                new DefaultHttpContext { RequestServices = _serviceProvider },
                new Microsoft.AspNetCore.Routing.RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            ViewEngineResult viewResult = _viewEngine.GetView(executingFilePath: null, viewPath, isMainPage: false);


            if (!viewResult.Success)
            {
                string searchedLocations = string.Join("\n", viewResult.SearchedLocations);
                throw new ArgumentNullException($"View '{viewPath}' Notfound. \nSearched at locations:\n{searchedLocations}");
            }

            ViewDataDictionary<TModel> viewDictionary = new(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = model
            };

            using (StringWriter sw = new())
            {
                TempDataDictionary tempData = new(actionContext.HttpContext, _tempDataProvider);
                ViewContext viewContext = new(
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
    }
}
