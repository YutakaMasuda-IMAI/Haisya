using System.Globalization;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace RenkeiDB.Common
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {

        /// <summary>
        /// 文字列型をDateTime型に変換
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();

            if (DateTime.TryParseExact(dateString, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue) ||
                DateTime.TryParseExact(dateString, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                return dateValue;
            }
            throw new JsonException(SystemConstants.Message.InValidDate);
        }

        /// <summary>
        /// DateTime型を文字列型に変換
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
