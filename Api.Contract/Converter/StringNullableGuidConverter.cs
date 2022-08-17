using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Contract.Converter
{
    public class StringNullableGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            if (!Guid.TryParse(value, out var result))
            {
                throw new JsonException($"Unable to convert \"{value}\" to Guid.");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
