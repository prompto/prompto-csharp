using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestAnnotations : BaseOParserTest
	{

		[Test]
		public void testReactState1()
		{
			compareResourceOMO("annotations/ReactState1.poc");
		}

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
		public void testWidgetChildProps1()
		{
			compareResourceOMO("annotations/WidgetChildProps1.poc");
		}

		[Test]
		public void testWidgetChildProps2()
		{
			compareResourceOMO("annotations/WidgetChildProps2.poc");
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
		public void testWidgetProps10()
		{
			compareResourceOMO("annotations/WidgetProps10.poc");
		}

		[Test]
		public void testWidgetProps11()
		{
			compareResourceOMO("annotations/WidgetProps11.poc");
		}

		[Test]
		public void testWidgetProps12()
		{
			compareResourceOMO("annotations/WidgetProps12.poc");
		}

		[Test]
		public void testWidgetProps13()
		{
			compareResourceOMO("annotations/WidgetProps13.poc");
		}

		[Test]
		public void testWidgetProps2()
		{
			compareResourceOMO("annotations/WidgetProps2.poc");
		}

		[Test]
		public void testWidgetProps3()
		{
			compareResourceOMO("annotations/WidgetProps3.poc");
		}

		[Test]
		public void testWidgetProps4()
		{
			compareResourceOMO("annotations/WidgetProps4.poc");
		}

		[Test]
		public void testWidgetProps5()
		{
			compareResourceOMO("annotations/WidgetProps5.poc");
		}

		[Test]
		public void testWidgetProps6()
		{
			compareResourceOMO("annotations/WidgetProps6.poc");
		}

		[Test]
		public void testWidgetProps7()
		{
			compareResourceOMO("annotations/WidgetProps7.poc");
		}

		[Test]
		public void testWidgetProps8()
		{
			compareResourceOMO("annotations/WidgetProps8.poc");
		}

		[Test]
		public void testWidgetProps9()
		{
			compareResourceOMO("annotations/WidgetProps9.poc");
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

		[Test]
		public void testInlined()
		{
			compareResourceOMO("annotations/inlined.poc");
		}

	}
}

