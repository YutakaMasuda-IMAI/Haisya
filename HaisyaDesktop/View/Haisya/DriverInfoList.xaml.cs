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

namespace HaisyaDesktop.View.Haisya
{
    /// <summary>
    /// DriverIchiran.xaml の相互作用ロジック
    /// </summary>
    public partial class DriverInfoList : Window
    {
        public DriverInfoList()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
