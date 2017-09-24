using System;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class Version : BaseValue, IComparable<Version>
    {

        public static Version Parse(String version)
        {
			String[] parts = version.Split('.');
			if(parts.Length<3)
				throw new Exception("Version must be like 1.2.3!");
			Version v = new Version();
			v.major = Int16.Parse(parts[0]);
			v.minor = Int16.Parse(parts[1]);
			v.fix = Int16.Parse(parts[2]);
			return v;
        }

		Int16 major;
		Int16 minor;
		Int16 fix;


		public Version()
			: base(VersionType.Instance)
		{
		}

		public Int32 AsInt()
		{
			return (major << 24) | (minor << 16) | fix;
		}


        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Version)
                return this.CompareTo((Version)value);
            else
                throw new SyntaxError("Illegal comparison: Version - " + value.GetType().Name);

        }

        override
        public Object ConvertTo(Type type)
        {
            return this;
        }
        
        public int CompareTo(Version other)
        {
			return this.AsInt().CompareTo(other.AsInt());
        }

        override
        public bool Equals(object obj)
        {
			if (obj is Version)
				return this.AsInt() == ((Version)obj).AsInt();
			else
				return false;
        }

        override
        public int GetHashCode()
        {
			return ToString().GetHashCode();
        }

        override
        public string ToString()
        {
			return "" + major + "." + minor + "." + fix;
			}
    }
}
