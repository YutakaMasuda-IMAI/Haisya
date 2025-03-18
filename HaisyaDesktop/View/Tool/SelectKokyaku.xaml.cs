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
    /// SelectKokyaku.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectKokyaku : Window
    {

        public Dto.M_Customer_Local SelectData { get; set; }


        public SelectKokyaku()
        {
            InitializeComponent();


            SelectKokyakuViewModel vm = new();
            DataContext = vm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private SelectKokyakuViewModel VModel => (SelectKokyakuViewModel)DataContext;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            MessageBox.Show(button.Content.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstKokyakuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dto.M_Customer_Local selectItem = (Dto.M_Customer_Local)lstKokyakuList.SelectedItem;
                SelectData = selectItem;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Maximized;
            try
            {
                await VModel.DataLoad();
            } catch(Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private async void btnKey_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string name = (string)button.Tag;

                await VModel.DataLoadForKey(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private async void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) { return; }
                TextBox tb = (TextBox)sender;
                await VModel.DataLoadForKey(tb.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private async void txtKokyakuCD_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) { return; }
                TextBox tb = (TextBox)sender;

                await VModel.DataLoadForCode(tb.Text);
            } catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private void lstMostRecentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dto.M_Customer_Local selectItem = (Dto.M_Customer_Local)lstMostRecentList.SelectedItem;
                SelectData = selectItem;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private void lstMostPastPointList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dto.M_Customer_Local selectItem = (Dto.M_Customer_Local)lstMostPastPointList.SelectedItem;
                SelectData = selectItem;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー：" + ex.Message);
            }
        }
    }
}
