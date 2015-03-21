using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSetters : BaseOParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceOPO("setters/getter.o");
		}

		[Test]
		public void testSetter()
		{
			compareResourceOPO("setters/setter.o");
		}

	}
}

