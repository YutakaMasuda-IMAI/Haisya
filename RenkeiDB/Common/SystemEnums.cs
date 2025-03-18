namespace RenkeiDB.Common
{
    /// <summary>
    /// システム列挙型クラス
    /// </summary>
    public class SystemEnums
    {
        /// <summary>
        /// マスターコード列挙型
        /// </summary>
        public class MasterCode
        {
            /// <summary>
            /// コードID列挙型
            /// </summary>
            public enum CodeID
            {
                SYASYU = 13
            }
        }

        /// <summary>
        /// 比較タイプ列挙型
        /// </summary>
        public enum TypeCompare
        {
            GreaterOrEqual = 1,
            LessOrEqual = 2,
            Equal = 3,
        }

        /// <summary>
        /// 車両共有区分列挙型
        /// </summary>
        public enum SpShareSyaryoKubun
        {
            CREATE = 1,
            UPDATE_ORDER = 2,
            UPDATE_STATUS = 3,
            UPDATE_CANCEL_DATETIME = 4,
        }

        /// <summary>
        /// 通知タイプ列挙型
        /// </summary>
        public enum NotifyType
        {
            NOTIFY_WHEN_EACH_CONFIRMED = 1,
            NOTIFY_WHEN_ALL_CONFIRMED = 0,
        }

        /// <summary>
        /// 荷物共有区分列挙型
        /// </summary>
        public enum SpShareLuggageKubun
        {
            CREATE = 1,
            UPDATE_ORDER = 2,
            UPDATE_STATUS = 3,
            UPDATE_CANCEL_DATETIME = 4,
        }

        /// <summary>
        /// ゼロ埋め列挙型
        /// </summary>
        public enum ZeroUme
        {
            CREATE = 6,
            UPDATE_STATUS = 7,
            CREATE_RENKEI_ANKEN = 7,
        }

        /// <summary>
        /// 連携案件区分列挙型
        /// </summary>
        public enum RenkeiAnkenKubun
        {
            OTHER = 1,
        }
    }
}

