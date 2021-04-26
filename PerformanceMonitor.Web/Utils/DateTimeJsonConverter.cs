using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PerformanceMonitor.Web.Utils
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var date = reader.GetString();

            return DateTime.Parse(date);
        }

        public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
        {
            var result = dateTimeValue.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            writer.WriteStringValue(result);
        }
    }
}
