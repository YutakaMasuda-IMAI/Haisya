using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 共有車両の結合情報を表すクラス
    /// </summary>
    public class JoinShareSyaryoDto
    {
        public T_Share_Syaryo shareSyaryo { get; set; }
        public T_Share_Syaryo_Detail shareSyaryoDetail { get; set; }
        public T_Share_Syaryo_Secure shareSyaryoSecure { get; set; }
        public M_Company company { get; set; }
        public M_CompanyUser_Group companyUserGroup { get; set; }
        public M_CompanyBranch companyBranch { get; set; }
    }
}
