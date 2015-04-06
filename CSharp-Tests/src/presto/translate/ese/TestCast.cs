using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceESE("cast/autoDowncast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceESE("cast/castChild.pec");
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

