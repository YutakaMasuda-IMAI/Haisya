using HaisyaDesktop.Models;
using HaisyaDesktop.View.Common;
using HaisyaDesktop.ViewModel.Anken;
using Microsoft.Web.WebView2.Core;
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
    /// DriveRouteList.xaml の相互作用ロジック
    /// </summary>
    public partial class DriveRouteList : Window
    {

        public Dto.DriveRouteListDisplay_Local SelectedListItem { set; get; }

        /// <summary>JavaScriptで呼ぶ関数を保持するオブジェクト</summary>
        private JstoCs CsClass = new JstoCs();


        public DriveRouteList()
        {

            InitializeComponent();

            //WebView2のロード完了時のイベント
            webView.NavigationCompleted += WebView_NavigationCompleted;

            //this.Width = 700;

        }

        private DriveRouteListViewModel VModel => (DriveRouteListViewModel)DataContext;

        public bool IsCancel { get; internal set; }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!PublicObjects.GetMapsServerLocalFlg())
            {
                if (webView.CoreWebView2 != null)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("logoutForDesktop();");
                    await Task.Delay(2000);
                }
            }

            return;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 最大化
            WindowState = System.Windows.WindowState.Maximized;

            await Task.Delay(8000);

            /// 地図にポリシーラインを書く
            await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");
            for (int i = VModel.DriveRouteListData.Count -1; i >= 0; i--)
            {
                string html = VModel.GetDriveRoteLineForHtml(i);
                await webView.CoreWebView2.ExecuteScriptAsync(html);
                //await Task.Delay(500);
                await webView.CoreWebView2.ExecuteScriptAsync("setPolyline(" + i.ToString() + ",'solid');");
            }

            if (VModel.DriveRouteListData.Count > 0 && VModel.DriveRouteListData[0] != null)
            {
                Dto.DriveRouteListDisplay_Local data = VModel.DriveRouteListData[0];

                // 中心マーカーの算出
                int i = data.line.Count / 2;
                await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + data.line[i].lat + "),Number(" + data.line[i].lng + ")));");

            }


        }


        /// <summary>
        /// WebView2のロード完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                if (webView.CoreWebView2 != null)
                {
                    //JavaScriptからC#のメソッドが実行できる様に仕込む
                    webView.CoreWebView2.AddHostObjectToScript("class", CsClass);
                }
                else
                {
                    MessageBox.Show("CoreWebView2==null");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_ClickAsync(object sender, RoutedEventArgs e)
        {

            


            MessageBox.Show("END");


        }

        private async void TEST_Click(object sender, RoutedEventArgs e)
        {
            await webView.CoreWebView2.ExecuteScriptAsync("setPolyline(1);");
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RouteDetail_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;

            Dto.DriveRouteListDisplay_Local list = (Dto.DriveRouteListDisplay_Local)btn.DataContext;

            string routeId = list.routeID;
            string routeType = list.routeType;

            int companuID = PublicObjects.GetCompanyID();

            string SyasyuDisplay = VModel.Syasyu.Value + "-" + VModel.Kata.Value + "-" + VModel.SyasyuSize.Value;
            string TsumiTaskTime = VModel.TsumiTaskTime.Value;
            string OroshiTaskTime = VModel.OroshiTaskTime.Value;
            string Eria = VModel.Eria.Value;

            ViewModel.Common.CommonDriveRouteDetailViewModel vm = new();
            vm.RouteID = routeId;
            vm.DriveRouteListData = list;
            vm.CompanyID = companuID;
            vm.SyasyuDisplay = SyasyuDisplay;
            vm.TsumiTaskTime = TsumiTaskTime;
            vm.OroshiTaskTime = OroshiTaskTime;
            vm.Eria = Eria;

            vm.RouteType = routeType;

            App app = App.Current as App;
            app.ShowView(vm, this, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgDriveRouteList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedListItem = (Dto.DriveRouteListDisplay_Local)dgDriveRouteList.SelectedItem;
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dgDriveRouteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dto.DriveRouteListDisplay_Local list = (Dto.DriveRouteListDisplay_Local)dgDriveRouteList.SelectedItem;

            /// 地図にポリシーラインを書く
            await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");

            int routeType = int.Parse(list.routeType);

            string html = "";

            for (int i = VModel.DriveRouteListData.Count - 1; i >= 0; i--)
            {
                if (i != routeType)
                {
                    html = VModel.GetDriveRoteLineForHtml(i);
                    await webView.CoreWebView2.ExecuteScriptAsync(html);
                    await webView.CoreWebView2.ExecuteScriptAsync("setPolyline(" + i.ToString() + ",'dash');");
                }
            }

            html = VModel.GetDriveRoteLineForHtml(routeType);
            await webView.CoreWebView2.ExecuteScriptAsync(html);
            await webView.CoreWebView2.ExecuteScriptAsync("setPolyline(" + routeType.ToString() + ",'solid');");


        }
    }
}
