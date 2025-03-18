using RenkeiDB.Data;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 案件確保情報を表すDTO
    /// </summary>
    public class AnkenSecureDto
    {
        public T_Renkei_Anken_Secure renkeiAnkenSecure { get; set; }
        public T_Renkei_Anken_Secure_Check renkeiAnkenSecureCheck { get; set; }
        public T_Renkei_Anken_Secure_Print renkeiAnkenSecurePrint { get; set; }
    }
}
