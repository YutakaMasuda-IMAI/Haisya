using RenkeiDB.Common;
using RenkeiDB.Data;
using System;
using System.ComponentModel;

namespace RenkeiDB.Dto.OperationInstructionDto
{
    /// <summary>
    /// 操作指示印刷情報を表すDTO
    /// </summary>
    public class OperationInstructionPrintDto
    {
        [DisplayName("依頼先")]
        public string KokyakuName { get; set; }

        [DisplayName("出力日")]
        public string ExportDate => DateTime.Now.JapaneseDate();

        public string SyasyuDisplay { get; set; }
        public int Daisuu { get; set; }
        [DisplayName("車種・台数")]
        public string SyasyuDaisuu => $"{SyasyuDisplay}×{Daisuu}";

        [DisplayName("貨物　荷姿\r\n重量　個数")]
        public string LuggageDisplay { get; set; }

        [DisplayName("装備品")]
        public string EquipmentDisplay { get; set; }

        [DisplayName("引取日時")]
        public AnkenPointDto PickupInfo { get; set; }

        [DisplayName("納品日時")]
        public AnkenPointDto DeliveryInfo { get; set; }

        [DisplayName("特記事項")]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 案件ポイント情報を表すDTO
    /// </summary>
    public class AnkenPointDto
    {
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public int? StatusKubun { get; set; }
        public string StatusKubunDisplay
            => StatusKubun == 1 ? "確定" : StatusKubun == 2 ? "暫定" : "";
        public string DateAndTime
            => $"{Date.JapaneseDate()} {Date.JapaneseDayOfWeek()} {Time} {StatusKubunDisplay}";

        public string BuildingName { get; set; }
        public string Post_code { get; set; }
        public string Address { get; set; }
        [DisplayName("場所")]
        public string FullAddress => $"{BuildingName} {Post_code} {Address}";

        public AnkenPointDto() { }
        public AnkenPointDto(T_Renkei_Anken_Point entity)
        {
            Date = entity.PointDate;
            Time = entity.PointTime;
            StatusKubun = entity.PointStatusKubun;
            BuildingName = entity.BuildingName;
            Post_code = entity.Post_code;
            Address = entity.Address;
        }
    }
}
