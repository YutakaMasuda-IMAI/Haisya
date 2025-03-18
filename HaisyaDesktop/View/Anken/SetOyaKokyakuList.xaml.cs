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
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// SelectKokyakuList.xaml の相互作用ロジック
    /// </summary>
    public partial class SetOyaKokyakuList : Window
    {

        /// <summary>親顧客リスト</summary>
        public ObservableCollection<Dto.T_Anken_OyaKokyaku_Local> OyaKokyakuListData { get; set; }

        public SetOyaKokyakuList()
        {
            InitializeComponent();

        }

        private ViewModel.Anken.SetOyaKokyakuListViewModel VModel => (ViewModel.Anken.SetOyaKokyakuListViewModel)DataContext;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 最大化
            //WindowState = System.Windows.WindowState.Maximized;
            
            Height = 700;
            Width = 800;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnKokyaku_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("登録せずに終了しますか","確認") == MessageBoxResult.OK)
            {
                Close();
            }
        }

        private void Commit_Click(object sender, RoutedEventArgs e)
        {
            // エラーチェック
            foreach (var data in listOyaKokyakuList.Items)
            {
                Dto.T_Anken_OyaKokyaku_Local item = (Dto.T_Anken_OyaKokyaku_Local)data;
                if (item.KokyakuId == 0)
                {
                    MessageBox.Show("顧客情報を選択していません。", "確認");
                    return;
                }
            }

            OyaKokyakuListData = VModel.OyaKokyakuList;

            DialogResult = true;
            Close();
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {

            int i = VModel.OyaKokyakuList.Count + 1;
            string title = "親顧客" + Utils.StringsConvert.StrConvToWide(i.ToString());
            VModel.OyaKokyakuList.Add(new Dto.T_Anken_OyaKokyaku_Local { Kokyaku_Order = i, KokyakuId = 0, KokyakuName = "", KomokuTitle = title });

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (VModel.OyaKokyakuList.Count == 1) { return; }

            Button aa = (Button)sender;
            Dto.T_Anken_OyaKokyaku_Local point = (Dto.T_Anken_OyaKokyaku_Local)aa.DataContext;
            int id = point.Anken_Order;

            VModel.ResetOyaKokyakuListTitle(id);
        }
    }
}
