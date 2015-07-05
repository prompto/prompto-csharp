// generated: 2015-07-05T23:01:01.178
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceEOE("cast/autoDowncast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEOE("cast/castChild.pec");
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

	}
}

