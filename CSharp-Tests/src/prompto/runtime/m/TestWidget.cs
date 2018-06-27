using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.m
{

	[TestFixture]
	public class TestWidget : BaseMParserTest
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
		public void testMinimal()
		{
			CheckOutput("widget/minimal.pmc");
		}

	}
}

