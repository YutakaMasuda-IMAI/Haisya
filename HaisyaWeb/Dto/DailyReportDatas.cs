using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HaisyaWeb.Dto
{
    public partial class T_Nippou_Anken_Local : WebApplication.Data.T_Nippou_Anken { }
    public partial class T_Nippou_Stay_Local : WebApplication.Data.T_Nippou_Stay { }
    public partial class T_Nippou_Kaiso_Local : WebApplication.Data.T_Nippou_Kaiso { }
    public partial class T_Nippou_Stay_Degitako_Local : WebApplication.Data.T_Nippou_Stay_Degitako { }
    public partial class T_Nippou_Approval_Local : WebApplication.Data.T_Nippou_Approval { }
    public partial class T_Nippou_Toll_Local : WebApplication.Data.T_Nippou_Toll 
    {
        // T_KUDGSIR.ID
        public int ID { get; set; }
        // T_KUDGSIR.読取NO
        public string 読取NO { get; set; }
    }
    public partial class T_Nippou_Kaiso_Degitako_Local : WebApplication.Data.T_Nippou_Kaiso_Degitako { }
    public partial class OtherPaidData_Local : WebApplication.Model.OtherPaidData { }

    public partial class CertificationRequestModel : WebApplication.Model.CertificationRequestModel
    {
        public IEnumerable<SelectListItem> GroupUserSelectList { set; get; }
    }

    /// <summary>
    /// 日報
    /// </summary>
    public partial class T_Nippou_Local : WebApplication.Data.T_Nippou
    {
        public T_Nippou_Local() { }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="list">コピー元の日報リスト</param>
        public T_Nippou_Local(T_Nippou_Local list)
        {
            List<PropertyInfo> props = list
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
                .ToList();

            props.ForEach(prop =>
            {
                object propValue = prop.GetValue(list);
                typeof(T_Nippou_Local).GetProperty(prop.Name).SetValue(this, propValue);
            });
        }
    }

    /// <summary>
    /// 日報拡張メソッド
    /// </summary>
    public static class T_Nippou_Local_Extension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Approval_status_label(this T_Nippou_Local n)
            => n == null ? "" : n.ApprovalStatus == 0 ? "未承認" : n.ApprovalStatus == 1 ? "承認" : n.ApprovalStatus == 2 ? "否認" : "";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Renkei_status_label(this T_Nippou_Local n)
            => n == null ? "" : n.RenkeiStatus == 0 ? "未連携" : n.RenkeiStatus == 1 ? "連携" : "";
    }
}
