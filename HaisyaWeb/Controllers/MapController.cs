using HaisyaWeb.API.Map;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Models.MapApiModel;
using static HaisyaWeb.Models.MapModel;

namespace HaisyaWeb.Controllers
{
    public class MapController : BaseController
    {
        private readonly ILogger<MapController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public MapController(ILogger<MapController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 住所からlatlon情報を取得
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetlatlonFromAddress(string address)
        {
            try
            {
                if (address == null || address == "") { return Json(new { partialView = "", message = "パラメータの取得に失敗しました。:" + address }); }

                address = address.Trim();

                AddressApi getAddress = new(_mapApiSettiong);
                MapAddress_Local addressResult = await getAddress.GetlatlonFromAddressAsync(address);

                if (addressResult == null) { return Json(new { partialView = "", message = "住所検索に失敗しました。原因不明" }); }
                if (addressResult.hit == 0) { return Json(new { partialView = "", message = "住所検索に失敗しました。検索ヒット0件" }); }

                if (addressResult.ErrrMessage != null) { throw new Exception(addressResult.ErrrMessage); }

                Latlon latlon = new()
                {
                    lat = addressResult.item[0].position.lat,
                    lng = addressResult.item[0].position.lng,
                };

                return Json(new { latlon_lat = latlon.lat, latlon_lng = latlon.lng,
                    address = addressResult.item[0].address,
                    address_code = addressResult.item[0].address_code,
                    address_level = addressResult.item[0].address_level,
                    post_code = addressResult.item[0].post_code,
                });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 緯度・経度・から住所選択画面の表示処理
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MapGetAddressFromPosition(string position, int modal_flg = 1, int range = 180,
                                                                string datum = Common.SystemConstants.Zenrin_datum.世界測地系)
        {
            MapddressListModel model = new()
            {
                Addresslist = new(),
                BuildingList = new(),
            };

            if (position == null)
            {
                return Json(new { partialView = "", message = "パラメータの取得に失敗しました。:" + position });
            }

            try
            {
                string lng = position.Split(",")[0];
                string lat = position.Split(",")[1];

                AddressApi getAddress = new(_mapApiSettiong);
                MapAddress_Local address = await getAddress.GetAddressDataAsync(position, range, datum);

                if (address == null) { return Json(new { partialView = "", errorMessage = "ネットワーク接続エラー" }); }
                if (address.ErrrMessage != null) { return Json(new { partialView = "", errorMessage = "住所検索に失敗しました：" + address.ErrrMessage }); }

                model.Addresslist = address.item;

                MapAddressItem_Local addressItemResult = new();
                Map_Building_NameItem_Local buidingResult = new();

                // 建物名のリストを設定
                List<MapAddressItem_Local> datalist = address.item.Where(m => m.address_level == "TBN").ToList();

                if (datalist != null && datalist.Count > 0)
                {
                    foreach (var target in datalist)
                    {
                        if (target.BuildingNameItemList != null && target.BuildingNameItemList.Count > 0)
                        {
                            foreach (var item in target.BuildingNameItemList)
                            {
                                if (item.building_name != null && item.building_name.Length > 0)
                                {
                                    Map_Building_NameItem_Local check = model.BuildingList.FirstOrDefault(m => m.zid == item.zid);

                                    if (check == null)
                                    {
                                        Map_Building_NameItem_Local data = new();
                                        CopyProperty(data, item);
                                        data.address2 = datalist[0].address2;
                                        data.address3 = datalist[0].address3;
                                        data.address4 = datalist[0].address4;
                                        //data.position = datalist[0].position;
                                        model.BuildingList.Add(data);
                                    }
                                }
                            }
                        }
                    }
                }
                return modal_flg == 0
                    ? await PartialViewAsJson("AddressList", model, true)
                    : await PartialViewAsJson("AddressListModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }
    }
}
