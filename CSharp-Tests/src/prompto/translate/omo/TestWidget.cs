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

		[Test]
		public void testNative()
		{
			compareResourceOMO("widget/native.poc");
		}

		[Test]
		public void testWithEvent()
		{
			compareResourceOMO("widget/withEvent.poc");
		}

	}
}

