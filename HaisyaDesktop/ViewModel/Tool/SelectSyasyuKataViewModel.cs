using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Dto;
using HaisyaDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Tool
{
    class SelectSyasyuKataViewModel :BaseViewModel
    {

        public List<M_SyaryoSize_Local> SyaryoSizeList { get; set; }

        public List<M_Syaryo_Local> SyaryoList { get; set; }



        public SelectSyasyuKataViewModel()
        {

            SyaryoSizeList = new();
            SyaryoList = new();

        }

        /// <summary>
        /// データロード
        /// </summary>
        /// <returns></returns>
        public async Task DataLoad()
        {
            Context.User user = GetUserData();

            MasterDataApi api = new();
            SyaryoSizeList = await api.GetSyaryoSizeList(user.CompanyID);
            SyaryoList = await api.GetSyaryoList(user.CompanyID);

        }


    }
}
