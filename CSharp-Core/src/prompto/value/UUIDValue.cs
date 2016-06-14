using System;
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

		public Text AsText()
		{
			return new Text (value.ToString ());
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
    }
}
