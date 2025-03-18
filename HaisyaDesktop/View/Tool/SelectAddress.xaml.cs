using HaisyaDesktop.ViewModel.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// SelectAddress.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectAddress : Window
    {

        public string AddressValArea { get; set; }
        public string AddressValKen { get; set; }
        public string AddressValShiku { get; set; }
        public string AddressValChyo { get; set; }
        public string AddressValFullAddress { get; set; }

        public bool ViewTopmost { get; set; }
        public double ViewLeft { get; set; }
        public double ViewTop { get; set; }

        public string lat { get; set; }
        public string lng { get; set; }


        public SelectAddress()
        {
            InitializeComponent();

            SelectAddressViewModel vm = new();
            DataContext = vm;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;


        }

        private SelectAddressViewModel VModel => (SelectAddressViewModel)DataContext;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            MessageBox.Show(button.Content.ToString());
        }

        private void listViewArea_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewArea.SelectedItem;
            if (selectAddressItem == null) { return; }

            SelectAddressViewModel vm = VModel;

            vm.SelectArea.Value = selectAddressItem.Name.ToString();

            vm.AfterForSelectArea(SelectAddressViewModelKubun.Address, vm.SelectArea.Value);

            listViewKen.Visibility = Visibility.Visible;
            listViewShiku.Visibility = Visibility.Hidden;
            listViewChyo.Visibility = Visibility.Hidden;

        }

        private async void listViewKen_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {

            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewKen.SelectedItem;
            if (selectAddressItem == null) { return; }

            VModel.SelectKen.Value = selectAddressItem.Name.ToString();

            SelectAddressViewModel vm = VModel;

            gridShiku.Background = Brushes.LightYellow;
            vm.IsBusy = true;

            await vm.GetShiKuChoDataList();

            vm.AfterForSelectKen();

            vm.IsBusy = false;
            gridShiku.Background = Brushes.Transparent;

            listViewShiku.Visibility = Visibility.Visible;
            listViewChyo.Visibility = Visibility.Hidden;

        }

        private void listViewShiku_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewShiku.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.SelectShiku.Value = selectAddressItem;

            SelectAddressViewModel vm = VModel;
            vm.AfterForSelectShiku();

            listViewChyo.Visibility = Visibility.Visible;

        }

         private void listViewChyo_PreviewMouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewChyo.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.SelectChyo.Value = selectAddressItem;

            SelectAddressViewModel vm = VModel;
            Task t = Task.Run(() => { vm.AfterForSelectChyo(); });
            Task.WaitAll(t);

            //MessageBox.Show("listViewChyo_PreviewMouseDoubleClickAsync:" + selectAddressItem.Name.ToString());


            AddressValArea = VModel.SelectArea.Value;
            AddressValKen = VModel.SelectKen.Value;
            AddressValShiku = VModel.SelectShiku.Value.Address;
            AddressValChyo = VModel.SelectChyo.Value.Address;

            DialogResult = true;

            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            // ポイントリストの取得
            await VModel.SetPointList();
        }

        private void listViewAddress_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItemForKey selectAddressItem = (SelectAddressItemForKey)listViewAddress.SelectedItem;
            if (selectAddressItem == null) { return; }
            string addressVal = selectAddressItem.Address;

            AddressValKen = selectAddressItem.Ken;
            AddressValShiku = selectAddressItem.Shiku;
            AddressValChyo = selectAddressItem.Cho;

            DialogResult = true;

            Close();
        }

        private void listViewTatemono_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            Map_Building_NameItem_Local selectData = (Map_Building_NameItem_Local)listViewTatemono.SelectedItem;

            if (selectData != null)
            {
                AddressValFullAddress = selectData.address;
                lat = selectData.position.lat;
                lng = selectData.position.lng;
                DialogResult = true;
                Close();
            }
        }

        private void listViewShiku_PreviewMouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewShiku.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.SelectShiku.Value = selectAddressItem;

            SelectAddressViewModel vm = VModel;
            Task t = Task.Run(() => { vm.AfterForSelectChyo(); });
            Task.WaitAll(t);


            AddressValArea = VModel.SelectArea.Value;
            AddressValKen = VModel.SelectKen.Value;
            AddressValShiku = VModel.SelectShiku.Value.Address;
            AddressValChyo = null;

            DialogResult = true;

            Close();
        }


        private void key_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (key.Text == null || key.Text.Length == 0) { return; }

                VModel.SetAddressListForKey(key.Text);
            }
            catch
            {

            }
        }

        private void listViewAreaForPoint_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewAreaForPoint.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.AfterForSelectArea(SelectAddressViewModelKubun.Point, selectAddressItem.Name.ToString());
            VModel.AreaForPoint(SelectAddressViewModelSelectKomoku.Area);
        }

        private void listViewAreaForSearchKey_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewAreaSearchKey.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.AfterForSelectArea(SelectAddressViewModelKubun.SearchKey, selectAddressItem.Name.ToString());
            VModel.AreaForSearchKey(SelectAddressViewModelSelectKomoku.Area);
        }

        private void listBoxPoint_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            Dto.T_Point_Local selectData = (Dto.T_Point_Local)listBoxPoint.SelectedItem;
            if (selectData != null)
            {
                AddressValFullAddress = selectData.Address;
                lat = selectData.Lat;
                lng = selectData.Lng;
                DialogResult = true;
                Close();
            }
        }

        private void listViewKenForSearchKey_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewKenSearchKey.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.SelectKenForSearchKey.Value = selectAddressItem.Name;
            VModel.AreaForSearchKey(SelectAddressViewModelSelectKomoku.ken);
        }

        private void listViewKenForPoint_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            SelectAddressItem selectAddressItem = (SelectAddressItem)listViewKenForPoint.SelectedItem;
            if (selectAddressItem == null) { return; }
            VModel.SelectKenForPoint.Value = selectAddressItem.Name;
            VModel.AreaForPoint(SelectAddressViewModelSelectKomoku.ken);
        }
    }
}
