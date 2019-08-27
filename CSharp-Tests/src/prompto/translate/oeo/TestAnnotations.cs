using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestAnnotations : BaseOParserTest
	{

		[Test]
		public void testReactWidgetProps1()
		{
			compareResourceOEO("annotations/ReactWidgetProps1.poc");
		}

		[Test]
		public void testReactWidgetProps2()
		{
			compareResourceOEO("annotations/ReactWidgetProps2.poc");
		}

		[Test]
		public void testWidgetField()
		{
			compareResourceOEO("annotations/WidgetField.poc");
		}

		[Test]
		public void testWidgetProps1()
		{
			compareResourceOEO("annotations/WidgetProps1.poc");
		}

		[Test]
		public void testWidgetProps2()
		{
			compareResourceOEO("annotations/WidgetProps2.poc");
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

