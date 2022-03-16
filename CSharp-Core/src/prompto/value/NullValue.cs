using System;
using Newtonsoft.Json.Linq;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
	public class NullValue : BaseValue
	{
		static NullValue instance_ = new NullValue();

		public static NullValue Instance
		{
			get
			{
				return instance_;
			}
		}

		private NullValue() 
			: base(NullType.Instance)
		{
		}

		public override object ConvertTo (Context context, Type type)
		{
			return null; // YES! you read correctly
		}

		public override object GetStorableData()
		{
			return null; // YES! you read correctly
		}

		public override JToken ToJsonToken()
		{
			return new JValue((string)null);
		}

        public override string ToString()
        {
            return "null";
        }

    }
}

