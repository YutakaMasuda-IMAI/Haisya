using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 配車モデルクラス
    /// </summary>
    public class HaisyaModel
    {
        /// <summary>
        /// データリストモデルクラス
        /// </summary>
        public class DataListModel
        {
            public int spaceDay { get; set; } = 3;
            public int Company_ID { get; set; }
            public DateTime SelectDay { get; set; }

            /// <summary>
            /// 画面再描画間隔
            /// </summary>
            public int RefreshKubun { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForHaisya Search { get; set; }

            /// <summary>
            /// 案件一覧
            /// </summary>
            public List<Dto.V_HaisyaDataList_Local> HaisyaDataLists { get; set; }
            //public List<V_HaisyaDataListDto> HaisyaDataLists { get; set; }

            /// <summary>
            /// 案件一覧（過去案件）
            /// </summary>
            public List<Dto.V_HaisyaDataList_Local> HaisyaUnderDataLists { get; set; }

            /// <summary>
            /// 乗務員一覧
            /// </summary>
            public List<V_DriverListDto> DriverDataLists { get; set; }

            /// <summary>
            /// 配車案件前後移動スケジュール
            /// </summary>
            public List<T_Haisya_Around_Local> HaisyaAroundList { set; get; }

            /// <summary>
            /// 総労働時間のリスト（勤怠システムから）
            /// </summary>
            public List<V_TOTAL_WORKING_TIME_Local> KintaiTotalWorkingTimeLists { get; set; }

            /// <summary>
            /// 勤怠確定データリスト（勤怠システムから）
            /// </summary>
            public List<T_Kintai_Commit_Local> KintaiCommitLists { get; set; }

            /// <summary>
            /// 休暇マスタ
            /// </summary>
            public List<M_LEAVE_Local> LeaveList { get; set; }

            /// <summary>
            /// 処理区分
            /// </summary>
            public int SyoriKubun { get; set; }

            /// <summary>マップAPIのURL（JS用）</summary>
            public string MapsApiForJSUrl { get; set; }

            /// <summary>
            /// ログイン者が所属するユーザーグループリスト
            /// </summary>
            public List<Dto.M_CompanyUser_Group_Local> GroupUserList { get; set; }

            /// <summary>
            /// 権限フラグ
            /// </summary>
            public bool EditEnabled { set; get; }

            public List<SyabanRenrakuModel> SyabanRenrakuLists { get; set; }

            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 配車データの受け渡し用DTO
        /// </summary>
        public class HaisyaDataModelDto
        {
            public T_Haisya_Local Haisya { set; get; }

            public T_Haisya_Local Haisya_Del { set; get; }

            public T_Haisya_Detail_Local Haisya_Detail { set; get; }

            public List<T_Haisya_Yosya_Local> Haisya_Yosya { set; get; }
        }

        /// <summary>
        /// 傭車ドライバーの配車登録用Dto
        /// </summary>
        public class HaisyaYosyaDriverRegisterDto
        {
            public V_LoginUser_Local loginUser { set; get; }
            public int AnkenDisplayID { set; get; }

            public Dto.T_Haisya_Yosya_Local HaisyaYosya { set; get; }
        }

        /// <summary>
        /// 配車データリストDTO
        /// </summary>
        public class V_HaisyaDataListDto : V_HaisyaDataList_Local
        {
            /// <summary>
            /// 更新可能（権限）フラグ
            /// </summary>
            public bool EditFlg { set; get; } = true;

            /// <summary>
            /// 報告済みフラグ
            /// </summary>
            public bool IsReported { set; get; } = false;
        }

        /// <summary>
        /// 配車登録モデルクラス
        /// </summary>
        public class HaisyaRegisterModel
        {
            public MapApiSettings MapApiSettings { set; get; }

            public int Haisya_ID { get; set; }
            public DateTime Day { get; set; }
            public int Driver_ID { get; set; }
            public int DriverSyaryo_ID { get; set; }
            public int Anken_ID { get; set; }
            public String Remarks { get; set; }
        }

        /// <summary>
        /// 配車検索用共通DTO
        /// </summary>
        public class SearchModelForHaisya : CommonModel
        {
            /// <summary>
            /// 選択タブ
            /// </summary>
            public string SelectTab { get; set; }

            /// <summary>
            /// 選択グループ
            /// </summary>
            public int SelectGroup { get; set; }

            /// <summary>
            /// 選択ソート
            /// </summary>
            public int SelectSort { get; set; }

            /// <summary>
            /// 選択フィルター
            /// </summary>
            public int SelectFilter { get; set; }

            /// <summary>
            /// 選択車両番号
            /// </summary>
            [Display(Name = "車輛")]
            public int SelectSyaban { get; set; }

            /// <summary>
            /// 選択乗務員ID
            /// </summary>
            [Display(Name = "乗務員")]
            public string SelectDriverId { get; set; }

            /// <summary>
            /// 選択処理区分
            /// </summary>
            public int SelectSyoriKubun { get; set; }

            /// <summary>
            /// 選択顧客
            /// </summary>
            public string SelectCustomer { get; set; }

            /// <summary>
            /// 選択ステータス
            /// </summary>
            public int[] SelectStatus { get; set; }

            public int SortOrder { set; get; }

            /// <summary>
            /// 表示順
            /// </summary>
            [Display(Name = "表示順")]
            public List<SelectListItem> SortSelectList { set; get; }
        }

        /// <summary>
        /// 個別月次配車表
        /// </summary>
        public class MonthScheduleModelViewModel
        {
            public string DriverName { get; set; }
            public string DriverBranch { get; set; }
            public string RemainingMonthlyWorkingTime { get; set; }
            public string TotalWorkingTime { get; set; }
            public string TotalLaborTime { get; set; }
            public string TotalOvertime { get; set; }
            public string TotalPublicHolidayWorkingTime { get; set; }
            public string TotalLegalHolidayWorkingTime { get; set; }
            public string TotalLateNightWorkingTime { get; set; }
            public double? TotalTravelDistance { get; set; }

            public string AnkenData { get; set; }
            public string KyukaData { get; set; }
            public string NocrewData { get; set; }

            public DateTime Date { get; set; }
            public int DriverID { get; set; }
            public int EmployeeNumber { get; set; }
        }

        /// <summary>
        /// 勤怠モーダルDTO
        /// </summary>
        public class AttendanceModalDto
        {
            public Dto.T_Kintai_Commit_Local KintaiCommit { get; set; }
            public Dto.V_CompanyDriver_Local CompanyDriver { get; set; }
            public int LeaveKubun { get; set; }
            public int LeaveReason { get; set; }
            public string Remark { get; set; }
            public bool EditEnabled { get; set; }
            public bool EditEnabledForKintai { get; set; }
            public DateTime Date { get; set; }
            public int UpdateUserId { get; set; }
        }

        /// <summary>
        /// 勤怠モーダルビューDTO
        /// </summary>
        public class AttendanceModalViewModel : AttendanceModalDto
        {
            public IEnumerable<SelectListItem> LeaveKubunList { set; get; }
            public IEnumerable<SelectListItem> LeaveReasonList { set; get; }
        }
    }
}
