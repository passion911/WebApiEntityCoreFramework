using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public class SourceContext
    {
        public ClaimsPrincipal User { get; set; }

        public string AccessToken { get; set; }
    }
}
