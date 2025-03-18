using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求担当DTOクラス
    /// </summary>
    public class SeikyuTantouDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// グループ名
        /// </summary>
        public string groupName { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// ユーザーリスト
        /// </summary>
        public List<UserDto> users { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからSeikyuTantouDtoを生成します。
        /// </summary>
        /// <param name="entity">MCompanyUserGroupエンティティ</param>
        /// <returns>SeikyuTantouDto</returns>
        public static SeikyuTantouDto FromEntity(MCompanyUserGroup entity) => new()
        {
            id = entity.GroupId,
            groupName = entity.GroupName,
            displayName = entity.DisplayName,
            phone = entity.Phone,
            users = entity.CompanyUserGroupUsers?.Select(u => u.DelFlg != true && u.CompanyUser != null && u.CompanyUser.DelFlg != true ? UserDto.FromEntity(u.CompanyUser) : null).Where(u => u != null).ToList(),
        };
    }
}
