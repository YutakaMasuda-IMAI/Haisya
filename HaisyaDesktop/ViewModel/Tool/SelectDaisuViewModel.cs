using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Tool
{
    class SelectDaisuViewModel : BaseViewModel
    {

        public int SelectData { set; get; }

        public List<int> DaisuList { set; get; }

        public SelectDaisuViewModel()
        {
            DaisuList = new();
            for (int i = 1; i <= 10; i++)
            {
                DaisuList.Add(i);
            }


        }





    }
}
