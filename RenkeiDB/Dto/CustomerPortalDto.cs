namespace RenkeiDB.Dto
{
    /// <summary>
    /// 顧客ポータルのカウント情報を表すDTO
    /// </summary>
    public class CustomerPortalCountsDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// 依頼中案件数
        /// </summary>
        public int requestCnt { get; set; }
        /// <summary>
        /// 取消案件数
        /// </summary>
        public int cancelCnt { get; set; }
        /// <summary>
        /// 未確定案件数
        /// </summary>
        public int orderCnt { get; set; }
        /// <summary>
        /// 配車確定車両数
        /// </summary>
        public int haisyaKakuteiCnt { get; set; }
        /// <summary>
        /// 配車中車両数
        /// </summary>
        public int haisyatyuCnt { get; set; }
        /// <summary>
        /// 輸送変更通知件数
        /// </summary>
        public int yusouhenkouRenrakuCnt { get; set; }
        /// <summary>
        /// 輸送変更確認件数
        /// </summary>
        public int yusouhenkouKakuninCnt { get; set; }
        /// <summary>
        /// 輸送取消依頼
        /// </summary>
        public int yusouCancelIraiCnt { get; set; }
        /// <summary>
        /// 輸送取消確認件数
        /// </summary>
        public int yusouCancelCnt { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
