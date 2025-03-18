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

namespace HaisyaDesktop.View.Tool
{
    /// <summary>
    /// SelectDaisu.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectDaisu : Window
    {

        public int SelectData { set; get; }

        public SelectDaisu()
        {
            InitializeComponent();

            //SelectDaisuViewModel vm = new();
            //DataContext = vm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int item = (int)lstDaisu.SelectedItem;
            SelectData = item;

            //MessageBox.Show(item.ToString());

            DialogResult = true;
            Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
