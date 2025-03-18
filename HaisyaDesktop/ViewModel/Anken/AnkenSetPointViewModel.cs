using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.ViewModel.Anken
{
    class AnkenSetPointViewModel : BaseViewModel
    {

        public ObservableCollection<PointDto_Local> PointList { get; set; }

        public AnkenSetPointViewModel()
        {


            PointList = new();
            PointList.Add(new PointDto_Local { PointId = 1, Title = "積み", Address = "", Lng = "", Lat = "", TollDisplayHeight = 0,TollDisplay = "" });;
            PointList.Add(new PointDto_Local { PointId = 99, Title = "卸し", Address = "", Lng = "", Lat = "", TollDisplayHeight = 0, TollDisplay = "" });

        }


        public void aaaa(int PointID)
        {

            var data = PointList.FirstOrDefault(m => m.PointId == PointID);

            if (data != null)
            {
                


                ObservableCollection<PointDto_Local> temp = new();

                foreach (PointDto_Local d in PointList)
                {
                    if (d.PointId == PointID)
                    {
                        d.TollDisplay = "あああああああ";
                        d.TollDisplayHeight = 30;
                    }

                    temp.Add(d);

                }

                PointList.Clear();

                foreach (PointDto_Local d in temp)
                {
                    PointList.Add(d);
                }


            }

            //RaiseErrorsChanged(nameof(PointList));

        }


    }
}
