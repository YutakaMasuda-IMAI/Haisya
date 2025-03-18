namespace SeikyuWeb.Common
{
    /// <summary>
    /// SystemEnums クラス
    /// </summary>
    public class SystemEnums
    {
        /// <summary>
        /// Enum：CheckKubun
        /// </summary>
        public enum CheckKubun
        {
            WEB = 1
        }

        /// <summary>
        /// Enum：CheckSeikyeKubun
        /// </summary>
        public enum CheckSeikyeKubun
        {
            確認済 = 1,
            未確認 = 2
        }

        /// <summary>
        /// Enum：SeikyuStatus
        /// </summary>
        public enum SeikyuStatus
        {
            全て = 1,
            未印刷 = 2,
            印刷済 = 3
        }

        /// <summary>
        /// Enum：ZeiKubun
        /// </summary>
        public enum ZeiKubun
        {
            課税 = 0,
            非課税 = 1,
            全て = 3
        }

        /// <summary>
        /// Enum：CheckShitabaraiKubun
        /// </summary>
        public enum CheckShitabaraiKubun
        {
            確認済 = 1,
            未確認 = 2
        }

        /// <summary>
        /// Enum：CheckShitabaraiStatus
        /// </summary>
        public enum CheckShitabaraiStatus
        {
            発行済み = 0,
            確認中 = 1,
            確認済 = 2
        }

        /// <summary>
        /// Enum: ReportType
        /// </summary>
        public enum ReportType
        {
            HAISYA_CHECK_LIST = 1,              // 配車チェックリスト
            UNKO_SHIJI_LIST = 2,                // 運行指示書
            HACCHUSHO = 3,                      // 発注書
            SYABAN_LIST = 4,                    // 車番連絡票（ストアド）
            URIAGE_LIST = 5,                    // 売上一覧表
            URIAGE_DETAIL_LIST = 6,             // 売上明細一覧表
            KOTSUHI_LIST = 7,                   // 交通費一覧表
            KOTSUHI_DETAIL_LIST = 8,            // 交通費明細表
            URIAGE_SHUKEI = 9,                  // 売上集計表
            DAILY_URIAGE_SHUKEI = 10,           // 日別売上集計表
            URIAGE_NIKKI = 11,                  // 売上日計表
            TOKUISAKI_URIAGE_SUII = 12,         // 得意先別売上月別推移表
            SEIKYU_MIKAKUTEI_LIST = 13,         // 請求未確定一覧表
            SHIIRE_SHIHARAISAKI = 14,           // 仕入れ・支払先元帳
            SHITABARAI_LIST = 15,               // 下払い一覧表
            SHITABARAI_DETAIL_LIST = 16,        // 下払い明細表
            SHIHARAISAKI_LIST = 17,             // 支払先一覧
            SHITABARAI_MIKAKUTEI_LIST = 18,     // 下払い未確定一覧表
            NYUKIN_LIST = 19,                   // 入金一覧表
            SEIKYU_LIST = 20,                   // 請求一覧表
            SEIKYU_DETAIL_LIST = 21,            // 請求明細一覧表
            KAIKEI_SYSTEM_RENKEI_DATA = 22,     // 会計システム連携データ出力
            RENKEI_SETTEI_DATA = 23,            // 連携項目設定データ出力
            JYOMUIN_DATA = 24,                  // 乗務員データ
            JYOMUIN_OUTPUT_DATA = 25,           // 乗務員データ出力項目
            JIKO_DETAIL_LIST = 26,              // 事故明細
            SEIKYU_TOIAWASE_LIST = 27,          // 請求問合せ一覧
            SEIKYUSHO = 28,                     // 請求書
            SHIHARAI_MIKAKUTEI_LIST = 29,       // 支払未確定一覧表
            SHIHARAI_DETAIL_LIST = 30,          // 支払明細表
            ORDER_TAG_LIST = 31,                // 受注札
            ANKEN_DATA_LIST = 32                // 案件一覧
        }
    }
}