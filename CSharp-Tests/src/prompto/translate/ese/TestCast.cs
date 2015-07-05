// generated: 2015-07-05T23:01:01.179
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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
