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
    /// SyabanPreview.xaml の相互作用ロジック
    /// </summary>
    public partial class SyabanPreview : Window
    {
        public SyabanPreview()
        {
            InitializeComponent();

            SyabanPreviewViewModel vm = new();
            DataContext = vm;

        }

        private SyabanPreviewViewModel VModel => (SyabanPreviewViewModel)DataContext;

        private void btnTopMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTopMenu2_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSendMail.Execute(null);
        }

        private void btnTopMenu3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTopMenu1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
