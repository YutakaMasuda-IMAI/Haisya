using HaisyaDesktop.Dto;
using HaisyaDesktop.ViewModel.Tool;
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
    /// SelectTantou.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectTantou : Window
    {

        public M_CompanyUser_Local SelectData { set; get; }

        public SelectTantou()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private SelectTantouViewModel VModel => (SelectTantouViewModel)DataContext;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await VModel.ViewLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private void TantouListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                M_CompanyUser_Local target = (M_CompanyUser_Local)TantouListBox.SelectedItem;
                SelectData = target;
                DialogResult = true;
                //MessageBox.Show(target.Last_Name);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private void CompanyBranchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                M_CompanyBranch_Local target = (M_CompanyBranch_Local)CompanyBranchListBox.SelectedItem;
                VModel.RefreshView(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }
    }
}
