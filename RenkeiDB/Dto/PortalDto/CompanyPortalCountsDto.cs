namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 会社ポータルのカウント情報を表すDTO
    /// </summary>
    public class CompanyPortalCountsDto
    {
        public int shareSyaryoCnt { get; set; }
        public int syaSyaryoKakuhoCnt {  get; set; }
        public int shareLuggageCnt {  get; set; }
        public int shareLuggageKakuhoCnt { get; set; }
        public int shareLuggageMitourokuCnt { get; set; }
        public int iraiAnkenCnt {  get; set; }
        public int iraiAnkenSyabanKakuteiCnt { get; set; }
        public int iraiAnkenSyabanMikakuteiCnt{ get; set; }
        public int juchuAnkenCnt { get; set; }
        public int juchuAnkenSyabanTourokuCnt { get; set; }
        public int juchuAnkenSyabanMitourokuCnt { get; set; }
    }
}
