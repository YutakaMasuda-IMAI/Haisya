using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Context;
using HaisyaDesktop.Dto;
using HaisyaDesktop.ViewModel;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HaisyaDesktop.View
{
    /// <summary>
    /// DataLoad.xaml の相互作用ロジック
    /// </summary>
    public partial class DataLoad : Window
    {

        

        private DispatcherTimer _timer;

        public DataLoad()
        {
            InitializeComponent();

            //DataLoadViewModel vm = new();
            //DataContext = vm;

            // 優先順位を指定してタイマのインスタンスを生成
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 1);

            // タイマメソッドを設定
            _timer.Tick += (e, s) => { TimerMethod(); };

            // 画面が閉じられるときに、タイマを停止
            this.Closing += (e, s) => { _timer.Stop(); };

            _timer.Start();
        }

        private DataLoadViewModel VModel => (DataLoadViewModel)DataContext;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
             * Windowの表示位置をマニュアル指定
             */
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ///*
            // * 表示位置(Top)を調整。
            // * 「ディスプレイの作業領域の高さ」-「表示するWindowの高さ」
            // */
            //this.Top = SystemParameters.WorkArea.Height - this.Height;

            ///*
            // * 表示位置(Left)を調整
            // * 「ディスプレイの作業領域の幅」-「表示するWindowの幅」
            // */
            //this.Left = SystemParameters.WorkArea.Width - this.Width;

            //TimerMethod();

            if (ContextManager.Instance.AddressList == null)
            {
                SetAddressList();
            }

            if (ContextManager.Instance.companyUserList == null)
            {
                SetCompanyUserList();
            }

        }


        private void TimerMethod()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (Context.ContextManager.Instance.AddressList != null && Context.ContextManager.Instance.companyUserList != null)
            {
                _timer.Stop();
                Close();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async void SetAddressList()
        {
            VModel.StatusValue.Value += 10;

            Context.AddressList addressList = new();

            MasterDataApi api = new();
            addressList.HokkaidoAddressItem = await api.GetGetAddressList(1);
            VModel.StatusValue.Value += 10;
            addressList.TohokuAddressItem = await api.GetGetAddressList(2);
            VModel.StatusValue.Value += 10;
            addressList.HokurikuAddressItem = await api.GetGetAddressList(3);
            VModel.StatusValue.Value += 10;
            addressList.ChubuAddressItem = await api.GetGetAddressList(4);
            VModel.StatusValue.Value += 10;
            addressList.KantoAddressItem = await api.GetGetAddressList(5);
            VModel.StatusValue.Value += 10;
            addressList.KinkiAddressItem = await api.GetGetAddressList(6);
            VModel.StatusValue.Value += 10;
            addressList.ChugokuAddressItem = await api.GetGetAddressList(7);
            VModel.StatusValue.Value += 10;
            addressList.ShikokuAddressItem = await api.GetGetAddressList(8);
            VModel.StatusValue.Value += 10;
            addressList.KyusyuAddressItem = await api.GetGetAddressList(9);
            VModel.StatusValue.Value += 10;
            addressList.OkinawaAddressItem = await api.GetGetAddressList(10);
            VModel.StatusValue.Value += 10;
            ContextManager.Instance.SetAddressOnce(addressList);


            //addressList.HokkaidoAddressItem = await SetAddress("北海道");
            //addressList.TohokuAddressItem = await SetAddress("青森県,岩手県,宮城県,秋田県,山形県,福島県");
            //addressList.ChubuAddressItem = await SetAddress("新潟県,富山県,石川県,福井県,岐阜県,長野県,山梨県,静岡県,愛知県");
            //addressList.KantoAddressItem = await SetAddress("東京都,神奈川県,千葉県,埼玉県,群馬県,栃木県,茨城県");
            //addressList.KinkiAddressItem = await SetAddress("京都府,大阪府,兵庫県,奈良県,和歌山県,滋賀県,三重県");
            //addressList.ShikokuAddressItem = await SetAddress("徳島県,香川県,愛媛県,高知県");
            //addressList.KyusyuAddressItem = await SetAddress("福岡県,佐賀県,長崎県,熊本県,大分県,鹿児島県");
            //addressList.OkinawaAddressItem = await SetAddress("沖縄県");
            //addressList.ChugokuAddressItem = await SetAddress("広島県,山口県,島根県,鳥取県,岡山県");

            //Task task1 = Task.Run(() => { addressList.HokkaidoAddressItem = SetAddress("北海道"); });
            //Task task2 = Task.Run(() => { addressList.TohokuAddressItem = SetAddress("青森県,岩手県,宮城県,秋田県,山形県,福島県"); });
            //Task task3 = Task.Run(() => { addressList.ChubuAddressItem = SetAddress("新潟県,富山県,石川県,福井県,岐阜県,長野県,山梨県,静岡県,愛知県"); });
            //Task task4 = Task.Run(() => { addressList.KantoAddressItem = SetAddress("東京都,神奈川県,千葉県,埼玉県,群馬県,栃木県,茨城県"); });
            //Task task5 = Task.Run(() => { addressList.KinkiAddressItem = SetAddress("京都府,大阪府,兵庫県,奈良県,和歌山県,滋賀県,三重県"); });
            //Task task6 = Task.Run(() => { addressList.ShikokuAddressItem = SetAddress("徳島県,香川県,愛媛県,高知県"); });
            //Task task7 = Task.Run(() => { addressList.KyusyuAddressItem = SetAddress("福岡県,佐賀県,長崎県,熊本県,大分県,鹿児島県"); });
            //Task task8 = Task.Run(() => { addressList.OkinawaAddressItem = SetAddress("沖縄県"); });
            //Task task9 = Task.Run(() => { addressList.ChugokuAddressItem = SetAddress("広島県,山口県,島根県,鳥取県,岡山県"); });

            //Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8, task9);
            //ContextManager.Instance.SetAddressOnce(addressList);
        }

        

        /// <summary>
        /// 
        /// </summary>
        private async void SetCompanyUserList()
        {
            API.WebApp.MasterDataApi api = new();
            List<M_CompanyUser_Local> companyUserList = new();
            companyUserList = await api.GetCompanyUserList(Context.ContextManager.Instance.User.CompanyID, "all");
            if (companyUserList != null)
            {
                ContextManager.Instance.SetCompanyUserOnce(companyUserList.ToList());
            }
            VModel.StatusValue.Value += 10;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Area"></param>
        ///// <returns></returns>
        //private async Task<IEnumerable<M_PostCode_Local>> SetAddress(int iArea)
        //{
        //VModel.StatusValue.Value += 10;
        //    MasterDataApi api = new();
        //    return await api.GetGetAddressList(iArea);
        //}


    }
}


namespace HaisyaDesktop.ViewModel
{

    public class DataLoadViewModel : BaseViewModel
    {

        public ReactiveProperty<int> StatusValue { get; set; }

        //public ReactiveProperty<string> StatusContent { get; set; }

        public DataLoadViewModel()
        {
            StatusValue = new() { Value = 0 };
        }

        //public void ChangeValStatusBar()
        //{
        //    //StatusValue.Value += 10;
        //    //RaiseErrorsChanged(nameof(StatusValue));
        //}
    }

}