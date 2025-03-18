using System;
using System.Collections.Generic;
using WebApplication.Data;

#nullable disable

namespace WebApplication.Model
{
    /// <summary>
    /// 事故モデル
    /// </summary>
    public class AccidentModel : AccidentModel<JikoItem> { }

    /// <summary>
    /// 事故モデル
    /// </summary>
    public class AccidentModel<JikoItem_Type>
        where JikoItem_Type : JikoItem
    {
        /// <summary>
        /// 事故区分
        /// </summary>
        public List<CodeDataDto> JikoKubun { get; set; }

        /// <summary>
        /// 天気区分
        /// </summary>
        public List<CodeDataDto> WeatherKubun { get; set; }

        /// <summary>
        /// 事故種別
        /// </summary>
        public List<CodeDataDto> JikoTypeKubun { get; set; }

        /// <summary>
        /// 一覧区分
        /// </summary>
        public List<CodeDataDto> ListKubun { get; set; }

        /// <summary>
        /// 仕事区分
        /// </summary>
        public List<CodeDataDto> WorkKubun { get; set; }

        /// <summary>
        /// 仕事フロー
        /// </summary>
        public List<CodeDataDto> WorkFlowList { get; set; }

        /// <summary>
        /// ユーザグループ
        /// </summary>
        public List<CodeDataDto> UserGroupData { get; set; }

        public string Driver_Display_Name { get; set; }

        public string SyaryoManagement_Number { get; set; }

        public string SyaryoManagement_Number1 { get; set; }

        /// <summary>
        /// 事故
        /// </summary>
        public T_Jiko Jiko { get; set; }

        /// <summary>
        /// 事故アイテム
        /// </summary>
        public List<JikoItem_Type> JikoItemList { get; set; }

        /// <summary>
        /// 事故種別
        /// </summary>
        public List<T_Jiko_Type> JikoTypeList { get; set; }

        /// <summary>
        /// 事故WF
        /// </summary>
        public List<WorkFlow> JikoWorkFlowList { get; set; }

        /// <summary>
        /// 無効であるか 
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 無効承認であるか
        /// </summary>
        public bool ApprovalIsDisabled { get; set; }

        /// <summary>
        /// 得意先データ
        /// </summary>
        public CustomerData CustomerData { get; set; }

        /// <summary>
        /// 処理ターゲットの[Jiko_WorkFlow_Status_ID]
        /// [Approval_User_ID]が0のデータ
        /// </summary>
        public int? JikoWorkFlowStatusID { get; set; } = null;
        
    }

    /// <summary>
    /// 仕事フロー
    /// </summary>
    public class WorkFlow
    {
        /// <summary>
        /// WFルート
        /// </summary>
        public T_Jiko_WorkFlow_Route WorkFlowRoute { get; set; }

        /// <summary>
        /// WFステータス
        /// </summary>
        public T_Jiko_WorkFlow_Status WorkFlowStatus { get; set; }

        /// <summary>
        /// WF名
        /// </summary>
        public string Display_Name { get; set; }

        /// <summary>
        /// WFフラグ
        /// </summary>
        public bool WorkFlowFlg { get; set; }
    }

    /// <summary>
    /// 得意先データ
    /// </summary>
    public class CustomerData
    {
        /// <summary>
        /// 得意先
        /// </summary>
        public string Customer_Name { get; set; }

        /// <summary>
        /// 得意先ID
        /// </summary>
        public int Customer_ID { get; set; }
    }

    /// <summary>
    /// changeステータス
    /// </summary>
    public class ChangeStatus
    {
        /// <summary>
        /// 事故ID
        /// </summary>
        public int Jiko_ID { get; set; }

        /// <summary>
        /// 事故ステータス
        /// </summary>
        public int Jiko_Status { get; set; }
    }

    /// <summary>
    /// 事故アイテム
    /// </summary>
    public class JikoItem
    {
        /// <summary>
        /// 事故アイテム名
        /// </summary>
        public string Jiko_Items_Prop_Name { get; set; }

        /// <summary>
        /// 事故アイテムID
        /// </summary>
        public int Jiko_Items_ID { get; set; }

        /// <summary>
        /// 事故ID  
        /// </summary>
        public int Jiko_ID { get; set; }

        /// <summary>
        /// 事故アイテム種別
        /// </summary>
        public string Jiko_Items_Type { get; set; }

        /// <summary>
        /// 事故アイテム値
        /// </summary>
        public string Jiko_Items_Val { get; set; }

    }

    /// <summary>
    /// WF一覧
    /// </summary>
    public class WorkFlowList
    {
        /// <summary>
        /// WFルート一覧
        /// </summary>
        public List<WorkFlowRoute> WorkFlowRouteList { get; set; }

        /// <summary>
        /// WFログ一覧
        /// </summary>
        public List<WorkFlowLog> WorkFlowLogList { get; set; }
    }

    /// <summary>
    /// WFルート
    /// </summary>
    public class WorkFlowRoute
    {
        /// <summary>
        /// ルート名
        /// </summary>
        public string Route_Name { get; set; }

        /// <summary>
        /// ルート表示名
        /// </summary>
        public string Display_Name { get; set; }
    }

    /// <summary>
    /// WFログ
    /// </summary>
    public class WorkFlowLog
    {
        /// <summary>
        /// 表示名
        /// </summary>
        public string Display_Name { get; set; }

        /// <summary>
        /// 承認名
        /// </summary>
        public string Approval_Name { get; set; }

        /// <summary>
        /// 承認日付
        /// </summary>
        public DateTime? Approval_Datetime { get; set; }

    }

    /// <summary>
    /// Post事故
    /// </summary>
    public class PostAccident
    {
        /// <summary>
        /// 事故アイテム一覧
        /// </summary>
        public List<JikoItem> JikoItemList { get; set; }

        /// <summary>
        /// 事故
        /// </summary>
        public T_Jiko Jiko { get; set; }

        /// <summary>
        /// 事故種別一覧
        /// </summary>
        public List<T_Jiko_Type> JikoTypeList { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        public int User_ID { get; set; }
    }

    /// <summary>
    /// 差し戻しデータ
    /// </summary>
    public class RemandDataModel
    {
        /// <summary>
        /// 差し戻し一覧モデル
        /// </summary>
        public List<RemandModel> RemandListModel { get; set; }

        /// <summary>
        /// 事故WFステータスID
        /// </summary>
        public int Jiko_WorkFlow_Status_ID { get; set; }
    }

    /// <summary>
    /// 差し戻しモデル
    /// </summary>
    public class RemandModel
    {
        /// <summary>
        /// ルート名
        /// </summary>
        public string Route_Name { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string Display_Name { get; set; }

        /// <summary>
        /// 事故WF ID
        /// </summary>
        public int Jiko_WorkFlow_ID { get; set; }
    }

    /// <summary>
    /// Post差し戻し
    /// </summary>
    public class PostRemand
    {
        /// <summary>
        /// 事故WF ID
        /// </summary>
        public int Jiko_WorkFlow_ID { get; set; }

        /// <summary>
        /// 事故WFステータスID
        /// </summary>
        public int Jiko_WorkFlow_Status_ID { get; set; }

        /// <summary>
        /// 拒否理由
        /// </summary>
        public string Reject_Reason { get; set; }
        
        /// <summary>
        /// ユーザID
        /// </summary>
        public int User_ID { get; set; }

        /// <summary>
        /// 事故ID
        /// </summary>
        public int Jiko_ID { get; set; }
    }
}