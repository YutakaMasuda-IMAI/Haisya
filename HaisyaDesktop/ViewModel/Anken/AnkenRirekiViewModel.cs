using Livet.Commands;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.ViewModel.Anken
{
    class AnkenRirekiViewModel: BaseViewModel
    {

        //public ObservableCollection<V_AnkenDataList_Local> AnkenList { get; set; }

        //public ObservableCollection<string> selectedAnkenList { get; set; }

        public ReactiveProperty<Dto.V_AnkenDataList_Local> SelectAnkenListL { get; set; }
        public ReactiveProperty<Dto.V_AnkenDataList_Local> SelectAnkenListR { get; set; }

        public ReactiveProperty<int> SelectAnkenListIndexL { get; set; }
        public ReactiveProperty<int> SelectAnkenListIndexR { get; set; }


        public AnkenRirekiViewModel()
        {

            V_AnkenDataList_LocalList = new();
            SelectedV_AnkenDataList_LocalR = new();
            SelectedV_AnkenDataList_LocalL = new();

            SelectAnkenListL = new();
            SelectAnkenListR = new();
            SelectAnkenListIndexL  = new();
            SelectAnkenListIndexR = new();

            Temp();


            SelectAnkenListIndexL.Value = 3;
            SelectAnkenListIndexR.Value = 4;
        }

        private void Temp()
        {

            //V_AnkenDataList_LocalList.Add(new V_AnkenDataList_Local { UpdateDateTime = DateTime.Parse("2022/06/25 10:15"), UpdateName = "〇〇　太郎", RirekiDisplay = "当初" });
            //V_AnkenDataList_LocalList.Add(new V_AnkenDataList_Local { UpdateDateTime = DateTime.Parse("2022/06/30 18:15"), UpdateName = "〇〇　太郎", RirekiDisplay = "" });
            //V_AnkenDataList_LocalList.Add(new V_AnkenDataList_Local { UpdateDateTime = DateTime.Parse("2022/07/02 09:15"), UpdateName = "配車　太郎", RirekiDisplay = "" });
            //V_AnkenDataList_LocalList.Add(new V_AnkenDataList_Local { UpdateDateTime = DateTime.Parse("2022/07/06 11:15"), UpdateName = "〇〇　太郎", RirekiDisplay = "" });
            //V_AnkenDataList_LocalList.Add(new V_AnkenDataList_Local { UpdateDateTime = DateTime.Parse("2022/07/12 19:15"), UpdateName = "〇〇　太郎", RirekiDisplay = "最新" });
            SelectedV_AnkenDataList_LocalL = V_AnkenDataList_LocalList.ElementAt(3);
            SelectedV_AnkenDataList_LocalR = V_AnkenDataList_LocalList.ElementAt(4);
        }


        #region V_AnkenDataList_LocalList変更通知プロパティ
        private ObservableCollection<Dto.V_AnkenDataList_Local> _V_AnkenDataList_LocalList;

        public ObservableCollection<Dto.V_AnkenDataList_Local> V_AnkenDataList_LocalList
        {
            get
            {
                return _V_AnkenDataList_LocalList;
            }
            set
            {
                if (object.Equals(_V_AnkenDataList_LocalList, value)) return;
                _V_AnkenDataList_LocalList = value;
                RaisePropertyChanged(nameof(V_AnkenDataList_LocalList));
            }
        }
        #endregion

        #region SelectedV_AnkenDataList_LocalR変更通知プロパティ
        private Dto.V_AnkenDataList_Local _SelectedV_AnkenDataList_LocalR;

        public Dto.V_AnkenDataList_Local SelectedV_AnkenDataList_LocalR
        {
            get
            {
                return _SelectedV_AnkenDataList_LocalR;
            }
            set
            {
                if (object.Equals(_SelectedV_AnkenDataList_LocalR, value)) return;
                _SelectedV_AnkenDataList_LocalR = value;
                RaisePropertyChanged(nameof(SelectedV_AnkenDataList_LocalR));
            }
        }
        #endregion

        #region SelectedV_AnkenDataList_Local変更通知プロパティ
        private Dto.V_AnkenDataList_Local _SelectedV_AnkenDataList_LocalL;

        public Dto.V_AnkenDataList_Local SelectedV_AnkenDataList_LocalL
        {
            get
            {
                return _SelectedV_AnkenDataList_LocalL;
            }
            set
            {
                if (object.Equals(_SelectedV_AnkenDataList_LocalL, value)) return;
                _SelectedV_AnkenDataList_LocalL = value;
                RaisePropertyChanged(nameof(SelectedV_AnkenDataList_LocalL));
            }
        }
        #endregion


    }


}
