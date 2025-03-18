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
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Windows.Threading;
using HaisyaDesktop.Context;

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// SetDriveRouteDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class SetDriveRouteDetail : Window
    {

        /// <summary>JavaScriptで呼ぶ関数を保持するオブジェクト</summary>
        private JstoCs CsClass = new JstoCs();

        /// <summary>タイマーイベント</summary>
        private DispatcherTimer _timer;


        public SetDriveRouteDetail()
        {
            InitializeComponent();

            //WebView2のロード完了時のイベント
            webView.NavigationCompleted += WebView_NavigationCompleted;

            // 住所検索ボタン非活性
            SeachAddress.IsEnabled = false;

            // 優先順位を指定してタイマのインスタンスを生成
            _timer = new DispatcherTimer(DispatcherPriority.Background);

            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 2);

            // タイマメソッドを設定
            _timer.Tick += (e, s) => { TimerMethod(); };

            // 画面が閉じられるときに、タイマを停止
            this.Closing += (e, s) => { _timer.Stop(); };

            _timer.Start();
        }

        /// <summary>
        /// 案件登録ビューモデル
        /// </summary>
        private SetDriveRouteDetailViewModel VModel => (SetDriveRouteDetailViewModel)DataContext;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// タイマーイベント
        /// </summary>
        private void TimerMethod()
        {
            if (ContextManager.Instance.AddressList != null)
            {
                _timer.Stop();
                SeachAddress.IsEnabled = true;
            }
        }


        /// <summary>
        /// WebView2のロード完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                //if (webView.CoreWebView2 != null)
                //{
                //    //JavaScriptからC#のメソッドが実行できる様に仕込む
                //    webView.CoreWebView2.AddHostObjectToScript("class", CsClass);

                //    if (VModel.AnkenId.Value > 0)
                //    {
                //        int i = 0;
                //        System.Threading.Thread.Sleep(8000);
                //        PointDto_Local dtoS = VModel.TsumiPointList[0];
                //        if (dtoS.Lat != null && dtoS.Lat.Length > 0)
                //        {
                //            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + dtoS.Lat + "\",\"" + dtoS.Lng + "\",\"" + "start" + "\");");
                //            i++;
                //        }
                //        PointDto_Local dtoE = VModel.OroshiPointList[VModel.OroshiPointList.Count - 1];
                //        if (dtoE.Lat != null && dtoE.Lat.Length > 0)
                //        {
                //            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + dtoE.Lat + "\",\"" + dtoE.Lng + "\",\"" + "end" + "\");");
                //            i++;
                //        }
                //        if (i == 2) { SetPolyline(); }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("CoreWebView2==null");
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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

        private void TimePiker_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPointReg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PointListAdd_Click(object sender, RoutedEventArgs e)
        {

            VModel.AddPointList();



        }

        private void ListBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void btnPointDel_Click(object sender, RoutedEventArgs e)
        {

            Button target = (Button)sender;
            PointDto_Local point = (PointDto_Local)target.DataContext;
            int id = point.PointId;

            if (point.PointId == 0) { return; }

            VModel.DelPointList(point);
        }

        /// <summary>
        /// 住所検索画面ボタンのクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SeachAddress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VModel.ThisView = this;
                VModel.OpenSubWindowCommandSelectAddress.Execute(null);

                Latlon latlon = new();

                if (VModel.Lat.Value != null && VModel.Lng.Value != null)
                {
                    latlon.lat = VModel.Lat.Value;
                    latlon.lng = VModel.Lng.Value;
                }
                else
                {
                    latlon = await VModel.GetlatlonFromAddress();

                }

                if (latlon != null)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + latlon.lat + "),Number(" + latlon.lng + ")));");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 住所項目入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void InputAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Latlon latlon = await VModel.GetlatlonFromAddress();
                if (latlon != null)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + latlon.lat + "),Number(" + latlon.lng + ")));");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void SetPoint_ClickAsync(object sender, RoutedEventArgs e)
        {

            VModel.ThisView = this;
            Button target = (Button)sender;

            try
            {
                PointDto_Local point = (PointDto_Local)target.DataContext;
                int id = point.PointId;

                if (point.PointId == 0) { return; }
                target.IsEnabled = false;
                string latlon = await webView.CoreWebView2.ExecuteScriptAsync("MapGetCenterPoint();");
                if ("ul".Equals(latlon)) { return; }
                latlon = latlon.Substring(1, latlon.Length - 2);

                bool resultflg = await VModel.SetPointForPointListAsync(sender, e, latlon, id);
                if (resultflg)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + latlon.Split(",")[1] + "\",\"" + latlon.Split(",")[0] + "\",\"" + "start" + "\");");
                    //SetPolyline();
                }
                else
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("ClearMarkerToPoint(\"start" + "\");");
                    await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                target.IsEnabled = true;
            }


        }
    }
}
