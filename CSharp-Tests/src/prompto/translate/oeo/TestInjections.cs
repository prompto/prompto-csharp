// generated: 2015-07-05T23:01:01.283
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestInjections : BaseOParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceOEO("injections/expressionInjection.poc");
		}

	}
}

