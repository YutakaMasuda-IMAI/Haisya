using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 顧客ポータルに関する操作を提供するコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CustomerPortalsController : ControllerBase
    {
        private readonly ILogger<CustomerPortalsController> _logger;
        private ICustomerPortalService _customerPortalService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerPortalsController(ILogger<CustomerPortalsController> logger, ICustomerPortalService customerPortalService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _customerPortalService = customerPortalService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// ポータル情報の取得（荷主）
        /// </summary>
        /// <returns>ポータル情報</returns>
        [HttpGet("customer-portals")]
        [Authorize]
        [ProducesResponseType(typeof(AnkenDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPortals()
        {
            try
            {
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                IEnumerable<AnkenDto> data = await _customerPortalService.GetPortalsAsync(cid, bid);
                return Ok(data);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 顧客ポータルのカウントを取得
        /// </summary>
        /// <param name="date">yyyy/MM/dd</param>
        /// <returns>ポータルのカウント</returns>
        [Authorize]
        [HttpGet("customer-portal-counts")]
        public async Task<IActionResult> GetPortalCount([FromQuery] string date)
        {

            try
            {
                // Invalid format (yyyy/MM/dd): BadRequestを返す
                if (date == null
                    || date.Length != "yyyy/MM/dd".Length
                    || !int.TryParse(date[..4], out var yyyy)
                    || !int.TryParse(date[5..7], out var mm)
                    || mm > 12 || mm < 1
                    || !int.TryParse(date[8..], out var dd)
                    || dd > 31 || dd < 1)
                {
                    return BadRequest();
                }
                DateTime d1 = new DateTime(yyyy, mm, 1).AddDays(dd - 1);

                // BranchIdとCompanyIdのCookie値を取得
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                CustomerPortalCountsDto data = await _customerPortalService.Get_portal_counts(cid, bid, d1);
                return Ok(data);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// ポータル/荷主カレンダー数の取得
        /// </summary>
        /// <param name="ym">yyyy/MM</param>
        /// <returns>カレンダー数</returns>
        [Authorize]
        [HttpGet("customer-portal-calendars")]
        public async Task<IActionResult> GetPortalCalendars([FromQuery] string ym)
        {
            try
            {
                // ymがnullまたはyyyy/MMの形式でない場合、またはyyyyが4桁でない場合、またはmmが1以上12以下でない場合、BadRequestを返す
                if (ym == null
                    || ym.Length != "yyyy/MM".Length
                    || !int.TryParse(ym[..4], out var yyyy)
                    || !int.TryParse(ym[5..], out var mm)
                    || mm > 12
                    || mm < 1)
                {
                    return BadRequest();
                }

                // yyyy年mm月1日のDateTimeを作成し、1ヶ月後のDateTimeを作成
                DateTime d1 = new DateTime(yyyy, mm, 1);
                DateTime d2 = d1.AddMonths(1);

                // BranchIdとCompanyIdのCookie値を取得
                string bid = User.FindFirst("BranchId")?.Value;
                string cid = User.FindFirst("CompanyId")?.Value;
                // BranchIdまたはCompanyIdがnullの場合、Unauthorizedを返す
                if (bid == null || cid == null)
                {
                    return Unauthorized();
                }
                // GetCalendarsメソッドを呼び出し、結果を返す
                IEnumerable<CountByDateString> data = await _customerPortalService.GetCalendars(int.Parse(cid), int.Parse(bid), d1, d2);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
