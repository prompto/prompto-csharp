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

	}
}

