using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
	{

		[Test]
		public void testGetter()
		{
			compareResourceESE("setters/getter.pec");
		}

		[Test]
		public void testGetterCall()
		{
			compareResourceESE("setters/getterCall.pec");
		}

		[Test]
		public void testSetter()
		{
			compareResourceESE("setters/setter.pec");
		}

	}
}

