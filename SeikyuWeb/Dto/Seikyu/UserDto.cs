using SeikyuWeb.Models;

﻿namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// ユーザーDTOクラス
    /// </summary>
    public class UserDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string displayName { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからUserDtoを生成します。
        /// </summary>
        /// <param name="entity">MCompanyUserエンティティ</param>
        /// <returns>UserDto</returns>
        public static UserDto FromEntity(MCompanyUser entity) => new()
        {
            id = entity.UserId,
            displayName = entity.DisplayName,
        };
    }
}
