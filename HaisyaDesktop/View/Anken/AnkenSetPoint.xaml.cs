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
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// AnkenSetPoint.xaml の相互作用ロジック
    /// </summary>
    public partial class AnkenSetPoint : Window
    {

        private bool aaaflg { set; get; }

        public AnkenSetPoint()
        {
            InitializeComponent();

            //ViewModel.Anken.AnkenSetPointViewModel vm = new();
            //DataContext = vm;

            aaaflg = false;






        }



        private ViewModel.Anken.AnkenSetPointViewModel VModel => (ViewModel.Anken.AnkenSetPointViewModel)DataContext;


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //datagridPoint.Height = VModel.PointList.Count * 20;

            aaaflg = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!aaaflg) { return; }

            ComboBox aa = (ComboBox)sender;

            ComboBoxItem bb = (ComboBoxItem)aa.SelectedItem;

            //MessageBox.Show(bb.Content.ToString());

            PointDto_Local point = (PointDto_Local)aa.DataContext;

            point.TollDisplayHeight = 50;

            aaaflg = false;

            VModel.aaaa(point.PointId);

            aaaflg = true;

        }
    }
}
