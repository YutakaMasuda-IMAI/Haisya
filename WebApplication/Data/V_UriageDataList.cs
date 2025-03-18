using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication.Data
{
    /// <summary>
    /// 売上データリストを表すクラス
    /// </summary>
    [Keyless]
    [Table("V_UriageDataList")]
    public partial class V_UriageDataList
    {
        /// <summary>
        /// 選択行
        /// </summary>
        public int? SelectRow { get; set; }

        /// <summary>
        /// 案件ID
        /// </summary>
        public int? Anken_ID { get; set; }

        /// <summary>
        /// 案件番号
        /// </summary>
        [StringLength(20)]
        public string Anken_No { get; set; }

        /// <summary>
        /// 案件ステータス
        /// </summary>
        public int? Anken_Status { get; set; }

        /// <summary>
        /// 案件最新オーダー
        /// </summary>
        public int? Anken_Latest_Order { get; set; }

        /// <summary>
        /// 案件区分
        /// </summary>
        public int? Anken_Kubun { get; set; }

        /// <summary>
        /// 専属ID
        /// </summary>
        public int? SenzokuID { get; set; }

        /// <summary>
        /// 専属名
        /// </summary>
        [StringLength(50)]
        public string Senzoku_Name { get; set; }

        /// <summary>
        /// 請求区分
        /// </summary>
        public int? Seikyu_Kubun { get; set; }

        /// <summary>
        /// 計算区分
        /// </summary>
        public int? Calc_Kubun { get; set; }

        /// <summary>
        /// 月額料金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Monthly_Fee { get; set; }

        /// <summary>
        /// 日額料金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Daily_Fee { get; set; }

        /// <summary>
        /// 公開グループID
        /// </summary>
        public int? PublishGroup_ID { get; set; }

        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool? Publish_Flg { get; set; }

        /// <summary>
        /// 公開開始日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Publish_FromDatetime { get; set; }

        /// <summary>
        /// 公開終了日時
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Publish_ToDatetime { get; set; }

        /// <summary>
        /// 車種表示2
        /// </summary>
        [StringLength(40)]
        public string SyasyuDisplay2 { get; set; }

        /// <summary>
        /// 車種台数表示
        /// </summary>
        [StringLength(56)]
        public string SyasyuDaisuDisplay { get; set; }

        /// <summary>
        /// 配車担当ID
        /// </summary>
        public int? Haisya_Tantou_ID { get; set; }

        /// <summary>
        /// 配車担当名
        /// </summary>
        [StringLength(50)]
        public string Haisya_Tantou_Name { get; set; }

        /// <summary>
        /// 営業名
        /// </summary>
        [StringLength(50)]
        public string Eigyo_Name { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(255)]
        public string Remarks { get; set; }

        /// <summary>
        /// 荷物
        /// </summary>
        [StringLength(100)]
        public string Luggage { get; set; }

        /// <summary>
        /// 設備
        /// </summary>
        [StringLength(100)]
        public string Equipment { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int? Customer_Branch_ID { get; set; }

        /// <summary>
        /// 顧客名略称
        /// </summary>
        [StringLength(40)]
        public string Customer_Name_Abbr { get; set; }

        /// <summary>
        /// 請求担当ID
        /// </summary>
        public int? SeikyuTantouID { get; set; }

        /// <summary>
        /// 請求担当名
        /// </summary>
        [StringLength(40)]
        public string SeikyuTantouName { get; set; }

        /// <summary>
        /// 案件ステータス表示
        /// </summary>
        [StringLength(14)]
        public string AnkenStatusDisplay { get; set; }

        /// <summary>
        /// 案件ステップ
        /// </summary>
        [StringLength(4)]
        public string AnkenStep { get; set; }

        /// <summary>
        /// 暫定金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? ZanteiAmount { get; set; }

        /// <summary>
        /// 確定金額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? KakuteiAmount { get; set; }

        /// <summary>
        /// 出発地表示3
        /// </summary>
        [StringLength(510)]
        public string StartAddressDisplay3 { get; set; }

        /// <summary>
        /// 到着地表示3
        /// </summary>
        [StringLength(510)]
        public string EndAddressDisplay3 { get; set; }

        /// <summary>
        /// 配車区分
        /// </summary>
        public int? Haisya_Kubun { get; set; }

        /// <summary>
        /// 車番
        /// </summary>
        [StringLength(10)]
        public string Syaban { get; set; }

        /// <summary>
        /// ドライバーコード
        /// </summary>
        public int? Driver_Code { get; set; }

        /// <summary>
        /// ドライバー名
        /// </summary>
        [StringLength(50)]
        public string Driver_Name { get; set; }

        /// <summary>
        /// 予社1車番
        /// </summary>
        [StringLength(10)]
        public string Yosya1_Syaban { get; set; }

        /// <summary>
        /// 予社1名
        /// </summary>
        [StringLength(50)]
        public string Yosya1_Name { get; set; }

        /// <summary>
        /// 予社1ドライバー名
        /// </summary>
        [StringLength(50)]
        public string Yosya1_Driver_Name { get; set; }

        /// <summary>
        /// 予社2車番
        /// </summary>
        [StringLength(10)]
        public string Yosya2_Syaban { get; set; }

        /// <summary>
        /// 予社2名
        /// </summary>
        [StringLength(50)]
        public string Yosya2_Name { get; set; }

        /// <summary>
        /// 予社2ドライバー名
        /// </summary>
        [StringLength(50)]
        public string Yosya2_Driver_Name { get; set; }

        /// <summary>
        /// 予社3車番
        /// </summary>
        [StringLength(10)]
        public string Yosya3_Syaban { get; set; }

        /// <summary>
        /// 予社3名
        /// </summary>
        [StringLength(50)]
        public string Yosya3_Name { get; set; }

        /// <summary>
        /// 予社3ドライバー名
        /// </summary>
        [StringLength(50)]
        public string Yosya3_Driver_Name { get; set; }

        /// <summary>
        /// 売上ID
        /// </summary>
        public int? Uriage_ID { get; set; }

        /// <summary>
        /// 日報ID
        /// </summary>
        public int? Nippou_ID { get; set; }

        /// <summary>
        /// 登録ステータス
        /// </summary>
        public int? Reg_Status { get; set; }

        /// <summary>
        /// 支払登録ステータス
        /// </summary>
        public int? Reg_Status_Shitabarai { get; set; }

        /// <summary>
        /// 請求運賃
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? SeikyuUnchin { get; set; }

        /// <summary>
        /// 立替金
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Tatekaekin { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int? Zei_Kubun { get; set; }

        /// <summary>
        /// 直接売上日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? Direct_Uriage_Date { get; set; }

        /// <summary>
        /// 直接顧客ID
        /// </summary>
        public int? Direct_Customer_ID { get; set; }

        /// <summary>
        /// 直接顧客名
        /// </summary>
        [StringLength(50)]
        public string Direct_Customer_Name { get; set; }

        /// <summary>
        /// 直接顧客担当ID
        /// </summary>
        public int? Direct_Customer_Tantou_ID { get; set; }

        /// <summary>
        /// 直接顧客担当名
        /// </summary>
        [StringLength(50)]
        public string Direct_Customer_TantouName { get; set; }

        /// <summary>
        /// 直接売上区分
        /// </summary>
        public int? Direct_Uriage_Kubun { get; set; }

        /// <summary>
        /// 直接売上区分名
        /// </summary>
        [StringLength(50)]
        public string Direct_Uriage_KubunName { get; set; }

        /// <summary>
        /// 直接売上部門
        /// </summary>
        public int? Direct_Uriage_Bumon { get; set; }

        /// <summary>
        /// 直接売上部門名
        /// </summary>
        [StringLength(50)]
        public string Direct_Uriage_BumonName { get; set; }

        /// <summary>
        /// 直接売上請求総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Direct_Uriage_SeikyuTotal { get; set; }

        /// <summary>
        /// 直接売上支払総額
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? Direct_Uriage_ShiharaiTotal { get; set; }
    }
}
