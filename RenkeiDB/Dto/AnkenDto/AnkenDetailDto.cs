using System.Collections.Generic;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件詳細情報を表すDTO
    /// </summary>
    public class AnkenDetailDto
    {
        public int id { get; set; }
        public string workName { get; set; }
        public int syaryoId { get; set; }
        public string syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public int daisuu { get; set; }
        public string tsumiTaskTime { get; set; }
        public string oroshiTaskTime { get; set; }
        public string routeTypeDisplay { get; set; }
        public double? routeTotalDistance { get; set; }
        public decimal? routeGrossAmount { get; set; }
        public decimal? routeTotalToll { get; set; }
        public string routeTotalTime { get; set; }
        public decimal? routeStgFreight { get; set; }
        public int seikyuKubun { get; set; }
        public decimal? extraCharge { get; set; }
        public decimal? baseFee { get; set; }
        public decimal? toll { get; set; }
        public decimal? grossAmount { get; set; }
        public int tollKubun { get; set; }
        public decimal tollMoney { get; set; }
        public string tollRemarks { get; set; }
        public string luggageDisplay { get; set; }
        public string equipmentDisplay { get; set; }
        public string syabanrenrakuRemarks { get; set; }
        public bool rootEigyoshoModori { get; set; }
        public bool checkOroshiSpace { get; set; }
        public bool ednGoBackEigyosyo { get; set; }
        public int routeType { get; set; }
        public double luggageWeight { get; set; }
        public IEnumerable<AnkenLuggageDto> ankenLuggages { get; set; }
        public IEnumerable<AnkenEquipmentDto> ankenEquipments { get; set; }
        public IEnumerable<AnkenPointDto> ankenPoints { get; set; }
    }
}
