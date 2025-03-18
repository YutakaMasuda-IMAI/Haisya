using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払確認結合DTOクラス
    /// </summary>
    public class JoinCheckShitabaraiDto
    {
        /// <summary>
        /// 支払確認詳細
        /// </summary>
        public TCheckShitabaraiDetail detail { get; set; }

        /// <summary>
        /// 売上支払
        /// </summary>
        public TUriageShitabarai uriageShirabarai { get; set; }

        /// <summary>
        /// 売上
        /// </summary>
        public TUriage uriage { get; set; }

        /// <summary>
        /// 案件詳細
        /// </summary>
        public TAnkenDetail ankenDetail { get; set; }

        /// <summary>
        /// 車両
        /// </summary>
        public MSyaryo syaryo { get; set; }
    }
}
