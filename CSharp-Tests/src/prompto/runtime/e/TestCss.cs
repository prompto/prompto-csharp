using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestCss : BaseEParserTest
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
			CheckOutput("css/codeValue.pec");
		}

		[Test]
		public void testCompositeValue()
		{
			CheckOutput("css/compositeValue.pec");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("css/hyphenName.pec");
		}

		[Test]
		public void testMultiValue()
		{
			CheckOutput("css/multiValue.pec");
		}

		[Test]
		public void testNumberValue()
		{
			CheckOutput("css/numberValue.pec");
		}

		[Test]
		public void testPixelValue()
		{
			CheckOutput("css/pixelValue.pec");
		}

		[Test]
		public void testTextValue()
		{
			CheckOutput("css/textValue.pec");
		}

	}
}

