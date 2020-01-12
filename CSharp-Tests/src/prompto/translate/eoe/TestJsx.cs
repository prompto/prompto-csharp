using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestJsx : BaseEParserTest
	{

		[Test]
		public void testChildElement()
		{
			compareResourceEOE("jsx/childElement.pec");
		}

		[Test]
		public void testCodeAttribute()
		{
			compareResourceEOE("jsx/codeAttribute.pec");
		}

		[Test]
		public void testCodeElement()
		{
			compareResourceEOE("jsx/codeElement.pec");
		}

		[Test]
		public void testDotName()
		{
			compareResourceEOE("jsx/dotName.pec");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceEOE("jsx/empty.pec");
		}

		[Test]
		public void testEmptyAttribute()
		{
			compareResourceEOE("jsx/emptyAttribute.pec");
		}

		[Test]
		public void testFragment()
		{
			compareResourceEOE("jsx/fragment.pec");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceEOE("jsx/hyphenName.pec");
		}

		[Test]
		public void testLiteralAttribute()
		{
			compareResourceEOE("jsx/literalAttribute.pec");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			compareResourceEOE("jsx/selfClosingDiv.pec");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			compareResourceEOE("jsx/selfClosingEmptyAttribute.pec");
		}

		[Test]
		public void testTextElement()
		{
			compareResourceEOE("jsx/textElement.pec");
		}

	}
}

