using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Contract.Validation
{
    public class ErrorKeyValue
    {
        [JsonPropertyName("errorKey")]
        public string ErrorKey { get; set; }
        [JsonPropertyName("errorParams")]
        public object[] ErrorParams { get; set; }

        public static ErrorKeyValue Build(string key, object[] args)
        {
            var errorKeyValue = new ErrorKeyValue();
            errorKeyValue.ErrorKey = key;
            errorKeyValue.ErrorParams = args;
            return errorKeyValue;
        }
    }
}
