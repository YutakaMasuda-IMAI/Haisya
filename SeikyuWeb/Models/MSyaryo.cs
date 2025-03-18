using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MSyaryo クラスは、車両情報を管理します。
    /// </summary>
    public partial class MSyaryo
    {
        /// <summary>
        /// 車両ID
        /// </summary>
        public int SyaryoId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string Syasyu { get; set; }
        /// <summary>
        /// 型
        /// </summary>
        public string Kata { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 型表示
        /// </summary>
        public string KataDisplay { get; set; }
        /// <summary>
        /// 長さ
        /// </summary>
        public double? Long { get; set; }
        /// <summary>
        /// 幅
        /// </summary>
        public double? Width { get; set; }
        /// <summary>
        /// 高さ
        /// </summary>
        public double? Height { get; set; }
        /// <summary>
        /// 最大積載量
        /// </summary>
        public double? MaxLoadCapa { get; set; }
        /// <summary>
        /// 車両重量
        /// </summary>
        public double? CarWeight { get; set; }
        /// <summary>
        /// 車両総重量
        /// </summary>
        public double? CarGrossWeight { get; set; }
        /// <summary>
        /// 平均燃費
        /// </summary>
        public double? AvgFuelCosts { get; set; }
        /// <summary>
        /// サイズ
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 型ID
        /// </summary>
        public string KataId { get; set; }
        /// <summary>
        /// 車種区分ID
        /// </summary>
        public int? SyasyuKubunId { get; set; }
        /// <summary>
        /// 通行料タイプ
        /// </summary>
        public string TollType { get; set; }
        /// <summary>
        /// 規制タイプ
        /// </summary>
        public string RegulationType { get; set; }
        /// <summary>
        /// 車両詳細情報
        /// </summary>
        public string Cardetailinfo { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpDate { get; set; }
        /// <summary>
        /// 非表示フラグ
        /// </summary>
        public bool HiddenFlg { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
