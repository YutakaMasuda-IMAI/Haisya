namespace RenkeiDB.Data
{
    /// <summary>
    /// 荷物情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class M_Luggage
    {
        public virtual M_Luggage_Group Luggage_Group { get; set; }
    }
}
