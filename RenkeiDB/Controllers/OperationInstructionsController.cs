using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenkeiDB.Common;
using RenkeiDB.Dto.OperationInstructionDto;
using RenkeiDB.Service;
using RenkeiDB.Services.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RenkeiDB.Controllers
{
    /// <summary>
    /// 運行指示書に関する操作を提供するコントローラー
    /// </summary>
    [Authorize]
    [Route("api")]
    [ApiController]
    public class OperationInstructionsController : Controller
    {
        private readonly IConverter _converter;
        private readonly IViewRenderService _view_service;
        private readonly IOperationInstructionService _service;
        private readonly ILogger<OperationInstructionsController> _logger;

        public OperationInstructionsController(
            IOperationInstructionService service,
            IViewRenderService view_service,
            IConverter converter,
            ILogger<OperationInstructionsController> logger)
            => (_service, _view_service, _converter, _logger) = (service, view_service, converter, logger);

        /// <summary>
        /// 運行指示書印刷
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <returns>PDFファイル</returns>
        [HttpGet("get-operation-instructions/print")]
        public async Task<IActionResult> Print([FromQuery] int anken_id)
        {
            try
            {
                if (anken_id < 1)
                {
                    // 400エラー
                    return ApiResponseCommon.CreateApiResponse(HttpStatusCode.BadRequest, string.Format(SystemConstants.Message.InValidNumber, nameof(anken_id)));
                }

                // データ取得
                OperationInstructionPrintDto r = await _service.Get_operation_instruction_print(anken_id);
                if (r == null)
                {
                    return Ok(new { });
                }

                string html = await _view_service.Render_to_string(this, "_Print", r, true);

                byte[] pdf = _converter.ExportToPdf(html, Orientation.Portrait);

                using MemoryStream ms = new();
                ms.Write(pdf, 0, pdf.Length);
                return File(ms.ToArray(), "application/pdf", $"operation-instructions-{anken_id}.pdf");
            }
            catch (Exception x)
            {
                _logger.LogError(x, SystemConstants.Message.Error);
                // 500エラー
                return ApiResponseCommon.CreateApiResponse(HttpStatusCode.InternalServerError, SystemConstants.Message.InternalServerError);
            }
        }
    }
}
