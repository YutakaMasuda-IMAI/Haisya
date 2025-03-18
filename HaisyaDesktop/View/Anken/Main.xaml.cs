using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
using HaisyaDesktop.API.Map;
using HaisyaDesktop.ViewModel.Anken;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class Main : Window
    {


        public Main()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AnkenViewModel vm = new();
            DataContext = vm;


        }


        private AnkenViewModel VModel => (AnkenViewModel)DataContext;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }



}
