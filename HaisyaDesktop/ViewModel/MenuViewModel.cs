using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Context;
using HaisyaDesktop.Dto;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel
{
    class MenuViewModel: BaseViewModel
    {
        public ReactiveProperty<string> Title { set; get; }

        public MenuViewModel()
        {
            Context.User user = GetUserData();
            Title = new() { Value = "ようこそ　" + user.UserName + "　さま" };

        }


        public async Task SetAddressList()
        {

            Context.AddressList addressList = new();

            MasterDataApi api = new();
            addressList.HokkaidoAddressItem = await api.GetGetAddressList(1);
            addressList.TohokuAddressItem = await api.GetGetAddressList(2);
            addressList.ChubuAddressItem = await api.GetGetAddressList(3);
            addressList.KantoAddressItem = await api.GetGetAddressList(4);
            addressList.KinkiAddressItem = await api.GetGetAddressList(5);
            addressList.ChugokuAddressItem = await api.GetGetAddressList(6);
            addressList.ShikokuAddressItem = await api.GetGetAddressList(7);
            addressList.KyusyuAddressItem = await api.GetGetAddressList(8);
            addressList.OkinawaAddressItem = await api.GetGetAddressList(9);
            ContextManager.Instance.SetAddressOnce(addressList);

        }

    }
}
