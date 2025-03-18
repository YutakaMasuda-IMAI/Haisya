using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using HaisyaDesktop.API.Map;
using HaisyaDesktop.Context;
using HaisyaDesktop.Models;
using HaisyaDesktop.ViewModel.Anken;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using static HaisyaDesktop.Models.AnkenModel;
using static HaisyaDesktop.Models.MapApiModel;

namespace HaisyaDesktop.View.Anken
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class Register : Window
    {

        /// <summary>JavaScriptで呼ぶ関数を保持するオブジェクト</summary>
        private JstoCs CsClass = new JstoCs();

        /// <summary>タイマーイベント</summary>
        private DispatcherTimer _timer;

        public Register()
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
        private AnkenViewModel VModel => (AnkenViewModel)DataContext;

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
                if (webView.CoreWebView2 != null)
                {
                    //JavaScriptからC#のメソッドが実行できる様に仕込む
                    webView.CoreWebView2.AddHostObjectToScript("class", CsClass);

                    if (VModel.AnkenId.Value > 0)
                    {
                        int i = 0;
                        System.Threading.Thread.Sleep(8000);
                        PointDto_Local dtoS = VModel.TsumiPointList[0];
                        if (dtoS.Lat != null && dtoS.Lat.Length > 0)
                        {
                            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + dtoS.Lat + "\",\"" + dtoS.Lng + "\",\"" + "start" + "\");");
                            i++;
                        }
                        PointDto_Local dtoE = VModel.OroshiPointList[VModel.OroshiPointList.Count - 1];
                        if (dtoE.Lat != null && dtoE.Lat.Length > 0)
                        {
                            await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + dtoE.Lat + "\",\"" + dtoE.Lng + "\",\"" + "end" + "\");");
                            i++;
                        }
                        if (i == 2) { SetPolyline(); }
                    }
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

        private void TsumiPlus_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            
            try
            {
                VModel.TsumiPointListPlus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        private void TsumiDel_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            
            try
            {
                VModel.TsumiPointListDel(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        private void OroshiPlus_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            
            try
            {
                VModel.OroshiPointListPlus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        private void OroshiDel_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            
            try
            {
                VModel.OroshiPointListDel(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        /// <summary>
        /// 【簡易設定】積地ポイント設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TsumiSetPoint_ClickAsync(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            Button me = (Button)sender;

            try
            {
                int id;
                switch (me.Tag)
                {
                    case "Complex":
                        PointDto_Local point = (PointDto_Local)me.DataContext;
                        id = point.PointId;
                        break;
                    default:
                        id = 1;
                        break;
                }
                me.IsEnabled = false;
                string latlon = await webView.CoreWebView2.ExecuteScriptAsync("MapGetCenterPoint();");
                if ("ul".Equals(latlon)) { return; }
                latlon = latlon.Substring(1, latlon.Length - 2);
                bool resultflg = await VModel.SetPointForPointListAsync(sender, e, latlon, VModel.TsumiPointList, id);
                if (resultflg) {
                    await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + latlon.Split(",")[1] + "\",\"" + latlon.Split(",")[0] + "\",\"" + "start" + "\");");
                    SetPolyline();
                }
                else
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("ClearMarkerToPoint(\"start" + "\");");
                    await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");
                }

                PointDto_Local data = VModel.TsumiPointList.FirstOrDefault(m => m.PointId == id);
                btnTsumiPointReg.IsEnabled = data.BuildingZid != null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        /// <summary>
        /// 地図ポイントの登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPointReg_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            Button me = (Button)sender;
            string btnName = me.Name;

            try
            {
                int id;
                switch (me.Tag)
                {
                    case "Complex":
                        PointDto_Local point = (PointDto_Local)me.DataContext;
                        id = point.PointId;
                        break;
                    default:
                        id = 1;
                        break;
                }
                me.IsEnabled = false;

                PointDto_Local data = null; ;
                switch (me.Name)
                {
                    case "btnTsumiPointReg":
                        data = VModel.TsumiPointList.FirstOrDefault(m => m.PointId == id);
                        break;
                    case "btnOroshiPointReg":
                        data = VModel.OroshiPointList.FirstOrDefault(m => m.PointId == id);
                        break;
                    default:
                        break;
                }
                
                if (data != null)
                {
                    VModel.OpenSubWindowCommandPointEntry.Execute(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        /// <summary>
        /// 【簡易設定】卸地ポイント設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OroshiSetPoint_ClickAsync(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            Button me = (Button)sender;

            try
            {
                int id;
                switch (me.Tag)
                {
                    case "Complex":
                        PointDto_Local point = (PointDto_Local)me.DataContext;
                        id = point.PointId;
                        break;
                    default:
                        id = 1;
                        break;
                }
                me.IsEnabled = false;
                string latlon = await webView.CoreWebView2.ExecuteScriptAsync("MapGetCenterPoint();");
                if ("ul".Equals(latlon)) { return; }
                latlon = latlon.Substring(1, latlon.Length - 2);
                bool resultflg = await VModel.SetPointForPointListAsync(sender, e, latlon, VModel.OroshiPointList, id);
                if (resultflg)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("SetMarkerToPoint(\"" + latlon.Split(",")[1] + "\",\"" + latlon.Split(",")[0] + "\",\"" + "end" + "\");");
                    SetPolyline();
                }
                else
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("ClearMarkerToPoint(\"end" + "\");");
                    await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");
                }

                PointDto_Local data = VModel.OroshiPointList.FirstOrDefault(m => m.PointId == id);
                btnOroshiPointReg.IsEnabled = data.BuildingZid != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }

        }

        /// <summary>
        /// 地図にポリシーラインを書く
        /// </summary>
        private async void SetPolyline()
        {
            string html = VModel.GetDriveRotePointForHtml();
            if (html == null) { return; }
            /// 地図にポリシーラインを書く
            await webView.CoreWebView2.ExecuteScriptAsync("$('#Polyline').empty();");
            await webView.CoreWebView2.ExecuteScriptAsync("$('#Map1').empty();");
            await webView.CoreWebView2.ExecuteScriptAsync(html);
            await webView.CoreWebView2.ExecuteScriptAsync("setPolyline(1,'solid');");
        }

        /// <summary>
        /// 地図APIのログオフ処理（localhost用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MapLogOff_ClickAsync(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            
            try
            {
                await webView.CoreWebView2.ExecuteScriptAsync("logoutForDesktop();");

            MessageBox.Show("MapLogOff");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        /// <summary>
        /// ルート検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchRoute_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            VModel.ThisView = this;
         
            try
            {
                if (RootSearchValidationCheck()) { return; }

                await VModel.GetDriveRouteListAsync();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                me.IsEnabled = true;
            }

        }

        /// <summary>
        /// ルート検索処理時の必須チェック
        /// </summary>
        /// <returns></returns>
        private bool RootSearchValidationCheck()
        {
            bool result = true;

            try
            {

                if (!(VModel.Syasyu != null && VModel.Syasyu.Value.Length > 0)) { throw new Exception("車種が選択されていません"); }
                if (!(VModel.Kata != null && VModel.Kata.Value.Length > 0)) { throw new Exception("型が選択されていません"); }

                if ((VModel.TsumiPointList == null || VModel.TsumiPointList[0].Lat == null || VModel.TsumiPointList[0].Lat.Length == 0)) { throw new Exception("積み場所が指定されていません"); }
                if ((VModel.OroshiPointList == null || VModel.OroshiPointList[0].Lat == null || VModel.OroshiPointList[0].Lat.Length == 0)) { throw new Exception("卸し場所が指定されていません"); }

                result = false;

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                
            }

            return result;
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
                } else
                {
                    latlon = await VModel.GetlatlonFromAddress();
                    
                }

                if (latlon != null)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("map.setCenter(new ZDC.LatLng(Number(" + latlon.lat + "),Number(" + latlon.lng + ")));");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 一時登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempReg_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;
            //MessageBox.Show(VModel.TsumiDate.Value.ToString());

            try
            {

                string sCopy = VModel.SelectedDriveRouteDisplay.RouteTypeDisplay + "\t" + 
                    VModel.SelectedDriveRouteDisplay.TotalTime + "\t" +
                    VModel.SelectedDriveRouteDisplay.TotalDistance + "\t" +
                    VModel.SelectedDriveRouteDisplay.Totaltoll + "\t" +
                    VModel.SelectedDriveRouteDisplay.GrossAmountTotal + "\t" +
                    VModel.SelectedDriveRouteDisplay.StdALLFreight + "\t" +
                    VModel.Syasyu.Value + VModel.Kata.Value
                    ;

                Clipboard.SetText(sCopy);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowState = System.Windows.WindowState.Maximized;

                if (VModel.AnkenOpenMode != AnkenOpenModeEnum.AddNew)
                {
                    await VModel.GetEditData(VModel.AnkenId.Value);
                }

                VModel.SetRegUpdateContent();

                ////数値の為、NULLチェック必要なし
                RadioButton rb = this.FindName("AnkenStatus_" + VModel.AnkenStatus.Value) as RadioButton;
                rb.IsChecked = true;

                rb = this.FindName("SeikyuKubun_" + VModel.SeikyuKubun.Value) as RadioButton;
                rb.IsChecked = true;

                rb = this.FindName("NumberComm_" + VModel.NumberCommLimitKubun.Value) as RadioButton;
                rb.IsChecked = true;

                ////文字列の為、NULLチェックを実施
                if (VModel.Ferry.Value != null && VModel.Ferry.Value != "")
                {
                    rb = this.FindName("Ferry_" + VModel.Ferry.Value) as RadioButton;
                    rb.IsChecked = true;
                }
                if (VModel.Regulation.Value != null && VModel.Regulation.Value != "")
                {
                    string val = VModel.Regulation.Value.Replace(",", "_");
                    rb = this.FindName("Regulation_" + val) as RadioButton;
                    rb.IsChecked = true;
                }
                if (VModel.Twouturn.Value != null && VModel.Twouturn.Value != "")
                {
                    rb = this.FindName("Twouturn_" + VModel.Twouturn.Value) as RadioButton;
                    rb.IsChecked = true;
                }

                ///車番連絡入力項目の有効変更処理
                ChangeEnableForNumberCommLimit();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車番連絡入力項目の有効変更処理
        /// </summary>
        private void ChangeEnableForNumberCommLimit()
        {
            if (1 == VModel.NumberCommLimitKubun.Value)
            {
                NumberCommLimit.IsEnabled = true;
            }
            else
            {
                NumberCommLimit.IsEnabled = false;
            }
        }


        private void FerryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.Ferry.Value = radio.Tag.ToString();　
        }

        private void AnkenStatusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.AnkenStatus.Value = int.Parse(radio.Tag.ToString());
        }

        private void RegulationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.Regulation.Value = radio.Tag.ToString();
        }

        private void TwouturnRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.Twouturn.Value = radio.Tag.ToString();
        }

        private void SeikyuKubunButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.SeikyuKubun.Value = int.Parse(radio.Tag.ToString());
        }

        private void NumberCommRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            VModel.NumberCommLimitKubun.Value = int.Parse(radio.Tag.ToString());
            ///車番連絡入力項目の有効変更処理
            ChangeEnableForNumberCommLimit();
        }

        private void OnMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            DatePicker dp = sender as DatePicker;
            if (dp != null)
            {
                dp.IsDropDownOpen = true;
            }
        }

        /// <summary>
        /// 積卸し時間の加算・減算処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsumiOroshiTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string ctlName = (string)btn.Tag;
                string content = (string)btn.Content;

                string time = VModel.TsumiTaskTime.Value;
                if ("Oroshi".Equals(ctlName)) { time = VModel.OroshiTaskTime.Value; }

                DateTime dtData = DateTime.Parse(time);
                if ("＋".Equals(content))
                {
                    dtData = dtData.AddMinutes(30);
                }
                else
                {
                    dtData = dtData.AddMinutes(-30);
                }

                if ("Tsumi".Equals(ctlName)) { VModel.TsumiTaskTime.Value = dtData.ToString("HH:mm"); }
                if ("Oroshi".Equals(ctlName)) { VModel.OroshiTaskTime.Value = dtData.ToString("HH:mm"); }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 時間選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimePiker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string ctlName = (string)btn.Tag;

                ViewModel.Tool.SelectTimePickerViewModel vm = new();
                App app = App.Current as App;
                View.Tool.SelectTimePicker view = (View.Tool.SelectTimePicker)app.ShowModalView(vm, this);

                if (view != null)
                {
                    string h = view.Houer;
                    string m = view.Munute;

                    TextBox tb = this.FindName(ctlName) as TextBox;
                    tb.Text = h + ":" + m;
                    var bindingExpression = BindingOperations.GetBindingExpression(tb, TextBox.TextProperty);
                    bindingExpression.UpdateSource();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 追加項目画面を非表示にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionMenuMin_Click(object sender, RoutedEventArgs e)
        {
            OptionMenuMin.Visibility = Visibility.Hidden;
            OptionMenuMax.Visibility = Visibility.Visible;

            AnkenMain.Width = new GridLength(4.3, GridUnitType.Star);
            AnkenOption.Width = new GridLength(0, GridUnitType.Star);
            RouteMap.Width = new GridLength(5.6, GridUnitType.Star);

        }

        /// <summary>
        /// 追加項目画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionMenuMax_Click(object sender, RoutedEventArgs e)
        {
            OptionMenuMax.Visibility = Visibility.Hidden;
            OptionMenuMin.Visibility = Visibility.Visible;

            AnkenMain.Width = new GridLength(4.3, GridUnitType.Star);
            AnkenOption.Width = new GridLength(2.5, GridUnitType.Star);
            RouteMap.Width = new GridLength(3.1, GridUnitType.Star);
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
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnTsumiPlus_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            MessageBox.Show("PreviewTouchDown");
        }

        private void btnKokyaku_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectKokyaku.Execute(null);
        }

        private void btnSyasyu_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectSyasyu.Execute(null);
        }

        private void btnKakoAnken_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectKakoAnken.Execute(null);
        }

        private void btnSelectTantou_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectTantou.Execute("tantou");
        }

        private void btnSelectEigyo_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectTantou.Execute("eigyo");
        }

        private void btnDaisu_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectDaisu.Execute(null);
        }

        private void btnTopMenu2_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSetOyaKokyaku.Execute(null);
        }

        /// <summary>
        /// 注文書印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrderPrint_Click(object sender, RoutedEventArgs e)
        {
            if (VModel.AnkenId.Value == 0)
            {
                //System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("登録??", "発注書を印刷するには一度登録してください。", System.Windows.Forms.MessageBoxButtons.YesNo);
                //if (result == System.Windows.Forms.DialogResult.Yes)
                //{ 

                //}

                MessageBox.Show("発注書を印刷するには一度登録してください");
                return;
            }

            VModel.ThisView = this;
            VModel.OpenSubWindowCommandPrintOrder.Execute(null);
        }

        private void btnShijiPrint_Click(object sender, RoutedEventArgs e)
        {

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

                int iAnkenStatus = 18;
                int iOther = 14;
                int iMenuBtn = 26;

                if (this.WindowState == WindowState.Maximized)
                {
                    //MessageBox.Show(screen.Bounds.Height.ToString());
                    dHeight = screen.Bounds.Height;
                }

                if (dHeight < 800)
                {
                    iAnkenStatus = 12;
                    iOther = 10;
                    iMenuBtn = 12;
                    this.Head.Height = new GridLength(double.Parse("25"));
                    this.Tail.Height = new GridLength(double.Parse("25"));
                } else
                {
                    this.Head.Height = new GridLength(double.Parse("50"));
                    this.Tail.Height = new GridLength(double.Parse("50"));
                }

                foreach (RadioButton tb in FindVisualChildren<RadioButton>(c))
                {
                    if (tb.Name.Contains("AnkenStatus"))
                    {
                        tb.FontSize = iAnkenStatus;
                    } else
                    {
                        tb.FontSize = iOther;
                    }
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
                    if (child != null && child is T)
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

        private void OyaYosya_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 登録（履歴追加）処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Reg_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;

            try
            {
                if ("".Equals(me.Content)) { return; }

                string errorMessage = string.Empty;
                if (VModel.InputCheck(out errorMessage))
                {
                    me.IsEnabled = true;
                    // チェックNG
                    MessageBox.Show(errorMessage, "入力エラー");
                    return;
                }
                

                string msg = "新規案件登録を行いますか？";
                if (VModel.t_Anken_Locals.Value != null && VModel.t_Anken_Locals.Value.Anken_ID > 0)
                {
                    msg = "登録されている案件データに変更履歴データを追加しますか？";
                }


                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(msg, "登録確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        await VModel.RegisterForAddNew();

                        System.Windows.Forms.DialogResult result2 = System.Windows.Forms.MessageBox.Show("登録が正常に終了しました。画面を閉じますか？", "確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                        if (result2 == System.Windows.Forms.DialogResult.Yes) { Close(); }
                        me.Content = "履歴登録";

                    } catch(Exception ex)
                    {
                        me.IsEnabled = true;
                        throw;
                    }
                } else
                {
                    me.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("案件データ新規登録エラー：" + ex.Message);
                me.IsEnabled = true;
            }
            finally
            {
                //me.IsEnabled = true;
            }


        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private　async void RegUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
            me.IsEnabled = false;

            try
            {
                if ("".Equals(me.Content)) { return; }

                string errorMessage = string.Empty;
                if (VModel.InputCheck(out errorMessage))
                {
                    // チェックNG
                    MessageBox.Show(errorMessage, "入力エラー");
                    return;
                }

                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("最新の案件情報を更新(上書き)しますか??", "更新確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    await VModel.RegisterForUpdate();

                    System.Windows.Forms.DialogResult result2 = System.Windows.Forms.MessageBox.Show("登録が正常に終了しました。画面を閉じますか？", "確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (result2 == System.Windows.Forms.DialogResult.Yes) { Close(); }
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("案件データ新規登録エラー：" + ex.Message);
            }
            finally
            {
                me.IsEnabled = true;
            }

        }

        /// <summary>
        /// 追加料金リストの削除ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExchargeDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dto.AnkenExchargeDto_Local dto = (Dto.AnkenExchargeDto_Local)dgExtraChargeListEx.SelectedItem;
                VModel.ExtraChargeListExDel(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("【エラー】" + ex.Message);
            }
            
        }

        /// <summary>
        /// 追加料金リストの追加ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExchargeAdd_Click(object sender, RoutedEventArgs e)
        {
            VModel.ThisView = this;
            VModel.OpenSubWindowCommandSelectExcharge.Execute(null);
        }

        /// <summary>
        /// 追加料金リストの金額更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExchargeUpDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("選択された車種サイズを元にマスタからデータを更新しますか？", "確認", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    await VModel.ExtraChargeListExUpdate();
                    MessageBox.Show("更新処理が終了しました。");
                } 
            }
            catch(Exception ex)
            {
                MessageBox.Show("【エラー】" + ex.Message);
            }
        }

        /// <summary>
        /// 割引項目の入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Discount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return;}
            var tb = (TextBox)sender;

            if (VModel.GrossAmount.Value == 0) { return; }

            var val = VModel.BaseFee.Value + VModel.ExtraCharge.Value + VModel.Toll.Value - double.Parse(tb.Text);
            VModel.GrossAmount.Value = val;

            GrossAmount.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrossAmount_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; }
            var tb = (TextBox)sender;

            var val = double.Parse(tb.Text) - (VModel.BaseFee.Value + VModel.ExtraCharge.Value + VModel.Toll.Value);
            VModel.Discount.Value = -val;
        }

        /// <summary>
        /// 有料料金項目の入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toll_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; }
            var tb = (TextBox)sender;

            if (VModel.GrossAmount.Value == 0) { return; }

            var val = VModel.BaseFee.Value + VModel.ExtraCharge.Value + double.Parse(tb.Text) - VModel.Discount.Value;
            VModel.GrossAmount.Value = val;

            Discount.Focus();
        }

        /// <summary>
        /// その他料金項目の入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtraCharge_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) { return; }
            var tb = (TextBox)sender;

            if (VModel.GrossAmount.Value == 0) { return; }

            var val = VModel.BaseFee.Value + double.Parse(tb.Text) + VModel.Toll.Value - VModel.Discount.Value;
            VModel.GrossAmount.Value = val;

            Toll.Focus();
        }
    }

    /// <summary>
    /// WebView2に読み込ませるためのJsで実行する関数を保持させたクラス
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class JstoCs
    {
        public void Test(string sText)
        {
            MessageBox.Show(sText);
        }
    }


}
