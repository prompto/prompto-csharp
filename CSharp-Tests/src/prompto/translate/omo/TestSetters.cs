using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSetters : BaseOParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceOMO("setters/getter.poc");
		}

		[Test]
		public void testSetter()
		{
			compareResourceOMO("setters/setter.poc");
		}

	}
}

