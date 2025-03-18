﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客支店を表すクラス
    /// </summary>
    public partial class MCustomerBranch
    {
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 親支店ID
        /// </summary>
        public int OyaBranchId { get; set; }
        
        /// <summary>
        /// 顧客支店コード
        /// </summary>
        public string CustomerBranchCode { get; set; }
        
        /// <summary>
        /// 顧客支店名
        /// </summary>
        public string CustomerBranchName { get; set; }
        
        /// <summary>
        /// 顧客支店名カナ
        /// </summary>
        public string CustomerBranchNameKana { get; set; }
        
        /// <summary>
        /// 顧客支店名略称
        /// </summary>
        public string CustomerBranchNameAbbr { get; set; }
        
        /// <summary>
        /// 顧客支店郵便番号
        /// </summary>
        public string CustomerBranchPost { get; set; }
        
        /// <summary>
        /// 顧客支店住所1
        /// </summary>
        public string CustomerBranchAddress1 { get; set; }
        
        /// <summary>
        /// 顧客支店住所2
        /// </summary>
        public string CustomerBranchAddress2 { get; set; }
        
        /// <summary>
        /// 顧客支店住所3
        /// </summary>
        public string CustomerBranchAddress3 { get; set; }
        
        /// <summary>
        /// 顧客支店電話番号1
        /// </summary>
        public string CustomerBranchPhone1 { get; set; }
        
        /// <summary>
        /// 顧客支店電話番号2
        /// </summary>
        public string CustomerBranchPhone2 { get; set; }
        
        /// <summary>
        /// 顧客支店FAX番号1
        /// </summary>
        public string CustomerBranchFax1 { get; set; }
        
        /// <summary>
        /// 顧客支店FAX番号2
        /// </summary>
        public string CustomerBranchFax2 { get; set; }
        
        /// <summary>
        /// メールタイトル
        /// </summary>
        public string MailTitle { get; set; }
        
        /// <summary>
        /// メールアドレス1
        /// </summary>
        public string MailAddress1 { get; set; }
        
        /// <summary>
        /// メールアドレス2
        /// </summary>
        public string MailAddress2 { get; set; }
        
        /// <summary>
        /// 案件備考
        /// </summary>
        public string AnkenRemarks { get; set; }
        
        /// <summary>
        /// 請求備考
        /// </summary>
        public string SeikyuRemarks { get; set; }
        
        /// <summary>
        /// 請求顧客ID
        /// </summary>
        public int SeikyuCustomerId { get; set; }
        
        /// <summary>
        /// 請求担当ID
        /// </summary>
        public int SeikyuTantouId { get; set; }
        
        /// <summary>
        /// 請求区分
        /// </summary>
        public int SeikyuKubun { get; set; }
        
        /// <summary>
        /// 請求日区分
        /// </summary>
        public int SeikyuDateKubun { get; set; }
        
        /// <summary>
        /// 通行料区分
        /// </summary>
        public int TollKubun { get; set; }
        
        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }
        
        /// <summary>
        /// 税端数区分
        /// </summary>
        public int TaxFractionKubun { get; set; }
        
        /// <summary>
        /// 税端数位置
        /// </summary>
        public double TaxFractionPosition { get; set; }
        
        /// <summary>
        /// 請求郵便番号
        /// </summary>
        public string SeikuyPostCode { get; set; }
        
        /// <summary>
        /// 請求住所1
        /// </summary>
        public string SeikyuAddress1 { get; set; }
        
        /// <summary>
        /// 請求住所2
        /// </summary>
        public string SeikyuAddress2 { get; set; }
        
        /// <summary>
        /// 支払担当ID
        /// </summary>
        public int ShiharaiTantouId { get; set; }
        
        /// <summary>
        /// 支払締め日
        /// </summary>
        public int ShiharaiShimeDay { get; set; }
        
        /// <summary>
        /// 支払備考
        /// </summary>
        public string ShiharaiRemarks { get; set; }
        
        /// <summary>
        /// 支払他社ID
        /// </summary>
        public int ShiharaiYosyaId { get; set; }
        
        /// <summary>
        /// レポート出力名フラグ
        /// </summary>
        public int ReportOutputNameFlg { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        
        /// <summary>
        /// 挿入ユーザー
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
        /// 顧客支店コードトラック得意先
        /// </summary>
        public int? CustomerBranchCodeTracTokuisaki { get; set; }
        
        /// <summary>
        /// 顧客支店コードトラック他社先
        /// </summary>
        public int? CustomerBranchCodeTracYosyasaki { get; set; }
    }
}