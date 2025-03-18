using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Dto;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.ViewModel.Tool
{

    public enum SelectAddressViewModelKubun
    {
        Address,
        SearchKey,
        Point,
    };

    public enum SelectAddressViewModelSelectKomoku
    {
        Area,
        ken,
        ShikuCho,
        Ooaza,
    };

    internal class SelectAddressViewModel : BaseViewModel
    {

        public AsyncReactiveCommand ButtonClickAsyncCommand1 { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand ButtonClickAsyncCommand2 { get; } = new AsyncReactiveCommand();

        public ObservableCollection<SelectAddressItem> Area { get; set; }
        public ReactiveProperty<string> SelectArea { get; set; }
        public ReactiveProperty<string> SelectAreaForSearchKey { get; set; }
        public ReactiveProperty<string> SelectAreaForPoint { get; set; }

        public ObservableCollection<SelectAddressItem> KenForPoint { get; set; }
        public ReactiveProperty<string> SelectKenForPoint { get; set; }

        public ObservableCollection<SelectAddressItem> KenForSearchKey { get; set; }
        public ReactiveProperty<string> SelectKenForSearchKey { get; set; }

        public ObservableCollection<SelectAddressItem> Ken { get; set; }
        public ReactiveProperty<string> SelectKen { get; set; }

        public ObservableCollection<SelectAddressItem> Shiku { get; set; }
        public ReactiveProperty<SelectAddressItem> SelectShiku { get; set; }

        public ObservableCollection<SelectAddressItem> Chyo { get; set; }
        public ReactiveProperty<SelectAddressItem> SelectChyo { get; set; }

        public ObservableCollection<SelectAddressItem> MostPastPoint { get; set; }
        public ReactiveProperty<string> SelectMostPastPoint { get; set; }

        public ObservableCollection<SelectAddressItem> MostRecent { get; set; }
        public ReactiveProperty<string> SelectMostRecent { get; set; }

        public ObservableCollection<SelectAddressItem> RegFacility { get; set; }
        public ReactiveProperty<string> SelectRegFacility { get; set; }

        public ObservableCollection<SelectAddressItemForKey> SearchAddressList { get; set; }
        public ObservableCollection<SelectAddressItemForKey> SearchAddressListFull { get; set; }

        public ObservableCollection<Map_Building_NameItem_Local> SearchTatemonoList { get; set; }
        public ObservableCollection<Map_Building_NameItem_Local> SearchTatemonoListFull { get; set; }

        public ObservableCollection<Dto.T_Point_Local> PointList { get; set; }
        public ObservableCollection<Dto.T_Point_Local> PointListFull { get; set; }

        public List<M_PostCode_Local> ShiKuChoList { get; set; }

        private IEnumerable<M_PostCode_Local> PostAddressCodeOfArea { get; set; }



        public SelectAddressViewModel()
        {
            ButtonClickAsyncCommand1.Subscribe(async _ => { await Task.Delay(500); });
            ButtonClickAsyncCommand2.Subscribe(async _ => { await Task.Delay(500); });


            Area = new();
            SelectAreaForSearchKey = new();
            SelectAreaForPoint = new();
            Ken = new();
            KenForSearchKey = new();
            KenForPoint = new();
            Shiku = new();
            Chyo = new();
            SelectArea = new();
            SelectKen = new();
            SelectShiku = new();
            SelectChyo = new();

            SelectKenForPoint = new();
            SelectKenForSearchKey = new();

            SearchAddressList = new();
            SearchAddressListFull = new();
            SearchTatemonoList = new();
            SearchTatemonoListFull = new();

            MostPastPoint = new();
            MostRecent = new();
            RegFacility = new();

            PointList = new();

            AddAreaList();
            //CreateRegFacilityList();
        }

        private void AddAreaList()
        {
            Area = new();
            Area.Add(new SelectAddressItem { Name = "北海道" });
            Area.Add(new SelectAddressItem { Name = "東北" });
            Area.Add(new SelectAddressItem { Name = "北陸" });
            Area.Add(new SelectAddressItem { Name = "関東" });
            Area.Add(new SelectAddressItem { Name = "中部" });
            Area.Add(new SelectAddressItem { Name = "近畿" });
            Area.Add(new SelectAddressItem { Name = "中国" });
            Area.Add(new SelectAddressItem { Name = "四国" });
            Area.Add(new SelectAddressItem { Name = "九州" });
            Area.Add(new SelectAddressItem { Name = "沖縄" });
        }

        /// <summary>
        /// エリアリスト選択後　対象エリアの県を抽出
        /// </summary>
        public void AfterForSelectArea(SelectAddressViewModelKubun kubun, string area)
        {
            ObservableCollection<SelectAddressItem> KenItems = new();

            switch (area)
            {
                case "中国":
                    KenItems.Add(new SelectAddressItem { Name = "広島県" });
                    KenItems.Add(new SelectAddressItem { Name = "山口県" });
                    KenItems.Add(new SelectAddressItem { Name = "島根県" });
                    KenItems.Add(new SelectAddressItem { Name = "鳥取県" });
                    KenItems.Add(new SelectAddressItem { Name = "岡山県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.ChugokuAddressItem;
                    break;
                case "沖縄":
                    KenItems.Add(new SelectAddressItem { Name = "沖縄県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.OkinawaAddressItem;
                    break;
                case "四国":
                    KenItems.Add(new SelectAddressItem { Name = "徳島県" });
                    KenItems.Add(new SelectAddressItem { Name = "香川県" });
                    KenItems.Add(new SelectAddressItem { Name = "愛媛県" });
                    KenItems.Add(new SelectAddressItem { Name = "高知県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.ShikokuAddressItem;
                    break;
                case "近畿":
                    KenItems.Add(new SelectAddressItem { Name = "京都府" });
                    KenItems.Add(new SelectAddressItem { Name = "大阪府" });
                    KenItems.Add(new SelectAddressItem { Name = "兵庫県" });
                    KenItems.Add(new SelectAddressItem { Name = "奈良県" });
                    KenItems.Add(new SelectAddressItem { Name = "和歌山県" });
                    KenItems.Add(new SelectAddressItem { Name = "滋賀県" });
                    KenItems.Add(new SelectAddressItem { Name = "三重県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.KinkiAddressItem;
                    break;
                case "中部":
                    KenItems.Add(new SelectAddressItem { Name = "福井県" });
                    KenItems.Add(new SelectAddressItem { Name = "岐阜県" });
                    KenItems.Add(new SelectAddressItem { Name = "山梨県" });
                    KenItems.Add(new SelectAddressItem { Name = "静岡県" });
                    KenItems.Add(new SelectAddressItem { Name = "愛知県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.ChubuAddressItem;
                    break;
                case "北陸":
                    KenItems.Add(new SelectAddressItem { Name = "新潟県" });
                    KenItems.Add(new SelectAddressItem { Name = "富山県" });
                    KenItems.Add(new SelectAddressItem { Name = "石川県" });
                    KenItems.Add(new SelectAddressItem { Name = "長野県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.HokurikuAddressItem;
                    break;
                case "関東":
                    KenItems.Add(new SelectAddressItem { Name = "東京都" });
                    KenItems.Add(new SelectAddressItem { Name = "神奈川県" });
                    KenItems.Add(new SelectAddressItem { Name = "千葉県" });
                    KenItems.Add(new SelectAddressItem { Name = "埼玉県" });
                    KenItems.Add(new SelectAddressItem { Name = "群馬県" });
                    KenItems.Add(new SelectAddressItem { Name = "栃木県" });
                    KenItems.Add(new SelectAddressItem { Name = "茨城県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.KantoAddressItem;
                    break;
                case "東北":
                    KenItems.Add(new SelectAddressItem { Name = "青森県" });
                    KenItems.Add(new SelectAddressItem { Name = "岩手県" });
                    KenItems.Add(new SelectAddressItem { Name = "宮城県" });
                    KenItems.Add(new SelectAddressItem { Name = "秋田県" });
                    KenItems.Add(new SelectAddressItem { Name = "山形県" });
                    KenItems.Add(new SelectAddressItem { Name = "福島県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.TohokuAddressItem;
                    break;
                case "九州":
                    KenItems.Add(new SelectAddressItem { Name = "福岡県" });
                    KenItems.Add(new SelectAddressItem { Name = "佐賀県" });
                    KenItems.Add(new SelectAddressItem { Name = "長崎県" });
                    KenItems.Add(new SelectAddressItem { Name = "熊本県" });
                    KenItems.Add(new SelectAddressItem { Name = "宮崎県" });
                    KenItems.Add(new SelectAddressItem { Name = "大分県" });
                    KenItems.Add(new SelectAddressItem { Name = "鹿児島県" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.KyusyuAddressItem;
                    break;
                case "北海道":
                    KenItems.Add(new SelectAddressItem { Name = "北海道" });
                    PostAddressCodeOfArea = Context.ContextManager.Instance.AddressList.HokkaidoAddressItem;
                    break;

            }

            switch(kubun)
            {
                case SelectAddressViewModelKubun.Address:
                    SelectArea.Value = area;
                    Ken = KenItems;
                    RaisePropertyChanged(nameof(Ken));
                    break;
                case SelectAddressViewModelKubun.SearchKey:
                    SelectAreaForSearchKey.Value = area;
                    KenForSearchKey = KenItems;
                    RaisePropertyChanged(nameof(KenForSearchKey));
                    break;
                case SelectAddressViewModelKubun.Point:
                    SelectAreaForPoint.Value = area;
                    KenForPoint = KenItems;
                    RaisePropertyChanged(nameof(KenForPoint));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 県リスト選択後　対象県の地区町村を抽出
        /// </summary>
        public void AfterForSelectKen()
        {

            Task<ObservableCollection<SelectAddressItem>> task1 = Task.Run(() =>
            {
                ObservableCollection<SelectAddressItem> ShikuList = new();
                IEnumerable<M_PostCode_Local> list = ShiKuChoList.Where(m => m.KEN == SelectKen.Value && m.POSTAL_CODE.Substring(5, 2) == "00").ToList();
                foreach (M_PostCode_Local data in list)
                {
                    ShikuList.Add(new SelectAddressItem { Name = data.SHI_KU_CHO, Address = data.SHI_KU_CHO, NameKana = data.SHI_KU_CHO_KANA, PostCode = data.POSTAL_CODE });
                }

                return ShikuList;
            });

            Task.WaitAll(task1);

            Shiku = task1.Result;
            RaisePropertyChanged(nameof(Shiku));

        }

        /// <summary>
        /// 地区町村リスト選択後　対象県の町域を抽出
        /// </summary>
        public void AfterForSelectShiku()
        {

            Task<ObservableCollection<SelectAddressItem>> task1 = Task.Run(() =>
            {
                ObservableCollection<SelectAddressItem> ChiikiList = new();
                IEnumerable<M_PostCode_Local> list = ShiKuChoList.Where(m => m.KEN == SelectKen.Value && m.SHI_KU_CHO == SelectShiku.Value.Address && m.POSTAL_CODE.Substring(3, 4) != "0000").ToList();
                foreach (M_PostCode_Local data in list)
                {
                    ChiikiList.Add(new SelectAddressItem { Name = data.CHO_IKI, Address = data.CHO_IKI, NameKana = data.CHO_IKI_KANA, PostCode = data.POSTAL_CODE });
                }
                return ChiikiList;
            });


            Task.WaitAll(task1);

            Chyo = task1.Result;
            RaisePropertyChanged(nameof(Chyo));

        }

        /// <summary>
        /// 町域リスト選択後
        /// </summary>
        public void AfterForSelectChyo()
        {

        }


        /// <summary>
        /// 県リスト抽出後、エリアで抽出した郵便住所リストを県で絞り込む
        /// </summary>
        /// <returns></returns>
        public async Task GetShiKuChoDataList()
        {
            Task<List<M_PostCode_Local>> task1 = Task.Run(() =>
            {
                return PostAddressCodeOfArea.Where(m => m.KEN == SelectKen.Value).ToList();
            });
            _ = await Task.WhenAll(task1);
            ShiKuChoList = task1.Result;
        }


        public void CreateRegFacilityList()
        {


            RegFacility.Add(new SelectAddressItem { Name = "〇〇工場", Address = "〇〇県〇〇市〇〇町" });
            RegFacility.Add(new SelectAddressItem { Name = "〇〇工場 〇〇支社", Address = "〇〇県〇〇市〇〇町" });
            RegFacility.Add(new SelectAddressItem { Name = "〇〇倉庫", Address = "〇〇県〇〇市〇〇町" });
            RegFacility.Add(new SelectAddressItem { Name = "〇〇センター", Address = "〇〇県〇〇市〇〇町" });
            RegFacility.Add(new SelectAddressItem { Name = "〇〇現場", Address = "〇〇県〇〇市〇〇町" });


            MostRecent.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostRecent.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostRecent.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostRecent.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostRecent.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });

            MostPastPoint.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostPastPoint.Add(new SelectAddressItem { Name = "〇〇工場", Address = "〇〇県〇〇市〇〇町" });
            MostPastPoint.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
            MostPastPoint.Add(new SelectAddressItem { Name = "〇〇倉庫", Address = "〇〇県〇〇市〇〇町" });
            MostPastPoint.Add(new SelectAddressItem { Name = "", Address = "〇〇県〇〇市〇〇町" });
        }

        /// <summary>
        /// 名称検索タブの名称入力後の抽出処理
        /// </summary>
        /// <param name="key"></param>
        public async void SetAddressListForKey(string key)
        {
            SearchAddressList = new();
            SearchAddressListFull = new();

            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.ChugokuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.KinkiAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.KyusyuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.ShikokuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.ChubuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.KantoAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.TohokuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.HokurikuAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.HokkaidoAddressItem);
            AddSearchAddressList(key, Context.ContextManager.Instance.AddressList.OkinawaAddressItem);

            SearchAddressListFull = SearchAddressList;
            RaisePropertyChanged(nameof(SearchAddressList));
            RaisePropertyChanged(nameof(SearchAddressListFull));

            SearchTatemonoList = new();
            SearchTatemonoListFull = new();

            API.Map.AddressApi api = new();
            Map_Building_Name_Local dataList = await api.GetBuildingNameAsync(null, key);
            if (dataList.ErrrMessage == null)
            {
                foreach (var item in dataList.item)
                {
                    Map_Building_NameItem_Local data = new() { position = new(), };
                    CopyProperty(data, item);
                    SearchTatemonoList.Add(data);
                }
            }
            SearchTatemonoListFull = SearchTatemonoList;
            RaisePropertyChanged(nameof(SearchTatemonoList));
            RaisePropertyChanged(nameof(SearchTatemonoListFull));
        }

        /// <summary>
        /// 引数の住所リストを「SearchAddressList」に追加する
        /// </summary>
        /// <param name="key"></param>
        /// <param name="target"></param>
        private void AddSearchAddressList(string key, IEnumerable<M_PostCode_Local> target)
        {
            IEnumerable<M_PostCode_Local> list = target.Where(m => m.SHI_KU_CHO.Contains(key) || m.CHO_IKI.Contains(key)).ToList();

            if (list != null && list.Count() > 0)
            {
                foreach(var data in list)
                {
                    SearchAddressList.Add(new SelectAddressItemForKey { Name = data.KEN + data.SHI_KU_CHO + data.CHO_IKI, Ken = data.KEN, 
                        Shiku = data.SHI_KU_CHO, Cho = data.CHO_IKI, Address = data.KEN + ";" + data.SHI_KU_CHO + ";" + data.CHO_IKI, 
                        NameKana = data.SHI_KU_CHO_KANA + data.CHO_IKI_KANA, PostCode = data.POSTAL_CODE });

                }
            }

        }

        /// <summary>
        /// 地図登録ポイントリストのセット
        /// </summary>
        public async Task SetPointList()
        {
            Context.User user = GetUserData();

            PointList = new();
            PointListFull = new();
            API.WebApp.AnkenDataApi api = new();
            List<T_Point_Local> dataList = await api.T_PointListFromUserId(user.UserId, 0);
            foreach(T_Point_Local data in dataList)
            {
                PointList.Add(data);
            }
            PointListFull = PointList;
            RaisePropertyChanged(nameof(PointList));
            RaisePropertyChanged(nameof(PointListFull));
        }

        public void AreaForSearchKey(SelectAddressViewModelSelectKomoku komoku) 
        {
            
            List<SelectAddressItemForKey> address = new(SearchAddressListFull);
            List<Map_Building_NameItem_Local> tatemono = new(SearchTatemonoListFull);

            switch (komoku)
            {
                case SelectAddressViewModelSelectKomoku.Area:
                    address = SearchAddressListFull.Where(m => KenForSearchKey.Select(m => m.Name).Contains(m.Ken)).ToList();
                    tatemono = SearchTatemonoListFull.Where(m => KenForSearchKey.Select(m => m.Name).Contains(m.address2)).ToList();
                    break;
                case SelectAddressViewModelSelectKomoku.ken:
                    address = SearchAddressListFull.Where(m => m.Ken == SelectKenForSearchKey.Value).ToList();
                    tatemono = SearchTatemonoListFull.Where(m => m.address2 == SelectKenForSearchKey.Value).ToList();
                    break;
                default:
                    break;
            }


            SearchAddressList = new();
            SearchTatemonoList = new();
            foreach (var data in address) { SearchAddressList.Add(data); }
            foreach (var data in tatemono) { SearchTatemonoList.Add(data); }

            RaisePropertyChanged(nameof(SearchAddressList));
            RaisePropertyChanged(nameof(SearchTatemonoList));
        }

        public void AreaForPoint(SelectAddressViewModelSelectKomoku komoku)
        {
            List<Dto.T_Point_Local> point = new(PointList);

            switch (komoku)
            {
                case SelectAddressViewModelSelectKomoku.Area:
                    point = PointListFull.Where(m => KenForPoint.Select(m => m.Name).Contains(m.Address2)).ToList();
                    break;
                case SelectAddressViewModelSelectKomoku.ken:
                    point = PointListFull.Where(m => m.Address2 == SelectKenForSearchKey.Value).ToList();
                    break;
                default:
                    break;
            }

            PointList = new();
            foreach (var data in point) { PointList.Add(data); }

            RaisePropertyChanged(nameof(PointList));
        }
    }



    public class SelectAddressItem
    {

        public string Name { set; get; }

        public string NameKana { set; get; }

        public string Address { set; get; }

        public string PostCode { set; get; }

    }


    public class SelectAddressItemForKey
    {

        public string Name { set; get; }

        public string NameKana { set; get; }

        public string Address { set; get; }

        public string Ken { set; get; }

        public string Shiku { set; get; }

        public string Cho { set; get; }

        public string PostCode { set; get; }

    }


}
