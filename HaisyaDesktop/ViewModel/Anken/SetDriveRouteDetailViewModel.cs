using HaisyaDesktop.API.Map;
using HaisyaDesktop.Command;
using HaisyaDesktop.View.Tool;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.ViewModel.Anken
{
    class SetDriveRouteDetailViewModel : BaseViewModel
    {
        /// <summary>ポイントリスト</summary>
        public ObservableCollection<PointDto_Local> PointList { get; set; }

        /// <summary>マップAPIのURL（JS用）</summary>
        public ReactiveProperty<string> MapsApiForJSUrl { get; set; }

        public ReactiveProperty<string> Address { get; set; }
        public ReactiveProperty<string> Lat { get; set; }
        public ReactiveProperty<string> Lng { get; set; }

        public ReactiveProperty<string> SelectArea { get; set; }
        public ReactiveProperty<string> SelectKen { get; set; }
        public ReactiveProperty<string> SelectShiku { get; set; }
        public ReactiveProperty<string> SelectChyo { get; set; }

        public DelegateCommand OpenSubWindowCommandSelectAddress { get; private set; }

        //　メイン画面状況（サブ画面にメインの画面状況を渡すため）
        public Window ThisView { get; set; }


        public SetDriveRouteDetailViewModel()
        {

            Address = new();
            Lat = new();
            Lng = new();

            SelectArea = new() { Value = "" };
            SelectKen = new() { Value = "" };
            SelectShiku = new() { Value = "" };
            SelectChyo = new() { Value = "" };

            PointList = new();
            PointList.Add(new PointDto_Local { PointId = 1, PointStatusKubun = 1, PointDate = DateTime.Now.AddDays(2), PointTime = "00:00",
                PointTimeKubun = 1, PointRoadTypeKubun = "3", btnPointReg = true,
                PointComboBoxItemsStatusKubun = PublicObjects.PointComboBoxItemsStatusKubun(),
                PointComboBoxItemsTimeKubun = PublicObjects.PointComboBoxItemsTimeKubun(),
                PointComboBoxItemsRoadType = PublicObjects.PointComboBoxItemsRoadType(),
                PointComboBoxItemsPointKubun = PublicObjects.PointComboBoxItemsPointKubun()
            });

            //デフォルト値で初期化
            string mapUrl = PublicObjects.GetMapsAPIHosts();
            MapsApiForJSUrl = new() { Value = mapUrl + "DesktopApp/Index?" + PublicObjects.GetWebViewFlgForString() };


            //　住所選択画面を開く
            OpenSubWindowCommandSelectAddress = new DelegateCommand(() =>
            {
                Lat = new();
                Lng = new();
                App app = Application.Current as App;
                SelectAddress win = (SelectAddress)app.ShowModalView(new Tool.SelectAddressViewModel(), ThisView);
                if (win != null)
                {
                    if (win.AddressValFullAddress != null)
                    {
                        Address.Value = win.AddressValFullAddress;
                        Lat.Value = win.lat;
                        Lng.Value = win.lng;
                    }
                    else if (win.AddressValShiku != null)
                    {
                        SelectArea.Value = win.AddressValArea;
                        SelectKen.Value = win.AddressValKen;
                        SelectShiku.Value = win.AddressValShiku;
                        SelectChyo.Value = win.AddressValChyo;
                        Address.Value = SelectKen.Value + SelectShiku.Value + SelectChyo.Value;
                    };
                }
            });

        }

        public void AddPointList()
        {

            PointDto_Local PointDtoMax = PointList.OrderByDescending(m => m.PointId).First(); ;

            PointList.Add(new PointDto_Local { PointId = PointDtoMax.PointId + 1, PointStatusKubun = 1, PointDate = DateTime.Now.AddDays(2),
                PointTime = "00:00", PointTimeKubun = 1, PointRoadTypeKubun = "3", btnPointReg = true,
                PointComboBoxItemsStatusKubun = PublicObjects.PointComboBoxItemsStatusKubun(),
                PointComboBoxItemsTimeKubun = PublicObjects.PointComboBoxItemsTimeKubun(),
                PointComboBoxItemsRoadType = PublicObjects.PointComboBoxItemsRoadType(),
                PointComboBoxItemsPointKubun = PublicObjects.PointComboBoxItemsPointKubun(),
                PointDelBtnEnable = true
            });

        }

        public void DelPointList(PointDto_Local pointDto)
        {

            PointDto_Local data = PointList.FirstOrDefault(m => m.PointId == pointDto.PointId);
            if (data != null)
            {
                _ = PointList.Remove(data);
            }


        }

        /// <summary>
        /// 住所からlatlon情報を取得
        /// </summary>
        /// <returns></returns>
        public async Task<Latlon> GetlatlonFromAddress()
        {

            try
            {
                if (Address.Value == null || Address.Value == "") { return null; }

                AddressApi getAddress = new();
                MapAddress_Local address = await getAddress.GelatlonFromAddressAsync(Address.Value);

                if (address == null) { throw new Utils.MapUserException("住所検索に失敗しました。原因不明"); }
                if (address.hit == 0) { throw new Utils.MapUserException("住所検索に失敗しました。検索ヒット0件"); }

                if (address.ErrrMessage != null) { throw new Exception(address.ErrrMessage); }

                ObservableCollection<Point> temp = new();

                Latlon latlon = new()
                {
                    lat = address.item[0].position.lat,
                    lng = address.item[0].position.lng,
                };

                return latlon;

            }
            catch (Utils.MapUserException ex)
            {
                //_ = MessageBox.Show("住所取得エラー：" + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("住所取得エラー：" + ex.Message);
                return null;
            }

        }


        /// <summary>
        /// ポイント設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="latlon"></param>
        /// <param name="list"></param>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public async Task<bool> SetPointForPointListAsync(object sender, RoutedEventArgs e, string latlon, int pointId)
        {
            bool resultFlg = false;

            try
            {
                if (latlon == null || latlon.Split(",").Count() < 2) { return false; }
                string lng = latlon.Split(",")[0];
                string lat = latlon.Split(",")[1];

                AddressApi getAddress = new();
                MapAddress_Local address = await getAddress.GetAddressDataAsync(latlon);

                if (address == null) { throw new Exception("ネットワーク接続エラー"); }
                if (address.ErrrMessage != null) { throw new Exception(address.ErrrMessage); }

                MapAddressItem_Local addressItemResult = new();
                Map_Building_NameItem_Local buidingResult = new();

                // 建物名のリストを設定
                List<MapAddressItem_Local> datalist = address.item.Where(m => m.address_level == "TBN").ToList();

                if (datalist != null && datalist.Count >= 0)
                {

                    List<Map_Building_NameItem_Local> itemList = new();

                    if (datalist.Count > 0)
                    {
                        if (datalist[0].BuildingNameItemList != null && datalist[0].BuildingNameItemList.Count > 0)
                        {
                            addressItemResult.BuildingNameItem = datalist[0].BuildingNameItemList[0];
                            foreach (var item in datalist[0].BuildingNameItemList)
                            {
                                Map_Building_NameItem_Local data = new();
                                CopyProperty(data, item);
                                data.address2 = datalist[0].address2;
                                data.address3 = datalist[0].address3;
                                data.address4 = datalist[0].address4;
                                itemList.Add(data);
                            }
                        }
                    }


                    App app = App.Current as App;
                    SelectAddressDetail win = (SelectAddressDetail)app.ShowModalView(new Tool.SelectAddressDetailViewModel(itemList, address.item), ThisView);
                    if (win == null)
                    {
                        //buidingResult = null;
                        //addressItemResult = null;
                        return false;
                    }
                    else
                    {
                        buidingResult = win.SelectBuildingData;
                        addressItemResult = win.SelectAddressData;
                    }

                }
                else
                {
                    List<MapAddressItem_Local> points = address.item.OrderByDescending(m => m.address_code).ToList();
                    addressItemResult = points[0];
                }


                ObservableCollection<PointDto_Local> temp = new();

                foreach (PointDto_Local d in PointList)
                {
                    if (d.PointId == pointId)
                    {
                        // 初期化
                        d.Address_Code = null;
                        d.Address = null;
                        d.Address_Level = null;
                        d.Lat = null;
                        d.Lng = null;
                        d.BuildingName = null;
                        d.BuildingZid = null;
                        d.BuildingNameRead = null;
                        d.BuildingNameItem = null;

                        if (buidingResult != null && buidingResult.zid != null)
                        {
                            d.Address_Code = buidingResult.address_code;
                            d.Address = buidingResult.address;
                            d.Address_Level = "TBN";
                            d.Lat = lat;
                            d.Lng = lng;
                            d.BuildingName = buidingResult.building_name;
                            d.BuildingZid = buidingResult.zid;
                            d.BuildingZid_Attr = buidingResult.zid_attr;
                            d.BuildingNameRead = buidingResult.name_read;
                            d.BuildingNameItem = buidingResult;
                            d.Post_code = buidingResult.post_code;
                            d.Address2 = buidingResult.address2;
                            d.Address3 = buidingResult.address3;
                            d.Address4 = buidingResult.address4;
                            d.btnPointReg = true;
                            resultFlg = true;
                        }
                        else if (addressItemResult != null)
                        {
                            d.BuildingName = addressItemResult.Building_name;
                            d.Address_Code = addressItemResult.address_code;
                            d.Address = addressItemResult.address;
                            d.Address_Level = addressItemResult.address_level;
                            d.Post_code = addressItemResult.post_code;
                            d.Address2 = addressItemResult.address2;
                            d.Address3 = addressItemResult.address3;
                            d.Address4 = addressItemResult.address4;
                            d.Lat = lat;
                            d.Lng = lng;
                            d.btnPointReg = false;
                            resultFlg = true;
                        }
                        else
                        {
                            resultFlg = false;
                        }
                    }
                    temp.Add(d);
                }

                //RaisePropertyChanged(nameof(PointList));

                PointList.Clear();

                foreach (PointDto_Local d in temp)
                {
                    PointList.Add(d);
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("住所取得エラー：" + ex.Message);
                return false;
            }

            return resultFlg;
        }

    }
}
