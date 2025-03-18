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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HaisyaDesktop.View.Haisya
{
    /// <summary>
    /// UserControl2.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        private void NowPointInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aaaa");
        }

        private void NowPointInfo_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            MessageBox.Show("bbbb");
        }

        private void DataGrid_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {

        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
    }
}
