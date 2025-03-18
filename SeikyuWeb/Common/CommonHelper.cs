using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Reflection;

namespace SeikyuWeb.Common
{
    /// <summary>
    /// 共通ヘルパークラス
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// TryParse（DateTime型）
        /// </summary>
        /// <param name="dateStr">解析する日付文字列</param>
        /// <param name="result">解析結果のDateTime値</param>
        /// <param name="key">エラーメッセージに使用するキー</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>解析成功の場合はtrue、失敗の場合はfalse</returns>
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
        /// TryParse（Int型）
        /// </summary>
        /// <param name="intStr">解析する整数文字列</param>
        /// <param name="result">解析結果の整数値</param>
        /// <param name="key">エラーメッセージに使用するキー</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>解析成功の場合はtrue、失敗の場合はfalse</returns>
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
        /// 年月のフォーマットチェック
        /// </summary>
        /// <param name="ym">チェックする年月文字列</param>
        /// <returns>有効なフォーマットの場合はtrue、無効な場合はfalse</returns>
        public static bool IsValidYearMonthFormat(string ym)
        {
            string[] formats = { "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss" };
            return DateTime.TryParseExact(ym, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// Double型を四捨五入してInt型で返す
        /// </summary>
        /// <param name="value">四捨五入するDouble値</param>
        /// <returns>四捨五入されたInt値</returns>
        public static int RoundDoubleToInt(double? value) => value.HasValue ? (int)Math.Round(value.Value) : 0;

        /// <summary>
        /// Decimal型を四捨五入してInt型で返す
        /// </summary>
        /// <param name="value">四捨五入するDecimal値</param>
        /// <returns>四捨五入されたInt値</returns>
        public static int RoundDecimalToInt(decimal? value) => value.HasValue ? (int)Math.Round(value.Value) : 0;

        /// <summary>
        /// Decimal型を切り上げしてInt型で返す
        /// </summary>
        /// <param name="value">切り上げするDecimal値</param>
        /// <returns>切り上げされたInt値</returns>
        public static int CeilingDecimalToInt(decimal? value) => value.HasValue ? (int)Math.Ceiling(value.Value) : 0;

        /// <summary>
        /// モデル名からインスタンスを取得
        /// </summary>
        /// <param name="modelName">プロシージャのモデル名</param>
        /// <returns>プロシージャのモデルインスタンス</returns>
        public static object GetInstanceByModelName(string modelName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullName = assembly.GetName().Name + ".Models.ReportCommons." + modelName;
            Type modelType = assembly.GetType(fullName);

            if (modelType == null)
            {
                throw new BadHttpRequestException(string.Format(SystemConstants.Message.InValidParam, "Class_Name"));
            }

            object modelInstance = Activator.CreateInstance(modelType);

            return modelInstance;
        }

        /// <summary>
        /// Double型を四捨五入してLong型で返す
        /// </summary>
        /// <param name="value">四捨五入するDouble値</param>
        /// <returns>四捨五入されたLong値</returns>
        public static long RoundDoubleToLong(double? value) => value.HasValue ? (long)Math.Round(value.Value) : 0L;

        /// <summary>
        /// Decimal型を四捨五入してLong型で返す
        /// </summary>
        /// <param name="value">四捨五入するDecimal値</param>
        /// <returns>四捨五入されたLong値</returns>
        public static long RoundDecimalToLong(decimal? value) => value.HasValue ? (long)Math.Round(value.Value) : 0L;

        /// <summary>
        /// Decimal型を切り上げしてLong型で返す
        /// </summary>
        /// <param name="value">切り上げするDecimal値</param>
        /// <returns>切り上げされたLong値</returns>
        public static long CeilingDecimalToLong(decimal? value) => value.HasValue ? (long)Math.Ceiling(value.Value) : 0L;
    }
}
