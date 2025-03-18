using RenkeiDB.Data;

namespace RenkeiDB.Dto.CarInfoDto
{
    /// <summary>
    /// 車両情報の結合情報を表すクラス
    /// </summary>
    public class JoinCarInfoDto
    {
        public T_Share_Syaryo shareSyaryo { get; set; }
        public T_Share_Syaryo_Detail shareSyaryoDetail { get; set; }
    }
}
