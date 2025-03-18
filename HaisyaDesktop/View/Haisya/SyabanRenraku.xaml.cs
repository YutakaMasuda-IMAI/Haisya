using HaisyaDesktop.ViewModel.Haisya;
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

namespace HaisyaDesktop.View.Haisya
{
    /// <summary>
    /// SyabanRenraku.xaml の相互作用ロジック
    /// </summary>
    public partial class SyabanRenraku : Window
    {
        public SyabanRenraku()
        {
            InitializeComponent();

            SyabanRenrakuViewModel vm = new();
            DataContext = vm;


        }

        private SyabanRenrakuViewModel VModel => (SyabanRenrakuViewModel)DataContext;

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandPreview.Execute(null);

        }

        /// <summary>
        /// グループ化を解除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupingNone(object sender, RoutedEventArgs e)
        {
            if (dgKokyakuList != null)
            {
                var cv = CollectionViewSource.GetDefaultView(dgKokyakuList.ItemsSource);

                if (cv != null && cv.CanGroup)
                {
                    cv.GroupDescriptions.Clear();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupingByAnkenStatus(object sender, RoutedEventArgs e)
        {
            GroupingByPropertyName("Status");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void GroupingByPropertyName(string propertyName)
        {
            var cv = CollectionViewSource.GetDefaultView(dgKokyakuList.ItemsSource);

            if (cv != null && cv.CanGroup)
            {
                cv.GroupDescriptions.Clear();

                cv.GroupDescriptions.Add(new PropertyGroupDescription(propertyName));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GroupingByPropertyName("Status");
        }
    }
}
