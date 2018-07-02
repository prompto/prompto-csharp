using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.m
{

	[TestFixture]
	public class TestCss : BaseMParserTest
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
		public void testCodeValue()
		{
			CheckOutput("css/codeValue.pmc");
		}

		[Test]
		public void testHyphenName()
		{
			CheckOutput("css/hyphenName.pmc");
		}

		[Test]
		public void testMultiValue()
		{
			CheckOutput("css/multiValue.pmc");
		}

		[Test]
		public void testNumberValue()
		{
			CheckOutput("css/numberValue.pmc");
		}

		[Test]
		public void testPixelValue()
		{
			CheckOutput("css/pixelValue.pmc");
		}

		[Test]
		public void testTextValue()
		{
			CheckOutput("css/textValue.pmc");
		}

	}
}

