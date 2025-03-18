using System.Collections.Generic;
using WebApplication.Data;
using WebApplication.Data.Kintai;

#nullable disable

namespace WebApplication.Model
{
    /// <summary>
    /// 日報登録詳細モデルクラス
    /// </summary>
    public class DailyReportRegistrationDetailModel
    {
        public T_Anken_Detail AnkenDetail { get; set; }
        public T_Anken_Display AnkenDisplay { get; set; }
        public List<T_Anken_Point> PointLists { get; set; }
        public T_Uriage Uriage { get; set; }
        public List<M_Customer_TollSeikyuKubun> TollSeikyuKubun { get; set; }
        public string SeikyuRemarks { get; set; }
        public string DisplayName { get; set; }
        public string SyabanNumber { get; set; }
        public List<T_KUDGIVT> DegitakoData { get; set; }
        public List<T_KUDGSIR> HighwayData { get; set; }
        public List<int> KUDGSIRIdList { get; set; }
        public T_Nippou_Stay Nippou_Stay { get; set; }
        public T_Nippou_Kaiso Nippou_Kaiso { get; set; }
        public List<T_Nippou_Stay_Degitako> Nippou_Stay_Degitako { get; set; }
        public T_Nippou Nippou { get; set; }
        public List<T_Nippou_Kaiso_Degitako> Nippou_Kaiso_Degitako { get; set; }
        public List<OtherPaidData> OtherPaidDataList { get; set; }
        public int NumOfFilter { get; set; } 
        public int Nippou_ID { get; set; } 
        public List<int> NippouStayDegitakoIdList { get; set; } 
        public List<int> NippouKaisoDegitakoIdList { get; set; } 
        public T_Nippou_Approval NippouApproval { set; get; }
        public string ExecType { get; set; }
        public List<CodeDataDto> CodeDataDto { get; set; }
        public List<T_Nippou_Toll_Other>  NippouTollOther { get; set; }
        public List<T_Nippou_Toll_Other>  InitialDisplayNippouTollOther { get; set; }
        public List<T_Nippou_Toll>  NippouToll { get; set; }
        public List<T_Nippou_Toll>  InitialDisplayNippouToll { get; set; }
        public int User_ID { get; set; } 
        public string SelectDay { get; set; }
    }

    /// <summary>
    /// その他支払データクラス
    /// </summary>
    public class OtherPaidData
    {
        public List<string> CodeData { get; set; }
        public string ItemName { get; set; }
        public string StartPlace { get; set; }
        public string EndPlace { get; set; }
        public decimal Fee { get; set; }
        public string BurdenType { get; set; }
    }

    /// <summary>
    /// 認証リクエストモデルクラス
    /// </summary>
    public class CertificationRequestModel
    {
        public T_Nippou_Approval NippouApprovalData { get; set; }
        public List<GroupUserDto> GroupUserList { get; set; } 
        public string GroupName { get; set; } 
        public string Comment { get; set; } 
        public int Approval_Group_ID { get; set; } 
        public int User_ID { get; set; } 
        public int Nippou_ID { get; set; } 
        public int Nippou_Approval_ID { get; set; } 
        public string FormattedLimitDate { get; set; } 
    }

    /// <summary>
    /// グループユーザーDTOクラス
    /// </summary>
    public class GroupUserDto
    {
        public int Group_ID { get; set; }
        public string Display_Name { get; set; }
    }

    /// <summary>
    /// コードデータDTOクラス
    /// </summary>
    public class CodeDataDto 
    {
        public string Code_Data { get; set; }
        public string Code_Name { get; set; }
    }
}