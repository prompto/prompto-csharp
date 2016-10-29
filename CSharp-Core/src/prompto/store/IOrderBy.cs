
using prompto.declaration;

namespace prompto.store
{
	public interface IOrderBy
	{
		AttributeInfo getAttributeInfo();
		bool isDescending();

	}
}
