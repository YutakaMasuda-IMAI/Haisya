using System.Collections.Generic;

namespace SeikyuWeb.Dto
{
    public class ApiResponseGroup
    {
        /// <summary>
        /// ステータスコード
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public Dictionary<string, object> message { get; set; }
    }
}
