using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestCss : BaseOParserTest
	{

		[Test]
		public void testCodeValue()
		{
			compareResourceOEO("css/codeValue.poc");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceOEO("css/hyphenName.poc");
		}

		[Test]
		public void testMultiValue()
		{
			compareResourceOEO("css/multiValue.poc");
		}

		[Test]
		public void testNumberValue()
		{
			compareResourceOEO("css/numberValue.poc");
		}

		[Test]
		public void testPixelValue()
		{
			compareResourceOEO("css/pixelValue.poc");
		}

		[Test]
		public void testTextValue()
		{
			compareResourceOEO("css/textValue.poc");
		}

	}
}

