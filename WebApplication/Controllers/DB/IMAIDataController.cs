using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Controllers.DB
{
    /// <summary>
    /// IMAIデータを管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IMAIDataController : ControllerBase
    {
        private readonly ILogger<IMAIDataController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// IMAIDataControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="context">データベースコンテキスト</param>
        public IMAIDataController(ILogger<IMAIDataController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region V_Tokuisaki
        /*****************************************************************************
        M_Customer
        *****************************************************************************/
        /// <summary>
        /// トラックメイトの顧客先マスタデータを指定条件で取得して返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Code">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <returns>顧客先マスタデータリスト</returns>
        [HttpGet("V_TokuisakiList")]
        public async Task<List<Data.V_Tokuisaki>> V_TokuisakiList(int CompanyID, string Code = null, string Key = null, string Phone = null)
        {
            try
            {
                IQueryable<Data.V_Tokuisaki> dataList = _context.V_Tokuisakis.Where(m => m.削除フラグ == 0);

                if (Code != null && Code.Length > 0)
                {
                    dataList = dataList.Where(m => (m.コード.ToString().Substring(0, Code.Length) == Code));
                }

                if (Key != null && Key.Length > 0)
                {
                    dataList = dataList.Where(m => (m.社名.Substring(0, Key.Length) == Key ||
                                                    m.検索カナ.Substring(0, Key.Length) == Key ||
                                                    m.略称.Substring(0, Key.Length) == Key));
                }

                if (Phone != null && Phone.Length > 0)
                {
                    dataList = dataList.Where(m => (m.電話番号.Contains(Phone) || m.FAX番号.Contains(Phone)));
                }

                List<Data.V_Tokuisaki> result = await dataList.OrderBy(m => m.コード).Take(100).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }
        }

        #endregion V_Tokuisaki

        #region V_TokuisakiForNotConnect
        /*****************************************************************************
        M_Customer
        *****************************************************************************/
        /// <summary>
        /// トラックメイトの顧客先マスタデータの未連携データを指定条件で取得して返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Code">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <returns>未連携顧客先マスタデータリスト</returns>
        [HttpGet("GetTokuisakiForNotConnectList")]
        public async Task<List<Data.V_TokuisakiForNotConnect>> GetTokuisakiForNotConnectList(int CompanyID, string Code = null, string Key = null, string Phone = null)
        {
            try
            {
                try
                {
                    IQueryable<Data.V_TokuisakiForNotConnect> dataList = _context.V_TokuisakiForNotConnects.Where(m => m.削除フラグ == 0);

                    if (Code != null && Code.Length > 0)
                    {
                        dataList = dataList.Where(m => (m.コード.ToString().Substring(0, Code.Length) == Code));
                    }

                    if (Key != null && Key.Length > 0)
                    {
                        //dataList = dataList.Where(m => ((m.社名.Substring(0, Key.Length) == Key ||
                        //                                m.検索カナ.Substring(0, Key.Length) == Key ||
                        //                                m.略称.Substring(0, Key.Length) == Key)));
                        dataList = dataList.Where(m => ((m.社名.Contains(Key) ||
                                                        m.検索カナ.Contains(Key) ||
                                                        m.略称.Contains(Key))));
                    }

                    if (Phone != null && Phone.Length > 0)
                    {
                        dataList = dataList.Where(m => (m.電話番号.Contains(Phone) || m.FAX番号.Contains(Phone)));
                    }

                    List<Data.V_TokuisakiForNotConnect> result = await dataList.OrderBy(m => m.コード).Take(100).ToListAsync();
                    return result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    await Response.WriteAsync(ex.Message);
                    throw;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }


        }

        #endregion V_TokuisakiForNotConnect

        #region V_YosyasakiForNotConnect
        /*****************************************************************************
        M_Customer
        *****************************************************************************/
        /// <summary>
        /// トラックメイトの傭車先マスタデータの未連携データを指定条件で取得して返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="Code">コード</param>
        /// <param name="Key">キー</param>
        /// <param name="Phone">電話番号</param>
        /// <returns>未連携傭車先マスタデータリスト</returns>
        [HttpGet("GetYosyasakiForNotConnectList")]
        public async Task<List<Data.V_YosyasakiForNotConnect>> GetYosyasakiForNotConnectList(int CompanyID, string Code = null, string Key = null, string Phone = null)
        {
            try
            {
                try
                {
                    IQueryable<Data.V_YosyasakiForNotConnect> dataList = _context.V_YosyasakiForNotConnects.Where(m => m.削除フラグ == 0);

                    if (Code != null && Code.Length > 0)
                    {
                        dataList = dataList.Where(m => (m.コード.ToString().Substring(0, Code.Length) == Code));
                    }

                    if (Key != null && Key.Length > 0)
                    {
                        dataList = dataList.Where(m => ((m.名称.Contains(Key) ||
                                                        m.検索カナ.Contains(Key) ||
                                                        m.略称.Contains(Key))));
                    }

                    if (Phone != null && Phone.Length > 0)
                    {
                        dataList = dataList.Where(m => (m.電話番号.Contains(Phone) || m.FAX番号.Contains(Phone)));
                    }

                    List<Data.V_YosyasakiForNotConnect> result = await dataList.OrderBy(m => m.コード).Take(100).ToListAsync();
                    return result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    await Response.WriteAsync(ex.Message);
                    throw;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync(ex.Message);
                throw;
            }


        }

        #endregion V_YosyasakiForNotConnectist

    }
}
