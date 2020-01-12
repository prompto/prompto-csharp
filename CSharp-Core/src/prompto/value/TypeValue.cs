using prompto.runtime;
using prompto.type;
namespace prompto.value
{

    public class TypeValue : BaseValue
    {

        IType value;

		public TypeValue(IType value)
			: base(new TypeType(value))
        {
			this.value = value;
        }

		public IType GetValue() {
			return value;
		}

        public override IValue GetMemberValue(Context context, string name, bool autoCreate)
        {
            return value.getStaticMemberValue(context, name);
        }

        public override string ToString()
        {
            return value.GetTypeName();
        }
    }
}
