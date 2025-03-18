using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// 支払いチェック詳細DTO
    /// </summary>
    public class CheckShiharaisDetailDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 支払いチェックID
        /// </summary>
        public int checkShitabaraiId { get; set; }

        /// <summary>
        /// 売上支払いID
        /// </summary>
        public int uriageShiharaiId { get; set; }

        /// <summary>
        /// 支払い変更情報
        /// </summary>
        public CheckShiharaisChangeDto change { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからDTOを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>支払いチェック詳細DTO</returns>
        public static CheckShiharaisDetailDto FromEntity(TCheckShitabaraiDetail entity)
            => new()
            {
                checkShitabaraiId = entity.CheckShitabaraiId,
                uriageShiharaiId = entity.UriageShiharaiId,
                change = entity.CheckShitabaraiChange != null ? CheckShiharaisChangeDto.FromEntity(entity.CheckShitabaraiChange) : null
            };
    }
}
