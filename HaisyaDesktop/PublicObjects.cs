using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop
{
    static class PublicObjects
    {

        public static string GetWebAPIHosts()
        {
            //return Properties.Settings.Default.WebAPIHosts;
            return Properties.Settings.Default.WebAPIHostsLocal;
        }

        public static string GetMapsAPIHosts()
        {
            return Properties.Settings.Default.MapsAPIHosts;
            //return Properties.Settings.Default.MapsAPIHostsLocal;
        }

        /// <summary>
        /// MapApiサーバーがローカルかサーバーかを判断して返却
        /// True:サーバー；False：ローカル
        /// </summary>
        /// <returns></returns>
        public static bool GetMapsServerLocalFlg()
        {
            if (GetMapsAPIHosts().Contains("localhost"))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public static string GetWebViewFlgForString()
        {
            return GetMapsServerLocalFlg() ? "webViewFlg=1" : "webViewFlg=0";
        }


        /// <summary>
        /// 暫定
        /// </summary>
        /// <returns></returns>
        public static int GetCompanyID()
        {
            return 1;
        }



        public static ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsTimeKubun()
        {

            return new ObservableCollection<Models.ComboBoxItem>
                {
                    new Models.ComboBoxItem( 1, "頃"),
                    new Models.ComboBoxItem( 2, "まで"),
                    new Models.ComboBoxItem( 3, "厳守"),
                };
        }

        public static ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsStatusKubun()
        {
            return new ObservableCollection<Models.ComboBoxItem>
                {
                    new Models.ComboBoxItem( 1, "確定"),
                    new Models.ComboBoxItem( 2, "暫定"),
                };
        }

        public static ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsRoadType()
        {
            return new ObservableCollection<Models.ComboBoxItem>
                {
                    new Models.ComboBoxItem( 1, "一般道優先"),
                    new Models.ComboBoxItem( 2, "高速道路優先"),
                    new Models.ComboBoxItem( 3, "全ての道路"),
                };
        }

        public static ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsPointKubun()
        {
            return new ObservableCollection<Models.ComboBoxItem>
                {
                    new Models.ComboBoxItem( 1, "積み"),
                    new Models.ComboBoxItem( 2, "卸し"),
                    new Models.ComboBoxItem( 3, "経由"),
                    new Models.ComboBoxItem( 4, "開始"),
                    new Models.ComboBoxItem( 5, "終了"),
                };
        }
    }
}
