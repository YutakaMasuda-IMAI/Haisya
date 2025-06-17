using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    public interface IViewRenderService
    {
        Task<string> Render_to_string(Controller controller, string view_name, object model, bool partial = false);
    }

    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> Render_to_string(Controller controller, string view_name, object model, bool partial = false)
        {
            ViewEngineResult view_result = _razorViewEngine.FindView(controller.ControllerContext, view_name, !partial);
            if (view_result.View == null)
            {
                throw new ArgumentNullException($"{view_name} not found!");
            }

            ViewDataDictionary data = new(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            using StringWriter sw = new();
            ViewContext viewContext = new(
                controller.ControllerContext,
                view_result.View,
                data,
                new TempDataDictionary(controller.ControllerContext.HttpContext, _tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await view_result.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }
}
