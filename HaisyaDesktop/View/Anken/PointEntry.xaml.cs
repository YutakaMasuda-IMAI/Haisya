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
    /// PointEntry.xaml の相互作用ロジック
    /// </summary>
    public partial class PointEntry : Window
    {
        public PointEntry()
        {
            InitializeComponent();
        }

        private PointEntryViewModel VModel => (PointEntryViewModel)DataContext;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await VModel.SetEntryKubunItems();
        }

        private void GroupKubun_Checked(object sender, RoutedEventArgs e)
        {
            if (VModel == null) { return; }
            RadioButton radio = (RadioButton)sender;
            int i = int.Parse(radio.Tag.ToString());
            if (i == 1)
            {
                if (VModel.PointComboBoxItemsEntrykubun == null || VModel.PointComboBoxItemsEntrykubun.Count == 0)
                {
                    MessageBox.Show("グループ設定のデータが無いので選択出来ません。");
                    GroupKubun_0.IsChecked = true;
                    return;
                }
            }

            VModel.EntryKubun.Value = i;
        }

        private async void btnReg_Click(object sender, RoutedEventArgs e)
        {

            if (VModel.EntryKubun.Value == 1)
            {
                if (VModel.SelectGroupId.Value == 0)
                {
                    MessageBox.Show("グループ設定が選択されていません。");
                    return;
                }
            }

            if (VModel.TPoint.Value.BuildingName == null || VModel.TPoint.Value.BuildingName.Length == 0)
            {
                MessageBox.Show("ポイント名は必須です。");
                return;
            }

            bool result = await VModel.RegExec();

            if (result)
            {
                MessageBox.Show("登録処理が完了しました。");
                DialogResult = true;
                Close();
            } else
            {
                MessageBox.Show("登録処理が失敗しました。");
                return;
            }

        }


    }
}
