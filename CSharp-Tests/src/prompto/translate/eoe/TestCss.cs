using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestCss : BaseEParserTest
	{

		[Test]
		public void testCodeValue()
		{
			compareResourceEOE("css/codeValue.pec");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceEOE("css/hyphenName.pec");
		}

		[Test]
		public void testMultiValue()
		{
			compareResourceEOE("css/multiValue.pec");
		}

		[Test]
		public void testNumberValue()
		{
			compareResourceEOE("css/numberValue.pec");
		}

		[Test]
		public void testPixelValue()
		{
			compareResourceEOE("css/pixelValue.pec");
		}

		[Test]
		public void testTextValue()
		{
			compareResourceEOE("css/textValue.pec");
		}

	}
}

