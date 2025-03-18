using RenkeiDB.Dto.EmptyCarDto;
using System.Collections;
using System.Collections.Generic;

namespace RenkeiDB.Dto.PortalDto
{
    /// <summary>
    /// 会社ポータル情報を表すDTO
    /// </summary>
    public class CompanyPortalsDto
    {
        public IEnumerable<ShareLuggagePortalDto> shareLuggages { get; set; }
        public IEnumerable<ShareSyaryoPortalDto> shareSyaryos { get; set; }
        public IEnumerable<ShareSyaryoKakuhoPortalDto> shareSyaryoKakuhos { get; set; }
        public IEnumerable<IraiAnkenDto> iraiAnkens { get; set; }
        public IEnumerable<JuchuAnkenDto> juchuAnkens { get; set; }
        public IEnumerable<KeepEmptyCarDto> keepEmptyCars { get; set; }
    }
}
