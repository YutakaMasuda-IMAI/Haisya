using HaisyaDesktop.API.WebApp;
using HaisyaDesktop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.ViewModel.Tool
{
    class SelectTantouViewModel : BaseViewModel
    {

        public string Selectkubun { set; get; }

        public ObservableCollection<M_CompanyBranch_Local> CompanyBranchList { get; set; }

        public ObservableCollection<M_CompanyUser_Local> CompanyUserList { get; set; }
        public List<M_CompanyUser_Local> CompanyUserListFull { get; set; }

        public readonly int CompanyID;

        public SelectTantouViewModel(string _selectkubun)
        {
            CompanyID = Context.ContextManager.Instance.User.CompanyID;

            CompanyBranchList = new();
            CompanyUserList = new();

            Selectkubun = _selectkubun;

            _ = ViewLoad();

        }


        public async Task ViewLoad()
        {

            MasterDataApi api = new();

            CompanyBranchList = new ObservableCollection<M_CompanyBranch_Local>(await api.GetCompanyBranchList(CompanyID));

            RaisePropertyChanged(nameof(CompanyBranchList));

            CompanyUserListFull = await api.GetCompanyUserList(CompanyID, Selectkubun);
            CompanyUserList = new ObservableCollection<M_CompanyUser_Local>(CompanyUserListFull);

            RaisePropertyChanged(nameof(CompanyUserList));
        }

        public void RefreshView(M_CompanyBranch_Local m_CompanyBranch)
        {
            ObservableCollection<M_CompanyUser_Local> list = CompanyUserList;

            List<M_CompanyUser_Local> dataList = CompanyUserListFull.Where(m => m.Branch_ID == m_CompanyBranch.Branch_ID).ToList();
            CompanyUserList = new();
            CompanyUserList = new ObservableCollection<M_CompanyUser_Local>(dataList);
            RaisePropertyChanged(nameof(CompanyUserList));
        }


    }
}
