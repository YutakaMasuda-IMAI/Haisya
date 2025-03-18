using System.Collections.Generic;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 支払バッチ登録モデル
    /// </summary>
    public class ShitabaraiBatchRegistrationModel
    {
        /// <summary>
        /// CheckSeikyuChange更新用リスト
        /// </summary>
        public List<T_Check_Shitabarai_Change> UpdateCheckShitabaraiChangeList { get; set; }

        /// <summary>
        /// 売上支払更新用リスト
        /// </summary>
        public List<T_Uriage_Shitabarai> UpdateUriageShitabaraiList { get; set; }

        /// <summary>
        /// チェック区分
        /// </summary>
        public int Check_Kubun { get; set; }

        /// <summary>
        /// チェック支払ID
        /// </summary>
        public int Check_Shitabarai_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public int User_ID { get; set; }
    }
}