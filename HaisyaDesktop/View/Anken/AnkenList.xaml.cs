using HaisyaDesktop.Context;
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
using System.Windows.Threading;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.View.Anken
{

    public partial class AnkenList : Window
    {

        public AnkenList()
        {
            InitializeComponent();

        }

        private AnkenListViewModel VModel => (AnkenListViewModel)DataContext;


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 案件画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dto.V_AnkenDataList_Local data = (Dto.V_AnkenDataList_Local)dgAnkenList.SelectedItem;
                ViewModel.Anken.AnkenViewModel vm = new(data.Anken_ID, AnkenOpenModeEnum.Edit);
                App app = App.Current as App;
                Window win = app.ShowModalView(vm, this, true);
                await VModel.GetAnkenData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


            //if (_Mode == 2)
            //{
            //    View.Anken.Register view = new();
            //    // プライマリ画面かどうかチェック
            //    var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            //    if (!screen.Primary)
            //    {
            //        //view.Topmost = true;
            //        view.Left = screen.Bounds.Left;
            //        view.Top = screen.Bounds.Top;
            //    }
            //    view.Show();
            //    // 全画面表示
            //    view.WindowState = System.Windows.WindowState.Maximized;
            //} else
            //{
            //    View.Anken.HenkoRireki view = new();
            //    view.Mode = 1;
            //    // プライマリ画面かどうかチェック
            //    var screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            //    if (!screen.Primary)
            //    {
            //        //view.Topmost = true;
            //        view.Left = screen.Bounds.Left;
            //        view.Top = screen.Bounds.Top;
            //    }
            //    view.Show();
            //    // 全画面表示
            //    view.WindowState = System.Windows.WindowState.Maximized;
            //}

        }

        /// <summary>
        /// グループ化を解除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupingNone(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radio = (RadioButton)sender;
                string s = radio.Tag.ToString();
                VModel.SelectOnGrouping.Value = s;

                if (dgAnkenList != null)
                {
                    var cv = CollectionViewSource.GetDefaultView(dgAnkenList.ItemsSource);

                    if (cv != null && cv.CanGroup)
                    {
                        cv.GroupDescriptions.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 一覧のグループ化を実行する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGrouping(object sender, RoutedEventArgs e)
        {
            try
            {
                if (VModel != null)
                {
                    RadioButton radio = (RadioButton)sender;
                    string s = radio.Tag.ToString();
                    VModel.SelectOnGrouping.Value = s;
                    GroupingByPropertyName(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 指定したグループ名でグループ化をする実行する
        /// </summary>
        /// <param name="propertyName"></param>
        public void GroupingByPropertyName(string propertyName)
        {
            try
            {
                if (dgAnkenList != null)
                {
                    var cv = CollectionViewSource.GetDefaultView(dgAnkenList.ItemsSource);
                    if (cv != null && cv.CanGroup)
                    {
                        cv.GroupDescriptions.Clear();
                        cv.GroupDescriptions.Add(new PropertyGroupDescription(propertyName));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 指定日付を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                await VModel.GetAnkenData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (VModel.Mode)
                {
                    case 1:  //変更確認
                        btnUnder0.Content = "案件新規登録";
                        btnUnder1.Content = "";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        break;
                    case 2: //変更一覧
                        btnUnder0.Content = "";
                        btnUnder1.Content = "";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        break;
                    default: //参照
                        btnUnder0.Content = "案件新規登録";
                        btnUnder1.Content = "";
                        btnUnder2.Content = "";
                        btnUnder3.Content = "";
                        btnUnder4.Content = "";
                        break;
                }


                if (this.RegulationSelected.Text != null && this.RegulationSelected.Text != "")
                {
                    RadioButton rb = FindName("Group_" + this.RegulationSelected.Text) as RadioButton;
                    rb.IsChecked = true;
                }

                await VModel.FormLoad();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"エラー");
            }

        }

        /// <summary>
        /// 案件新規登録を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AnkenAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Anken.AnkenViewModel vm = new(0, AnkenOpenModeEnum.AddNew);
                App app = App.Current as App;
                Window win = app.ShowModalView(vm, this, false);
                await VModel.GetAnkenData();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 発注書印刷画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPrintOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dto.V_AnkenDataList_Local data = (Dto.V_AnkenDataList_Local)dgAnkenList.SelectedItem;

                ViewModel.Print.AnkenOrderViewModel vm = new(data.Anken_ID);
                App app = App.Current as App;
                Window win = app.ShowModalView(vm, this, true);
                await VModel.GetAnkenData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 複製処理を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dto.V_AnkenDataList_Local data = (Dto.V_AnkenDataList_Local)dgAnkenList.SelectedItem;
                ViewModel.Anken.AnkenViewModel vm = new(data.Anken_ID, AnkenOpenModeEnum.Copy);
                App app = App.Current as App;
                Window win = app.ShowModalView(vm, this, true);
                await VModel.GetAnkenData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
