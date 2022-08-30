using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestCast : BaseOParserTest
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
		public void testAutoDowncast()
		{
			CheckOutput("cast/autoDowncast.poc");
		}

		[Test]
		public void testAutoDowncastMethod()
		{
			CheckOutput("cast/autoDowncastMethod.poc");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.poc");
		}

		[Test]
		public void testCastEnum()
		{
			CheckOutput("cast/castEnum.poc");
		}

		[Test]
		public void testCastMethod()
		{
			CheckOutput("cast/castMethod.poc");
		}

		[Test]
		public void testCastMissing()
		{
			CheckOutput("cast/castMissing.poc");
		}

		[Test]
		public void testCastNull()
		{
			CheckOutput("cast/castNull.poc");
		}

		[Test]
		public void testCastParent()
		{
			CheckOutput("cast/castParent.poc");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.poc");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.poc");
		}

		[Test]
		public void testMutableEntity()
		{
			CheckOutput("cast/mutableEntity.poc");
		}

		[Test]
		public void testMutableList()
		{
			CheckOutput("cast/mutableList.poc");
		}

		[Test]
		public void testNullIsNotAText()
		{
			CheckOutput("cast/nullIsNotAText.poc");
		}

	}
}

