using Microsoft.AspNetCore.Mvc;
using SeikyuWeb.Dto;
using System.Net;

namespace SeikyuWeb.Common
{
    public static class ApiResponseCommon
    {
        /// <summary>
        /// Apiレスポンスの作成
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ObjectResult CreateApiResponse(HttpStatusCode statusCode, string message)
        {
            ApiResponse response = new ApiResponse
            {
                code = (int)statusCode,
                message = message
            };
            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
