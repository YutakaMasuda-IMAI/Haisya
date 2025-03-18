using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// ドライバーモデル
    /// </summary>
    public class DriverModel
    {
        #region 乗務員メニュー
        /// <summary>
        /// ドライバーメニューDTO
        /// </summary>
        public class DriverMenuDto
        {
            public int CompanyID { set; get; }

            public List<M_CompanyDriver_Local> CompanyDriverList { set; get; }

            public List<M_CompanyBranch_Local> M_CompanyBranchList { set; get; }

            public bool EditEnabled { set; get; }
        }

        #region M_CompanyDriver
        /// <summary>
        /// 会社ドライバーモーダルDTO
        /// </summary>
        public class CompanyDriverModalDto
        {
            public M_CompanyDriver_Local CompanyDriver { set; get; }

            public List<M_CompanyBranch_Local> M_CompanyBranchList { set; get; }

            public string EmployeeNumber { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }
        }
        #endregion M_CompanyDriver
        #endregion 乗務員メニュー


        #region 乗務員車輌設定メニュー
        /// <summary>
        /// 会社ドライバー車輌設定メニューDTO
        /// </summary>
        public class CompanyDriverSyaryoMenuDto
        {
            /// <summary>
            /// 検索項目
            /// </summary>
            public CommonModel Search { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectFilter { get; set; }

            public int CompanyID { set; get; }

            public List<M_CompanyDriver_Syaryo_Local> CompanyDriverSyaryoList { set; get; }

            public List<M_CompanyDriver_Local> CompanyDriverList { set; get; }

            public List<M_SyaryoManagement_Local> SyaryoManagementList { set; get; }

            public List<M_CompanyBranch_Local> CompanyBranchList { set; get; }

            public List<M_Syaryo_Local> SyaryoList { set; get; }

            public List<M_CompanyUser_Group_Local> CompanyUserGroupList { set; get; }

            public bool EditEnabled { set; get; }
        }

        #region M_CompanyDriverSyaryo
        /// <summary>
        /// 会社ドライバー車輌モーダルDTO
        /// </summary>
        public class CompanyDriverSyaryoModalDto
        {
            public List<M_CompanyDriver_Syaryo_Local> CompanyDriverSyaryoList { set; get; }

            public M_CompanyDriver_Local CompanyDriver { set; get; }

            public M_CompanyBranch_Local CompanyBranch { set; get; }

            public List<M_SyaryoManagement_Local> SyaryoManagementList { set; get; }

            public List<M_Syaryo_Local> SyaryoList { set; get; }

            public List<M_SyasyuKubun_Local> SyasyuKubunList { set; get; }

            public List<SelectListItem> SyaryoSelectList { get; set; }

            public List<SelectListItem> GrouSelectpList { get; set; }

            public List<SelectListItem> SyaryoKubunSelectList { get; set; }

            public int DriverID { set; get; }

            public int CompanyID { set; get; }

            public string Title { set; get; }

            public string BtnCaption { set; get; }

            public bool EditEnabled { set; get; }
        }
        #endregion M_CompanyDriverSyaryo
        #endregion 乗務員車輌設定メニュー
    }
}
