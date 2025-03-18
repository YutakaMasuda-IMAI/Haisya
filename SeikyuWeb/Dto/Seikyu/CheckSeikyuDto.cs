using System.Collections.Generic;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求チェックのDTOクラス
    /// </summary>
    public class CheckSeikyuDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// チェックステータス
        /// </summary>
        public int checkStatus { get; set; }
        /// <summary>
        /// 請求月
        /// </summary>
        public string seikyuMonth { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int shimeDay { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int zeiKubun { get; set; }
        /// <summary>
        /// 顧客支店情報
        /// </summary>
        public CustomerBranchDto customerBranch { get; set; }
        /// <summary>
        /// チェック完了情報
        /// </summary>
        public CheckSeikyuDoneDto done { get; set; }
        /// <summary>
        /// 変更回数
        /// </summary>
        public int changeCount { get; set; }
        /// <summary>
        /// 案件数
        /// </summary>
        public int ankenCount { get; set; }
        /// <summary>
        /// 詳細数
        /// </summary>
        public int detailCount { get; set; }
        /// <summary>
        /// 変更前の請求運賃
        /// </summary>
        public decimal beforeSeikyuUnchin { get; set; }
        /// <summary>
        /// 変更前の立替金
        /// </summary>
        public decimal beforeTatekaekin { get; set; }
        /// <summary>
        /// 変更後の請求運賃
        /// </summary>
        public decimal afterSeikyuUnchin { get; set; }
        /// <summary>
        /// 変更後の立替金
        /// </summary>
        public decimal afterTatekaekin { get; set; }
        /// <summary>
        /// ステータス
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 請求詳細リスト
        /// </summary>
        public IEnumerable<CheckSeikyuDetailDto> details { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
