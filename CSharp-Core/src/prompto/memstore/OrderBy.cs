using prompto.store;

namespace prompto.memstore
{
	public class OrderBy
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
