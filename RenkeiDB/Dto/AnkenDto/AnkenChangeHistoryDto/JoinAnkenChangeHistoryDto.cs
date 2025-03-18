using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.AnkenDto.AnkenChangeHistoryDto
{
    /// <summary>
    /// 案件変更履歴の結合情報を表すクラス
    /// </summary>
    public class JoinAnkenChangeHistoryDto
    {
        public T_Renkei_Anken renkeiAnken { get; set; }
        public List<JoinRenkeiAnkenDetailDto> renkeiAnkenDetails { get; set; }
        public List<T_Renkei_Anken_Point> renkeiAnkenPoints { get; set; }
    }
}
