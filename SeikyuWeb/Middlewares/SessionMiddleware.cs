using Microsoft.AspNetCore.Http;
using SeikyuWeb.Common;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeikyuWeb.Middlewares
{
    /// <summary>
    /// セッションを処理するためのミドルウェアです。
    /// </summary>
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="next">次のミドルウェアへのデリゲート</param>
        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// APIを呼び出す前にセッションを処理するためのミドルウェアです
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <returns>非同期タスク</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // ログインAPIパスかどうか確認し、ログインAPIパスであれば次の処理に進みます
            if (context.Request.Path.StartsWithSegments("/api/login", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            //セッションのログインIDを確認します
            //ログインIDが存在しない場合、アクセス権限がないエラー（401）を返します。
            if (context.Session.GetString("LoginId") == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(SystemConstants.Message.Unauthorized);
                return;
            }
            // ログインIDが存在な場合、パイプライン内の次のミドルウェアに処理を移行します
            await _next(context);
        }
    }
}
