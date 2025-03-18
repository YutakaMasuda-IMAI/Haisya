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
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.View.Tool
{
    /// <summary>
    /// SelectAddressDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectAddressDetail : Window
    {

        public Map_Building_NameItem_Local SelectBuildingData { set; get; }

        public MapAddressItem_Local SelectAddressData { set; get; }

        public SelectAddressDetail()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Map_Building_NameItem_Local selectData = (Map_Building_NameItem_Local)ListBox.SelectedItem;

            if (selectData != null)
            {
                SelectBuildingData = selectData;
                SelectAddressData = null;
                DialogResult = true;
                Close();
            }
        }

        private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MapAddressItem_Local selectData = (MapAddressItem_Local)ListBox2.SelectedItem;

            if (selectData != null)
            {
                if (InputbuildName.Text != null && InputbuildName.Text.Length > 0)
                {
                    selectData.Building_name = InputbuildName.Text;
                }

                SelectAddressData = selectData;
                SelectBuildingData = null;
                DialogResult = true;
                Close();
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            Map_Building_NameItem_Local selectData = (Map_Building_NameItem_Local)btn.DataContext;

            if (selectData != null)
            {
                SelectBuildingData = selectData;
                DialogResult = true;
                Close();
            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



            

        }
    }
}
