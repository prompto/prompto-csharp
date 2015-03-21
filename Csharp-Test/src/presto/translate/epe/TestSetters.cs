using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceEPE("setters/getter.e");
		}

		[Test]
		public void testSetter()
		{
			compareResourceEPE("setters/setter.e");
		}

	}
}

