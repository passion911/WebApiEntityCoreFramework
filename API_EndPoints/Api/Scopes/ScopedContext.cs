using Api.Contract.Models;
using Client.Area;

namespace API_EndPoints.Api.Scopes
{
    public class ScopedContext : SourceContext
    {
        public MachineScope Scope { get; set; }
    }
}
