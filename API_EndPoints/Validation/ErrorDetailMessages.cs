using Api.Contract.Validation;
using Api.Resources;
using System.Text.Json.Serialization;

namespace API_EndPoints.Validation
{
    public class ErrorDetailMessages
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("errorMessageTrans")]
        public MessageTrans ErrorMessageTrans { get; set; }
        [JsonPropertyName("errorDetails")]
        public Dictionary<string, string> ErrorDetails { get; set; }
        [JsonPropertyName("errorDetailsTrans")]
        public MessageDetailsTrans ErrorDetailsTrans { get; set; }

        public static ErrorDetailMessages Build(string key, Dictionary<string, ErrorDetailMessage> errorDetails)
        {
            ErrorDetailMessages errorItem = new ErrorDetailMessages();

            var item = ErrorDetailMessage.Build(key);
            errorItem.ErrorMessage = item.ErrorMessage;
            errorItem.ErrorMessageTrans = item.ErrorMessageTrans;

            errorItem.ErrorDetails = GetErrorDetails(errorDetails);
            errorItem.ErrorDetailsTrans = GetErrorDetailsTrans(errorDetails);
            return errorItem;
        }

        public static Dictionary<string, string> GetErrorDetails(Dictionary<string, ErrorDetailMessage> errorDetails)
        {
            var errors = new Dictionary<string, string>();
            foreach (KeyValuePair<string, ErrorDetailMessage> error in errorDetails)
            {
                errors.Add(error.Key, error.Value.ErrorMessage);
            }
            return errors;
        }

        public static MessageDetailsTrans GetErrorDetailsTrans(Dictionary<string, ErrorDetailMessage> errorDetails)
        {
            var messageTran = new MessageDetailsTrans();
            foreach (KeyValuePair<string, ErrorDetailMessage> error in errorDetails)
            {
                messageTran.De.Add(error.Key, error.Value.ErrorMessageTrans.De);
                messageTran.En.Add(error.Key, error.Value.ErrorMessageTrans.En);
                messageTran.Fr.Add(error.Key, error.Value.ErrorMessageTrans.Fr);
                messageTran.Sw.Add(error.Key, error.Value.ErrorMessageTrans.Sw);
            }
            return messageTran;
        }
    }

    public class MessageDetailsTrans
    {
        [JsonPropertyName("en")]
        public Dictionary<string, string> En { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("de")]
        public Dictionary<string, string> De { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("fr")]
        public Dictionary<string, string> Fr { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("sw")]
        public Dictionary<string, string> Sw { get; set; } = new Dictionary<string, string>();

    }

    public class MessageTrans
    {
        [JsonPropertyName("en")]
        public string En { get; set; }
        [JsonPropertyName("de")]
        public string De { get; set; }
        [JsonPropertyName("fr")]
        public string Fr { get; set; }
        [JsonPropertyName("sw")]
        public string Sw { get; set; }


        public static MessageTrans Build(string key, params object[] args)
        {
            return new MessageTrans
            {
                En = ManageTranslateResource.GetInstance().En(key, ErrorDetailMessage.GetTranslationValue(nameof(ManageTranslateResource.En), args)),
                De = ManageTranslateResource.GetInstance().De(key, ErrorDetailMessage.GetTranslationValue(nameof(ManageTranslateResource.De), args)),
                Fr = ManageTranslateResource.GetInstance().Fr(key, ErrorDetailMessage.GetTranslationValue(nameof(ManageTranslateResource.Fr), args)),
                Sw = ManageTranslateResource.GetInstance().Sw(key, ErrorDetailMessage.GetTranslationValue(nameof(ManageTranslateResource.Sw), args))
            };
        }
    }

    public class ErrorDetailMessage
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("errorMessageTrans")]
        public MessageTrans ErrorMessageTrans { get; set; }

        public static ErrorDetailMessage Build(string key, params object[] args)
        {
            return new ErrorDetailMessage
            {
                ErrorMessageTrans = MessageTrans.Build(key, args),
                ErrorMessage = ManageTranslateResource.GetInstance().En(key, GetTranslationValue(nameof(ManageTranslateResource.En), args))
            };
        }

        public static string[] GetTranslationValue(string typeOfLanugage, object[] args)
        {
            if (args == null || args.Length == 0) return new string[] { };

            string[] enAgrsStr = new string[args.Count()];
            for (int i = 0; i < args.Count(); i++)
            {
                string argvalue = args[i].ToString();
                if (!string.IsNullOrEmpty(argvalue) && argvalue.StartsWith(ErrorMessages.NEED_TO_TRANSLATE_KEY))
                {
                    argvalue = argvalue.Replace(ErrorMessages.NEED_TO_TRANSLATE_KEY, string.Empty);
                }

                if (typeOfLanugage.Equals(nameof(ManageTranslateResource.En)))
                    enAgrsStr[i] = ManageTranslateResource.GetInstance().En(argvalue);
                else if (typeOfLanugage.Equals(nameof(ManageTranslateResource.Fr)))
                    enAgrsStr[i] = ManageTranslateResource.GetInstance().Fr(argvalue);
                else if (typeOfLanugage.Equals(nameof(ManageTranslateResource.De)))
                    enAgrsStr[i] = ManageTranslateResource.GetInstance().De(argvalue);
                else if (typeOfLanugage.Equals(nameof(ManageTranslateResource.Sw)))
                    enAgrsStr[i] = ManageTranslateResource.GetInstance().Sw(argvalue);

            }

            return enAgrsStr;
        }

        public static ErrorDetailMessage Build(ErrorKeyValue errorKeyValue)
        {
            return new ErrorDetailMessage
            {
                ErrorMessage = ManageTranslateResource.GetInstance().En(errorKeyValue.ErrorKey, errorKeyValue.ErrorParams.ToArray()),
                ErrorMessageTrans = MessageTrans.Build(errorKeyValue.ErrorKey, errorKeyValue.ErrorParams),
            };
        }
    }
}
