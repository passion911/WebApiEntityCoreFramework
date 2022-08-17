using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Contract.Validation
{
    public class ValidationResultHelper
    {
        private static string _singleKey = string.Empty;
        public static ValidationResult BuildValidationResult(string key, ValidationContext validationContext, params object[] args)
        {
            string message = BuildSingleMessage(key, args);
            return new ValidationResult(message, new List<string> { validationContext.MemberName });
        }
        public static ValidationResult BuildValidationResult(string key, ValidationContext validationContext)
        {
            string message = BuildSingleMessage(key, validationContext.MemberName);
            return new ValidationResult(message, new List<string> { validationContext.MemberName });
        }

        public static string BuildSingleMessage(string key, params object[] args)
        {
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };

            return JsonSerializer.Serialize(ErrorKeyValue.Build(key, args), options);
        }

        public static Dictionary<string, string[]> BuildMessageForActionResult(string key, params object[] args)
        {
            return new Dictionary<string, string[]>
                {
                    {_singleKey, new string[] { BuildSingleMessage(key, args ) }
                }
            };
        }

        public static Dictionary<string, string[]> BuildMessageForActionResult(string key, IReadOnlyDictionary<string, string[]> errors)
        {
            string value = errors.Count > 0 ? errors.FirstOrDefault().Value[0].ToString() : string.Empty;
            return BuildMessageForActionResult(key, errors.FirstOrDefault().Key, value);
        }
    }
}
