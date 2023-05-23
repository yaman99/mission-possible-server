using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MissionPossible.Shared.Authentication
{
    public class JwtAuthAttribute : AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}