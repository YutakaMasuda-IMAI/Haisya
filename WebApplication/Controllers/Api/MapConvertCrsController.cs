using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApplication.Model;

namespace WebApplication.Controllers.Api
{
    /// <summary>
    /// 座標変換を管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MapConvertCrsController : ControllerBase
    {
        private readonly ILogger<MapConvertCrsController> _logger;
        private readonly MapApiSettings _mapApiSettings;

        /// <summary>
        /// MapConvertCrsControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="mapApiSettings">マップAPI設定</param>
        public MapConvertCrsController(ILogger<MapConvertCrsController> logger, IOptions<MapApiSettings> mapApiSettings)
        {
            _logger = logger;
            _mapApiSettings = mapApiSettings.Value;
        }

        /// <summary>
        /// 座標変換を取得
        /// </summary>
        /// <param name="latlon">緯度経度</param>
        /// <returns>座標変換結果</returns>
        [HttpGet]
        public Task<GenericResult> Get(Latlon latlon)
        {
            return null;
        }

        /// <summary>
        /// 日本の座標を世界の座標に変換
        /// </summary>
        /// <param name="lat">緯度</param>
        /// <param name="lng">経度</param>
        /// <returns>座標変換結果</returns>
        [HttpGet("GetJpnToWorld")]
        public async Task<GenericResult> GetJpnToWorld(string lat, string lng)
        {
            GenericResult result = new();

            if (lat == null)
            {
                result.ErrrMessage = "パラメータの取得に失敗しました。:" + lat;
                return result;
            }
            if (lng == null)
            {
                result.ErrrMessage = "パラメータの取得に失敗しました。:" + lng;
                return result;
            }

            string responseBody = "";
            string errorMessage = null;

            try
            {
                Latlon latlon = new() { lat = lat, lng = lng };

                ZenrinMapAPI api = new(_mapApiSettings);
                result = await api.GetConvert_CrsForJpnToWorld(latlon);

                if (result.ErrrMessage != null)
                {
                    result.ErrrMessage = "座標変換に失敗しました：" + result.ErrrMessage;
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                result.ErrrMessage = errorMessage ?? e.Message;
                return result;
            }
            finally
            {
            }
        }
    }
}
