using System;

#nullable disable

namespace WebApplication.Model
{
    /// <summary>
    /// 車番連絡モデル
    /// </summary>
    public class SyabanRenrakuModel
    {
        /// <summary>
        /// 得意先コード
        /// </summary>
        public string Customer_Code { get; set; }

        /// <summary>
        /// 得意先省略
        /// </summary>
        public string Customer_Name_Abbr { get; set; }

        /// <summary>
        /// 担当者省略
        /// </summary>
        public string Tantou_Name_Abbr { get; set; }

        /// <summary>
        /// メール１
        /// </summary>
        public string Mail_Address1 { get; set; }

        /// <summary>
        /// メール２
        /// </summary>
        public string Mail_Address2 { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// ファックス
        /// </summary>
        public string Fax1 { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 未配送
        /// </summary>
        public int Not_Dispatched { get; set; }

        /// <summary>
        /// 前提配送
        /// </summary>
        public int Temporary_Dispatch { get; set; }

        /// <summary>
        /// 確認配送
        /// </summary>
        public int Confirmed_Dispatch { get; set; }

        /// <summary>
        /// 案件 ID
        /// </summary>
        public int Anken_ID { get; set; }

        /// <summary>
        /// 得意先 ID
        /// </summary>
        public int Customer_ID { get; set; }

        /// <summary>
        /// 担当 ID
        /// </summary>
        public int Tantou_ID { get; set; }
        public int Group_ID { get; set; }
        public int? Haisya_ID { get; set; }
    }

    /// <summary>
    /// 車番連絡２
    /// </summary>
    public class SyabanRenrakuModel2
    {
        /// <summary>
        /// 印刷日
        /// </summary>
        public DateOnly PrintDate { get; set; }

        /// <summary>
        /// 担当 ID
        /// </summary>
        public int TantouId { get; set; }

        /// <summary>
        /// 得意先ブランチ ID
        /// </summary>
        public int CustomerBranchId { get; set; }

        /// <summary>
        /// メール１
        /// </summary>
        public string MailAddress1 { get; set; }

        /// <summary>
        /// メール２
        /// </summary>
        public string MailAddress2 { get; set; }

        /// <summary>
        /// 案件 ID
        /// </summary>
        public int AnkenId { get; set; }

        /// <summary>
        /// 連絡区分
        /// </summary>
        public int RenrakuKubun { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 備考一覧
        /// </summary>
        public string RemarksList { get; set; }

        /// <summary>
        /// グループ ID
        /// </summary>
        public int Group_ID { get; set; }

        /// <summary>
        /// DAY
        /// </summary>
        public string Day { get; set; }
        
    }

    /// <summary>
    /// 車番連絡 3rd
    /// </summary>
    public class SyabanRenrakuModel3
    {
        /// <summary>
        /// ターゲット日
        /// </summary>
        public string TargetDate { get; set; }

        /// <summary>
        /// 得意先コード
        /// </summary>
        public string Customer_Code { get; set; }

        /// <summary>
        /// 担当 ID
        /// </summary>
        public int TantouId { get; set; }

        /// <summary>
        /// 得意先 Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 得意先省略
        /// </summary>
        public string Customer_Name_Abbr { get; set; }

        /// <summary>
        /// 担当者省略
        /// </summary>
        public string Tantou_Name_Abbr { get; set; }

        /// <summary>
        /// メール１
        /// </summary>
        public string MailAddress1 { get; set; }

        /// <summary>
        /// メール２
        /// </summary>
        public string MailAddress2 { get; set; }

        /// <summary>
        /// 案件 ID
        /// </summary>
        public int AnkenId { get; set; }

        /// <summary>
        /// 連絡区分
        /// </summary>
        public int RenrakuKubun { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// グループ ID
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Haisya ID
        /// </summary>
        public int HaisyaID { get; set; }
    }
}

