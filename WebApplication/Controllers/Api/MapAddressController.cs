using WebApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers.Api
{
    /// <summary>
    /// 住所検索を管理するコントローラー
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MapAddressController : ControllerBase
    {
        private readonly ILogger<MapAddressController> _logger;
        private readonly MapApiSettings _mapApiSettings;

        /// <summary>
        /// MapAddressControllerのコンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="mapApiSettings">マップAPI設定</param>
        public MapAddressController(ILogger<MapAddressController> logger, IOptions<MapApiSettings> mapApiSettings)
        {
            _logger = logger;
            _mapApiSettings = mapApiSettings.Value;
        }

        /// <summary>
        /// 住所検索（座標指定）
        /// </summary>
        /// <param name="position">座標を指定</param>
        /// <param name="range">範囲</param>
        /// <param name="datum">入出力座標の測地系を指定</param>
        /// <returns>住所検索結果</returns>
        [HttpGet]
        public async Task<ActionResult<MapAddress>> Get(string position, int range, string datum)
        {
            MapAddress address = new();

            if (position == null)
            {
                address.ErrrMessage = "パラメータの取得に失敗しました。:" + position;
                return address;
            }

            string responseBody = "";
            string errorMessage = null;

            try
            {
                ZenrinMapAPI api = new(_mapApiSettings);

                for (int i = 0; i < 10; i++)
                {
                    string positionEx = position + ',' + (range + 50 * i);

                    address = await api.GetMapAddress(null, 3, null, 1, positionEx, null, null, null, null, datum);

                    MapAddressItem check = address.item.FirstOrDefault(m => m.address_level == "TBN");
                    
                    if (check != null) { break; }

                  
                }

                if (address.ErrrMessage != null)
                {
                    address.ErrrMessage = "住所検索に失敗しました：" + address.ErrrMessage;
                    return address;
                } else if (address.item.Count == 0)
                {
                    address.ErrrMessage = "選択箇所の住所が見つかりませんでした";
                    return address;
                }
                else
                {
                    return address;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                address.ErrrMessage = errorMessage;
                return address;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 住所検索（アドレス指定）
        /// </summary>
        /// <param name="address">住所</param>
        /// <returns>住所検索結果</returns>
        [HttpGet("Getlatlon")]
        public async Task<ActionResult<MapAddress>> Getlatlon(string address)
        {
            MapAddress mapAddress = new();

            if (address == null)
            {
                mapAddress.ErrrMessage = "パラメータの取得に失敗しました。:" + address;
                return mapAddress;
            }

            string responseBody = "";
            string errorMessage = null;

            try
            {
                ZenrinMapAPI api = new(_mapApiSettings);
                mapAddress = await api.GetMapAddress(address, 3, null, 1, null, null);

                if (mapAddress.ErrrMessage != null)
                {
                    mapAddress.ErrrMessage = "住所検索に失敗しました：" + mapAddress.ErrrMessage;
                    return mapAddress;
                }
                else
                {
                    return mapAddress;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                mapAddress.ErrrMessage = errorMessage;
                return mapAddress;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 建物・テナント名称検索
        /// </summary>
        /// <param name="address_code">住所コード</param>
        /// <param name="word">検索ワード</param>
        /// <returns>建物・テナント名称検索結果</returns>
        [HttpGet("GetBuildingName")]
        public async Task<ActionResult<Map_Building_Name>> GetBuildingName(string address_code, string word)
        {
            Map_Building_Name result = new();

            if (address_code == null && word == null)
            {
                result.ErrrMessage = "パラメータの取得に失敗しました。:" + address_code;
                return result;
            }

            string responseBody = "";
            string errorMessage = null;

            try
            {
                ZenrinMapAPI api = new(_mapApiSettings);
                result = await api.GetBuilding_Name(address_code, word);

                if (result.ErrrMessage != null)
                {
                    result.ErrrMessage = "建物・テナント名称検索に失敗しました：" + result.ErrrMessage;
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                result.ErrrMessage = errorMessage;
                return result;
            }
            finally
            {
            }
        }
    }
}
