using RenkeiDB.Data;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 連携案件の結合情報を表すクラス
    /// </summary>
    public class JoinRenkeiAnken
    {
        public T_Renkei_Anken Anken { get; set; }
        public T_Renkei_Anken_Detail AnkenDetail { get; set; }
        public T_Renkei_Anken_Luggage AnkenLuggage { get; set; }
        public T_Renkei_Anken_Equipment AnkenEquipment { get; set; }
        public T_Renkei_Anken_Point AnkenPointS { get; set; }
        public T_Renkei_Anken_Point AnkenPointE { get; set; }
        public T_Renkei_Anken_Check AnkenCheck { get; set; }
        public T_Renkei_Anken_Secure AnkenSecure { get; set; }
        public T_Renkei_Anken_Secure_Check AnkenSecureCheck { get; set; }
        public M_Company Company { get; set; }
        public M_CompanyUser_Group CompanyUserGroup { get; set; }
    }
}
