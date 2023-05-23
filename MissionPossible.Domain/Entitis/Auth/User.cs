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
        public string SecondaryEmail { get; private set; }
        public string? Phone { get; private set; }
        public string PasswordHash { get; private set; }
        public string UserType { get; set; }
        public string ActivationCode { get; private set; }
        public bool IsActive { get; private set; }
        public RefreshToken RefreshToken { get; private set; }
        public List<Violation> Violations { get; private set; }
        public List<string> Punishments { get; private set; }

        public User(Guid id) : base(id)
        {
        }

        public User(Guid id, string email, string userType) : base(id)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            IsActive = false;
            SetUserType(userType.ToLowerInvariant());
            Violations = new List<Violation>();
            Punishments = new List<string>();
        }

        private void SetUserType(string type)
        {
            switch (type)
            {
                case UserTypes.Promoter:
                    UserType = UserTypes.Promoter;
                    break;
                case UserTypes.Advertiser:
                    UserType = UserTypes.Advertiser;
                    break;
                case UserTypes.Admin:
                    UserType = UserTypes.Admin;
                    break;
                default:
                    throw new IdentityException(Codes.InvalidUserType);
            }
        }

        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetSecondaryEmail(string email)
        {
            SecondaryEmail = email;
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

        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        public void GenerateRandomCode(int length = 8)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            ActivationCode = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Tuple<string, int> AddViolation(string type)
        {
            var facts = new Tuple<string, int>("",0);
            switch (type)
            {
                case ViolationTypes.LackOfWalletBalance:
                    facts = AddLackOfWalletViolation();
                    break;
            }
            return facts;
        }

        public void ResetViolationByType(string type)
        {
            Violations.RemoveAll(x => x.Type == type);
        }
       
        private string LackOfWalletViolationDecision(int count)
        {
            string? punishment;
            if (count > 3) // warned 3 times
            {
                punishment = PunishmentTypes.SuspendAccountFromCampaigns;
            }
            else
            {
                punishment = PunishmentTypes.LackofWalletWarning;
            }
            AddPunishment(punishment);
            return punishment;
        }
        private Tuple<string , int> AddLackOfWalletViolation()
        {
            var violation = Violations.Find(x => x.Type == ViolationTypes.LackOfWalletBalance);
            if (violation != null)
            {
                violation.RepeatedCount++;
            }
            else
            {
                violation = new Violation
                {
                    Type = ViolationTypes.LackOfWalletBalance,
                    RepeatedCount = 1,
                    CreatedDate = DateTime.UtcNow
                };
                Violations.Add(violation);
            }
            var decision  = LackOfWalletViolationDecision(violation.RepeatedCount);
            return new Tuple<string, int>(decision, violation.RepeatedCount);
        }

        private void AddPunishment(string punishmentType)
        {
            var punishment = Punishments.FirstOrDefault(x => x.Equals(punishmentType));
            if (punishment == null)
            {
                Punishments.Add(punishmentType);
            }
        }
    }
}