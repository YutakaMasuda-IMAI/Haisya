using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 顧客ポータルの結合情報を表すクラス
    /// </summary>
    public class JoinCustomerPortalDto
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
        public T_Renkei_Anken_Check renkeiAnkenCheck { get; set; }
        public T_Renkei_Anken_Point renkeiAnkenPoint { get; set; }
    }
}
