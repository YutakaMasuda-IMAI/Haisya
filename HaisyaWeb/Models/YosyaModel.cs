using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    public class YosyaModel
    {
        public class YosyaDataListModel
        {
            /// <summary>
            /// 企業ID
            /// </summary>
            public int CompanyID { set; get; }

            /// <summary>
            /// モーダルの種類
            /// </summary>
            /// <remarks>
            /// From
            /// To
            /// </remarks>
            public string ModalType { set; get; }

            /// <summary>
            /// M_Customer_Branchリスト
            /// </summary>
            public List<Dto.M_Customer_Branch_Local> CustomerBranchList { get; set; }

            /// <summary>V_Customer_Syaryoリスト </summary>
            public List<Dto.M_Customer_Driver_Local> CustomerDriverList { set; get; }

            /// <summary>V_Customer_Syaryoリスト </summary>
            public List<Dto.M_Customer_Driver_Syaryo_Local> CustomerDriverSyaryoList { set; get; }

            /// <summary>V_Customer_Syaryoリスト </summary>
            public List<Dto.V_Customer_Syaryo_Local> CustomerSyaryoList { set; get; }

            public bool EditEnabled { set; get; }

            public List<Dto.M_Yosya_Local> YosyaDataList2 { get; set; }
        }

        public class YosyaModalDto
        {
            public Dto.M_Customer_Driver_Local CustomerDriverData { set; get; }

            public Dto.M_Customer_Driver_Syaryo_Local CustomerDriverSyaryoData { set; get; }

            public List<Dto.M_Customer_Tantou_Local> CustomerDataTantouList { set; get; }

            public List<Dto.M_Customer_TantouHaisyaGroup_Local> CustomerDataTantouHaisyaGroupList { set; get; }

            public IEnumerable<SelectListItem> CompanyUserGroupList { set; get; }

            public IEnumerable<SelectListItem> GroupList { get; set; }

            public int CustomerDriverSyaryoID { set; get; }

            public int CompanyID { set; get; }

            public bool EditEnabled { set; get; }

            public string Title { set; get; }

            public string TitleForDriver { set; get; }

            public string TitleForSyaryo { set; get; }

            public string BtnCaption { set; get; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class SearchModelForYosya : CommonModel
        {
            /// <summary>
            /// 
            /// </summary>
            public int CompanyID { set; get; }

            /// <summary>
            /// 
            /// </summary>
            public string SelectQuery { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string SelectTab { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectGroup { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int SelectFilter { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Display(Name = "車輛")]
            public int SelectSyaban { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Display(Name = "乗務員")]
            public string SelectDriverId { get; set; }


            /// <summary>
            /// 
            /// </summary>
            public int SelectSyoriKubun { get; set; }


            ///// <summary>
            ///// 
            ///// </summary>
            //public string SelectMonthDate { get; set; }

            //[Required]
            //[Display(Name = "乗務員区分")]
            //public string SelectDriverKubun { get; set; }

            ///// <summary>
            ///// 選択乗務員区分
            ///// </summary>
            //public IEnumerable<SelectListItem> DriverKubunList { set; get; }




        }

    }
}
