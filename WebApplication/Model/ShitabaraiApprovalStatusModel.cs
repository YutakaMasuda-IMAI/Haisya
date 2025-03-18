namespace WebApplication.Model
{
    /// <summary>
    /// 支払承認ステータスモデル
    /// </summary>
    public class ShitabaraiApprovalStatusModel
    {
        /// <summary>
        /// 支払チェックID
        /// </summary>
        public int Check_Shitabarai_ID { get; set; }

        /// <summary>
        /// 売上支払ID
        /// </summary>
        public int Uriage_Shiharai_ID { get; set; }

        /// <summary>
        /// チェック区分
        /// </summary>
        public int Check_Kubun { get; set; }
    }
}