using System;
using System.Globalization;

namespace HaisyaWeb.Common
{
    public static class DateTimeConverter
    {
        /// <summary>
        /// 「yyyy/MM/dd か yyyyMMdd」の文字列をdateに変換します
        /// </summary>
        /// <param name="yyyy_mm_dd"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseDate(this string yyyy_mm_dd, out DateTime? date)
        {
            date = default;
            yyyy_mm_dd = yyyy_mm_dd?.Trim() ?? "";
            // lengthチェク
            if (yyyy_mm_dd.Length != "yyyy/MM/dd".Length && yyyy_mm_dd.Length != "yyyyMMdd".Length)
                return false;
            // delta計算
            int delta = yyyy_mm_dd.Length == "yyyy/MM/dd".Length ? 0 : 1;

            // [yyyy, MM, dd]計算し、チェック
            if (!int.TryParse(yyyy_mm_dd[..4], out int yyyy)
                || !int.TryParse(yyyy_mm_dd[(5 - delta)..7], out int mm)
                || mm > 12 || mm < 1
                || !int.TryParse(yyyy_mm_dd[(8 - delta - delta)..], out int dd)
                || dd > 31 || dd < 1)
            {
                return false;
            }
            date = new DateTime(yyyy, mm, 1).AddDays(dd - 1);
            return true;
        }

        /// <summary>
        ///日時を日本の形式に変換
        /// </summary>
        /// <param name="date">日本の形式日付に変換する必要がある</param>
        /// <returns>日本の形式日付になる</returns>
        public static string DateTimeConverterJP(DateTime date)
        {
            // 日本のカルチャ情報オブジェクトを作る
            CultureInfo culture = new CultureInfo("ja-JP", true);

            //カレンダーを日本カレンダーに設定
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            // 日付を「gy年M月d日 」形式の文字列に変換
            return date.ToString("gy年M月d日", culture);
        }

        /// <summary>
        ///日本の曜日を取得
        /// </summary>
        /// <param name="date">日本の日付に変換する必要がある</param>
        /// <returns>日本の曜日</returns>
        public static string GetDayOfWeekInJapanese(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            //日本のカルチャ情報オブジェクトを作る
            CultureInfo culture = new CultureInfo("ja-JP");

            // 日本語で曜日の形式を作成
            string dayOfWeekFormat = "ddd";

            //日本語で曜日を取得
            return date?.ToString(dayOfWeekFormat, culture);
        }

        /// <summary>
        /// 日付から時間を取得
        /// </summary>
        ///<param name="date">日付を時間に変換する必要がある</param>
        /// <returns>HH:mmの形式で時間</returns>
        public static string GetTimeInHHmm(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            string timeFormat = "HH:mm";

            return date?.ToString(timeFormat);
        }

        /// <summary>
        ///日本語で曜日を全文取得
        /// </summary>
        /// <param name="date">日本で曜日を変換する必要がある</param>
        /// <returns>日本語での全文日付形式</returns>
        public static string GetDayOfWeekFullJapanese(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            //日本のカルチャ情報オブジェクトを作る
            CultureInfo culture = new CultureInfo("ja-JP");

            //日本語で曜日の形式を作成
            string dayOfWeekFormat = "dddd";

            //日本語で曜日を取得
            return date?.ToString(dayOfWeekFormat, culture);
        }


        /// <summary>
        /// Datetimeの時間を文字表記に変換して返却
        /// NULLや空文字の場合は""を返却
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetTimeForString(string val)
        {

            if (val == null) { return ""; }
            if (val.Length == 0) { return ""; }
            if (!DateTime.TryParse(val, out DateTime target)) { return ""; }

            int ihour = 0;

            if (target >= DateTime.Parse("1900/01/02 00:00:00"))
            {
                int i = (int)(target - DateTime.Parse("1900/01/01 00:00:00")).TotalDays;
                ihour = i * 24;
            }

            string s = (target.Hour + ihour).ToString("00") + ":" + (target.Minute).ToString("00");
            return s;

        }

        /// <summary>
        /// Datetimeの時間を文字表記に変換して返却
        /// NULLや空文字の場合は"00:00"を返却
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetTimeForStringNZ(string val)
        {

            if (val == null) { return "00:00"; }
            if (val.Length == 0) { return "00:00"; }
            if (!DateTime.TryParse(val, out DateTime target)) { return "00:00"; }

            int ihour = 0;

            if (target >= DateTime.Parse("1900/01/02 00:00:00"))
            {
                int i = (int)(target - DateTime.Parse("1900/01/01 00:00:00")).TotalDays;
                ihour = i * 24;
            }

            string result = (target.Hour + ihour).ToString("00") + ":" + (target.Minute).ToString("00");
            return result;

        }

        /// <summary>
        /// 文字列の時間("00:00")をDatetime型に変換して返却
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime GetStringToDatetime(string val)
        {

            if (val == null) { return Convert.ToDateTime("1900/01/01"); }
            if (val == "") { return Convert.ToDateTime("1900/01/01"); }

            DateTime result = GetMinutesToDatetime(GetStringToMinutes(val));
            return result;

        }

        /// <summary>
        /// 文字列の時間("00:00")を分(INT)に変換して返却
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static int GetStringToMinutes(string val)
        {
            if (val == null) { return 0; }
            if (val.Length != 5) { return 0; }

            int i;
            int h;
            int m;

            i = val.IndexOf(":");
            h = int.Parse(val.Substring(0, i - 1));
            m = int.Parse(val.Substring(i + 1));
            return h * 60 + m;

        }

        /// <summary>
        /// 文字列の時間("00:00")をDatetimeに変換
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static DateTime GetMinutesToDatetime(int val)
        {
            return Convert.ToDateTime("1900/01/01").AddMinutes(val);
        }

    }
}
