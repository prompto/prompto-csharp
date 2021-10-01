using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestCss : BaseEParserTest
	{

		[Test]
		public void testCodeValue()
		{
			compareResourceEME("css/codeValue.pec");
		}

		[Test]
		public void testCompositeValue()
		{
			compareResourceEME("css/compositeValue.pec");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceEME("css/hyphenName.pec");
		}

		[Test]
		public void testMultiValue()
		{
			compareResourceEME("css/multiValue.pec");
		}

		[Test]
		public void testNumberValue()
		{
			compareResourceEME("css/numberValue.pec");
		}

		[Test]
		public void testPixelValue()
		{
			compareResourceEME("css/pixelValue.pec");
		}

		[Test]
		public void testTextValue()
		{
			compareResourceEME("css/textValue.pec");
		}

	}
}

