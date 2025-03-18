using HaisyaDesktop.ViewModel.Anken;
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

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// SelectExcharge.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectExcharge : Window
    {
        private SelectExchargeViewModel VModel => (SelectExchargeViewModel)DataContext;

        public Dto.M_Anken_Excharge_Dto SelectData { get; set; }

        public SelectExcharge()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await VModel.DataLoad();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dto.M_Anken_Excharge_Dto dto = (Dto.M_Anken_Excharge_Dto)lst.SelectedItem;
            SelectData = dto;

            DialogResult = true;
            Close();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
