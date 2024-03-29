using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestJsx : BaseOParserTest
	{

		[Test]
		public void testChildElement()
		{
			compareResourceOEO("jsx/childElement.poc");
		}

		[Test]
		public void testCodeAttribute()
		{
			compareResourceOEO("jsx/codeAttribute.poc");
		}

		[Test]
		public void testCodeElement()
		{
			compareResourceOEO("jsx/codeElement.poc");
		}

		[Test]
		public void testDotName()
		{
			compareResourceOEO("jsx/dotName.poc");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceOEO("jsx/empty.poc");
		}

		[Test]
		public void testEmptyAttribute()
		{
			compareResourceOEO("jsx/emptyAttribute.poc");
		}

		[Test]
		public void testFragment()
		{
			compareResourceOEO("jsx/fragment.poc");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceOEO("jsx/hyphenName.poc");
		}

		[Test]
		public void testLiteralAttribute()
		{
			compareResourceOEO("jsx/literalAttribute.poc");
		}

		[Test]
		public void testMethodCall()
		{
			compareResourceOEO("jsx/methodCall.poc");
		}

		[Test]
		public void testMethodRef()
		{
			compareResourceOEO("jsx/methodRef.poc");
		}

		[Test]
		public void testNonAsciiTextElement()
		{
			compareResourceOEO("jsx/nonAsciiTextElement.poc");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			compareResourceOEO("jsx/selfClosingDiv.poc");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			compareResourceOEO("jsx/selfClosingEmptyAttribute.poc");
		}

		[Test]
		public void testTextElement()
		{
			compareResourceOEO("jsx/textElement.poc");
		}

		[Test]
		public void testThisLowerMethodRef()
		{
			compareResourceOEO("jsx/thisLowerMethodRef.poc");
		}

		[Test]
		public void testThisMethodCall()
		{
			compareResourceOEO("jsx/thisMethodCall.poc");
		}

		[Test]
		public void testThisUpperMethodRef()
		{
			compareResourceOEO("jsx/thisUpperMethodRef.poc");
		}

	}
}

