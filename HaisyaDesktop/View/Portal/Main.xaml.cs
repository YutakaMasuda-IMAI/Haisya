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

namespace HaisyaDesktop.View.Portal
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnTopMenu0_Click(object sender, RoutedEventArgs e)
        {

            View.Menu.Main view = new();
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

        }

        private void btnTopMenu4_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTopMenu1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
