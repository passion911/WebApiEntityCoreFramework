using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Storage
{
    public class StorageSourceOptions
    {
        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }

        public string TableNamePrefix { get; set; }
    }
}
