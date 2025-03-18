using RenkeiDB.Data;
using System;

namespace RenkeiDB.Dto.LuggageDto
{
    /// <summary>
    /// 荷物共有の結合情報を表すクラス
    /// </summary>
    public class JoinShareLuggage
    {
        public T_Share_Luggage ShareLuggage { get; set; }
        public T_Share_Luggage_Detail ShareLuggageDetail { get; set; }
        public M_CompanyBranch CompanyBranch { get; set; }
        public M_CompanyUser_Group CompanyUserGroup { get; set; }
    }

    /// <summary>
    /// 荷物共有の結合情報を表すクラス（ステータス更新のため）
    /// </summary>
    public class JoinShareLuggage2 : JoinShareLuggage
    {
        public T_Share_Luggage_Secure ShareLuggageSecure { get; set; }
        public M_Company Company { get; set; }
        public T_Renkei_Anken RenkeiAnken { get; set; }
        public M_Syaryo Syaryo { get; set; }
    }

    /// <summary>
    /// 荷物共有情報を表すDTO
    /// </summary>
    public class ShareLuggageDto
    {
        public int id { get; set; }
        public string kokyakuName { get; set; }
        public int kokyakuPublicFlg { get; set; }
        public string primeContractor { get; set; }
        public decimal unchin { get; set; }
        public int tollKubun { get; set; }
        public decimal tollMoney { get; set; }
        public string tumiDatetime { get; set; }
        public int tumiTimeKubun { get; set; }
        public int tumiStatusKubun { get; set; }
        public string tumiPostCode { get; set; }
        public string tumiAddress { get; set; }
        public string oroshiDatetime { get; set; }
        public int oroshiTimeKubun { get; set; }
        public int oroshiStatusKubun { get; set; }
        public string oroshiPostCode { get; set; }
        public string oroshiAddress { get; set; }
        public double luggageWeight { get; set; }
        public int syasyu { get; set; }
        public string syasyuDisplay { get; set; }
        public string luggageDisplay { get; set; }
        public string remarks { get; set; }
        public string equipmentDisplay { get; set; }
        public string updateDatetime { get; set; }
        public CompanyBranchDto companyBranch { get; set; }
        public string shareLuggageNo { get; set; }
        public int shareLuggageStatus { get; set; }
        public int shareLuggageLatestOrder { get; set; }
        public string cancelDatetime { get; set; }
        public string tantouGroupName { get; set; }
        public int tantouGroupId { get; set; }
        public CompanyUserGroupDto companyUserGroup { get; set; } = null;
    }
}
