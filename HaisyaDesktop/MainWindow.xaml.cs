using HaisyaDesktop.Context;
using HaisyaDesktop.View.Haisya;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HaisyaDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;

            //View.Portal.Main view = new();
            View.Menu.Main view = new();

            // プライマリ画面かどうかチェック
            var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            if (!screen.Primary)
            {
                //view.Topmost = true;
                view.Left = screen.Bounds.Left;
                view.Top = screen.Bounds.Top;
            }
            view.Show();
            // 全画面表示
            view.WindowState = System.Windows.WindowState.Maximized;

            me.IsEnabled = true;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

            


            //Button me = (Button)sender;
            //me.IsEnabled = false;

            //View.Print.AnkenOrder view = new();

            //var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            //if (!screen.Primary)
            //{
            //    //view.Topmost = true;
            //    view.Left = screen.Bounds.Left;
            //    view.Top = screen.Bounds.Top;
            //}
            //if (view.ShowDialog() == true)
            //{

            //}


            //App app = App.Current as App;
            //View.Anken.SetOyaKokyakuList win = (View.Anken.SetOyaKokyakuList)app.ShowModalView(new ViewModel.Anken.SetOyaKokyakuListViewModel(null, 0, null,0,null, null), this) ;
            //if (win == null)
            //{
            //}
            //else
            //{
            //    var OyaKokyakuListData = win.OyaKokyakuListData;

            //    Console.WriteLine("aaa");
            //}



            //// プライマリ画面かどうかチェック
            //var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            //if (!screen.Primary)
            //{
            //    //view.Topmost = true;
            //    view.Left = screen.Bounds.Left;
            //    view.Top = screen.Bounds.Top;
            //}
            //view.Show();
            // 全画面表示
            //view.WindowState = System.Windows.WindowState.Maximized;

            //AnkenInfoDialog win = new();
            //win.ShowDialog();

            //me.IsEnabled = true;
        }


        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            // ユーザ情報、セッションクッキーのクリア
            ContextManager.Instance.ClearUserInfo();

            Button me = (Button)sender;

            string[] s = me.Tag.ToString().Split(";");

            Context.User user = new() {
                UserId = int.Parse(s[0].ToString()),
                LoginId = s[1],
                UserName = s[2],
            };

            if (s[3].Length > 0) { user.SyasyuSize = s[3]; }
            if (s[4].Length > 0) { user.Syasyu = s[4]; }
            if (s[5].Length > 0) { user.Kata = s[5]; }
            if (s[6].Length > 0) { user.SyasyuDisplay = s[6]; }

            user.CompanyID = 1;
            user.BranchID = 1;

            ContextManager.Instance.SetUserOnce(user);

            //ContextManager.Instance.User.UserId = int.Parse(s[0].ToString());
            //ContextManager.Instance.User.LoginId = s[1];
            //ContextManager.Instance.User.UserName = s[2];

            //if (s[3].Length > 0) { ContextManager.Instance.User.SyasyuSize = s[3]; }
            //if (s[4].Length > 0) { ContextManager.Instance.User.Syasyu = s[4]; }
            //if (s[5].Length > 0) { ContextManager.Instance.User.Kata = s[5]; }
            //if (s[6].Length > 0) { ContextManager.Instance.User.SyasyuDisplay = s[6]; }

            me.IsEnabled = false;

            ViewModel.MenuViewModel vm = new();

            View.Menu.Main view = new();
            view.DataContext = vm;

            // プライマリ画面かどうかチェック
            var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            if (!screen.Primary)
            {
                //view.Topmost = true;
                view.Left = screen.Bounds.Left;
                view.Top = screen.Bounds.Top;
            }
            view.Show();
            // 全画面表示
            view.WindowState = System.Windows.WindowState.Maximized;

            me.IsEnabled = true;

            Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //// ユーザ情報、セッションクッキーのクリア
            //ContextManager.Instance.ClearUserInfo();
            //// 会社ユーザ情報、セッションクッキーのクリア
            //ContextManager.Instance.ClearCompanyUserInfo();

            ViewModel.Account.LoginViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, this, false);

            Close();
        }




    }
}
