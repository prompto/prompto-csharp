using prompto.type;
namespace prompto.value
{

    public class TypeValue : BaseValue
    {

        IType value;

		public TypeValue(IType value)
			: base(null)
        {
			this.value = value;
        }

		public IType GetValue() {
			return value;
		}

    }
}
