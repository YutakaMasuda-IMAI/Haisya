using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.ViewModel.Anken
{

    class AnkenListViewModel: BaseViewModel
    {

        public int Mode { get; set; }

        public ReactiveProperty<DateTime> TartgetDate { get; set; }

        public ReactiveProperty<int> SelectTantouID { get; set; }
        public ReactiveProperty<string> SelectSyasyu { get; set; }
        public ReactiveProperty<string> SelectKata { get; set; }

        //public ObservableCollection<V_AnkenDataList_Local> AnkenList { get; set; }

        public ListCollectionView AnkenList { get; set; }

        public IEnumerable<Dto.V_AnkenDataList_Local> AnkenDataList { get; set; }

        /// <summary>案件ステータス選択</summary>
        public ObservableCollection<Models.ComboBoxItem> TantouItems { get; set; }

        /// <summary>車種選択</summary>
        public ObservableCollection<Models.ComboBoxItem> SyasyuItems { get; set; }

        /// <summary>型選択</summary>
        public ObservableCollection<Models.ComboBoxItem> KataItems { get; set; }


        public ReactiveProperty<string> SelectOnGrouping { get; set; }

        public readonly int CompanyID;

        public readonly int BranchID;

        public readonly int UserId;


        public AnkenListViewModel()
        {

            CompanyID = Context.ContextManager.Instance.User.CompanyID;
            BranchID = Context.ContextManager.Instance.User.BranchID;
            UserId = Context.ContextManager.Instance.User.UserId;
            SelectTantouID = new() { Value = UserId };

            SelectSyasyu = new() { Value = "全て" };
            SelectKata = new() { Value = "全て" };
            SelectOnGrouping = new() { Value = "AnkenStatusDisplay" };

            TartgetDate = new() { Value = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")) };

            SelectTantouID.Subscribe(x => SelectAnkenData());
            SelectSyasyu.Subscribe(x => SelectAnkenData());
            SelectKata.Subscribe(x => SelectAnkenData());
        }

        /// <summary>
        /// フォームのデータロード処理
        /// </summary>
        /// <returns></returns>
        public async Task FormLoad()
        {
            TantouItems = new();
            TantouItems = Service.SearchCommonService.GetTantouSelect(true);
            RaisePropertyChanged(nameof(TantouItems));

            SyasyuItems = new();
            SyasyuItems = await Service.SearchCommonService.GetSyasyuSelect(CompanyID, true);
            RaisePropertyChanged(nameof(SyasyuItems));

            KataItems = new();
            KataItems = await Service.SearchCommonService.GetSyasyuSelect(CompanyID, true);
            RaisePropertyChanged(nameof(KataItems));

            await GetAnkenData();
        }

        /// <summary>
        /// 案件一覧を取得し直す
        /// </summary>
        /// <returns></returns>
        public async Task GetAnkenData()
        {
            AnkenList = null;
            using API.WebApp.AnkenDataApi api = new();
            AnkenDataList = await api.GetAnkenDataList(TartgetDate.Value.ToString("yyyy/MM/dd"), null, null, CompanyID, BranchID);
            if (AnkenDataList.Count() > 0)
            {
                SelectAnkenData();
            } else
            {
                RaisePropertyChanged(nameof(AnkenList));
            }
        }

        /// <summary>
        /// 案件データをフィルタとグループ化を実行する
        /// </summary>
        public async void SelectAnkenData()
        {
            ObservableCollection<Dto.V_AnkenDataList_Local> p2_result = await Task.Factory.StartNew(GetSelectAnkenDataList);

            if (p2_result != null) { 
                ListCollectionView view = new ListCollectionView(p2_result);
                if (!"none".Equals(SelectOnGrouping.Value))
                {
                    view.GroupDescriptions.Add(new PropertyGroupDescription(SelectOnGrouping.Value));
                }
                AnkenList = view;
            }

            RaisePropertyChanged(nameof(AnkenList));
        }

        /// <summary>
        /// 案件一覧データのフィルタを実行する
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Dto.V_AnkenDataList_Local> GetSelectAnkenDataList()
        {
            if (AnkenDataList == null) { return null; }

            ObservableCollection<Dto.V_AnkenDataList_Local> result = new();

            List<Dto.V_AnkenDataList_Local> list = AnkenDataList.ToList();

            if (SelectTantouID.Value > 0)
            {
                list = AnkenDataList.Where(x => x.TantouID == SelectTantouID.Value).ToList();
            }
            if (!"全て".Equals(SelectSyasyu.Value))
            {
                list = AnkenDataList.Where(x => x.Syasyu == SelectSyasyu.Value).ToList();
            }
            if (!"全て".Equals(SelectKata.Value))
            {
                list = AnkenDataList.Where(x => x.Kata == SelectKata.Value).ToList();
            }

            foreach (Dto.V_AnkenDataList_Local data in list)
            {
                Dto.V_AnkenDataList_Local dto = new();
                CopyProperty(dto, data);
                result.Add(dto);
            }

            return result;

        }





    }
}
