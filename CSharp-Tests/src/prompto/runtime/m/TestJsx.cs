using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.m
{

	[TestFixture]
	public class TestJsx : BaseMParserTest
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
			CheckOutput("jsx/childElement.pmc");
		}

		[Test]
		public void testCodeAttribute()
		{
			CheckOutput("jsx/codeAttribute.pmc");
		}

		[Test]
		public void testCodeElement()
		{
			CheckOutput("jsx/codeElement.pmc");
		}

		[Test]
		public void testDotName()
		{
			CheckOutput("jsx/dotName.pmc");
		}

		[Test]
		public void testEmpty()
		{
			CheckOutput("jsx/empty.pmc");
		}

		[Test]
		public void testEmptyAttribute()
		{
			CheckOutput("jsx/emptyAttribute.pmc");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("jsx/hyphenName.pmc");
		}

		[Test]
		public void testLiteralAttribute()
		{
			CheckOutput("jsx/literalAttribute.pmc");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			CheckOutput("jsx/selfClosingDiv.pmc");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			CheckOutput("jsx/selfClosingEmptyAttribute.pmc");
		}

		[Test]
		public void testTextElement()
		{
			CheckOutput("jsx/textElement.pmc");
		}

	}
}

