using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDecimalCast()
		{
			compareResourceESE("cast/autoDecimalCast.pec");
		}

		[Test]
		public void testAutoDowncast()
		{
			compareResourceESE("cast/autoDowncast.pec");
		}

		[Test]
		public void testAutoIntegerCast()
		{
			compareResourceESE("cast/autoIntegerCast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceESE("cast/castChild.pec");
		}

		[Test]
		public void testCastDecimal()
		{
			compareResourceESE("cast/castDecimal.pec");
		}

		[Test]
		public void testCastInteger()
		{
			compareResourceESE("cast/castInteger.pec");
		}

		[Test]
		public void testCastMissing()
		{
			compareResourceESE("cast/castMissing.pec");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceESE("cast/castNull.pec");
		}

		[Test]
		public void testCastRoot()
		{
			compareResourceESE("cast/castRoot.pec");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceESE("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceESE("cast/isAText.pec");
		}

	}
}

