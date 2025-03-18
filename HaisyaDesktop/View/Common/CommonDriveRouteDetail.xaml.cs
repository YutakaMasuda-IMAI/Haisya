using HaisyaDesktop.Models;
using HaisyaDesktop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaisyaDesktop.View.Common
{
    /// <summary>
    /// CommonSearchPastMatter.xaml の相互作用ロジック
    /// </summary>
    public partial class CommonDriveRouteDetail : Window
    {

        public CommonDriveRouteDetail()
        {
            InitializeComponent();
        }

        private CommonDriveRouteDetailViewModel VModel => (CommonDriveRouteDetailViewModel)DataContext;

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            webView.Source = new Uri(VModel.Url);
            await Task.Delay(6000);
            await webView.CoreWebView2.ExecuteScriptAsync("setCenter();");

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
    }
}

namespace HaisyaDesktop.ViewModel.Common
{

    public class CommonDriveRouteDetailViewModel :ViewModel.BaseViewModel
    {

        public Dto.DriveRouteListDisplay_Local DriveRouteListData { get; set; }

        public string RouteID { get; set; }

        public string Url { get; set; }

        public int CompanyID { get; set; }

        public string SyasyuDisplay { get; set; }

        public string TsumiTaskTime { get; set; }

        public string OroshiTaskTime { get; set; }

        public string Eria { get; set; }

        private string routeType;
        public string RouteType
        {
            get => routeType;
            set
            {
                this.routeType = value;
                Url = PublicObjects.GetMapsAPIHosts() + "DesktopApp/DriveRouteDetail?companyID=" + CompanyID + "&routeID=" + RouteID + 
                                                        "&routeType=" + routeType + "&sasyuDisp=" + SyasyuDisplay + "&eria=" + Eria +
                                                        "&tsumiTaskTime=" + TsumiTaskTime + "&oroshiTaskTime=" + OroshiTaskTime + "";
                Url += string.Format("&" + PublicObjects.GetWebViewFlgForString());
            }
        }


        public CommonDriveRouteDetailViewModel()
        {

        }

    }
}
