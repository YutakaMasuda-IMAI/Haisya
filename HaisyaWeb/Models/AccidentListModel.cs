using HaisyaWeb.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 事故リストモデルを表します。
    /// </summary>
    public class AccidentListModel
    {
        /// <summary>
        /// データリストモデルを表します。
        /// </summary>
        public class DataListModel
        {
            /// <summary>
            /// 事故データリスト
            /// </summary>
            public List<AccidentListItem_Local> JikoDataLists { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForAccidentList Search { get; set; }

            /// <summary>
            /// ソート順
            /// </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 事故リストの検索モデルを表します。
        /// </summary>
        public class SearchModelForAccidentList 
        {
            /// <summary>
            /// 〜
            /// </summary>
            [Display(Name = "〜")]
            public string WaveDash { get; set; }

            /// <summary>
            /// 事故発生日
            /// </summary>
            [Display(Name = "事故発生日")]
            public string AccidentDate { get; set; }

            /// <summary>
            /// 事故名
            /// </summary>
            [Display(Name = "事故名")]
            public string AccidentName { get; set; }

            /// <summary>
            /// 乗務員名
            /// </summary>
            [Display(Name = "乗務員名")]
            public string DriverName { get; set; }

            /// <summary>
            /// 車番
            /// </summary>
            [Display(Name = "車番")]
            public string CarNumber { get; set; }

            public int CarInt { get; set; }

            /// <summary>
            /// 事故区分
            /// </summary>
            [Display(Name = "事故区分")]
            public string AccidentCategory { get; set; }

            public int AccidentReason { get; set; }

            public int WfRadioButtons { get; set; } 
            public string FromDate { get; set; } 
            public string ToDate { get; set; } 

            /// <summary>
            /// 事故タイプのリスト
            /// </summary>
            public List<M_Code_Data_Local> AccidentTypes { get; set; }

            public bool IsBack { get; set; }
        }
    }
}
