using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 会社ポータルに関するAPIコントローラー
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CompanyPortalController : ControllerBase
    {
        private readonly ICompanyPortalService _service;
        private readonly ILogger<CompanyPortalController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyPortalController(ILogger<CompanyPortalController> logger, ICompanyPortalService service, IHttpContextAccessor httpContextAccessor)
            => (_logger, _service, _httpContextAccessor) = (logger, service, httpContextAccessor);

        /// <summary>
        /// 会社ポータルカレンダーを取得
        /// </summary>
        /// <param name="ym">yyyyMM形式の年月</param>
        /// <returns>カレンダー情報</returns>
        [Authorize]
        [HttpGet("company-portal-calendars")]
        public async Task<IActionResult> GetCalendars([FromQuery] string ym)
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

            try
            {
                // BranchIdとCompanyIdのCookie値を取得
                var (cid, bid) = User.RequiredCompanyIdBranchId();
                
                // get_calendarsメソッドを呼び出し、結果を返す
                IEnumerable<CountByDateString> data = await _service.get_calendars(cid, bid, d1, d2);
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
        /// 会社ポータルを取得
        /// </summary>
        /// <returns>ポータル情報</returns>
        [Authorize]
        [HttpGet("company-portal")]
        public IActionResult GetPortal()
        {
            try
            {
                // BranchIdとCompanyIdのCookie値を取得
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                throw new NotImplementedException();
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
        /// 各日付のポータル件数取得
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns>ポータル件数</returns>
        [Authorize]
        [HttpGet("company-portal-counts")]
        public async Task<IActionResult> GetPortalCounts([FromQuery] string date)
        {
            try
            {
                string companyId = User.FindFirst("CompanyId")?.Value;
                string branchId = User.FindFirst("BranchId")?.Value;
                
                if (string.IsNullOrEmpty(date))
                    date = date.Replace("%2F", "/");

                if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(branchId) ||
                    !DateTime.TryParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime temp))
                    return BadRequest();

                CompanyPortalCountsDto data = await _service.GetPortalCountsAsync(int.Parse(companyId), int.Parse(branchId), date);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// ポータル情報取得
        /// </summary>
        /// <returns>ポータル情報</returns>
        [Authorize]
        [HttpGet("company-portals")]
        public async Task<IActionResult> GetPortals()
        {
            try
            {
                string companyId = User.FindFirst("CompanyId")?.Value;
                string branchId = User.FindFirst("BranchId")?.Value;

                if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(branchId))
                    return BadRequest();

                CompanyPortalsDto data = await _service.GetPortalsAsync(int.Parse(companyId), int.Parse(branchId));
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
