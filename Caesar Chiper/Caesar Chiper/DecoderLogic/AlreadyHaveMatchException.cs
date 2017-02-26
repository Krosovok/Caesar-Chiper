using System;
using System.Runtime.Serialization;

namespace Caesar_Chiper.DecoderLogic
{
    [Serializable]
    internal class AlreadyHaveMatchException : Exception
    {
        public AlreadyHaveMatchException()
        {
        }

        public AlreadyHaveMatchException(string message) : base(message)
        {
        }

        public AlreadyHaveMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyHaveMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}