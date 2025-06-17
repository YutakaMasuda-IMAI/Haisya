using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using WebApplication.Model;
using static HaisyaWeb.Models.AccidentListModel;

namespace HaisyaWeb.Dto
{
    /// <summary>
    /// 事故モデル
    /// </summary>
    public partial class AccidentModel_Local : AccidentModel<JikoItem_Local>
    {
        public int Login_User_ID { set; get; }
        public string Login_User_Name { set; get; }
        public int Company_ID { set; get; }
        public IEnumerable<SelectListItem> JikoKubunDto { set; get; }
        public IEnumerable<SelectListItem> WeatherKubunDto { set; get; }
        public IEnumerable<SelectListItem> JikoTypeKubunDto { set; get; }
        public IEnumerable<SelectListItem> ListKubunDto { set; get; }
        public IEnumerable<SelectListItem> WorkKubunDto { set; get; }
        public IEnumerable<SelectListItem> WorkFlowListDto { set; get; }
        public IEnumerable<SelectListItem> UserGroupDataDto { set; get; }
        public SearchModelForAccidentList Search { get; set; }
    }

    /// <summary>
    /// 事故アイテム
    /// </summary>
    public partial class JikoItem_Local : JikoItem { }

    /// <summary>
    /// JikoItemExtension
    /// </summary>
    public static class JikoItemExtension
    {
        /// <summary>
        /// 数値または元の値をフォーマットします。
        /// </summary>
        /// <param name="item">事故アイテム</param>
        /// <param name="format">フォーマット</param>
        /// <returns>フォーマットされた値</returns>
        public static string FormatNumberOrRawValue(this JikoItem_Local item, string format = null)
            => item == null || string.IsNullOrWhiteSpace(item.Jiko_Items_Val) ? ""
            : item.Jiko_Items_Type.Equals("int", StringComparison.OrdinalIgnoreCase) ? (int.TryParse(item.Jiko_Items_Val, out int v1) ? v1.ToString(format ?? "0,0") : item.Jiko_Items_Val)
            : item.Jiko_Items_Type.Equals("double", StringComparison.OrdinalIgnoreCase) ? (double.TryParse(item.Jiko_Items_Val, out double v2) ? v2.ToString(format ?? "N") : item.Jiko_Items_Val)
            : item.Jiko_Items_Type.Equals("money", StringComparison.OrdinalIgnoreCase) ? (decimal.TryParse(item.Jiko_Items_Val, out decimal v3) ? v3.ToString(format ?? "N") : item.Jiko_Items_Val)
            : item.Jiko_Items_Val;
    }

    public partial class ChangeStatus : WebApplication.Model.ChangeStatus
    {
    }

    public partial class WorkFlowList : WebApplication.Model.WorkFlowList
    {
        public int Jiko_ID { set; get; }
        public int Jiko_WorkFlow_Base_ID { set; get; }
    }

    public partial class PostAccident : WebApplication.Model.PostAccident
    {
    }

    public partial class RemandDataModel : WebApplication.Model.RemandDataModel
    {
        public int Jiko_ID { set; get; }
    }

    public partial class PostRemand : WebApplication.Model.PostRemand
    {
    }

    public class AccidentListItem_Local : WebApplication.Model.AccidentListModel.AccidentListItem
    {
    }
}
