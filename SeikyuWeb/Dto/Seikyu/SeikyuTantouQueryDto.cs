using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求担当クエリDTOクラス
    /// </summary>
    public class SeikyuTantouQueryDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public TCheckSeikyu checkSeikyu { get; set; }
        public MCustomerBranch mCustomerBranch { get; set; }
        public MCompanyUserGroup mCompanyUserGroup { get; set; }
        public MCompanyUserGroupUser mCompanyUserGroupUser { get; set; }
        public MCompanyUser mCompanyUser { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
