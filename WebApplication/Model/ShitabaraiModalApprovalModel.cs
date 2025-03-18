using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 下払い承認モデル
    /// </summary>
    public class ShitabaraiModalApprovalModel
    {
        /// <summary>
        /// 下払い詳細確認更新
        /// </summary>
        public T_Check_Shitabarai_Detail UpdateCheckShitabaraiDetail { get; set; }

        /// <summary>
        /// 下払い変更確認更新
        /// </summary>
        public T_Check_Shitabarai_Change UpdateCheckShitabaraiChange { get; set; }

        /// <summary>
        /// 下払い売上更新
        /// </summary>
        public T_Uriage_Shitabarai UpdateUriageShitabarai { get; set; }
        
        /// <summary>
        /// 区分確認
        /// </summary>
        public int Check_Kubun { get; set; }

        /// <summary>
        /// 下払い ID確認
        /// </summary>
        public int Check_Shitabarai_ID { get; set; }

        /// <summary>
        /// ユーザ ID
        /// </summary>
        public int User_ID { get; set; }

        /// <summary>
        /// 下払い売上 ID
        /// </summary>
        public int Uriage_Shiharai_ID { get; set; }

        /// <summary>
        /// 変更フラグ
        /// </summary>
        public int Change_Flg { get; set; }
    }
}