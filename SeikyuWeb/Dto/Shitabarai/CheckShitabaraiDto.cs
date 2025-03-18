using System.Collections.Generic;

namespace SeikyuWeb.Dto.Shitabarai
{
    /// <summary>
    /// 支払確認DTOクラス
    /// </summary>
    public class CheckShitabaraiDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 確認ステータス
        /// </summary>
        public int checkStatus { get; set; }

        /// <summary>
        /// 支払月
        /// </summary>
        public string shiharaiMonth { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int shimeDay { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int zeiKubun { get; set; }

        /// <summary>
        /// 案件数
        /// </summary>
        public int ankenCount { get; set; }

        /// <summary>
        /// 変更数
        /// </summary>
        public int changeCount { get; set; }

        /// <summary>
        /// 詳細数
        /// </summary>
        public int detailCount { get; set; }

        /// <summary>
        /// 支払前運賃
        /// </summary>
        public decimal beforeShiharaiUnchin { get; set; }

        /// <summary>
        /// 支払前立替金
        /// </summary>
        public decimal beforeTatekaekin { get; set; }

        /// <summary>
        /// 支払後運賃
        /// </summary>
        public decimal afterShiharaiUnchin { get; set; }

        /// <summary>
        /// 支払後立替金
        /// </summary>
        public decimal afterTatekaekin { get; set; }

        /// <summary>
        /// 支払確認完了DTO
        /// </summary>
        public CheckShitabaraiDoneDto done { get; set; }

        /// <summary>
        /// 支払確認詳細リスト
        /// </summary>
        public IEnumerable<CheckShitabaraiDetailDto> details { get; set; }

        /// <summary>
        /// 顧客支店DTO
        /// </summary>
        public CustomerBranchShitabaraiDto customerBranch { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
