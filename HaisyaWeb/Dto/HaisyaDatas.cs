using System.Collections.Generic;
using WebApplication.Data;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 乗務員一覧
    /// </summary>
    public partial class V_DriverListDto : V_CompanyDriver_Local
    {
        //public List<Models.HaisyaModel.V_HaisyaDataListDto> V_Ankens { get; set; } = new List<Models.HaisyaModel.V_HaisyaDataListDto>();

        public List<Dto.V_HaisyaDataList_Local> V_Ankens { get; set; } = new();
        
        public string HolidayKubun { get; set; }
        public string Remark { get; set; }
        public string BuildingName_Abbr { get; set; }
        public double Leave_Days { get; set; } = 0;
        public string Restraint_Time { get; set; }
        public int countHaisya { get; set; } = 0;
        public int countHaisyaConfirm { get; set; } = 0;
        public bool EditFlg { get; set; } = false;
        public int? status { get; set; }
        public bool? IsReported { get; set; }
        /// <summary> 拘束時間 </summary>
        public double KousokuTime { get; set; }
        /// <summary> 連続勤務日 </summary>
        public int RenzokuWorkDays { get; set; }

        public int YosyaKubun { get; set; }

        public int? YosyaDriver_ID { get; set; }
        public int? YosyaDriverSyaryo_ID { get; set; }

        public Dto.V_HaisyaDataList_Local HaisyaUnderData { get; set; }

        /// <summary>
        /// クローンメソッド
        /// </summary>
        /// <returns>クローンされたV_DriverListDto</returns>
        public V_DriverListDto Clone() { return (V_DriverListDto)this.MemberwiseClone(); }
    }

    /// <summary>
    /// 配車
    /// </summary>
    public partial class T_Haisya_Local : T_Haisya
    {
        public int Index { get; set; }
    }

    /// <summary>
    /// 配車
    /// </summary>
    public partial class V_HaisyaDataList_Local : V_HaisyaDataList
    {
    }

    /// <summary>
    /// 配車
    /// </summary>
    public partial class T_Haisya_Around_Local : T_Haisya_Around
    {
    }

    public partial class SyabanRenrakuModel : WebApplication.Model.SyabanRenrakuModel
    {
    }

    public partial class SyabanRenrakuModel2 : WebApplication.Model.SyabanRenrakuModel2
    {
    }
    public partial class SyabanRenrakuModel3 : WebApplication.Model.SyabanRenrakuModel3
    {
    }
    public partial class SyabanRenrakuCustomModel
    {
        public SyabanRenrakuModel3 SyabanRenrakuModel3 { get; set; }
        public List<Dto.V_HaisyaDataList_Local> CarNumberLists { get; set; }
    }

    public partial class SyabanRenrakuPostModel : WebApplication.Model.SyabanRenrakuPostModel
    {
    }

    public partial class T_Haisya_SyabanRenraku_Local : T_Haisya_SyabanRenraku
    {
    }

    public partial class T_Haisya_SyabanRenraku_Detail_Local : T_Haisya_SyabanRenraku_Detail
    {
    }
    public partial class T_Haisya_Detail_Local : T_Haisya_Detail
    {
    }

    public partial class T_Haisya_Yosya_Local : T_Haisya_Yosya
    {
    }

    public partial class T_Haisya_Driver_Day_Remark_Local : T_Haisya_Driver_Day_Remark
    {
    }

    public partial class OperationInstructionsModel : WebApplication.Model.OperationInstructionsModel
    {
    }

    public partial class T_Print_Rireki_Local : T_Print_Rireki
    {
    }

    /// <summary>
    /// 配車
    /// </summary>
    public partial class ContactAbility
    {
       public bool status { get; set; }
       public string KokyakuName { get; set; }
    }

    /// <summary>
    /// 配車
    /// </summary>
    public partial class T_Haisya_Move_Param
    {
        public int Haisya_ID_From { get; set; }
        public int Driver_ID_From { get; set; }
        public int DriverSyaryo_ID_From { get; set; }
        public int Haisya_Kubun_From { get; set; }

        public int Driver_ID { get; set; }
        public int DriverSyaryo_ID { get; set; }
        public int Anken_ID { get; set; }
        public int AnkenDisplay_ID { get; set; } = 0;
        public int SyaryoManagement_ID { get; set; } = 0;
        public int SyaryoManagement_ID1 { get; set; } = 0;
    }
}
