using RenkeiDB.Data;
using RenkeiDB.Dto.EquipmentDto;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 連携案件情報を表すDTO
    /// </summary>
    public class RenkeiAnkenDto
    {
        public int id { get; set; }
        public string renkeiAnkenNo { get; set; }
        public int renkeiAnkenStatus { get; set; }
        public RenkeiAnkenCompanyDto company { get; set; }
        public int renkeiAnkenKubun { get; set; }
        public RenkeiAnkenDetailDto detail { get; set; }
        
    }

    /// <summary>
    /// 連携案件の会社情報を表すDTO
    /// </summary>
    public class RenkeiAnkenCompanyDto
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string companyNameDisplay { get; set; }
        public int ownerFlg { get; set; }

    }

    /// <summary>
    /// 連携案件の詳細情報を表すDTO
    /// </summary>
    public class RenkeiAnkenDetailDto
    {
        public int id { get; set; }
        public string workName { get; set; }
        public int syaryoId { get; set; }
        public int syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public int daisuu { get; set; }
        public string tsumiTaskTime { get; set; }
        public string oroshiTaskTime { get; set; }
        public string routeTypeDisplay { get; set; }
        public double? routeTotalDistance { get; set; }
        public int? routeGrossAmount { get; set; }
        public int? routeTotalToll { get; set; }
        public string routeTotalTime { get; set; }
        public int? routeStgFreight { get; set; }
        public int seikyuKubun { get; set; }
        public int extraCharge { get; set; }
        public int baseFee { get; set; }
        public int toll { get; set; }
        public int grossAmount { get; set; }
        public int tollKubun { get; set; }
        public int tollMoney { get; set; }
        public string tollRemarks { get; set; }
        public string luggageDisplay { get; set; }
        public string equipmentDisplay { get; set; }
        public string syabanrenrakuRemarks { get; set; }
        public bool rootEigyoshoModori { get; set; }
        public bool checkOroshiSpace { get; set; }
        public bool ednGoBackEigyosyo { get; set; }
        public List<AnkenLuggageDto> ankenLuggages { get; set; }
        public List<AnkenEquipmentDto> ankenEquipments { get; set; }
        public List<AnkenPointDto> ankenPoints { get; set; }
        public int routeType { get; set; }
        public double luggageWeight { get; set; }
        public decimal discount { get; set; }
    }
}
