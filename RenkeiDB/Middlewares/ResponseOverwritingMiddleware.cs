using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using RenkeiDB.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Middlewares
{
    /// <summary>
    /// レスポンスを処理するためのミドルウェア
    /// </summary>
    public class ResponseOverwritingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseOverwritingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// レスポンスを処理するためのミドルウェア
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <returns>非同期タスク</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    // レスポンス内容を入れ替え
                    context.Response.Body = responseBody;
                    await _next(context);
                    // _next関数呼び出し後にリクエスト内容を取得する
                    var requestBodyStream = GetRequestBodyStream(context);
                    context.Response.Body = originalBodyStream;
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var json = await new StreamReader(responseBody).ReadToEndAsync();

                    if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && context.Response.ContentType == "application/problem+json; charset=utf-8")
                    {
                        await HandleBadRequestAsync(context, json, requestBodyStream);
                    }
                    else if (context.Response.StatusCode == (int)HttpStatusCode.UnsupportedMediaType)
                    {
                        await HandleUnsupportedMediaTypeAsync(context, json);
                    }
                    else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized && !IsCustomError(context))
                    {
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
                await HandleInternalServerErrorAsync(context, ex, originalBodyStream);
            }
        }

        /// <summary>
        /// リクエストボディのストリームからオブジェクトタイプを取得する
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <returns>オブジェクトタイプ</returns>
        private static Type GetRequestBodyStream(HttpContext context)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            
            return controllerActionDescriptor?.Parameters
                .Select(parameter => parameter.ParameterType)
                .LastOrDefault();
        }

        /// <summary>
        /// 不正なリクエストの場合にレスポンスを処理する
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <param name="json">レスポンスのJSON文字列</param>
        /// <param name="dtoType">DTOのタイプ</param>
        /// <returns>非同期タスク</returns>
        private async Task HandleBadRequestAsync(HttpContext context, string json, Type dtoType)
        {
            var parsedInput = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(parsedInput["errors"].ToString());
            var listDisplayName = GetDisplayName(dtoType);
            var groupedErrors = new Dictionary<string, object>();
            if (context.Request.Path == "/api/ankens" && context.Request.Method == "POST")
            {
                groupedErrors = GroupErrorsHaveListError(errors, listDisplayName);
            }
            else
            {
                groupedErrors = GroupErrors(errors, listDisplayName);
            }

            var newResponseObj = new
            {
                code = (int)HttpStatusCode.BadRequest,
                message = groupedErrors
            };

            var newJson = JsonSerializer.Serialize(newResponseObj, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            await context.Response.WriteAsync(newJson);

        }

        /// <summary>
        /// 属性名と表示名をキーと値のセットとして設定する
        /// </summary>
        /// <param name="dtoType">DTOのタイプ</param>
        /// <returns>属性名と表示名の辞書</returns>
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
        /// サポートされていないメディアタイプの場合にレスポンスを処理する
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <param name="json">レスポンスのJSON文字列</param>
        /// <returns>非同期タスク</returns>
        private async Task HandleUnsupportedMediaTypeAsync(HttpContext context, string json)
        {
            var responseObj = JsonDocument.Parse(json);
            string message = responseObj.RootElement.GetProperty("title").ToString();

            var newResponseObj = new
            {
                code = responseObj.RootElement.GetProperty("status").GetInt32(),
                message = message
            };

            var newJson = JsonSerializer.Serialize(newResponseObj);
            await context.Response.WriteAsync(newJson);
        }

        /// <summary>
        /// アクセス権限がない場合にレスポンスを処理する
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <param name="json">レスポンスのJSON文字列</param>
        /// <returns>非同期タスク</returns>
        private async Task HandleUnauthorizedAsync(HttpContext context, string json)
        {
            var newResponseObj = new ApiResponse()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = Message.Unauthorized
            };

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var newJson = JsonSerializer.Serialize(newResponseObj);
            await context.Response.WriteAsync(newJson);
        }

        /// <summary>
        /// 内部サーバーエラーの場合にレスポンスを処理する
        /// </summary>
        /// <param name="context">HTTPコンテキスト</param>
        /// <param name="ex">例外</param>
        /// <param name="originalBodyStream">元のレスポンスボディストリーム</param>
        /// <returns>非同期タスク</returns>
        private async Task HandleInternalServerErrorAsync(HttpContext context, Exception ex, Stream originalBodyStream)
        {
            var newResponseObj = new
            {
                code = (int)HttpStatusCode.InternalServerError,
                message = Message.InternalServerError
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var newJson = JsonSerializer.Serialize(newResponseObj);

            using (var writer = new StreamWriter(originalBodyStream))
            {
                await writer.WriteAsync(newJson);
                await writer.FlushAsync();
            }
        }

        /// <summary>
        /// 渡されたメッセージによってカスタムエラーメッセージを取得する
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetCustomErrorMessageInvalidType(string message, Dictionary<string, string> keyValuePairs)
        {
            var systemTypeMatch = Regex.Match(message, @"System\.(\w+)");
            var fieldMatch = Regex.Match(message, @"Path: \$.*\.(\w+)");
            if (systemTypeMatch.Success && fieldMatch.Success)
            {
                string systemType = systemTypeMatch.Groups[1].Value;
                string field = fieldMatch.Groups[1].Value;

                if (systemType == "Nullable")
                {
                    var matchTypeNullable = Regex.Match(message, @"System.Nullable`1\[System\.(?<Type>[^]]+)\]");

                    systemType = matchTypeNullable.Success ? matchTypeNullable.Groups["Type"].Value : systemType;
                }

                string name = field;
                keyValuePairs.TryGetValue(field, out name);

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
        /// エラーがカスタムエラーかどうかをチェックする
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsCustomError(HttpContext context)
        {
            bool isError = context.Response.Headers.TryGetValue("X-Error-Message", out var errorMessage);
            if (isError && (errorMessage == "Lock" || errorMessage == "InvalidUser"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// キーに基づいて、エラーをグループ化してから、辞書形式（C#の1つのデータ型）にフォーマットする
        /// </summary>
        /// <param name="errors"キーがエラーキーで、値がエラーメッセージの配列であるエラーの辞書形式</param>
        /// <returns>グループ化されたエラーを含む辞書形式</returns>
        private Dictionary<string, object> GroupErrors(Dictionary<string, string[]> errors, Dictionary<string, string> keyValuePairs)
        {
            var groupedErrors = new Dictionary<string, object>();

            foreach (var error in errors)
            {
                var parts = error.Key.TrimStart('$', '.').Split('.');
                var listMatch = parts[0].Split('[');

                if (listMatch.Length > 1)
                {
                    GroupListError(groupedErrors, listMatch[0], parts.Last(), error.Value.First(), keyValuePairs);
                }
                else
                {
                    GroupFieldError(groupedErrors, parts.Last(), error.Value.First(), keyValuePairs);
                }
            }

            return groupedErrors;
        }

        /// <summary>
        /// エラーをキーに基づいてグループ化し、辞書形式にフォーマットする
        /// </summary>
        /// <param name="errors"キーがエラーキーで、値がエラーメッセージの配列であるエラーの辞書形式</param>
        /// <returns>グループ化されたエラーを含む辞書形式</returns>
        private Dictionary<string, object> GroupErrorsHaveListError(Dictionary<string, string[]> errors, Dictionary<string, string> keyValuePairs)
        {
            var groupedErrors = new Dictionary<string, object>();

            foreach (var error in errors)
            {
                var parts = error.Key.TrimStart('$', '.').Split('.');
                var listMatch = parts[0].Split('[');

                if (listMatch.Length > 1)
                {
                    int.TryParse(listMatch[1].Replace("]",""), out int index);
                    GroupListHaveListError(groupedErrors, listMatch[0], parts.Last(), error.Value.First(), keyValuePairs,index);
                }
                else
                {
                    GroupFieldError(groupedErrors, parts.Last(), error.Value.First(), keyValuePairs);
                }
            }

            return groupedErrors;
        }

        /// <summary>
        /// エラーをキーに基づいてグループ化し、辞書形式にフォーマットする
        /// </summary>
        /// <param name="groupedErrors">グループ化されたエラー情報を格納する辞書形式</param>
        /// <param name="listName">リスト名</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="message">フィールド名</param>
        private void GroupListError(Dictionary<string, object> groupedErrors, string listName, string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            if (!groupedErrors.ContainsKey(listName))
            {
                groupedErrors[listName] = new Dictionary<string, string>();
            }

            var listGroup = (Dictionary<string, string>)groupedErrors[listName];
            listGroup[fieldName] = GetFormattedMessage(fieldName, message, keyValuePairs);
        }
        /// <summary>
        /// エラーをキーに基づいてグループ化し、辞書形式（リスト）にフォーマットします。
        /// </summary>
        /// <param name="groupedErrors">グループ化されたエラー情報を格納する辞書形式</param>
        /// <param name="listName">リスト名</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="message">フィールド名</param>
        /// <param name="index">エラーリストのインデックス</param>
        private void GroupListHaveListError(Dictionary<string, object> groupedErrors, string listName, string fieldName, string message, Dictionary<string, string> keyValuePairs, int index)
        {
            if (!groupedErrors.ContainsKey(listName))
            {
                groupedErrors[listName] = new List<Dictionary<string, object>>();
            }

            var listGroup = (List<Dictionary<string, object>>)groupedErrors[listName];
            if(listGroup.Count == 0)
            {
                listGroup = new List<Dictionary<string, object>>();
            }
            if(listGroup.Count < index + 1)
            {
                listGroup.Add(new Dictionary<string, object>());
            }
            var groups = listGroup[index];
            if (!groups.ContainsKey(fieldName))
            {
                groups[fieldName] = GetFormattedMessage(fieldName, message, keyValuePairs);
            }
            groupedErrors[listName] = listGroup;
        }

        /// <summary>
        ///キーに基づいて、特定のリストにエラーをグループ化し、辞書形式にフォーマットする
        /// </summary>
        /// <param name="groupedErrors">グループ化されたエラーを格納する辞書形式</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="message">エラーメッセージ</param>
        private void GroupFieldError(Dictionary<string, object> groupedErrors, string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            groupedErrors[fieldName] = GetFormattedMessage(fieldName, message, keyValuePairs);
        }

        /// <summary>
        /// フィールド名とメッセージに基づいてエラーメッセージをフォーマットする
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="message">エラーメッセージ</param>
        /// <returns></returns>
        private string GetFormattedMessage(string fieldName, string message, Dictionary<string, string> keyValuePairs)
        {
            if (message.StartsWith("The JSON value could not be converted to"))
            {
                return GetCustomErrorMessageInvalidType(message, keyValuePairs);
            }
            return message;
        }
    }
}
