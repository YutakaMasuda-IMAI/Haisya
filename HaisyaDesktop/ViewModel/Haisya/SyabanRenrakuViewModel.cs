using HaisyaDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HaisyaDesktop.ViewModel.Haisya
{
    class SyabanRenrakuViewModel: BaseViewModel
    {

        public ObservableCollection<SyabanRenraku> SyabanRenrakuList { get; set; }

        public ICommand OpenSubWindowCommandPreview { get; private set; }

        public System.Windows.Window ThisView { get; set; }

        public SyabanRenrakuViewModel()
        {

            SyabanRenrakuList = new();

            OpenSubWindowCommandPreview = CreateCommand(v =>
            {
                App app = App.Current as App;
                View.Haisya.SyabanPreview win = (View.Haisya.SyabanPreview)app.ShowModalView(new SyabanPreviewViewModel(), ThisView);
                if (win == null)
                {

                }
                else
                {

                }
            });

            Temp();
        }


        private void Temp()
        {

            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運輸", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 5, AnkenCommitCount = 3, AnkenTempCount = 1,AnkenTempCountFlg = 1, AnkenNonCount = 1,AnkenNonCountFlg = 1,
                NumberCommLimitDateDisplay = null, RowColor = "#FFC0CB", Status = "未配車あり",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運送", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 3, AnkenCommitCount = 3, AnkenTempCount = 0, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#A9A9A9", Status = "連絡完了",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運送", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 2, AnkenCommitCount = 0, AnkenTempCount = 1,AnkenTempCountFlg = 1, AnkenNonCount = 1,AnkenNonCountFlg = 1,
                NumberCommLimitDateDisplay = null, RowColor = "#FFC0CB", Status = "未配車あり",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運輸", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 5, AnkenCommitCount = 3, AnkenTempCount = 2,AnkenTempCountFlg = 1, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#FFFF00", Status = "未確定あり",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運送", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 3, AnkenCommitCount = 3, AnkenTempCount = 0, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#00BFFF", Status = "連絡可能",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運輸", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 5, AnkenCommitCount = 3, AnkenTempCount = 1,AnkenTempCountFlg = 1, AnkenNonCount = 1,AnkenNonCountFlg = 1,
                NumberCommLimitDateDisplay = null, RowColor = "#FFC0CB", Status = "未配車あり",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運送", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 4, AnkenCommitCount = 4, AnkenTempCount = 0, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#00BFFF", Status = "連絡可能",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇運輸", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 5, AnkenCommitCount = 3, AnkenTempCount = 2,AnkenTempCountFlg = 1, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#FFFF00", Status = "未確定あり",
            });
            SyabanRenrakuList.Add(new SyabanRenraku { KokyakuId = 1, KokyakuName = "〇〇〇鋼産", KokyakuPhoneNumber = "999-999-9999", 
                KokyakuTantouID = 1, KokyakuTantouName = "〇〇　太郎", AnkenCount = 1, AnkenCommitCount = 1, AnkenTempCount = 0, AnkenNonCount = 0,
                NumberCommLimitDateDisplay = null, RowColor = "#00BFFF", Status = "連絡可能",
            });
        }

    }
}
