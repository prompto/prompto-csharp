using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

