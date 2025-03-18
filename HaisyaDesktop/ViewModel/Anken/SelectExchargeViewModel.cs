using HaisyaDesktop.API.WebApp;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Data;

namespace HaisyaDesktop.ViewModel.Anken
{

    class SelectExchargeViewModel : BaseViewModel
    {

        public List<Dto.M_Anken_Excharge_Dto> AnkenExchargeList { get; set; }
        
        public string SyasyuSize { get; set; }

        public SelectExchargeViewModel(string _SyasyuSize)
        {
            SyasyuSize = _SyasyuSize;
            AnkenExchargeList = new();
        }

        public async Task DataLoad()
        {
            AnkenExchargeList = new();
            Context.User user = GetUserData();

            MasterDataApi api = new();
            List<Dto.M_Anken_Excharge_Local> list = await api.M_Anken_ExchargeList(user.CompanyID, SyasyuSize);
            List<Dto.M_Anken_Excharge_Local> listData = list.Where(m => m.HIDDEN_FLG == false).ToList();

            foreach (var data in listData)
            {
                Dto.M_Anken_Excharge_Dto dto = new();
                CopyProperty(dto, data);
                AnkenExchargeList.Add(dto);
            }
            RaisePropertyChanged(nameof(AnkenExchargeList));

        }


    }


}
