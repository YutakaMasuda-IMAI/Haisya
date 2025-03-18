using HaisyaDesktop.Context;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HaisyaDesktop.ViewModel.Account
{

    class LoginViewModel
    {
        public ReactiveProperty<string> KaisyaCD { get; set; }

        public ReactiveProperty<string> UserID { get; set; }

        public ReactiveProperty<string> Password { get; set; }


        public LoginViewModel()
        {

            KaisyaCD = new() { Value = "100001" };
            UserID = new() { Value = "" };
            Password = new() { Value = "" };

            UserID.Value = Environment.UserName;
            


        }

        public async Task<bool> LoginExec(Window view)
        {
            API.WebApp.MasterDataApi api = new();
            Dto.V_LoginUser_Local userData = await api.GetLoginUserList(KaisyaCD.Value, UserID.Value);
            if (userData == null)
            {
                MessageBox.Show("会社IDかユーザーIDが間違っています。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            if (!Password.Value.Equals(userData.Password, StringComparison.Ordinal))
            {
                MessageBox.Show("パスワードが間違っております。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            // ユーザ情報、セッションクッキーのクリア
            ContextManager.Instance.ClearUserInfo();

            Context.User user = new()
            {
                CompanyID = userData.Company_ID,
                UserId = userData.User_ID,
                LoginId = userData.LoginID,
                UserName = userData.User_Name,
                Syasyu = userData.DefaultSyasyu,
                Kata = userData.DefaultKata,
                SyasyuSize = userData.DefaultSize,
                SyasyuDisplay = userData.SyasyuDisplay,
                KataDisplay = userData.KataDisplay,
                HaisyaTantouID = userData.Tntou_ID,
            };

            ContextManager.Instance.SetUserOnce(user);

            ViewModel.MenuViewModel vm = new();
            App app = App.Current as App;
            app.ShowView(vm, view, true);

            return false;
        }

        public async Task ForgetPassword()
        {

        }


    }
}
