using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestAnnotations : BaseOParserTest
	{

		[Test]
		public void testWidgetField()
		{
			compareResourceOEO("annotations/WidgetField.poc");
		}

		[Test]
		public void testCallback()
		{
			compareResourceOEO("annotations/callback.poc");
		}

		[Test]
		public void testCategory()
		{
			compareResourceOEO("annotations/category.poc");
		}

	}
}

