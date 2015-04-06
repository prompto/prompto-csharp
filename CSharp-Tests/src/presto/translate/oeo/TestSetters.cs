using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSetters : BaseOParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceOEO("setters/getter.poc");
		}

		[Test]
		public void testSetter()
		{
			compareResourceOEO("setters/setter.poc");
		}

	}
}

