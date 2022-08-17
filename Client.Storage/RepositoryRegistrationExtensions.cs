using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Storage
{
    public class CustomBsonMapper : ICustomBsonMapper
    {
        public void Register()
        {
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }
    }

    public interface ICustomBsonMapper
    {
        void Register();
    }
}
