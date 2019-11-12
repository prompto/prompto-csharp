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
		public void testWidgetChildProps1()
		{
			compareResourceOEO("annotations/WidgetChildProps1.poc");
		}

		[Test]
		public void testWidgetChildProps2()
		{
			compareResourceOEO("annotations/WidgetChildProps2.poc");
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
		public void testWidgetProps10()
		{
			compareResourceOEO("annotations/WidgetProps10.poc");
		}

		[Test]
        [Ignore("Until we implement WidgetPropertiesProcessor")]
        public void testWidgetProps11()
		{
			compareResourceOEO("annotations/WidgetProps11.poc");
		}

		[Test]
		public void testWidgetProps12()
		{
			compareResourceOEO("annotations/WidgetProps12.poc");
		}

		[Test]
		public void testWidgetProps2()
		{
			compareResourceOEO("annotations/WidgetProps2.poc");
		}

		[Test]
		public void testWidgetProps3()
		{
			compareResourceOEO("annotations/WidgetProps3.poc");
		}

		[Test]
		public void testWidgetProps4()
		{
			compareResourceOEO("annotations/WidgetProps4.poc");
		}

		[Test]
		public void testWidgetProps5()
		{
			compareResourceOEO("annotations/WidgetProps5.poc");
		}

		[Test]
		public void testWidgetProps6()
		{
			compareResourceOEO("annotations/WidgetProps6.poc");
		}

		[Test]
		public void testWidgetProps7()
		{
			compareResourceOEO("annotations/WidgetProps7.poc");
		}

		[Test]
		public void testWidgetProps8()
		{
			compareResourceOEO("annotations/WidgetProps8.poc");
		}

		[Test]
		public void testWidgetProps9()
		{
			compareResourceOEO("annotations/WidgetProps9.poc");
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

