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
    /// AnkenInfoDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class AnkenInfoDialog : Window
    {

        public string AnkenId { get; set; }

        private AnkenInfoDialogViewmodel VModel => (AnkenInfoDialogViewmodel)DataContext;

        public AnkenInfoDialog()
        {
            InitializeComponent();



        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            this.Close();
        }
    }
}
