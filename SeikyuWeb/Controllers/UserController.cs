using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeikyuWeb.Common;
using SeikyuWeb.Dto;
using SeikyuWeb.Dto.Contractor;
using SeikyuWeb.Dto.Customer;
using SeikyuWeb.Dto.LoginDto;
using SeikyuWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Controllers
{
    /// <summary>
    /// ユーザーコントローラークラス
    /// </summary>
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(ILogger<UserController> logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region M_LoginUser
        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="model">ログイン情報</param>
        /// <returns>
        /// 200: 成功
        /// 400: パラメータが不正となります。
        /// 401: 許可されていません。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserReqDto model)
        {
            try
            {
                int guardInt = 0;
                if (model.guard == DefaultValue.GuardCustomer)
                {
                    guardInt = 1;
                }
                LoginUserResDto data = await _userService.Login(model.loginId, model.password, guardInt);

                if (data == null)
                {
                    Response.Headers.Add("X-Error-Message", "InvalidUser");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.InvalidUser);
                }

                if ((!data.IsCustomer && data.LoginUser != null && data.LoginUser.LockFlg)
                    || (data.IsCustomer && data.LoginUserCustomer != null && data.LoginUserCustomer.LockFlg))
                {
                    Response.Headers.Add("X-Error-Message", "Lock");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.LockUser);
                }

                // クッキーの設定
                List<Claim> claims = new List<Claim>
                {
                    new Claim("LoginId", model.loginId),
                    new Claim("UserId",
                        (data.IsCustomer
                            ? data.LoginUserCustomer?.LoginUserCustomerId.ToString()
                            : data.LoginUser?.UserId.ToString()) ?? "0"),
                    new Claim("CompanyId", data.CompanyUser?.CompanyId.ToString() ?? "0"),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                HttpContext.Session.SetString("LoginId", model.loginId);
                if (guardInt == 0)
                {
                    await GetContractor();
                }
                else
                {
                    await GetCustomer();
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }

        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// </returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ApiResponse result = new ApiResponse()
                {
                    code = StatusCodes.Status200OK,
                    message = Message.Logout
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

        #region contractor
        /// <summary>
        /// ログイン情報取得（業者）
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: このアカウントはロックされています。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("company/me")]
        [Authorize]
        [ProducesResponseType(typeof(ContractorDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetContractor()
        {
            try
            {
                string loginId = HttpContext.Session.GetString("LoginId");
                ContractorDto result = await _userService.GetContractor(loginId);

                if (result is not null && result.LockFlg)
                {
                    Response.Headers.Add("X-Error-Message", "Lock");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.LockUser);
                }
                // companyIdをセッションに追加する
                if (result is not null)
                {
                    AddMeToSession(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        /// <summary>
        /// companyId をセッションに追加する
        /// </summary>
        /// <param name="data">業者情報</param>
        private void AddMeToSession(ContractorDto data)
        {
            ISession session = _httpContextAccessor.HttpContext.Session;
            int? companyId = data.companyUser?.company?.id;
            if (companyId != null && companyId > 0)
            {
                session.SetString("companyId", System.Text.Json.JsonSerializer.Serialize(companyId));
            }
        }
        #endregion contractor
        #region customer
        /// <summary>
        /// ログイン情報取得（荷主）
        /// </summary>
        /// <returns>
        /// 200: 成功
        /// 404: ページが見つかりません。
        /// 401: 許可されていません。
        /// 401: このアカウントはロックされています。
        /// 500: リクエストの処理中にエラーが発生しました。
        /// </returns>
        [HttpGet("customer/me")]
        [Authorize]
        [ProducesResponseType(typeof(LogisticsUnitDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                string loginId = HttpContext.Session.GetString("LoginId");
                LogisticsUnitDto result = await _userService.GetCustomer(loginId);
                if (result is not null && result.LockFlg)
                {
                    Response.Headers.Add("X-Error-Message", "Lock");
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.LockUser);
                }

                if (result is not null)
                {
                    AddMeToSession(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }

        /// <summary>
        /// companyIdおよびtantouId をセッションに追加する
        /// </summary>
        /// <param name="data">荷主情報</param>
        /// <exception cref="ArgumentException">無効なデータ型</exception>
        private void AddMeToSession(object data)
        {
            ISession session = _httpContextAccessor.HttpContext.Session;

            switch (data)
            {
                case ContractorDto contractor:
                    int? companyId = contractor.companyUser?.company?.id;
                    if (companyId != null && companyId > 0)
                    {
                        session.SetString("companyId", System.Text.Json.JsonSerializer.Serialize(companyId));
                    }
                    break;

                case LogisticsUnitDto customer:
                    int? tantouId = customer.customerTantou?.id;
                    if (tantouId != null && tantouId > 0)
                    {
                        session.SetString("tantouId", System.Text.Json.JsonSerializer.Serialize(tantouId));

                    }
                    int? customerBranchId = customer.customerTantou?.customerBranchId;
                    if (customerBranchId != null)
                    {
                        session.SetString("customerBranchId", System.Text.Json.JsonSerializer.Serialize(customerBranchId));
                    }
                    int? customerId = customer.customerTantou?.customer.id;
                    if (customerId != null && customerId > 0)
                    {
                        session.SetString("customerId", System.Text.Json.JsonSerializer.Serialize(customerId));
                        
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid data type");
            }
        }
        #endregion customer
    }
}
