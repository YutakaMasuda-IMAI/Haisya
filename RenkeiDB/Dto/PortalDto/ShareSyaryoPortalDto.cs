using System.Web;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 共有車両ポータル情報を表すDTO
    /// </summary>
    public class ShareSyaryoPortalDto
    {
        public int id { get; set; }
        public string shareSyaryoNo { get; set; }
        public int shareSyaryoStatus { get; set; }
        public string syasyuDisplay { get; set; }
        public string emptyCarDay { get; set; }
        public string emptyAddress { get; set; }
        public string destAddress {  get; set; }
        public string companyId {  get; set; }
        public int remarksCnt { get; set; }
        public CompanyUserGroupDto companyUserGroup { get; set; }
        public CompanyBranchDto companyBranch { get; set; }
    }
}
