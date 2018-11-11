using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.m
{

	[TestFixture]
	public class TestMethods : BaseMParserTest
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
		public void testTextAsync()
		{
			CheckOutput("methods/textAsync.pmc");
		}

		[Test]
		public void testVoidAsync()
		{
			CheckOutput("methods/voidAsync.pmc");
		}

	}
}

