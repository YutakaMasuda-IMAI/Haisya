using System.Collections.Generic;

namespace RenkeiDB.Common
{
    /// <summary>
    /// システム定数クラス
    /// </summary>
    public static class SystemConstants
    {
        /// <summary>
        /// 定数：メッセージ
        /// </summary>
        public static class Message
        {
            public const string DataNotFound = "データが存在しません。";
            public const string InternalServerError = "リクエストの処理中にエラーが発生しました。";
            public const string Unauthorized = "アクセスは許可されていません。";
            public const string Error = "エラー。";
            public const string RequiredField = "{0}がありません。";
            public const string InvalidUser = "ユーザーもしくはパスワードが存在しません。";
            public const string LockUser = "このアカウントはロックされています。";
            public const string OwnerFlagIncorrectly = "オーナーフラグの設定に誤りがあります。";
            public const string RequiredValue = "{0}の値は0または1でなければなりません。";
            public const string Logout = "ログアウトしました。";
            public const string FromDayGreaterThanToDay = "{1}には{0}以降の日付を指定して下さい。";
            public const string AllowValue = "{0}の設定に誤りがあります。";
            public const string InValidNumber = "{0}が数値ではありません。";
            public const string InValidDate = "{0}が日付ではありません。";
            public const string InValidString = "{0}が文字列ではありません。";
            public const string InValidParam = "{0}パラメーターが不正です。";
            public const string InValidEmail = "メールアドレスのフォーマットに誤りがあります。";
            public const string InvalidNumberLength = "{0}の桁数に誤りがあります。";
            public const string InvalidBoolean = "{0}の設定に誤りがあります。";
            public const string MissingParams = "{0}または{1}がありません。";
            public const string AnkenPointDuplicate = "ankenPointsのkubunが重複しています。";
            public const string MustSetNull = "{0} を null に設定する必要があります。";
        }

        /// <summary>
        /// 定数：デフォルト値
        /// </summary>
        public static class DefaultValue
        {
            public const string GuardCompany = "company";
            public const string GuardCustomer = "customer";
        }

        /// <summary>
        /// 定数：フラグ
        /// </summary>
        public static class Flag
        {
            public const int FALSE = 0;
            public const int TRUE = 1;
        }

        /// <summary>
        /// 定数：案件ステータス
        /// </summary>
        public static class RenkeiAnkenStatus
        {
            public const string Confirmed = "確定";
            public const string Temporarily = "暫定時";
            public const string Ordered = "受注済";
            public const string Others = "却下";
            public const string Cancel = "取消";
            public const string ChangeRequest = "変更依頼";
            public const string ChangeConfirmation = "変更確認";
        }

        /// <summary>
        /// 定数：配車ステータス
        /// </summary>
        public static class DispatchStatus
        {
            public const string DispatchInProgress = "配車中";
            public const string TemporaryVehicleAllocation = "仮配車";
            public const string ConfirmedDispatch = "確定配車";
        }

        /// <summary>
        /// 定数：案件ステータス（数値）
        /// </summary>
        public static class NumberAnkenStatus
        {
            public const int Confirmed = 0;
            public const int Temporarily = 1;
            public const int Cancel = 3;
            public const int Others = 5;
            public const int ChangeRequest = 7;
        }

        /// <summary>
        /// 定数：配車ステータス（数値）
        /// </summary>
        public static class NumberDispatchStatus
        {
            public const int DispatchInProgress = 1;
            public const int TemporaryVehicleAllocation = 1;
            public const int ConfirmedDispatch = 0;
        }

        /// <summary>
        /// 定数：車両ステータス
        /// </summary>
        public static class SyaryoStatus
        {
            public const int 車輌確保 = 1;
            public const int 確保取消 = 2;
            public const int 公開終了 = 3;
            public const int 公開再開 = 4;
            public const int 公開中 = 0;
            public const int 確保 = 1;
            public const int 取消 = 2;
            public const int 取消_RenkeiAnken = 3;
        }

        /// <summary>
        /// 定数：荷物ステータス
        /// </summary>
        public static class ShareLuggageStatus
        {
            public const int 公開中 = 0;
            public const int 確保 = 1;
        }

        /// <summary>
        /// 定数：プロシージャ
        /// </summary>
        public static class StoreProceduresName
        {
            public const string SP_T_Renkei_Anken_No = "SP_T_Renkei_Anken_No";
            public const string SP_T_Renkei_Anken = "SP_T_Renkei_Anken";
            public const string SP_T_Share_Luggage = "SP_T_Share_Luggage";
            public const string SP_T_Share_No = "SP_T_Share_No";
            public const string SP_T_Share_Syaryo = "SP_T_Share_Syaryo";
        }

        /// <summary>
        /// 0:確定,1:暫定,3:取消,7:変更依頼
        /// </summary>
        public static class AnkenStatus
        {
            public const int 確定 = 0;
            public const int 暫定 = 1;
            public const int 取消 = 3;
            public const int 変更依頼 = 7;
        }

        /// <summary>
        /// 定数：Create/Update時のデフォルト値
        /// </summary>
        public static class DefaultValueCreate
        {
            public const int Zero = 0;
            public const int One = 1;
            public const string SEKubun = "S";
            public const string TaskTime = "00:30";
            public const int Four = 4;
        }

        /// <summary>
        /// 定数：区分
        /// </summary>
        public static class Kubun
        {
            public const int One = 1;
            public const int Two = 2;
            public const int Four = 4;
            public const int Nine = 9;

            public static class Types
            {
                public const string TypeOne = "S";
                public const string TypeNine = "E";
            }
        }

        /// <summary>
        /// 定数：ルートタイプ
        /// </summary>
        public static class RouteType
        {
            public const int 推奨 = 1;
            public const int 一般道優先 = 2;
            public const int 道幅優先 = 3;
            public const int 距離優先 = 4;
            public const int 別ルート = 5;
        }

        /// <summary>
        /// 定数：CSVヘッダ
        /// </summary>
        public static class HeaderCsv
        {
            public const string EmptyCars = "車輛公開№,車種,車番,空車日,空車場所,会社名,乗務員名,電話番号,特記事項,ID";
            public const string JuchuAnken = "案件№,荷物公開№,車種,積日,積地,卸地,荷主,傭車先,車番,乗務員名";
            public const string Anken = "輸送日,状況,配車状況,車種,積み,卸し,積日時,荷物,車番";
        }

        /// <summary>
        /// 定数：CSVフィールド順序
        /// </summary>
        public static class FieldOrderCsv
        {
            /// <summary>
            /// 定数：空車車両一覧CSVフィールド
            /// </summary>
            public static readonly List<string> KeepEmptyCarFields = new List<string>
            {
                "shareSyaryoNo",
                "syasyuDisplay",
                "syaban",
                "emptyCarDay",
                "emptyAddress",
                "companyName",
                "driverName",
                "cellPhone",
                "remark",
                "id",
            };

            /// <summary>
            /// 定数：受注案件CSVフィールド
            /// </summary>
            public static readonly List<string> JuchuAnkenFields = new List<string>
            {
                "ankenNo",
                "shareLuggageNo",
                "syasyuDisplay",
                "tumiDatetime",
                "tumiAddress",
                "oroshiAddress",
                "kokyakuName",
                "vehicleRentalDestination",
                "syaban",
                "driverName",
            };

            /// <summary>
            /// CSVファイルにエクスポートする案件のフィールド
            /// </summary>
            public static readonly List<string> AnkenFields = new List<string>
            {
                "transportationDate",
                "status",
                "dispatchStatus",
                "syasyuDisplay",
                "tumi",
                "oroshi",
                "oroshiDatetime",
                "luggage",
                "syaban"
            };
        }

        /// <summary>
        /// 定数：通行料区分
        /// </summary>
        public static class TollKubun
        {
            public const string 全高 = "1";
            public const string 一部 = "2";
            public const string 金額 = "3";
            public const string 無し = "4";
            public const string 他 = "9";
            public static class Label
            {
                public const string 全高 = "全高";
                public const string 一部 = "一部";
                public const string 金額 = "金額";
                public const string 無し = "無し";
                public const string 他 = "他";
            }
        }

        /// <summary>
        /// 定数：案件変更履歴
        /// </summary>
        public static class NameHistoryChangeAnken
        {
            public const string HistoryNumber = "01_履歴番号";
            public const string InsertDateTime = "02_登録日時";
            public const string InsertUser = "03_登録者";
            public const string WorkName = "04_案件名";
            public const string SyasyuDisplay = "05_車種";
            public const string Daisuu = "06_台数";
            public const string WaitingTime = "07_荷待ち時間";
            public const string RouteTypeDisplay = "08_ルート";
            public const string RouteTotalTime = "09_所要時間";
            public const string RouteTotalDistance = "10_距離";
            public const string RouteTotalToll = "11_有料道路(1)";
            public const string RouteStdAllFreight = "12_標準運賃";
            public const string SeikyuKubun = "13_運賃区分";
            public const string BaseFee = "14_基本運賃";
            public const string ExtraCharge = "15_追加費用";
            public const string Toll = "16_有料道路(2)";
            public const string Discount = "17_値引額";
            public const string GrossAmount = "18_請求運賃";
            public const string TollKubun = "19_高速代";
            public const string TollMoney = "20_高速金額";
            public const string TollRemarks = "21_高速備考";
            public const string LuggageDisplay = "22_荷物情報";
            public const string EquipmentDisplay = "23_装備品情報";
            public const string SyabanrenrakuRemarks = "24_特記事項";
            public const string StartBuildingName = "25_積建物名";
            public const string StartAddress = "26_積住所";
            public const string StartPointDateTime = "27_積日時";
            public const string EndBuildingName = "28_卸建物名";
            public const string EndAddress = "29_卸住所";
            public const string EndPointDateTime = "30_卸日時";
        }

        /// <summary>
        /// 定数：案件変更履歴キー
        /// </summary>
        public static class KeyHistoryChangeAnken
        {
            public const string HistoryNumber = "Renkei_Anken_Order";
            public const string InsertDateTime = "Insert_Datetime";
            public const string WorkName = "Work_Name";
            public const string SyasyuDisplay = "SyasyuDisplay";
            public const string Daisuu = "Daisuu";
            public const string RouteTypeDisplay = "RouteTypeDisplay";
            public const string RouteTotalTime = "Route_TotalTime";
            public const string RouteTotalDistance = "Route_TotalDistance";
            public const string RouteTotalToll = "Route_Totaltoll";
            public const string RouteStdAllFreight = "Route_StdFreight";
            public const string SeikyuKubun = "SeikyuKubun";
            public const string BaseFee = "BaseFee";
            public const string ExtraCharge = "ExtraCharge";
            public const string Toll = "Toll";
            public const string Discount = "Discount";
            public const string GrossAmount = "GrossAmount";
            public const string TollKubun = "Toll_Kubun";
            public const string TollMoney = "Toll_Money";
            public const string TollRemarks = "Toll_Remarks";
            public const string LuggageDisplay = "LuggageDisplay";
            public const string EquipmentDisplay = "EquipmentDisplay";
            public const string SyabanrenrakuRemarks = "SyabanRenraku_Remarks";
        }

        /// <summary>
        /// 定数：請求区分
        /// </summary>
        public static class SeikyuKubun
        {
            public const string ItemName = "SeikyuKubun";
            public static class Value
            {
                public const string Temporary = "0";
            }
            public static class LabelValue
            {
                public const string Temporary = "確定運賃";
                public const string Final = "暫定運賃";
            }
        }

        /// <summary>
        /// 定数：フォーマット
        /// </summary>
        public static class Format
        {
            public const string Date = "yyyy/MM/dd";
            public const string Number = "#,0.#############################";
            public const string DateTime = "yyyy/MM/dd HH:mm:ss";
            public const string TimeHourMinute = "HH:mm";
        }
    }
}