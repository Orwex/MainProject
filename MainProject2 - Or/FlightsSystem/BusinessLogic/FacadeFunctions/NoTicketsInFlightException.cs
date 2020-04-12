using System;
using System.Runtime.Serialization;

namespace FlightsSystem
{
    [Serializable]
    internal class NoTicketsInFlightException : Exception
    {
        public NoTicketsInFlightException()
        {
        }

        public NoTicketsInFlightException(string message) : base(message)
        {
        }

        public NoTicketsInFlightException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoTicketsInFlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}