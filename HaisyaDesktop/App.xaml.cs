using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Context;
using HaisyaDesktop.Dto;
using HaisyaDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HaisyaDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private Dictionary<Type, Type> ViewModels { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public App() : base()
        {
            // ViewModel と View の対応を設定する
            ViewModels = new Dictionary<Type, Type>();


            ViewModels.Add(typeof(ViewModel.Account.LoginViewModel), typeof(View.Account.Login));
            ViewModels.Add(typeof(ViewModel.MenuViewModel), typeof(View.Menu.Main));

            ViewModels.Add(typeof(ViewModel.Anken.AnkenViewModel), typeof(View.Anken.Register));
            ViewModels.Add(typeof(ViewModel.Anken.AnkenListViewModel), typeof(View.Anken.AnkenList));
            ViewModels.Add(typeof(ViewModel.Anken.DriveRouteListViewModel), typeof(View.Anken.DriveRouteList));
            ViewModels.Add(typeof(ViewModel.Anken.SetOyaKokyakuListViewModel), typeof(View.Anken.SetOyaKokyakuList));
            ViewModels.Add(typeof(ViewModel.Anken.PointEntryViewModel), typeof(View.Anken.PointEntry));
            ViewModels.Add(typeof(ViewModel.Anken.SelectExchargeViewModel), typeof(View.Anken.SelectExcharge));
            ViewModels.Add(typeof(ViewModel.Anken.SetDriveRouteDetailViewModel), typeof(View.Anken.SetDriveRouteDetail));


            ViewModels.Add(typeof(ViewModel.Haisya.HaisyaBookViewModel), typeof(View.Haisya.HaisyaBook));
            ViewModels.Add(typeof(ViewModel.Haisya.HaisyaViewModel), typeof(View.Haisya.Main));
            ViewModels.Add(typeof(ViewModel.Haisya.SyabanRenrakuViewModel), typeof(View.Haisya.SyabanRenraku));
            ViewModels.Add(typeof(ViewModel.Haisya.DriverInfoListViewModel), typeof(View.Haisya.DriverInfoList));
            ViewModels.Add(typeof(ViewModel.Haisya.SyabanPreviewViewModel), typeof(View.Haisya.SyabanPreview));

            ViewModels.Add(typeof(ViewModel.Tool.SelectAddressViewModel), typeof(View.Tool.SelectAddress));
            ViewModels.Add(typeof(ViewModel.Tool.SelectKokyakuViewModel), typeof(View.Tool.SelectKokyaku));
            ViewModels.Add(typeof(ViewModel.Tool.SelectSyasyuKataViewModel), typeof(View.Tool.SelectSyasyuKata));
            ViewModels.Add(typeof(ViewModel.Tool.MailSendViewModel), typeof(View.Tool.MailSend));
            ViewModels.Add(typeof(ViewModel.Tool.SelectKakoAnkenViewModel), typeof(View.Tool.SelectKakoAnken));
            ViewModels.Add(typeof(ViewModel.Tool.SelectTantouViewModel), typeof(View.Tool.SelectTantou));
            ViewModels.Add(typeof(ViewModel.Tool.SelectDaisuViewModel), typeof(View.Tool.SelectDaisu));
            ViewModels.Add(typeof(ViewModel.Tool.SelectTimePickerViewModel), typeof(View.Tool.SelectTimePicker));
            ViewModels.Add(typeof(ViewModel.Tool.SelectAddressDetailViewModel), typeof(View.Tool.SelectAddressDetail));

            ViewModels.Add(typeof(ViewModel.Common.CommonDriveRouteDetailViewModel), typeof(View.Common.CommonDriveRouteDetail));

            ViewModels.Add(typeof(ViewModel.Print.AnkenOrderViewModel), typeof(View.Print.AnkenOrder));

            ViewModels.Add(typeof(ViewModel.DataLoadViewModel), typeof(View.DataLoad));



            //ViewModels.Add(typeof(ViewModel.Print.Report_AnkenOrderViewModel), typeof(View.Print.Report_AnkenOrder));


        }

        /// <summary>
        /// ViewModelからViewを生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Window CreateView<T>(T viewModel)
        {
            // ViewModel に対応する Viewが存在する？
            if (ViewModels.ContainsKey(viewModel.GetType()))
            {
                // View を生成し、DataContext に ViewModel を設定する
                Type viewType = ViewModels[viewModel.GetType()];
                Window wnd = Activator.CreateInstance(viewType) as Window;
                if (wnd != null)
                    wnd.DataContext = viewModel;
                return wnd;
            }
            else
            {
                return null;
            }
        }


        //// ViewModelからUserControlを生成する
        //public UserControl CreateUserControl<T>(T viewModel)
        //{
        //    // ViewModel に対応する Viewが存在する？
        //    if (ViewModels.ContainsKey(viewModel.GetType()))
        //    {
        //        // View を生成し、DataContext に ViewModel を設定する
        //        Type viewType = ViewModels[viewModel.GetType()];
        //        UserControl wnd = Activator.CreateInstance(viewType) as UserControl;
        //        if (wnd != null)
        //            wnd.DataContext = viewModel;
        //        return wnd;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// ViewModelからモーダルでViewを表示する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        /// <param name="BeforeView"></param>
        /// <param name="flgMaximized"></param>
        /// <returns></returns>
        public Window ShowModalView<T>(T viewModel, Window BeforeView, bool flgCenterScreen = false)
        {
            Window view = CreateView(viewModel);
            if (view != null)
            {
                // プライマリ画面かどうかチェック
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(BeforeView).Handle);
                if (!screen.Primary)
                {
                    view.Activate();
                    view.Left = screen.Bounds.Left;
                    view.Top = screen.Bounds.Top;

                    if (view.Height > screen.Bounds.Height)
                    {
                        view.Height = screen.Bounds.Height;
                    }

                    //if (flgMaximized)
                    //{
                    //    view.WindowState = WindowState.Maximized;
                    //}

                    if (flgCenterScreen)
                    {
                        view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    }

                }
                if (view.ShowDialog() == true)
                {
                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// ViewModeからモードレスでViewを表示する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewModel"></param>
        /// <param name="BeforeView"></param>
        /// <param name="flgMaximized"></param>
        public void ShowView<T>(T viewModel, Window BeforeView, bool flgMaximized = true)
        {
            Window view = CreateView(viewModel);
            if (view != null)
            {
                if (BeforeView != null) {
                    // プライマリ画面かどうかチェック
                    System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(BeforeView).Handle);
                    if (!screen.Primary)
                    {
                        ////view.Topmost = true;
                        view.Left = screen.Bounds.Left;
                        view.Top = screen.Bounds.Top;
                    }
                }
                view.Show();
                if (flgMaximized) {
                    // 全画面表示
                    view.WindowState = System.Windows.WindowState.Maximized;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // コンテキストマネージャ情報の初期化
            Context.ContextManager.Initialize();



        }




    }

}
