using RenkeiDB.Data;

namespace RenkeiDB.Dto.EmptyCarDto
{
    /// <summary>
    /// 空車の結合情報を表すクラス
    /// </summary>
    public class JoinEmptyCarDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public T_Share_Syaryo shareSyaryo { get; set; }
        public T_Share_Syaryo_Detail shareSyaryoDetail { get; set; }
        public T_Share_Syaryo_Secure shareSyaryoSecure { get; set; }
        public T_Renkei_Anken renkeiAnken { get; set; }
        public M_Company mCompany { get; set; }
        public M_Syaryo mSyaryo { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
