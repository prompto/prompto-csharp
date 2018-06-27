using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestWidget : BaseOParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceOEO("widget/minimal.poc");
		}

	}
}

