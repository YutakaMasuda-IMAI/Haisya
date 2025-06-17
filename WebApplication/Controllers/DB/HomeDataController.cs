using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Common;
using WebApplication.Data;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// ホームデータを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeDataController : ControllerBase
    {
        private readonly ILogger<HomeDataController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// HomeDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        public HomeDataController(ILogger<HomeDataController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// データベースのプロバイダ名を取得します。
        /// </summary>
        /// <returns>プロバイダ名</returns>
        [HttpGet]
        public string Get()
        {
            return _context.Database.ProviderName;
        }

        #region  T_Admin_Info
        /// <summary>
        /// T_Admin_Infoリストを返却
        /// </summary>
        /// <param name="dateTime">日付</param>
        /// <returns>T_Admin_Infoリスト</returns>
        [HttpGet("GetAdminInfos")]
        public async Task<List<T_Admin_Info>> GetAdminInfos(DateTime dateTime)
        {
            //DateTime.TryParse(dateTime, out DateTime dateTime1);
            try
            {
                IQueryable<T_Admin_Info> query = _context.T_Admin_Infos;
                query = query.Where(m => m.Info_Datetime <= dateTime);
                query = query.Where(m => m.Info_End_Datetime >= dateTime);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        #endregion T_Admin_Info

        #region  T_Portal_Info

        /// <summary>
        /// T_Portal_Infoリストを返却
        /// </summary>
        /// <param name="dateTime">日付</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="portalKubun">ポータル区分</param>
        /// <returns>T_Portal_Infoリスト</returns>
        [HttpGet("GetPortalInfos")]
        public async Task<List<T_Portal_Info>> GetPortalInfos(DateTime dateTime, int companyId, int userId,
                                                                SystemEnums.PortalKubun portalKubun = SystemEnums.PortalKubun.配車WEB)
        {
            try
            {
                IQueryable<T_Portal_Info> query = _context.T_Portal_Infos;
                query = query.Where(m => m.Portal_Kubun == (int)portalKubun);
                query = query.Where(m => m.Limit_Date >= dateTime);
                query = query.Where(m => m.Company_ID >= companyId && m.User_ID == 0 || m.User_ID == userId);
                query = query.Where(m => m.Display_Flg >= SystemEnums.DisplayFlag.Visible);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 指定IDのT_Portal_Infoを返却
        /// </summary>
        /// <param name="infoId">情報ID</param>
        /// <returns>T_Portal_Info</returns>
        [HttpGet("GetPortalInfo")]
        public async Task<T_Portal_Info> GetPortalInfo(int infoId)
        {
            try
            {
                IQueryable<T_Portal_Info> query = _context.T_Portal_Infos;
                query = query.Where(m => m.Portal_Info_ID == infoId);

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        #endregion T_Portal_Info
    }
}
