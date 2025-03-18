//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Reflection;
//using WebApplication.Data;


//namespace HaisyaWeb.Models
//{
//    public class DailyReportHaisyaModel
//    {

//        public class DataListModel
//        {
//            public int Company_ID { get; set; }

//            /// <summary>
//            /// 検索項目
//            /// </summary>
//            public SearchModelForDailyReportList Search { get; set; }

//            /// <summary>  </summary>
//            public List<HaisyaDataList> DataDataLists { get; set; }

//            /// <summary>  </summary>
//            public int SyoriKubun { get; set; }

//            /// <summary>  </summary>
//            public int NumOfFilter { get; set; }

           

//            /// <summary>
//            /// V_HaisyaDataList
//            /// </summary>
//            public List<Dto.V_HaisyaDataList_Local> HaisyaDataList { set; get; }

//            /// <summary>
//            /// 選択されたV_HaisyaDataList
//            /// </summary>
//            public Dto.V_HaisyaDataList_Local HaisyaDataData { set; get; }

//            /// <summary>
//            /// 
//            /// </summary>
//            public List<DriveRouteListDisplay_Local> DriveRouteListData { set; get; }

//            /// <summary>
//            /// 
//            /// </summary>
//            public DriveRouteListDisplay_Local SelectedDriveRouteDisplay { set; get; }


//            public string ExecType { get; set; }

//            public string Eria { set; get; }
//            public string Ferry { get; set; }
//            public string Regulation { get; set; }
//            public string Twouturn { get; set; }
//            public string TsumiTaskTime { get; set; }
//            public string OroshiTaskTime { get; set; }
//            public double Height { get; set; }
//            public double Width { get; set; }
//            public double Weight { get; set; }
//            public double Nenpi { get; set; }
//            public int SyasyuID { get; set; }
//            public string SyasyuSize { get; set; }
//            public string Syasyu { get; set; }
//            public string Kata { get; set; }

//            /// <summary> 選択キー  </summary>
//            public int Anken_ID { get; set; }
//            public int AnkenDisplay_ID { get; set; }
//        }

//        public class GroupedData
//        {

//           public string KokyakuName { set; get; }
//           public List<HaisyaDataList>  HaisyaDataList { set; get; }

//        }


//        public class SearchModelForDailyReportList : CommonModel
//        {
//            public bool SingleSwitch { get; set; }

//            /// <summary>   </summary>
//            public string SelectTab { get; set; }

//            /// <summary>   </summary>
//            public int SelectGroup { get; set; }

//            /// <summary> 選択キー  </summary>
//            public int Anken_ID { get; set; }

//            /// <summary> 選択キー  </summary>
//            public int AnkenDisplay_ID { get; set; }

//            /// <summary> 乗務員CD  </summary>
//            public int DriverCd { get; set; }

//            /// <summary> 車番  </summary>
//            public int Syaban { get; set; }

//            /// <summary>   </summary>
//            public int SelectFilter { get; set; }

//            /// <summary>案件担当</summary>
//            [Display(Name = "請求担当")]
//            public int TantouID { get; set; }
//            public string TantouName { get; set; }

//            public string SelectTantou2 { get; set; }

//            public IEnumerable<SelectListItem> SelectSeikyuTantouList { set; get; }

//            [Required]
//            [Display(Name = "請求担当")]
//            public string SelectSeikyuTantou { get; set; }

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public class HaisyaDataList : Dto.V_HaisyaDataList_Local
//        {

//            /// <summary>
//            /// 
//            /// </summary>
//            public bool SetReciptFlg { set; get; }

//            public T_Nippou TNippou { set; get; }


//            public HaisyaDataList()
//            {

//            }

//            public HaisyaDataList(Dto.V_HaisyaDataList_Local list)
//            {
//                // 親クラスのプロパティ情報を一気に取得して使用する。
//                List<PropertyInfo> props = list
//                    .GetType()
//                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)?
//                    .ToList();

//                props.ForEach(prop =>
//                {
//                    var propValue = prop.GetValue(list);
//                    typeof(Dto.V_HaisyaDataList_Local).GetProperty(prop.Name).SetValue(this, propValue);
//                });
//            }

//        }

           
//    }
//}
