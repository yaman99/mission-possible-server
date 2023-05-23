using System.Collections.Generic;

namespace MissionPossible.Shared.Authentication
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string email, string userType , IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}