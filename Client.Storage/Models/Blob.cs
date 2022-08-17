using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Storage.Models
{
    public class Blob
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }
    }
}
