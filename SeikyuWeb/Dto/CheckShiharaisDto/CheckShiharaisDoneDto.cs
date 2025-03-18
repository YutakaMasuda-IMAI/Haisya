using SeikyuWeb.Models;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// 支払いチェック完了DTO
    /// </summary>
    public class CheckShiharaisDoneDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// チェック日時
        /// </summary>
        public string checkDatetime { get; set; }

        /// <summary>
        /// チェックユーザー
        /// </summary>
        public string checkUser { get; set; }

        /// <summary>
        /// チェック結果
        /// </summary>
        public int? checkReault { get; set; }

        /// <summary>
        /// 変更フラグ
        /// </summary>
        public bool? changeFlg { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからDTOを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>支払いチェック完了DTO</returns>
        public static CheckShiharaisDoneDto FromEntity(TCheckShitabaraiDone entity)
            => new()
            {
                checkDatetime = entity?.CheckDatetime.ToString(DateFormat.DATE_JP),
                checkUser = entity?.CustomerTantou?.TantouNameAbbr,
                checkReault = entity?.CheckReault,
                changeFlg = entity?.ChangeFlg != null ? entity.ChangeFlg == 1 : null,
            };
    }
}
