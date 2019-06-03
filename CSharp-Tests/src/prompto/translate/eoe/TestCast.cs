using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDecimalCast()
		{
			compareResourceEOE("cast/autoDecimalCast.pec");
		}

		[Test]
		public void testAutoDowncast()
		{
			compareResourceEOE("cast/autoDowncast.pec");
		}

		[Test]
		public void testAutoIntegerCast()
		{
			compareResourceEOE("cast/autoIntegerCast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEOE("cast/castChild.pec");
		}

		[Test]
		public void testCastDecimal()
		{
			compareResourceEOE("cast/castDecimal.pec");
		}

		[Test]
		public void testCastDocument()
		{
			compareResourceEOE("cast/castDocument.pec");
		}

		[Test]
		public void testCastInteger()
		{
			compareResourceEOE("cast/castInteger.pec");
		}

		[Test]
		public void testCastMethod()
		{
			compareResourceEOE("cast/castMethod.pec");
		}

		[Test]
		public void testCastMissing()
		{
			compareResourceEOE("cast/castMissing.pec");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceEOE("cast/castNull.pec");
		}

		[Test]
		public void testCastRoot()
		{
			compareResourceEOE("cast/castRoot.pec");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceEOE("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceEOE("cast/isAText.pec");
		}

		[Test]
		public void testNullisNotAText()
		{
			compareResourceEOE("cast/nullisNotAText.pec");
		}

	}
}

