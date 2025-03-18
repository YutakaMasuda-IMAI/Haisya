using HaisyaDesktop.Dto;
using HaisyaDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Tool
{
    class SelectAddressDetailViewModel: BaseViewModel
    {

        public List<MapApiModel.Map_Building_NameItem_Local> ItemList { set; get; }

        public List<MapApiModel.MapAddressItem_Local> Addresslist { set; get; }

        public SelectAddressDetailViewModel(List<MapApiModel.Map_Building_NameItem_Local> _itemList,
                                        List<MapApiModel.MapAddressItem_Local> _addresslist)
        {
            ItemList = _itemList;
            Addresslist = _addresslist;

        }




    }
}
