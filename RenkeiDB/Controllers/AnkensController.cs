using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 案件に関するAPIコントローラー
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnkensController : ControllerBase
    {
        private readonly ILogger<AnkensController> _logger;
        private ICustomerPortalService _customerPortalService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAnkensService _service;

        public AnkensController(
            ILogger<AnkensController> logger,
            IHttpContextAccessor httpContextAccessor,
            IAnkensService service,
            ICustomerPortalService customerPortalService
        )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _service = service;
            _customerPortalService = customerPortalService;
        }

        /// <summary>
        /// 案件詳細を取得
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>案件詳細情報</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnken(string id)
        {
            if (!int.TryParse(id, out int intValue))
            {
                return BadRequest();
            }

            try
            {
                int uid = User.RequiredUserID();

                RenkeiAnkenDto result = await _service.GetAnkenDetailAsync(intValue, uid);
                if (result == null)
                {
                    return Ok(new { });
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 案件情報を更新
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <param name="dtos">更新情報</param>
        /// <returns>更新結果</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)] string id, UpdateAnkenDto dtos)
        {
            try
            {
                // Retrieve the cookie value
                var (cid, uid) = User.RequiredCompanyIdUserID();
                if (!CommonHelper.TryParseInt(id, out int intValue, "id", out string errorMessage))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, errorMessage);
                }

                ApiResponse result = await _service.UpdateAnkenAsync(intValue, uid, cid, dtos);

                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 案件一覧の取得
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns>案件一覧</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SearchForProjects([FromQuery] AnkenSearchDto dto)
        {
            try
            {
                int cid = User.RequiredCompanyId();

                IEnumerable<AnkensDto> data = await _service.GetAnkenListAsync(cid, dto?.oroshiAddress);

                return Ok(data);
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 案件情報を新規登録
        /// </summary>
        /// <param name="dto">新規登録情報</param>
        /// <returns>登録結果</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(AnkenCreateDto dto)
        {
            try
            {
                // Retrieve the cookie value
                var (uid, cid, bid) = User.RequiredUserIdCompanyIdBranchId();
                UserLoginDto user = new()
                {
                    CompanyId = cid,
                    BranchId = bid,
                    UserId = uid
                };

                ApiResponse result = await _service.CreateAnken(user, dto);
                if (result.Message != null)
                {
                    return ApiResponseCommon.CreateApiResponse((HttpStatusCode)result.Code, result.Message);
                }

                return Ok(new { });
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, SystemConstants.Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 案件変更履歴情報を取得
        /// </summary>
        /// <param name="id">案件ID</param>
        /// <returns>変更履歴情報</returns>
        [HttpGet("{id}/changes")]
        [Authorize]
        public async Task<IActionResult> GetHistoryAnkenChangeInfo(string id)
        {
            try
            {
                //IDがnullまたは空の場合、RequiredFieldのメッセージとともにBadRequestを返します
                if (string.IsNullOrWhiteSpace(id))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.RequiredField, "id"));
                }

                //IDが整数値でない場合は、InValid NumberのメッセージとともにBadRequestを返します。
                if (!int.TryParse(id, out int intId))
                {
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, "id"));
                }
                GetAnkenChangeDto data = await _service.GetHistoryChangeAnkenInfo(intId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, SystemConstants.Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }

        /// <summary>
        /// 案件CSVファイルをエクスポート
        /// </summary>
        /// <returns>CSVファイル</returns>
        [HttpGet("csv")]
        [Authorize]
        public async Task<IActionResult> ExportCsv()
        {
            try
            {
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                IEnumerable<AnkenDto> data = await _customerPortalService.GetPortalsAsync(cid, bid);

                if (!data.Any())
                {
                    return Ok(new { });
                }
                string headers = HeaderCsv.Anken;

                List<string> fieldOrder = FieldOrderCsv.AnkenFields;

                byte[] csvBytes = CsvExportHelper.ExportToCsv(data, headers, fieldOrder);
                return File(new MemoryStream(csvBytes), "application/octet-stream", "anken.csv");
            }
            catch (UnauthorizedAccessException)
            {
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.Unauthorized, Message.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.Error);
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, Message.InternalServerError);
            }
        }
    }
}