using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestCss : BaseOParserTest
	{

		[Test]
		public void testCodeValue()
		{
			compareResourceOMO("css/codeValue.poc");
		}

		[Test]
		public void testCompositeValue()
		{
			compareResourceOMO("css/compositeValue.poc");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceOMO("css/hyphenName.poc");
		}

		[Test]
		public void testMultiValue()
		{
			compareResourceOMO("css/multiValue.poc");
		}

		[Test]
		public void testNumberValue()
		{
			compareResourceOMO("css/numberValue.poc");
		}

		[Test]
		public void testPixelValue()
		{
			compareResourceOMO("css/pixelValue.poc");
		}

		[Test]
		public void testTextValue()
		{
			compareResourceOMO("css/textValue.poc");
		}

	}
}

