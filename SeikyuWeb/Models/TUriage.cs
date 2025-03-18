using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上情報を表します。
    /// </summary>
    public partial class TUriage
    {
        /// <summary>
        /// 売上ID
        /// </summary>
        public int UriageId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 登録ステータス
        /// </summary>
        public int RegStatus { get; set; }
        /// <summary>
        /// 支払登録ステータス
        /// </summary>
        public int RegStatusShitabarai { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplayId { get; set; }
        /// <summary>
        /// 登録区分
        /// </summary>
        public int RegKubun { get; set; }
        /// <summary>
        /// 配車日
        /// </summary>
        public DateTime HaisyaDate { get; set; }
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// 請求日区分
        /// </summary>
        public int SeikyuDateKubun { get; set; }
        /// <summary>
        /// 請求区分
        /// </summary>
        public int SeikyuKubun { get; set; }
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        /// <summary>
        /// 立替超過区分
        /// </summary>
        public int TatekaeOverKubun { get; set; }
        /// <summary>
        /// 立替超過理由
        /// </summary>
        public string TatekaeOverReason { get; set; }
        /// <summary>
        /// 支払超過区分
        /// </summary>
        public int ShiharaiOverKubun { get; set; }
        /// <summary>
        /// 支払超過理由
        /// </summary>
        public string ShiharaiOverReason { get; set; }
        /// <summary>
        /// 立替超過承認
        /// </summary>
        public int TatekaeOverApproval { get; set; }
        /// <summary>
        /// 支払超過承認
        /// </summary>
        public int ShiharaiOverApproval { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
        /// <summary>
        /// 直接売上日
        /// </summary>
        public DateTime? DirectUriageDate { get; set; }
        /// <summary>
        /// 直接顧客支店ID
        /// </summary>
        public int DirectCustomerBranchId { get; set; }
        /// <summary>
        /// 直接顧客担当ID
        /// </summary>
        public int DirectCustomerTantouId { get; set; }
        /// <summary>
        /// 直接売上区分
        /// </summary>
        public int DirectUriageKubun { get; set; }
        /// <summary>
        /// 直接売上部門
        /// </summary>
        public int DirectUriageBumon { get; set; }
        /// <summary>
        /// 直接請求グループID
        /// </summary>
        public int DirectSeikyuGroupId { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
