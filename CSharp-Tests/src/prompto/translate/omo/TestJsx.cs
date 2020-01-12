using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestJsx : BaseOParserTest
	{

		[Test]
		public void testChildElement()
		{
			compareResourceOMO("jsx/childElement.poc");
		}

		[Test]
		public void testCodeAttribute()
		{
			compareResourceOMO("jsx/codeAttribute.poc");
		}

		[Test]
		public void testCodeElement()
		{
			compareResourceOMO("jsx/codeElement.poc");
		}

		[Test]
		public void testDotName()
		{
			compareResourceOMO("jsx/dotName.poc");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceOMO("jsx/empty.poc");
		}

		[Test]
		public void testEmptyAttribute()
		{
			compareResourceOMO("jsx/emptyAttribute.poc");
		}

		[Test]
		public void testFragment()
		{
			compareResourceOMO("jsx/fragment.poc");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceOMO("jsx/hyphenName.poc");
		}

		[Test]
		public void testLiteralAttribute()
		{
			compareResourceOMO("jsx/literalAttribute.poc");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			compareResourceOMO("jsx/selfClosingDiv.poc");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			compareResourceOMO("jsx/selfClosingEmptyAttribute.poc");
		}

		[Test]
		public void testTextElement()
		{
			compareResourceOMO("jsx/textElement.poc");
		}

	}
}

