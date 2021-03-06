using System;
using Newtonsoft.Json.Linq;
using prompto.type;

namespace prompto.value
{
    public class UUIDValue : BaseValue
    {
        Guid value;

        public UUIDValue(Guid value)
			: base(UUIDType.Instance)
        {
            this.value = value;
        }

        public Guid Value { get { return value; } }

		public TextValue AsText()
		{
			return new TextValue (value.ToString ());
		}

        
        public override String ToString()
        {
            return Value.ToString();
        }

        
        public override bool Equals(object obj)
        {
            if (obj is UUIDValue)
                return value.Equals(((UUIDValue)obj).value);
            else
                return value.Equals(obj);
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override JToken ToJsonToken()
        {
            return new JValue(this.ToString());
        }

    }
}
