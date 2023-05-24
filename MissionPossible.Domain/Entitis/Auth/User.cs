using MissionPossible.Domain.Common;
using MissionPossible.Domain.Constants;
using MissionPossible.Domain.Entitis.Auth;
using MissionPossible.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace MissionPossible.Domain.Entities.Auth
{
    public class User : BaseEntity<Guid>
    {

        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string PasswordHash { get; private set; }
        public string UserType { get; set; }
        public string ActivationCode { get; private set; }
        public bool IsActive { get; private set; }
        public RefreshToken RefreshToken { get; private set; }

        public User(Guid id) : base(id)
        {
        }

        public User(Guid id, string email, string fullName, string userType) : base(id)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            FullName = fullName;
            IsActive = true;
            UserType = userType;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new IdentityException(Codes.InvalidPassword,
                    "Password can not be empty.");
            }
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public void GenerateRefreshToken(IPasswordHasher<User> passwordHasher)
        {
            RefreshToken = new RefreshToken(this, passwordHasher);

        }

        public void GenerateActivationCode()
        {
            ActivationCode = Guid.NewGuid().ToString("N");
        }
        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;

        public void ActivateAccount() => this.IsActive = true;
        public void DeActivateAccount() => this.IsActive = false;
        public string GenerateRandomCode(int length = 8)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}