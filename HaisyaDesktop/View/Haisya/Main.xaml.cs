using HaisyaDesktop.ViewModel.Haisya;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace HaisyaDesktop.View.Haisya
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class Main : Window
    {

        private readonly HaisyaMainForJsToCs CsClass = new();

        public Main()
        {
            InitializeComponent();

            HaisyaViewModel vm = new();
            DataContext = vm;

            //WebView2のロード完了時のイベント
            webView.NavigationCompleted += WebView_NavigationCompleted;
        }

        private void MapOnPoint_Click(object sender, RoutedEventArgs e)
        {

            MapOnPosition mapOnPosition = new();
            
            if (mapOnPosition.ShowDialog() == true)
            {

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
                if (webView.CoreWebView2 != null)
                {
                    // 地図を読み込む時間を待機
                    await Task.Delay(10000);

                    //JavaScriptからC#のメソッドが実行できる様に仕込む
                    webView.CoreWebView2.AddHostObjectToScript("class", CsClass);

                    await SetPoint();
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


        private async void AnkenListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            AnkenInfo ankenInfo = (AnkenInfo)AnkenListBox.SelectedItem;

            string lat = ankenInfo.TsumiPoint.lat;
            string lng = ankenInfo.TsumiPoint.lng;

            await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + lat + "),Number(" + lng + ")));");
        }



        private void gdSplitter_Click(object sender, RoutedEventArgs e)
        {
            if (GridSchedule.Width == new GridLength(10.0, GridUnitType.Star))
            {
                double width = dgScheduleList.Columns[0].Width.DisplayValue + dgScheduleList.Columns[1].Width.DisplayValue + dgScheduleList.Columns[2].Width.DisplayValue;
                width += double.Parse("40");

                GridSchedule.Width = new GridLength(width, GridUnitType.Pixel);
                GridMap.Width = new GridLength(10.0, GridUnitType.Star);
            }
            else
            {
                GridSchedule.Width = new GridLength(10.0, GridUnitType.Star);
                GridMap.Width = new GridLength(20.0, GridUnitType.Pixel);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async Task SetPoint()
        {

            string lat = "35.5";
            string lng = "139.5";
            string ankenID = "2";
            string kubun = "TruckB";
            string popupVal = "3633<br/>益田";
            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToTruck(\"" + lat + "\",\"" + lng + "\",\"" + ankenID + "\",\"" + kubun + "\",\"" + popupVal + "\");");

            lat = "35.5";
            lng = "139.0";
            ankenID = "3";
            kubun = "TruckR";
            popupVal = "9999<br/>西本";
            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToTruck(\"" + lat + "\",\"" + lng + "\",\"" + ankenID + "\",\"" + kubun + "\",\"" + popupVal + "\");");

            lat = "35.2";
            lng = "138.5";
            ankenID = "4";
            kubun = "TruckR";
            popupVal = "5555<br/>原田";
            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToTruck(\"" + lat + "\",\"" + lng + "\",\"" + ankenID + "\",\"" + kubun + "\",\"" + popupVal + "\");");

            lat = "35.3";
            lng = "139.7";
            ankenID = "5";
            kubun = "CargoB";
            popupVal = "〇〇運輸<br/>コイル<br/>倉敷市";
            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToCargo(\"" + lat + "\",\"" + lng + "\",\"" + ankenID + "\",\"" + kubun + "\",\"" + popupVal + "\");");

            lat = "35.5";
            lng = "140.0";
            ankenID = "6";
            kubun = "CargoR";
            popupVal = "〇〇工業<br/>鋼材<br/>呉市";
            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToCargo(\"" + lat + "\",\"" + lng + "\",\"" + ankenID + "\",\"" + kubun + "\",\"" + popupVal + "\");");

        }

        private async void NowPointInfo_Click(object sender, RoutedEventArgs e)
        {
            double width = dgScheduleList.Columns[0].Width.DisplayValue + dgScheduleList.Columns[1].Width.DisplayValue + dgScheduleList.Columns[2].Width.DisplayValue;
            width += double.Parse("40");

            GridSchedule.Width = new GridLength(width, GridUnitType.Pixel);
            GridMap.Width = new GridLength(10.0, GridUnitType.Star);


            string lat = "35.5";
            string lng = "139.5";

            await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + lat + "),Number(" + lng + ")));");

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    /// <summary>
    /// WebView2に読み込ませるためのJsで実行する関数を保持させたクラス
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class HaisyaMainForJsToCs
    {

        public void WebViewReady(string ankenId = "")
        {
            //string ankenId = "";

            AnkenInfoDialog win = new();
            win.AnkenId = ankenId;
            win.ShowDialog();


        }


    }
}
