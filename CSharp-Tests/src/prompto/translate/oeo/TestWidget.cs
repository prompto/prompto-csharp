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

		[Test]
		public void testNative()
		{
			compareResourceOEO("widget/native.poc");
		}

		[Test]
		public void testWithEvent()
		{
			compareResourceOEO("widget/withEvent.poc");
		}

	}
}

