using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;
using SeikyuWeb.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Middlewares
{
    /// <summary>
    /// レスポンスを処理するためのミドルウェアです
    /// </summary>
    public class ResponseOverwritingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="next">次のミドルウェアへのデリゲート</param>
        public ResponseOverwritingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///レスポンスを処理するためにミドルウェアを呼び出します。
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>タスクは非同期処理を表します。</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //元のリクエストボディのストリームを保存します。
            Stream originalBodyStream = context.Response.Body;

            try
            {
                using (MemoryStream responseBody = new MemoryStream())
                {
                    //レスポンス本文を入れ替えます。
                    context.Response.Body = responseBody;
                    //next（context）は、HttpContext をパイプライン内の次のミドルウェアに処理を引き継ぎます。
                    // これは、リクエストが次のミドルウェアコンポーネントによって処理されることを保証します。
                    await _next(context);
                    //_next関数呼び出し後にリクエストボディのストリームを取得します。
                    Type requestBodyStream = GetRequestBodyStream(context);
                    //元のレスポンスボディストリームを復元します。
                    context.Response.Body = originalBodyStream;
                    //レスポンスデータを読み込んでJSONに変換します。
                    responseBody.Seek(0, SeekOrigin.Begin);
                    string json = await new StreamReader(responseBody).ReadToEndAsync();
                    //特定のステータスコードを処理します
                    if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && context.Response.ContentType == "application/problem+json; charset=utf-8")
                    {
                        //不正なリクエスト(400)の場合に、レスポンスを処理します。
                        await HandleBadRequestAsync(context, json, requestBodyStream);
                    }
                    else if (context.Response.StatusCode == (int)HttpStatusCode.UnsupportedMediaType)
                    {
                        //サポートされていないメディアタイプ (415)の場合に、レスポンスを処理します。
                        await HandleUnsupportedMediaTypeAsync(context, json);
                    }
                    else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized && !IsCustomError(context))
                    {
                        //アクセス権限がないエラー（401）と、ロック、期限切れ、無効なユーザー等のカスタムエラーを含む、全て401エラーを処理します。
                        await HandleUnauthorizedAsync(context, json);
                    }
                    else
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }

                }
            }
            catch (Exception ex)
            {
                //内部サーバーエラーの(500)場合にレスポンスを処理します。
                await HandleInternalServerErrorAsync(context, ex, originalBodyStream);
            }
        }

        /// <summary>
        ///リクエストボディストリームのタイプを取得します。
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>リクエストボディストリームのタイプです.</returns>
        private static Type GetRequestBodyStream(HttpContext context)
        {
            Endpoint endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            ControllerActionDescriptor controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            return controllerActionDescriptor?.Parameters
                .Select(parameter => parameter.ParameterType)
                .LastOrDefault();
        }

        /// <summary>
        ///不正なリクエスト(400)に対して、リクエスト検証と同様のエラー処理を行います。
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="json">レスポンスを表すJSON文字列です.</param>
        /// <param name="dtoType">DTOのタイプです.</param>
        /// <returns>タスクは非同期処理を表します.</returns>
        private async Task HandleBadRequestAsync(HttpContext context, string json, Type dtoType)
        {
            Dictionary<string, object> parsedInput = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            Dictionary<string, string[]> errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(parsedInput["errors"].ToString());
            Dictionary<string, string> listDisplayName = GetDisplayName(dtoType);
            Dictionary<string, object> groupedErrors = GroupErrors(errors, listDisplayName);

            ApiResponseGroup newResponseObj = new ApiResponseGroup
            {
                code = (int)HttpStatusCode.BadRequest,
                message = groupedErrors
            };

            string newJson = JsonSerializer.Serialize(newResponseObj, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            await context.Response.WriteAsync(newJson);
        }

        /// <summary>
        ///GetDisplayNameメソッドは、DTO内に設定された表示名を返します。表示名はDisplayAttribute属性を使用して設定されます。
        ///結果として、表示名は辞書形式（リスト）で返されます。
        /// </summary>
        /// <param name="dtoType">DTOのタイプです.</param>
        /// <returns>///プロパティ名とその対応する表示名を格納したリストです。</returns>
        private static Dictionary<string, string> GetDisplayName(Type dtoType)
        {
            return dtoType.GetProperties()
                .Select(property => new
                {
                    PropertyName = property.Name,
                    DisplayName = property.GetCustomAttributesData()
                        .FirstOrDefault(attr => attr.AttributeType == typeof(DisplayAttribute))?
                        .NamedArguments
                        .FirstOrDefault(arg => arg.MemberName == "Name")
                        .TypedValue
                        .Value
                        ?.ToString()
                })
                .Where(x => x.DisplayName != null)
                .ToDictionary(x => x.PropertyName, x => x.DisplayName);
        }

        /// <summary>
        /// サポートされていないメディアタイプの場合に、レスポンスを処理します。
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="json">レスポンスを表すJSON文字列です.</param>
        /// <returns>タスクは非同期処理を表します.</returns>
        private async Task HandleUnsupportedMediaTypeAsync(HttpContext context, string json)
        {
            JsonDocument responseObj = JsonDocument.Parse(json);
            string message = responseObj.RootElement.GetProperty("title").ToString();

            ApiResponse newResponseObj = new ApiResponse
            {
                code = responseObj.RootElement.GetProperty("status").GetInt32(),
                message = message
            };

            string newJson = JsonSerializer.Serialize(newResponseObj);
            await context.Response.WriteAsync(newJson);
        }

        /// <summary>
        /// アクセス権限がない場合にレスポンスを処理します。
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="json">レスポンスを表すJSON文字列です.</param>
        /// <returns>タスクは非同期処理を表します.</returns>
        private async Task HandleUnauthorizedAsync(HttpContext context, string json)
        {
            ApiResponse newResponseObj = new ApiResponse()
            {
                code = (int)HttpStatusCode.Unauthorized,
                message = Message.Unauthorized
            };

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            string newJson = JsonSerializer.Serialize(newResponseObj);
            await context.Response.WriteAsync(newJson);
        }

        /// <summary>
        /// 内部サーバーエラーの場合にレスポンスを処理します
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="ex">例外が発生しました.</param>
        /// <param name="originalBodyStream">元のレスポンスボディストリームです.</param>
        /// <returns>タスクは非同期処理を表します.</returns>
        private async Task HandleInternalServerErrorAsync(HttpContext context, Exception ex, Stream originalBodyStream)
        {
            ApiResponse newResponseObj = new ApiResponse
            {
                code = (int)HttpStatusCode.InternalServerError,
                message = Message.InternalServerError
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            string newJson = JsonSerializer.Serialize(newResponseObj);

            using (StreamWriter writer = new StreamWriter(originalBodyStream))
            {
                await writer.WriteAsync(newJson);
                await writer.FlushAsync();
            }
        }

        /// <summary>
        /// 不適切なデータ型の場合に、カスタムエラーメッセージを取得します
        /// </summary>
        /// <param name="message">エラーメッセージです.</param>
        /// <param name="keyValuePairs">プロパティ名とその対応する表示名を格納したリストです.</param>
        /// <returns>フォーマットされたエラーメッセージです.</returns>
        private string GetCustomErrorMessageInvalidType(string message, Dictionary<string, string> keyValuePairs)
        {
            //エラーメッセージからフィールド名とシステム型を抽出します
            Match systemTypeMatch = Regex.Match(message, @"System\.(\w+)");
            Match fieldMatch = Regex.Match(message, @"Path: \$\.(\w+)");
            //システム型とフィールド名が抽出されたかどうかを確認します
            if (systemTypeMatch.Success && fieldMatch.Success)
            {
                //システム型とフィールド名を抽出します
                string systemType = systemTypeMatch.Groups[1].Value;
                //フィールド名を抽出します
                string field = fieldMatch.Groups[1].Value;
                //システム型がnull許容かどうかチェックします
                if (systemType == "Nullable")
                {
                    Match matchTypeNullable = Regex.Match(message, @"System.Nullable`1\[System\.(?<Type>[^]]+)\]");

                    systemType = matchTypeNullable.Success ? matchTypeNullable.Groups["Type"].Value : systemType;
                }

                string name = field;
                keyValuePairs.TryGetValue(field, out name);

                //カスタムエラーメッセージを生成します
                switch (systemType)
                {
                    case "Int32":
                    case "Int64":
                    case "Double":
                    case "Decimal":
                        return string.Format(Message.InValidNumber, name);
                    case "DateTime":
                        return string.Format(Message.InValidDate, name);
                    case "String":
                        return string.Format(Message.InValidString, name);
                    case "Boolean":
                        return string.Format(Message.InvalidBoolean, name);
                    default:
                        return string.Format(Message.AllowValue, name);
                }
            }
            return message;
        }

        /// <summary>
        /// エラーがカスタムエラー（ロック、無効なユーザー、期限切れなど）かどうかをチェックします。全てのエラーコードは401を返します
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>エラーがカスタムエラーの場合は trueで、それ以外の場合は falseとなります。</returns>
        private bool IsCustomError(HttpContext context)
        {
            bool isError = context.Response.Headers.TryGetValue("X-Error-Message", out StringValues errorMessage);
            if (isError && (errorMessage == "Lock" || errorMessage == "InvalidUser" || errorMessage == "Expire"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// GroupErrorsメソッドは、エラーをフィールド名に基づいてグループ化し、辞書形式（リスト）にフォーマットします。
        /// </summary>
        /// <param name="errors">エラーの辞書（エラーリスト）であり、その中にキーはエラーキーで、値はエラーメッセージの配列です。</param>
        /// <returns>グループ化されたエラーを含むリストです。</returns>
        private Dictionary<string, object> GroupErrors(Dictionary<string, string[]> errors, Dictionary<string, string> keyValuePairs)
        {
            Dictionary<string, object> groupedErrors = new Dictionary<string, object>();

            foreach (var error in errors)
            {
                string[] parts = error.Key.TrimStart('$', '.').Split('.');
                string[] listMatch = parts[0].Split('[');

                if (listMatch.Length > 1)
                {
                    //フィールドエラーのリストを持つオブジェクトを返します。
                    GroupListError(groupedErrors, listMatch[0], parts.Last(), error.Value.First(), keyValuePairs);
                }
                else
                {
                    //エラーが発生しているフィールドを返します
                    GroupFieldError(groupedErrors, parts.Last(), error.Value.First(), keyValuePairs);
                }
            }

            return groupedErrors;
        }

        /// <summary>
        /// キーに基づいて、特定のリストにエラーをグループ化し、辞書形式（リスト）にフォーマットします。
        /// </summary>
        /// <param name="groupedErrors">グループ化されたエラー情報を格納するリストです</param>
        /// <param name="listName">リスト名です</param>
        /// <param name="fieldName">フィールド名です</param>
        /// <param name="message">エラーメッセージです</param>
        private void GroupListError(Dictionary<string, object> groupedErrors, string listName, string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            if (!groupedErrors.ContainsKey(listName))
            {
                groupedErrors[listName] = new Dictionary<string, string>();
            }

            Dictionary<string, string> listGroup = (Dictionary<string, string>)groupedErrors[listName];
            listGroup[fieldName] = GetFormattedMessage(fieldName, message, keyValuePairs);
        }

        /// <summary>
        ///キーに基づいて、特定のリストにエラーをグループ化し、辞書形式（リスト）にフォーマットします。
        /// </summary>
        /// <param name="groupedErrors">グループ化されたエラーを格納するリストです。</param>
        /// <param name="fieldName">フィールド名です</param>
        /// <param name="message">エラーメッセージです</param>
        private void GroupFieldError(Dictionary<string, object> groupedErrors, string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            groupedErrors[fieldName] = GetFormattedMessage(fieldName, message, keyValuePairs);
        }

        /// <summary>
        /// フィールド名とメッセージに基づいて、フォーマットされたエラーメッセージを取得します。
        /// </summary>
        /// <param name="fieldName">フィールド名です。</param>
        /// <param name="message">エラーメッセージです。</param>
        /// <param name="keyValuePairs">プロパティ名とその対応する表示名を格納したリストです。</param>
        /// <returns>フォーマットされたエラーメッセージです.</returns>
        private string GetFormattedMessage(string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            //エラーメッセージがカスタムエラーかどうかを確認します
            if (message.StartsWith("The JSON value could not be converted to"))
            {
                return GetCustomErrorMessageInvalidType(message, keyValuePairs);
            }
            return message;
        }
    }
}
