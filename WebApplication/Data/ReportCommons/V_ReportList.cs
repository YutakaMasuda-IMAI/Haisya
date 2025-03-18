using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace WebApplication.Data.ReportCommons
{
    /// <summary>
    /// V_ReportListクラスは共通帳票のモデル
    /// </summary>
    [Keyless]
    [Table("V_ReportList")]
    public partial class V_ReportList
    {
        /// <summary>
        /// 顧客ID1
        /// </summary>
        [JsonPropertyName("Customer_ID1")]
        public int Customer_ID1 { get; set; } 
        
        /// <summary>
        /// 顧客名
        /// </summary>
        [JsonPropertyName("Customer_Name")]
        public string Customer_Name { get; set; }

        /// <summary>
        /// 車種
        /// </summary>
        [JsonPropertyName("Car_Type")]
        public string Car_Type { get; set; }

        /// <summary>
        /// データ1
        /// </summary>
        [JsonPropertyName("Data1")]
        public string Data1 { get; set; }

        /// <summary>
        /// データ2
        /// </summary>
        [JsonPropertyName("Data2")]
        public string Data2 { get; set; }

        /// <summary>
        /// データ3
        /// </summary>
        [JsonPropertyName("Data3")]
        public string Data3 { get; set; }

        /// <summary>
        /// データ4
        /// </summary>
        [JsonPropertyName("Data4")]
        public string Data4 { get; set; }

        /// <summary>
        /// データ5
        /// </summary>
        [JsonPropertyName("Data5")]
        public string Data5 { get; set; }

        /// <summary>
        /// データ6
        /// </summary>
        [JsonPropertyName("Data6")]
        public string Data6 { get; set; }
        
        /// <summary>
        /// データ7
        /// </summary>
        [JsonPropertyName("Data7")]
        public string Data7 { get; set; }

        /// <summary>
        /// データ8
        /// </summary>
        [JsonPropertyName("Data8")]
        public string Data8 { get; set; }

        /// <summary>
        /// データ9
        /// </summary>
        [JsonPropertyName("Data9")]
        public string Data9 { get; set; }

        /// <summary>
        /// データ10
        /// </summary>
        [JsonPropertyName("Data10")]
        public string Data10 { get; set; }

        /// <summary>
        /// データ11
        /// </summary>
        [JsonPropertyName("Data11")]
        public string Data11 { get; set; }

        /// <summary>
        /// データ12
        /// </summary>
        [JsonPropertyName("Data12")]
        public string Data12 { get; set; }

        /// <summary>
        /// データ13
        /// </summary>
        [JsonPropertyName("Data13")]
        public string Data13 { get; set; }

        /// <summary>
        /// データ14
        /// </summary>
        [JsonPropertyName("Data14")]
        public string Data14 { get; set; }

        /// <summary>
        /// データ15
        /// </summary>
        [JsonPropertyName("Data15")]
        public string Data15 { get; set; }

        /// <summary>
        /// データ16
        /// </summary>
        [JsonPropertyName("Data16")]
        public string Data16 { get; set; }

        /// <summary>
        /// データ17
        /// </summary>
        [JsonPropertyName("Data17")]
        public string Data17 { get; set; }

        /// <summary>
        /// データ18
        /// </summary>
        [JsonPropertyName("Data18")]
        public string Data18 { get; set; }

        /// <summary>
        /// データ19
        /// </summary>
        [JsonPropertyName("Data19")]
        public string Data19 { get; set; }

        /// <summary>
        /// データ20
        /// </summary>
        [JsonPropertyName("Data20")]
        public string Data20 { get; set; }

        /// <summary>
        /// データ21
        /// </summary>
        [JsonPropertyName("Data21")]
        public string Data21 { get; set; }

        /// <summary>
        /// データ22
        /// </summary>
        [JsonPropertyName("Data22")]
        public string Data22 { get; set; }

        /// <summary>
        /// データ23
        /// </summary>
        [JsonPropertyName("Data23")]
        public string Data23 { get; set; }

        /// <summary>
        /// データ24
        /// </summary>
        [JsonPropertyName("Data24")]
        public string Data24 { get; set; }

        /// <summary>
        /// データ25
        /// </summary>
        [JsonPropertyName("Data25")]
        public string Data25 { get; set; }

        /// <summary>
        /// データ26
        /// </summary>
        [JsonPropertyName("Data26")]
        public string Data26 { get; set; }

        /// <summary>
        /// データ27
        /// </summary>
        [JsonPropertyName("Data27")]
        public string Data27 { get; set; }

        /// <summary>
        /// データ28
        /// </summary>
        [JsonPropertyName("Data28")]
        public string Data28 { get; set; }

        /// <summary>
        /// データ29
        /// </summary>
        [JsonPropertyName("Data29")]
        public string Data29 { get; set; }

        /// <summary>
        /// データ30
        /// </summary>
        [JsonPropertyName("Data30")]
        public string Data30 { get; set; }

        /// <summary>
        /// 通貨1
        /// </summary>
        [JsonPropertyName("Currency1")]
        public string Currency1 { get; set; }

        /// <summary>
        /// 通貨2
        /// </summary>
        [JsonPropertyName("Currency2")]
        public string Currency2 { get; set; }

        /// <summary>
        /// 通貨3
        /// </summary>
        [JsonPropertyName("Currency3")]
        public string Currency3 { get; set; }

        /// <summary>
        /// 通貨4
        /// </summary>
        [JsonPropertyName("Currency4")]
        public string Currency4 { get; set; }

        /// <summary>
        /// 通貨5
        /// </summary>
        [JsonPropertyName("Currency5")]
        public string Currency5 { get; set; }

        /// <summary>
        /// 通貨6
        /// </summary>
        [JsonPropertyName("Currency6")]
        public string Currency6 { get; set; }

        /// <summary>
        /// 通貨7
        /// </summary>
        [JsonPropertyName("Currency7")]
        public string Currency7 { get; set; }

        /// <summary>
        /// 通貨8
        /// </summary>
        [JsonPropertyName("Currency8")]
        public string Currency8 { get; set; }

        /// <summary>
        /// 通貨9
        /// </summary>
        [JsonPropertyName("Currency9")]
        public string Currency9 { get; set; }

        /// <summary>
        /// 通貨10
        /// </summary>
        [JsonPropertyName("Currency10")]
        public string Currency0 { get; set; }

        /// <summary>
        /// 整数1
        /// </summary>
        [JsonPropertyName("Int1")]
        public int Int1 { get; set; }

        /// <summary>
        /// 整数2
        /// </summary>
        [JsonPropertyName("Int2")]
        public int Int2 { get; set; }

        /// <summary>
        /// 整数3
        /// </summary>
        [JsonPropertyName("Int3")]
        public int Int3 { get; set; }

        /// <summary>
        /// 整数4
        /// </summary>
        [JsonPropertyName("Int4")]
        public int Int4 { get; set; }

        /// <summary>
        /// 整数5
        /// </summary>
        [JsonPropertyName("Int5")]
        public int Int5 { get; set; }

        /// <summary>
        /// 整数6
        /// </summary>
        [JsonPropertyName("Int6")]
        public int Int6 { get; set; }

        /// <summary>
        /// 整数7
        /// </summary>
        [JsonPropertyName("Int7")]
        public int Int7 { get; set; }

        /// <summary>
        /// 整数8
        /// </summary>
        [JsonPropertyName("Int8")]
        public int Int8 { get; set; }

        /// <summary>
        /// 整数9
        /// </summary>
        [JsonPropertyName("Int9")]
        public int Int9 { get; set; }

        /// <summary>
        /// 整数10
        /// </summary>
        [JsonPropertyName("Int10")]
        public int Int10 { get; set; }

        /// <summary>
        /// 整数11
        /// </summary>
        [JsonPropertyName("Int11")]
        public int Int11 { get; set; }

        /// <summary>
        /// 整数12
        /// </summary>
        [JsonPropertyName("Int12")]
        public int Int12 { get; set; }

        /// <summary>
        /// 整数13
        /// </summary>
        [JsonPropertyName("Int13")]
        public int Int13 { get; set; }

        /// <summary>
        /// 整数14
        /// </summary>
        [JsonPropertyName("Int14")]
        public int Int14 { get; set; }

        /// <summary>
        /// 整数15
        /// </summary>
        [JsonPropertyName("Int15")]
        public int Int15 { get; set; }

        /// <summary>
        /// 整数16
        /// </summary>
        [JsonPropertyName("Int16")]
        public int Int16 { get; set; }

        /// <summary>
        /// 整数17
        /// </summary>
        [JsonPropertyName("Int17")]
        public int Int17 { get; set; }

        /// <summary>
        /// 整数18
        /// </summary>
        [JsonPropertyName("Int18")]
        public int Int18 { get; set; }

        /// <summary>
        /// 整数19
        /// </summary>
        [JsonPropertyName("Int19")]
        public int Int19 { get; set; }

        /// <summary>
        /// 整数20
        /// </summary>
        [JsonPropertyName("Int20")]
        public int Int20 { get; set; }

        /// <summary>
        /// 小数1
        /// </summary>
        [JsonPropertyName("Float1")]
        public float Float1 { get; set; }

        /// <summary>
        /// 小数2
        /// </summary>
        [JsonPropertyName("Float2")]
        public float Float2 { get; set; }

        /// <summary>
        /// 小数3
        /// </summary>
        [JsonPropertyName("Float3")]
        public float Float3 { get; set; }

        /// <summary>
        /// 小数4
        /// </summary>
        [JsonPropertyName("Float4")]
        public float Float4 { get; set; }

        /// <summary>
        /// 小数5
        /// </summary>
        [JsonPropertyName("Float5")]
        public float Float5 { get; set; }

        /// <summary>
        /// 小数6
        /// </summary>
        [JsonPropertyName("Float6")]
        public float Float6 { get; set; }

        /// <summary>
        /// 小数7
        /// </summary>
        [JsonPropertyName("Float7")]
        public float Float7 { get; set; }

        /// <summary>
        /// 小数8
        /// </summary>
        [JsonPropertyName("Float8")]
        public float Float8 { get; set; }

        /// <summary>
        /// 小数9
        /// </summary>
        [JsonPropertyName("Float9")]
        public float Float9 { get; set; }

        /// <summary>
        /// 小数10
        /// </summary>
        [JsonPropertyName("Float10")]
        public float Float10 { get; set; }

        /// <summary>
        /// 小数11
        /// </summary>
        [JsonPropertyName("Float11")]
        public float Float11 { get; set; }

        /// <summary>
        /// 小数12
        /// </summary>
        [JsonPropertyName("Float12")]
        public float Float12 { get; set; }

        /// <summary>
        /// 小数13
        /// </summary>
        [JsonPropertyName("Float13")]
        public float Float13 { get; set; }

        /// <summary>
        /// 小数14
        /// </summary>
        [JsonPropertyName("Float14")]
        public float Float14 { get; set; }

        /// <summary>
        /// 小数15
        /// </summary>
        [JsonPropertyName("Float15")]
        public float Float15 { get; set; }

        /// <summary>
        /// 小数16
        /// </summary>
        [JsonPropertyName("Float16")]
        public float Float16 { get; set; }

        /// <summary>
        /// 小数17
        /// </summary>
        [JsonPropertyName("Float17")]
        public float Float17 { get; set; }

        /// <summary>
        /// 小数18
        /// </summary>
        [JsonPropertyName("Float18")]
        public float Float18 { get; set; }

        /// <summary>
        /// 小数19
        /// </summary>
        [JsonPropertyName("Float19")]
        public float Float19 { get; set; }

        /// <summary>
        /// 小数20
        /// </summary>
        [JsonPropertyName("Float20")]
        public float Float20 { get; set; }

        /// <summary>
        /// 日付1
        /// </summary>
        [JsonPropertyName("Date1")]
        public DateTime Date1 { get; set; }

        /// <summary>
        /// 日付2
        /// </summary>
        [JsonPropertyName("Date2")]
        public DateTime Date2 { get; set; }

        /// <summary>
        /// 日付3
        /// </summary>
        [JsonPropertyName("Date3")]
        public DateTime Date3 { get; set; }

        /// <summary>
        /// 日付4
        /// </summary>
        [JsonPropertyName("Date4")]
        public DateTime Date4 { get; set; }

        /// <summary>
        /// 日付5
        /// </summary>
        [JsonPropertyName("Date5")]
        public DateTime Date5 { get; set; }

        /// <summary>
        /// 日付6
        /// </summary>
        [JsonPropertyName("Date6")]
        public DateTime Date6 { get; set; }

        /// <summary>
        /// 日付7
        /// </summary>
        [JsonPropertyName("Date7")]
        public DateTime Date7 { get; set; }

        /// <summary>
        /// 日付8
        /// </summary>
        [JsonPropertyName("Date8")]
        public DateTime Date8 { get; set; }

        /// <summary>
        /// 日付9
        /// </summary>
        [JsonPropertyName("Date9")]
        public DateTime Date9 { get; set; }

        /// <summary>
        /// 日付10
        /// </summary>
        [JsonPropertyName("Date10")]
        public DateTime Date10 { get; set; }

    }
}
