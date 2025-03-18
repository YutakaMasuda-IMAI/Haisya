using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RenkeiDB.Common
{
    /// <summary>
    /// 共通ヘルパークラス
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 文字列を日付に変換する
        /// </summary>
        /// <param name="dateStr">日付文字列</param>
        /// <param name="result">変換結果の日付</param>
        /// <param name="key">キー</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>変換成功かどうか</returns>
        public static bool TryParseDate(string dateStr, out DateTime? result, string key, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out DateTime tempResult))
            {
                result = tempResult;
                return true;
            }
            else if (!string.IsNullOrWhiteSpace(dateStr))
            {
                errorMessage = string.Format(SystemConstants.Message.InValidDate, key);
                result = null;
                return false;
            }
            result = null;
            return true;
        }

        /// <summary>
        /// 文字列を整数に変換する
        /// </summary>
        /// <param name="intStr">整数文字列</param>
        /// <param name="result">変換結果の整数</param>
        /// <param name="key">キー</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>変換成功かどうか</returns>
        public static bool TryParseInt(string intStr, out int result, string key, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!string.IsNullOrWhiteSpace(intStr) && int.TryParse(intStr, out int tempResult))
            {
                result = tempResult;
                return true;
            }
            else if (!string.IsNullOrWhiteSpace(intStr))
            {
                errorMessage = string.Format(SystemConstants.Message.InValidNumber, key);
                result = 0;
                return false;
            }
            result = 0;
            return true;
        }

        /// <summary>
        /// 文字列を郵便番号形式「^\d{3}-\d{4}$」にフォーマットします
        /// 郵便番号が「1234567」形式で想定し、「123-4567」にフォーマットします。
        /// </summary>
        /// <param name="input">郵便番号文字列</param>
        /// <returns>フォーマットされた郵便番号</returns>
        public static string FormatPostCode(string input)
        {
            string postCode = String.Empty;
            string pattern = @"^\d{3}-\d{4}$";
            if (!Regex.IsMatch(input, pattern))
            {

                if (input.Length == 7 && Regex.IsMatch(input, @"^\d{7}$"))
                {
                    postCode = input.Insert(3, "-");
                }
            }
            return postCode;
        }

        /// <summary>
        /// 表示名でプロパティを取得する
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="displayName">表示名</param>
        /// <returns>プロパティ情報</returns>
        public static PropertyInfo GetPropertyByDisplayName(object obj, string displayName)
        {
            return obj.GetType().GetProperties()
               .FirstOrDefault(prop => prop.GetCustomAttribute<DisplayAttribute>()?.Name == displayName);
        }
    }

    /// <summary>
    /// 共通拡張メソッドクラス
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// 日時を日本の形式に変換
        /// </summary>
        /// <param name="date">日本の形式日付に変換する必要がある</param>
        /// <returns>日本の形式日付になる</returns>
        public static string JapaneseDate(this DateTime date)
        {
            // 日本のカルチャ情報オブジェクトを作る
            CultureInfo ja = new("ja-JP", true);

            // カレンダーを日本カレンダーに設定
            ja.DateTimeFormat.Calendar = new JapaneseCalendar();

            // 日付を「gy年M月d日 」形式の文字列に変換
            return date.ToString("gy年M月d日", ja);
        }

        /// <summary>
        /// 日時を日本の形式に変換
        /// </summary>
        /// <param name="date">日本の形式日付に変換する必要がある</param>
        /// <returns>日本の形式日付になる</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string JapaneseDate(this DateTime? date) => date?.JapaneseDate() ?? "";

        /// <summary>
        /// 日本の曜日を取得
        /// </summary>
        /// <param name="date">日本の日付に変換する必要がある</param>
        /// <returns>日本の曜日</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string JapaneseDayOfWeek(this DateTime date)
            => date.ToString("ddd", new CultureInfo("ja-JP"));

        /// <summary>
        /// 日本の曜日を取得
        /// </summary>
        /// <param name="date">日本の日付に変換する必要がある</param>
        /// <returns>日本の曜日</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string JapaneseDayOfWeek(this DateTime? date) => date?.JapaneseDayOfWeek() ?? "";

        /// <summary>
        /// 「yyyy/MM/dd か yyyyMMdd」の文字列をdateに変換します
        /// </summary>
        /// <param name="yyyy_mm_dd">日付文字列</param>
        /// <param name="date">変換結果の日付</param>
        /// <returns>変換成功かどうか</returns>
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
    }
}
