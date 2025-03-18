using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// ログインユーザー情報を表すクラス
    /// </summary>
    public partial class VLoginUser
    {
        /// <summary>
        /// ログインユーザーID
        /// </summary>
        public int LoginUserId { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ログインID
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ロックフラグ
        /// </summary>
        public bool LockFlg { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        /// <summary>
        /// 役割
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// 役割名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 社員番号
        /// </summary>
        public int? EmployeeNumber { get; set; }
        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TntouId { get; set; }
        /// <summary>
        /// 会社コード
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 会社名略称
        /// </summary>
        public string CompanyNameAbbr { get; set; }
        /// <summary>
        /// 支店コード
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// 支店名
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 支店名略称
        /// </summary>
        public string BranchNameAbbr { get; set; }
        /// <summary>
        /// エリアID
        /// </summary>
        public int? AreaId { get; set; }
        /// <summary>
        /// デフォルトエリア
        /// </summary>
        public string DefaultArea { get; set; }
        /// <summary>
        /// デフォルトサイズ
        /// </summary>
        public string DefaultSize { get; set; }
        /// <summary>
        /// デフォルト車種
        /// </summary>
        public string DefaultSyasyu { get; set; }
        /// <summary>
        /// デフォルト型
        /// </summary>
        public string DefaultKata { get; set; }
        /// <summary>
        /// デフォルトグループ
        /// </summary>
        public int DefaultGroup { get; set; }
        /// <summary>
        /// 車種表示
        /// </summary>
        public string SyasyuDisplay { get; set; }
        /// <summary>
        /// 型表示
        /// </summary>
        public string KataDisplay { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpDate { get; set; }
        /// <summary>
        /// 会社ユーザー削除フラグ
        /// </summary>
        public bool CompanyUserDelFlg { get; set; }
    }
}
