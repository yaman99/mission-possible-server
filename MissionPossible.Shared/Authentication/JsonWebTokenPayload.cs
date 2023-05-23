using System.Collections.Generic;

namespace MissionPossible.Shared.Authentication
{
    public class JsonWebTokenPayload
    {
        public string Subject { get; set; }
        public long Expires { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}