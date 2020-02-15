using System;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class VersionValue : BaseValue, IComparable<VersionValue>
    {

        public static VersionValue Parse(String version)
        {
			if (version[0]=='v')
				version = version.Substring(1);
			String[] parts = version.Split('.');
			if(parts.Length<3)
				throw new Exception("Version must be like 1.2.3!");
			VersionValue v = new VersionValue();
			v.major = Int16.Parse(parts[0]);
			v.minor = Int16.Parse(parts[1]);
			v.fix = Int16.Parse(parts[2]);
			return v;
        }

        Int32 major;
        Int32 minor;
        Int32 fix;


		public VersionValue()
			: base(VersionType.Instance)
		{
		}

		public Int32 AsInt()
		{
			return (major << 24) | (minor << 16) | fix;
		}


        
        public override Int32 CompareTo(Context context, IValue value)
        {
            if (value is VersionValue)
                return this.CompareTo((VersionValue)value);
            else
                throw new SyntaxError("Illegal comparison: Version - " + value.GetType().Name);

        }

        
        public override Object ConvertTo(Type type)
        {
            return this;
        }
        
        public int CompareTo(VersionValue other)
        {
			return this.AsInt().CompareTo(other.AsInt());
        }

        
        public override bool Equals(object obj)
        {
			if (obj is VersionValue)
				return this.AsInt() == ((VersionValue)obj).AsInt();
			else
				return false;
        }

        
        public override int GetHashCode()
        {
			return ToString().GetHashCode();
        }

        
        public override string ToString()
        {
			return "" + major + "." + minor + "." + fix;
			}
    }
}
