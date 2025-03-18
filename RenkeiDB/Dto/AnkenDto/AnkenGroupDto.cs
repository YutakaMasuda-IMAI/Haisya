using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件グループ情報を表すDTO
    /// </summary>
    public class AnkenGroupDto
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public M_Company company { get; set; }
        public IEnumerable<T_Renkei_Anken_Point> renkeiAnkenPoint { get; set; }
        public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
        public IEnumerable<T_Renkei_Anken_Luggage> renkeiAnkenLuggage { get; set; }
        public IEnumerable<T_Renkei_Anken_Equipment> renkeiAnkenEquipment { get; set; }
        public IEnumerable<M_Luggage> mLuggage { get; set; }
        public IEnumerable<M_Equipment> mEquipment { get; set; }
        public IEnumerable<M_Luggage_Group> mLuggageGroups { get; set; }
        public IEnumerable<M_Equipment_Group> mEquipmentGroups { get; set; }
    }
}
