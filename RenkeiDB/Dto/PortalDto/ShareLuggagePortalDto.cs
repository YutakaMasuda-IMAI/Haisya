using RenkeiDB.Data;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 共有荷物ポータル情報を表すDTO
    /// </summary>
    public class ShareLuggagePortalDto
    {
        public int id { get; set; }
        public string shareLuggageNo { get; set; }
        public string syasyuDisplay {  get; set; }
        public string tumiDatetime { get; set; }
        public string tumiAddress { get; set; }
        public string kokyakuName { get; set; }
        public string oroshiAddress { get; set; }
        public double luggageWeight { get; set; }
        public decimal unchin { get; set; }
        public CompanyUserGroupDto companyUserGroup { get; set; }
        public CompanyBranchDto companyBranch { get; set; }
    }
}
