using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// ユーザーに関する操作を提供するコントローラー
    /// </summary>
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        #region M_LoginUser
        /*****************************************************************************
         M_LoginUser
         *****************************************************************************/
        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="body">ログイン情報</param>
        /// <returns>ログイン結果</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginBodyDto body)
        {
            try
            {
                Dto.CommonResultValDto resultVal = new();

                int guardInt = 0;
                if (body.guard == DefaultValue.GuardCustomer)
                {
                    guardInt = 1;
                }
                LoginUserDto data = await _userService.Login(body.loginId, body.password, guardInt);

                if (data == null)
                {
                    Response.Headers.Add("X-Error-Message", "InvalidUser");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.InvalidUser);
                }

                if (data.LoginUser.Lock_Flg == true)
                {
                    Response.Headers.Add("X-Error-Message", "Lock");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.LockUser);
                }

                // Set cookie
                List<Claim> claims = new()
                {
                    new Claim("LoginId", body.loginId),
                    new Claim("UserID", data.LoginUser.User_ID.ToString()),
                    new Claim("BranchId", data.CompanyUser.Branch_ID.ToString()),
                    new Claim("CompanyId", data.CompanyUser.Company_ID.ToString()),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authProperties = new()
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                HttpContext.Session.SetString("LoginId", body.loginId);
                await GetLoginUserInfo();
                return Ok(new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }


        /// <summary>
        /// ログアウト
        /// </summary>
        /// <returns>ログアウト結果</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Remove("LoginId");
                HttpContext.Session.Remove("GroupIds");
                HttpContext.Session.Remove("companyUserId");
                ApiResponse result = new()
                {
                    Code = StatusCodes.Status200OK,
                    Message = SystemConstants.Message.Logout
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }
        #endregion M_LoginUser

        #region GetLoginUserInfo
        /// <summary>
        /// ログインユーザー情報の取得
        /// </summary>
        /// <returns>ログインユーザー情報</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetLoginUserInfo()
        {
            try
            {
                Dto.CommonResultValDto resultVal = new();

                // Retrieve the cookie value
                string loginId = HttpContext.Session.GetString("LoginId");

                UserDto data = await _userService.GetLoginUserInfo(loginId);

                if (data == null)
                {
                    resultVal.code = StatusCodes.Status404NotFound;
                    resultVal.message = SystemConstants.Message.DataNotFound;
                    return NotFound(resultVal);
                }
                AddMeToSession(data);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        private void AddMeToSession(UserDto data)
        {
            ISession session = _httpContextAccessor.HttpContext.Session;
            int[] groupIds = data.CompanyUser?.Groups?.Select(g => g.Id).ToArray();
            if (groupIds != null && groupIds.Length > 0)
            {
                session.SetString("GroupIds", System.Text.Json.JsonSerializer.Serialize(groupIds));
            }

            int companyUserId = data.CompanyUser.Id;
            session.SetString("companyUserId", System.Text.Json.JsonSerializer.Serialize(companyUserId));
        }
        #endregion GetLoginUserInfo
    }
}
