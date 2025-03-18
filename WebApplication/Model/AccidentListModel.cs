using System.Collections.Generic;
using WebApplication.Data;
using static WebApplication.Models.AccidentListModel;

namespace WebApplication.Models
{
    /// <summary>
    /// 事故リストモデルクラス
    /// </summary>
    public class AccidentListModel : AccidentListModel<AccidentListItem, M_Code_Datum>
    {
        /// <summary>
        /// 事故リストアイテムクラス
        /// </summary>
        public class AccidentListItem : T_Jiko
        {
            /// <summary>
            /// M_CompanyDriver．Driver_IDのDisplay_Name
            /// </summary>
            public string Display_Name { get; set; }

            /// <summary>
            /// M_SyaryoManagement．SyaryouManagement_IDのSyaban_Number
            /// </summary>
            public string Syaban_Number { get; set; }

            /// <summary>
            /// Jiko_Kubun　→M_Code：18で、M_Code_Data．Data_ID＝Jiko_KubunのCode_Nameを表示
            /// </summary>
            public string Code_Name { get; set; }
        }
    }

    /// <summary>
    /// 事故リストモデルのジェネリッククラス
    /// </summary>
    /// <typeparam name="JikoItemDataType">事故アイテムデータ型</typeparam>
    /// <typeparam name="AccidentItemDataType">事故アイテムデータ型</typeparam>
    public class AccidentListModel<JikoItemDataType, AccidentItemDataType>
        where JikoItemDataType : class
        where AccidentItemDataType : M_Code_Datum
    {
        /// <summary>
        /// 事故データリスト
        /// </summary>
        public List<JikoItemDataType> JikoDataLists { get; set; }

        /// <summary>
        /// 事故タイプリスト
        /// </summary>
        public List<AccidentItemDataType> AccidentTypes { get; set; }
    }
}
