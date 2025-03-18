using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>ObjectResult</returns>
        public static ObjectResult HandleError(Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            int statusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
            return new ObjectResult(ex.Message)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
