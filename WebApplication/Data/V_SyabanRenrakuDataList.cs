using Microsoft.EntityFrameworkCore;
using System;

#nullable enable

/// <summary>
/// 車番連絡データリストを表すクラス
/// </summary>
[Keyless]
public class V_SyabanRenrakuDataList
{
    /// <summary>
    /// 選択行
    /// </summary>
    public int? SelectRow { get; set; }

    /// <summary>
    /// 顧客支店ID
    /// </summary>
    public int? Customer_Branch_ID { get; set; }

    /// <summary>
    /// 担当ID
    /// </summary>
    public int? Tantou_ID { get; set; }

    /// <summary>
    /// 配車ID
    /// </summary>
    public int? Haisya_ID { get; set; }

    /// <summary>
    /// 配車名
    /// </summary>
    public string? Haisya_Name { get; set; }

    /// <summary>
    /// 顧客コード
    /// </summary>
    public string? Customer_Code { get; set; }

    /// <summary>
    /// 顧客名
    /// </summary>
    public string? Customer_Name { get; set; }

    /// <summary>
    /// 顧客略称
    /// </summary>
    public string? Customer_Name_Abbr { get; set; }

    /// <summary>
    /// 担当コード
    /// </summary>
    public string? Tantou_Code { get; set; }

    /// <summary>
    /// 担当名
    /// </summary>
    public string? Tantou_Name { get; set; }

    /// <summary>
    /// 担当略称
    /// </summary>
    public string? Tantou_Name_Abbr { get; set; }

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string? PostCode { get; set; }

    /// <summary>
    /// メールタイトル
    /// </summary>
    public string? Mail_Title { get; set; }

    /// <summary>
    /// メールアドレス1
    /// </summary>
    public string? Mail_Address1 { get; set; }

    /// <summary>
    /// メールアドレス2
    /// </summary>
    public string? Mail_Address2 { get; set; }

    /// <summary>
    /// 住所1
    /// </summary>
    public string? Address1 { get; set; }

    /// <summary>
    /// 住所2
    /// </summary>
    public string? Address2 { get; set; }

    /// <summary>
    /// 住所3
    /// </summary>
    public string? Address3 { get; set; }

    /// <summary>
    /// 電話番号1
    /// </summary>
    public string? Phone1 { get; set; }

    /// <summary>
    /// 電話番号2
    /// </summary>
    public string? Phone2 { get; set; }

    /// <summary>
    /// FAX番号1
    /// </summary>
    public string? Fax1 { get; set; }

    /// <summary>
    /// FAX番号2
    /// </summary>
    public string? Fax2 { get; set; }

    /// <summary>
    /// 配車なし
    /// </summary>
    public int Haisya_Non { get; set; }

    /// <summary>
    /// 配車一時
    /// </summary>
    public int Haisya_Temp { get; set; }

    /// <summary>
    /// 配車確定
    /// </summary>
    public int Haisya_Commit { get; set; }

    /// <summary>
    /// 案件ID
    /// </summary>
    public int? Anken_ID { get; set; }

    /// <summary>
    /// 印刷日
    /// </summary>
    public DateTime? PrintDate { get; set; }
}