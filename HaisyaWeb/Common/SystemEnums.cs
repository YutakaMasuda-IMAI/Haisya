using System;
using System.Linq;

namespace HaisyaWeb.Common
{
    public class SystemEnums
    {
        /// <summary>
        /// T_Portal_InfoのPortal_Kubun
        /// </summary>
        public enum PortalKubun
        {
            配車WEB = 1,
            請求WEB = 2,
            連携WEB = 3
        }

        /// <summary>
        /// Report_Type
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
        }
    }

    /// <summary>
    /// Enum拡張クラス
    /// </summary>
    public static class EnumExtention
    {
        /// <summary>
        /// 文字列をenum値に変換する
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="s">文字列</param>
        /// <param name="e">enum</param>
        /// <returns></returns>
        /// <exception cref="SystemException"></exception>
        public static bool TryParser<TEnum>(this string s, out TEnum e)
            where TEnum : Enum
        {
            e = default;
            if (string.IsNullOrWhiteSpace(s))
                return false;

            s = s.Trim();
            System.Collections.Generic.IEnumerable<TEnum> a = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            foreach (var ai in a)
            {
                if (s.Equals($"{ai}", StringComparison.OrdinalIgnoreCase) ||
                    (int.TryParse(s, out int n) && n == Convert.ToInt32(ai)))
                {
                    e = ai;
                    return true;
                }
            }
            return false;
        }
    }
}
