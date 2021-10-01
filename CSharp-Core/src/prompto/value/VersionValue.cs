using System;
using System.Text;
using Newtonsoft.Json.Linq;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class VersionValue : BaseValue, IComparable<VersionValue>
    {
   
        public static readonly VersionValue LATEST = ParseUInt32(0xFFFFFFFF);
        public static readonly VersionValue DEVELOPMENT = ParseUInt32(0xFEFEFEFE);

        public static VersionValue Parse(String literal)
        {
            if ("latest" == literal)
                return LATEST;
            else if ("development" == literal)
                return DEVELOPMENT;
            else
                return ParsePrefixedSemanticVersion(literal);
        }

        static VersionValue ParsePrefixedSemanticVersion(String literal)
        {
            if (literal[0]=='v')
				literal = literal.Substring(1);
            return ParseSemanticVersion(literal);
        }

        public static VersionValue ParseSemanticVersion(String literal)
        {
            String[] parts = literal.Split('-');
            VersionValue version = ParseVersionNumber(parts[0]);
            if (parts.Length > 1)
                version.qualifier = ParseVersionQualifier(parts[1]);
            return version;
        }

        public static VersionValue ParseVersionNumber(String literal)
        {
            String[] parts = literal.Split('.');
			if(parts.Length < 2)
				throw new Exception("Version must be like 1.2{.3}!");
			VersionValue v = new VersionValue();
			v.major = Int16.Parse(parts[0]);
			v.minor = Int16.Parse(parts[1]);
            if(parts.Length > 2)
			    v.fix = Int16.Parse(parts[2]);
			return v;
        }

        private static Int16 ParseVersionQualifier(String literal)
        {
            if ("alpha" == literal)
                return -3;
            else if ("beta" == literal)
                return -2;
            else if ("candidate" == literal)
                return -1;
            else
                throw new Exception("Version qualifier must be 'alpha', 'beta' or 'candidate'!");
        }

        private static VersionValue ParseUInt32(UInt32 value)
        {
            VersionValue v = new VersionValue();
            v.major = (Int16)(value >> 24 & 0xFF);
            v.minor = (Int16)(value >> 16 & 0xFF);
            v.fix = (Int16)(value >> 8 & 0xFF);
            v.qualifier = (Int16)(value & 0xFF);
            return v;
        }


        Int16 major;
        Int16 minor;
        Int16 fix;
        Int16 qualifier;

        public VersionValue()
			: base(VersionType.Instance)
		{
		}

		public Int32 AsInt()
		{
			return (Int32)(major << 24) | (Int32)(minor << 16) | (Int32)(fix << 8) | (Int32)qualifier;
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
            if (this == LATEST)
                return "latest";
            else if (this == DEVELOPMENT)
                return "development";
            else
            {
                StringBuilder sb = new StringBuilder("v")
                    .Append(major)
                    .Append('.')
                    .Append(minor);
                if (fix > 0)
                    sb.Append('.')
                        .Append(fix);
                if (qualifier < 0)
                    sb.Append('-')
                        .Append(QualifierToString());
                return sb.ToString();
            }
		}

        private String QualifierToString()
        {
            switch (qualifier)
            {
                case -3:
                    return "alpha";
                case -2:
                    return "beta";
                case -1:
                    return "candidate";
                default:
                    throw new Exception("Unsupported version qualifier: " + qualifier);
            }
        }

        public override JToken ToJsonToken()
        {
            return new JValue(this.ToString());
        }

        public override IValue GetMemberValue(Context context, String name, bool autoCreate)
        {
            if ("major" == name)
                return new IntegerValue(major);
            else if ("minor" == name)
                return new IntegerValue(minor);
           else if ("fix" == name)
                return new IntegerValue(fix);
           else if ("qualifier" == name)
                return new TextValue(QualifierToString());
            else
                return base.GetMemberValue(context, name, autoCreate);
        }

    }
}
