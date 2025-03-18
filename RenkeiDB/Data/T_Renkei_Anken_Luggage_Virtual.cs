namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件荷物情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class T_Renkei_Anken_Luggage
    {
        public virtual M_Luggage Luggage { get; set; }
    }
}
