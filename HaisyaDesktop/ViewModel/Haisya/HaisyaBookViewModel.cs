using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using HaisyaDesktop.Models;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HaisyaDesktop.ViewModel.Haisya
{

    public class HaisyaBookViewModel : BaseViewModel
    {
        /// <summary>検索日付</summary>
        public ReactiveProperty<DateTime> TartgetDate { get; set; }

        public ReactiveProperty<int> SelectTantouID { get; set; }
        public ReactiveProperty<string> SelectSyasyu { get; set; }
        public ReactiveProperty<string> SelectKata { get; set; }

        public List<Dto.M_CompanyUser_Local> HaisyaList { get; set; }

        /// <summary>配車担当選択リスト</summary>
        public ObservableCollection<Models.ComboBoxItem> TantouItems { get; set; }

        /// <summary>車種選択リスト</summary>
        public ObservableCollection<Models.ComboBoxItem> SyasyuItems { get; set; }

        /// <summary>型選択リスト</summary>
        public ObservableCollection<Models.ComboBoxItem> KataItems { get; set; }

        ///// <summary>ドライバーリスト</summary>
        //public ObservableCollection<HaisyaBookForDriverData> DriverLists { get; set; }

        public ListCollectionView AnkenList { get; set; }
        public ObservableCollection<Dto.V_AnkenDataList_Local> AnkenDataList { get; set; }

        private ObservableCollection<HaisyaBookForAnkenData> _ankenData;
        public ObservableCollection<HaisyaBookForAnkenData> AnkenData { get => _ankenData; set => SetProperty(ref _ankenData, value); }

        private ObservableCollection<HaisyaBookForAnkenData> _ankenDataaFull;
        public ObservableCollection<HaisyaBookForAnkenData> AnkenDataFull { get => _ankenDataaFull; set => SetProperty(ref _ankenDataaFull, value); }

        public ReactiveProperty<string> SelectOnGrouping { get; set; }

        ///// <summary>ドライバーリスト</summary>
        private ObservableCollection<HaisyaBookForDriverData> _driverData;
        public ObservableCollection<HaisyaBookForDriverData> DriverData { get => _driverData; set => SetProperty(ref _driverData, value); }

        ///// <summary>ドライバーリスト</summary>
        private ObservableCollection<HaisyaBookForDriverData> _driverDataFull;
        public ObservableCollection<HaisyaBookForDriverData> DriverDataFull { get => _driverDataFull; set => SetProperty(ref _driverDataFull, value); }

        /// <summary>案件リスト</summary>
        public ObservableCollection<Dto.V_AnkenDataList_Local> VAnkenDataList { get; set; }

        // IDropTargetを実装したクラス
        //public HaisyaBookDropHandler HaisyaBookDropHandler2 { get; set; } = new HaisyaBookDropHandler();
        public bool IsDragSource { get; set; }
        public bool IsDropTarget { get; set; }

        public readonly int CompanyID;
        public readonly int BranchID;
        public Context.User User { get; set; }


        public HaisyaBookViewModel()
        {
            CompanyID = Context.ContextManager.Instance.User.CompanyID;
            BranchID = Context.ContextManager.Instance.User.BranchID;

            User = GetUserData();

            DriverData = new();

            TartgetDate = new() { Value = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")) };

            SelectTantouID = new();
            SelectSyasyu = new() { Value = "全て" };
            SelectKata = new() { Value = "全て" };
            SelectOnGrouping = new() { Value = "OnGroupingNone" };

            //HaisyaBookDropHandler2 = new HaisyaBookDropHandler();

        }

        /// <summary>
        /// 検索項目データをロードする
        /// </summary>
        /// <returns></returns>
        public async Task DataLoadToSarch()
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


            API.WebApp.MasterDataApi api = new();
            HaisyaList = await api.GetCompanyUserList(User.CompanyID, "tantou");
            Dto.M_CompanyUser_Local user = HaisyaList.FirstOrDefault(m => m.User_ID == User.UserId);
            if (user != null)
            {
                SelectTantouID.Value = user.User_ID;
            } else
            {
                SelectTantouID.Value = 0;
            }

        }

        /// <summary>
        /// ドライバー一覧を取得する
        /// </summary>
        /// <returns></returns>
        public async Task DataLoadForDriverLists()
        {
            DriverDataFull = new();

            API.WebApp.MasterDataApi apiM = new();
            List<Dto.M_SyaryoManagement_Local> syaryo = await apiM.GetSyaryoManagementList(CompanyID);

            API.WebApp.HaisyaDataApi api = new();
            //List<  > dataList = await api.GetProcHeisyaSyaryoData(CompanyID, TartgetDate.Value.ToString("yyyy/MM/dd"));

            //if (dataList != null && dataList.Count > 0)
            //{
            //    foreach(Dto.HeisyaSyaryoDto_Local target in dataList)
            //    {
            //        //Models.DriverList dto = new();
            //        HaisyaBookForDriverData dto = new() { DriverLists = new(), };
            //        CopyProperty(dto.DriverLists, target);
            //        Dto.M_SyaryoManagement_Local syaryo1 = syaryo.FirstOrDefault(m => m.SyaryoManagement_ID == target.Syaryo_ID);
            //        if (syaryo1 != null)
            //        {
            //            dto.DriverLists.Syaryo_Sayban = syaryo1.Syaban_Number;
            //            dto.DriverLists.Syaryo_Sasyu = syaryo1.Syasyu;
            //            dto.DriverLists.Syaryo_Kata = syaryo1.Spec;
            //        }
            //        if (target.Syaryo_ID_Trailer > 0)
            //        {
            //            Dto.M_SyaryoManagement_Local syaryo2 = syaryo.FirstOrDefault(m => m.SyaryoManagement_ID == target.Syaryo_ID_Trailer);
            //            if (syaryo1 != null)
            //            {
            //                dto.DriverLists.Syaryo_Sayban_Trailer = syaryo2.Syaban_Number;
            //                dto.DriverLists.Syaryo_Sasyu_Trailer = syaryo2.Syasyu;
            //                dto.DriverLists.Syaryo_Kata_Trailer = syaryo2.Spec;
            //            }
            //        }

            //        DriverDataFull.Add(dto);
            //    }
            //}
        }

        /// <summary>
        /// 案件一覧データを取得する
        /// </summary>
        /// <returns></returns>
        public async Task DataLoadForAnkenData()
        {
            
            using API.WebApp.AnkenDataApi api = new();
            //AnkenDataList = new ObservableCollection<Dto.V_AnkenDataList_Local>(await api.GetAnkenDataList(TartgetDate.Value.ToString("yyyy/MM/dd"), null, null, CompanyID, BranchID));
           
            AnkenDataFull = new();
            List<Dto.V_AnkenDataList_Local> dataList = await api.GetAnkenDataList(TartgetDate.Value.ToString("yyyy/MM/dd"), null, null, CompanyID, BranchID);

            foreach(var target in dataList)
            {
                HaisyaBookForAnkenData dto = new() { VAnkenDataList = new(), };
                CopyProperty(dto.VAnkenDataList, target);
                AnkenDataFull.Add(dto);
            }

        }

        /// <summary>
        /// 案件一覧データをフィルタとグループ化を実行する
        /// </summary>
        public async void SelectAnkenLists()
        {
            if (SelectTantouID.Value == 0) { return; }

            AnkenData = new();
            if (AnkenDataFull != null)
            {
                AnkenData = new ObservableCollection<HaisyaBookForAnkenData>(AnkenDataFull.Where(m => m.VAnkenDataList.TantouID == SelectTantouID.Value).ToList());
            }



            //AnkenList = null;
            //ObservableCollection<Dto.V_AnkenDataList_Local> p2_result = await Task.Factory.StartNew(GetSelectAnkenDataList);

            //if (p2_result != null)
            //{
            //    ListCollectionView view = new ListCollectionView(p2_result);
            //    if (!"none".Equals(SelectOnGrouping.Value))
            //    {
            //        view.GroupDescriptions.Add(new PropertyGroupDescription(SelectOnGrouping.Value));
            //    }
            //    AnkenList = view;
            //}

            //RaisePropertyChanged(nameof(AnkenList));
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

        /// <summary>
        /// ドライバー一覧データのフィルタを実行する
        /// </summary>
        public void SelectDriverLists()
        {
            if (SelectTantouID.Value == 0) { return; }

            DriverData = new();
            if (DriverDataFull != null)
            {
                DriverData = new ObservableCollection<HaisyaBookForDriverData>(DriverDataFull.Where(m => m.DriverLists.Tantou_ID == SelectTantouID.Value).ToList());
            }
            
        }

        //public void DragOver(IDropInfo dropInfo)
        //{
        //    if (dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource)    // 同じItemのときはDropしません
        //    {
        //        dropInfo.NotHandled = dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource;
        //    }
        //    else
        //    {
        //        dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
        //        dropInfo.Effects = DragDropEffects.Move;
        //    }

        //}

        //public void Drop(IDropInfo dropInfo)
        //{
        //    if (dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource)    // 同じItemのときはDropしません
        //    {
        //        dropInfo.NotHandled = dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource;
        //    }
        //    else
        //    {
        //        int? targetId = null;
        //        string targetText = null;

        //        var dataRow = ((Border)dropInfo.VisualTarget).DataContext;


        //        foreach (var data in _ankenData)
        //        {

        //        }


        //    }
        //}



    }


    /// <summary>
    /// 
    /// </summary>
    public class HaisyaBookForAnkenData : INotifyPropertyChanged
    {

        private Dto.V_AnkenDataList_Local _vAnkenDataList;
        public Dto.V_AnkenDataList_Local VAnkenDataList
        {
            get => _vAnkenDataList;
            set
            {
                _vAnkenDataList = value;
                OnPropertyChanged();
            }
        }

        private bool _isDragSource = true;
        private bool _isDropTarget = true;

        public bool IsDragSource
        {
            get => _isDragSource;
            set
            {
                if (_isDragSource != value)
                {
                    _isDragSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDropTarget
        {
            get => _isDropTarget;
            set
            {
                if (_isDropTarget != value)
                {
                    _isDropTarget = value;
                    OnPropertyChanged();
                }
            }
        }

        //// IDropTargetを実装したクラス
        public HaisyaBookDropHandler HaisyaBookDropHandler { get; set; } = new HaisyaBookDropHandler();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HaisyaBookForDriverData : INotifyPropertyChanged
    {

        //private ObservableCollection<Dto.V_AnkenDataList_Local> _vAnkenDataList;
        //public ObservableCollection<Dto.V_AnkenDataList_Local> VAnkenDataList

        private Models.DriverList _driverLists;
        public Models.DriverList DriverLists
        {
            get => _driverLists;
            set
            {
                _driverLists = value;
                OnPropertyChanged();
            }
        }

        private bool _isDragSource = true;
        private bool _isDropTarget = true;

        public bool IsDragSource
        {
            get => _isDragSource;
            set
            {
                if (_isDragSource != value)
                {
                    _isDragSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDropTarget
        {
            get => _isDropTarget;
            set
            {
                if (_isDropTarget != value)
                {
                    _isDropTarget = value;
                    OnPropertyChanged();
                }
            }
        }

        //// IDropTargetを実装したクラス
        public HaisyaBookDropHandler HaisyaBookDropHandler { get; set; } = new HaisyaBookDropHandler();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    public class HaisyaBookDropHandler : IDropTarget
    {

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource)    // 同じItemのときはDropしません
            {
                dropInfo.NotHandled = dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource;
            }
            else
            {
                dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
                dropInfo.Effects = DragDropEffects.Move;
            }

        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource)    // 同じItemのときはDropしません
            {
                dropInfo.NotHandled = dropInfo.VisualTarget == dropInfo.DragInfo.VisualSource;
            }
            else
            {
                int? targetId = null;
                string targetText = null;

                var dataRow = ((Border)dropInfo.VisualTarget).DataContext;


                // Drop先のデータ処理
                foreach (var child in ((Grid)dropInfo.VisualTarget).Children)
                {
                    if (child.GetType().Equals(typeof(TextBlock)))
                    {
                        // コントロールのDataContextにアクセスして値をセットする
                        if (((TextBlock)child).Name == "SampleId")
                        {
                            targetId = ((SampleItem)((TextBlock)child).DataContext).SampleId;
                            ((SampleItem)((TextBlock)child).DataContext).SampleId = ((SampleItem)dropInfo.Data).SampleId;
                        }

                        if (((TextBlock)child).Name == "SampleText")
                        {
                            targetText = ((SampleItem)((TextBlock)child).DataContext).SampleText;
                            ((SampleItem)((TextBlock)child).DataContext).SampleText = ((SampleItem)dropInfo.Data).SampleText;
                        }
                    }
                }
                ((SampleItem)((Grid)dropInfo.VisualTarget).DataContext).IsDragSource = true;

                //// Drag元のデータ処理
                //if (targetId != null)
                //{
                //    // Darg先と入れ替え
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).SampleId = targetId;
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).SampleText = targetText;
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).IsDragSource = true;
                //}
                //else
                //{
                //    // Drag元を空にする
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).SampleId = null;
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).SampleText = "(なし)";
                //    ((SampleItem)dropInfo.DragInfo.SourceItem).IsDragSource = false;
                //}
            }
        }
    }

}