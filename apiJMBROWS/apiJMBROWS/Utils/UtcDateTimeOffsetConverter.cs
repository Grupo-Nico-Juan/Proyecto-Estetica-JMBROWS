using System.Text.Json.Serialization;
using System.Text.Json;

namespace apiJMBROWS.Utils
{
    public class UtcDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Intenta leer la fecha, asumiendo que puede venir con o sin zona
            var value = reader.GetString();
            if (DateTimeOffset.TryParse(value, out var dto))
            {
                // Convierte siempre a UTC
                return dto.ToUniversalTime();
            }
            throw new JsonException($"No se pudo convertir '{value}' a DateTimeOffset.");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            // Siempre serializa en UTC
            writer.WriteStringValue(value.ToUniversalTime().ToString("o"));
        }
    }
}
