using System;
using System.Runtime.Serialization;

namespace prompto.store
{
	[Serializable]
	class InvalidValueError : Exception
	{
		object p;

		public InvalidValueError()
		{
		}

		public InvalidValueError(string message) : base(message)
		{
		}

		public InvalidValueError(object p)
		{
			this.p = p;
		}

		public InvalidValueError(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidValueError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}