using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestWidget : BaseEParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceEME("widget/minimal.pec");
		}

		[Test]
		public void testNative()
		{
			compareResourceEME("widget/native.pec");
		}

	}
}

