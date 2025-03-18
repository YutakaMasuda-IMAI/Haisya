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
    /// HaisyaTable.xaml の相互作用ロジック
    /// </summary>
    public partial class HaisyaBook : Window
    {
        public HaisyaBook()
        {
            InitializeComponent();
        }

        private HaisyaBookViewModel VModel => (HaisyaBookViewModel)DataContext;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await VModel.DataLoadToSarch();
                await VModel.DataLoadForAnkenData();
                await VModel.DataLoadForDriverLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 画面サイズ変更処理
        /// 画面高さが低い時に、メニュー高さ変更、メニューボタンのフォントサイズを小さくする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            try
            {
                Window c = this;
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);

                double dHeight = c.Height;

                //int iAnkenStatus = 18;
                //int iOther = 14;
                int iMenuBtn = 26;

                if (this.WindowState == WindowState.Maximized)
                {
                    //MessageBox.Show(screen.Bounds.Height.ToString());
                    dHeight = screen.Bounds.Height;
                }

                if (dHeight < 800)
                {
                    //iAnkenStatus = 12;
                    //iOther = 10;
                    iMenuBtn = 12;
                    this.Head.Height = new GridLength(double.Parse("25"));
                    this.Tail.Height = new GridLength(double.Parse("25"));
                }
                else
                {
                    this.Head.Height = new GridLength(double.Parse("50"));
                    this.Tail.Height = new GridLength(double.Parse("50"));
                }

                foreach (Button tb in FindVisualChildren<Button>(c))
                {
                    if (tb.Name.Contains("btnTopMenu") || tb.Name.Contains("OptionMenu") || tb.Name.Contains("btnUnder"))
                    {
                        tb.FontSize = iMenuBtn;
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
            }

        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child == null || child is not T)
                    {
                    }
                    else
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void TargetDateChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if ("After".Equals(btn.Tag))
                {
                    VModel.TartgetDate.Value = VModel.TartgetDate.Value.AddDays(1);
                }
                else
                {
                    VModel.TartgetDate.Value = VModel.TartgetDate.Value.AddDays(-1);
                }

                await VModel.DataLoadForAnkenData();
                await VModel.DataLoadForDriverLists();
                
                VModel.SelectAnkenLists();
                VModel.SelectDriverLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                VModel.SelectAnkenLists();
                VModel.SelectDriverLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void gridAnkenList_PreviewDragOver(object sender, DragEventArgs e)
        {

            //==== データの使用可否判定 ====//
            // 非対応データは受け入れ拒否する。
            //
            var DragData = e.Data;

            //var aa = DragData;

    //        if (isDragData & amp; &amp; (sender == e.Source))
    //{
    //            //==== 操作設定：移動可 ====//
    //            e.Effects = DragDropEffects.Move;
    //        }
    //else
    //        {
    //            //==== 受け入れ拒否 ====//
    //            e.Effects = DragDropEffects.None;
    //        }

            //e.Effects = DragDropEffects.Move;
            ////e.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
           
            //e.Handled = true;


        }

        private void Border_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
