using prompto.declaration;

namespace prompto.store
{
	public class OrderBy : IOrderBy
	{


		AttributeInfo attribute;
		bool descending;

		public OrderBy(AttributeInfo attribute, bool descending)
		{
			this.attribute = attribute;
			this.descending = descending;
		}

		public AttributeInfo getAttributeInfo()
		{
			return attribute;
		}

		public bool isDescending()
		{
			return descending;
		}

	}
}
