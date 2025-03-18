using System.Globalization;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace SeikyuWeb.Common
{
    /// <summary>
    /// DateTimeコンバータクラス
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 文字列型からDateTime型への変換
        /// </summary>
        /// <param name="reader">JSONリーダー</param>
        /// <param name="typeToConvert">変換する型</param>
        /// <param name="options">JSONシリアライザーオプション</param>
        /// <returns>変換されたDateTime</returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();

            if (DateTime.TryParseExact(dateString, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue) ||
                DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue) ||
                DateTime.TryParseExact(dateString, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue) ||
                DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                return dateValue;
            }
            throw new JsonException(SystemConstants.Message.InValidDate);
        }

        /// <summary>
        /// DateTime型から文字列型への変換
        /// </summary>
        /// <param name="writer">JSONライター</param>
        /// <param name="value">変換するDateTime値</param>
        /// <param name="options">JSONシリアライザーオプション</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
