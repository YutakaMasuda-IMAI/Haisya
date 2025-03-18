using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Service
{
    public enum TantouLists
    {
        ALL = 0,
        Tantou = 1,
        Eigyo = 2,
    }

    public class SearchCommonService
    {

        /// <summary>
        ///  案件担当者選択リストの返却
        /// </summary>
        /// <param name="flgAllSelect"></param>
        /// <returns></returns>
        public static ObservableCollection<Models.ComboBoxItem> GetTantouSelect(bool flgAllSelect = true)
        {
            if (Context.ContextManager.Instance.companyUserList == null) { return null; }
            //
            API.WebApp.MasterDataApi api = new();
            List<Dto.M_CompanyUser_Local> list =
                    Context.ContextManager.Instance.companyUserList.Where(m => m.Tantou_Flg == true).ToList();

            ObservableCollection<Models.ComboBoxItem>  TantouItems = new();
            Models.ComboBoxItem target;

            if (flgAllSelect)
            {
                target = new Models.ComboBoxItem(0, "全て");
                TantouItems.Add(target);
            } else
            {
                target = new Models.ComboBoxItem(0, "");
                TantouItems.Add(target);
            }
            

            foreach (Dto.M_CompanyUser_Local data in list)
            {
                target = new Models.ComboBoxItem(data.Tntou_ID, data.Last_Name);
                TantouItems.Add(target);
            }

            return TantouItems;
        }


        /// <summary>
        /// 車種選択リストの返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="flgAllSelect"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Models.ComboBoxItem>> GetSyasyuSelect(int CompanyId, bool flgAllSelect = true)
        {

            API.WebApp.MasterDataApi api = new();
            IEnumerable<Dto.M_Syaryo_Local> list = await api.GetSyaryoList(CompanyId);
            var querySyasyu = list.OrderBy(x => x.SYASYU).GroupBy(x => new { syasyu = x.SYASYU });

            ObservableCollection<Models.ComboBoxItem> SyasyuItems = new();
            Models.ComboBoxItem target;

            if (flgAllSelect)
            {
                target = new Models.ComboBoxItem(0, "全て");
                SyasyuItems.Add(target);
            }
            else
            {
                target = new Models.ComboBoxItem(0, "");
                SyasyuItems.Add(target);
            }

            foreach (var data in querySyasyu)
            {
                target = new Models.ComboBoxItem(1, data.Key.syasyu);
                SyasyuItems.Add(target);
            }

            return SyasyuItems;
        }

        /// <summary>
        /// 型選択リストの返却
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="flgAllSelect"></param>
        /// <returns></returns>
        private static async Task<ObservableCollection<Models.ComboBoxItem>> GetKataSelect(int CompanyId, bool flgAllSelect = true)
        {

            API.WebApp.MasterDataApi api = new();
            IEnumerable<Dto.M_Syaryo_Local> list = await api.GetSyaryoList(CompanyId);
            var queryKata = list.OrderBy(x => x.KATA).GroupBy(x => new { kata = x.KATA });

            ObservableCollection<Models.ComboBoxItem> KataItems = new();
            Models.ComboBoxItem target;

            if (flgAllSelect)
            {
                target = new Models.ComboBoxItem(0, "全て");
                KataItems.Add(target);
            }
            else
            {
                target = new Models.ComboBoxItem(0, "");
                KataItems.Add(target);
            }

            foreach (var data in queryKata)
            {
                target = new Models.ComboBoxItem(1, data.Key.kata);
                KataItems.Add(target);
            }

            return KataItems;
        }

    }




}
