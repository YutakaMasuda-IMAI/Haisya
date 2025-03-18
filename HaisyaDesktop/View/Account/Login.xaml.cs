using HaisyaDesktop.Context;
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

namespace HaisyaDesktop.View.Account
{
    /// <summary>
    /// Login.xaml の相互作用ロジック
    /// </summary>
    public partial class Login : Window
    {

        private ViewModel.Account.LoginViewModel VModel => (ViewModel.Account.LoginViewModel)DataContext;

        public Login()
        {
            InitializeComponent();

            ViewModel.Account.LoginViewModel vm = new();
            DataContext = vm;


        }

        private void btnForgetPass_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("未実装");
        }

        private async void btnLoginExec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnLoginExec.IsEnabled = false;
                txtPass.IsEnabled = false;

                if (ValidationCheck()) { return; }

                VModel.Password.Value = txtPass.Password;
                bool result = await VModel.LoginExec(this);
                if (result) {
                    btnLoginExec.IsEnabled = true;
                    txtPass.IsEnabled = true;
                    return; }

                btnLoginExec.IsEnabled = true;
                txtPass.IsEnabled = true;
                Close();

            } catch (Exception ex)
            {
                btnLoginExec.IsEnabled = true;
                txtPass.IsEnabled = true;
                MessageBox.Show("エラー：" + ex.Message);
            }


        }

        private async void Password_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) { return; }

                btnLoginExec.IsEnabled = false;
                txtPass.IsEnabled = false;

                if (ValidationCheck()) { return; }

                VModel.Password.Value = txtPass.Password;
                bool result = await VModel.LoginExec(this);
                if (result) { return; }

                btnLoginExec.IsEnabled = true;
                txtPass.IsEnabled = true;
                Close();

            }
            catch (Exception ex)
            {
                btnLoginExec.IsEnabled = true;
                txtPass.IsEnabled = true;
                MessageBox.Show("エラー：" + ex.Message);
            }
        }

        private bool ValidationCheck()
        {
            System.Text.StringBuilder errMsg = new System.Text.StringBuilder();
            IInputElement ctlFocus = null;

            bool func(IInputElement objControl, string strName, string strValue)
            {
                if (string.IsNullOrEmpty(strValue.Trim()))
                {
                    errMsg.AppendFormat("{0}が未入力です。{1}", strName, Environment.NewLine);
                    if (objControl.GetType().Equals(typeof(TextBox))) { ((TextBox)objControl).BorderBrush = Brushes.Red; }
                    if (objControl.GetType().Equals(typeof(PasswordBox))) { ((PasswordBox)objControl).BorderBrush = Brushes.Red; }
                    if (ctlFocus == null)
                    {
                        ctlFocus = objControl;
                    }
                    return true;
                }
                return false;
            }

            _ = func(txtKaisyaCD, txtKaisyaCD.Tag.ToString(), txtKaisyaCD.Text);
            _ = func(txtUserID, txtUserID.Tag.ToString(), txtUserID.Text);
            _ = func(txtPass, txtPass.Tag.ToString(), txtPass.Password);

            if (errMsg != null && errMsg.Length > 0)
            {
                _ = MessageBox.Show(errMsg.ToString(), "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                _ = ctlFocus.Focus();
                return true;
            }
            return false;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ユーザ情報、セッションクッキーのクリア
            ContextManager.Instance.ClearUserInfo();
            // 会社ユーザ情報、セッションクッキーのクリア
            ContextManager.Instance.ClearCompanyUserInfo();

            WindowState = System.Windows.WindowState.Maximized;

        }



    }
}
