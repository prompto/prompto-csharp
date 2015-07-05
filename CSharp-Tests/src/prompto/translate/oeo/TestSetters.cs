// generated: 2015-07-05T23:01:01.395
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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

