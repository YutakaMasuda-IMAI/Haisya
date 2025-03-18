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
    /// HenkoRireki.xaml の相互作用ロジック
    /// </summary>
    public partial class HenkoRireki : Window
    {

        public HenkoRireki()
        {
            InitializeComponent();

            //AnkenRirekiViewModel vm = new();
            //DataContext = vm;

            Width = 1200;

        }




        private int _Mode;
        public int Mode
        {
            get => _Mode;
            set
            {
                _Mode = value;
                switch (_Mode)
                {
                    case 1:  //変更確認
                        btnUnder0.Content = "変更確認";
                        btnUnder1.Content = "修正";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        btnTopMenu.Content = "案件一覧";
                        btnTopMenu1.Content = "";
                        btnTopMenu2.Content = "";
                        btnTopMenu3.Content = "";
                        btnTopMenu4.Content = "";
                        btnTopMenu5.Content = "";
                        break;
                    case 2: //まだ
                        btnUnder0.Content = "";
                        btnUnder1.Content = "";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        btnTopMenu.Content = "案件一覧";
                        btnTopMenu1.Content = "";
                        btnTopMenu2.Content = "";
                        btnTopMenu3.Content = "";
                        btnTopMenu4.Content = "";
                        btnTopMenu5.Content = "";
                        break;
                    default:　//参照
                        btnUnder0.Content = "";
                        btnUnder1.Content = "";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        btnTopMenu.Content = "案件一覧";
                        btnTopMenu1.Content = "";
                        btnTopMenu2.Content = "";
                        btnTopMenu3.Content = "";
                        btnTopMenu4.Content = "";
                        btnTopMenu5.Content = "";
                        break;
                }
            }
        }

        


        private void btnTopMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUnder0_Click(object sender, RoutedEventArgs e)
        {
            if ("変更確認".Equals(btnUnder0.Content))
            {
                // メッセージボックスを表示
                MessageBoxResult result = MessageBox.Show("対象案件の変更確認通知をします", "タイトル", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show("はい");
                }
                else
                {
                    MessageBox.Show("いいえ");
                }

            }
        }

        private void btnUnder1_Click(object sender, RoutedEventArgs e)
        {
            if ("変更確認".Equals(btnUnder0.Content))
            {
                ViewModel.Anken.AnkenViewModel vm = new();
                App app = App.Current as App;
                app.ShowView(vm, this, true);

                this.Close();
            }
        }

        private void btnUnder2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnder3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnder4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cboAnkenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //MessageBox.Show("aaa");
        }
    }
}
