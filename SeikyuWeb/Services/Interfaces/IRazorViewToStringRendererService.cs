using System.Threading.Tasks;

namespace SeikyuWeb.Services.Interfaces
{
    /// <summary>
    /// Razorビューを文字列にレンダリングするサービスインターフェース
    /// </summary>
    public interface IRazorViewToStringRendererService
    {
        /// <summary>
        /// 指定されたビューを文字列にレンダリングします。
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewName">ビューの名前</param>
        /// <param name="model">ビューに渡すモデル</param>
        /// <returns>レンダリングされたビューの文字列</returns>
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);

        /// <summary>
        /// JavaScriptを実行した後にビューを文字列にレンダリングします。
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewName">ビューの名前</param>
        /// <param name="model">ビューに渡すモデル</param>
        /// <returns>レンダリングされたビューの文字列</returns>
        Task<string> RenderViewAfterExecuseJSToStringAsync<TModel>(string viewName, TModel model);
    }
}
