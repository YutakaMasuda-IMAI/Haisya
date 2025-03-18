using RenkeiDB.Data;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 会社支店情報を表すDTO
    /// </summary>
    public class CompanyBranchDto
    {
        public int id { get; set; }
        public string branchCode { get; set; }
        public string branchName { get; set; }
        public string branchNameAbbr { get; set; }

        public static CompanyBranchDto FromEntity(M_CompanyBranch entity)
        {
            return new CompanyBranchDto
            {
                id = entity.Branch_ID,
                branchCode = entity.Branch_Code,
                branchName = entity.Branch_Name,
                branchNameAbbr = entity.Branch_Name_Abbr
            };
        }
    }
}
