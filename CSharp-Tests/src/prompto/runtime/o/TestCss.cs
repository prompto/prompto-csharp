using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestCss : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testCodeValue()
		{
			CheckOutput("css/codeValue.poc");
		}

		[Test]
		public void testCompositeValue()
		{
			CheckOutput("css/compositeValue.poc");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("css/hyphenName.poc");
		}

		[Test]
		public void testMultiValue()
		{
			CheckOutput("css/multiValue.poc");
		}

		[Test]
		public void testNumberValue()
		{
			CheckOutput("css/numberValue.poc");
		}

		[Test]
		public void testPixelValue()
		{
			CheckOutput("css/pixelValue.poc");
		}

		[Test]
		public void testTextValue()
		{
			CheckOutput("css/textValue.poc");
		}

	}
}

