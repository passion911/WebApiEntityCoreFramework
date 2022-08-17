using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    [Serializable]
    public class SourceException : Exception
    {
        public SourceException(SourceResultCode statusCode) : base(statusCode.ToString())
        {
            this.StatusCode = statusCode;
        }

        public SourceException(SourceResultCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public SourceException(SourceResultCode statusCode, string message, Exception inner) : base(message, inner)
        {
            this.StatusCode = statusCode;
        }

        protected SourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            this.StatusCode = (SourceResultCode)info.GetInt32(nameof(this.StatusCode));
        }

        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //
        public SourceResultCode StatusCode { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(this.StatusCode), this.StatusCode);
            base.GetObjectData(info, context);
        }
    }
}
