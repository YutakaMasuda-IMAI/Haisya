using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication.Model;
using WebApplication.Services;
using Microsoft.AspNetCore.Http;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    /// <summary>
    /// 事故情報を管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccidentController : ControllerBase
    {
        private readonly IAccidentService _accidentService;

        public AccidentController(IAccidentService accidentService)
        {
            _accidentService = accidentService;
        }

        /// <summary>
        /// 事故情報の取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>事故情報</returns>
        [HttpGet("GetAcciden")]
        public async Task<IActionResult> GetAcciden(int Jiko_ID, int User_ID)
        {
            try
            {
                AccidentModel Accident = await _accidentService.GetAccident(Jiko_ID, User_ID);
                return new OkObjectResult(Accident);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// Jiko_Statusの更新
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("ChangeStatu")]
        public async Task<Dto.MsterDataCommonResultValDto> ChangeStatu(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out Microsoft.Extensions.Primitives.StringValues abc);
                ChangeStatus dataDto = System.Text.Json.JsonSerializer.Deserialize<ChangeStatus>(abc);

                await _accidentService.ChangeStatus(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// ワークフローリストの取得
        /// </summary>
        /// <param name="Jiko_ID">事故ID</param>
        /// <param name="Jiko_WorkFlow_Base_ID">ワークフロー基本ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>ワークフローリスト</returns>
        [HttpGet("GetWorkflowLis")]
        public async Task<IActionResult> GetWorkflowLis(int Jiko_ID, int Jiko_WorkFlow_Base_ID, int User_ID)
        {
            try
            {
                WorkFlowList Accident = await _accidentService.GetWorkflowList(Jiko_ID, Jiko_WorkFlow_Base_ID, User_ID);
                return new OkObjectResult(Accident);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// 差し戻し情報の取得
        /// </summary>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Jiko_WorkFlow_Status_ID">ワークフローステータスID</param>
        /// <returns>差し戻し情報</returns>
        [HttpGet("GetRemandDat")]
        public async Task<IActionResult> GetRemandDat(int User_ID, int Jiko_WorkFlow_Status_ID)
        {
            try
            {
                RemandDataModel modelData = await _accidentService.GetRemandData(User_ID, Jiko_WorkFlow_Status_ID);
                return new OkObjectResult(modelData);
            }
            catch (Exception ex)
            {
                return CommonHelper.HandleError(ex);
            }
        }

        /// <summary>
        /// PostAccidentの登録および更新
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("PostAccidentData")]
        public async Task<Dto.MsterDataCommonResultValDto> PostAccidentData(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = await Request.ReadFormAsync();

                if (!form.ContainsKey("name"))
                {
                    throw new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out var abc);
                PostAccident dataDto = System.Text.Json.JsonSerializer.Deserialize<PostAccident>(abc);

                int Jiko_ID = await _accidentService.PostAccident(dataDto);
                resultVal.RetrunFlg = true;
                resultVal.Jiko_ID = Jiko_ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// PostRemandの登録および更新
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("PostReman")]
        public async Task<Dto.MsterDataCommonResultValDto> PostReman(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out var abc);
                PostRemand dataDto = System.Text.Json.JsonSerializer.Deserialize<PostRemand>(abc);

                await _accidentService.PostRemand(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }

        /// <summary>
        /// PostApprovalの登録および更新
        /// </summary>
        /// <param name="jsonString">JSON形式の文字列</param>
        /// <returns>結果のDTO</returns>
        [HttpPost("PostApprova")]
        public async Task<Dto.MsterDataCommonResultValDto> PostApprova(string jsonString)
        {
            Dto.MsterDataCommonResultValDto resultVal = new();
            try
            {
                IFormCollection form = Request.ReadFormAsync().Result;

                if (!form.ContainsKey("name"))
                {
                    new Exception("name無し");
                }

                bool oo = form.TryGetValue("name", out var abc);
                PostRemand dataDto = System.Text.Json.JsonSerializer.Deserialize<PostRemand>(abc);

                await _accidentService.PostApproval(dataDto);
                resultVal.RetrunFlg = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = ex is BadHttpRequestException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                resultVal.ErrrMessage = ex.Message;
            }
            finally
            {

            }
            return resultVal;
        }
    }
}
