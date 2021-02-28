using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDecimalCast()
		{
			compareResourceEME("cast/autoDecimalCast.pec");
		}

		[Test]
		public void testAutoDowncast()
		{
			compareResourceEME("cast/autoDowncast.pec");
		}

		[Test]
		public void testAutoIntegerCast()
		{
			compareResourceEME("cast/autoIntegerCast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEME("cast/castChild.pec");
		}

		[Test]
		public void testCastDecimal()
		{
			compareResourceEME("cast/castDecimal.pec");
		}

		[Test]
		public void testCastDocument()
		{
			compareResourceEME("cast/castDocument.pec");
		}

		[Test]
		public void testCastDocumentList()
		{
			compareResourceEME("cast/castDocumentList.pec");
		}

		[Test]
		public void testCastInteger()
		{
			compareResourceEME("cast/castInteger.pec");
		}

		[Test]
		public void testCastMethod()
		{
			compareResourceEME("cast/castMethod.pec");
		}

		[Test]
		public void testCastMissing()
		{
			compareResourceEME("cast/castMissing.pec");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceEME("cast/castNull.pec");
		}

		[Test]
		public void testCastRoot()
		{
			compareResourceEME("cast/castRoot.pec");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceEME("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceEME("cast/isAText.pec");
		}

		[Test]
		public void testNullIsNotAText()
		{
			compareResourceEME("cast/nullIsNotAText.pec");
		}

	}
}

