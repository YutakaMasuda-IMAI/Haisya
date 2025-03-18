using System.Threading.Tasks;

namespace RenkeiDB.Services.Interfaces
{
    /// <summary>
    /// Razorビューを文字列にレンダリングするサービスインターフェース
    /// </summary>
    public interface IRazorViewToStringRendererService
    {
        /// <summary>
        /// ビューを文字列にレンダリングします。
        /// </summary>
        /// <typeparam name="TModel">モデルの型</typeparam>
        /// <param name="viewName">ビュー名</param>
        /// <param name="model">モデル</param>
        /// <returns>レンダリングされたビューの文字列</returns>
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
