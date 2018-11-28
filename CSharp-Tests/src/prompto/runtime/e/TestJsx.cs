using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestJsx : BaseEParserTest
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
			CheckOutput("jsx/childElement.pec");
		}

		[Test]
		public void testCodeAttribute()
		{
			CheckOutput("jsx/codeAttribute.pec");
		}

		[Test]
		public void testCodeElement()
		{
			CheckOutput("jsx/codeElement.pec");
		}

		[Test]
		public void testDotName()
		{
			CheckOutput("jsx/dotName.pec");
		}

		[Test]
		public void testEmptyAttribute()
		{
			CheckOutput("jsx/emptyAttribute.pec");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("jsx/hyphenName.pec");
		}

		[Test]
		public void testLiteralAttribute()
		{
			CheckOutput("jsx/literalAttribute.pec");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			CheckOutput("jsx/selfClosingDiv.pec");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			CheckOutput("jsx/selfClosingEmptyAttribute.pec");
		}

		[Test]
		public void testTextElement()
		{
			CheckOutput("jsx/textElement.pec");
		}

	}
}

