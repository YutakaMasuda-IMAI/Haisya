using Microsoft.AspNetCore.Mvc;
using RenkeiDB.Dto;
using System.Net;

namespace RenkeiDB.Common
{
    /// <summary>
    /// APIレスポンス共通クラス
    /// </summary>
    public static class ApiResponseCommon
    {
        /// <summary>
        /// APIレスポンスを作成する
        /// </summary>
        /// <param name="statusCode">HTTPステータスコード</param>
        /// <param name="message">メッセージ</param>
        /// <returns>APIレスポンス</returns>
        public static ObjectResult CreateApiResponse(HttpStatusCode statusCode, string message)
        {
            ApiResponse response = new()
            {
                Code = (int)statusCode,
                Message = message
            };
            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
