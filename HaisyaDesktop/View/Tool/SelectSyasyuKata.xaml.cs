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
    /// SelectSyasyuKata.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectSyasyuKata : Window
    {

        public string SelectSyasyuDisplay { set; get; }

        public string SelectSyasyu { set; get; }

        public string SelectKata { set; get; }

        public string SelectSize { set; get; }


        public SelectSyasyuKata()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private SelectSyasyuKataViewModel VModel => (SelectSyasyuKataViewModel)DataContext;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            string val = (string)btn.Tag;
            int i = val.IndexOf("-");
            int m = val.IndexOf(":");
            string val2 = val.Substring(0, m);
            SelectSyasyu = val2.Substring(0, i);
            SelectKata = val2.Substring(i + 1);
            SelectSyasyuDisplay = val.Substring(m + 1);

            SetSize();

            DialogResult = true;

            Close();
        }

        private void SetSize()
        {

            if ("トレーラ".Equals(SelectKata, StringComparison.Ordinal))
            {
                SelectSize = "トレーラ";
                return;
            }

            if ("トレーラ".Equals(SelectSyasyu, StringComparison.Ordinal))
            {
                SelectSize = "トレーラ";
                return;
            }

            int i;

            if (SelectSyasyu.IndexOf("t") > 0) {
                string aa = SelectSyasyu.Substring(0, SelectSyasyu.IndexOf("t"));
                if (!int.TryParse(aa, out i)) { return; }
            } else
            {
                if (!int.TryParse(SelectSyasyu, out i)) { return; }
            }
            


            if (i is >= 2 and < 4)
            {
                SelectSize = "小型車";
                return;
            }
            if (i is >= 4 and < 10)
            {
                SelectSize = "中型車";
                return;
            }
            if (i is >= 10)
            {
                SelectSize = "大型車";
                return;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await VModel.DataLoad();


            List<Dto.M_SyaryoSize_Local> size = VModel.SyaryoSizeList;

            for (int i = 0; i < size.Count; i++)
            {
                ColumnDefinition column = this.FindName("cd" + i.ToString()) as ColumnDefinition;
                column.Width = new GridLength(1.0, GridUnitType.Star);

                Grid grid = this.FindName("grid" + i.ToString()) as Grid;

                var targetSize = VModel.SyaryoList.Where(m => m.SIZE == size[i].SIZE).OrderBy(m => m.SortOrder).ToList();

                for (int m = 0; m < targetSize.Count; m++)
                {
                    Dto.M_Syaryo_Local syaryo = targetSize[m];

                    RowDefinition rowDef1 = null;
                    rowDef1 = this.FindName("grid" + i.ToString() + "_rd" + m.ToString()) as RowDefinition;
                    if (rowDef1 == null) { rowDef1 = new RowDefinition(); grid.RowDefinitions.Add(rowDef1); }

                    Style newGridStyle = new Style(typeof(Grid));
                    newGridStyle.Setters.Add(new Setter(Grid.RowProperty, m));
                    //newGridStyle.Setters.Add(new Setter(Grid.BackgroundProperty, new SolidColorBrush(Colors.Red)));

                    Grid newGrid = new Grid();
                    newGrid.Style = newGridStyle;

                    //Style btnStyle = this.FindName("SelectButton") as Style;

                    //TextBlock textBlock = new TextBlock();
                    //textBlock.Text = syaryo.SyasyuDisplay;
                    //textBlock.TextWrapping = TextWrapping.Wrap;

                    Button btn = new Button();
                    btn.Content = syaryo.SyasyuDisplay;
                    btn.Tag = syaryo.SYASYU + "-" + syaryo.KATA + ":" + syaryo.SyasyuDisplay;
                    btn.Style = FindResource("SelectButton") as Style; ;
                    btn.Click += new RoutedEventHandler(Button_Click);
                    

                    newGrid.Children.Add(btn);
                    grid.Children.Add(newGrid);


                }


                    



            }




        }
    }
}
