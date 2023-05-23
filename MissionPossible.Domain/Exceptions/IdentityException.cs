using System;

namespace MissionPossible.Domain.Exceptions
{
    public class IdentityException : Exception
    {
        public string Code { get; }

        public IdentityException()
        {
        }

        public IdentityException(string code)
        {
            Code = code;
        }

        public IdentityException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public IdentityException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public IdentityException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public IdentityException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}