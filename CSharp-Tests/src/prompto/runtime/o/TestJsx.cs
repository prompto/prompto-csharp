using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestJsx : BaseOParserTest
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
		public void testChildElement()
		{
			CheckOutput("jsx/childElement.poc");
		}

		[Test]
		public void testCodeAttribute()
		{
			CheckOutput("jsx/codeAttribute.poc");
		}

		[Test]
		public void testCodeElement()
		{
			CheckOutput("jsx/codeElement.poc");
		}

		[Test]
		public void testDotName()
		{
			CheckOutput("jsx/dotName.poc");
		}

		[Test]
		public void testEmptyAttribute()
		{
			CheckOutput("jsx/emptyAttribute.poc");
		}

		[Test]
		public void testFragment()
		{
			CheckOutput("jsx/fragment.poc");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("jsx/hyphenName.poc");
		}

		[Test]
		public void testLiteralAttribute()
		{
			CheckOutput("jsx/literalAttribute.poc");
		}

		[Test]
		public void testMethodCall()
		{
			CheckOutput("jsx/methodCall.poc");
		}

		[Test]
		public void testMethodRef()
		{
			CheckOutput("jsx/methodRef.poc");
		}

		[Test]
		public void testNonAsciiTextElement()
		{
			CheckOutput("jsx/nonAsciiTextElement.poc");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			CheckOutput("jsx/selfClosingDiv.poc");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			CheckOutput("jsx/selfClosingEmptyAttribute.poc");
		}

		[Test]
		public void testTextElement()
		{
			CheckOutput("jsx/textElement.poc");
		}

		[Test]
		public void testThisLowerMethodRef()
		{
			CheckOutput("jsx/thisLowerMethodRef.poc");
		}

		[Test]
		public void testThisMethodCall()
		{
			CheckOutput("jsx/thisMethodCall.poc");
		}

		[Test]
		public void testThisUpperMethodRef()
		{
			CheckOutput("jsx/thisUpperMethodRef.poc");
		}

	}
}

