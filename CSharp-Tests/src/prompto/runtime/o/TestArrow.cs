using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestArrow : BaseOParserTest
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
		public void testSortCategory1Arg()
		{
			CheckOutput("arrow/sortCategory1Arg.poc");
		}

		[Test]
		public void testSortCategory2Args()
		{
			CheckOutput("arrow/sortCategory2Args.poc");
		}

		[Test]
		public void testSortText1Arg()
		{
			CheckOutput("arrow/sortText1Arg.poc");
		}

		[Test]
		public void testSortText1ArgDesc()
		{
			CheckOutput("arrow/sortText1ArgDesc.poc");
		}

		[Test]
		public void testSortText2Args()
		{
			CheckOutput("arrow/sortText2Args.poc");
		}

		[Test]
		public void testSortText2ArgsDesc()
		{
			CheckOutput("arrow/sortText2ArgsDesc.poc");
		}

	}
}

