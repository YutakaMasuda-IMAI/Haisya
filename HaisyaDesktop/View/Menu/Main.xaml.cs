using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HaisyaDesktop.View.Menu
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class Main : Window
    {
        private DispatcherTimer _timer;

        public Main()
        {
            InitializeComponent();

            //BackGroundExec();

            // 優先順位を指定してタイマのインスタンスを生成
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            // タイマメソッドを設定
            _timer.Tick += (e, s) => { TimerMethod(); };

            // 画面が閉じられるときに、タイマを停止
            this.Closing += (e, s) => { _timer.Stop(); };

            _timer.Start();
        }


        private ViewModel.MenuViewModel VModel => (ViewModel.MenuViewModel)DataContext;


        private void TimerMethod()
        {
            _timer.Stop();

            OpenDataLoad();

        }



        //private void BackGroundExec()
        //{
        //    var t = new Thread(_ =>
        //    {
        //        View.DataLoad dataLoad = new();
        //        dataLoad.Closed += (_, __) =>
        //        {
        //            // Window が閉じたら Dispatcher を終了
        //            Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.SystemIdle);
        //            Dispatcher.Run();
        //        };
        //        dataLoad.ShowDialog();
        //    });
        //    t.SetApartmentState(ApartmentState.STA); // 必須
        //    t.IsBackground = true;
        //    t.Start();
        //}

        private void OpenDataLoad()
        {
            ViewModel.DataLoadViewModel vm = new();
            App app = App.Current as App;
            app.ShowModalView(vm, this, true);
        }



        private void btnAnkenReg_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Anken.AnkenViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, false);
        }

        private void btnAnkenList_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Anken.AnkenListViewModel vm = new();
            vm.Mode = 1;
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void btnAnkenEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Anken.AnkenListViewModel vm = new();
            vm.Mode = 2;
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void BtnAnkenShare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HaisyaList_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Haisya.DriverInfoListViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void HaisyaReg_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Haisya.HaisyaViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void SyabanRenraku_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Haisya.SyabanRenrakuViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void KusyaShare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTopMenu0_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void btnTopMenu4_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //OpenDataLoad();

            //await VModel.SetAddressList();

        }

        private void HaisyaTable_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Haisya.HaisyaBookViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

            //View.Haisya.Test view = new();
            //view.Show();

            ViewModel.Anken.SetDriveRouteDetailViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, true);


        }
    }
}
