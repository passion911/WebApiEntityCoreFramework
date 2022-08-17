using Api.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Validation
{
    public class ErrorMessages
    {
        public static string NEED_TO_TRANSLATE_KEY = "NT:";
        public static string ForbiddenForState => "Field {0} is not allowed for machine of type {1}.";

        public static string RequiredForState => "Filed {0} is mandatory for machine of type {1}.";

        public static string ForbiddenFor => "Field {0} is not allowed for machine of type {1} in context of {2}.";

        public static string RequiredFor => "Field {0} is required for machine of type {1} in context of {2}.";

        public static string FieldIsNotPartOfModel => "Unknown field {0}.";

        public static string FieldIsRequired => "Field {0} is mandatory.";

        public static string MachineClassForbiddenForScope => "Machine class {0} is not allowed in context of {1}.";

        public static string DuplicationValue => "Value {0} for field {1} cannot be duplicated.";
        public static string InvalidRequest => "Invalid Request!";
        public static string NotModified => "Not Modified!";
        public static string NoUpdatedWereDetected => "No updated were detected for this data object.";
        public static string PreconditionFailed => "Someone else has updated this {0} in the meantime. Please discard changes and reload data.";
        public static string Conflict => "{0} value {1} already exists in database.";

        public static string SomeThingBadHappend => "Something bad happened; please try again later.";

        public static string DuplicatedCoolant => "There are the duplicated Coolants in Coolant group.";


        public static Dictionary<MachineScope, string> MachineScopeNames => new Dictionary<MachineScope, string>
        {
            {MachineScope.Generic,"Generic"},
            {MachineScope.Iot,"CoroPlus Maichining Foresight"},
            {MachineScope.Cd2C,"CoroPlus ToolGuide"},
            { MachineScope.Tulip,"CoroPlus Machining Guide" }
        };

        public static string CanNotFindProperty => "Cannot find property of type {0}.";
        public static string CanNotValidateNullObject => "Cannot validate null object.";

        public static string InvalidError => "The value '{0}' is invalid.";
        public static string NotValidError => "The value '{0}' is not valid.";

        public static string MachineNameIsNotUnique => "Machine name is not unique";
        public static string MachineNameIsExistedInRecycleBin => $"Machine name already exists in the recycle bin." +
            $" You can either recover that machine or delete it permanently and create new one with the same name.";
        public static string DeletedMachineIsNotInRecycleBin = "It is not allowed to permanently delete machine that is not in the recycle bin.";
        public static string UpdateDeletedMachine = "It is not allowed update deleted machine.";

        public static string DataIsNotValid => "Data is not valid.";
        public static string MachineIsNotValid => "Machine data is not valid";
        public static string MeterialsIsNotValid => "Materials data is not valid";
        public static string LocationIsNotValid => "Location data is not valid";
        public static string MagazineIsNotValid => "Magazine data is not valid";
        public static string AssembliesIsNotValid => "Assemblies data is not valid";
        public static string LookupsIsNotValid => "Lookup data is not valid";
        public static string UnrecognizedEquipmentType => "Unrecognized equipmentType";

        public static string RouteIdentifierMismatch => "{0}: Route identifier/Model identifier mismatch";
        public static string FileIsRequired => "File is required.";
        public static string ReadingFileFailure => "Reading file failure. Error: {0}";

        public static string MachineIdIsNotEmpty => "MachineId is not empty";

        public static string BodyIsRequired => "Body is required";
        public static string InvalidContentType => "Invalid content type";
        public static string FileTooLarge => "File too large. Max length is 100 Kilobytes";

        public static string InvalidField => "The field {0} is invalid.";
        public static string Equal => "The field {0} value {1} is not equal field {2} value {3}.";
        public static string GreaterThan => "The field {0} value {1} is not greater than field {2} value {3}.";
        public static string GreaterThanValue => "The field {0} value {1} is not greater than {2}.";

        public static string TimeZoneIsInCorrectFormat => "Timezone: {0} is incorrect.";

        public static string GreaterThanOrEqual => "The field {0} value {1} is not greater than or equal field {2} value {3}.";
        public static string LowerThenOrEqual => "The field {0} value {1} is not lower than or equal field {2} value {3}.";
        public static string LowerThan => "The field {0} value {1} is not lower than field {2} value {3}.";
        public static string NotEqual => "The field {0} value {1} is must equal field {2} value {3}.";
        public static string NotFound => "{0} is not found";
        public static string NotSupportedException => "Not Supported: {0}";
        public static string SourceException => "{0}";
        public static string NotRequireForPrivate => "{0} is required for private entity";
        public static string NumberOfToolExceedTheLimit => "Number of tools '{0}' exceeded the limit '{1}'.";

        public static string ArgumentNullException => "{0} cannot be null or empty.";
        public static string ArgumentException => "{0} cannot be null or empty";

        public static string Forbidden => "User can not access to another resource";
        public static string IsRequired => "The {0} is mandatory";
        public static string SpindleCombinationOfValuesNotAllowed => "Combination of Spindle values not allowed.";

        public static string MaterialNameAlreadyExistInRecycleBin => "The material {0} already exists." +
            $" You can either recover that material or delete it permanently and create new one with the same name.";

        public static string MaterialNameAlreadyExist => "The material {0} already exists.";

        public static string UpdateDeletedMaterial = "It is not allowed update deleted material.";

        public static string DeletedMaterialIsNotInRecycleBin = "It is not allowed to permanently delete material that is not in the recycle bin.";


        public static string SerialNumberIsNotUnique => "SerialNumber is not unique";
    }
}
