using System;
using System.Runtime.Serialization;

namespace prompto.store
{
	[Serializable]
	class InvalidValueError : Exception
	{

		public InvalidValueError(object o)
			: this(o.ToString())
		{
			
		}

		public InvalidValueError(string message) : base(message)
		{
		}

		public InvalidValueError(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidValueError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}