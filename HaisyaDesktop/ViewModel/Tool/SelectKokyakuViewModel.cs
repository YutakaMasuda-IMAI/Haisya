using HaisyaDesktop.API.WebApp;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Tool
{
    internal class SelectKokyakuViewModel : BaseViewModel
    {

        public AsyncReactiveCommand ButtonClickAsyncCommand1 { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand ButtonClickAsyncCommand2 { get; } = new AsyncReactiveCommand();

        //public ObservableCollection<Dto.M_Customer_Local> KokyakuFullList { get; set; }
        public ObservableCollection<Dto.M_Customer_Local> KokyakuList { get; set; }
        public ReactiveProperty<string> SelectKokyaku { get; set; }

        public ObservableCollection<Dto.M_Customer_Local> MostPastPointList { get; set; }
        public ReactiveProperty<string> SelectMostPastPoint { get; set; }

        public ObservableCollection<Dto.M_Customer_Local> MostRecentList { get; set; }
        public ReactiveProperty<string> SelectMostRecent { get; set; }

        public ReactiveProperty<string> SearchKey { get; set; }

        public string TitleForTop30 { get; set; }


        public SelectKokyakuViewModel()
        {
            Context.User user = GetUserData();

            ButtonClickAsyncCommand1.Subscribe(async _ => { await Task.Delay(500); });
            ButtonClickAsyncCommand2.Subscribe(async _ => { await Task.Delay(500); });

            //KokyakuFullList = new();
            KokyakuList = new();

            SelectKokyaku = new();

            MostPastPointList = new();
            MostRecentList = new();

            SelectMostPastPoint = new();
            SelectMostRecent = new();

            TitleForTop30 = user.UserName + "さんの使用率TOP30";
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task DataLoad()
        {
            Context.User user = GetUserData();

            MasterDataApi api = new();
            //List<Dto.M_Customer_Local> m_CustomerList = await api.M_CustomerList(user.CompanyID);
            //CopyProperty(KokyakuFullList, m_CustomerList);
            //RaisePropertyChanged(nameof(KokyakuFullList));

            AnkenDataApi ankenApi = new();
            MostRecentList = new ObservableCollection<Dto.M_Customer_Local>(await ankenApi.GetCustomerListToMostRecentForUser(user.CompanyID, user.UserId, 30));
            MostPastPointList = new ObservableCollection<Dto.M_Customer_Local>(await ankenApi.GetCustomerListForUser(user.CompanyID, user.UserId, 30));

            RaisePropertyChanged(nameof(MostRecentList));
            RaisePropertyChanged(nameof(MostPastPointList));



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DataLoadForKey(string key)
        {
            Context.User user = GetUserData();
            KokyakuList = new();

            MasterDataApi api = new();
            List<Dto.M_Customer_Local> m_CustomerList = await api.CustomerList(user.CompanyID, key, null);
            if (m_CustomerList == null) { return; }
            foreach(var data in m_CustomerList)
            {
                Dto.M_Customer_Local dto = new();
                CopyProperty(dto, data);
                KokyakuList.Add(dto);
            }

            RaisePropertyChanged(nameof(KokyakuList));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DataLoadForCode(string cd)
        {
            Context.User user = GetUserData();
            KokyakuList = new();

            MasterDataApi api = new();
            List<Dto.M_Customer_Local> m_CustomerList = await api.CustomerList(user.CompanyID, null, cd);
            if (m_CustomerList == null) { return; }
            foreach (var data in m_CustomerList)
            {
                Dto.M_Customer_Local dto = new();
                CopyProperty(dto, data);
                KokyakuList.Add(dto);
            }

            RaisePropertyChanged(nameof(KokyakuList));

        }
    }
}
