using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Anken
{
    class DriveRouteListViewModel : AnkenViewModel
    {




        public DriveRouteListViewModel(AnkenViewModel model)
        {

            DriveRouteListData = model.DriveRouteListData;

            ExtraChargeList = model.ExtraChargeList;

            Syasyu.Value = model.Syasyu.Value;
            Kata.Value = model.Kata.Value;
            SyasyuSize.Value = model.SyasyuSize.Value;
            Eria.Value = model.Eria.Value;

        }


    }
}
