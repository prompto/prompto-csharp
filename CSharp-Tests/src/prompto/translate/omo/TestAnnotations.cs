using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestAnnotations : BaseOParserTest
	{

		[Test]
		public void testReactWidgetProps1()
		{
			compareResourceOMO("annotations/ReactWidgetProps1.poc");
		}

		[Test]
		public void testReactWidgetProps2()
		{
			compareResourceOMO("annotations/ReactWidgetProps2.poc");
		}

		[Test]
		public void testWidgetField()
		{
			compareResourceOMO("annotations/WidgetField.poc");
		}

		[Test]
		public void testWidgetProps1()
		{
			compareResourceOMO("annotations/WidgetProps1.poc");
		}

		[Test]
		public void testWidgetProps2()
		{
			compareResourceOMO("annotations/WidgetProps2.poc");
		}

		[Test]
		public void testCallback()
		{
			compareResourceOMO("annotations/callback.poc");
		}

		[Test]
		public void testCategory()
		{
			compareResourceOMO("annotations/category.poc");
		}

	}
}

