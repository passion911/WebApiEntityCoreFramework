using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Models
{
    public class BlobModel
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }
    }

    public class BlobOptions
    {
        public Uri BaseUri { get; set; }

        public string SharedAccessSignature { get; set; }
    }
}
