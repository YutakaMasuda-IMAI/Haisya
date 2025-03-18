using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 共有荷物の結合情報を表すクラス
    /// </summary>
    public class JoinShareLuggageDto
    {
        public T_Share_Luggage shareLuggage { get; set; }
        public T_Share_Luggage_Detail shareLuggageDetail { get; set; }
        public T_Share_Luggage_Secure shareLuggageSecure { get; set; }
        public M_Company company { get; set; }
        public M_CompanyUser_Group companyUserGroup { get; set; }
        public M_CompanyBranch companyBranch { get; set; }
    }
}
