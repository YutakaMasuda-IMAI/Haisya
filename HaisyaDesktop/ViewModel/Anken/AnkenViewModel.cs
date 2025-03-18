using HaisyaDesktop.API.Map;
using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Command;
using HaisyaDesktop.Dto;
using HaisyaDesktop.Models;
using HaisyaDesktop.View.Tool;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.ViewModel.Anken
{

    /// <summary>画面オープンモード </summary>
    public enum AnkenOpenModeEnum
    {
        AddNew,
        Edit,
        Copy,
    };

    internal class AnkenViewModel : BaseViewModel
    {
        public AnkenOpenModeEnum AnkenOpenMode { get; private set; }

        public AsyncReactiveCommand ButtonClickAsyncCommand1 { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand ButtonClickAsyncCommand2 { get; } = new AsyncReactiveCommand();

        public DelegateCommand OpenSubWindowCommandDriveRouteList { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectAddress { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectKokyaku { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectSyasyu { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectKakoAnken { get; private set; }
        public DelegateCommand<string> OpenSubWindowCommandSelectTantou { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectDaisu { get; private set; }
        public DelegateCommand OpenSubWindowCommandSetOyaKokyaku { get; private set; }
        public DelegateCommand OpenSubWindowCommandPrintOrder { get; private set; }
        public DelegateCommand OpenSubWindowCommandSelectExcharge { get; private set; }
        

        public ICommand OpenSubWindowCommandPointEntry { get; private set; }

        /// <summary>積みポイント</summary>
        public ObservableCollection<PointDto_Local> TsumiPointList { get; set; }
        /// <summary>卸しポイント</summary>
        public ObservableCollection<PointDto_Local> OroshiPointList { get; set; }
        /// <summary>経由ポイント</summary>
        public ObservableCollection<PointDto_Local> ThroughPointList { get; set; }

        /// <summary>ポイント暫定確定選択</summary>
        public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsStatusKubun { get; set; }
        /// <summary>ポイント暫定確定選択</summary>
        public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsTimeKubun { get; set; }


        /// <summary>マップAPIのURL（JS用）</summary>
        public ReactiveProperty<string> MapsApiForJSUrl { get; set; }
        /// <summary>案件ステータス</summary>
        public ReactiveProperty<int> AnkenStatus { get; set; }

        /// <summary>顧客情報</summary>
        public ReactiveProperty<Dto.M_Customer_Local> KokyakuData { get; private set; }
        public ReactiveProperty<int> KokyakuId { get; private set; }
        //public ReactiveProperty<string> KokyakuCode { get; private set; }
        //public ReactiveProperty<string> KokyakuName { get; private set; }
        // <summary>顧客担当者ID</summary>
        public ReactiveProperty<int> KokyakuTantouId { get; set; }
        // <summary>顧客担当名称</summary>
        public ReactiveProperty<string> KokyakuTantouName { get; set; }
        // <summary>顧客担当名称</summary>
        public ReactiveProperty<string> KokyakuTantouPhone { get; set; }

        /// <summary>親顧客リスト</summary>
        public ObservableCollection<Dto.T_Anken_OyaKokyaku_Local> OyaKokyakuListData { get; set; }
        /// <summary>親顧客リスト</summary>
        public ReactiveProperty<string> OyaKokyakuLabel { get; set; }

        /// <summary>案件公開グループ</summary>
        public ReactiveProperty<int> PublishGroup { get; set; }
        public ObservableCollection<Models.ComboBoxItem> PublishGroupSelectList { set; get; }
        public ReactiveProperty<bool> PublishFlg { get; set; }
        public ReactiveProperty<DateTime?> PublishFromDatetime { get; set; }
        public ReactiveProperty<DateTime?> PublishToDatetime { get; set; }

        /// <summary>車種タイプ情報</summary>
        public ReactiveProperty<string> Syasyu { get; set; }
        public ReactiveProperty<string> SyasyuSize { get; set; }
        public ReactiveProperty<string> SyasyuDisplay { get; set; }
        public ReactiveProperty<string> Kata { get; set; }

        public ReactiveProperty<string> Eria { get; set; }

        /// <summary>台数</summary>
        public ReactiveProperty<int> Daisuu { get; set; }
        /// <summary>案件担当</summary>
        public ReactiveProperty<int> TantouID { get; set; }
        public ReactiveProperty<string> TantouName { get; set; }
        /// <summary>案件営業担当</summary>
        public ReactiveProperty<int> EigyoID { get; set; }
        public ReactiveProperty<string> EigyoName { get; set; }

        //public ReactiveProperty<string> Products { get; set; }

        /// <summary>ルート検索オプション情報</summary>
        public ReactiveProperty<string> Ferry { get; set; }
        public ReactiveProperty<string> Regulation { get; set; }
        public ReactiveProperty<string> Twouturn { get; set; }
        public ReactiveProperty<bool> EigyoshoModori { get; set; }

        /// <summary>請求金額区分（暫定/確定）</summary>
        public ReactiveProperty<int> SeikyuKubun { get; set; }

        // 積卸し時間
        public ReactiveProperty<string> TsumiTaskTime { get; set; }
        public ReactiveProperty<string> OroshiTaskTime { get; set; }

        public ReactiveProperty<bool> CheckOroshiSpace { get; set; }
        public ReactiveProperty<bool> EdnGoBackEigyosyo { get; set; }

        /// <summary>車番連絡</summary>
        public ReactiveProperty<int> NumberCommLimitKubun { get; set; }
        public ReactiveProperty<DateTime> NumberCommLimitDate { get; set; }
        public ReactiveProperty<string> NumberCommLimitTime { get; set; }

        public ReactiveProperty<string> Address { get; set; }
        public ReactiveProperty<string> Lat { get; set; }
        public ReactiveProperty<string> Lng { get; set; }

        public ReactiveProperty<string> SelectArea { get; set; }
        public ReactiveProperty<string> SelectKen { get; set; }
        public ReactiveProperty<string> SelectShiku { get; set; }
        public ReactiveProperty<string> SelectChyo { get; set; }

        public ReactiveProperty<double> Height { get; set; }
        public ReactiveProperty<double> Width { get; set; }
        public ReactiveProperty<double> Weight { get; set; }
        public ReactiveProperty<double> Nenpi { get; set; }

        public ReactiveProperty<double> BaseFee { get; set; }　　//基本運賃
        public ReactiveProperty<double> ExtraCharge { get; set; }     //追加費用
        public ReactiveProperty<double> Toll { get; set; }    //有料道路
        public ReactiveProperty<double> Discount { get; set; }    //割引額
        public ReactiveProperty<double> GrossAmount { get; set; }     //請求運賃

        public ReactiveProperty<int> AnkenId { get; set; }
        public ReactiveProperty<string> AnkenNo { get; set; }

        public ReactiveProperty<string> Remarks { get; set; }
        public ReactiveProperty<string> Luggage { get; set; }

        // 検索されたドライブルートリスト
        public ObservableCollection<DriveRouteListDisplay_Local> DriveRouteListData { get; set; }
        // 選択されたドライブルートリスト
        public DriveRouteListDisplay_Local SelectedDriveRouteDisplay { set; get; }
        /// <summary>その他追加費用</summary>
        public ObservableCollection<ExtraChargeDto_Local> ExtraChargeList { get; set; }
        // 
        public ObservableCollection<AnkenExchargeDto_Local> ExtraChargeListEx { get; set; }
  
        public ReactiveProperty<T_Anken_Local> t_Anken_Locals { get; set; }

        public ReactiveProperty<string> BtnRegUpdateContent { get; set; }
        public ReactiveProperty<string> BtnRegContent { get; set; }

        //　メイン画面状況（サブ画面にメインの画面状況を渡すため）
        public Window ThisView { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="paramAnkenId"></param>
        public AnkenViewModel(int paramAnkenId = 0, AnkenOpenModeEnum _ankenOpenMode = AnkenOpenModeEnum.AddNew)
        {
            AnkenOpenMode = _ankenOpenMode;

            ButtonClickAsyncCommand1.Subscribe(async _ => { await Task.Delay(500); });
            ButtonClickAsyncCommand2.Subscribe(async _ => { await Task.Delay(500); });

            TsumiPointList = new();
            OroshiPointList = new();
            ThroughPointList = new();
            ExtraChargeList = new();
            ExtraChargeListEx = new();

            TsumiPointList.Add(new PointDto_Local { PointId = 1, PointStatusKubun = 1, PointDate = DateTime.Now.AddDays(2), PointTime = "00:00", PointTimeKubun = 1 });
            OroshiPointList.Add(new PointDto_Local { PointId = 1, PointStatusKubun = 1, PointDate = DateTime.Now.AddDays(2), PointTime = "00:00", PointTimeKubun = 1 });
            ThroughPointList.Add(new PointDto_Local { PointId = 1, PointStatusKubun = 1, PointDate = DateTime.Now.AddDays(2), PointTime = "00:00", PointTimeKubun = 1 });

            //ExtraChargeListEx.Add(new Anken_ExchargeDto { Komoku_ID = 1, SIZE = "中型車", Komoku_Name = "テスト", Komoku_Key = "T", StdExcharge = 99999, Excharge = 0 });
            //ExtraChargeListEx.Add(new Anken_ExchargeDto { Komoku_ID = 2, SIZE = "中型車", Komoku_Name = "テスト2", Komoku_Key = "K", StdExcharge = 88888, Excharge = 0 });

            //デフォルト値で初期化
            string mapUrl = PublicObjects.GetMapsAPIHosts();
            MapsApiForJSUrl = new() { Value = mapUrl + "DesktopApp/Index?" + PublicObjects.GetWebViewFlgForString() };
            /// <summary>案件状況</summary>
            AnkenStatus = new() { Value = 0 };

            /// <summary>顧客情報</summary>
            KokyakuId = new() { Value = 0 };
            KokyakuData = new();

            /// <summary>顧客担当者</summary>
            KokyakuTantouId = new() { Value = 0 };
            KokyakuTantouName = new() { Value = "" };
            KokyakuTantouPhone = new() { Value = "" };
            OyaKokyakuLabel = new() { Value = "傭車元\n  無し" };
            OyaKokyakuListData = new();


            /// <summary>案件公開グループ</summary>
            PublishGroup = new();
            PublishGroupSelectList = new();
            PublishFlg = new() { Value = false };
            PublishFromDatetime = new();
            PublishToDatetime = new();

            /// <summary>車種サイズ情報</summary>
            SyasyuSize = new() { Value = "" };
            SyasyuDisplay = new() { Value = "" };
            Kata = new() { Value = "" };
            Syasyu = new() { Value = "" };

            /// <summary>エリア</summary>
            Eria = new() { Value = "中国" };

            /// <summary>担当配車</summary>
            TantouID = new() { Value = 0 };
            TantouName = new() { Value = "" };

            /// <summary>営業担当</summary>
            EigyoID = new() { Value = 0 };
            EigyoName = new() { Value = "" };

            //　走行状況
            Daisuu = new() { Value = 1 };
            Ferry = new() { Value = "F" };
            Regulation = new() { Value = "season,time" };
            Twouturn = new() { Value = "T" };

            EigyoshoModori = new() { Value = false };
            NumberCommLimitKubun = new() { Value = 1 };
            NumberCommLimitDate = new() { Value = DateTime.Today };
            NumberCommLimitTime = new();

            // 積卸し時間
            TsumiTaskTime = new() { Value = "00:00" };
            OroshiTaskTime = new() { Value = "00:00" };

            CheckOroshiSpace = new() { Value = false };
            EdnGoBackEigyosyo = new() { Value = true };

            Address = new();
            Lat = new();
            Lng = new();
            //Address.CollectionChanged += Address_CollectionChanged;

            SelectArea = new() { Value = "" };
            SelectKen = new() { Value = "" };
            SelectShiku = new() { Value = "" };
            SelectChyo = new() { Value = "" };

            SeikyuKubun = new() { Value = 0 };
            //PointSettingKubun = new() { Value = 0 };

            Height = new() { Value = 0 };
            Width = new() { Value = 0 };
            Weight = new() { Value = 0 };
            Nenpi = new() { Value = 0 };

            BaseFee = new() { Value = 0 };
            ExtraCharge = new() { Value = 0 };
            Toll = new() { Value = 0 };
            Discount = new() { Value = 0 };
            GrossAmount = new() { Value = 0 };

            AnkenId = new() { Value = 0 };
            AnkenNo = new() { Value = "" };

            Remarks = new() { Value = null };
            Luggage = new() { Value = null };

            t_Anken_Locals = new();

            DriveRouteListData = new();
            BtnRegUpdateContent = new();
            BtnRegContent = new();

            t_Anken_Locals = new();
            t_Anken_Locals.Subscribe(x => SetRegUpdateContent());


            PointComboBoxItemsStatusKubun = PublicObjects.PointComboBoxItemsStatusKubun();
            PointComboBoxItemsTimeKubun = PublicObjects.PointComboBoxItemsTimeKubun();


            /////新規、修正、コピー処理
            if (_ankenOpenMode == AnkenOpenModeEnum.AddNew)
            {
                NewRegisterBaseSetting();
            }
            else
            {
                AnkenId.Value = paramAnkenId;
            }

            // ドライブルート選択画面を開く
            OpenSubWindowCommandDriveRouteList = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                View.Anken.DriveRouteList win = (View.Anken.DriveRouteList)app.ShowModalView(new DriveRouteListViewModel(this), ThisView);
                if (win != null)
                {
                    bool flgRet = true;
                    SelectedDriveRouteDisplay = win.SelectedListItem;

                    if (BaseFee.Value == 0 && ExtraCharge.Value == 0 && Toll.Value == 0 && GrossAmount.Value == 0)
                    {
                    } else
                    {
                        string msg = "請求額項目(金額入力)を更新してもよろしいでしょうか？";
                        System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(msg, "登録確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.No) { flgRet = false; }
                    }

                    if (flgRet)
                    {
                        BaseFee.Value = SelectedDriveRouteDisplay.StdALLFreight;
                        ExtraCharge.Value = 0;
                        Toll.Value = SelectedDriveRouteDisplay.Totaltoll;
                        Discount.Value = 0;
                        GrossAmount.Value = SelectedDriveRouteDisplay.StdTotalFreight;
                    }
                    RaisePropertyChanged(nameof(SelectedDriveRouteDisplay));
                }
            });

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
                    } else if (win.AddressValShiku != null)
                    {
                        SelectArea.Value = win.AddressValArea;
                        SelectKen.Value = win.AddressValKen;
                        SelectShiku.Value = win.AddressValShiku;
                        SelectChyo.Value = win.AddressValChyo;
                        Address.Value = SelectKen.Value + SelectShiku.Value + SelectChyo.Value;
                    };
                }
            });

            //　顧客選択画面を開く
            OpenSubWindowCommandSelectKokyaku = new DelegateCommand(() =>
            {
                App app = App.Current as App;
                SelectKokyaku win = (SelectKokyaku)app.ShowModalView(new Tool.SelectKokyakuViewModel(), ThisView);
                if (win != null) {
                    KokyakuData.Value = win.SelectData;
                    KokyakuId.Value = win.SelectData.Customer_ID;
                };
            });

            //　車種選択画面を開く
            OpenSubWindowCommandSelectSyasyu = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                SelectSyasyuKata win = (SelectSyasyuKata)app.ShowModalView(new Tool.SelectSyasyuKataViewModel(), ThisView);
                if (win != null)
                {
                    SyasyuSize.Value = win.SelectSize;
                    SyasyuDisplay.Value = win.SelectSyasyuDisplay;
                    Kata.Value = win.SelectKata;
                    Syasyu.Value = win.SelectSyasyu;
                };
            });

            //　過去案件選択画面を開く
            OpenSubWindowCommandSelectKakoAnken = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                SelectKakoAnken win = (SelectKakoAnken)app.ShowModalView(new Tool.SelectKakoAnkenViewModel(), ThisView);
                if (win != null)
                {
                };
            });

            //　担当者選択画面を開く
            OpenSubWindowCommandSelectTantou = new DelegateCommand<string>(v =>
            {
                App app = Application.Current as App;
                SelectTantou win = (SelectTantou)app.ShowModalView(new Tool.SelectTantouViewModel(v.ToString()), ThisView);
                if (win != null)
                {
                    switch (v.ToString())
                    {
                        case "tantou":
                            TantouID.Value = win.SelectData.Tntou_ID;
                            TantouName.Value = win.SelectData.Last_Name;
                            break;
                        case "eigyo":
                            EigyoID.Value = win.SelectData.User_ID;
                            EigyoName.Value = win.SelectData.Last_Name;
                            break;
                        default:
                            break;
                    };
                };
            });

            //　台数選択画面を開く
            OpenSubWindowCommandSelectDaisu = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                SelectDaisu win = (SelectDaisu)app.ShowModalView(new Tool.SelectDaisuViewModel(), ThisView);
                if (win != null)
                {
                    Daisuu.Value = win.SelectData;
                };
            });

            //　親顧客選択画面を開く
            OpenSubWindowCommandSetOyaKokyaku = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                View.Anken.SetOyaKokyakuList win = (View.Anken.SetOyaKokyakuList)app.ShowModalView(
                                                            new SetOyaKokyakuListViewModel(OyaKokyakuListData,
                                                                                        KokyakuId.Value, KokyakuData.Value, KokyakuTantouId.Value,
                                                                                        KokyakuTantouName.Value, KokyakuTantouPhone.Value), ThisView);
                if (win != null)
                {
                    OyaKokyakuListData = win.OyaKokyakuListData;
                    if (OyaKokyakuListData != null)
                    {
                        OyaKokyakuLabel.Value = "傭車元\n  有り";
                    };
                };
            });

            //　注文書印刷画面を開く
            OpenSubWindowCommandPrintOrder = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                View.Print.AnkenOrder win = (View.Print.AnkenOrder)app.ShowModalView(new Print.AnkenOrderViewModel(AnkenId.Value), ThisView);
                if (win != null)
                {
                };
            });

            //　地図選択画面を開く
            OpenSubWindowCommandPointEntry = CreateCommand(dto =>
            {
                if (dto == null) { return; }
                Dto.T_Point_Local t_Point = new();
                CopyProperty(t_Point, dto);

                var ViewModel = new ViewModel.Anken.PointEntryViewModel(t_Point);

                App app = App.Current as App;
                View.Anken.PointEntry win = (View.Anken.PointEntry)app.ShowModalView(ViewModel, ThisView);
                if (win != null)
                {
                };
            });

            //　追加料金項目選択を開く
            OpenSubWindowCommandSelectExcharge = new DelegateCommand(() =>
            {
                App app = Application.Current as App;
                View.Anken.SelectExcharge win = (View.Anken.SelectExcharge)app.ShowModalView(new Anken.SelectExchargeViewModel(SyasyuSize.Value), ThisView);
                if (win != null)
                {
                    Dto.M_Anken_Excharge_Dto SelectData = win.SelectData;
                    var check = ExtraChargeListEx.FirstOrDefault(m => m.Komoku_Key == SelectData.Komoku_Key);
                    if (check == null)
                    {
                        AnkenExchargeDto_Local dto = new();
                        CopyProperty(dto, SelectData);
                        ExtraChargeListEx.Add(dto);
                        RaisePropertyChanged(nameof(ExtraChargeListEx));
                    }
                };
            });
            

        }

        /// <summary>
        /// 更新ボタンの表題設定
        /// </summary>
        public void SetRegUpdateContent()
        {
            if (AnkenOpenMode == AnkenOpenModeEnum.Edit)
            {
                BtnRegContent.Value = "変更履歴追加";
                BtnRegUpdateContent.Value = "修正更新";
            } else
            {
                BtnRegContent.Value = "新規登録";
                BtnRegUpdateContent.Value = "";
            }
        }

        /// <summary>
        /// ログイン情報データ取得
        /// </summary>
        public void NewRegisterBaseSetting()
        {

            if (Context.ContextManager.Instance.User.Syasyu != null)
            {
                Syasyu.Value = Context.ContextManager.Instance.User.Syasyu;
            }

            if (Context.ContextManager.Instance.User.Kata != null)
            {
                Kata.Value = Context.ContextManager.Instance.User.Kata;
            }

            if (Context.ContextManager.Instance.User.SyasyuDisplay != null)
            {
                SyasyuDisplay.Value = Context.ContextManager.Instance.User.SyasyuDisplay;
            }

            if (Context.ContextManager.Instance.User.SyasyuSize != null)
            {
                SyasyuSize.Value = Context.ContextManager.Instance.User.SyasyuSize;
            }


            if (Context.ContextManager.Instance.User.UserId > 0)
            {
                TantouID.Value = Context.ContextManager.Instance.User.UserId;
            }

            if (Context.ContextManager.Instance.User.UserName != null)
            {
                TantouName.Value = Context.ContextManager.Instance.User.UserName;
            }

        }



        /// <summary>
        /// 積地リスト行追加
        /// </summary>
        public void TsumiPointListPlus()
        {
            int i = TsumiPointList.Max(e => e.PointId);
            TsumiPointList.Add(new PointDto_Local { PointId = i + 1, Address = "", Lng = "", Lat = "" });
        }

        /// <summary>
        /// 卸地リスト行追加
        /// </summary>
        public void OroshiPointListPlus()
        {
            int i = OroshiPointList.Max(e => e.PointId);
            OroshiPointList.Add(new PointDto_Local { PointId = i + 1, Address = "", Lng = "", Lat = "" });
        }

        /// <summary>
        /// 積地リスト行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TsumiPointListDel(object sender, RoutedEventArgs e)
        {
            Button aa = (Button)sender;
            PointDto_Local point = (PointDto_Local)aa.DataContext;
            int id = point.PointId;

            PointDto_Local data = TsumiPointList.FirstOrDefault(m => m.PointId == id);
            if (data != null)
            {
                _ = TsumiPointList.Remove(data);
            }
        }

        /// <summary>
        /// 卸地リスト行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OroshiPointListDel(object sender, RoutedEventArgs e)
        {
            Button aa = (Button)sender;
            PointDto_Local point = (PointDto_Local)aa.DataContext;
            int id = point.PointId;

            PointDto_Local data = OroshiPointList.FirstOrDefault(m => m.PointId == id);
            if (data != null)
            {
                _ = OroshiPointList.Remove(data);
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
        public async Task<bool> SetPointForPointListAsync(object sender, RoutedEventArgs e, string latlon, ObservableCollection<PointDto_Local> list, int pointId)
        {
            bool resultFlg = false;

            try
            {
                if (latlon == null || latlon.Split(",").Count() < 2) { return false ; }
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

                } else
                {
                    List<MapAddressItem_Local> points = address.item.OrderByDescending(m => m.address_code).ToList();
                    addressItemResult = points[0];
                }


                ObservableCollection<PointDto_Local> temp = new();

                foreach (PointDto_Local d in list)
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
                            resultFlg = true;
                        }
                        else
                        {
                            resultFlg = false;
                        }
                    }
                    temp.Add(d);
                }

                list.Clear();

                foreach (PointDto_Local d in temp)
                {
                    list.Add(d);
                }

            } catch (Exception ex)
            {
                _ = MessageBox.Show("住所取得エラー：" + ex.Message);
                return false;
            }

            return resultFlg;
        }


        /// <summary>
        /// ドライブルートリスト検索
        /// </summary>
        /// <returns></returns>
        public async Task GetDriveRouteListAsync()
        {


            /// <param name="from">出発地点</param>
            /// <param name="to">到着地点</param>
            /// <param name="mpoint">経由地点</param>
            /// <param name="searchparam">検索挙動変更</param>
            /// <param name="height">車高</param>
            /// <param name="width">車幅</param>
            /// <param name="weight">車重</param>
            /// <param name="departuretime">出発時刻指定</param>
            /// <param name="cardetailinfo">詳細車種</param>
            /// <param name="tolltype">料金車種</param>
            /// <param name="smartic">スマートIC利用指定</param>
            /// <param name="regulation">規制考慮</param>
            /// <param name="twouturn">2段階Uターン回避指定</param>
            /// <param name="ferry">フェリー考慮指定</param>

            try
            {

                //経由地点
                string mpoint = "";

                /////////////パラメーター
                //出発地点
                PointDto_Local fromPoint = TsumiPointList.OrderBy(m => m.PointId).FirstOrDefault();
                string from = fromPoint.Lat + "," + fromPoint.Lng;

                if (TsumiPointList.Count > 1)
                {
                    List<PointDto_Local> list = TsumiPointList.OrderBy(m => m.PointId).ToList();
                    foreach (PointDto_Local aa in list)
                    {
                        if (aa.PointId != fromPoint.PointId)
                        {
                            mpoint += "," + aa.Lat + "," + aa.Lng;
                        }
                    }
                }

                //到着地点
                PointDto_Local toPoint = OroshiPointList.OrderByDescending(m => m.PointId).FirstOrDefault();
                string to = toPoint.Lat + "," + toPoint.Lng;

                if (OroshiPointList.Count > 1)
                {
                    List<PointDto_Local> list = OroshiPointList.OrderBy(m => m.PointId).ToList();
                    foreach (PointDto_Local aa in list)
                    {
                        if (aa.PointId != fromPoint.PointId)
                        {
                            mpoint += "," + aa.Lat + "," + aa.Lng;
                        }
                    }
                }

                if (mpoint != null && mpoint != "")
                {
                    mpoint = mpoint[1..];
                }
                

                //検索挙動変更（1：ルート所要時間に最適化した値を使用します。（デフォルト値 ：0) に比べ平均的にルート所要時間が短縮されます。）
                int searchparam = 1;

                //出発時刻指定
                string departuretime = null;

                //車種
                string syasyu = Syasyu.Value;
                //型
                string kata = Kata.Value;
                //車種サイズ
                string syasyuSize = SyasyuSize.Value;
                //詳細車種
                string cardetailinfo = "B";
                //料金車種
                string tolltype = "large";


                //string mpointstype = "general,highway,highway,general";
                string mpointstype = "";

                //スマートIC利用指定
                string smartic = "T";
                //規制考慮
                string regulation = Regulation.Value;
                if ("none".Equals(regulation, StringComparison.Ordinal)) { regulation = ""; }
                //2段階Uターン回避指定
                string twouturn = Twouturn.Value;
                //フェリー考慮指定]
                string ferry = Ferry.Value;

                //追加料金
                List<AnkenExchargeDto_Local> AnkenExchargeList = new();
                foreach (var data in ExtraChargeListEx)
                {
                    AnkenExchargeDto_Local exchargeData = new();
                    CopyProperty(exchargeData, data);
                    AnkenExchargeList.Add(exchargeData);
                }


                MasterDataApi dataApi = new();
                M_Syaryo_Local syaryo = await dataApi.GetSyaryoData(PublicObjects.GetCompanyID(), syasyu, kata);
                if (syaryo != null && syaryo.SYASYU != null)
                {
                    Height.Value = (double)syaryo.HEIGHT;
                    Width.Value = (double)syaryo.WIDTH;
                    Weight.Value = (double)syaryo.CAR_GROSS_WEIGHT;
                    Nenpi.Value = (double)syaryo.AVG_FUEL_COSTS;
                    cardetailinfo = syaryo.CARDETAILINFO;
                    tolltype = syaryo.TOLL_TYPE;
                }

                if (syaryo.SYASYU == null)
                {
                    string msg = "対象の車種マスタが存在しません。ルート検索を中止します。";
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(msg, "エラー");
                    return;
                }

                if (syaryo.HEIGHT == null || syaryo.WIDTH == null || syaryo.CAR_GROSS_WEIGHT == null || syaryo.AVG_FUEL_COSTS == null)
                {
                    string msg = "対象の車種においてルート検索に必要なマスタ設定に不備があります。それでも検索を実行しますか？";
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(msg, "確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (result == System.Windows.Forms.DialogResult.No) { return ; }
                }

                // 検索ルート検索処理
                AnkenDataApi api = new();
                DriveRouteListDto_Local driveListtEx = await api.GetDriveRouteListExAsync(
                                                                                Eria.Value,
                                                                                PublicObjects.GetCompanyID(),
                                                                                from, to, mpoint, syasyu, kata,
                                                                                Height.Value, Width.Value, Weight.Value, Nenpi.Value, 
                                                                                mpointstype, SyasyuSize.Value,
                                                                                searchparam, departuretime, cardetailinfo,
                                                                                tolltype, smartic, regulation, twouturn, ferry,
                                                                                TsumiTaskTime.Value, OroshiTaskTime.Value,
                                                                                AnkenExchargeList);

                if (driveListtEx == null) { throw new Exception("ネットワーク接続エラー"); }

                if (driveListtEx.ErrrMessage != null && driveListtEx.ErrrMessage.Length > 0) { throw new Exception(driveListtEx.ErrrMessage); }

                if (driveListtEx.DriveRouteListDisplayList.Count == 1) { throw new Exception("ルート検索出来ませんでした"); }

                DriveRouteListData = new();
                foreach(DriveRouteListDisplay_Local data in driveListtEx.DriveRouteListDisplayList)
                {
                    DriveRouteListData.Add(data);
                };

                ExtraChargeList = new();
                foreach (ExtraChargeDto_Local data in driveListtEx.ExchargeDataList)
                {
                    ExtraChargeList.Add(data);
                };

                OpenSubWindowCommandDriveRouteList.Execute(this);
                //OpenSubWindowCommandDriveRouteList.DelegateCanExecute();

            } catch (Exception ex)
            {
                _ = MessageBox.Show("自動車ルート検索エラー：" + ex.Message);
                return ;
            }

        }

        /// <summary>
        /// 地図上のドライブルートライン作成
        /// </summary>
        /// <param name="routeType"></param>
        /// <returns></returns>
        public string GetDriveRoteLineForHtml(int routeType)
        {

            string html = "";

            DriveRouteListDisplay_Local data = DriveRouteListData[routeType];

            html += "     var table = '<table id=\"" + "Map" + routeType + "\">';";
            html += "     var tableJQ = $(table);";

            for (int m = 0; m < data.line.Count; m++)
            {

                html += "    var trJQ_r = $('<tr></tr>').appendTo(tableJQ);";

                html += "    $('<td>" + data.line[m].lat + "</td>').appendTo(trJQ_r);";
                html += "    $('<td>" + data.line[m].lng + "</td>').appendTo(trJQ_r);";

            }

            html += "$('#Polyline').append(tableJQ);";

            return html;

        }

        /// <summary>
        /// 地図上に積み下ろしポイント設定
        /// </summary>
        /// <returns></returns>
        public string GetDriveRotePointForHtml()
        {

            int intVal = 0;

            string html = "";
            html += "     var table = '<table id=\"" + "Map1" + "\">';";
            html += "     var tableJQ = $(table);";

            //ConvertCrsApi api = new();

            foreach (var target in TsumiPointList)
            {
                if (!"".Equals(target.Lat) && !"".Equals(target.Lng))
                {
                    //MapApiModel.Latlon latlon1 = await api.GetConvert_CrsForJpnToWorld(target.Lat, target.Lng);
                    //if (latlon1 == null) { return null; }
                    intVal += 1;
                    html += "    var trJQ_r = $('<tr></tr>').appendTo(tableJQ);";
                    html += "    $('<td>" + target.Lat + "</td>').appendTo(trJQ_r);";
                    html += "    $('<td>" + target.Lng + "</td>').appendTo(trJQ_r);";
                    //html += "    $('<td>" + latlon1.lat + "</td>').appendTo(trJQ_r);";
                    //html += "    $('<td>" + latlon1.lng + "</td>').appendTo(trJQ_r);";
                }
            }

            foreach (var target in OroshiPointList)
            {
                if (!"".Equals(target.Lat) && !"".Equals(target.Lng))
                {
                    //MapApiModel.Latlon latlon1 = await api.GetConvert_CrsForJpnToWorld(target.Lat, target.Lng);
                    //if (latlon1 == null) { return null; }
                    intVal += 1;
                    html += "    var trJQ_r = $('<tr></tr>').appendTo(tableJQ);";
                    html += "    $('<td>" + target.Lat + "</td>').appendTo(trJQ_r);";
                    html += "    $('<td>" + target.Lng + "</td>').appendTo(trJQ_r);";
                    //html += "    $('<td>" + latlon1.lat + "</td>').appendTo(trJQ_r);";
                    //html += "    $('<td>" + latlon1.lng + "</td>').appendTo(trJQ_r);";
                }
            }

            html += "$('#Polyline').append(tableJQ);";

            return intVal == 2 ? html : null;
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
        /// 案件データ新規登録処理
        /// </summary>
        /// <returns></returns>
        public async Task RegisterForAddNew()
        {

            //if (InputCheck(out string errorMessage)) { throw new Exception(errorMessage); }

            Models.AnkenModel.AnkenDataModelDto ankenDataModelDto = new()
            {
                T_Anken = new()
                ,T_Anken_Detail = new()
                ,T_Anken_Remarks = new()
                ,T_Anken_PointList = new()
                ,T_Anken_Publish = new()
                ,T_Anken_ExchargeList = new()
                ,T_Anken_Luggage = new()
            };

            try
            {
                SetDto(ankenDataModelDto);
                AnkenDataApi api = new();
                t_Anken_Locals.Value = await api.AddNewAnkenData(ankenDataModelDto);
            }
            catch (Exception ex)
            {
                //_ = MessageBox.Show("案件データ新規登録エラー：" + ex.Message);
                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// データ登録・データ履歴登録
        /// </summary>
        /// <returns></returns>
        public async Task RegisterForUpdate()
        {

            //if (InputCheck(out string errorMessage)) { throw new Exception(errorMessage); }

            Models.AnkenModel.AnkenDataModelDto ankenDataModelDto = new()
            {
                T_Anken = new()
                ,T_Anken_Detail = new()
                ,T_Anken_Remarks = new()
                ,T_Anken_PointList = new()
                ,T_Anken_ExchargeList = new()
                ,T_Anken_Publish = new()
                ,T_Anken_Luggage = new()
            };

            try
            {
                SetDto(ankenDataModelDto);
                AnkenDataApi api = new();
                UpdateAnkenDataDto dto = await api.UpdateAnkenData(ankenDataModelDto);
                t_Anken_Locals.Value = dto.Anken;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// 入力データをDtoに格納
        /// </summary>
        /// <param name="ankenDataModelDto"></param>
        /// <returns></returns>
        public Models.AnkenModel.AnkenDataModelDto SetDto(Models.AnkenModel.AnkenDataModelDto ankenDataModelDto)
        {
            try
            {
                /////////////////////////T_Anken//////////////////////////////
                if (t_Anken_Locals.Value != null)
                {
                    ankenDataModelDto.T_Anken = t_Anken_Locals.Value;
                } else
                {
                    ankenDataModelDto.T_Anken.Anken_ID = 0;
                    ankenDataModelDto.T_Anken.Anken_Latest_Order = 0;
                }
                
                ankenDataModelDto.T_Anken.Anken_Status = AnkenStatus.Value;
                if (ankenDataModelDto.T_Anken.Anken_ID == 0)
                {
                    ankenDataModelDto.T_Anken.Company_ID = Context.ContextManager.Instance.User.CompanyID;
                    ankenDataModelDto.T_Anken.Branch_ID = Context.ContextManager.Instance.User.BranchID;
                }

                /////////////////////////T_Anken_Publish//////////////////////////////
                ankenDataModelDto.T_Anken_Publish.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Publish.PublishGroup_ID = PublishGroup.Value;
                ankenDataModelDto.T_Anken_Publish.Publish_Flg = PublishFlg.Value;
                ankenDataModelDto.T_Anken_Publish.Publish_FromDatetime = PublishFromDatetime.Value;
                ankenDataModelDto.T_Anken_Publish.Publish_ToDatetime = PublishToDatetime.Value;


                /////////////////////////T_Anken_Detail//////////////////////////////
                ankenDataModelDto.T_Anken_Detail.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Detail.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                ankenDataModelDto.T_Anken_Detail.TantouID = TantouID.Value;
                ankenDataModelDto.T_Anken_Detail.EigyoID = EigyoID.Value;
                ////請求関連;
                ankenDataModelDto.T_Anken_Detail.SeikyuKubun = SeikyuKubun.Value;
                ////顧客情報;
                ankenDataModelDto.T_Anken_Detail.KokyakuId = KokyakuId.Value;
                ankenDataModelDto.T_Anken_Detail.KokyakuCode = KokyakuData.Value.Customer_Code;
                ankenDataModelDto.T_Anken_Detail.KokyakuName = KokyakuData.Value.Customer_Name_Abbr;

                ankenDataModelDto.T_Anken_Detail.KokyakuTantouId = KokyakuTantouId.Value;
                ankenDataModelDto.T_Anken_Detail.KokyakuTantouName = KokyakuTantouName.Value;
                ankenDataModelDto.T_Anken_Detail.KokyakuTantouPhone = KokyakuTantouPhone.Value;
                ////車種情報;
                ankenDataModelDto.T_Anken_Detail.Syasyu = Syasyu.Value;
                ankenDataModelDto.T_Anken_Detail.SyasyuSize = SyasyuSize.Value;
                ankenDataModelDto.T_Anken_Detail.SyasyuDisplay = SyasyuDisplay.Value;
                ankenDataModelDto.T_Anken_Detail.Kata = Kata.Value;
                ////台数;
                ankenDataModelDto.T_Anken_Detail.Daisuu = Daisuu.Value;
                ////ルート検索条件;
                ankenDataModelDto.T_Anken_Detail.Root_Ferry = Ferry.Value;
                ankenDataModelDto.T_Anken_Detail.Root_Regulation = Regulation.Value;
                ankenDataModelDto.T_Anken_Detail.Root_Twouturn = Twouturn.Value;
                ankenDataModelDto.T_Anken_Detail.Root_EigyoshoModori = EigyoshoModori.Value;
                ///積み降ろし時間
                ankenDataModelDto.T_Anken_Detail.TsumiTaskTime = TsumiTaskTime.Value;
                ankenDataModelDto.T_Anken_Detail.OroshiTaskTime = OroshiTaskTime.Value;

                ankenDataModelDto.T_Anken_Detail.CheckOroshiSpace = CheckOroshiSpace.Value;
                ankenDataModelDto.T_Anken_Detail.EdnGoBackEigyosyo = EdnGoBackEigyosyo.Value;

                ////車番連絡;
                ankenDataModelDto.T_Anken_Detail.NumberCommLimitKubun = NumberCommLimitKubun.Value;
                if (NumberCommLimitDate != null && NumberCommLimitTime.Value != null)
                {
                    ankenDataModelDto.T_Anken_Detail.NumberCommLimitDateTime =
                        DateTime.Parse(NumberCommLimitDate.Value.ToString("yyyy/MM/dd") + " " + NumberCommLimitTime.Value);
                } else if (NumberCommLimitDate != null && NumberCommLimitTime.Value == null)
                {
                    ankenDataModelDto.T_Anken_Detail.NumberCommLimitDateTime =
                        DateTime.Parse(NumberCommLimitDate.Value.ToString("yyyy/MM/dd") + " 00:00");
                }

                ////選択ルート;
                if (SelectedDriveRouteDisplay != null)
                {
                    
                    ankenDataModelDto.T_Anken_Detail.RouteID = SelectedDriveRouteDisplay.routeID;
                    ankenDataModelDto.T_Anken_Detail.RouteType = int.Parse(SelectedDriveRouteDisplay.routeType);
                    ankenDataModelDto.T_Anken_Detail.RouteTypeDisplay = SelectedDriveRouteDisplay.RouteTypeDisplay;
                    ankenDataModelDto.T_Anken_Detail.Route_TotalTime = SelectedDriveRouteDisplay.TotalTime;
                    ankenDataModelDto.T_Anken_Detail.Route_BreakTime = SelectedDriveRouteDisplay.BreakTime;
                    ankenDataModelDto.T_Anken_Detail.Route_RestTime = SelectedDriveRouteDisplay.RestTime;

                    ankenDataModelDto.T_Anken_Detail.Route_TotalDistance = SelectedDriveRouteDisplay.TotalDistance;
                    ankenDataModelDto.T_Anken_Detail.Route_FuelConsume = (decimal?)SelectedDriveRouteDisplay.FuelConsume;
                    ankenDataModelDto.T_Anken_Detail.Route_Totaltoll = (decimal?)SelectedDriveRouteDisplay.Totaltoll;
                    ankenDataModelDto.T_Anken_Detail.Route_RestTimeDisplay = SelectedDriveRouteDisplay.RestTimeDisplay;

                    ankenDataModelDto.T_Anken_Detail.Route_StdFreight = (decimal?)SelectedDriveRouteDisplay.StdFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_StdALLFreight = (decimal?)SelectedDriveRouteDisplay.StdALLFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_StdExcharge = (decimal?)SelectedDriveRouteDisplay.StdExcharge;
                    ankenDataModelDto.T_Anken_Detail.Route_StdTotalFreight = (decimal?)SelectedDriveRouteDisplay.StdTotalFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForLaborCost = (decimal?)SelectedDriveRouteDisplay.GrossAmountForLaborCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForFuelCost = (decimal?)SelectedDriveRouteDisplay.GrossAmountForFuelCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForSyaryoCost = (decimal?)SelectedDriveRouteDisplay.GrossAmountForSyaryoCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForLuggage = (decimal?)SelectedDriveRouteDisplay.GrossAmountForLuggage;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForExcharge = (decimal?)SelectedDriveRouteDisplay.GrossAmountForExcharge;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmount = (decimal?)SelectedDriveRouteDisplay.GrossAmount;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountTotal = (decimal?)SelectedDriveRouteDisplay.GrossAmountTotal;
                    ankenDataModelDto.T_Anken_Detail.Route_TotalDays = SelectedDriveRouteDisplay.TotalDays;
                }

                ////請求情報;
                ankenDataModelDto.T_Anken_Detail.BaseFee = (decimal?)BaseFee.Value;
                ankenDataModelDto.T_Anken_Detail.ExtraCharge = (decimal?)ExtraCharge.Value;
                ankenDataModelDto.T_Anken_Detail.Toll = (decimal?)Toll.Value;
                ankenDataModelDto.T_Anken_Detail.Discount = (decimal?)Discount.Value;
                ankenDataModelDto.T_Anken_Detail.GrossAmount = (decimal?)GrossAmount.Value;

                ankenDataModelDto.T_Anken_Detail.Height = Height.Value;
                ankenDataModelDto.T_Anken_Detail.Width = Width.Value;
                ankenDataModelDto.T_Anken_Detail.Weight = Weight.Value;
                ankenDataModelDto.T_Anken_Detail.Nenpi = Nenpi.Value;

                ankenDataModelDto.T_Anken_Detail.Insert_Datetime = DateTime.Now;
                ankenDataModelDto.T_Anken_Detail.Insert_User = 1;

                ////////////////////////////////T_Anken_Remarks///////////////////////////////////
                ankenDataModelDto.T_Anken_Remarks.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Remarks.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                ankenDataModelDto.T_Anken_Remarks.Remarks = Remarks.Value;

                ////////////////////////////////T_Anken_Luggage///////////////////////////////////
                ankenDataModelDto.T_Anken_Luggage.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Luggage.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                //ankenDataModelDto.T_Anken_Luggage.Luggage = Luggage.Value;

                ////////////////////////////////T_Anken_PointList///////////////////////////////////
                int pointOrder = 1;

                foreach (PointDto_Local dto in TsumiPointList)
                {
                    Dto.T_Anken_Point_Local t_Anken_Point_Local = new();
                    CopyProperty(t_Anken_Point_Local, dto);
                    t_Anken_Point_Local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                    t_Anken_Point_Local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                    t_Anken_Point_Local.Kubun = 1;
                    t_Anken_Point_Local.Point_Order = pointOrder;
                    if (pointOrder == 1) { t_Anken_Point_Local.SEKubun = "S"; }
                    pointOrder += 1;
                    ankenDataModelDto.T_Anken_PointList.Add(t_Anken_Point_Local);
                }

                pointOrder = 1;
                if (ThroughPointList != null)
                {
                    foreach (PointDto_Local dto in ThroughPointList)
                    {
                        if (dto.Address != null)
                        {
                            Dto.T_Anken_Point_Local t_Anken_Point_Local = new();
                            CopyProperty(t_Anken_Point_Local, dto);
                            t_Anken_Point_Local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                            t_Anken_Point_Local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                            t_Anken_Point_Local.Kubun = 2;
                            t_Anken_Point_Local.Point_Order = pointOrder;
                            pointOrder += 1;
                            ankenDataModelDto.T_Anken_PointList.Add(t_Anken_Point_Local);
                        }
                    }
                }

                pointOrder = 1;
                foreach (PointDto_Local dto in OroshiPointList)
                {
                    Dto.T_Anken_Point_Local t_Anken_Point_Local = new();
                    CopyProperty(t_Anken_Point_Local, dto);
                    t_Anken_Point_Local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                    t_Anken_Point_Local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                    t_Anken_Point_Local.Kubun = 9;
                    t_Anken_Point_Local.Point_Order = pointOrder;
                    if (pointOrder == OroshiPointList.Count) { t_Anken_Point_Local.SEKubun = "E"; }
                    pointOrder += 1;
                    ankenDataModelDto.T_Anken_PointList.Add(t_Anken_Point_Local);
                }

                /////////////////////////////////T_Anken_OyaKokyakuList///////////////////////////////////
                if (OyaKokyakuListData.Count > 1)
                {
                    ankenDataModelDto.T_Anken_OyaKokyakuList = new();
                    int i = 1;
                    foreach (T_Anken_OyaKokyaku_Local data in OyaKokyakuListData)
                    {
                        Dto.T_Anken_OyaKokyaku_Local t_Anken_OyaKokyaku = new();
                        CopyProperty(t_Anken_OyaKokyaku, data);
                        t_Anken_OyaKokyaku.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                        t_Anken_OyaKokyaku.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                        t_Anken_OyaKokyaku.Kokyaku_Order = i;
                        i += 1;
                        ankenDataModelDto.T_Anken_OyaKokyakuList.Add(t_Anken_OyaKokyaku);
                    }
                }

                /////////////////////////////////T_Anken_ExchargeList///////////////////////////////////
                if (ExtraChargeListEx.Count > 1)
                {
                    ankenDataModelDto.T_Anken_ExchargeList = new();
                    int i = 1;
                    foreach (AnkenExchargeDto_Local data in ExtraChargeListEx)
                    {
                        Dto.T_Anken_Excharge_Local t_Anken_Excharge = new();
                        CopyProperty(t_Anken_Excharge, data);
                        t_Anken_Excharge.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                        t_Anken_Excharge.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                        i += 1;
                        ankenDataModelDto.T_Anken_ExchargeList.Add(t_Anken_Excharge);
                    }
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("案件データ新規登録エラー：" + ex.Message);
                throw;
            }
            finally
            {
            }

            return ankenDataModelDto;
        }


        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        public bool InputCheck(out string errorMessage)
        {
            bool flgResult = true;
            errorMessage = "";

            try
            {

                //　顧客情報確認　顧客（荷主）データ
                if (KokyakuData.Value == null) { errorMessage += "荷主は必須です。\r\n"; }
                //　配車担当確認
                if (TantouID.Value == 0) { errorMessage += "案件担当者は必須です。\r\n";  }
                //　車種
                if (SyasyuDisplay.Value == null || SyasyuDisplay.Value.Length == 0) { errorMessage += "車種は必須です。\r\n"; }
                //　
                
                

                if (errorMessage.Length == 0)
                {
                    flgResult = false;
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                
            }

            return flgResult;
        }

        /// <summary>
        /// データベースからデータの取得してプロパティに設定
        /// 更新用
        /// </summary>
        /// <param name="paramAnkenId"></param>
        /// <returns></returns>
        public async Task GetEditData(int paramAnkenId)
        {
            Context.User user = GetUserData();

            Models.AnkenModel.AnkenDataModelDto dto = new()
            {
                T_Anken = new()
                ,T_Anken_Detail = new()
                ,T_Anken_Remarks = new()
                ,T_Anken_Luggage = new()
                ,T_Anken_PointList = new()
                ,T_Anken_ExchargeList = new()
            };

            try
            {

                API.WebApp.AnkenDataApi api = new();
                dto = await api.GetAnkenData(paramAnkenId);

                API.WebApp.MasterDataApi apiMaster = new();

                /////////////////////////T_Anken//////////////////////////////
                if (AnkenOpenMode != AnkenOpenModeEnum.Copy)
                {
                    AnkenId.Value = dto.T_Anken.Anken_ID;
                    AnkenNo.Value = dto.T_Anken.Anken_No;
                    AnkenStatus.Value = dto.T_Anken.Anken_Status;
                    t_Anken_Locals.Value = dto.T_Anken;
                }

                /////////////////////////T_Anken_Detail//////////////////////////////
                TantouID.Value = (int)dto.T_Anken_Detail.TantouID;
                if (TantouID.Value > 0)
                {
                    TantouName.Value = Context.ContextManager.Instance.companyUserList.FirstOrDefault(m => m.User_ID == TantouID.Value).Last_Name;
                }
                
                EigyoID.Value = (int)dto.T_Anken_Detail.EigyoID;
                if (EigyoID.Value > 0)
                {
                    EigyoName.Value = Context.ContextManager.Instance.companyUserList.FirstOrDefault(m => m.User_ID == EigyoID.Value).Last_Name;
                }
                


                ////請求関連;
                SeikyuKubun.Value = (int)dto.T_Anken_Detail.SeikyuKubun;
                ////顧客情報;
                KokyakuId.Value = (int)dto.T_Anken_Detail.KokyakuId;
                KokyakuData.Value = await apiMaster.M_CustomerData(KokyakuId.Value);


                KokyakuTantouId.Value = (int)dto.T_Anken_Detail.KokyakuTantouId;
                KokyakuTantouName.Value = dto.T_Anken_Detail.KokyakuTantouName;
                KokyakuTantouPhone.Value = dto.T_Anken_Detail.KokyakuTantouPhone;
                ////車種情報;
                Syasyu.Value = dto.T_Anken_Detail.Syasyu;
                SyasyuSize.Value = dto.T_Anken_Detail.SyasyuSize;
                SyasyuDisplay.Value = dto.T_Anken_Detail.SyasyuDisplay;
                Kata.Value = dto.T_Anken_Detail.Kata;
                ////台数;
                Daisuu.Value = (int)dto.T_Anken_Detail.Daisuu;
                ////ルート検索条件;
                Ferry.Value = dto.T_Anken_Detail.Root_Ferry;
                Regulation.Value = dto.T_Anken_Detail.Root_Regulation;
                Twouturn.Value = dto.T_Anken_Detail.Root_Twouturn;
                EigyoshoModori.Value = dto.T_Anken_Detail.Root_EigyoshoModori;
                ///積み降ろし時間
                TsumiTaskTime.Value = dto.T_Anken_Detail.TsumiTaskTime;
                OroshiTaskTime.Value = dto.T_Anken_Detail.OroshiTaskTime;

                CheckOroshiSpace.Value = dto.T_Anken_Detail.CheckOroshiSpace;
                EdnGoBackEigyosyo.Value = dto.T_Anken_Detail.EdnGoBackEigyosyo;

                ////車番連絡;
                NumberCommLimitKubun.Value = (int)dto.T_Anken_Detail.NumberCommLimitKubun;
                if (dto.T_Anken_Detail.NumberCommLimitDateTime != null)
                {
                    NumberCommLimitDate.Value = DateTime.Parse(((DateTime)dto.T_Anken_Detail.NumberCommLimitDateTime).ToString("yyyy/MM/dd"));
                    NumberCommLimitTime.Value = ((DateTime)dto.T_Anken_Detail.NumberCommLimitDateTime).ToString("HH:mm");
                }

                ////選択ルート;
                if (dto.T_Anken_Detail.RouteTypeDisplay != null)
                {
                    SelectedDriveRouteDisplay = new();
                    SelectedDriveRouteDisplay.routeID = dto.T_Anken_Detail.RouteID;
                    SelectedDriveRouteDisplay.routeType = dto.T_Anken_Detail.RouteType.ToString();
                    SelectedDriveRouteDisplay.RouteTypeDisplay = dto.T_Anken_Detail.RouteTypeDisplay;
                    SelectedDriveRouteDisplay.TotalTime = dto.T_Anken_Detail.Route_TotalTime;
                    SelectedDriveRouteDisplay.BreakTime = (int)dto.T_Anken_Detail.Route_BreakTime;
                    SelectedDriveRouteDisplay.RestTime = (int)dto.T_Anken_Detail.Route_RestTime;

                    SelectedDriveRouteDisplay.TotalDistance = (double)dto.T_Anken_Detail.Route_TotalDistance;
                    SelectedDriveRouteDisplay.FuelConsume = (double)dto.T_Anken_Detail.Route_FuelConsume;
                    SelectedDriveRouteDisplay.Totaltoll = (double)dto.T_Anken_Detail.Route_Totaltoll;
                    SelectedDriveRouteDisplay.RestTimeDisplay = dto.T_Anken_Detail.Route_RestTimeDisplay;

                    SelectedDriveRouteDisplay.StdFreight = (double)dto.T_Anken_Detail.Route_StdFreight;
                    SelectedDriveRouteDisplay.StdALLFreight = (double)dto.T_Anken_Detail.Route_StdALLFreight;
                    SelectedDriveRouteDisplay.StdExcharge = (double)dto.T_Anken_Detail.Route_StdExcharge;
                    SelectedDriveRouteDisplay.StdTotalFreight = (double)dto.T_Anken_Detail.Route_StdTotalFreight;
                    SelectedDriveRouteDisplay.GrossAmountForLaborCost = (double)dto.T_Anken_Detail.Route_GrossAmountForLaborCost;
                    SelectedDriveRouteDisplay.GrossAmountForFuelCost = (double)dto.T_Anken_Detail.Route_GrossAmountForFuelCost;
                    SelectedDriveRouteDisplay.GrossAmountForSyaryoCost = (double)dto.T_Anken_Detail.Route_GrossAmountForSyaryoCost;
                    SelectedDriveRouteDisplay.GrossAmountForLuggage = (double)dto.T_Anken_Detail.Route_GrossAmountForLuggage;
                    SelectedDriveRouteDisplay.GrossAmountForExcharge = (double)dto.T_Anken_Detail.Route_GrossAmountForExcharge;
                    SelectedDriveRouteDisplay.GrossAmount = (double)dto.T_Anken_Detail.Route_GrossAmount;
                    SelectedDriveRouteDisplay.GrossAmountTotal = (double)dto.T_Anken_Detail.Route_GrossAmountTotal;
                    SelectedDriveRouteDisplay.TotalDays = (int)dto.T_Anken_Detail.Route_TotalDays;
                    
                }

                ////請求情報;
                BaseFee.Value = (double)dto.T_Anken_Detail.BaseFee;
                ExtraCharge.Value = (double)dto.T_Anken_Detail.ExtraCharge;
                Toll.Value = (double)dto.T_Anken_Detail.Toll;
                Discount.Value = (double)dto.T_Anken_Detail.Discount;
                GrossAmount.Value = (double)dto.T_Anken_Detail.GrossAmount;

                Height.Value = (double)dto.T_Anken_Detail.Height;
                Width.Value = (double)dto.T_Anken_Detail.Width;
                Weight.Value = (double)dto.T_Anken_Detail.Weight;
                Nenpi.Value = (double)dto.T_Anken_Detail.Nenpi;

                ////////////////////////////////T_Anken_Remarks///////////////////////////////////
                if (dto.T_Anken_Remarks != null)
                {
                    Remarks.Value = dto.T_Anken_Remarks.Remarks;
                }

                ////////////////////////////////T_Anken_Luggage///////////////////////////////////
                if (dto.T_Anken_Luggage != null)
                {
                    //Luggage.Value = dto.T_Anken_Luggage.Luggage;
                }

                ////////////////////////////////T_Anken_PointList///////////////////////////////////
                List<T_Anken_Point_Local> list = dto.T_Anken_PointList.Where(m => m.Kubun == 1).OrderBy(m => m.Point_Order).ToList();
                if (list != null && list.Count > 0)
                {
                    TsumiPointList = new();
                    foreach (T_Anken_Point_Local data in list)
                    {
                        PointDto_Local point = new();
                        CopyProperty(point, data);
                        point.PointId = data.Point_Order;
                        TsumiPointList.Add(point);
                    }
                    RaisePropertyChanged(nameof(TsumiPointList));
                }

                list = dto.T_Anken_PointList.Where(m => m.Kubun == 2).OrderBy(m => m.Point_Order).ToList();
                if (list != null && list.Count > 0)
                {
                    ThroughPointList = new();
                    foreach (T_Anken_Point_Local data in list)
                    {
                        PointDto_Local point = new();
                        CopyProperty(point, data);
                        point.PointId = data.Point_Order;
                        ThroughPointList.Add(point);
                    }
                    RaisePropertyChanged(nameof(ThroughPointList));
                }

                list = dto.T_Anken_PointList.Where(m => m.Kubun == 9).OrderBy(m => m.Point_Order).ToList();
                if (list != null && list.Count > 0)
                {
                    OroshiPointList = new();
                    foreach (T_Anken_Point_Local data in list)
                    {
                        PointDto_Local point = new();
                        CopyProperty(point, data);
                        point.PointId = data.Point_Order;
                        OroshiPointList.Add(point);
                    }
                    RaisePropertyChanged(nameof(OroshiPointList));
                }


                /////////////////////////////////T_Anken_OyaKokyakuList///////////////////////////////////
                if (dto.T_Anken_OyaKokyakuList != null && dto.T_Anken_OyaKokyakuList.Count > 0)
                {
                    OyaKokyakuListData = new();
                    foreach (T_Anken_OyaKokyaku_Local data in dto.T_Anken_OyaKokyakuList)
                    {
                        T_Anken_OyaKokyaku_Local local = new();
                        CopyProperty(local, data);
                        OyaKokyakuListData.Add(local);
                    }
                    RaisePropertyChanged(nameof(OyaKokyakuListData));
                }

                /////////////////////////////////T_Anken_ExchargeList///////////////////////////////////
                if (dto.T_Anken_ExchargeList != null && dto.T_Anken_ExchargeList.Count > 0)
                {
                    List<Dto.M_Anken_Excharge_Local> Anken_ExchargeList = await apiMaster.M_Anken_ExchargeList(user.CompanyID, null);

                    ExtraChargeListEx = new();
                    foreach (T_Anken_Excharge_Local data in dto.T_Anken_ExchargeList)
                    {
                        Dto.M_Anken_Excharge_Local master = Anken_ExchargeList.FirstOrDefault(m => m.Komoku_ID == data.Komoku_ID);
                        AnkenExchargeDto_Local local = new();
                        CopyProperty(local, data);
                        local.SIZE = master.SIZE;
                        local.Komoku_Key = master.Komoku_Key;
                        local.SortOrder = master.SortOrder;
                        local.Komoku_Name = master.Komoku_Name;
                        local.Komoku_Name_abbr = master.Komoku_Name_abbr;
                        ExtraChargeListEx.Add(local);
                    }
                    RaisePropertyChanged(nameof(ExtraChargeListEx));
                }

                RaisePropertyChanged(nameof(SelectedDriveRouteDisplay));
                
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("案件データ新規登録エラー：" + ex.Message);
                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// 追加料金リストの行削除処理
        /// </summary>
        /// <param name="dto"></param>
        public void ExtraChargeListExDel(AnkenExchargeDto_Local dto)
        {
            List<AnkenExchargeDto_Local> taget = new();

            AnkenExchargeDto_Local data = ExtraChargeListEx.FirstOrDefault(m => m.Komoku_Key == dto.Komoku_Key);
            if (data != null)
            {
                _ = ExtraChargeListEx.Remove(data);
            }
        }

        /// <summary>
        /// 追加料金リストの更新処理
        /// </summary>
        /// <param name="dto"></param>
        public async Task ExtraChargeListExUpdate()
        {
            Context.User user = GetUserData();
            /////////////////////////////////T_Anken_ExchargeList///////////////////////////////////
            if (ExtraChargeListEx != null && ExtraChargeListEx.Count > 0)
            {
                MasterDataApi api = new();
                List<Dto.M_Anken_Excharge_Local> Anken_ExchargeList = await api.M_Anken_ExchargeList(user.CompanyID, null);
                ObservableCollection<AnkenExchargeDto_Local> targetList = ExtraChargeListEx;

                ExtraChargeListEx = new();
                foreach (AnkenExchargeDto_Local data in targetList)
                {
                    Dto.M_Anken_Excharge_Local master = Anken_ExchargeList.FirstOrDefault(m => m.SIZE == SyasyuSize.Value && m.Komoku_Key == data.Komoku_Key);
                    AnkenExchargeDto_Local target = new();
                    CopyProperty(target, data);
                    target.SIZE = master.SIZE;
                    target.Komoku_Key = master.Komoku_Key;
                    target.SortOrder = master.SortOrder;
                    target.Komoku_Name = master.Komoku_Name;
                    target.Komoku_Name_abbr = master.Komoku_Name_abbr;
                    target.StdExcharge = master.StdExcharge;
                    target.GrossExcharge = master.GrossExcharge;
                    ExtraChargeListEx.Add(target);
                }
                RaisePropertyChanged(nameof(ExtraChargeListEx));
            }


        }

    }


}
