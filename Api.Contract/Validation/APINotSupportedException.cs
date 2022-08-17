using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Validation
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {

        }
    }

    public class APINotSupportedException : APIException
    {
        public APINotSupportedException(string value) : base(
             ValidationResultHelper.BuildSingleMessage(nameof(ErrorMessages.NotSupportedException), value)
        )
        {

        }
    }

    public class APIArgumentException : APIException
    {
        public APIArgumentException(string value) : base(
             ValidationResultHelper.BuildSingleMessage(nameof(ErrorMessages.IsRequired), value)
        )
        {

        }
    }

    public class APIArgumentNullException : APIException
    {
        public APIArgumentNullException(string value) : base(
             ValidationResultHelper.BuildSingleMessage(nameof(ErrorMessages.IsRequired), value)
        )
        {

        }
    }

}
