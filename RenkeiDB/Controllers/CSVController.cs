using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// CSVに関する操作を提供するコントローラー
    /// </summary>
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class CSVController : ControllerBase
    {
        private readonly ILogger<CSVController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAnkensService _ankensService;

        public CSVController(ILogger<CSVController> logger, IHttpContextAccessor httpContextAccessor, IAnkensService ankensService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _ankensService = ankensService;
        }

        /// <summary>
        /// 受注案件CSVを取得する
        /// </summary>
        /// <returns>CSVファイル</returns>
        [HttpGet("juchu-anken/csv")]
        public async Task<IActionResult> GetJuchuAnkenCSV()
        {
            try
            {
                // cookie取得
                var (cid, bid) = User.RequiredCompanyIdBranchId();

                // csvデータ取得
                List<JuchuAnkenDto> data = await _ankensService.GetOrdersAsync(cid, bid);

                if (!data.Any())
                {
                    return Ok(new { });
                }

                // csvデータからcsvバイトに変換する
                byte[] csvBytes = CsvExportHelper.ExportToCsv(data,
                    SystemConstants.HeaderCsv.JuchuAnken, SystemConstants.FieldOrderCsv.JuchuAnkenFields);

                // csv-octet-streamで返す
                return File(new MemoryStream(csvBytes), "application/octet-stream", "JuchuAnken.csv");
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
    }
}
