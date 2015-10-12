using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestSetters : BaseOParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceOSO("setters/getter.poc");
		}

		[Test]
		public void testSetter()
		{
			compareResourceOSO("setters/setter.poc");
		}

	}
}

