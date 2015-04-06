using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceEOE("setters/getter.pec");
		}

		[Test]
		public void testSetter()
		{
			compareResourceEOE("setters/setter.pec");
		}

	}
}

