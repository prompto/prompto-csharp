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
		public void testSort1()
		{
			CheckOutput("arrow/sort1.poc");
		}

		[Test]
		public void testSort1_desc()
		{
			CheckOutput("arrow/sort1_desc.poc");
		}

		[Test]
		public void testSort2()
		{
			CheckOutput("arrow/sort2.poc");
		}

		[Test]
		public void testSort2_desc()
		{
			CheckOutput("arrow/sort2_desc.poc");
		}

	}
}

