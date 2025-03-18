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
    /// MailSend.xaml の相互作用ロジック
    /// </summary>
    public partial class MailSend : Window
    {
        public MailSend()
        {
            InitializeComponent();

            this.Width = double.Parse("500");

            MailSendViewModel vm = new();
            DataContext = vm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private MailSendViewModel VModel => (MailSendViewModel)DataContext;





    }
}
