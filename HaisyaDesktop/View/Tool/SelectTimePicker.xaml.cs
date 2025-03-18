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
    /// SelectTimePicker.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectTimePicker : Window
    {

        public string Houer { set; get; }
        public string Munute { set; get; }

        public SelectTimePicker()
        {
            InitializeComponent();

            SelectTimePickerViewModel vm = new();
            DataContext = vm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

   

        private void HourClick(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            Houer = (string)btn.Tag;

            if (Houer != null && Munute != null)
            {
                DialogResult = true;
                Close();
            }

            //MessageBox.Show(Houer);
        }

        private void MinuteClick(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            Munute = (string)btn.Tag;

            if (Houer != null && Munute != null)
            {
                DialogResult = true;
                Close();
            }

            //MessageBox.Show(Munute);

        }


    }
}
