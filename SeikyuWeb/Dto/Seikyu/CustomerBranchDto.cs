using SeikyuWeb.Models;

﻿namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 顧客支店のDTOクラス
    /// </summary>
    public class CustomerBranchDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 売上計算情報
        /// </summary>
        public CustomerUriageCalcDto uriageCalc { get; set; }
        /// <summary>
        /// 請求担当情報
        /// </summary>
        public SeikyuTantouDto seikyuTantou { get; set; }
        /// <summary>
        /// 支払担当情報
        /// </summary>
        public ShiharaiTantouDto shiharaiTantou { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからCustomerBranchDtoを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>CustomerBranchDto</returns>
        public static CustomerBranchDto FromEntity(MCustomerBranch entity) => new()
        {
            id = entity.CustomerBranchId,
            uriageCalc = entity.CustomerUriageCalc != null ? CustomerUriageCalcDto.FromEntity(entity.CustomerUriageCalc) : null,
            seikyuTantou = entity.SeikuTantou != null && entity.SeikuTantou.DelFlg != true ? SeikyuTantouDto.FromEntity(entity.SeikuTantou) : null,
            shiharaiTantou = entity.ShiharaiTantou != null && entity.SeikuTantou.DelFlg != true ? ShiharaiTantouDto.FromEntity(entity.ShiharaiTantou) : null,
        };
    }
}
