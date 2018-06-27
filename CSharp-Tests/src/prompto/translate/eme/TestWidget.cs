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

	}
}

