using Api.Contract.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Contract.Converter
{
    public class RestrictedStringEnumConverter<T> : JsonConverter<T>
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Type t = IsNullableType(typeToConvert) ? Nullable.GetUnderlyingType(typeToConvert) : typeToConvert;
            object value;

            if (reader.TokenType ==JsonTokenType.Null)
            {
                return default;
            }else if (reader.TokenType == JsonTokenType.String)
            {
                value = reader.GetString();
            }
            else
            {
                value = reader.GetInt32();
            }

            if (!Enum.TryParse(t, value?.ToString(), false, out object? result) && !Enum.TryParse(t, value?.ToString(), true, out result))
            {
                ThrowException(value, t);
            }
            else if (!Enum.IsDefined(t, result.ToString()))
            {
                ThrowException(value, t);
            }

            return (T)result;
        }

        private static void ThrowException(object value, Type t)
        {
            string errorMessage = string.Format($"Value {ToString(value)}");
            throw new JsonException(errorMessage);
        }

        private static string ToString(object value)
        {
            if (value == null)
            {
                return "{null}";
            }

            return (value is string s) ? @"""" + s + @"""" : value.ToString();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
       
    }

    public class RestrictedStringEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum || Nullable.GetUnderlyingType(typeToConvert)?.IsEnum == true; ;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(RestrictedStringEnumConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
}

    

