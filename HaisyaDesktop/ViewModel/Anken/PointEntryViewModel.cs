using HaisyaDesktop.Models;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HaisyaDesktop.ViewModel.Anken
{
    class PointEntryViewModel : BaseViewModel
    {

        public ReactiveProperty<Dto.T_Point_Local> TPoint { get; set; }

        public ObservableCollection<Models.ComboBoxItem> PointComboBoxItemsEntrykubun { get; set; }

        public ReactiveProperty<int> SelectGroupId { get; set; }

        public ReactiveProperty<int> EntryKubun { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="t_Point"></param>
        public PointEntryViewModel(Dto.T_Point_Local t_Point_)
        {
            TPoint = new() { Value = t_Point_ };
            EntryKubun = new() { Value = 0 };
            SelectGroupId = new() { Value = 0 };
            //SetEntryKubunItems();
        }

        public async Task SetEntryKubunItems()
        {

            if (Context.ContextManager.Instance.companyUserList == null) { return; }
            //
            API.WebApp.MasterDataApi api = new();
            List<Dto.M_CompanyUser_Group_Local> list = await api.M_CompanyUserGroupList(GetUserData().UserId);

            PointComboBoxItemsEntrykubun = new();
            Models.ComboBoxItem target;
            //PointComboBoxItemsEntrykubun.Add(target);

            foreach (Dto.M_CompanyUser_Group_Local data in list)
            {
                target = new Models.ComboBoxItem(data.Group_ID, data.Group_Name);
                PointComboBoxItemsEntrykubun.Add(target);
            }
            RaisePropertyChanged(nameof(PointComboBoxItemsEntrykubun));

        }

        public async Task<bool> RegExec()
        {
            if (EntryKubun.Value == 0) { TPoint.Value.User_ID = GetUserData().UserId; }
            if (EntryKubun.Value == 1) { TPoint.Value.Group_ID = SelectGroupId.Value; }

            API.WebApp.AnkenDataApi api = new();
            var checkData = await api.T_PointData(TPoint.Value.BuildingZid, TPoint.Value.User_ID, TPoint.Value.Group_ID);
            if (checkData != null)
            {
                MessageBoxResult res = MessageBox.Show("対象のポイントは既に登録されています。上書き更新しますか？", "注意", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.No) { return false;  }
            }

            var result = await api.InsertUpdatePointData(TPoint.Value);
            if (result.ErrrMessage != null)
            {
                MessageBox.Show("登録エラー：" + result.ErrrMessage);
                return false;
            } else
            {
                return true;
            }
        }

    }
}
