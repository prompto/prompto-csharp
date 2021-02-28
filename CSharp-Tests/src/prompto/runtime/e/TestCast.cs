using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestCast : BaseEParserTest
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
		public void testAutoDecimalCast()
		{
			CheckOutput("cast/autoDecimalCast.pec");
		}

		[Test]
		public void testAutoDowncast()
		{
			CheckOutput("cast/autoDowncast.pec");
		}

		[Test]
		public void testAutoIntegerCast()
		{
			CheckOutput("cast/autoIntegerCast.pec");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.pec");
		}

		[Test]
		public void testCastDecimal()
		{
			CheckOutput("cast/castDecimal.pec");
		}

		[Test]
		public void testCastDocument()
		{
			CheckOutput("cast/castDocument.pec");
		}

		[Test]
		public void testCastDocumentList()
		{
			CheckOutput("cast/castDocumentList.pec");
		}

		[Test]
		public void testCastInteger()
		{
			CheckOutput("cast/castInteger.pec");
		}

		[Test]
		public void testCastMethod()
		{
			CheckOutput("cast/castMethod.pec");
		}

		[Test]
		public void testCastMissing()
		{
			CheckOutput("cast/castMissing.pec");
		}

		[Test]
		public void testCastNull()
		{
			CheckOutput("cast/castNull.pec");
		}

		[Test]
		public void testCastRoot()
		{
			CheckOutput("cast/castRoot.pec");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.pec");
		}

		[Test]
		public void testNullIsNotAText()
		{
			CheckOutput("cast/nullIsNotAText.pec");
		}

	}
}

