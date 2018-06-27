using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestWidget : BaseOParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceOMO("widget/minimal.poc");
		}

	}
}

