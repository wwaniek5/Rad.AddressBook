using System;
using System.Runtime.Serialization;

namespace RAD.AddressBook.Security
{
    [Serializable]
    internal class DuplikateUserException : Exception
    {
        public DuplikateUserException()
        {
        }

        public DuplikateUserException(string message) : base(message)
        {
        }

        public DuplikateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplikateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}