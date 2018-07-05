using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestWidget : BaseEParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceEOE("widget/minimal.pec");
		}

		[Test]
		public void testNative()
		{
			compareResourceEOE("widget/native.pec");
		}

	}
}

