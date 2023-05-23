
using MissionPossible.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace MissionPossible.Domain.Entities.Auth
{
    public class RefreshToken 
    {
        public string Token { get; private set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Expired => DateTime.UtcNow > ExpireDate;
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            
            CreatedAt = DateTime.UtcNow;
            ExpireDate = DateTime.UtcNow.AddDays(7);
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new IdentityException(Codes.RefreshTokenAlreadyRevoked, 
                    $"Refresh token:  was already revoked at '{RevokedAt}'.");
            }
            RevokedAt = DateTime.UtcNow;
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}