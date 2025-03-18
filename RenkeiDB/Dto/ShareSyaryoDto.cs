using RenkeiDB.Data;
using RenkeiDB.Dto.ValidateRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static RenkeiDB.Common.SystemConstants;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 共有車両情報を表すDTO
    /// </summary>
    public class ShareSyaryoDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public string emptyCarDay { get; set; }
        public string emptyPostCode { get; set; }
        public IEnumerable<string> emptyAddresses { get; set; } = new List<string>();
        public string destPostCode { get; set; }
        public IEnumerable<string> destAddresses { get; set; } = new List<string>();
        public string remarks { get; set; }
        public string syaban { get; set; }
        public int syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public string enquipmentDisplay { get; set; }
        public double syaryoWeight { get; set; }
        public double syaryoTotalWeight { get; set; }
        public string driverName { get; set; }
        public string cellPhone { get; set; }
        public CompanyBranchDto companyBranch { get; set; } = null;
        public string updateDatetime { get; set; }
        public string shareSyaryoNo { get; set; }
        public int shareSyaryoStatus { get; set; }
        public int shareSyaryoLatestOrder { get; set; }
        public string cancelDatetime { get; set; }
        public CompanyUserGroupDto companyUserGroup { get; set; } = null;
        public string tantouGroupName { get; set; } 
        public int tantouGroupId { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public static ShareSyaryoDto FromEntity(T_Share_Syaryo entity) => new()
        {
            id = entity.Share_Syaryo_ID,
            emptyCarDay = entity.Share_Syaryo_Detail.Empty_Car_Day.ToString("yyyy/MM/dd"),
            emptyPostCode = entity.Share_Syaryo_Detail.Empty_Post_code,
            emptyAddresses = new List<string> { entity.Share_Syaryo_Detail.Empty_Address, entity.Share_Syaryo_Detail.Empty_Address2, entity.Share_Syaryo_Detail.Empty_Address3 },
            destPostCode = entity.Share_Syaryo_Detail.Dest_Post_code,
            destAddresses = new List<string> { entity.Share_Syaryo_Detail.Dest_Address, entity.Share_Syaryo_Detail.Dest_Address2, entity.Share_Syaryo_Detail.Dest_Address3 },
            remarks = entity.Share_Syaryo_Detail.Remarks,
            syaban = entity.Share_Syaryo_Detail.Syaban,
            syasyu = entity.Share_Syaryo_Detail.Syasyu,
            syasyuDisplay = entity.Share_Syaryo_Detail.SyasyuDisplay,
            enquipmentDisplay = entity.Share_Syaryo_Detail.EquipmentDisplay,
            syaryoWeight = entity.Share_Syaryo_Detail.Syaryo_Weight,
            syaryoTotalWeight = entity.Share_Syaryo_Detail.Syaryo_Total_Weight,
            driverName = entity.Share_Syaryo_Detail.Driver_Name,
            cellPhone = entity.Share_Syaryo_Detail.Cell_Phone,
            companyBranch = entity.CompanyBranch != null ? CompanyBranchDto.FromEntity(entity.CompanyBranch) : null,
            updateDatetime = entity.Share_Syaryo_Detail.Update_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
            shareSyaryoNo = entity.Share_Syaryo_No,
            shareSyaryoStatus = entity.Share_Syaryo_Status,
            shareSyaryoLatestOrder = entity.Share_Syaryo_Latest_Order,
            cancelDatetime = entity.Cancel_Datetime?.ToString("yyyy/MM/dd HH:mm:ss"),
            companyUserGroup = entity.CompanyUserGroup != null ? CompanyUserGroupDto.FromEntity(entity.CompanyUserGroup) : null,
            tantouGroupName = entity.CompanyUserGroup?.Display_Name,
            tantouGroupId = entity.Tantou_Group_ID,
        };
    }

    /// <summary>
    /// 共有車両リクエスト情報を表すDTO
    /// </summary>
    public class ShareSyaryoListRequestDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "空車開始日")]
        public string emptyFromDate { get; set; }

        [FromDateGreaterThanToDate("emptyFromDate")]
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "空車終了日")]
        public string emptyToDate { get; set; }

        public string emptyAddress { get; set; }

        public string destAddress { get; set; }

        public string syasyu { get; set; }

        [Range(double.MinValue, double.MaxValue, ErrorMessage = Message.InValidNumber)]
        [Display(Name = "車両重量")]
        public string syaryoWeight { get; set; }

        [RequiredIfNotEmpty("syaryoWeight")]
        [EnumDataType(typeof(TypeCompare), ErrorMessage = Message.AllowValue)]
        [Display(Name = "車両重量タイプ")]
        public string syaryoWeightType { get; set; }

        [Range(double.MinValue, double.MaxValue, ErrorMessage = Message.InValidNumber)]
        [Display(Name = "総車両重量")]
        public string syaryoTotalWeight { get; set; }

        [RequiredIfNotEmpty("syaryoTotalWeight")]
        [EnumDataType(typeof(TypeCompare), ErrorMessage = Message.AllowValue)]
        [Display(Name = "総車両重量タイプ")]
        public string syaryoTotalWeightType { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
