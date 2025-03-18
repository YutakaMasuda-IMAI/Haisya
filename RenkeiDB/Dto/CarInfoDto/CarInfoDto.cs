using RenkeiDB.Dto.ValidateRules;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.CarInfoDto
{
    /// <summary>
    /// 車両情報を表すDTO
    /// </summary>
    public class CarInfoDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        [CustomDateFormat("yyyy/MM/dd")]
        [Display(Name = "空車日")]
        public string emptyCarDay { get; set; }
        public string emptyPostCode { get; set; }
        public string[] emptyAddresses { get; set; }
        public string destPostCode { get; set; }
        public string[] destAddresses { get; set; }
        public string remarks { get; set; }
        public int syaban { get; set; }
        public string syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public string enquipmentDisplay { get; set; }
        public double syaryoWeight { get; set; }
        public double syaryoTotalWeight { get; set; }
        public string driverName { get; set; }
        public string cellPhone { get; set; }
        public CompanyBranchDto companyBranch { get; set; }
        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        public string updateDatetime { get; set; }
        public string shareSyaryoNo { get; set; }
        public int shareSyaryoStatus { get; set; }
        public int shareSyaryoLatestOrder { get; set; }
        [CustomDateFormat("yyyy/MM/dd HH:mm:ss")]
        public string cancelDatetime { get; set; }
        public CompanyUserGroupDto companyUserGroup { get; set; }
        public string tantouGroupName { get; set; }
        public int tantouGroupId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
