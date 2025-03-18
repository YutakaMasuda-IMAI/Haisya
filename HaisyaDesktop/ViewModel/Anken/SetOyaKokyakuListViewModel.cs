using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HaisyaDesktop.Models.AnkenModel;

namespace HaisyaDesktop.ViewModel.Anken
{
    class SetOyaKokyakuListViewModel: BaseViewModel
    {

        /// <summary>親顧客リスト</summary>
        public ObservableCollection<Dto.T_Anken_OyaKokyaku_Local> OyaKokyakuList { get; set; }
        // <summary>最終顧客ID</summary>
        public int KokyakuId { get; set; }
        // <summary>最終顧客データ(M_Customer)</summary>
        public Dto.M_Customer_Local KokyakuData { get; set; }
        // <summary>最終顧客担当者ID</summary>
        public int KokyakuTantouId { get; set; }
        // <summary>最終顧客担当名称</summary>
        public string KokyakuTantouName { get; set; }
        // <summary>最終顧客担当名称</summary>
        public string KokyakuTantouPhone { get; set; }

        public SetOyaKokyakuListViewModel(ObservableCollection<Dto.T_Anken_OyaKokyaku_Local> _OyaKokyakuList,
                                            int _KokyakuId, Dto.M_Customer_Local _CustomerData, int _KokyakuTantouId,
                                            string _KokyakuTantouName, string _KokyakuTantouPhone)
        {
            if (_OyaKokyakuList == null)
            {
                OyaKokyakuList = new();
                OyaKokyakuList.Add(new Dto.T_Anken_OyaKokyaku_Local {  Anken_Order = 1, KokyakuId = 0, KokyakuName = "", KomokuTitle = "親顧客１" });
            } else
            {
                OyaKokyakuList = new();
                OyaKokyakuList = _OyaKokyakuList;
            }

            KokyakuId = _KokyakuId;
            KokyakuData = _CustomerData;
            KokyakuTantouId = _KokyakuTantouId;
            KokyakuTantouName = _KokyakuTantouName;
            KokyakuTantouPhone = _KokyakuTantouPhone;
        }


        public void ResetOyaKokyakuListTitle(int ankenOrder)
        {

            Dto.T_Anken_OyaKokyaku_Local data = OyaKokyakuList.FirstOrDefault(m => m.Anken_Order == ankenOrder);
            if (data != null)
            {
                _ = OyaKokyakuList.Remove(data);
            }

            ObservableCollection<Dto.T_Anken_OyaKokyaku_Local> _OyaKokyakuList = new();

            for (int i = 0; i < OyaKokyakuList.Count; i++)
            {
                string title = "親顧客" + Utils.StringsConvert.StrConvToWide((i + 1).ToString());
                OyaKokyakuList[i].Anken_Order = i + 1;
                OyaKokyakuList[i].KomokuTitle = title;
                RaisePropertyChanged(nameof(OyaKokyakuList));
                _OyaKokyakuList.Add(OyaKokyakuList[i]);
            }

            OyaKokyakuList.Clear();
            OyaKokyakuList = _OyaKokyakuList;

            RaisePropertyChanged(nameof(OyaKokyakuList));

        }


    }
}
