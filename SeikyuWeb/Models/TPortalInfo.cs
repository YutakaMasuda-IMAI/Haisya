using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ポータル情報を表します。
    /// </summary>
    public partial class TPortalInfo
    {
        /// <summary>
        /// ポータル情報ID
        /// </summary>
        public int PortalInfoId { get; set; }
        /// <summary>
        /// ポータル区分
        /// </summary>
        public int PortalKubun { get; set; }
        /// <summary>
        /// 重要区分
        /// </summary>
        public int CriticalKubun { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 詳細
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 期限日
        /// </summary>
        public DateTime? LimitDate { get; set; }
        /// <summary>
        /// 表示フラグ
        /// </summary>
        public int DisplayFlg { get; set; }
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
        /// コントローラー
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// アクション
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// パラメータ整数1
        /// </summary>
        public double? ParamInt1 { get; set; }
        /// <summary>
        /// パラメータ整数1の名前
        /// </summary>
        public string ParamInt1Name { get; set; }
        /// <summary>
        /// パラメータ整数2
        /// </summary>
        public double? ParamInt2 { get; set; }
        /// <summary>
        /// パラメータ整数2の名前
        /// </summary>
        public string ParamInt2Name { get; set; }
        /// <summary>
        /// パラメータ整数3
        /// </summary>
        public double? ParamInt3 { get; set; }
        /// <summary>
        /// パラメータ整数3の名前
        /// </summary>
        public string ParamInt3Name { get; set; }
        /// <summary>
        /// パラメータ文字列1
        /// </summary>
        public string ParamString1 { get; set; }
        /// <summary>
        /// パラメータ文字列1の名前
        /// </summary>
        public string ParamString1Name { get; set; }
        /// <summary>
        /// パラメータ文字列2
        /// </summary>
        public string ParamString2 { get; set; }
        /// <summary>
        /// パラメータ文字列2の名前
        /// </summary>
        public string ParamString2Name { get; set; }
        /// <summary>
        /// パラメータ文字列3
        /// </summary>
        public string ParamString3 { get; set; }
        /// <summary>
        /// パラメータ文字列3の名前
        /// </summary>
        public string ParamString3Name { get; set; }
        /// <summary>
        /// 印刷ID
        /// </summary>
        public int PrintId { get; set; }
    }
}
