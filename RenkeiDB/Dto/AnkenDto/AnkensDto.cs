using RenkeiDB.Data;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件情報を表すDTO
    /// </summary>
    public class AnkensDto
    {
        public int id { get; set; }
        public string renkeiAnkenNo { get; set; }
        public int renkeiAnkenStatus { get; set; }
        public CompanyDto company { get; set; }
        public int renkeiAnkenKubun { get; set; }
        public AnkenDetailDto detail { get; set; }
    }

    /// <summary>
    /// 案件の結合情報を表すクラス
    /// </summary>
    public class JoinAnken
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public M_Company company { get; set; }
        public T_Renkei_Anken_Point renkeiAnkenPoint { get; set; }
        public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
        public T_Renkei_Anken_Luggage renkeiAnkenLuggage { get; set; }
        public T_Renkei_Anken_Equipment renkeiAnkenEquipment { get; set; }
        public M_Luggage luggage { get; set; }
        public M_Equipment equipment { get; set; }
        public M_Luggage_Group mLuggageGroup { get; set; }
        public M_Equipment_Group mEquipmentGroup { get; set; }
    }

    /// <summary>
    /// 案件のCSV結合情報を表すクラス
    /// </summary>
    public class JoinAnkenCsv
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
        public T_Renkei_Anken_Point renkeiAnkenPoint { get; set; }
        public T_Renkei_Anken_Secure renkeiAnkenSecure { get; set; }
    }
}
