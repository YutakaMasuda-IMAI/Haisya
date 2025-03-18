using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 会社ポータルの結合情報を表すクラス
    /// </summary>
    public class JoinCompanyPortalDto
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
        public T_Renkei_Anken_Check renkeiAnkenCheck { get; set; }
        public T_Renkei_Anken_Point renkeiAnkenPointE { get; set; }
        public T_Renkei_Anken_Point renkeiAnkenPointS { get; set; }
        public M_CompanyUser_Group companyUserGroup { get; set; }
        public M_Company company1 { get; set; }
        public M_Company company2 { get; set; }
        public M_Company company3 { get; set; }
        public T_Share_Syaryo_Secure shareSyaryoSecure { get; set; }
        public T_Share_Luggage_Secure shareLuggageSecure { get; set; }
        public T_Share_Luggage shareLuggage { get; set; }
        public T_Renkei_Anken_Secure renkeiAnkenSecure { get; set; }
    }
}
