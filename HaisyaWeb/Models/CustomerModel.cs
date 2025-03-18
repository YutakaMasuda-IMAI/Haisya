using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 顧客リストDTO
    /// </summary>
    public class CustomerListDto
    {
        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 顧客リストデータDTO
    /// </summary>
    public class CustomerListDataDto
    {
        public int CompanyID { set; get; }

        public List<Dto.M_Customer_Local> CustomerList { set; get; }
    }

    /// <summary>
    /// 顧客モーダルDTO
    /// </summary>
    public class CustomerModalDto
    {
        public int CompanyID { set; get; }

        public Dto.M_Customer_Local CustomerData { set; get; }

        public List<Dto.M_Customer_Branch_Local> CustomerBranchList { set; get; }

        public string Customer_Name_Oya { set; get; }

        public int CustomerID { set; get; }

        public bool YosyaFlg { set; get; }

        public string Title { set; get; }

        public string BtnCaption { set; get; }

        public bool EditEnabled { set; get; }
    }

    /// <summary>
    /// 顧客支店リストデータDTO
    /// </summary>
    public class CustomerBranchListDataDto
    {
        public int CompanyID { set; get; }

        public List<Dto.M_Customer_Branch_Local> CustomerBranchList { set; get; }

    }

    /// <summary>
    /// 顧客支店モーダルDTO
    /// </summary>
    public class CustomerBranchModalDto
    {
        public int CompanyID { set; get; }

        public int CustomerID { set; get; }

        public int CustomerBranchID { set; get; }

        public Dto.M_Customer_Branch_Local CustomerBranchData { set; get; }

        public string Seikyu_Customer_Branch_Name { set; get; }

        public string Shiharai_Customer_Branch_Name { set; get; }

        public Dto.M_Customer_Local CustomerData { set; get; }

        public List<Dto.M_Customer_Tantou_Local> CustomerTantouList { set; get; }

        /// <summary> 有料距離ごと府tンの設定リスト/// </summary>
        public List<Dto.M_Customer_TollSeikyuKubun_Local> CustomerTollSeikyuKubunList { set; get; }

        public List<Dto.M_Customer_TollSeikyuKubun_Local> CustomerTollSeikyuKubunList_Distance { set; get; }

        public List<Dto.M_Customer_TollSeikyuKubun_Local> CustomerTollSeikyuKubunList_Address { set; get; }

        public List<Dto.M_Customer_TollSeikyuKubun_Local> CustomerTollSeikyuKubunList_IC { set; get; }

        public Dto.M_Customer_Uriage_Calc_Local CustomerUriageCalcData { set; get; }

        public Dto.M_Customer_Shiharai_Calc_Local CustomerShiharaiCalcData { set; get; }

        public string CustomerBranch_Name_Oya { set; get; }

        public string Title { set; get; }

        public string BtnCaption { set; get; }

        public bool EditEnabled { set; get; }


        [DisplayName("顧客特記事項")]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayName("案件登録時特記事項")]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string AnkenRemarks { get; set; }

        [DisplayName("日報請求特記事項")]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string SeikyuRemarks { get; set; }

        [DisplayName("支払特記事項")]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string ShiharaiRemarks { get; set; }

        [DisplayName("受領書印刷フラグ")]
        public bool ReceiptOutputFlg { get; set; } = true;


        /// <summary> 負担区分選択リスト </summary>
        public IEnumerable<SelectListItem> FutanKubunSelectList { set; get; }

        // <summary> 負担タイプ選択リスト </summary>
        public IEnumerable<SelectListItem> FutanTypeSelectList { set; get; }

        /// <summary> 請求担当グループ選択リスト </summary>
        public IEnumerable<SelectListItem> CompanyUserGroupList { set; get; }

        /// <summary> 下払い担当グループ選択リスト </summary>
        public IEnumerable<SelectListItem> CompanyUserGroupListForShiharai { set; get; }

        /// <summary> 請求区分選択リスト </summary>
        public IEnumerable<SelectListItem> SeikyuKubunSelectList { set; get; }

        /// <summary> 請求日区分選択リスト </summary>
        public IEnumerable<SelectListItem> SeikyuDateKubunSelectList { set; get; }

        /// <summary> 消費税区分選択リスト </summary>
        public IEnumerable<SelectListItem> SeikyuTakKubunSelectList { set; get; }

        /// <summary> 回収サイト選択リスト </summary>
        public IEnumerable<SelectListItem> CollectionSightSelectList { set; get; }

        /// <summary> 請求名担当者表示フラグ選択リスト </summary>
        public IEnumerable<SelectListItem> ReportOutputNameFlgSelectList { set; get; }
        
    }

    /// <summary>
    /// 顧客トラックメートリストDTO
    /// </summary>
    public class CustomerTruckmeteListDto
    {
        public int CompanyID { set; get; }

        public int SelectKubun { set; get; }
    }

    /// <summary>
    /// 顧客トラックメートリストデータDTO
    /// </summary>
    public class CustomerTruckmeteListDataDto
    {
        public List<V_TokuisakiForNotConnect_Local> TokuisakiForNotConnectList { set; get; }

        public int CompanyID { set; get; }
    }

    /// <summary>
    /// 顧客トラックメートモーダルDTO
    /// </summary>
    public class CustomerTruckmeteModalDto
    {
        public int CompanyID { set; get; }

        public bool YosyaFlg { set; get; }

        public Dto.M_Customer_Local CustomerData { set; get; }

        public Dto.M_Customer_Branch_Local CustomerBranchData { set; get; }

        public List<Dto.M_Customer_Tantou_Local> CustomerTantouList { set; get; }

        public Dto.V_Tokuisaki_Local TokuisakiData { set; get; }

        public string Customer_Name_Oya { set; get; }

        public IEnumerable<SelectListItem> TaxFractionKubunSelectList { set; get; }

        /// <summary>請求担当選択リスト </summary>
        public IEnumerable<SelectListItem> SeikyuTantouSelectList { set; get; }

        /// <summary>支払担当選択リスト </summary>
        public IEnumerable<SelectListItem> ShiharaiTantouSelectList { set; get; }
    }

    /// <summary>
    /// 顧客担当DTO
    /// </summary>
    public class CustomerTantouDto
    {
        public int CompanyID { set; get; }

        public int CustomerID { set; get; }

        public int TantouID { set; get; }

        public Dto.M_Customer_Local CustomerData { set; get; }

        public List<Dto.M_Customer_Tantou_Local> CustomerTantouList { set; get; }

        public Dto.M_Customer_Tantou_Local CustomerTantouData { set; get; }

        public string Title { set; get; }

        public string BtnCaption { set; get; }
    }

    /// <summary>
    /// ベンダーモーダルDTO
    /// </summary>
    public class VenderModalDto
    {
        public int CompanyID { set; get; }

        public Dto.M_Vender_Local VenderData { set; get; }

        public int VenderID { set; get; }

        public string Title { set; get; }

        public string BtnCaption { set; get; }

        public bool EditEnabled { set; get; }
    }
}
